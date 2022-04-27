using QuanLyThueXe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyThueXe.Daos
{
    public class VehicleDao
    {
        QuanLyThueXeContext myDb = new QuanLyThueXeContext();

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
            return myDb.vehicles.Where(x => x.type_vehicle == type).Take(3).ToList();
        }

        public List<vehicle> GetTop5XeMay()
        {
            return myDb.vehicles.Where(x => x.type_vehicle == "Xe máy").Take(3).ToList();
        }

        public List<vehicle> GetTop5Oto()
        {
            return myDb.vehicles.Where(x => x.type_vehicle == "Xe ô tô").Take(3).ToList();
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

        public void add(vehicle vehicles)
        {
            myDb.vehicles.Add(vehicles);
            myDb.SaveChanges();
        }

        public void delete(int id)
          {
              var obj = myDb.vehicles.FirstOrDefault(x => x.vehicle_id == id);
              myDb.vehicles.Remove(obj);
              myDb.SaveChanges();
          }

        /*public void delete(int id)
        {
            var obj = myDb.vouchers.FirstOrDefault(x => x.voucher_id == id);
            obj.status = 0;
            myDb.SaveChanges();
        }*/

        public void update(vehicle vehicles)
        {
            var obj = myDb.vehicles.FirstOrDefault(x => x.vehicle_id == vehicles.vehicle_id);
            obj.name = vehicles.name;
            obj.image = vehicles.image;
            obj.type_vehicle = vehicles.type_vehicle;
            obj.price = vehicles.price;
            obj.quantity = vehicles.quantity;
            obj.description = vehicles.description;
            myDb.SaveChanges();
        }
        public void updateQuantity(vehicle vehicles)
        {
            var obj = myDb.vehicles.FirstOrDefault(x => x.vehicle_id == vehicles.vehicle_id);
            obj.quantity = vehicles.quantity;
            myDb.SaveChanges();
        }
    }
}