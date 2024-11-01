using QuanLyThueXe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyThueXe.Daos
{
    public class RentalDao
    {
        QuanLyThueXeContext myDb = new QuanLyThueXeContext();

        public List<rental> getAll()
        {
            return myDb.Rentals.ToList();
        }
        public void Add(rental rental)
        {
            myDb.Rentals.Add(rental);
            myDb.SaveChanges();
        }
        public rental getRentalID(int id)
        {
            return myDb.Rentals.FirstOrDefault(x => x.rental_id == id);
        }
        public List<rental> getRetalUserId(int userId)
        {
            return myDb.Rentals.Where(x => x.user_id == userId).ToList();
        }
        public List<rental> getRetalVehicle(int id)
        {
            return myDb.Rentals.Where(x => x.vehicle_id == id).ToList();
        }

        public void update(rental Rentals)
        {
            var obj = myDb.Rentals.FirstOrDefault(x => x.rental_id == Rentals.rental_id);
            obj.status = Rentals.status;
            myDb.SaveChanges();
        }
    }
}