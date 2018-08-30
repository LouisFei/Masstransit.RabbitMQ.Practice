using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace UserManagement.Service.ContainerInstallers
{
    /// <summary>
    /// 依赖注入
    /// </summary>
    public class GreetingWriterInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<GreetingWriter>()
                .LifestyleTransient());
        }
    }
}