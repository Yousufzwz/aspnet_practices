using Autofac;
using PracticeApplication.Application;
using PracticeApplication.Domain.Entities.Repositories;
using PracticeApplication.Infrastructure;
using PracticeApplication.Infrastructure.Repositories;
using PracticeApplication1.Application.Features.Training;
using PracticeApplication1.Domain.Features.Training;
using PracticeApplication1.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeApplication1.Application;

public class InfrastructureModule : Module
{
    private readonly string _connectionString;
    private readonly string _migrationAssembly;
    public InfrastructureModule(string connectionString, string migrationAssembly)
    {
        _connectionString = connectionString;
        _migrationAssembly = migrationAssembly;
    }
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<ApplicationDbContext>().AsSelf()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssembly", _migrationAssembly)
                .InstancePerLifetimeScope();

        builder.RegisterType<ApplicationDbContext>().As<IApplicationDbContext>()
            .WithParameter("connectionString", _connectionString)
            .WithParameter("migrationAssembly", _migrationAssembly)
            .InstancePerLifetimeScope();


        builder.RegisterType<ApplicationUnitOfWork>().As<IApplicationUnitOfWork>()
            .InstancePerLifetimeScope();

        builder.RegisterType<CourseRepository>().As<ICourseRepository>()
            .InstancePerLifetimeScope();
    }
}
