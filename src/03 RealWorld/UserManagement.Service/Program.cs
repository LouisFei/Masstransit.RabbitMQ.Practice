using System;

namespace UserManagement.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start UserManagement service...");

            //创建用户管理服务服务器实例
            var server = new UserManagementServiceServer();
            //启动用户管理服务
            server.Start();

            Console.ReadLine();
        }
    }
}
