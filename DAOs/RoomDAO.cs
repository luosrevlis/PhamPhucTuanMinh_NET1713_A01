using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DAOs
{
    public class RoomDAO
    {
        private readonly FuMiniHotelManagementContext _db;

        public RoomDAO(FuMiniHotelManagementContext db)
        {
            _db = db;
        }

        public List<RoomInformation> GetAll()
        {
            return _db.RoomInformations
                .Where(info => info.RoomStatus != (byte)Status.Deleted)
                .Include(info => info.RoomType)
                .OrderBy(info => info.RoomNumber)
                .ToList();
        }

        public RoomInformation? FindById(int id)
        {
            var info = _db.RoomInformations
                .FirstOrDefault(info => info.RoomId == id && info.RoomStatus != (byte)Status.Deleted);
            if (info == null)
            {
                return null;
            }
            info.RoomType = _db.RoomTypes.Find(info.RoomTypeId)!;
            return info;
        }

        public List<RoomInformation> FindByPredicate(Func<RoomInformation, bool> predicate)
        {
            return _db.RoomInformations
                .Include(info => info.RoomType)
                .Where(predicate)
                .OrderBy(info => info.RoomNumber)
                .ToList();
        }

        public void Add(RoomInformation roomInformation)
        {
            _db.RoomInformations.Add(roomInformation);
            _db.SaveChanges();
        }

        public void Update(RoomInformation roomInformation)
        {
            _db.RoomInformations.Update(roomInformation);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            var room = FindById(id);
            if (room == null)
            {
                return;
            }
            room.RoomStatus = (byte)Status.Deleted;
            Update(room);
        }
    }
}
