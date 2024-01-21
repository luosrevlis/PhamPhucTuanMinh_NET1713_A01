using BusinessObjects;
using DAOs;

namespace Repositories.Impl
{
    public class RoomTypeRepository : IRoomTypeRepository
    {
        private readonly RoomTypeDAO _dao;

        public RoomTypeRepository(RoomTypeDAO dao)
        {
            _dao = dao;
        }

        public List<RoomType> GetAllRoomTypes() => _dao.GetAll();
    }
}
