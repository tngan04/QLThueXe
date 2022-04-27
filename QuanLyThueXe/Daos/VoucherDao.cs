using QuanLyThueXe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyThueXe.Daos
{
    public class VoucherDao
    {
        QuanLyThueXeContext myDb = new QuanLyThueXeContext();

        public List<voucher> getAll()
        {
            return myDb.vouchers.Where(x => x.status == 1).ToList();
        }

        public void add(voucher vouchers)
        {
            myDb.vouchers.Add(vouchers);
            myDb.SaveChanges();
        }

      /*  public void delete(int id)
        {
            var obj = myDb.vouchers.FirstOrDefault(x => x.voucher_id == id);
            myDb.vouchers.Remove(obj);
            myDb.SaveChanges();
        }*/

        public void delete(int id)
        {
            var obj = myDb.vouchers.FirstOrDefault(x => x.voucher_id == id);
            obj.status = 0;
            myDb.SaveChanges();
        }

        public void update(voucher vouchers)
        {
            var obj = myDb.vouchers.FirstOrDefault(x => x.voucher_id == vouchers.voucher_id);
            obj.name = vouchers.name;
            obj.discount = vouchers.discount;
            obj.quantity = vouchers.quantity;
            obj.date_expire = vouchers.date_expire;
            myDb.SaveChanges();
        }

        public voucher GetVoucherByName(string name)
        {
            return myDb.vouchers.FirstOrDefault(x => x.name == name);
        }
    }
}