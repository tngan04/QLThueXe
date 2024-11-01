using QuanLyThueXe.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyThueXe.Daos
{
    public class VehicleDao
    {
        private readonly QuanLyThueXeContext myDb;

        public VehicleDao()
        {
            myDb = new QuanLyThueXeContext();
        }

        // Lấy tất cả xe
        public List<vehicle> GetAll()
        {
            try
            {
                var vehicles = myDb.Vehicles.ToList();

                // Kiểm tra các giá trị của manufacturer_id
                foreach (var vehicle in vehicles)
                {
                    if (vehicle.manufacturer_id == null)
                    {
                        Console.WriteLine($"Vehicle ID: {vehicle.vehicle_id} has ManufacturerId as NULL");
                    }
                }

                return vehicles;
            }
            catch (Exception ex)
            {
                // Ghi log hoặc in ra thông tin lỗi để kiểm tra
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        // Lấy xe theo ID
        public vehicle GetVehicleById(int id)
        {
            return myDb.Vehicles.FirstOrDefault(x => x.vehicle_id == id);
        }

        // Lấy xe liên quan theo loại
        public List<vehicle> GetVehicleRelated(string type)
        {
            return myDb.Vehicles.Where(x => x.type_vehicle == type).Take(3).ToList();
        }

        // Lấy top 5 xe máy
        public List<vehicle> GetTop5XeMay()
        {
            return myDb.Vehicles.Where(x => x.type_vehicle == "Xe máy").Take(3).ToList();
        }

        // Lấy top 5 xe ô tô
        public List<vehicle> GetTop5Oto()
        {
            return myDb.Vehicles.Where(x => x.type_vehicle == "Xe ô tô").Take(5).ToList();
        }

        // Lấy xe ô tô với phân trang
        public List<vehicle> GetOto(int page, int pageSize, int seatType)
        {
            // Đảm bảo giá trị page và pageSize hợp lệ
            page = Math.Max(1, page);
            pageSize = Math.Max(1, pageSize);

            var query = myDb.Vehicles.Where(v => v.type_vehicle == "Xe ô tô");

            // Lọc theo số chỗ ngồi
            if (seatType == 4 || seatType == 5 || seatType == 7)
            {
                query = query.Where(v => v.number_vehicle == seatType);
            }

            return query
                .OrderBy(v => v.vehicle_id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        // Đếm số lượng xe ô tô theo số chỗ ngồi
        public int GetNumberOto(int seatType)
        {
            var query = myDb.Vehicles.Where(v => v.type_vehicle == "Xe ô tô");

            if (seatType == 4)
            {
                query = query.Where(v => v.number_vehicle == 4); // Lọc xe 4 chỗ
            }
            else if (seatType == 5)
            {
                query = query.Where(v => v.number_vehicle == 5); // Lọc xe 5 chỗ
            }
            else if (seatType == 7)
            {
                query = query.Where(v => v.number_vehicle == 7); // Lọc xe 7 chỗ
            }

            return query.Count();
        }

        // Lấy xe máy với phân trang
        public List<vehicle> GetXeMay(int page, int pageSize)
        {
            page = Math.Max(1, page);
            pageSize = Math.Max(1, pageSize);

            return myDb.Vehicles
                .Where(v => v.type_vehicle == "Xe máy")
                .OrderBy(v => v.vehicle_id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        // Đếm số lượng xe máy
        public int GetNumberXeMay()
        {
            return myDb.Vehicles.Count(v => v.type_vehicle == "Xe máy");
        }

        // Tìm kiếm theo tên
        public List<vehicle> SearchByName(int page, int pageSize, string name)
        {
            return myDb.Vehicles
                       .Where(x => x.name.Contains(name))
                       .OrderBy(x => x.vehicle_id) // Đảm bảo sắp xếp trước khi phân trang
                       .Skip((page - 1) * pageSize)
                       .Take(pageSize)
                       .ToList();
        }

        // Tìm kiếm theo loại
        public List<vehicle> SearchByType(int page, int pageSize, string idType)
        {
            return myDb.Vehicles
                       .Where(x => x.type_vehicle == idType)
                       .OrderBy(x => x.vehicle_id)
                       .Skip((page - 1) * pageSize)
                       .Take(pageSize)
                       .ToList();
        }

        // Đếm số lượng xe theo loại
        public int GetNumberVehicleByType(string idType)
        {
            return myDb.Vehicles.Count(x => x.type_vehicle == idType);
        }

        // Đếm số lượng xe theo tên
        public int GetNumberVehicleByName(string name)
        {
            return myDb.Vehicles.Count(x => x.name.Contains(name));
        }

        // Tìm kiếm theo loại và tên
        public List<vehicle> SearchByTypeAndName(int page, int pageSize, string idType, string name)
        {
            return myDb.Vehicles
                       .Where(x => x.type_vehicle == idType && x.name.Contains(name))
                       .OrderBy(x => x.vehicle_id)
                       .Skip((page - 1) * pageSize)
                       .Take(pageSize)
                       .ToList();
        }

        // Đếm số lượng xe theo tên và loại
        public int GetNumberVehicleByNameAndType(string name, string idType)
        {
            return myDb.Vehicles.Count(x => x.name.Contains(name) && x.type_vehicle == idType);
        }

        // Thêm xe mới
        public void Add(vehicle vehicle)
        {
            myDb.Vehicles.Add(vehicle);
            myDb.SaveChanges();
        }

        // Xóa xe
        public void Delete(int id)
        {
            var obj = myDb.Vehicles.FirstOrDefault(x => x.vehicle_id == id);
            if (obj != null)
            {
                myDb.Vehicles.Remove(obj);
                myDb.SaveChanges();
            }
        }

        // Cập nhật thông tin xe
        public void Update(vehicle vehicle)
        {
            var existingVehicle = myDb.Vehicles.Find(vehicle.vehicle_id);
            if (existingVehicle != null)
            {
                existingVehicle.name = vehicle.name;
                existingVehicle.price = vehicle.price;
                existingVehicle.type_vehicle = vehicle.type_vehicle;
                existingVehicle.quantity = vehicle.quantity;
                existingVehicle.fuel_type = vehicle.fuel_type;
                existingVehicle.image = vehicle.image;
                existingVehicle.description = vehicle.description;
                existingVehicle.number_vehicle = vehicle.number_vehicle;
                existingVehicle.manufacturer_id = vehicle.manufacturer_id;
                myDb.SaveChanges();
            }
        }

        // Lấy tất cả các hãng xe
        public List<Manufacturer> GetAllManufacturer()
        {
            return myDb.Manufacturer.ToList();
        }

        // Cập nhật số lượng xe
        public void UpdateQuantity(vehicle vehicle)
        {
            var obj = myDb.Vehicles.FirstOrDefault(x => x.vehicle_id == vehicle.vehicle_id);
            if (obj != null)
            {
                obj.quantity = vehicle.quantity;
                myDb.SaveChanges();
            }
        }

        // Lấy tất cả xe bất đồng bộ
        public async Task<List<vehicle>> GetAllAsync()
        {
            return await myDb.Vehicles.ToListAsync();
        }
    }
}
