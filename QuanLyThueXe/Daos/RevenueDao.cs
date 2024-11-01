using System.Collections.Generic;
using System.Linq;
using QuanLyThueXe.Models;

namespace QuanLyThueXe.Daos
{
    public class RevenueDao
    {
        private QuanLyThueXeContext myDb = new QuanLyThueXeContext();

        public List<dynamic> GetRevenueStatistics()
        {
            // Fetch and group revenue data by date
            return myDb.Revenue
                .GroupBy(r => r.Date.Date)
                .Select(g => new
                {
                    Date = g.Key,
                    TotalRevenue = g.Sum(r => r.amount)
                })
                .OrderBy(r => r.Date)
                .ToList<dynamic>();
        }
    }
}
