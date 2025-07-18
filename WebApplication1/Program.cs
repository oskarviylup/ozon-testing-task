using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Presentation.Kafka;
using Presentation.Kafka.Consumer;
using project.Application;
using project.Application.Contracts;
using project.Application.Services;
using project.Endpoints;
using project.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddRepositories();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddKafkaProducer();
builder.Services.AddKafkaConsumer();
builder.Services.AddKafkaOptions();
builder.Services.AddScoped<ReportsController>();
builder.Services.AddHostedService<ReportRequestKafkaConsumer>();
builder.Services.AddScoped<ReportRequestHandler>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(op =>
{
    string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    op.IncludeXmlComments(xmlPath);
});

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers(); 

app.Run();
