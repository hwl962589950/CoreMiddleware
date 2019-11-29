using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DemoAspNetCoreMiddleware
{
    /// <summary>
    /// 处理请求的委托
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public delegate Task RequestDelegate(HttpContext context);
    
}
