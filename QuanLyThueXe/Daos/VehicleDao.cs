using QuanLyThueXe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyThueXe.Daos
{
    public class VehicleDao
    {
        QuanLyXeContext myDb = new QuanLyXeContext();

        public List<vehicle> getAll()
        {
            return myDb.vehicles.ToList();
        }
    }
}