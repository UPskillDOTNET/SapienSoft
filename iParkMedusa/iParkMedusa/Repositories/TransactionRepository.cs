using iParkMedusa.Contexts;
using iParkMedusa.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iParkMedusa.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;

namespace iParkMedusa.Repositories
{
    public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(ApplicationDbContext context) : base(context)
        {
            
        }

        public async Task<Transaction> GetTransactionByIdAsync(int id)
        {
            return await _context.Transactions.Where(s => s.Id.Equals(id)).Include(t => t.TransactionType).FirstOrDefaultAsync();
        }

        public async Task<List<Transaction>> GetTransactionsByUserId(string userId)
        {
            return await _context.Transactions.Where(t => t.UserId == userId).Include(t => t.TransactionType).ToListAsync();
        }

        public async Task<double> GetBalanceByUserIdAsync(string userId)
        {
            var transaction = await _context.Transactions.Where(t => t.UserId == userId).OrderByDescending(p => p.Date).FirstOrDefaultAsync(); 
            if(transaction != null)
            {
                return transaction.Balance;
            }
            else
            {
                return 0;
            }
        }
        public async Task<Transaction> AddTransaction (Transaction transaction, string id)
        {
            var balance = await GetBalanceByUserIdAsync(id);
            var NewTransaction = new Transaction()
            {
                Date = DateTime.Now,
                Value = transaction.Value,
                Balance = balance + transaction.Value,
                TransactionTypeId = transaction.TransactionTypeId,
                UserId = id
            };
            _context.Add(NewTransaction);
            await _context.SaveChangesAsync();
            return NewTransaction;
        }
        public  StripeModel GetStripeModel()
        {
            var model = new StripeModel()
            {
                CardNumber = "4020020806588832",
                Cvc = 123,
                ExpMonth = 8,
                ExpYear = 2022,
                Test = "true"
            };
            return  model;
        }
        public async Task<string> GetStripeToken(StripeModel model)
        {
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
            return Token;
        }
        public PaymentModel CreatePaymentModel(string Token, double amount)
        {
            
            var Payment = new PaymentModel()
            {
                Amount = amount,
                Source = Token,
                Currency = "Eu",
                Stripe_account = "acct_12abcDEF34GhIJ5K",
                Description = "This is only a test",
                Test = "true",
            };
            return Payment;
        }
        public async Task<HttpResponseMessage> PostFundsStripe(PaymentModel model)
        {
            var Json = Newtonsoft.Json.JsonConvert.SerializeObject(model);
            var data = new StringContent(Json, Encoding.UTF8, "application/json");
            var url = "https://noodlio-pay.p.rapidapi.com/charge/token";
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-rapidapi-key", "e86d877af5msh19b8acc8a01228fp14d306jsn02e68f5b3ff1");
            client.DefaultRequestHeaders.Add("x-rapidapi-host", "noodlio-pay.p.rapidapi.com");
           return  await client.PostAsync(url, data);
        }
            

    }
}
