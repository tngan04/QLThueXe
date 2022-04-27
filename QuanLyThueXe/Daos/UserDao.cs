using QuanLyThueXe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace QuanLyThueXe.Daos
{
    public class UserDao
    {
        QuanLyThueXeContext myDb = new QuanLyThueXeContext();

        public List<user> getAll()
        {
            return myDb.users.ToList();
        }
        public List<user> getAllUser()
        {
            return myDb.users.OrderBy(x => x.role_id).ToList();
        }
        public bool checkLogin(string email, string password)
        {
            var obj = myDb.users.FirstOrDefault(x => x.email == email && x.password == password);
            if (obj == null) { return false; }
            return true;
        }

        public user getUserByEmail(string email)
        {
            return myDb.users.FirstOrDefault(x => x.email.Equals(email));
        }

        public user getInfor(int id)
        {
            return myDb.users.FirstOrDefault(x => x.user_id == id);
        }

        public void add(user user)
        {
            myDb.users.Add(user);
            myDb.SaveChanges();
        }

        public bool checkExistEmail(string email)
        {
            var obj = myDb.users.FirstOrDefault(x => x.email == email);
            if (obj != null) { return true; }
            return false;
        }

      /*  public void delete(int id)
        {
            var obj = myDb.users.FirstOrDefault(x => x.idUser == id);
            myDb.users.Remove(obj);
            myDb.SaveChanges();
        }

        public void update(User user)
        {
            var obj = myDb.users.FirstOrDefault(x => x.idUser == user.idUser);
            obj.fullName = user.fullName;
            obj.userName = user.userName;
            obj.password = user.password;
            obj.address = user.address;
            obj.phoneNumber = user.phoneNumber;
            obj.gender = user.gender;
            obj.idGroup = user.idGroup;
            myDb.SaveChanges();
        }*/

        public string md5(string password)
        {
            MD5 md = MD5.Create();
            byte[] inputString = System.Text.Encoding.ASCII.GetBytes(password);
            byte[] hash = md.ComputeHash(inputString);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x"));
            }
            return sb.ToString();
        }
    }
}