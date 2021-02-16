using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iParkMedusa.Entities;
using iParkMedusa.Services;
using System.Net.Http;
using System.Net.Http.Json;
using iParkMedusa.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Text;
using iParkMedusa.Services.PaypalService;
using iParkMedusa.Models.PayPalModels;
using System.Globalization;
using Microsoft.Extensions.Options;
using iParkMedusa.Settings;

namespace iParkMedusa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
        
    {
        private readonly TransactionService _service;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly PayPalService _servicePayPal;
        
        public TransactionsController(TransactionService service, UserManager<ApplicationUser> userManager, PayPalService servicePayPal)
        {
            _service = service;
            _userManager = userManager;
            _servicePayPal = servicePayPal;
        }

        // GET: api/Transactions
        [Authorize(Roles="Administrator, Moderator")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions()
        {
            try
            {
                var transactions = await _service.FindAll();
                return Ok(transactions);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Something went wrong. Contact Support.", error = e.Message });
            }
        }

        // GET: api/Transaction/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> GetTransaction(int id)
        {
            try
            {
                var transaction = await _service.GetTransactionById(id);
                if (transaction == null)
                {
                    return NotFound();
                }
                return Ok(transaction);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Something went wrong. Contact Support.", error = e.Message });
            }
        }

        // GET: api/Transaction/User (for regular user)
        // GET: api/Transactiob/User?id=... (for Admin user)
        [HttpGet]
        [Route("~/api/transactions/user")]
        public async Task<ActionResult<List<Transaction>>> GetTransactionByUserId(string userId)
        {
            if (User.IsInRole("Administrator") || User.IsInRole("Moderator"))
            {
                var transactionsAdmin = await _service.GetTransactionsByUserId(userId);
                return transactionsAdmin;
            }
            var userName = _userManager.GetUserId(HttpContext.User);
            var user = _userManager.Users.FirstOrDefault(u => u.UserName == userName);
            var loggedUserId = user.Id;

            var transactions = await _service.GetTransactionsByUserId(loggedUserId);
            return transactions;
        }

        // PUT: api/Transaction/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransaction(int id, Transaction transaction)
        {
            if (id != transaction.Id)
            {
                return BadRequest();
            }

            try
            {
                await _service.UpdateTransaction(transaction);
                return Ok(transaction);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Something went wrong. Contact Support.", error = e.Message });
            }
        }

        // POST: api/Transactions
        [HttpPost]
        public async Task<ActionResult<Transaction>> PostTransaction(Transaction transaction)
        {
            var userName = _userManager.GetUserId(HttpContext.User);
            var user = _userManager.Users.FirstOrDefault(u => u.UserName == userName);
            var id = user.Id;
            try
            {
                await _service.CreateTransaction(transaction, id);
                return CreatedAtAction("GetSlot", new { id = transaction.Id }, transaction); ;
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Something went wrong. Contact Support.", error = e.Message });
            }
        }

        [Authorize]
        [HttpGet]
        [Route("~/api/transactions/user/balance")]
        public async Task<ActionResult<double>> GetBalance()
        {
            var userName = _userManager.GetUserId(HttpContext.User);
            var user = _userManager.Users.FirstOrDefault(u => u.UserName == userName);
            var loggedUserId = user.Id;
            if (loggedUserId != null)
            {
                try
                {
                    var balance = await _service.GetBalanceByUserId(loggedUserId);
                    return Ok(balance);

                }
                catch (Exception e)
                {
                    return BadRequest(new { message = "Something went wrong. Contact Support.", error = e.Message });
                }
            }
            else return Unauthorized();
        }

        [Authorize(Roles = "User")]
        [Route("~/api/transactions/user/addfunds")]
        [HttpPost]
        public async Task<ActionResult> AddFunds(Transaction transaction)
        {
            var userName = _userManager.GetUserId(HttpContext.User);
            var user = _userManager.Users.FirstOrDefault(u => u.UserName == userName);
            var id = user.Id;
            var Funds = await _service.CreateTransaction(transaction, id);

            return Ok(Funds.Value + " were added to user's " + userName + " wallet. New balance = " + Funds.Balance + ".");
        }

        [Authorize(Roles = "User")]
        [Route("~/api/transactions/user/addfunds/paypal")]
        [HttpPost]
        public async Task<ActionResult> AddFundsPaypal(Transaction transaction)
        {
            var userName = _userManager.GetUserId(HttpContext.User);
            var user = _userManager.Users.FirstOrDefault(u => u.UserName == userName);
            var id= user.Id;

            //GET Client
            HttpClient http = _servicePayPal.GetPaypalHttpClient();

            //Get Token
            PayPalTokenModel accessToken = await _servicePayPal.GetPayPalAccessTokenAsync(http);

            //Get Payment
            PayPalPaymentCreatedResponse createdPayment = await _servicePayPal.CreatePaypalPaymentAsync(http, accessToken, transaction);

            var approval_url = createdPayment.links.ElementAt(1).href;
            if (createdPayment != null)
            {
                return Ok(approval_url+"\n Finalize o pagamento e depois chame o ~api/transactions/user/addfunds/paypal/execute?paymentID="+createdPayment.id);
            }

            else return BadRequest();


        }
        [Authorize(Roles = "User")]
        [Route("~/api/transactions/user/addfunds/paypal/execute")]
        [HttpPost]
        public async Task<ActionResult> executeAddFundsPaypal([FromQuery] string paymentID)
        {
            var userName = _userManager.GetUserId(HttpContext.User);
            var user = _userManager.Users.FirstOrDefault(u => u.UserName == userName);
            var id = user.Id;

            //GET Client
            HttpClient http = _servicePayPal.GetPaypalHttpClient();

            //Get Token
            PayPalTokenModel accessToken = await _servicePayPal.GetPayPalAccessTokenAsync(http);
           
            //Executa o pagamento e adiciona fundos
            
            PayPalPaymentExecutedResponse executePayment = await _servicePayPal.ExecutePaypalPaymentAsync(http, accessToken, paymentID);
            if (executePayment != null)
            {
                var Value = executePayment.transactions.FirstOrDefault().amount.total;

                NumberFormatInfo provider = new NumberFormatInfo();

                provider.NumberDecimalSeparator = ".";
                provider.NumberGroupSeparator = ",";

                Transaction transaction = new Transaction()
                {
                    Value = Convert.ToDouble(Value, provider),
                    TransactionTypeId = 2

                };
                var Funds = await _service.CreateTransaction(transaction, id);

                return Ok(Funds.Value +" were added to " + userName + "'s wallet. \n New Balance = " + Funds.Balance+".");
            }

            else return BadRequest();


        }

        [Authorize(Roles = "User")]
        [Route("~/api/transactions/user/addfunds/stripe")]
        [HttpPost]
        public async Task<ActionResult> AddFundsStripe(Transaction transaction)
        {
            var userName = _userManager.GetUserId(HttpContext.User);
            var user = _userManager.Users.FirstOrDefault(u => u.UserName == userName);
            var id = user.Id;
            var StripeModel =  _service.GetStripeModel();
            var token = await _service.GetStripeToken(StripeModel);
            var amount = transaction.Value;
            var payment = _service.CreatePaymentModel(token, amount);
            var response = await _service.PostFundsStripe(payment);
            try
            {
                if (response.StatusCode.Equals(200) && token != null)
                {
                    var balance = await _service.GetBalanceByUserId(id);
                    await _service.CreateTransaction(transaction, id);
                    return Ok(transaction.Value + " were added to user's " + userName + " wallet. New balance = " + balance + ".");
                }
                return BadRequest("Payment not executed. Stripe server error");
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Something went wrong. Contact Support.", error = e.Message });
            }
        }
    }
}
