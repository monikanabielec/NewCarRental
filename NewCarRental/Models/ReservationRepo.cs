using NewCarRental.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewCarRental.Models
{
    public class ReservationRepo : IReservationRepo
    {
        public IQueryable<Reservations> GetActiveReservations(int? excludedReservationId = null)
        {
            var unitOfWork = new UnitOfWork();

            var reservations =
                           unitOfWork.Query<Reservations>()
                           .Where(
                            b => b.Status != "Cancelled");

            if (excludedReservationId.HasValue)
                reservations = reservations.Where(b => b.Id != excludedReservationId.Value);

            return reservations;
        }
    }
}