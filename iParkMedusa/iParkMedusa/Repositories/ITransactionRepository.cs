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
        Task<List<Transaction>> GetTransactionsAsync();
        Task<Transaction> GetTransactionByIdAsync(int id);
        Task<List<Transaction>> GetTransactionsByUserId(string userId);
        Task<double> GetBalanceByUserIdAsync(string userId);
        
       
        
    }
}
