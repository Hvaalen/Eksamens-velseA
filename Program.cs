using PlayGroundLib;

var builder = WebApplication.CreateBuilder(args);

// Tilføj services
builder.Services.AddSingleton<PlayGroundRepository>();
builder.Services.AddControllers();

// Swagger setup - fjern AddOpenApi()
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});
var app = builder.Build();

//if (app.Environment.IsDevelopment()) //skal indeholde useSwagger hvis man vil benytte swagger inden publish.
//{

//}
app.UseSwagger();
app.UseSwaggerUI(); // Det er denne der laver den grafiske side
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors("AllowAll");
app.MapControllers();

app.Run();
