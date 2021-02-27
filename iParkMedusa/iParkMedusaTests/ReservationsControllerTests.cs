using iParkMedusa.Controllers;
using iParkMedusa.Entities;
using iParkMedusa.Contexts;
using iParkMedusa.Repositories;
using iParkMedusa.Services;
using iParkMedusa.Settings;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System;
using iParkMedusa.Services.ParkingLot;
using Microsoft.Extensions.Options;
using iParkMedusa.Models;
using Microsoft.AspNetCore.Identity;

namespace iParkMedusaTests
{
    public class ReservationsControllerTests
    {
        [Fact]
        public async Task GetReservations_ShouldReturnAllReservations()
        {
            // Arrange
            /*var testContext = iParkMedusa_ParkContext.GetiParkMedusaContext("GetReservations");
            var reservationRepository = new ReservationRepository(testContext);
            var transactionRepository = new TransactionRepository(testContext);
            var reservationService = new ReservationService(reservationRepository, transactionRepository);
            var transactionService = new TransactionService(transactionRepository);
            var parkAPIService = "";
            var paxAPIService = "";
            var applicationUser = new ApplicationUser();
            var userManager = "";
            var theController = new ReservationsController(reservationService, parkAPIService, paxAPIService, userManager, transactionService);
            // Act
            var result = await theController.GetReservations();
            // Assert
            var items = Assert.IsType<List<Park>>(result.Value);
            Assert.Equal(2, items.Count);*/
        }
    }

        public static class iParkMedusa_ReservationsContext
    {
        private static ApplicationDbContext ReservationsContext;

        public static ApplicationDbContext GetiParkMedusaContext(string db)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                            .UseInMemoryDatabase(databaseName: db)
                            .Options;

            ReservationsContext = new ApplicationDbContext(options);
            Seed();
            return ReservationsContext;
        }

        private static void Seed()
        {
            ReservationsContext.Parks.Add(new Park { Id = 1, Name = "Park" });
            ReservationsContext.Parks.Add(new Park { Id = 2, Name = "Pax" });

            ReservationsContext.Users.Add(new ApplicationUser { FirstName = "João", LastName = "Oliveira", PaymentMethodId = 1 });

            ReservationsContext.Reservations.Add(new Reservation { Start = DateTime.Parse("2021-04-01 19:00:00"), End = DateTime.Parse("2021-04-06 12:00:00"), DateCreated = DateTime.Parse("2021-01-01 10:00:00"), ExternalId = 1, Latitude = 30, Longitude = 30, Locator="A02", ParkId=1, SlotId = 1, UserId = "1A", Value=30} );
            ReservationsContext.Reservations.Add(new Reservation { Start = DateTime.Parse("2021-04-01 19:00:00"), End = DateTime.Parse("2021-04-06 12:00:00"), DateCreated = DateTime.Parse("2021-01-01 10:00:00"), ExternalId = 1, Latitude = 30, Longitude = 30, Locator = "B01", ParkId=2, SlotId = 1, UserId = "1B", Value = 35});

            ReservationsContext.SaveChanges();
        }


    }

}
