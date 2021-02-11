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

namespace iParkMedusa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly TransactionService _service;
        private readonly UserManager<ApplicationUser> _userManager;

        public TransactionsController(TransactionService service, UserManager<ApplicationUser> userManager)
        {
            _service = service;
            _userManager = userManager;
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
            try
            {
                await _service.AddTransaction(transaction);
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
        public async Task<ActionResult> PostCharge()
        {
            var userName = _userManager.GetUserId(HttpContext.User);
            var user = _userManager.Users.FirstOrDefault(u => u.UserName == userName);
            var loggedUserId = user.Id;
            // Pedido de token
            //Cria Modelo de "registo"
            var model = new StripeModel()
            {
                CardNumber = "4242424242424242",
                Cvc = 123,
                ExpMonth = 8,
                ExpYear = 2022,
                Test = "true"
            };
            //Formata pedido HTTP
            var Json = Newtonsoft.Json.JsonConvert.SerializeObject(model);
            var data = new StringContent(Json, Encoding.UTF8, "application/json");
            var url = "https://noodlio-pay.p.rapidapi.com/tokens/create";
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-rapidapi-key", "e86d877af5msh19b8acc8a01228fp14d306jsn02e68f5b3ff1");
            client.DefaultRequestHeaders.Add("x-rapidapi-host", "noodlio-pay.p.rapidapi.com");
            //Faz Post
            var response = await client.PostAsync(url, data);
            //Recebe Token
            var Token = await response.Content.ReadAsStringAsync();
            //Cria pedido de carregamento com o novo token
            var Payment = new PaymentModel()
            {
                Amount = 100,
                Source = Token,
                Currency = "Eu",
                Stripe_account = "acct_12abcDEF34GhIJ5K",
                Description = "This is only a test",
                Test = "true",
            };
            var NewUrl = "https://noodlio-pay.p.rapidapi.com/charge/token";
            //Faz post de pedido de carregamento
            Task<HttpResponseMessage> response2 = client.PostAsJsonAsync(NewUrl, Payment);
            //New transaction
            var CurrentBalance = await _service.GetBalanceByUserId(loggedUserId);
            var NewTransaction = new Transaction()
            {
                Date = DateTime.Now,
                Value = Payment.Amount,
                Balance = CurrentBalance + Payment.Amount,
                TransactionTypeId = 2,
                UserId = loggedUserId
            };
            await _service.AddTransaction(NewTransaction);
            return Ok( NewTransaction.Value +" were added to user's " + userName + " wallet. New balance = " + NewTransaction.Balance + ".");

        }
    }
}
