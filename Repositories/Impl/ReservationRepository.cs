using BusinessObjects;
using DAOs;

namespace Repositories.Impl
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly ReservationDAO _dao;

        public ReservationRepository(ReservationDAO dao)
        {
            _dao = dao;
        }

        public void AddReservation(BookingReservation reservation) => _dao.Add(reservation);

        public void DeleteReservation(int id) => _dao.Delete(id);

        public BookingReservation? FindReservationById(int id) => _dao.FindById(id);

        public List<BookingReservation> FindReservations(Func<BookingReservation, bool> predicate)
            => _dao.FindByPredicate(predicate);

        public List<BookingReservation> GetAllReservation() => _dao.GetAll();

        public void UpdateReservation(BookingReservation reservation) => _dao.Update(reservation);
    }
}
