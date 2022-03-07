using CodeExercise_Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CodeExercise.DataAcccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            var types = Assembly.GetExecutingAssembly().GetTypes()
               .Where(x => x.GetInterfaces().Any(type =>
                   type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)))
               .ToList();

            //Get all the IEntityTypeConfiguration and execute the HasData()
            foreach (var type in types)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.ApplyConfiguration(configurationInstance);
                var entityType = type.GetGenericInterfaceParameter(typeof(IEntityTypeConfiguration<>));
                modelBuilder.Entity(entityType).HasData();
            }
            base.OnModelCreating(modelBuilder);
        }
  
    }

    public static class TypeExtensions
    {
        public static Type GetGenericInterfaceParameter(this Type concreteType, Type interfaceType)
        {
            var _interface = concreteType
                .GetInterfaces()
                .Single(y => y.IsGenericType && y.GetGenericTypeDefinition() == interfaceType);

            return _interface.GetGenericArguments().First();
        }
    }
}
