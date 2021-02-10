using iParkMedusa.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iParkMedusa.Repositories
{
    public interface IPaymentMethodRepository : IBaseRepository<PaymentMethod>
    {
        Task<PaymentMethod> GetPaymentMethodByIdAsync(int id);
        Task<int> DeletePaymentMethodByIdAsync(int id);
    }
}
