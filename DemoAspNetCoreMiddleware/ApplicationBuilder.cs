using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace DemoAspNetCoreMiddleware
{
    /// <summary>
    /// 给管道添加中间件核心类
    /// </summary>
    public class ApplicationBuilder
    {
        /// <summary>
        /// 所有的中间件(我理解为处理管道)
        /// </summary>
        private readonly IList<Func<RequestDelegate, RequestDelegate>> _components = new List<Func<RequestDelegate, RequestDelegate>>();

        /// <summary>
        /// 增加中间件
        /// </summary>
        /// <returns></returns>
        public ApplicationBuilder Use(Func<RequestDelegate, RequestDelegate> middleware)
        {
            this._components.Add(middleware);
            return this;
        }

        
        /// <summary>
        /// 组合中间件
        /// </summary>
        /// <returns></returns>
        public RequestDelegate Builb()
        {
            //创建没有任何中间件 就返回此中间件结果
            RequestDelegate requestDelegate = new RequestDelegate((conetxt) =>
            {
                conetxt.Response += "\n404 没有任何中间件请求\n";
                return Task.CompletedTask;
            });

            //组合中间件  需要把中间件集合倒出来 后面讲到为什么
            foreach (var item in this._components.Reverse())
            {
                //这是指定中间件的下一个 
                requestDelegate = item(requestDelegate);
                
            }
            return requestDelegate;
        }
        

    }
}
