using iParkMedusa.Contexts;
using iParkMedusa.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    }
}
