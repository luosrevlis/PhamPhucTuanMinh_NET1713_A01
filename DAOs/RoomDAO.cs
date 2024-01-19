﻿using BusinessObjects;
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

        public List<RoomInformation> GetAll() => _db.RoomInformations.Include(info => info.RoomType).ToList();

        public RoomInformation? FindById(int id)
        {
            var info = _db.RoomInformations.Find(id);
            if (info == null)
            {
                return null;
            }
            info.RoomType = _db.RoomTypes.Find(info.RoomTypeId)!;
            return info;
        }

        public List<RoomInformation> FindByPredicate(Func<RoomInformation, bool> predicate)
            => _db.RoomInformations.Where(predicate).ToList();

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
            _db.RoomInformations.Remove(room);
            _db.SaveChanges();
        }
    }
}