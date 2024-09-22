using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using Es.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var settings = new ElasticsearchClientSettings(new Uri("http://localhost:9200")).Authentication(new BasicAuthentication("elastic","DkIedPPSCb"));
var client = new ElasticsearchClient(settings);
builder.Services.AddSingleton(client);

builder.Services.AddScoped<TTrComRepository>();

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