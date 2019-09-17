using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Newtonsoft.Json;
using RestSharp;
using WeekendTask.Models;

namespace WeekendTask.DAL
{
    class CRUDContext : DbContext
    {
        public CRUDContext() : base("RestSharpConnection")
        {
              
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
