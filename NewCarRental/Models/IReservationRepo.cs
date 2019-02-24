using NewCarRental.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCarRental.Models
{
    public interface IReservationRepo
    {
        IQueryable<Reservations> GetActiveReservations(int? excludedReservationId = null);
    }
}
