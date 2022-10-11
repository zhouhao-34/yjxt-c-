using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using Entity;
using CefSharp;
using CefSharp.WinForms;
using Newtonsoft.Json;

namespace warning
{
    public partial class FmMain : Form
    {
        

        private System.Threading.Timer messageSend;
        private object _oTmp;
        Alarm_BLL alarm_Bll = new Alarm_BLL();
        List_BLL list_BLL = new List_BLL();
        //定义委托
        public delegate void messageSendValue(int proID);
        private messageSendValue myMessageSendValue;

        public static ChromiumWebBrowser chromeBrowser;
        string url;
        public FmMain()
        {
            InitializeComponent();
            try
            {
                url = System.Environment.CurrentDirectory + "\\dist\\index.html";//获取页面地址
                var _settings = new CefSettings();   //下面设置，减少白屏的发生，此为cefSharp的bug
                if (!_settings.MultiThreadedMessageLoop)
                {
                    Application.Idle += (sender, e) => { Cef.DoMessageLoopWork(); };
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message + "Index");
            }
        }
        private void FmMain_Load(object sender, EventArgs e)
        {
            InitializeChromium();//初始化浏览器
            //首页面板
            var list = new Index_BLL().indexPanel();
            myMessageSendValue = new messageSendValue(setTextValue);
			//启动PLC线程
            new DAL.PlcLink().YJ_PLC_linkLog();
            start();
        }
        public void InitializeChromium()
        {
            try
            {
                var settings = new CefSettings
                {
                    Locale = "zh-CN",
                    AcceptLanguageList = "zh-CN,zh;q=0.8",
                    PersistSessionCookies = true,
                    //LocalesDirPath = Path.GetFullPath("cache\\localeDir"),
                    //LogFile = Path.GetFullPath("cache\\LogData"),
                    //UserDataPath = Path.GetFullPath("cache\\UserData"),
                    //CachePath = Path.GetFullPath("cache\\CacheFile"),
                };
                Cef.Initialize(settings);
                //使用提供的设置初始化cef
                //chromeBrowser = new ChromiumWebBrowser(url);
                chromeBrowser = new ChromiumWebBrowser("http://192.168.3.26:8080");
                // 给页面注入调用方法
                chromeBrowser.JavascriptObjectRepository.Register("frmKuchun", new jsVisit(), isAsync: true, options: BindingOptions.DefaultBinder);
                chromeBrowser.FrameLoadEnd += (t, s) =>
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("(function(){")
                        .Append("CefSharp.BindObjectAsync('frmKuchun');")
                        .Append("})();");
                    chromeBrowser.GetFocusedFrame().EvaluateScriptAsync(sb.ToString());
                };

                // 添加浏览器到窗口
                Controls.Add(chromeBrowser);
                chromeBrowser.Dock = DockStyle.Fill;
                //F5刷新
                chromeBrowser.KeyboardHandler = new CEFKeyBoardHander();
                //屏蔽右键
                chromeBrowser.MenuHandler = new MenuHandler();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "InitializeChromium");
            }


        }
        public class CEFKeyBoardHander : IKeyboardHandler
        {
            public bool OnKeyEvent(IWebBrowser browserControl, IBrowser browser, KeyType type, int windowsKeyCode, int nativeKeyCode, CefEventFlags modifiers, bool isSystemKey)
            {
                if (type == KeyType.KeyUp && Enum.IsDefined(typeof(Keys), windowsKeyCode))
                {
                    var key = (Keys)windowsKeyCode;
                    switch (key)
                    {
                        case Keys.F12:
                            browser.ShowDevTools();
                            break;
                    }
                }
                return false;


            }

            public bool OnPreKeyEvent(IWebBrowser chromiumWebBrowser, IBrowser browser, KeyType type, int windowsKeyCode, int nativeKeyCode, CefEventFlags modifiers, bool isSystemKey, ref bool isKeyboardShortcut)
            {
                return false;
            }
        }
        internal class MenuHandler : IContextMenuHandler
        {
            public void OnBeforeContextMenu(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model)
            {
                model.Clear();
            }

            public bool OnContextMenuCommand(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IContextMenuParams parameters, CefMenuCommand commandId, CefEventFlags eventFlags)
            {
                return false;
            }

            public void OnContextMenuDismissed(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame)
            {
            }

            public bool RunContextMenu(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model, IRunContextMenuCallback callback)
            {
                return false;
            }
        }
        private void setTextValue(int proID)
        {

        }
        private void button6_Click(object sender, EventArgs e)
        {
         
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            var list = alarm_Bll.alarmTask();
            foreach (var item in list)
            {
                var obj = new { data = item, name = "111", type = "2" };
                chromeBrowser.ExecuteScriptAsync($"vue.warning('{JsonConvert.SerializeObject(obj)}')");
                return;
            }
              
        }
        //启动预/警通知程序
        public void start() {
            _oTmp = new object();
            messageSend = new System.Threading.Timer(Tick, null, 0, 2000);//参数：委托函数，委托函数参数，在委托函数执行前等待时间，在委托函数执行后等待时间
        }
        private void Tick(object Data)
        {
            SendMail sendMail = new SendMail();
            System.DateTime currentTime = new System.DateTime();
            currentTime = System.DateTime.Now;
            messageSend.Change(-1,-1);
            
            lock (_oTmp)
            {
                var list = alarm_Bll.alarmTask();
                int proID = 0;
                foreach(var item in list)
                {
                    int dval = 0;
                    if(item.unit == "自然日")
                    {
                        DateTime createTime = Convert.ToDateTime(item.createTime);
                        TimeSpan span = currentTime.Subtract(createTime);
                        dval = span.Days + 1;
                    }
                    else
                    {
                        dval = (int)item.DValue;
                    }
                    //把时间null和1900-01-01转成统一1900-01-01
                    DateTime daT;
                    DateTime daTY;
                    if (item.baojing_sendTime == null) {
                        daT = DateTime.Parse("1900-01-01");
                    }
                    else
                    {
                        daT = DateTime.Parse(item.baojing_sendTime.ToString());
                    }
                    if (item.yujing_sendTime == null)
                    {
                        daTY = DateTime.Parse("1900-01-01");
                    }
                    else
                    {
                        daTY = DateTime.Parse(item.yujing_sendTime.ToString());
                    }

                    //先判断是否有报敬的，在判断预警
                    if (dval > item.lifeValue && daT.Year.ToString()=="1900")
                    {
                        proID = Convert.ToInt32(item.proID);
                        //第一参数：proID产品ID，第二参数：1是报警，2是预警报醒
                        Fmalarm fmalarm = new Fmalarm(proID,1);
                        //fmalarm.ShowDialog();
                        //写入报警记录
                        int mid = alarm_Bll.messageSendAdd(proID, "报警");
                        if (mid > 0)
                        {
                            new easyYJEntities().Database.ExecuteSqlCommand(string.Format(@"update YJ_Product set baojing_sendTime='{0}' where proID='{1}'", currentTime, proID));
                            //发送邮件                            
                            var userList = list_BLL.userList(proID);//查询接收人
                            string[] menuArr = new Menu_BLL().GetSysMenu(Convert.ToInt32(item.menuID));//查询工厂/车间/产线名称
                            string productName = "";
                            //组装：工厂/车间/产线/设备名称
                            for(int p = 0; p < menuArr.Length; p++)
                            {
                                if (p == 0)
                                {
                                    productName = menuArr[p].ToString();
                                }
                                else
                                {
                                    productName = productName+"_"+ menuArr[p].ToString();
                                }
                            }
                            var obj = new { data = item, name = productName, type = "2" };
                            chromeBrowser.ExecuteScriptAsync($"warning('{JsonConvert.SerializeObject(obj)}')");
                            //循环发送信息到维护人邮箱和短信
                            if (userList.Count > 0)
                            {
                                foreach (var utem in userList)
                                {
                                    //发送邮件
                                    string mailTo = utem.Email;
                                    string mailTitle = "【设备报警】";
                                    string mailContent= productName+","+item.proName+"设备预警值:"+item.yujingValue+"，当前值:"+ dval + "，设定寿命:"+item.lifeValue;
                                    var sendReslut = sendMail.SendEmail(mailTo, mailTitle, mailContent);//发送
                                    if (sendReslut == "成功")
                                    {
                                        alarm_Bll.messageSendLogAdd(utem.userID,"Eamil", utem.Email, mid, mailContent,"成功");
                                    }
                                    else
                                    {
                                        alarm_Bll.messageSendLogAdd(utem.userID, "Eamil", utem.Email, mid, mailContent, sendReslut);
                                    }
                                    //发送短信:参数一:手机号，参数二：设备名，参数三：预警值，参数四：当前值，参数五：设定寿命
                                    var sendsmsReslut = new SendSms().send(utem.mobile, productName + "_" + item.proName, (int)item.yujingValue, dval, (int)item.lifeValue);
                                    if (sendsmsReslut == "成功")
                                    {
                                        alarm_Bll.messageSendLogAdd(utem.userID, "手机", utem.mobile, mid, mailContent, "成功");
                                    }
                                    else
                                    {
                                        alarm_Bll.messageSendLogAdd(utem.userID, "手机", utem.mobile, mid, mailContent, sendReslut);
                                    }
                                }
                            }
                        }
                    }

                    else if (dval > item.yujingValue && daTY.Year.ToString() == "1900")
                    {
                        proID = Convert.ToInt32(item.proID);
                        Fmalarm fmalarm = new Fmalarm(proID,2);
                        // 弹出框chromeBrowser

                        //fmalarm.ShowDialog();
                        //写入报警记录
                        int mid = alarm_Bll.messageSendAdd(proID, "预警");
                        if (mid > 0)
                        {
                            new easyYJEntities().Database.ExecuteSqlCommand(string.Format(@"update YJ_Product set yujing_sendTime='{0}' where proID='{1}'", currentTime, proID));
                            //发送邮件                            
                            var userList = list_BLL.userList(proID);//查询接收人
                            string[] menuArr = new Menu_BLL().GetSysMenu(Convert.ToInt32(item.menuID));//查询工厂/车间/产线名称
                            string productName = "";
                            //组装：工厂/车间/产线/设备名称
                            for (int p = 0; p < menuArr.Length; p++)
                            {
                                if (p == 0)
                                {
                                    productName = menuArr[p].ToString();
                                }
                                else
                                {
                                    productName = productName + "_" + menuArr[p].ToString();
                                }
                            }
                            var obj = new { data = item,name= productName,type="1" };
                            chromeBrowser.ExecuteScriptAsync($"warning('{JsonConvert.SerializeObject(obj)}')");
                            //chromeBrowser.GetBrowser().MainFrame.ExecuteJavaScriptAsync("document.dispatchEvent(new CustomEvent('event_name', { productName: "+ productName + " }));");
                            //循环发送信息到维护人邮箱
                            if (userList.Count > 0)
                            {
                                foreach (var utem in userList)
                                {
                                    string mailTo = utem.Email;
                                    string mailTitle = "【设备报警】";
                                    string mailContent = productName + "," + item.proName + "设备预警值:" + item.yujingValue + "，当前值:" + dval + "，设定寿命:" + item.lifeValue;
                                    var sendReslut = sendMail.SendEmail(mailTo, mailTitle, mailContent);
                                    if (sendReslut== "成功")
                                    {
                                        alarm_Bll.messageSendLogAdd(utem.userID, "Eamil", utem.Email, mid, mailContent, "成功");
                                    }
                                    else
                                    {
                                        alarm_Bll.messageSendLogAdd(utem.userID, "Eamil", utem.Email, mid, mailContent, sendReslut);
                                    }
                                    //发送短信:参数一:手机号，参数二：设备名，参数三：预警值，参数四：当前值，参数五：设定寿命
                                    var sendsmsReslut = new SendSms().send(utem.mobile, productName +"_"+ item.proName, (int)item.yujingValue, dval, (int)item.lifeValue);
                                    if (sendsmsReslut == "成功")
                                    {
                                        alarm_Bll.messageSendLogAdd(utem.userID, "手机", utem.mobile, mid, mailContent, "成功");
                                    }
                                    else
                                    {
                                        alarm_Bll.messageSendLogAdd(utem.userID, "手机", utem.mobile, mid, mailContent, sendReslut);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            //messageSend.Dispose();//释放资源
            messageSend.Change(1000, 1000);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            
           // FmMenuAdd fmMenuAdd = new FmMenuAdd();
            //fmMenuAdd.ShowDialog();
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            var obj = new { data ="222", name = "aaa", type = "2" };
            chromeBrowser.ExecuteScriptAsync($"warning('{JsonConvert.SerializeObject(obj)}')");
        }
    }
}
