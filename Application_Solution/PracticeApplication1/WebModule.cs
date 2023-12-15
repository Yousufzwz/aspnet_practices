using Autofac;
using PracticeApplication1.Areas.Admin.Models;
using PracticeApplication1.Models;

namespace PracticeApplication1;

public class WebModule:Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<UnicodeSmsSender>().As<ISMsSender>();
        builder.RegisterType<CourseCreateModel>().AsSelf();  
    }
}
