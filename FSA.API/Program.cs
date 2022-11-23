using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


// Add services to the container.

builder.Services.AddControllers();
//builder.Services.AddAntiforgery();
builder.Services.AddAuthentication("Bearer").AddJwtBearer(
    options =>
    options.TokenValidationParameters = new()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration.GetValue<string>("Authentication:Issuer"),
        ValidAudience = builder.Configuration.GetValue<string>("Authentication:Audience"),
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("Authentication:SecretForKey")))
    }
    );
builder.Services.AddAuthorization(
    options => options.AddPolicy("AdminRole", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("Role", "Admin");
    }
    ));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();