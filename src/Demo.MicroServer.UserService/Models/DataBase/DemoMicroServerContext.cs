using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Demo.MicroServer.UserService.Models
{
    public partial class DemoMicroServerContext : DbContext
    {
        private readonly IConfiguration Configuration;
        public DemoMicroServerContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public DemoMicroServerContext()
        {
        }

        public DemoMicroServerContext(DbContextOptions<DemoMicroServerContext> options)
            : base(options)
        {
        }


        public DbSet<users> users { get; set; }
        public DbSet<products> products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(Configuration["MySqlConnections"]);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //MappingEntityTypes(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        ////动态添加实体
        //private readonly string ModelAssemblyName = "Rpa.MicroServer.Model";
        //private void MappingEntityTypes(ModelBuilder modelBuilder)
        //{
        //    if (string.IsNullOrEmpty(ModelAssemblyName))
        //        return;
        //    var assembly = System.Reflection.Assembly.Load(ModelAssemblyName);
        //    var types = assembly?.GetTypes();
        //    var list = types?.Where(t =>
        //        t.IsClass && !t.IsGenericType && !t.IsAbstract).ToList();
        //    //&& t.GetInterfaces().Any(m => m.IsAssignableFrom(typeof(BaseModel<>)))).ToList();
        //    if (list != null && list.Any())
        //    {
        //        list.ForEach(t =>
        //        {
        //            if (modelBuilder.Model.FindEntityType(t) == null)
        //                modelBuilder.Model.AddEntityType(t);
        //        });
        //    }
        //}

    }
}
