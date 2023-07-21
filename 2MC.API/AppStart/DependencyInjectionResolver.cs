using _2MC.BusinessTier.Services;
using _2MC.DataTier.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace _2MC.API.AppStart
{
    public static class DependencyInjectionResolver
    {
        public static IServiceCollection ConfigureDependencyInjection(this IServiceCollection services)
        {
            //FireBase
            services.AddScoped<IFireBaseService, FireBaseService>();
            
            //Subject
            services.AddScoped<ISubjectRepository, SubjectRepository>();
            services.AddScoped<ISubjectService, SubjectService>();
            //Course
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<ICourseService, CourseService>();
           
            //Major
            services.AddScoped<IMajorRepository, MajorRepository>();
            services.AddScoped<IMajorService, MajorService>();
            

            //Session
            services.AddScoped<ISessionRepository, SessionRepository>();
            services.AddScoped<ISessionService, SessionService>();

            //Report
            services.AddScoped<IReportRepository, ReportRepository>();
            services.AddScoped<IReportService, ReportService>();

            //Certificate
            services.AddScoped<ICertificateRepository, CertificateRepository>();
            services.AddScoped<ICertificateService, CertificateService>();


            //User
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            //Role
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IRoleService, RoleService>();

            //MenteeSession
            services.AddScoped<IMenteeSessionRepository, MenteeSessionRepository>();
            services.AddScoped<IMenteeSessionService, MenteeSessionService>();


            //Order
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderService, OrderService>();

            return services;
        }
    }
}