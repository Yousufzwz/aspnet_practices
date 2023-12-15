using Autofac;
using PracticeApplication1.Application.Features.Training;
using PracticeApplication1.Domain.Features.Training;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeApplication1.Application;

public class ApplicationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<CourseManagementService>().As<ICourseManagementService>()
            .InstancePerLifetimeScope();

    }
}
