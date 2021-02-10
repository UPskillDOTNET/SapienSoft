using iParkMedusa.Entities;
using iParkMedusa.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iParkMedusa.Services
{
    public class PaymentMethodService
    {
        private readonly IPaymentMethodRepository _repo;

        public PaymentMethodService(IPaymentMethodRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<PaymentMethod>> FindAll()
        {
            return await _repo.FindAllAsync();
        }

        public async Task<PaymentMethod> GetPaymentMethodById(int id)
        {
            return await _repo.GetPaymentMethodByIdAsync(id);
        }

        public async Task<int> UpdatePaymentMethod(PaymentMethod paymentMethod)
        {
            return await _repo.UpdateEntityAsync(paymentMethod);
        }

        public async Task<int> AddPaymentMethod(PaymentMethod paymentMethod)
        {
            return await _repo.AddEntityAsync(paymentMethod);
        }

        public async Task<int> DeletePaymentMethodById(int id)
        {
            return await _repo.DeletePaymentMethodByIdAsync(id);
        }
    }
}
