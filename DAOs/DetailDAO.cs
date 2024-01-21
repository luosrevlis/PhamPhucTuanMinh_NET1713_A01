using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DAOs
{
    public class DetailDAO
    {
        private readonly FuMiniHotelManagementContext _db;
        
        public DetailDAO(FuMiniHotelManagementContext db)
        {
            _db = db;
        }

        public List<BookingDetail> FindByPredicate(Func<BookingDetail, bool> predicate)
        {
            return _db.BookingDetails
                .Include(details => details.Room)
                .Where(predicate)
                .OrderBy(details => details.Room.RoomNumber)
                .ToList();
        }
    }
}
