using PaxAPI.Controllers;
using PaxAPI.Entities;
using PaxAPI.Contexts;
using PaxAPI.Repositories;
using PaxAPI.Services;
using PaxAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;


namespace PaxAPITests
{
    public class SlotsControllerTests
    {
        [Fact]
        public async Task GetSlots_ShouldReturnAllSlots()
        {
            // Arrange
            var testContext = Pax_SlotsContext.GetPaxContext("GetSlots");
            var slotsRepository = new SlotRepository(testContext);
            var slotsService = new SlotService(slotsRepository);
            var theController = new SlotsController(slotsService);
            // Act
            var result = await theController.GetSlots();
            // Assert       
            Assert.IsType<OkObjectResult>(result.Result);
            
            /*
            var result = actionResult.Result as OkObjectResult;
            var items = Assert.IsType<List<Slot>>(result.Value);
            Assert.Equal(2, items.Count);*/
        }

        [Fact]
        public async Task GetSlot_ShouldReturnParkById()
        {
            // Arrange
            var testContext = Pax_SlotsContext.GetPaxContext("GetSlot");
            var slotsRepository = new SlotRepository(testContext);
            var slotsService = new SlotService(slotsRepository);
            var theController = new SlotsController(slotsService);
            // Act
            var result = await theController.GetSlot(1);
            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal("A01", result.Value.Locator);
        }

        [Fact]
        public async Task GetSlot_ShouldReturnParkNotFound()
        {
            // Arrange
            var testContext = Pax_SlotsContext.GetPaxContext("GetSlot");
            var slotsRepository = new SlotRepository(testContext);
            var slotsService = new SlotService(slotsRepository);
            var theController = new SlotsController(slotsService);
            // Act
            var result = await theController.GetSlot(999);
            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
    public static class Pax_SlotsContext
    {
        private static ApplicationDbContext SlotsContext;

        public static ApplicationDbContext GetPaxContext(string db)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                            .UseInMemoryDatabase(databaseName: db)
                            .Options;

            SlotsContext = new ApplicationDbContext(options);
            Seed();
            return SlotsContext;
        }

        private static void Seed()
        {
            SlotsContext.Slots.Add(new Slot { Id = 1, Locator = "A01", PricePerHour = 5, StatusId = 1, IsChargingAvailable = true, HasVallet = true, IsCovered = true, IsOutside = false, SocialDistanceFlag = false, IsCovidFreeCertified = true, VehicleType = new List<VehicleType>(), PrioritySlot = true });
            SlotsContext.Slots.Add(new Slot { Id = 2, Locator = "A02", PricePerHour = 5, StatusId = 1, IsChargingAvailable = true, HasVallet = true, IsCovered = true, IsOutside = false, SocialDistanceFlag = false, IsCovidFreeCertified = true, VehicleType = new List<VehicleType>(), PrioritySlot = true });

            SlotsContext.SaveChanges();
        }
    }
}
