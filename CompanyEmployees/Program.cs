using CompanyEmployees.Extensions;
using CompanyEmployees.Presentation.ActionFilters;
using Contracts;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Options;
using NLog;

//By using AddNewtonsoftJson, we are replacing the System.Text.Json
//formatters for all JSON content. We don’t want to do that so, we are 
//going ton add a simple workaround in the Program class:

NewtonsoftJsonPatchInputFormatter GetJsonPatchInputFormatter() =>
new ServiceCollection().AddLogging().AddMvc().AddNewtonsoftJson()
.Services.BuildServiceProvider()
.GetRequiredService<IOptions<MvcOptions>>().Value.InputFormatters
.OfType<NewtonsoftJsonPatchInputFormatter>().First();


var builder = WebApplication.CreateBuilder(args);
LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
// the above one is depricated now we can use below method also
//LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

// Add services to the container.
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.ConfigureSqlContext(builder.Configuration);

//builder.Services.Configure<ApiBehaviorOptions>(options =>
//{
//    options.SuppressModelStateInvalidFilter = true;
//});
builder.Services.AddScoped<ValidationFilterAttribute>();
builder.Services.AddControllers(config =>
{
    config.InputFormatters.Insert(0, GetJsonPatchInputFormatter());
    config.RespectBrowserAcceptHeader = true;
    config.ReturnHttpNotAcceptable = true;  //in this line if the client tries to negotiate for the media type the 
                                            // server doesn’t support, it should return the 406 Not Acceptable status  code
}).AddXmlDataContractSerializerFormatters()
  .AddCustomCSVFormatter()  // this line will format the output to the csv format
  .AddApplicationPart(typeof(CompanyEmployees.Presentation.AssemblyReference).Assembly)
  .AddNewtonsoftJson();

builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();
builder.Services.ConfigureLoggerService();

var app = builder.Build();
var logger = app.Services.GetRequiredService<ILoggerManager>();
app.ConfigureExceptionHandler(logger);
if (app.Environment.IsDevelopment())
    app.UseHsts();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseForwardedHeaders(new ForwardedHeadersOptions { ForwardedHeaders = ForwardedHeaders.All });
app.UseCors("CorsPolicy");
app.UseAuthorization();

app.MapControllers();

app.Run();
