using GunShopBackPart.Data;
using GunShopBackPart.Interfaces;
using GunShopBackPart.Mappers;
using GunShopBackPart.Models;
using GunShopBackPart.Repository;
using GunShopBackPart.Tool;
using GunShopBackPart.Tool.CreateProduct;
using GunShopBackPart.Tool.Crypto;
using GunShopBackPart.Tool.JVT;
using GunShopBackPart.Tool.Update;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped(typeof(IRepo<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IProductServices), typeof(ProductServices));
builder.Services.AddScoped(typeof(ICustomerServices), typeof(CustomerServices));
builder.Services.AddScoped(typeof(IRequestHelper), typeof(ProductRequestHelper));
builder.Services.AddScoped(typeof(IProductFactory), typeof(ProductFactory));
builder.Services.AddScoped(typeof(IImgageHelper), typeof(PicHelper));
builder.Services.AddScoped(typeof(IHandleProductUpdate), typeof(HandleProductUpdate));
builder.Services.AddScoped(typeof(IUpdateProductHelper), typeof(UpdateProductHelper));
builder.Services.AddScoped(typeof(ICrypto), typeof(Crypto));
builder.Services.AddScoped(typeof(IProductPurchaseRepo), typeof(ProductPurchaseRepo));
builder.Services.AddScoped(typeof(IJVTProvider), typeof(JVTProvider));
builder.Services.AddScoped(typeof(IProductPurchaseServices), typeof(ProductPurchaseServices));
builder.Services.AddScoped(typeof(ILogin), typeof(LoginHelper));
builder.Services.AddAuthenticationHeader(builder.Configuration);
builder.Services.Configure<JvtOptions>(
    builder.Configuration.GetSection("JwtOptions"));


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();

    if (db.Database.CanConnect())
        Console.WriteLine("DB connected");
    else
        Console.WriteLine("DB failed");
 
}


    Console.WriteLine(app.Environment.EnvironmentName);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "img")),
    RequestPath = "/img"
});
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
