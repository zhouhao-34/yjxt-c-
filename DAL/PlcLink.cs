using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using IoTClient.Clients.PLC;
using IoTServer.Servers.PLC;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace DAL

{
    public class PlcLink
    {
        ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();
        private object _oTmp;
        //连接PLC定义
        public OmronFinsClient[] client;
        private IIoTServer server;
        Task[] Threads;
        volatile static bool IsCancel;

        PLC_Dal plc_Dal = new PLC_Dal();
        List_DAL list_Dal = new List_DAL();
        List<readPlcList> Rlist = new List<readPlcList>();
        List<YJ_PLC_list> plcTolist = null;
        private static object locker = new object();//创建锁
        //定时读取看板
        System.Timers.Timer timer;
        private static object _lockKB = new object();
        private static object _lockLinkR = new object();
        List<plcRestartList> plcRestartList = new List<plcRestartList>();
        List<YJ_PLC> shebei = new List<YJ_PLC>();
        //启动PLC连接
        public PlcLink()
        {
            KBstart();
            //始初化多个PLC连接线程
            using (var db = new easyYJEntities())
            {
                shebei = db.YJ_PLC.Where(o => o.status == 1).ToList();
                client = new OmronFinsClient[shebei.Count()];
                Threads = new Task[shebei.Count];
                plcTolist = db.YJ_PLC_list.Where(r=>r.status == 1 && r.chufa < 6 && r.proID != null && r.Type_y==1).AsNoTracking().ToList();
            }
           // _lockKB = new object();
            //messageSend = new System.Threading.Timer(TickKB, null, 0, 1000);//参数：委托函数，委托函数参数，在委托函数执行前等待时间，在委托函数执行后等待时间
        }
        public void KBstart()
        {
            timer = new System.Timers.Timer();
            timer.Interval = 1000;//1秒执行1次
            timer.AutoReset = true;
            timer.Enabled = true;
            timer.Elapsed += Timer;
            GC.KeepAlive(timer);
            timer.Start();
        }
        //重连
        public void YJ_PLC_reStart()
        {
            try
            {
                lock (_lockLinkR)
                {
                    using (var db = new easyYJEntities())
                    {
                        plcRestartList = plcRestartList.ToList();
                        //判断是否有需要重连的
                        if (plcRestartList.Count == 0)
                        {
                            return;
                        }
                        YJ_PLC_linkLog linkLog = new YJ_PLC_linkLog();
                        for (int i = 0; i < plcRestartList.Count; i++)
                        {
                            var plcS = shebei.Where(s => s.plcID == plcRestartList[i].plcID).FirstOrDefault() ;
                            int n = plcRestartList[i].n;//线程号
                            switch (plcS.PLC_brand)
                            {
                                case "Omron":
                                    
                                    client[n] = new OmronFinsClient(plcS.PLC_ip?.Trim(), (int)plcS.PLC_port);
                                    var result = client[n].Open();
                                    if (!result.IsSucceed)
                                    {
                                        linkLog.linkStatus = "重新连接失败";
                                        linkLog.plcID = plcS.plcID;
                                        linkLog.createTime = DateTime.Now;
                                        linkLog.ipAddress = plcS.PLC_ip?.Trim();
                                        //MessageBox.Show($"连接失败：{result.Err}");
                                        IsCancel = false;
                                    }
                                    else
                                    {
                                        linkLog.linkStatus = "重新连接成功";
                                        linkLog.plcID = plcS.plcID;
                                        linkLog.createTime = DateTime.Now;
                                        linkLog.ipAddress = plcS.PLC_ip?.Trim();
                                        IsCancel = true;
                                    }
                                    db.YJ_PLC_linkLog.Add(linkLog);
                                    db.SaveChanges();
                                    break;
                            }
                            if (client[n] != null)
                            {
                                if (client[n].Connected != false)
                                {
                                    //Threads[n].Abort();
                                    plcRestartList.Remove(plcRestartList[i]);//连接成功删除重连数组中的数据
                                    Threads[n] = new Task(() => read(n, shebei[n].plcID, shebei[n].PLC_brand)); //指定线程起始设置
                                    Threads[n].Start();//逐个开启线程
                                }
                            }
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void YJ_PLC_linkLog()
        {
            try
            {
                using (var db = new easyYJEntities())
                {
                    //var shebei = db.YJ_PLC.Where(o => o.status == 1).ToList();
                    //client = new OmronFinsClient[shebei.Count()];
                    //Threads = new Task[shebei.Count];
                    YJ_PLC_linkLog linkLog = new YJ_PLC_linkLog();
                    for (int i = 0; i < shebei.Count; i++)
                    {
                        int n = i;
                        switch (shebei[n].PLC_brand)
                        {
                            case "Omron":
                                client[n] = new OmronFinsClient(shebei[i].PLC_ip?.Trim(), (int)shebei[i].PLC_port);
                                var result = client[n].Open();
                                if (!result.IsSucceed)
                                {
                                    linkLog.linkStatus = "连接失败";
                                    linkLog.plcID = shebei[n].plcID;
                                    linkLog.createTime = DateTime.Now;
                                    linkLog.ipAddress = shebei[i].PLC_ip?.Trim();
                                    //MessageBox.Show($"连接失败：{result.Err}");
                                    IsCancel = false;
                                }
                                else
                                {
                                    linkLog.linkStatus = "连接成功";
                                    linkLog.plcID = shebei[n].plcID;
                                    linkLog.createTime = DateTime.Now;
                                    linkLog.ipAddress = shebei[i].PLC_ip?.Trim();
                                    IsCancel = true;
                                }
                                db.YJ_PLC_linkLog.Add(linkLog);
                                db.SaveChanges();
                                break;
                        }
                        if (client[n] != null)
                        {
                            Threads[n] = new Task(() => read(n, shebei[n].plcID, shebei[n].PLC_brand)); //指定线程起始设置
                            Threads[n].Start();//逐个开启线程
                        }
                        else
                        {
                            //加载重连数组
                            plcRestartList.Add(new plcRestartList { plcID = shebei[i].plcID,n=n });
                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 读取PLC
        /// </summary>
        /// <param name="n">线程号</param>
        /// <param name="plcID">PLCID</param>
        /// <param name="PLC_brand">品牌</param>
        private void read(int n, int plcID, string PLC_brand)
        {
            try
            {
                //_lock.EnterUpgradeableReadLock();
                //循环读取PLC
                while (true)
                {
                    lock (locker)//加锁
                    {
                        using (easyYJEntities db = new easyYJEntities())
                        {
                            //判断连接是否断开
                            //var plcLinkOpen = client[n].Open();
                            if (client[n].Connected==false)
                            {
                                client[n] = new OmronFinsClient(shebei[n].PLC_ip?.Trim(), (int)shebei[n].PLC_port);
                                var result = client[n].Open();
                                var linkStatus = "";
                                if (!result.IsSucceed)
                                {
                                    linkStatus = "重新连接失败";
                                }
                                else
                                {
                                    linkStatus = "重新连接成功";
                                }
                                YJ_PLC_linkLog plog = new YJ_PLC_linkLog
                                {
                                    //plcID = n,
                                    linkStatus = linkStatus,
                                    createTime = DateTime.Now,
                                    ipAddress = client[n].IpEndPoint.Address.ToString(),
                                    
                                };
                                db.YJ_PLC_linkLog.Add(plog);
                                //加载重连数组
                                //plcRestartList.Add(new plcRestartList { plcID = plcID ,n=n});
                                //if (!result.IsSucceed)
                                //{
                                //    IsCancel = false;
                                //    return;
                                //}                                    
                            }
                            if (client[n].Connected == false)
                            {
                                continue;
                            }
                            var plcaddress = plcTolist.Where(r => r.plcID == plcID).ToList();
                            //var plcaddress = db.YJ_PLC_list.Where(r => r.status == 1 && r.chufa < 6 && r.proID != null && r.plcID == plcID).AsNoTracking().ToList();
                            if (plcaddress.Count == 0)
                            {
                                continue;
                            }
                            YJ_PLC_log plcLog = new YJ_PLC_log();
                            //西门子小技巧:1、读取地址支持批量读取，如V2634、V2638、V2642。
                            //关于PLC地址：VB263、VW263、VD263中的B、W、D分别表示byte、word、doubleword数据类型，分别对应C#中的byte、ushort(UInt16)、uint(UInt32)类型。
                            //直接传入地址（如:V263）即可。
                            foreach (var item in plcaddress.ToArray())
                            {
                                //确认产品是否存在
                                var productList = db.YJ_Product.Where(pp => pp.proID == item.proID).ToList();
                                if (productList.Count == 0)
                                {
                                    continue;
                                }
                                dynamic result = null;
                                if (item.chufa == 3)
                                {
                                    //指定信号,先读取信号判断是否满足读取条件，然后读取PLC_address的值
                                    result = readPlcAddress(item.where_PLC_addressType, item.where_PLC_address, n);
                                    bool W = false;
                                    //判断是否读取成功
                                    if (result.IsSucceed)
                                    {
                                        //判断读取的类型
                                        if (item.where_PLC_addressType == "int" || item.where_PLC_addressType == "int16")
                                        {
                                            //读取的值与设置的值按条件比对，满足修改W=true
                                            string whereVal = Convert.ToString(result.Value);
                                            if (item.where_tiaojian == "=")
                                            {
                                                if (whereVal == Convert.ToString(item.where_content))
                                                {
                                                    W = true;
                                                }
                                            }
                                            else if (item.where_tiaojian == ">")
                                            {
                                                if (Convert.ToInt32(whereVal) > Convert.ToInt32(item.where_content))
                                                {
                                                    W = true;
                                                }
                                            }
                                            else if (item.where_tiaojian == "<")
                                            {
                                                if (Convert.ToInt32(whereVal) < Convert.ToInt32(item.where_content))
                                                {
                                                    W = true;
                                                }
                                            }
                                        }
                                        else if (item.where_PLC_addressType == "bool")
                                        {
                                            //读取类型是bool类型，只能是等于，没有大于和小于
                                            bool whereVal = Convert.ToBoolean(result.Value);
                                            if (whereVal == Convert.ToBoolean(item.where_content))
                                            {
                                                W = true;
                                            }
                                        }
                                        else
                                        {
                                            //读取类型是其它类型，只能是等于，没有大于和小于
                                            string whereVal = Convert.ToString(result.Value);
                                            if (whereVal == Convert.ToString(item.where_content))
                                            {
                                                W = true;
                                            }
                                        }
                                    }
                                    //如果W=true即满足读取条件
                                    if (W == true)
                                    {
                                        result = readPlcAddress(item.PLC_addressType, item.PLC_adress, n);
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                                else
                                {
                                    result = readPlcAddress(item.PLC_addressType, item.PLC_adress, n);
                                }

                                string record = "";
                                int status = 0;
                                string type = "读";
                                //读取的数据追加到List集合中
                                if (result.IsSucceed)
                                {
                                    //是否需要回写,回写不为空且当前读取值!=回写值
                                    if (item.returnVal!=null && Convert.ToString(item.returnVal) != Convert.ToString(result.Value))
                                    {
                                        WritePlcAddress(n, plcaddress.Where(o => o.plcListID == item.plcListID).ToList());
                                    }
                                    else if (item.where_returnVal!=null)//判断指定信号是否需要回写
                                    {
                                        WritePlcAddress_W(n, plcaddress.Where(o => o.plcListID == item.plcListID).ToList());
                                    }
                                    lock (Rlist)
                                    {
                                        if (item.returnVal != null && Convert.ToString(item.returnVal) != Convert.ToString(result.Value))
                                        {
                                            //如果回写值不等于当前数据
                                            Rlist.Add(new readPlcList { plclistID = Convert.ToInt32(item.plcListID), val = Convert.ToString(result.Value) });
                                        }                                            
                                    }                                    
                                    //记录读取的数据
                                    plc_Dal.plcListLog_Add(Convert.ToInt32(item.proID), Convert.ToInt32(item.plcID), Convert.ToInt32(item.plcListID), Convert.ToString(result.Value), Convert.ToString(type), record, status);
                                }
                                else
                                {
                                    status = 2;
                                    record = $"[读取 {item.PLC_adress} 失败]：耗时：{result.TimeConsuming}ms";
                                    //记录读取的数据
                                    plc_Dal.plcListLog_Add(Convert.ToInt32(item.proID), Convert.ToInt32(item.plcID), Convert.ToInt32(item.plcListID), "", Convert.ToString(type), record, status);
                                }
                                //.Sleep(200);
                                //判断是否读取成功
                                //if (result.IsSucceed)
                                //{
                                //    status = 1;
                                //    record = $"[读取 {item.PLC_adress} 成功]：{result.Value}\t\t耗时：{result.TimeConsuming}ms";
                                //    //处理读取数据
                                //    ReadWhere(plcaddress.Where(o => o.plcListID == item.plcListID).ToList(), Convert.ToString(result.Value));
                                //    //回写数据不为空开始回写
                                //    if (!string.IsNullOrEmpty(item.returnVal))
                                //    {
                                //        WritePlcAddress(n, plcaddress.Where(o => o.plcListID == item.plcListID).ToList());
                                //    }
                                //    else if (!string.IsNullOrEmpty(item.where_returnVal.ToString()))//判断指定信号是否需要回写
                                //    {
                                //        WritePlcAddress_W(n, plcaddress.Where(o => o.plcListID == item.plcListID).ToList());
                                //    }
                                //    else
                                //    {
                                //        //记录读取的数据
                                //        plc_Dal.plcListLog_Add(Convert.ToInt32(item.proID), Convert.ToInt32(item.plcID), Convert.ToInt32(item.plcListID), Convert.ToString(result.Value), Convert.ToString(type), record, status);
                                //    }

                                //}
                                //else
                                //{
                                //    status = 2;
                                //    record = $"[读取 {item.PLC_adress} 失败]：耗时：{result.TimeConsuming}ms";
                                //    //记录读取的数据
                                //    plc_Dal.plcListLog_Add(Convert.ToInt32(item.proID), Convert.ToInt32(item.plcID), Convert.ToInt32(item.plcListID), "", Convert.ToString(type), record, status);
                                //}
                            }
                            //重载plctolist数据,准备下循环使用
                            plcTolist = db.YJ_PLC_list.Where(r => r.status == 1 && r.chufa < 6 && r.proID != null).AsNoTracking().ToList();

                            if (Rlist.Count == 0)
                            {
                                continue;
                            }
                            //写入时上锁
                            try
                            {
                                _lock.EnterWriteLock();
                                //处理读到的PLC数据
                                int fh = list_Dal.ReadUpdate(Rlist);
                                if (fh == 0)
                                {
                                    Rlist.Clear();
                                    continue;
                                }
                                //Console.WriteLine($"fh:{fh}");
                            }
                            finally
                            {
                                Console.WriteLine(n);
                                _lock.ExitWriteLock();
                            }
                            Rlist.Clear();
                        }
                        
                        if (IsCancel == false)
                        {
                            return;
                        }
                    }
                    Thread.Sleep(1000);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //finally
            //{
            //    _lock.ExitUpgradeableReadLock();
            //}
        }
        /// <summary>
        /// 读取成功后处理数据
        /// </summary>
        /// <param name="list">plc_list数组</param>
        /// <param name="readVal">读取的值</param>
        //private void ReadWhere(List<YJ_PLC_list> list, string readVal)
        //{
        //    string shuju = list[0].shuju_u;//上一次读取的数据
        //    int proID = Convert.ToInt32(list[0].proID);
        //    string chuli = list[0].chuli;
        //    int plcListID = Convert.ToInt32(list[0].plcListID);
        //    bool res = false;
            
        //    if (list[0].chufa == 1)
        //    {
        //        //不等于空
        //        if (!string.IsNullOrEmpty(readVal) && readVal != "0")
        //        {
        //            res = list_Dal.proUpdate_DValue(list, readVal, chuli, plcListID);
        //        }
        //    }
        //    else if (list[0].chufa == 2)
        //    {
        //        if (list[0].PLC_addressType == "bool")
        //        {
        //            //如果布尔型判断等于true还是等于false才能执行
        //            if (Convert.ToBoolean(readVal) == true && list[0].chufa == 4)
        //            {
        //                res = list_Dal.proUpdate_DValue2(list, readVal, chuli);
        //            }
        //            else if (Convert.ToBoolean(readVal) == false && list[0].chufa == 5)
        //            {
        //                res = list_Dal.proUpdate_DValue2(list, readVal, chuli);
        //            }
        //        }
        //        else if (shuju != readVal)
        //        {
        //            res = list_Dal.proUpdate_DValue2(list, readVal, chuli);
        //        }
        //    }
        //    else if (list[0].chufa == 3)//3和2处理数据程序相同
        //    {
        //        if (list[0].PLC_addressType == "bool")
        //        {
        //            //如果布尔型判断等于true还是等于false才能执行
        //            if (Convert.ToBoolean(readVal) == true && list[0].chufa == 4)
        //            {
        //                res = list_Dal.proUpdate_DValue2(list, readVal, chuli);
        //            }
        //            else if (Convert.ToBoolean(readVal) == false && list[0].chufa == 5)
        //            {
        //                res = list_Dal.proUpdate_DValue2(list, readVal, chuli);
        //            }
        //        }
        //        //else if (shuju != readVal)
        //        else if (!string.IsNullOrEmpty(readVal) || readVal!="0")
        //        {
        //            res = list_Dal.proUpdate_DValue2(list, readVal, chuli);
        //        }
        //    }
        //    else if (list[0].chufa == 4 || list[0].chufa == 5)
        //    {
        //        if (list[0].PLC_addressType == "bool")
        //        {
        //            //如果布尔型判断等于true还是等于false才能执行
        //            if (Convert.ToBoolean(readVal) == true && list[0].chufa == 4)
        //            {
        //                res = list_Dal.proUpdate_DValue3(list, readVal, chuli);
        //            }
        //            else if (Convert.ToBoolean(readVal) == false && list[0].chufa == 5)
        //            {
        //                res = list_Dal.proUpdate_DValue3(list, readVal, chuli);
        //            }
        //        }
        //    }
        //}
        /// <summary>
        /// 读取PLC值
        /// </summary>
        /// <param name="plcAddressType">读取数据的类型</param>
        /// <param name="plcAddress">读取地址</param>
        /// <param name="n">PLC连接线程号</param>
        /// <returns>
        /// byte = byte
        /// int16 = short
        /// Dint32 = int
        /// Uint16 = ushort
        /// UDint32 = uint
        /// real = float
        /// bool = bool
        /// string = string
        /// </returns>
        private dynamic readPlcAddress(string plcAddressType, string plcAddress, int n)
        {
            try
            {
                dynamic result = null;
                if (plcAddressType == "bool")
                {
                    result = client[n].ReadBoolean(plcAddress);
                }
                else if (plcAddressType == "int16")
                {
                    result = client[n].ReadInt16(plcAddress);
                }
                else if (plcAddressType == "Uint16")
                {
                    result = client[n].ReadUInt16(plcAddress);
                }
                else if (plcAddressType == "Dint32")
                {
                    result = client[n].ReadInt32(plcAddress);
                }
                else if (plcAddressType == "UDint32")
                {
                    result = client[n].ReadUInt32(plcAddress);
                }
                //else if (plcAddressType == "long")
                //{
                //    result = client[n].ReadInt64(plcAddress);
                //}
                //else if (plcAddressType == "ulong")
                //{
                //    result = client[n].ReadUInt64(plcAddress);
                //}
                else if (plcAddressType == "real")
                {
                    result = client[n].ReadFloat(plcAddress);
                }
                //else if (plcAddressType == "double")
                //{
                //    result = client[n].ReadDouble(plcAddress);
                //}
                else if (plcAddressType == "string")
                {
                    result = client[n].ReadString(plcAddress);
                }
                return result;
            }catch(Exception ex)
            {
                throw ex;
            }
            
        }
        /// <summary>
        /// 读取PLC值
        /// </summary>
        /// <param name="plcAddressType">写入数据的类型</param>
        /// <param name="plcAddress">写入地址</param>
        /// <param name="content">写入内容</param>
        /// <param name="n">PLC连接线程号</param>
        /// <returns>
        /// byte = byte
        /// int16 = short
        /// Dint32 = int
        /// Uint16 = ushort
        /// UDint32 = uint
        /// real = float
        /// bool = bool
        /// string = string
        /// </returns>
        private void WritePlcAddress(int n, List<YJ_PLC_list> list)
        {
            dynamic result = null;
            string plcAddressType = list[0].PLC_addressType;
            string plcAddress = list[0].PLC_adress;
            string content = list[0].returnVal;
            //判断回写参数是否为空
            if (string.IsNullOrWhiteSpace(plcAddressType))
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(plcAddress))
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(content))
            {
                return;
            }
            try
            {
                
                if (plcAddressType == "bool")
                {
                    if (!bool.TryParse(content, out bool bit))
                    {
                        if (content == "0")
                            bit = false;
                        else if (content == "1")
                            bit = true;

                    }
                    result = client[n].Write(plcAddress, bit);
                }
                else if (plcAddressType == "int16")
                {
                    result = client[n].Write(plcAddress, short.Parse(content));
                }
                else if (plcAddressType == "Uint16")
                {
                    result = client[n].Write(plcAddress, ushort.Parse(content));
                }
                else if (plcAddressType == "Dint32")
                {
                    result = client[n].Write(plcAddress, int.Parse(content));
                }
                else if (plcAddressType == "UDint32")
                {
                    result = client[n].Write(plcAddress, uint.Parse(content));
                }
                //else if (plcAddressType == "long")
                //{
                //    result = client[n].Write(plcAddress, long.Parse(content));
                //}
                //else if (plcAddressType == "ulong")
                //{
                //    result = client[n].Write(plcAddress, ulong.Parse(content));
                //}
                else if (plcAddressType == "real")
                {
                    result = client[n].Write(plcAddress, float.Parse(content));
                }
                else if (plcAddressType == "double")
                {
                    result = client[n].Write(plcAddress, double.Parse(content));
                }
                else if (plcAddressType == "string")
                {
                    result = client[n].Write(plcAddress, content);
                }
                

                string record = "";
                int status = 0;
                string type = "写";
                //判断是否读取成功
                if (result.IsSucceed)
                {
                    status = 1;
                    record = $"[写入 {plcAddress} 成功]：{content}\t\t耗时：{result.TimeConsuming}ms";
                }
                else
                {
                    status = 2;
                    record = $"[写入  {plcAddress} 失败]：{content}\t\t耗时：{result.TimeConsuming}ms";
                }
                //记录写入的数据
                plc_Dal.plcListLog_Add(Convert.ToInt32(list[0].proID), Convert.ToInt32(list[0].plcID), Convert.ToInt32(list[0].plcListID), content, Convert.ToString(type), record, status);
            }
            catch (Exception ex)
            {
                throw ex;
                //MessageBox.Show(ex.Message);
            }
        }
        private void WritePlcAddress_W(int n, List<YJ_PLC_list> list)
        {
            dynamic result = null;
            string plcAddressType = list[0].where_PLC_addressType;
            string plcAddress = list[0].where_PLC_address;
            string content = list[0].where_returnVal.ToString();
            //判断回写参数是否为空
            if (string.IsNullOrWhiteSpace(plcAddressType))
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(plcAddress))
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(content))
            {
                return;
            }
            try
            {

                if (plcAddressType == "bool")
                {
                    if (!bool.TryParse(content, out bool bit))
                    {
                        if (content == "0")
                            bit = false;
                        else if (content == "1")
                            bit = true;

                    }
                    result = client[n].Write(plcAddress, bit);
                }
                else if (plcAddressType == "int16")
                {
                    result = client[n].Write(plcAddress, short.Parse(content));
                }
                else if (plcAddressType == "Uint16")
                {
                    result = client[n].Write(plcAddress, ushort.Parse(content));
                }
                else if (plcAddressType == "Dint32")
                {
                    result = client[n].Write(plcAddress, int.Parse(content));
                }
                else if (plcAddressType == "UDint32")
                {
                    result = client[n].Write(plcAddress, uint.Parse(content));
                }
                //else if (plcAddressType == "long")
                //{
                //    result = client[n].Write(plcAddress, long.Parse(content));
                //}
                //else if (plcAddressType == "ulong")
                //{
                //    result = client[n].Write(plcAddress, ulong.Parse(content));
                //}
                else if (plcAddressType == "real")
                {
                    result = client[n].Write(plcAddress, float.Parse(content));
                }
                else if (plcAddressType == "double")
                {
                    result = client[n].Write(plcAddress, double.Parse(content));
                }
                else if (plcAddressType == "string")
                {
                    result = client[n].Write(plcAddress, content);
                }


                string record = "";
                int status = 0;
                string type = "写";
                //判断是否读取成功
                if (result.IsSucceed)
                {
                    status = 1;
                    record = $"[写入 {plcAddress} 成功]：{content}\t\t耗时：{result.TimeConsuming}ms";
                }
                else
                {
                    status = 2;
                    record = $"[写入  {plcAddress} 失败]：{content}\t\t耗时：{result.TimeConsuming}ms";
                }
                //记录写入的数据
                plc_Dal.plcListLog_Add(Convert.ToInt32(list[0].proID), Convert.ToInt32(list[0].plcID), Convert.ToInt32(list[0].plcListID), content, Convert.ToString(type), record, status);
            }
            catch (Exception ex)
            {
                throw ex;
                //MessageBox.Show(ex.Message);
            }
        }
        private void Timer(object sender, ElapsedEventArgs e)
        {
            //初始化看板数据
            List<KB_DataSave> DataSaveList = null;
            using (easyYJEntities db = new easyYJEntities())
            {
                //判断当前看板记录是否创建，如果没有创建则创建一条当前看板记录
                DataSaveList = db.KB_DataSave.OrderBy(o => o.ID).Where(o => o.Datetime.Value.Day == DateTime.Now.Day && o.Datetime.Value.Month == DateTime.Now.Month && o.Datetime.Value.Year == DateTime.Now.Year).ToList();
                if (DataSaveList.Count == 0)
                {
                    KB_DataSave ds = new KB_DataSave
                    {
                        ShiftName = "空班",
                        Datetime = DateTime.Now,
                        PlanNumber = 0,
                        Single = 0,
                        ChangeNumber = 0,
                        Differences= 0,
                        Actual=0,
                    };
                    db.KB_DataSave.Add(ds);
                    db.SaveChanges();
                }
                else
                {
                    //DateTime abc = DateTime.Now.ToString("HH:mm");
                    //查询当前生产班次
                    var ShiftList = db.KB_Shift.ToList();
                    DateTime nowDT = DateTime.Now;
                    TimeSpan dspNow = nowDT.TimeOfDay;
                    int shiftID = 0;
                    foreach (var item in ShiftList)
                    {
                        TimeSpan startDT = DateTime.Parse(item.startTime).TimeOfDay;
                        TimeSpan endDT = DateTime.Parse(item.endTime).TimeOfDay;
                        if (dspNow > startDT&& dspNow < endDT)
                        {
                            shiftID = item.shiftID;
                            //如果当前班次不等于看板当前班次，则修改看板班次
                            if (DataSaveList[0].ShiftName != item.ShiftName)
                            {
                                db.Database.ExecuteSqlCommand(string.Format("update KB_DataSave set ShiftName='{0}' where ID='{1}'", item.ShiftName, DataSaveList[0].ID));
                                
                            }
                        }
                    }
                    //重新获取数据
                    DataSaveList = db.KB_DataSave.OrderBy(o => o.ID).Where(o => o.Datetime.Value.Day == DateTime.Now.Day && o.Datetime.Value.Month == DateTime.Now.Month && o.Datetime.Value.Year == DateTime.Now.Year).ToList();
                    //如果空班不获取数据
                    if (DataSaveList[0].ShiftName == null)
                    {
                        return;
                    }
                    
                    //var modelList
                    //实际产量=今天任务已生产数量相加
                    var dTime = DateTime.Now.ToString("yyyy-MM-dd");
                    var planList = db.KB_ProductionPlan.OrderByDescending(oo=>oo.endTime).Where(p => p.planTime.ToString() == dTime).ToList();
                    if (planList.Count == 0)
                    {
                        return;
                    }
                    int Actual = 0;
                    
                    if (planList.Count > 0)
                    {
                        string st1 = "15:30";
                        string st2 = DateTime.Now.ToString("HH:mm");
                        DateTime dt1 = Convert.ToDateTime(st1);
                        DateTime dt2 = Convert.ToDateTime(st2);
                        //生产计划大于0时更新面板,15：30后不在更新看板数据
                        if (shiftID > 0 && DataSaveList[0].ModelID > 0 && DateTime.Compare(dt1, dt2) > 0)
                        {
                            int model_DID = Convert.ToInt32(DataSaveList[0].ModelID);
                            var ShiftList_D = ShiftList.Where(sd => sd.shiftID == shiftID).FirstOrDefault();
                            var model_D = db.KB_Model.Where(mm => mm.modelID == model_DID).FirstOrDefault();
                            DateTime EndTime1 = Convert.ToDateTime(ShiftList_D.endTime);//班次结束时间
                            DateTime StartTime1 = Convert.ToDateTime(ShiftList_D.startTime);//班次开始时间
                            TimeSpan timeSpan = EndTime1 - StartTime1;
                            int ChangeNumber = Convert.ToInt32(DataSaveList[0].ChangeNumber);//换模次数
                            double ChangeTime = Convert.ToDouble(model_D.ChangeTime);//换模时间
                            double CycleTime = timeSpan.TotalMinutes - (ChangeNumber * ChangeTime);//实际总工作时间
                            double IntervalTime = Convert.ToDouble(model_D.intervalTime);//生产型号节拍
                            DateTime CurrentTime = Convert.ToDateTime(DateTime.Now.ToString("HH:mm:ss").ToString());
                            //if (planList[0].endTime != null)
                            //{
                            //    CurrentTime = Convert.ToDateTime(planList[0].endTime);//使用生产任务最后结束的时间来计算
                            //}
                            StartTime1 = Convert.ToDateTime(ShiftList_D.startTime);
                            timeSpan = CurrentTime - StartTime1;
                            double WorkingTime = timeSpan.TotalMinutes - (ChangeNumber * ChangeTime);//实际已工作时间

                            int Target = Convert.ToInt32(WorkingTime / (IntervalTime / 60));//目标产量
                            int Plan = Convert.ToInt32(CycleTime / (IntervalTime / 60));//计划产量
                            int Differences = (int)(DataSaveList[0].Actual - Target);//差异值
                            db.Database.ExecuteSqlCommand(string.Format("update KB_DataSave set PlanNumber='{0}',Target='{1}',Differences='{2}' where ID='{3}'", Plan, Target, Differences, DataSaveList[0].ID));
                            db.SaveChanges();
                        }
                        foreach (var pitem in planList)
                        {
                            Actual = (int)(Actual + pitem.ActualProduction);
                        }
                        //如果计划生产数量发生变
                        if (DataSaveList[0].Actual != Actual)
                        {
                            db.Database.ExecuteSqlCommand(string.Format("update KB_DataSave set Actual='{0}' where ID='{1}'", Actual, DataSaveList[0].ID));
                            //累加模具使用次数
                            //查询当前生产型号使用模具
                            int dmID = (int)DataSaveList[0].ModelID;
                            var mojuModel = db.KB_mojuModel.Where(m2=>m2.status==1 && m2.modelID== dmID).ToList();
                            if (mojuModel.Count > 0)
                            {
                                string mojuUpdateSql = "";
                                foreach(var aa in mojuModel)
                                {
                                    mojuUpdateSql += string.Format(@"update KB_moju set mojuNub=mojuNub+1,updateTime='{0}' where mjID='{1}'", DateTime.Now, aa.mjID);
                                }
                                //批量修改模具使用次数
                                db.Database.ExecuteSqlCommand(mojuUpdateSql);
                                db.SaveChanges();
                            }
                        }
                    }
                    var planListEnd = planList.Where(p => p.startTime != null && p.endTime == null).ToList();
                }
            }
            //YJ_PLC_reStart();
            System.DateTime currentTime = new System.DateTime();
            currentTime = System.DateTime.Now;
            lock (_lockKB)
            {
                //读取当前生产型号
                using (easyYJEntities db = new easyYJEntities())
                {
                    var gudingList = db.KB_PLC_guding.ToList();
                    var guding = gudingList.Where(g => g.GID == 1).FirstOrDefault();
                    if (guding==null)
                    {
                        return;
                    }
                    if(!string.IsNullOrEmpty(guding.plc_address) && !string.IsNullOrEmpty(guding.plc_addressType))
                    {
                        //连接读取当前型号PLC
                        var PLC = db.YJ_PLC.Where(p => p.plcID == guding.plcID).FirstOrDefault();
                        int guding_i = 9999999;
                        for(int i = 0; i < client.Length; i++)
                        {
                            //没成功连接跳出
                            if (client[i] == null)
                            {
                                continue;
                            }
                            string ip = client[i].IpEndPoint.Address.ToString();
                            if (PLC.PLC_ip == ip)
                            {
                                guding_i = i;
                            }
                        }
                        //没有找到连接退出
                        if(guding_i == 9999999)
                        {
                            return;
                        }
                        //var result = client[guding_i].Open();
                        if (client[guding_i].Connected==true)
                        {
                            //读取当前PLC
                            dynamic plcReadGD = readPlcGD(guding.plc_addressType,guding.plc_address,client[guding_i], Convert.ToUInt16(guding.addressLenth));
                            //string modelName_PLC = Convert.ToString(plcReadGD.Value);
                            //var modelList = db.KB_Model.Where(m=>m.ModelName==modelName_PLC).ToList();
                            int modelName_PLC = Convert.ToInt32(plcReadGD.Value);
                            var modelList = db.KB_Model.Where(m=>m.PLC_modelID==modelName_PLC).ToList();
                            DataSaveList = DataSaveList.ToList();
                            if (modelList.Count > 0)
                            {
                                //如果看板当前生产型号！=正在生产的型号则修改
                                if (DataSaveList[0].ModelID != modelList[0].modelID)
                                {
                                    //判断是否第一个生产任务
                                    if (DataSaveList[0].ModelID != null)
                                    {
                                        //增加换模次数
                                        db.Database.ExecuteSqlCommand(string.Format("update KB_DataSave set ChangeNumber='{0}' where ID='{1}'", DataSaveList[0].ChangeNumber + 1, DataSaveList[0].ID));
                                    }
                                    //修改当前生产型号
                                    db.Database.ExecuteSqlCommand(string.Format("update KB_DataSave set ModelID='{0}',ModelName='{1}' where ID='{2}'", modelList[0].modelID, modelList[0].ModelName, DataSaveList[0].ID));
                                    //关闭开始时不为空并结束时间为空的计划生产任务
                                    //db.Database.ExecuteSqlCommand(string.Format("update KB_ProductionPlan set endTime='{0}' where planTime='"+DateTime.Now.ToString("yyyy-MM-dd") +"' and startTime!=NULL and endTime=NULL", DateTime.Now));
                                    //修改当前生产型号的开始时间
                                    db.Database.ExecuteSqlCommand(string.Format("update KB_ProductionPlan set startTime='{0}' where ModelID='{1}' and DateDiff(dd,planTime,getdate())=0", DateTime.Now, modelList[0].modelID));
                                }
                                db.SaveChanges();
                                //根据当前生产型号名称，查询当前生产型号读取生产数据的PLC地址
                                int modelID_D = (int)modelList[0].plcListID;
                                List<YJ_PLC_list> plcList = db.YJ_PLC_list.Where(p => p.plcListID == modelID_D && p.Type_y == 2 && p.status == 1).ToList();
                                if (plcList.Count == 0)
                                {
                                    return;
                                }
                                int plcID_D = (int)plcList[0].plcID;
                                var plc2 = db.YJ_PLC.Where(y=>y.plcID== plcID_D).FirstOrDefault();
                                int list_i = 9999999;
                                for (int i = 0; i < client.Length; i++)
                                {
                                    //没成功连接跳出
                                    if (client[i] == null)
                                    {
                                        continue;
                                    }
                                    string ip = client[i].IpEndPoint.Address.ToString();
                                    if (plc2.PLC_ip == ip)
                                    {
                                        list_i = i;
                                    }
                                }
                                //没有找到连接退出
                                if (list_i == 9999999)
                                {
                                    return;
                                }
                                //var modelRead = client[list_i].Open();
                                if (client[list_i].Connected == true)
                                {
                                    //读取单产和OEE
                                    //var guding_dc= gudingList.Where(g => g.GID == 3).FirstOrDefault();
                                    var guding_oee = gudingList.Where(g => g.GID == 2).FirstOrDefault();
                                    int Single = 0;
                                    int OEE = 0;
                                    //if (!string.IsNullOrEmpty(guding_dc.plc_address))
                                    //{
                                    //    dynamic plcRead_DC = readPlcGD(guding_dc.plc_addressType, guding_dc.plc_address, client[guding_i], Convert.ToUInt16(guding_dc.addressLenth));
                                    //    if (plcRead_DC != null)
                                    //    {
                                    //        Single = plcRead_DC.Value != null ? Convert.ToInt32(plcRead_DC.Value) : 0;
                                    //    }
                                    //}
                                    if (!string.IsNullOrEmpty(guding_oee.plc_address))
                                    {
                                        dynamic plcRead_OEE = readPlcGD(guding_oee.plc_addressType, guding_oee.plc_address, client[guding_i], Convert.ToUInt16(guding_oee.addressLenth));
                                        if (plcRead_OEE != null)
                                        {
                                            OEE = Convert.ToInt32(plcRead_OEE.Value);
                                        }
                                    }
                                    
                                    //读取生产数量
                                    dynamic planNubRead = readPlcGD(plcList[0].PLC_addressType, plcList[0].PLC_adress, client[list_i],Convert.ToUInt16(plcList[0].addressLenth));
                                    int planNub2 = Convert.ToInt32(planNubRead.Value);
                                    int planNub = new KanbanPlc_DAL().ReadUpdate(plcList[0].plcListID,Convert.ToString(planNub2));
                                    int modelID = modelList[0].modelID;
                                    var productionPlanList = db.KB_ProductionPlan.Where(kk => kk.ModelID == modelID && kk.planTime.Value.Year == DateTime.Now.Year && kk.planTime.Value.Month == DateTime.Now.Month && kk.planTime.Value.Day == DateTime.Now.Day).ToList();
                                    if (productionPlanList.Count > 0)
                                    {
                                        if(planNub > 0)//判断是否需要累加，0不需要累加生产任务产量
                                        {
                                            int shijianNub = (int)(planNub + productionPlanList[0].ActualProduction);

                                            //修改生产任务实际产量
                                            if (productionPlanList[0].startTime == null)
                                            {
                                                db.Database.ExecuteSqlCommand(string.Format("update KB_ProductionPlan set ActualProduction='{0}',endTime='{1}',startTime='{2}' where planID='{3}'", shijianNub, DateTime.Now, DateTime.Now, productionPlanList[0].planID));
                                            }
                                            else
                                            {
                                                db.Database.ExecuteSqlCommand(string.Format("update KB_ProductionPlan set ActualProduction='{0}',endTime='{1}' where planID='{2}'", shijianNub, DateTime.Now, productionPlanList[0].planID));
                                            }
                                            //修改单项产项=当前生产任务的生产数量
                                            db.Database.ExecuteSqlCommand(string.Format("update KB_DataSave set Single='{0}',OEE='{1}' where ID='{2}'", shijianNub, OEE, DataSaveList[0].ID));
                                        }                                        
                                    }
                                }
                            }
                        }
                    }
                }                    
            }
        }
        //固定PLC地址读取
        private dynamic readPlcGD(string plcAddressType, string plcAddress, OmronFinsClient clientLink,ushort addressLenth)
        {
            try
            {
                dynamic result = null;
                if (plcAddressType == "bool")
                {
                    result = clientLink.ReadBoolean(plcAddress);
                }
                else if (plcAddressType == "int16")
                {
                    result = clientLink.ReadInt16(plcAddress);
                }
                else if (plcAddressType == "Uint16")
                {
                    result = clientLink.ReadUInt16(plcAddress);
                }
                else if (plcAddressType == "Dint32")
                {
                    result = clientLink.ReadInt32(plcAddress);
                }
                else if (plcAddressType == "UDint32")
                {
                    result = clientLink.ReadUInt32(plcAddress);
                }
                else if (plcAddressType == "real")
                {
                    result = clientLink.ReadFloat(plcAddress);
                }
                //else if (plcAddressType == "string")
                //{
                //    result = clientLink.ReadString(plcAddress, addressLenth);
                //}
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
