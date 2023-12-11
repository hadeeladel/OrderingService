using Store.EntityModels.SqlServer;
using Ordering.Server.Core.IRepositories;

namespace Ordering.Server.Core
{
    public interface IUnitOfWork:IDisposable
    {
        IOrderRepository Order { get; }
        int Complete();

    }
}
