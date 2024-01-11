using Autofac;
using Exam1.Application.Features.AccessStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam1.Application;

public class ApplicationModule : Module
{
	protected override void Load(ContainerBuilder builder)
	{
		builder.RegisterType<LibraryManagementService>().As<ILibraryManagementService>()
			.InstancePerLifetimeScope();
	}
}