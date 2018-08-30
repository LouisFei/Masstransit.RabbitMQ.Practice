using Castle.Windsor;
using Castle.Windsor.Installer;

namespace UserManagement.Service
{
    /// <summary>
    /// 启用IoC容器
    /// </summary>
    public class ApplicationBootstrapper
    {
        /// <summary>
        /// Windsor IoC容器
        /// </summary>
        public static IWindsorContainer Container;

        public static IWindsorContainer RegisterContainer()
        {
            Container = new WindsorContainer();
            Container.Install(FromAssembly.InThisApplication());
            return Container;
        }

    }
}
