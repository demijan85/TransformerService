using Transformer.Api.Services.Implementations;
using Transformer.Api.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

/*builder.Services.Scan(scan => scan
    .FromAssemblyOf<ITransformer>()
    .AddClasses(classes => classes.AssignableTo<ITransformer>())
    .AsImplementedInterfaces()
    .WithScopedLifetime());*/
var assembly = typeof(ITransformer).Assembly;
var transformerTypes = assembly.GetTypes()
    .Where(type => typeof(ITransformer).IsAssignableFrom(type) && type.IsClass && !type.IsAbstract);

foreach (var type in transformerTypes)
{
    builder.Services.AddScoped(type);
}

builder.Services.AddScoped<ITransformerFactory, TransformerFactory>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//app.UseAuthorization();
app.MapControllers();

app.Run();

public partial class Program { }