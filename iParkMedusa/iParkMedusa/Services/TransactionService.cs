using iParkMedusa.Entities;
using iParkMedusa.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<int> AddTransaction(Transaction transaction)
        {
            return await _repo.AddEntityAsync(transaction);
        }

        public async Task<List<Transaction>> GetTransactionsByUserId (string userId)
        {
            return await _repo.GetTransactionsByUserId(userId);
        }
        public async Task<double> GetBalanceByUserId(string userId)
        {
                var balance = await _repo.GetBalanceByUserIdAsync(userId);
                return balance;
        }
    }
}
