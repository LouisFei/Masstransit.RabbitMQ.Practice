using System;

namespace UserManagement.Service
{
    public class GreetingWriter
    {
         public void SayHello()
         {
            Console.WriteLine($"Hello world from {this.GetType().ToString()}");
         }
    }
}