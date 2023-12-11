using Ordering.Server.Core;
using Ordering.Server.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.DataContext.SqlServer.Repositories;


namespace Store.DataContext.SqlServer;

public class UnitOFWork : IUnitOfWork
{
    private readonly StoreContext _context;
    public UnitOFWork(StoreContext context)
    {

        _context = context;
        Order = new OrderRepository(_context); 

    }
    public IOrderRepository Order { get; private set; }

    public int Complete()
    {
        return _context.SaveChanges();
    }

    public void Dispose()
    {
        _context.Dispose(); 
    }
}
