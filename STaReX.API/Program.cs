using STaReX.API.Filters;
using STaReX.API.Middleware;
using STaReX.BUSINESS.Abstract.IConnectionTestingService;
using STaReX.BUSINESS.Abstract.IHolidayService;
using STaReX.BUSINESS.Abstract.IQCDELogReaderService;
using STaReX.BUSINESS.Concrete.ConnectionTestingService;
using STaReX.BUSINESS.Concrete.HolidayService;
using STaReX.BUSINESS.Concrete.QCDELogReaderService;
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

builder.Services.AddTransient<IConnectionService, ConnectionService>();
builder.Services.AddTransient<IInformationService, InformationService>();
builder.Services.AddTransient<IQCDELogReaderService, QCDELogReaderService>();

builder.Services.AddScoped<IConnectionService, ConnectionService>();
builder.Services.AddScoped<AuthFilter>();
builder.Services.AddScoped<DBFilter>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<FileMiddleware>();
app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();
app.UseAuthorization();

app.UseStaticFiles();
app.MapControllers();

app.Run();
