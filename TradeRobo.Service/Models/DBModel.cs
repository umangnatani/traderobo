using System;
using System.Collections.Generic;
using System.Text;

namespace TradeRobo.Service
{

    public class FavStocks
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string Symbol { get; set; }

        public User User { get; set; }
    }


    public class Menu
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Icon { get; set; }
        public bool Active { get; set; }
        public string Url { get; set; }
        public int? ParentId { get; set; }
        public HashSet<Menu> Children { get; set; }
        public Menu Parent { get; set; }
        public ICollection<RoleMenu> Roles { get; set; } = new List<RoleMenu>();

    }

    public class RoleMenu
    {
        public int Id { get; set; }

        public int RoleId { get; set; }
        public int MenuId { get; set; }

        public Role Role { get; set; }

        public Menu Menu { get; set; }

        public DateTime StartDate { get; set; } = DateTime.Now;

        public DateTime? EndDate { get; set; }

    }

    public class Role
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; } = true;

        public ICollection<UserRole> Users { get; set; } = new List<UserRole>();

        public ICollection<RoleMenu> Menus { get; set; } = new List<RoleMenu>();
    }

    public class UserRole
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public int RoleId { get; set; }

        public Role Role { get; set; }

        public User User { get; set; }

        public DateTime StartDate { get; set; } = DateTime.Now;

        public DateTime? EndDate { get; set; }

    }

    public partial class User
    {
        public int Id { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }
        public string RHToken { get; set; }
        public string RHNumber { get; set; }
        public string TDNumber { get; set; }

        public bool Active { get; set; } = true;

        public ICollection<UserRole> Roles { get; set; } = new List<UserRole>();

        public ICollection<Pie> Pies { get; set; } = new List<Pie>();
        public ICollection<FavStocks> FavStocks { get; set; } = new List<FavStocks>();

    }


    public partial class Pie
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }

        public User User { get; set; }
        public ICollection<PieDetail> PieDetails { get; set; } = new List<PieDetail>();

    }

    public partial class PieDetail
    {
        public int Id { get; set; }
        public int PieId { get; set; }
        public string Symbol { get; set; }
        public double Weight { get; set; }
        public Pie Pie { get; set; }
        public bool Enabled { get; set; } = true;
    }

    public class AppSettings
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }

    }
}
