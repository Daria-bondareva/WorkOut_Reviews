using Microsoft.EntityFrameworkCore;
using ReviewsSystem.BLL.Interfaces;
using ReviewsSystem.BLL.Services;
using ReviewsSystem.DAL;
using ReviewsSystem.DAL.Data;
using ReviewsSystem.DAL.Data.Repositories;
using ReviewsSystem.DAL.Interfaces;
using ReviewsSystem.DAL.Interfaces.Repositories;
using AutoMapper;
using FluentValidation;
using ReviewsSystem.BLL.Configurations;
using ReviewsSystem.BLL.Validation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<ReviewsSystemContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IWorkOutRepository, WorkOutRepository>();

builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IWorkOutService, WorkOutService>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddValidatorsFromAssemblyContaining<ReviewValidator>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
