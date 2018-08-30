using Castle.Windsor;
using Castle.Windsor.Installer;

namespace UserManagement
{
    /// <summary>
    /// 程序启动（创建IoC容器）类
    /// </summary>
    public class ApplicationBootstrapper
    {
        public static IWindsorContainer Container;

        static ApplicationBootstrapper()
        {
            Container = new WindsorContainer();

            Container.Install(FromAssembly.InThisApplication());
        }
    }
}