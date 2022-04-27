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
            return myDb.rentals.ToList();
        }
        public void Add(rental rental)
        {
            myDb.rentals.Add(rental);
            myDb.SaveChanges();
        }
        public rental getRentalID(int id)
        {
            return myDb.rentals.FirstOrDefault(x => x.rental_id == id);
        }
        public List<rental> getRetalUserId(int userId)
        {
            return myDb.rentals.Where(x => x.user_id == userId).ToList();
        }
        public List<rental> getRetalVehicle(int id)
        {
            return myDb.rentals.Where(x => x.vehicle_id == id).ToList();
        }

        public void update(rental rentals)
        {
            var obj = myDb.rentals.FirstOrDefault(x => x.rental_id == rentals.rental_id);
            obj.status = rentals.status;
            myDb.SaveChanges();
        }
    }
}