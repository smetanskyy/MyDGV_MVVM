using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDGV_MVVM.Entities
{
    public class EFContext : DbContext
    {
        public EFContext() : base("") { }
        public DbSet<Person> People { get; set; }
    }
}
