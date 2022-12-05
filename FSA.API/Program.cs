using FSA.API.Business;
using FSA.API.Business.Services;
using FSA.Data.DBContext;
using FSA.Data.Repository;
using FSA.Data.Repository.FSAClaimRepository;
using FSA.Data.Repository.FSARuleRepository;
using FSA.Data.Repository.GenericRepository;
using FSA.Data.Repository.LoginRepository;
using FSA.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(
    options =>
    options.AddPolicy("ClientApp",
    builder => builder.WithOrigins("https://localhost:44422")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
    )
    );

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle



// Add services to the container.
//SERVICES/ BUSINESS LOGIC
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IFSAClaimBusinessService, ClaimsBusinessLogic>();
builder.Services.AddScoped<IClaimsApprovalService, ClaimsApprovalLogic>();
builder.Services.AddScoped<IFSARuleService, FSARuleLogic>();
builder.Services.AddScoped<ILoginService, LoginLogic>();

//REPOSITORIES
builder.Services.AddScoped<IJoinRepository<Employee, EmployeeFSA, FSARule>, EmployeeFSARepository>();
builder.Services.AddScoped<IRepository<FSAClaim>, FSAClaimRepository>();
builder.Services.AddScoped<IRepository<Employee>, TRepository<Employee>>();
builder.Services.AddScoped<IRepository<FSARule>, TRepository<FSARule>>();
builder.Services.AddScoped<IViewRepository<Login>, LoginRepository>();
builder.Services.AddScoped<ITransactAssociateEntityRepository<Employee, EmployeeFSA, FSARule>, TransactAssociateEntityRepository>();

//DbContext
builder.Services.AddDbContext<FSAClaimContext>(
    optionsBuilder => optionsBuilder.UseSqlServer("Server=PCM-6H43TL3\\SQLEXPRESS; Initial Catalog=FSAClaims; Integrated Security=true; Encrypt=false")
    , ServiceLifetime.Scoped
    );


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
app.UseCors("ClientApp");
app.UseHttpsRedirection();

//app.UseAuthentication();
//app.UseAuthorization();

app.MapControllers();

app.Run();