using iParkMedusa.Controllers;
using iParkMedusa.Entities;
using iParkMedusa.Contexts;
using iParkMedusa.Repositories;
using iParkMedusa.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using iParkMedusa.Models;

namespace iParkMedusaTests
{
    public class ParksControllerTests
    {
        [Fact]
        public async Task GetParks_ShouldReturnAllParks()
        {
            // Arrange
            var testContext = iParkMedusa_ParkContext.GetiParkMedusaContext("GetParks");
            var parksRepository = new ParkRepository(testContext);
            var parksService = new ParkService(parksRepository);
            var theController = new ParksController(parksService);
            // Act
            var result = await theController.GetParks();
            // Assert
            var items = Assert.IsType<List<Park>>(result.Value);
            Assert.Equal(2, items.Count);
        }

        [Fact]
        public async Task GetParks_ShouldReturnParkById()
        {
            // Arrange
            var testContext = iParkMedusa_ParkContext.GetiParkMedusaContext("GetPark");
            var parksRepository = new ParkRepository(testContext);
            var parksService = new ParkService(parksRepository);
            var theController = new ParksController(parksService);
            // Act
            var result = await theController.GetPark(1);
            // Assert
            var items = Assert.IsType<Park>(result.Value);
            Assert.Equal("Park", items.Name);
        }

        [Fact]
        public async Task GetParks_ShouldReturnParkNotFound()
        {
            // Arrange
            var testContext = iParkMedusa_ParkContext.GetiParkMedusaContext("GetPark");
            var parksRepository = new ParkRepository(testContext);
            var parksService = new ParkService(parksRepository);
            var theController = new ParksController(parksService);
            // Act
            var result = await theController.GetPark(999);
            // Assert
            var items = Assert.IsType<Park>(result.Value);
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }
    }
    public static class iParkMedusa_ParkContext
    {
         private static ApplicationDbContext ParksContext;

         public static ApplicationDbContext GetiParkMedusaContext(string db)
         {
             var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                             .UseInMemoryDatabase(databaseName: db)
                             .Options;

             ParksContext = new ApplicationDbContext(options);
             Seed();
             return ParksContext;
         }

        private static void Seed()
        {
            ParksContext.Users.Add(new ApplicationUser { FirstName = "João", LastName = "Oliveira", PaymentMethodId = 1 });
            
            ParksContext.Parks.Add(new Park {Id = 1, Name = "Park"});
            ParksContext.Parks.Add(new Park {Id = 2, Name = "Pax"});

            ParksContext.SaveChanges();
        }
    }
}
