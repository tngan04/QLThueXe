using QuanLyThueXe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyThueXe.Daos
{
    public class UserDao
    {
        QuanLyXeContext myDb = new QuanLyXeContext();

        public List<user> getAll()
        {
            return myDb.users.ToList();
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

        /*public List<User> getListEmployee() { return myDb.users.Where(x => x.idRole == 1).ToList(); }

        public void add(User user)
        {
            myDb.users.Add(user);
            myDb.SaveChanges();
        }

        public bool checkExistUsername(string userName)
        {
            var obj = myDb.users.FirstOrDefault(x => x.userName == userName);
            if (obj != null) { return true; }
            return false;
        }

        public void delete(int id)
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
    }
}