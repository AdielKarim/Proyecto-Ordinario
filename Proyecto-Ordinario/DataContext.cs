using Proyecto_Ordinario.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Ordinario
{
    public class DataContext:DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<Adviser> Advisers { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Major> Majors { get; set; }
        public DbSet<Coordinator> Coordinators { get; set; }
        public DataContext() : base("name=myConexions")
        {

        }
    }
}
