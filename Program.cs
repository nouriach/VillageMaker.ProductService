using Microsoft.EntityFrameworkCore;
using VillageMaker.ProductService.Applications.AsyncDataServices;
using VillageMaker.ProductService.Data;
using VillageMaker.ProductService.Data.Interfaces;
using VillageMaker.ProductService.Data.Repositories;
using VillageMaker.ProductService.EventProcessing;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("InMemory"));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IProductRepo, ProductRepo>();
builder.Services.AddSingleton<IEventProcessor, EventProcessor>();
builder.Services.AddHostedService<MessageBusSubscriber>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
