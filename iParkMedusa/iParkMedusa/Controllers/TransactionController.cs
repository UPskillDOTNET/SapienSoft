using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iParkMedusa.Controllers;
using iParkMedusa.Entities;
using iParkMedusa.Services;


using iParkMedusa.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace iParkMedusa.Controllers
{
    public class TransactionController : ControllerBase
    {
        private readonly TransactionService _service;
        private readonly UserManager<ApplicationUser> _userManager;

        public TransactionController(TransactionService service, UserManager<ApplicationUser> userManager)
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
        [HttpGet]
        [Route("~/api/transactions/user/balance")]
        public async Task<ActionResult<double>> GetBalance(string userId)
        {
            try
            {
                var transactions = await _service.GetTransactionsByUserId(userId);
                var balance =(from x in transactions orderby x.Date descending select x).FirstOrDefault();
                return balance.Balance;
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Something went wrong. Contact Support.", error = e.Message });
            }
        }
    }
}
