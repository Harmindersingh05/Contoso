
using ContosoUniversity.Data;
using ContosoUniversity.Middleware;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            
            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
                cfg.AddOpenBehavior(typeof(ValidationPipeline<,>));
            });
            
            builder.Services.AddValidatorsFromAssemblyContaining<Program>();
            
            builder.Services.AddDbContext<ContosoDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
                
            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseMiddleware<GlobalExceptionMiddleware>();
            app.MapControllers();

            app.Run();
        }
    }
}
