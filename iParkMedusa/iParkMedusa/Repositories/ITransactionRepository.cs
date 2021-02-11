using iParkMedusa.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iParkMedusa.Models;
using System.Net.Http;

namespace iParkMedusa.Repositories
{
    public interface ITransactionRepository : IBaseRepository<Transaction>
    {
        Task<Transaction> GetTransactionByIdAsync(int id);
        Task<List<Transaction>> GetTransactionsByUserId(string userId);
        Task<double> GetBalanceByUserIdAsync(string userId);
        Task<Transaction> AddTransaction(Transaction transaction, string id);
        StripeModel GetStripeModel();
        Task<string> GetStripeToken(StripeModel model);
        PaymentModel CreatePaymentModel(string Token, double amount);
        Task<HttpResponseMessage> PostFundsStripe(PaymentModel model);
        
    }
}
