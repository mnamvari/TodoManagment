using System.Reflection;
using TodoManagment.UI.Services;
using TodoManagment.UI.Services.Base;

namespace TodoManagment.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddScoped<IClient, Client>();
            services.AddHttpClient<IClient, Client>(client =>
                client.BaseAddress=new Uri("http://localhost:5066"));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<ITodoService, TodoService>();
            services.AddControllersWithViews();
        }
    }
}
