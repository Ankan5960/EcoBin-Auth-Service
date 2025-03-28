using Auth_Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureCors();
builder.Services.ConfigureDapperMapping();
builder.Services.ConfigureDBConnection(builder.Configuration);
builder.Services.AddControllers();
builder.Services.InjectRepository();
builder.Services.InjectService();
builder.Services.InjectJwtHelper();


var app = builder.Build();

app.MapControllers();
app.UseCors("CorsPolicy");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();