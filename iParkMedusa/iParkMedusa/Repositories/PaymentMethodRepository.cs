using iParkMedusa.Contexts;
using iParkMedusa.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iParkMedusa.Repositories
{
    public class PaymentMethodRepository : BaseRepository<PaymentMethod>, IPaymentMethodRepository
    {
        public PaymentMethodRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<PaymentMethod>> GetAllPaymentMethodsAsync()
        {
            return await _context.PaymentMethods.ToListAsync();
        }

        public async Task<PaymentMethod> GetPaymentMethodByIdAsync(int id)
        {
            return await _context.PaymentMethods.Where(s => s.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public async Task<int> DeletePaymentMethodByIdAsync(int id)
        {
            var paymentMethod = await _context.PaymentMethods.FindAsync(id);
            _context.PaymentMethods.Remove(paymentMethod);
            return await _context.SaveChangesAsync();
        }
    }
}
