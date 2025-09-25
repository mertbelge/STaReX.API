using STaReX.BUSINESS.Abstract.IConnectionTestingService;
using STaReX.BUSINESS.Abstract.IHolidayService;
using STaReX.BUSINESS.Abstract.IQCDELogReaderService;
using STaReX.BUSINESS.Abstract.IWeatherService;
using STaReX.BUSINESS.Concrete.ConnectionTestingService;
using STaReX.BUSINESS.Concrete.HolidayService;
using STaReX.BUSINESS.Concrete.QCDELogReaderService;
using STaReX.BUSINESS.Concrete.WeatherService;
using STaReX.DB;
using STaReX.DB.Abstract;
using STaReX.DB.Concrete;
using STaReX.DB.Dtos;
using STaReX.HELPERS.Abstract;
using STaReX.HELPERS.Concrete;
using STaReX.HELPERS.Dto;
using STaReX.HELPERS.Dtos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<ProcedureOptions>(builder.Configuration.GetSection("ProcedureOptions"));
builder.Services.Configure<HelperOptions>(builder.Configuration.GetSection("HelperOptions"));

builder.Services.AddSingleton<DBContext>();
builder.Services.AddTransient(typeof(IDatabaseRepository<>), typeof(DatabaseRepository<>));
builder.Services.AddTransient(typeof(IHelperRepository<>), typeof(HelperRepository<>));

builder.Services.AddTransient<IConnectionTestingService, ConnectionTestingService>();
builder.Services.AddTransient<IHolidayService, HolidayService>();
builder.Services.AddTransient<IWeatherService, WeatherService>();
builder.Services.AddTransient<IQCDELogReaderService, QCDELogReaderService>();

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
