
using Demo.MicroServer.Repository;
using Demo.MicroServer.UserService.Models;

namespace Demo.MicroServer.UserService.Repositories
{
    public class DemoMicroServerRepository<T> : Repository<T, DemoMicroServerContext>, IDemoMicroServerRepository<T>
        where T : class
    {

        public DemoMicroServerRepository(DemoMicroServerContext dbContext) : base(dbContext)
        {
        }
    }
}
