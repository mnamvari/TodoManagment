using Microsoft.EntityFrameworkCore;
using TodoManagement.Appliaction.Contracts.Persistence;
using TodoManagement.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddDbContext<TodoManagementDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("TodoManagementConnectionString"),
                    //@"Server=sqlserver,1433;Database=CourseLibraryDB;User=sa;Password=RaymonTest@3354002"
                    b => b.MigrationsAssembly(typeof(TodoManagementDbContext).Assembly.FullName))
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                );

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<TodoManagementDbContext>();
    dbContext.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }