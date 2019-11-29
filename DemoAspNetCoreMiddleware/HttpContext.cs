using System;
using System.Collections.Generic;
using System.Text;

namespace DemoAspNetCoreMiddleware
{
    /// <summary>
    /// 模拟的Http上下文
    /// </summary>
    public class HttpContext
    {
        public string Request { get; set; }

        public string Response { get; set; }

        public void Show()
        {
            Console.WriteLine($"Current Context  Response : \n{Response}");
        }

    }
}
