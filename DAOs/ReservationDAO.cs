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

        public List<BookingReservation> GetAll()
        {
            return _db.BookingReservations
                .Where(res => res.BookingStatus != (byte)Status.Deleted)
                .Include(res => res.Customer)
                .OrderBy(res => res.Customer.CustomerFullName)
                .ToList();
        }

        public BookingReservation? FindById(int id)
        {
            var res = _db.BookingReservations
                .FirstOrDefault(res => res.BookingReservationId == id && res.BookingStatus != (byte)Status.Deleted);
            if (res == null)
            {
                return null;
            }
            res.Customer = _db.Customers.Find(res.CustomerId)!;
            return res;
        }

        public List<BookingReservation> FindByPredicate(Func<BookingReservation, bool> predicate)
        {
            return _db.BookingReservations
                .Include(res => res.Customer)
                .Where(predicate)
                .OrderBy(res => res.Customer.CustomerFullName)
                .ToList();
        }

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
            res.BookingStatus = (byte)Status.Deleted;
            Update(res);
        }
    }
}
