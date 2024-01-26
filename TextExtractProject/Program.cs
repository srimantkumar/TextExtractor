using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Register all DbContexts
builder.Services.AddDbContext<UserContext>(opt => {
    opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnectionString"));
});
builder.Services.AddDbContext<UserAdharInformationContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnectionString"));
});
builder.Services.AddDbContext<UserLoginContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnectionString"));
});
// ------------------------ //

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 500000; // Set the limit to the maximum possible value
});


// Add CORS configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("CORSPolicy", builder =>
    { 
        builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); 
}

app.UseHttpsRedirection();

// Allow CORS before authorization
app.UseCors("CORSPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
