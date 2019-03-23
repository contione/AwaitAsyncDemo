using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyConsole
{
    /// <summary>
    /// 1 await/async语法和使用
    /// 2 原理探究和使用建议
    /// 
    /// await/async 是C#保留关键字，通常是成对出现
    /// async修饰方法，可以单独出现，但是有警告
    /// await在方法体，只能出现在task/async方法前面，只有await会报错
    /// 
    /// 主线程调用async/await方法，主线程遇到await返回执行后续动作，
    ///    await后面的代码会等着task任务的完成后再继续执行
    ///    其实就像把await后面的代码包装成一个continue的回调动作
    ///    然后这个回调动作可能是Task线程，也可能是新的子线程，也可能是主线程
    ///    
    /// 一个async方法，如果没有返回值，可以方法声明返回Task
    /// await/async能够用同步的方式编写代码，但又是非阻塞的
    /// 
    /// .NetFramework4.5----await/async 语法糖：由编译器提供的功能
    /// 
    /// 
    /// async方法在编译后会生成一个状态机(实现了IAsyncStateMachine接口)
    /// 状态机：初始化状态0--执行就修改状态1--再执行就修改状态0---执行就修改状态1---如果出现其他状态就结束了
    ///         红绿灯
    ///         
    /// async方法里面的逻辑其实都放在了MoveNext---主线程new一个状态机状态-1、
    /// ----主线程调用MoveNext---执行了await之前的东西--启动Task---主线程改状态为0--回去干自己的事儿
    /// ---子线程再去MoveNext---状态又回归-1----再执行后续的逻辑--如果需要还可以继续循环
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("这里是控制台测试");
                //AwaitAsyncLibrary.AwaitAsyncClass.TestShow();
                AwaitAsyncLibrary.AwaitAsyncILSpy.Show();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.Read();
        }
    }
}
