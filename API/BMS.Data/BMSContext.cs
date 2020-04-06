using BMS.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Data
{
    public class BMSContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<StateMaster> StateMaster { get; set; }
        public DbSet<CityMaster> CityMaster { get; set; }
        public DbSet<ClassMaster> ClassMaster { get; set; }
        public DbSet<BookMaster> BookMaster { get; set; }
        public DbSet<BooksClassMapping> BooksClassMapping { get; set; }
        public DbSet<BookSellerMaster> BookSellerMaster { get; set; }
        public DbSet<BookTypeMaster> BookTypeMaster { get; set; }
        public DbSet<DeliveryDetails> DeliveryDetails { get; set; }
        public DbSet<EmployeeMaster> EmployeeMaster { get; set; }
        public DbSet<OrderMaster> OrderMaster { get; set; }
        public DbSet<OrderDetailMaster> OrderDetailMaster { get; set; }
        public DbSet<ParentsMaster> ParentsMaster { get; set; }
        public DbSet<PublisherMaster> PublisherMaster { get; set; }
        public DbSet<SchoolMaster> SchoolMaster { get; set; }
        public DbSet<SessionMaster> SessionMaster { get; set; }
        public DbSet<SellerSchoolMapping> SellerSchoolMapping { get; set; }
        public DbSet<StudentMaster> StudentMaster { get; set; }
        public DbSet<StudentEnrollment> StudentEnrollment { get; set; }
        public DbSet<UserLogin> UserLogin { get; set; }
        public DbSet<EmailAccount> EmailAccount { get; set; }
    }
}
