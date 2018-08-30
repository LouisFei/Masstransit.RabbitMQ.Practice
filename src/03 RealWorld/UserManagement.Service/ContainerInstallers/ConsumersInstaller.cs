using MassTransit;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace UserManagement.Service.ContainerInstallers
{
    /*
     　当使用依赖注入容器时，你首先要向容器中注册你的组件，
      Windsor使用installser（该类型实现了IWindsorInstaller接口）来封装和隔离注册的逻辑，
      可以使用Configuration和FromAssembly来完成工作。
      Installers是实现了IWindsorInstaller接口的简单类型，只有一个Install方法，该方法接收container参数，该参数使用fluent registration API方式来注册组件。


      使用单独的installer来注册一些相关的服务信息（如repositories, controllers）
      installer必须是公共的而且包含一个公共的默认构造函数，Windsor使用InstallerFactory扫描公开的installer。

        installer的注册没有特定的顺序，所以你不知道哪个installer先被注册，如果要进行特殊的排序，使用InstallerFactory来实现。
    */


    /// <summary>
    /// 向容器中注册组件（消费者组件）
    /// </summary>
    public class ConsumersInstaller : IWindsorInstaller
    {
        /// <summary>
        /// 向容器注册注册
        /// </summary>
        /// <param name="container">容器</param>
        /// <param name="store">配置</param>
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            //向容器注册组件
            container.Register(
                Classes.FromThisAssembly() //当前程序集
                .BasedOn(typeof(IConsumer))
                .WithServiceBase()
                .WithServiceSelf()
                .LifestyleTransient());
        }
    }
}