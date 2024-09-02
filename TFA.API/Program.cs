using Microsoft.EntityFrameworkCore;
using TFA.Domain;
using TFA.Domain.UseCases;
using TFA.Domain.UseCases.CreateTopic;
using TFA.Domain.UseCases.GetForums;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Postgres");

builder.Services.AddScoped<IGetForumsUseCase, GetForumsUseCase>();
builder.Services.AddScoped<ICreateTopicUseCase, CreateTopicUseCase>();
builder.Services.AddTransient<IGuidFactory, GuidFactory>();
builder.Services.AddTransient<IMomentProvider, MomentProvider>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContextPool<ForumDbContext>(options => options
    .UseNpgsql(connectionString));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();