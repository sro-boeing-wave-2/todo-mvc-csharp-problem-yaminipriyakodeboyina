using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Todolist.Models;

namespace finaltodo.Models
{
    public class finaltodoContext : DbContext
    {
        public finaltodoContext (DbContextOptions<finaltodoContext> options)
            : base(options)
        {
        }

        public DbSet<Todolist.Models.Todo> Todo { get; set; }
    }
}
