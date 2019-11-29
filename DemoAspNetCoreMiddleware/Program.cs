using System;
using System.Threading.Tasks;

namespace DemoAspNetCoreMiddleware
{
    class Program
    {
        /**
         * 1. 第一个 Use  把第一个中间件加入到 集合中 
         * 2. 第一个 Use  把第二个中间件加入到 集合中 
         * 3. Bulie 倒叙循环所有中间件 默认一个404 为最后一个中间件
         * 4. item(requestDelegate) 指定 item 的下一个中间件 因为Use 中的 Option 是他的下一个中间件 等到执行的时候 Option就执行自己的方法块 一直执行到 404的（或者不设置他的下一个中间件，404就是这个原理）
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         */
        static void Main(string[] args)
        {
            //模拟请求进入
            var context = new HttpContext();       
            var app = new ApplicationBuilder();
            {
                //这里来增加中间件  option 为 RequestDelegate 方法：Use 参数 RequestDelegate 返回结果为 RequestDelegate
                app.Use((option) =>
                {
                    //这里构建一个返回结果 RequestDelegate委托中需要参数 HttpContext * 注意现在是不会调用 middlewareDelegate(或者说自己不够理解掉坑了)
                    RequestDelegate middlewareDelegate = new RequestDelegate((context) =>
                    {
                        //这里开始处理请求
                        context.Response += "Create middleware One Start\n";
                        //注意这里的 option  这里可以选择是否执行下一个中间件
                        option(context);
                        context.Response += "Create middleware One End\n";
                        return Task.CompletedTask;
                    });

                    return middlewareDelegate;
                });

                app.Use((option) =>
                {
                    //这里构建一个返回结果 RequestDelegate委托中需要参数 HttpContext * 注意现在是不会调用 middlewareDelegate(或者说自己不够理解掉坑了)
                    RequestDelegate middlewareDelegate = new RequestDelegate((context) =>
                    {
                        //这里开始处理请求
                        context.Response += "Create middleware Two Start\n";
                        //注意这里的 option  这里可以选择是否执行下一个中间件
                        option(context);
                        context.Response += "Create middleware Two End\n";
                        return Task.CompletedTask;
                    });

                    return middlewareDelegate;
                });


                app.Use( (dcontext, option) =>
                {
                    dcontext.Response += "UseExtensions Start \n";
                    option();
                    dcontext.Response += "UseExtensions End \n";
                    return Task.CompletedTask;
                });



                app.Use((option) =>
                {
                    //这里构建一个返回结果 RequestDelegate委托中需要参数 HttpContext * 注意现在是不会调用 middlewareDelegate(或者说自己不够理解掉坑了)
                    RequestDelegate middlewareDelegate = new RequestDelegate((context) =>
                    {
                        //这里开始处理请求
                        context.Response += "End All middleware\n";
                        //不调用 option() 做到短路
                        return Task.CompletedTask;
                    });
                    return middlewareDelegate;
                });


            }

            //开始处理管道
            app.Builb()(context);
            context.Show();
            Console.Read();
        }
    }
}
