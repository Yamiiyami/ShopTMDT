using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ShopTMDT.Data;
using ShopTMDT.services;
using SpQuanAo.services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<StmdtContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("ShopConnection"));
});

builder.Services.AddScoped<IAccountRepository,AccountRepository>();
builder.Services.AddScoped<IRoleRepository,RoleRepository>();
builder.Services.AddScoped<ILoaiHangHoaRepository,LoaiHangHoaRepository>();
builder.Services.AddScoped<IHangHoaRepository, HangHoaRepository>();
builder.Services.AddScoped<ITrangThaiVanchuyenRepository,TrangThaiVanchuyenRepository>();
builder.Services.AddScoped<ITrangThaiThanhToanRepository,TrangThaiThanhToanRepository>();
builder.Services.AddScoped<IThongTinXuatRepository,ThongTinXuatRepository>();
builder.Services.AddScoped<IXuatHangHoaRepository, XuatHangHoaRepository>();
builder.Services.AddScoped<IThongTinDonNhapRepository,ThongTinDonNhapRepository>();
builder.Services.AddScoped<INhapHangHoaRepository,NhapHangHoaRepository>();
builder.Services.AddScoped<IHinhAnhRepository,HinhAnhRepository>();
builder.Services.AddScoped<INhaCungCapReepository,NhaCungCapReepository>();
builder.Services.AddScoped<IKhuyenMaiRepository,KhuyenMaiRepository>();
builder.Services.AddScoped<ISendEmailService, SenEmailService>();
builder.Services.AddScoped<IWriteFileRepository, WriteFileRepository>();
builder.Services.AddScoped<IRatingRepository, RatingRepository>();
builder.Services.AddScoped<ISlideRepository,SlideRepository>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(option =>
{
    option.SaveToken = true;
    option.RequireHttpsMetadata = false;
    option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Access_Secret"]!))
    };
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization Example : 'Bearer asdkljfasudbsdj'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
             new OpenApiSecurityScheme{
                    Reference = new OpenApiReference
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                    },
                    Scheme = "OAuth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header,
                    },
             new List<string>()
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
app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Upload")),
    RequestPath = new PathString("/Upload")
});
app.UseHttpsRedirection();
app.UseCors(options =>
{
    options.AllowAnyHeader();
    options.AllowAnyMethod();
    options.AllowAnyOrigin();
});


app.UseAuthorization();

app.MapControllers();

app.Run();
