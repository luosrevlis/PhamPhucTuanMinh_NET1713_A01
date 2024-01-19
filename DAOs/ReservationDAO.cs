using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DAOs
{
    public class ReservationDAO
    {
        private readonly FuMiniHotelManagementContext _db;

        public ReservationDAO(FuMiniHotelManagementContext db)
        {
            _db = db;
        }

        public List<BookingReservation> GetAll() => _db.BookingReservations.Include(res => res.Customer).ToList();

        public BookingReservation? FindById(int id)
        {
            var res = _db.BookingReservations.Find(id);
            if (res == null)
            {
                return null;
            }
            res.Customer = _db.Customers.Find(res.CustomerId)!;
            res.BookingDetails = _db.BookingDetails
                .Where(details => details.BookingReservationId == res.BookingReservationId).ToList();
            return res;
        }

        public List<BookingReservation> FindByPredicate(Func<BookingReservation, bool> predicate)
            => _db.BookingReservations.Where(predicate).ToList();

        public void Add(BookingReservation reservation)
        {
            _db.BookingReservations.Add(reservation);
            _db.SaveChanges();
        }

        public void Update(BookingReservation reservation)
        {
            _db.BookingReservations.Update(reservation);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            var res = FindById(id);
            if (res == null)
            {
                return;
            }
            _db.BookingReservations.Remove(res);
            _db.SaveChanges();
        }
    }
}
