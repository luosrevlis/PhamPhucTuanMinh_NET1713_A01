using BusinessObjects;
using DAOs;

namespace Repositories.Impl
{
    public class RoomRepository : IRoomRepository
    {
        private readonly RoomDAO _dao;

        public RoomRepository(RoomDAO dao)
        {
            _dao = dao;
        }

        public void AddRoom(RoomInformation roomInformation) => _dao.Add(roomInformation);

        public void DeleteRoom(int id) => _dao.Delete(id);

        public RoomInformation? FindRoomById(int id) => _dao.FindById(id);

        public List<RoomInformation> FindRooms(Func<RoomInformation, bool> predicate) => _dao.FindByPredicate(predicate);

        public List<RoomInformation> GetAllRooms() => _dao.GetAll();

        public void UpdateRoom(RoomInformation roomInformation) => _dao.Update(roomInformation);
    }
}
