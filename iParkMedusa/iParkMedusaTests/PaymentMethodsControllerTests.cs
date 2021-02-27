using iParkMedusa.Controllers;
using iParkMedusa.Entities;
using iParkMedusa.Contexts;
using iParkMedusa.Repositories;
using iParkMedusa.Services;
using iParkMedusa.Constants;
using iParkMedusa.Models;
using iParkMedusa.Settings;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace iParkMedusaTests
{
    public class PaymentMethodsControllerTests
    {
        [Fact]
        public async Task GetPaymentMethods_ShouldReturnAllPaymentMethods()
        {
            // Arrange
            var testContext = iParkMedusa_PaymentMethodsContext.GetiParkMedusaContext("GetPaymentMethods");
            var paymentMethodRepository = new PaymentMethodRepository(testContext);
            var paymentMethodService = new PaymentMethodService(paymentMethodRepository);
            var theController = new PaymentMethodsController(paymentMethodService);
            // Act
            var result = await theController.GetPaymentMethods();
            // Assert
            var items = Assert.IsType<List<PaymentMethod>>(result.Value);
            Assert.Equal(2, items.Count);
        }

        [Fact]
        public async Task GetPaymentMethods_ShouldReturnPaymentMethodById()
        {
            // Arrange
            var testContext = iParkMedusa_PaymentMethodsContext.GetiParkMedusaContext("GetPaymentMethod");
            var parksRepository = new PaymentMethodRepository(testContext);
            var parksService = new PaymentMethodService(parksRepository);
            var theController = new PaymentMethodsController(parksService);
            // Act
            var result = await theController.GetPaymentMethod(1);
            // Assert
            var items = Assert.IsType<PaymentMethod>(result.Value);
            Assert.Equal("Numerário", items.Name);
        }

        [Fact]
        public async Task GetPaymentMethods_ShouldReturnPaymentMethodNotFound()
        {
            // Arrange
            var testContext = iParkMedusa_PaymentMethodsContext.GetiParkMedusaContext("GetPaymentMethod");
            var parksRepository = new PaymentMethodRepository(testContext);
            var parksService = new PaymentMethodService(parksRepository);
            var theController = new PaymentMethodsController(parksService);
            // Act
            var result = await theController.GetPaymentMethod(999);
            // Assert
            var items = Assert.IsType<PaymentMethod>(result.Value);
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }
    }
    public static class iParkMedusa_PaymentMethodsContext
    {
         private static ApplicationDbContext dbContext;

         public static ApplicationDbContext GetiParkMedusaContext(string db)
         {
             var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                             .UseInMemoryDatabase(databaseName: db)
                             .Options;

             dbContext = new ApplicationDbContext(options);
             Seed();
             return dbContext;
         }
         private static void Seed()
         {
             dbContext.PaymentMethods.Add(new PaymentMethod { Id = 1, Name = "Numerário" });
             dbContext.PaymentMethods.Add(new PaymentMethod { Id = 2, Name = "MBWay" });

             dbContext.SaveChanges();
         }
    }
}
