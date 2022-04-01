
using Play.Catalog.Service.Entities;
using Play.Common.MassTransit;
using Play.Common.MongoDB;
using Play.Common.Settings;
using MassTransit;
using MassTransit.Definition;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var serviceSettings = builder.Configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();

builder.Services
    .AddMongo()
    .AddMongoRepository<Item>("items")
    .AddMassTransitWithRabbitMq();


//var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
//{
//    cfg.Host((builder.Configuration.GetSection(nameof(RabbitMQSettings)).Get<RabbitMQSettings>()).Host);
//});
//builder.Services.AddSingleton<IBus>(bus);



//Aggiunto options.SuppressAsyncSuffixInActionNames = false per non togliere il suffisso ai metodi Async
builder.Services.AddControllers(options =>
{
    options.SuppressAsyncSuffixInActionNames = false;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
 