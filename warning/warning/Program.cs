using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;

namespace warning
{
    static class Program
    {
        // /<summary>
        /// 该函数设置由不同线程产生的窗口的显示状态
        /// </summary>
        /// <param name = "hWnd" > 窗口句柄 </ param >
        /// < param name="cmdShow">指定窗口如何显示。查看允许值列表，请查阅ShowWlndow函数的说明部分</param>
        /// <returns>如果函数原来可见，返回值为非零；如果函数原来被隐藏，返回值为零</returns>
        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);

        /// <summary>
        ///  该函数将创建指定窗口的线程设置到前台，并且激活该窗口。键盘输入转向该窗口，并为用户改各种可视的记号。
        ///  系统给创建前台窗口的线程分配的权限稍高于其他线程。 
        /// </summary>
        /// <param name="hWnd">将被激活并被调入前台的窗口句柄</param>
        /// <returns>如果窗口设入了前台，返回值为非零；如果窗口未被设入前台，返回值为零</returns>
        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("User32.dll", EntryPoint = "FindWindow")]

        private extern static IntPtr FindWindow(string lpClassName, string lpWindowName);

        private const int SW_SHOWNOMAL = 1;
        private static void HandleRunningInstance(Process instance)
        {
            IntPtr i = instance.MainWindowHandle;
            if (i.ToString() == "0")
            {
                i = FindWindow(null, "林内大型冲床看板");
                ShowWindowAsync(i, SW_SHOWNOMAL);
                SetForegroundWindow(i);
            }
            else
            {
                ShowWindowAsync(i, SW_SHOWNOMAL);
                SetForegroundWindow(i);
            }
        }
        private static Process RuningInstance()
        {
            Process currentProcess = Process.GetCurrentProcess();
            Process[] Processes = Process.GetProcessesByName(currentProcess.ProcessName);
            foreach (Process process in Processes)
            {
                if (process.Id != currentProcess.Id)
                {
                    if (Assembly.GetExecutingAssembly().Location.Replace("/", "\\") == currentProcess.MainModule.FileName)
                    {
                        return process;
                    }
                }
            }
            return null;
        }
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Process process = RuningInstance();
                if (process == null)
                {
                    //处理未捕获的异常   
                    Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                    //处理UI线程异常   
                    Application.ThreadException += Application_ThreadException;
                    //处理非UI线程异常   
                    AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new FmMain());
                }
                else
                {
                    HandleRunningInstance(process);
                }                
            }
            catch(Exception ex)
            {
                var strDateInfo = "出现应用程序未处理的异常：" + DateTime.Now + "";
                var str = string.Format(strDateInfo + "异常类型：{0}异常消息：{1}异常信息：{2}",ex.GetType().Name, ex.Message, ex.StackTrace);

                WriteLog(str);//日志写入
                MessageBox.Show("发生1错误，请查看程序日志！", "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);//进行弹窗提示
                Environment.Exit(0);
            }            
        }
        /// <summary>
        ///错误弹窗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            string str;
            var strDateInfo = "出现应用程序未处理的异常：" + DateTime.Now + "";
            var error = e.Exception;
            if (error != null)
            {
                str = string.Format(strDateInfo + "异常类型：{0}异常消息：{1}异常信息：{2}",error.GetType().Name, error.Message, error.StackTrace);
            }
            else
            {
                str = string.Format("应用程序线程错误:{0}", e);
            }

            WriteLog(str);
            MessageBox.Show("发生错误，请查看程序日志！", "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Environment.Exit(0);
        }


        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var error = e.ExceptionObject as Exception;
            var strDateInfo = "出现应用程序未处理的异常：" + DateTime.Now + "";
            var str = error != null ? string.Format(strDateInfo + "Application UnhandledException:{0};堆栈信息:{1}", error.Message, error.StackTrace) : string.Format("Application UnhandledError:{0}", e);

            WriteLog(str);
            MessageBox.Show("发生错误，请查看程序日志！", "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Environment.Exit(0);
        }
        /// <summary>
        /// 写文件
        /// </summary>
        /// <param name="str"></param>
        static void WriteLog(string str)
        {
            Log_BLL log_BLL = new Log_BLL();
            log_BLL.Log_ALL(str);
            if (!Directory.Exists("ErrLog"))
            {
                Directory.CreateDirectory("ErrLog");
            }


            using (var sw = new StreamWriter(@"ErrLogErrLog.txt", true))
            {
                sw.WriteLine(str);
                sw.WriteLine("---------------------------------------------------------");
                sw.Close();
            }
        }
    }
}
