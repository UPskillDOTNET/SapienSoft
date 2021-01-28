using OKAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OKAPI.Data
{
    public interface IData
    {

        // Slot

        List<Slot> GetSlots();

        Slot GetSlot(Guid id);

        Slot AddSlot(Slot slot);

        void DeleteSlot(Slot slot);

        Slot EditSlot(Slot slot);

        // Slot
          
    }

    public interface IReservationData
    {

        // Reservation

        List<Reservation> GetReservations();

        Reservation GetReservation(Guid id);

        Reservation AddReservation(Reservation reservation);

        void DeleteReservation(Reservation reservation);

        Reservation EditReservation(Reservation reservation);

        // Reservation

    }
}
