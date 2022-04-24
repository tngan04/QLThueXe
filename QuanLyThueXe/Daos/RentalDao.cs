using QuanLyThueXe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyThueXe.Daos
{
    public class RentalDao
    {
        QuanLyXeDbContext myDb = new QuanLyXeDbContext();

        public List<rental> getAll()
        {
            return myDb.rentals.ToList();
        }
        public void Add(rental rental)
        {
            myDb.rentals.Add(rental);
            myDb.SaveChanges();
        }

        public List<rental> getRetalUserId(int userId)
        {
            return myDb.rentals.Where(x => x.user_id == userId).ToList();
        }
    }
}