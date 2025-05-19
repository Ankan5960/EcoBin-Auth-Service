using EcoBin_Auth_Service.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureCors();
builder.Services.ConfigureDapperMapping();
builder.Services.ConfigureDBConnection(builder.Configuration);
builder.Services.AddControllers();
builder.Services.InjectRepository();
builder.Services.InjectService();
builder.Services.InjectJwtHelper();
builder.Services.ConfigureAuthentication(builder.Configuration);
builder.Services.ConfigureAuthorization();
builder.Services.ConfigureSwaggerGen();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseCors("CorsPolicy");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();