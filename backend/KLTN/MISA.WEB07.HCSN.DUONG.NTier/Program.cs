using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MISA.WEB07.HCSN2.DUONG.BL;
using MISA.WEB07.HCSN2.DUONG.BL.PropertyBL;
using MISA.WEB07.HCSN2.DUONG.Common.Entity;
using MISA.WEB07.HCSN2.DUONG.DL;
using MISA.WEB07.HCSN2.DUONG.DL.PropertyDL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped(typeof(IBaseDL<>), typeof(BaseDL<>));
builder.Services.AddScoped(typeof(IBaseBL<>), typeof(BaseBL<>));
builder.Services.AddScoped<IPropertyBL, PropertyBL>();
builder.Services.AddScoped<IPropertyDL, PropertyDL>();
builder.Services.AddScoped<IDepartmentDL, DepartmentDL>();
builder.Services.AddScoped<IDepartmentBL, DepartmentBL>();

builder.Services.AddScoped<IPropertyTypeDL, PropertyTypeDL>();
builder.Services.AddScoped<IPropertyTypeBL, PropertyTypeBL>();

builder.Services.AddScoped<IUserDL, UserDL>();
builder.Services.AddScoped<IUserBL, UserBL>();

builder.Services.AddScoped<IBudgetDL, BudgetDL>();
builder.Services.AddScoped<IBudgetBL, BudgetBL>();


builder.Services.AddScoped<IVoucherDL, VoucherDL>();
builder.Services.AddScoped<IVoucherBL, VoucherBL>();

builder.Services.AddScoped<IVoucherDetailDL, VoucherDetailDL>();
builder.Services.AddScoped<IVoucherDetailBL, VoucherDetailBL>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Sửa lỗi cor không cho phép truy cập
builder.Services.AddCors(p => p.AddPolicy("corspolicy", build =>
{
    build.WithOrigins("http://localhost:8080").AllowAnyMethod().AllowAnyHeader();
}));
// Xác thực bằng jwt 
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseCors("corspolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
