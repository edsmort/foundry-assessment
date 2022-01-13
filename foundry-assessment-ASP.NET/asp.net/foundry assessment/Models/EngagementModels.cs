using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace foundry_assessment.Models
{
    public class EngagementContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Engagements> Engagements { get; set; }
    }
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class Engagement
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual Client Client { get; set; }
        public virtual Employee Employee { get; set; }
        public string Description { get; set; }
        public DateTime started { get; set; }
        public DateTime ended { get; set; }
    }
    public class ClientList
    {
        public List<Client> Clients { get; set; }
    }

    public class EmployeeList
    {
        public List<Employee> Employees { get; set; }
    }

    public class EngagementList
    {
        public List<Engagements> Engagements { get; set; }
    }

}