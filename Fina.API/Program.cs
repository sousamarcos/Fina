using Fina.API;
using Fina.API.Common.API;
using Fina.API.EndPoints;


var builder = WebApplication.CreateBuilder(args);
builder.AddConfiguration();
//builder.AddSecurity();
builder.AddDataContexts();
builder.AddCrossOrigin();
builder.AddDocumentation();
builder.AddServices();

var app = builder.Build();
if (app.Environment.IsDevelopment())
    app.ConfigureAmbienteDesenvolvimento();

app.UseCors(APIConfiguration.CorsPolicyName);
//app.UseSEcurity();
app.MapEndPoints();

app.Run();


