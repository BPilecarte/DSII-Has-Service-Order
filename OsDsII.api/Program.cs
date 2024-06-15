using Microsoft.EntityFrameworkCore;
using OsDsII.Api.Data;
using OsDsII.Api.Repository.CommentsRepository;
using OsDsII.Api.Repository.CustomersRepository;
using OsDsII.Api.Repository.ServiceOrderRepository;
using OsDsII.Api.Services.Customers;
using OsDsII.Api.Services.ServiceOrders;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection"));
});
// Add services to the container.

builder.Services.AddCors();


builder.Services.AddScoped<IServiceOrderRepository, ServiceOrderRepository>();
builder.Services.AddScoped<ICustomersRepository, CustomersRepository>();
builder.Services.AddScoped<ICommentsRepository, CommentsRepository>();

builder.Services.AddScoped<ICustomersService, CustomersService>();
builder.Services.AddScoped<IServiceOrderService, ServiceOrderService>();

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenApi at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();
