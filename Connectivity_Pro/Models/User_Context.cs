using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
namespace Connectivity_Pro.Models
{
    public class User_Context : DbContext
    {
        public User_Context(DbContextOptions<User_Context> options) : base(options)
        {

        }
        public DbSet<Users> users { get; set; }

    }
}
