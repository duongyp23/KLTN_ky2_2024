using System.Text;
using KLTN.BussinesLayer;
using KLTN.DataLayer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped(typeof(IBaseDL<>), typeof(BaseDL<>));
builder.Services.AddScoped(typeof(IBaseBL<>), typeof(BaseBL<>));


builder.Services.AddScoped<IUserDL, UserDL>();
builder.Services.AddScoped<IUserBL, UserBL>();

builder.Services.AddScoped<ICategoryDL, CategoryDL>();
builder.Services.AddScoped<ICategoryBL, CategoryBL>();

builder.Services.AddScoped<IProductDL, ProductDL>();
builder.Services.AddScoped<IProductBL, ProductBL>();

builder.Services.AddScoped<IProductCategoryDL, ProductCategoryDL>();

builder.Services.AddScoped<IOrderBL, OrderBL>();
builder.Services.AddScoped<IOrderDL, OrderDL>();

builder.Services.AddScoped<IOrderDetailDL, OrderDetailDL>();
builder.Services.AddScoped<IOrderDetailBL, OrderDetailBL>();

builder.Services.AddScoped<IStockDL, StockDL>();
builder.Services.AddScoped<IStockBL, StockBL>();

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
