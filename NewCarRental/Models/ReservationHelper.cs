using NewCarRental.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewCarRental.Models
{
    public class ReservationHelper
    {
        public static string OverlappingReservationsExist(Reservations reservation, IReservationRepo _repository)
        {
            if (reservation.Status == "Cancelled")
                return string.Empty;

            var reservations = _repository.GetActiveReservations(1);

            var overlappingReservation =
            reservations.FirstOrDefault(
            b =>
            reservation.DateFrom < b.DateTo && b.DateFrom < reservation.DateTo);

            return overlappingReservation == null ? string.Empty
            : overlappingReservation.Reference;
        }
    }
}
