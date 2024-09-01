using Microsoft.EntityFrameworkCore;
using TFA.Storage;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContextPool<ForumDbContext>(options => options
    .UseNpgsql("User ID=gorbunovDA;Password=w2mir14;Host=localhost;Port=15432;Database=tfaDb;Pooling=true;MinPoolSize=0;MaxPoolSize=100;Connection Idle Lifetime=60;"));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();