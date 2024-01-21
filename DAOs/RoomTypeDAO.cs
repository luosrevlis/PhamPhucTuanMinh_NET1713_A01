using BusinessObjects;

namespace DAOs
{
    public class RoomTypeDAO
    {
        private readonly FuMiniHotelManagementContext _db;

        public RoomTypeDAO(FuMiniHotelManagementContext db)
        {
            _db = db;
        }

        public List<RoomType> GetAll()
        {
            return _db.RoomTypes.ToList();
        }
    }
}
