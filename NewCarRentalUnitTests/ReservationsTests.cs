using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using NUnit;
using System.Collections.Generic;
using System.Linq;
using NewCarRental.Models.DAL;
using NewCarRental.Models;

namespace NewCarRentalUnitTests
{
    [TestFixture]
    public class BookingAppTest
    {
        private Reservations _existingReservation;
        private Mock<IReservationRepo> _repository;

        [SetUp]
        public void SetUp()
        {
            _existingReservation = new Reservations
            {
                Id = 1,
                DateFrom = ArivalOn(2018, 11, 24),
                DateTo = DepartOn(2018, 11, 30),
                Reference = "ref1",
                Status = "status"
            };

            _repository = new Mock<IReservationRepo>();
            _repository.Setup(r => r.GetActiveReservations(1)).Returns(new List<Reservations>
            {
              _existingReservation
            }.AsQueryable());
        }

        [Test]
        public void BookingStartsAndFinishesBeforeAnExistingBooking_ReturnEmptyString()
        {
            var result = ReservationHelper.OverlappingReservationsExist(new Reservations
            {
                Id = 2,
                DateFrom = Before(_existingReservation.DateFrom, days: 2),
                DateTo = Before(_existingReservation.DateTo)
            },
            _repository.Object);
            NUnit.Framework.Assert.That(result, Is.Empty);
        }

        [Test]
        public void BookingStartsBeforeExistingBookingAndEndInBookingTime_ReturnRefString()
        {
            var result = ReservationHelper.OverlappingReservationsExist(new Reservations
            {
                Id = 2,
                DateFrom = Before(_existingReservation.DateFrom, days: 2),
                DateTo = Before(_existingReservation.DateTo, days: -1)
            },
            _repository.Object);
            NUnit.Framework.Assert.AreEqual("ref1", result);
        }

        [Test]
        public void BookingStartsInExistingBookingAndEndInBookingTime_ReturnRefString()
        {
            var result = ReservationHelper.OverlappingReservationsExist(new Reservations
            {
                Id = 2,
                DateFrom = _existingReservation.DateFrom,
                DateTo = _existingReservation.DateTo
            },
            _repository.Object);
            NUnit.Framework.Assert.AreEqual("ref1", result);
        }

        [Test]
        public void BookingStartsInExistingBookingAndEndAfterBookingTime_ReturnRefString()
        {
            var result = ReservationHelper.OverlappingReservationsExist(new Reservations
            {
                Id = 2,
                DateFrom = _existingReservation.DateFrom,
                DateTo = After(_existingReservation.DateTo, 2)
            },
            _repository.Object);
            NUnit.Framework.Assert.AreEqual("ref1", result);
        }

        [Test]
        public void BookingStartsAndFinishesAfterAnExistingBooking_ReturnEmptyString()
        {
            var result = ReservationHelper.OverlappingReservationsExist(new Reservations
            {
                Id = 2,
                DateFrom = After(_existingReservation.DateFrom, 10),
                DateTo = After(_existingReservation.DateTo, 10)
            },
            _repository.Object);
            NUnit.Framework.Assert.That(result, Is.Empty);
        }

        [Test]
        public void BookingStartsBeforeExistingBookingAndFinishesAfterExistingBooking_ReturnRefString()
        {
            var result = ReservationHelper.OverlappingReservationsExist(new Reservations
            {
                Id = 2,
                DateFrom = Before(_existingReservation.DateFrom, 10),
                DateTo = After(_existingReservation.DateTo, 10)
            },
            _repository.Object);
            NUnit.Framework.Assert.AreEqual("ref1", result);
        }

        private DateTime ArivalOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 14, 0, 0);
        }

        private DateTime Before(DateTime arrivalDate, int days = 1)
        {
            return arrivalDate.AddDays(-days);
        }

        private DateTime After(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(days);
        }

        private DateTime DepartOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 10, 0, 0);
        }
    }
}
