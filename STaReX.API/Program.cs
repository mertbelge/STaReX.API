using STaReX.BUSINESS.Abstract.IConnectionTestingService;
using STaReX.BUSINESS.Concrete.ConnectionTestingService;
using STaReX.DB;
using STaReX.DB.Abstract;
using STaReX.DB.Concrete;
using STaReX.DB.Dtos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<ProcedureOptions>(builder.Configuration.GetSection("ProcedureOptions"));

builder.Services.AddSingleton<DBContext>();
builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddTransient<IConnectionTestingService, ConnectionTestingService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
