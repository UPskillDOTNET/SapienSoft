using OKAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OKAPI.Data
{
    public class SqlReservationData : IReservationData
    {
        private Context _reservationContext;
        public SqlReservationData(Context reservationContext) 
        {
            _reservationContext = reservationContext;
        }

        public Reservation AddReservation(Reservation reservation)
        {
            throw new NotImplementedException();
        }

        public void DeleteReservation(Reservation reservation)
        {
            throw new NotImplementedException();
        }

        public Reservation EditReservation(Reservation reservation)
        {
            throw new NotImplementedException();
        }

        public Reservation GetReservation(Guid id)
        {
            var reservation = _reservationContext.Reservations.Find(id);
            return reservation;
        }

        public List<Reservation> GetReservations()
        {
            return _reservationContext.Reservations.ToList();
        }
    }
}
