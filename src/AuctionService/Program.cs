using Microsoft.EntityFrameworkCore;
using AuctionService.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<AuctionDbContext>(
    opts => opts.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


var app = builder.Build();

app.UseAuthorization();

app.MapControllers();

try {
    DbInitializer.InitDb(app);
} catch (Exception ex) {
    Console.WriteLine(ex);
}

app.Run();

