using Demo.MicroServer.Repository;
using Demo.MicroServer.UserService.Models;

namespace Demo.MicroServer.UserService.Repositories
{
    public interface IDemoMicroServerRepository<T>: IRepository<T, DemoMicroServerContext>
        where T:class
    {
    }
}
