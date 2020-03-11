using NewsFeed.Data.Migrations;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Reflection;

namespace NewsFeed.Data
{
    public class NewsFeedContext : DbContext
    {
        public NewsFeedContext() : base("localConnection")
        {
            Database.SetInitializer(new NewsFeedInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
        .Where(type => !String.IsNullOrEmpty(type.Namespace))
        .Where(type => type.BaseType != null && type.BaseType.IsGenericType
             && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));

            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
