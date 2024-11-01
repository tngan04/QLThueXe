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
            return myDb.Vouchers.Where(x => x.status == 1).ToList();
        }

        public void add(voucher Vouchers)
        {
            myDb.Vouchers.Add(Vouchers);
            myDb.SaveChanges();
        }

      /*  public void delete(int id)
        {
            var obj = myDb.Vouchers.FirstOrDefault(x => x.voucher_id == id);
            myDb.Vouchers.Remove(obj);
            myDb.SaveChanges();
        }*/

        public void delete(int id)
        {
            var obj = myDb.Vouchers.FirstOrDefault(x => x.voucher_id == id);
            obj.status = 0;
            myDb.SaveChanges();
        }

        public void update(voucher Vouchers)
        {
            var obj = myDb.Vouchers.FirstOrDefault(x => x.voucher_id == Vouchers.voucher_id);
            obj.name = Vouchers.name;
            obj.discount = Vouchers.discount;
            obj.quantity = Vouchers.quantity;
            obj.date_expire = Vouchers.date_expire;
            myDb.SaveChanges();
        }

        public voucher GetVoucherByName(string name)
        {
            return myDb.Vouchers.FirstOrDefault(x => x.name == name);
        }
    }
}