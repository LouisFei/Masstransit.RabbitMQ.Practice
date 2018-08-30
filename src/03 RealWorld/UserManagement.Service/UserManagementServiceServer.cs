using Castle.MicroKernel.Registration;
using MassTransit;

namespace UserManagement.Service
{
    /// <summary>
    /// 用户管理服务
    /// </summary>
    public class UserManagementServiceServer
    {
        //总线
        private readonly IBusControl _bus;

        public UserManagementServiceServer()
        {
            //创建IoC容器
            var container = ApplicationBootstrapper.RegisterContainer();

            //向容器注册总线
            RegisterBus();

            //从容器中获得总线实例
            _bus = container.Resolve<IBusControl>();
        }

        /// <summary>
        /// 向容器注册总线组件
        /// </summary>
        private void RegisterBus()
        {
            ApplicationBootstrapper.Container.Register(
                Component.For<IBus, IBusControl>()
                    .Instance(UserManagementServiceBusConfiguration.BusInstance)
                    .LifestyleSingleton());
        }

        public void Start()
        {
            _bus.Start();
        }

        public void Stop()
        {
            _bus.Stop();
        }
    }
}