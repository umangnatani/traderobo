using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TradeRobo.Service
{

    public enum Strategy
    {
        Buyback = 1,
        Pie = 2,
        Staggerd = 3,
        Multi = 4
    }

    public partial class OrderGroup : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int? PieId { get; set; }

        [Column(TypeName = "decimal(16,4)")]
        public decimal Amount { get; set; }
        public string Broker { get; set; }
        public string Side { get; set; } = "buy";
        public Strategy? Strategy { get; set; }
        public DateTime StartTime { get; set; } = DateTime.Now;

        public DateTime EndTime { get; set; } = DateTime.Now;

        public User User { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }


    public partial class Order : IEntity
    {
        public int Id { get; set; }

        public int OrderGroupId { get; set; }

        public string Symbol { get; set; }

        [Column(TypeName = "decimal(10,4)")]
        public decimal Quantity { get; set; } = 10;

        [Column(TypeName = "decimal(16,4)")]
        public decimal Amount { get; set; }

        [Column(TypeName = "decimal(10,4)")]
        public decimal Price { get; set; }
        public DateTime TimeStamp { get; set; } = DateTime.Now;
        public DateTime? CoverTimeStamp { get; set; }
        public bool Success { get; set; }
        public string ExecMessage { get; set; }

        public OrderGroup OrderGroup { get; set; }

        [NotMapped]
        public Quote Quote { get; set; }

        [NotMapped]
        public string TDAccountId { get; set; }

    }


   

    public class Menu : IEntity
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

    public class RoleMenu : IEntity
    {
        public int Id { get; set; }

        public int RoleId { get; set; }
        public int MenuId { get; set; }

        public Role Role { get; set; }

        public Menu Menu { get; set; }

        public DateTime StartDate { get; set; } = DateTime.Now;

        public DateTime? EndDate { get; set; }

    }

    public class Role : IEntity
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; } = true;

        public ICollection<UserRole> Users { get; set; } = new List<UserRole>();

        public ICollection<RoleMenu> Menus { get; set; } = new List<RoleMenu>();
    }

    public class UserRole : IEntity
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public int RoleId { get; set; }

        public Role Role { get; set; }

        public User User { get; set; }

        public DateTime StartDate { get; set; } = DateTime.Now;

        public DateTime? EndDate { get; set; }

    }

    public class UserConfig : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public bool Editable { get; set; } = false;
        public User User { get; set; }


    }


    public class TDAccount : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string AccountName { get; set; }
        public string AccountId { get; set; }


    }

    public partial class User: IEntity
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }

        public bool Active { get; set; } = true;

        public ICollection<UserRole> Roles { get; set; } = new List<UserRole>();

        public ICollection<Pie> Pies { get; set; } = new List<Pie>();

        public ICollection<OrderGroup> TradeBatch { get; set; } = new List<OrderGroup>();

        public ICollection<UserConfig> UserConfig { get; set; } = new List<UserConfig>();


    }


    public partial class Pie: IEntity
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public int? SortOrder { get; set; }

        public User User { get; set; }
        public ICollection<PieDetail> PieDetails { get; set; } = new List<PieDetail>();

        public ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

    }



    public partial class Schedule : IEntity
    {
        public int Id { get; set; }
        public int PieId { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Amount { get; set; }
        public bool MarketWeight { get; set; }
        public Pie Pie { get; set; }
        public bool Enabled { get; set; } = true;
    }


    public partial class PieDetail : IEntity
    {
        public int Id { get; set; }
        public int PieId { get; set; }
        public string Symbol { get; set; }

        [Column(TypeName = "decimal(10,4)")]
        public decimal Weight { get; set; }
        public Pie Pie { get; set; }
        public bool Enabled { get; set; } = true;
    }

    public class AppSettings : IEntity
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }

    }
}
