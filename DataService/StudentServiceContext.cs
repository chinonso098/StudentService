using Microsoft.EntityFrameworkCore;
using StudentService.DomainObjects;
using System;



namespace StudentService.DataService
{
    public class StudentServiceContext: DbContext
    {

        public DbSet<Student> Students { get; set; }

        public DbSet<State> States { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public StudentServiceContext(DbContextOptions<StudentServiceContext> options) : base(options)
        {
            //...
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Student>().HasData(
        //        new Student { StudentID = 1, FirstName = "Nicholas", LastName = "Gunn", Age = 38, StudentNumber = "9580389442", AdmissionDate = new DateTime(2020, 10, 03), DoB = new DateTime(1984, 10, 03), Program = "Music Studies" },
        //        new Student { StudentID = 2, FirstName = "Charles", LastName = "Freiderick", Age = 42, StudentNumber = "9580389442", AdmissionDate = new DateTime(2021, 11, 03), DoB = new DateTime(1979, 10, 03), Program = "Something Something Science" });

        //    modelBuilder.Entity<State>().HasData(
        //        new State { StateID = 1, Name = "NY" },
        //        new State { StateID = 2, Name = "CA" });


        //    modelBuilder.Entity<Address>().HasData(
        //        new Address { AddressID = 1, StudentID = 1, StreetOne = "8564 Fresco Street", StreetTwo = String.Empty, City = "Polmero", StateID = 1, ZipCode = "96542" },
        //        new Address { AddressID = 2, StudentID = 2, StreetOne = "9621 Pinta Lane", StreetTwo = String.Empty, City = "Nina", StateID = 2, ZipCode = "52145" });
        //}



        //private readonly IConfiguration _config;
        //public StudentServiceContext(IConfiguration config)
        //{
        //    _config = config;
        //}

        //public string DbPath { get; private set; }


        //public StudentServiceContext() 
        //{
        //    var folder = Environment.SpecialFolder.LocalApplicationData;
        //    var path = Environment.GetFolderPath(folder);
        //    DbPath = $"{path}{System.IO.Path.DirectorySeparatorChar}studentservice.db";
        //}




        //protected override void OnConfiguring(DbContextOptionsBuilder options) 
        //{
        //    base.OnConfiguring(options);

        //    options.UseSqlServer(_config["ConnectionStrings:AWSConnection"]);
        //}


    }
}
