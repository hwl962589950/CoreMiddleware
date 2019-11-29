using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DemoAspNetCoreMiddleware
{
    public static class UseExtensions
    {
        /// <summary>
        /// Use 方法扩展
        /// </summary>
        /// <param name="app"></param>
        /// <param name="middleware"></param>
        /// <returns></returns>
        public static ApplicationBuilder Use(this ApplicationBuilder app, Func<HttpContext, Func<Task>, Task> middleware)
        {
            return app.Use( (next) =>
                delegate (HttpContext context)
                {
                    Func<Task> arg = () => next(context);
                    return middleware(context, arg);
                }
            );

        }




    }
}
