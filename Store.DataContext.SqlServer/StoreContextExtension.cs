using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Store.DataContext.SqlServer;

public static class StoreContextExtension
{
    public static IServiceCollection AddStoreContext(this IServiceCollection services)
    {
        string ConnectionString = "Data Source=.;Initial Catalog=StoreDb;Integrated Security=true;MultipleActiveResultSets=true;TrustServerCertificate=True;Encrypt=True;";
        services.AddDbContext<StoreContext>(options => options.UseSqlServer(ConnectionString,b=>b.MigrationsAssembly("Ordering.Server")));
        return services;

    }
}
