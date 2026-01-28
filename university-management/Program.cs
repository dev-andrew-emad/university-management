using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using universitydatalayer.dbcontext;
using universitydatalayer.data;
using universitybusinesslayer.business;
using universitybusinesslayer.Dtos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
string constr = builder.Configuration.GetConnectionString("connstr");
builder.Services.AddDbContext<appdbcontext>(options=>
options.UseSqlServer(constr));
builder.Services.AddScoped<studentdata>();
builder.Services.AddScoped<studentbusiness>();
builder.Services.AddScoped<coursedata>();
builder.Services.AddScoped<coursebusiness>();
builder.Services.AddScoped<instructordata>();
builder.Services.AddScoped<instructorbusiness>();
builder.Services.AddScoped<studentcoursesdata>();
builder.Services.AddScoped<studentcoursebusiness>();
builder.Services.AddScoped<userdata>();
builder.Services.AddScoped<userbusiness>();

builder.Services.Configure<jwtsettings>(builder.Configuration.GetSection("jwt"));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(option =>
    {
        var jwtsettings = builder.Configuration.GetSection("jwt");
        option.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtsettings["Issuer"],
            ValidAudience = jwtsettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtsettings["Key"]))
        };
    });
builder.Services.AddAuthorization(option=>
{
    option.AddPolicy("addnewuserpermission", policy =>
    policy.RequireClaim("permission", "adduser"));

    option.AddPolicy("addstudentpermission", policy =>
    policy.RequireClaim("permission", "addstudent"));

    option.AddPolicy("deletestudentpermission", policy =>
    policy.RequireClaim("permission", "deletestudent"));

    option.AddPolicy("deleteuserpermission", policy =>
    policy.RequireClaim("permission", "deleteuser"));
});



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c=>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Bearer {your token}"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference=new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,Id="Bearer"
                }
            },
            new string[]{}
        }
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

app.UseAuthorization();
app.UseAuthorization();

app.MapControllers();

app.Run();
