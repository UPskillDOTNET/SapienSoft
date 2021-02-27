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
    public class StatusControllerTests
    {
        [Fact]
        public async Task GetStatus_ShouldReturnAllStatus()
        {
            // Arrange
            var testContext = Pax_StatusContext.GetPaxContext("GetStatus");
            var statusRepository = new StatusRepository(testContext);
            var statusService = new StatusService(statusRepository);
            var theController = new StatusController(statusService);
            // Act
            var result = await theController.GetStatus();
            // Assert       
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetSlot_ShouldReturnParkById()
        {
            // Arrange
            var testContext = Pax_StatusContext.GetPaxContext("GetStatus");
            var statusRepository = new StatusRepository(testContext);
            var statusService = new StatusService(statusRepository);
            var theController = new StatusController(statusService);
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
            var testContext = Pax_StatusContext.GetPaxContext("GetStatus");
            var statusRepository = new StatusRepository(testContext);
            var statusService = new StatusService(statusRepository);
            var theController = new StatusController(statusService);
            // Act
            var result = await theController.GetSlot(999);
            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
    public static class Pax_StatusContext
    {
        private static ApplicationDbContext StatusContext;

        public static ApplicationDbContext GetPaxContext(string db)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                            .UseInMemoryDatabase(databaseName: db)
                            .Options;

            StatusContext = new ApplicationDbContext(options);
            Seed();
            return StatusContext;
        }

        private static void Seed()
        {
            StatusContext.Status.Add(new Status { Id = 1, Name = "Available" });
            StatusContext.Status.Add(new Status { Id = 2, Name = "Reserved" });

            StatusContext.SaveChanges();
        }
    }
}
