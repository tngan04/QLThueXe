using QuanLyThueXe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyThueXe.Daos
{
    public class VehicleDao
    {
        QuanLyXeDbContext myDb = new QuanLyXeDbContext();

        public List<vehicle> getAll()
        {
            return myDb.vehicles.ToList();
        }

        public vehicle GetVehicleById(int id)
        {
            return myDb.vehicles.FirstOrDefault(x => x.vehicle_id == id);
        }

        public List<vehicle> GetVehivleRelated(string type)
        {
            return myDb.vehicles.Where(x => x.type_vehicle == type).Take(4).ToList();
        }

        public List<vehicle> GetTop5XeMay()
        {
            return myDb.vehicles.Where(x => x.type_vehicle == "Xe máy").Take(5).ToList();
        }

        public List<vehicle> GetTop5Oto()
        {
            return myDb.vehicles.Where(x => x.type_vehicle == "Xe ô tô").Take(5).ToList();
        }

        public List<vehicle> GetOto(int page, int pagesize)
        {
            return myDb.vehicles.Where(x => x.type_vehicle == "Xe ô tô").ToList().Skip((page - 1) * pagesize).Take(pagesize).ToList();
        }
        public int GetNumberOto()
        {
            int total = myDb.vehicles.Where(x => x.type_vehicle == "Xe ô tô").ToList().Count;
            int count = 0;
            count = total / 10;
            if (total % 10 != 0)
            {
                count++;
            }
            return count;
        }

        public List<vehicle> GetXeMay(int page, int pagesize)
        {
            return myDb.vehicles.Where(x => x.type_vehicle == "Xe máy").ToList().Skip((page - 1) * pagesize).Take(pagesize).ToList();
        }
        public int GetNumberXeMay()
        {
            int total = myDb.vehicles.Where(x => x.type_vehicle == "Xe máy").ToList().Count;
            int count = 0;
            count = total / 10;
            if (total % 10 != 0)
            {
                count++;
            }
            return count;
        }

        public List<vehicle> SearchByName(int page, int pagesize, string name)
        {
            return myDb.vehicles.Where(x =>x.name.Contains(name)).ToList().Skip((page - 1) * pagesize).Take(pagesize).ToList();
        }

        public List<vehicle> SearchByType(int page, int pagesize, string idType)
        {
            return myDb.vehicles.Where(x => x.type_vehicle == idType).ToList().Skip((page - 1) * pagesize).Take(pagesize).ToList();
        }

        public int GetNumberVehicleByType(string idType)
        {
            int total = myDb.vehicles.Where(x =>x.type_vehicle == idType).ToList().Count;
            int count = 0;
            count = total / 10;
            if (total % 10 != 0)
            {
                count++;
            }
            return count;
        }

        public int GetNumberVehicleByName(string name)
        {
            int total = myDb.vehicles.Where(x => x.name.Contains(name)).ToList().Count;
            int count = 0;
            count = total / 10;
            if (total % 10 != 0)
            {
                count++;
            }
            return count;
        }

        public List<vehicle> SearchByTypeAndName(int page, int pagesize, string idType, string name)
        {
            return myDb.vehicles.Where(x =>x.type_vehicle == idType && x.name.Contains(name)).ToList().Skip((page - 1) * pagesize).Take(pagesize).ToList();
        }

        public int GetNumberVehicleByNameAndType(string name, string idType)
        {
            int total = myDb.vehicles.Where(x =>x.name.Contains(name) && x.type_vehicle == idType).ToList().Count;
            int count = 0;
            count = total / 10;
            if (total % 10 != 0)
            {
                count++;
            }
            return count;
        }
    }
}