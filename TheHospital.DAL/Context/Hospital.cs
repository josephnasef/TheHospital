using System;
using System.Data.Entity;
using System.Linq;
using TheHospital.DAL.Models;

namespace TheHospital.DAL.Context
{
    public class Hospital : DbContext
    {
        // Your context has been configured to use a 'Hospital' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'TheHospital.DAL.Context.Hospital' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'Hospital' 
        // connection string in the application configuration file.
        public Hospital()
            : base("name=Hospital")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

         public virtual DbSet<clinic> Clinics { get; set; }
         public virtual DbSet<Visit> Visit { get; set; }
         public virtual DbSet<Department> Departments { get; set; }
         public virtual DbSet<User> Users{ get; set; }
         public virtual DbSet<Kind> Kinds{ get; set; }

    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}