using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTask_ManagerNotes.Models
{
    public class ManagerContext : DbContext
    {
        public ManagerContext(DbContextOptions<ManagerContext> options):base(options)
        {

        }

        public DbSet<ManagerNote> managerNotes { get; set; }
    }
}
