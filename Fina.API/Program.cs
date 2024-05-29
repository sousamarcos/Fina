using Fina.API.Data;
using Fina.API.Services;
using Fina.Core.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

const string conectionstring = "Server=NTB-ASUS\\SQLEXPRESS;Database=Fina;Password=qazwsx3s;User ID=sa;Trusted_Connection=False;TrustServerCertificate=True;";

builder.Services.AddDbContext<AppDbContext>(
    x=> x.UseSqlServer(conectionstring));

builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<ITransactionService, TransactionService>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
