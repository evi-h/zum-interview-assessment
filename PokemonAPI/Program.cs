var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
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

// Use the CORS policy
app.UseCors(  options => options.WithOrigins("http://localhost:4200") // Allow Angular app's origin
              .AllowAnyMethod()                    // Allow all HTTP methods (GET, POST, etc.)
              .AllowAnyHeader()                 // Allow all headers);
);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
