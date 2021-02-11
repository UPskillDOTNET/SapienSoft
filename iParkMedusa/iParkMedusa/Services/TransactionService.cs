using iParkMedusa.Entities;
using iParkMedusa.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iParkMedusa.Models;
using System.Net.Http;

namespace iParkMedusa.Services
{
    public class TransactionService
    {
        private readonly ITransactionRepository _repo;

        public TransactionService(ITransactionRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Transaction>> FindAll()
        {
            return await _repo.FindAllAsync();
        }

        public async Task<Transaction> GetTransactionById(int id)
        {
            return await _repo.GetTransactionByIdAsync(id);
        }

        public async Task<int> UpdateTransaction(Transaction transaction)
        {
            return await _repo.UpdateEntityAsync(transaction);
        }

        public async Task<Transaction> AddTransaction(Transaction transaction, string id)
        {
            return await _repo.AddTransaction(transaction, id);
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
            return _repo.GetStripeModel();
        }
        public async Task<string> GetStripeToken(StripeModel model)
        {
            return await _repo.GetStripeToken(model);
        }

        public PaymentModel CreatePaymentModel(string Token, double amount)
        {
            return  _repo.CreatePaymentModel(Token, amount);
        }
        public Task<HttpResponseMessage> PostFundsStripe(PaymentModel model)
        {
            return  _repo.PostFundsStripe(model);
        }
    }
}
