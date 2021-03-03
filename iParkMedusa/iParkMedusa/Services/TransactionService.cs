using iParkMedusa.Entities;
using iParkMedusa.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iParkMedusa.Models;
using System.Net.Http;
using System.Text;

namespace iParkMedusa.Services
{
    public class TransactionService
    {
        private readonly ITransactionRepository _repo;

        public TransactionService(ITransactionRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Transaction>> GetTransactions()
        {
            return await _repo.GetTransactionsAsync();
        }

        public async Task<Transaction> GetTransactionById(int id)
        {
            return await _repo.GetTransactionByIdAsync(id);
        }

        public async Task<int> UpdateTransaction(Transaction transaction)
        {
            return await _repo.UpdateEntityAsync(transaction);
        }

        public async Task<Transaction> CreateTransaction(Transaction transaction, string id)
        {

            switch (transaction.TransactionTypeId)
            {

                case 2:
                case 3:
                    {
                        var balance = await _repo.GetBalanceByUserIdAsync(id);
                        var NewTransaction = new Transaction()
                        {
                            Date = DateTime.Now,
                            Value = transaction.Value,
                            Balance = balance + transaction.Value,
                            TransactionTypeId = transaction.TransactionTypeId,
                            UserId = id
                        };
                        await _repo.AddEntityAsync(NewTransaction);
                        return NewTransaction;


                    }

                case 1:
                    {
                        var balance = await _repo.GetBalanceByUserIdAsync(id);
                        var NewTransaction = new Transaction()
                        {
                            Date = DateTime.Now,
                            Value = transaction.Value * -1,
                            Balance = balance - transaction.Value,
                            TransactionTypeId = transaction.TransactionTypeId,
                            UserId = id
                        };
                        await _repo.AddEntityAsync(NewTransaction);
                        return NewTransaction;
                    }
                default: return null;
            }
        }
            public async Task<List<Transaction>> GetTransactionsByUserId(string userId)
        {
            return await _repo.GetTransactionsByUserId(userId);
        }
        public async Task<double> GetBalanceByUserId(string userId)
        {
            var balance = await _repo.GetBalanceByUserIdAsync(userId);
            return balance;
        }
        public StripeModel GetStripeModel()
        {
            var model = new StripeModel()
            {
                CardNumber = "4020020806588832",
                Cvc = 123,
                ExpMonth = 8,
                ExpYear = 2022,
                Test = "true"
            };
            return model;
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
            Task<HttpResponseMessage> response = client.PostAsync(url, data);
            //Recebe Token
            var Token = await response.Result.Content.ReadAsStringAsync();
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
            return await client.PostAsync(url, data);
        }
    }
}
