using QuanLyThueXe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyThueXe.Daos
{
    public class RentalDao
    {
        QuanLyXeContext myDb = new QuanLyXeContext();

        public List<rental> getAll()
        {
            return myDb.rentals.ToList();
        }
    }
}