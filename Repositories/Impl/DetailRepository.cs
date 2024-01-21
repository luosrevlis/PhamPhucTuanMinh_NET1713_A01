using BusinessObjects;
using DAOs;

namespace Repositories.Impl
{
    public class DetailRepository : IDetailRepository
    {
        private readonly DetailDAO _dao;

        public DetailRepository(DetailDAO dao)
        {
            _dao = dao;
        }

        public List<BookingDetail> FindBookingDetails(Func<BookingDetail, bool> predicate) => _dao.FindByPredicate(predicate);
    }
}
