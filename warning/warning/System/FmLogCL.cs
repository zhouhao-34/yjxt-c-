using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;

namespace warning
{
    public partial class FmLogCL : Form
    {
        List_BLL list_BLL = new List_BLL();
        Menu_BLL menu_BLL = new Menu_BLL();
        public FmLogCL()
        {
            InitializeComponent();
        }

        private void FmLog_Load(object sender, EventArgs e)
        {
            readList(20,0,0);
        }
        public void readList(int PageIndex, int PageSize, int seach_menuID)
        {
            var list = list_BLL.proLogListCL(PageIndex, PageSize, seach_menuID);
            this.LogView1.Rows.Clear();
            //if (list.Count > 0)
            //{
            //    for (int i = 0; i < list.Count; i++)
            //    {
            //        this.LogView1.Rows.Add();
            //        //查询所属菜单及所有父级菜单名
            //        int menuID = Convert.ToInt32(list[i].menuID);
            //        string[] menuNameArr = menu_BLL.GetSysMenu(menuID);
            //        string menuName = "";
            //        if (menuNameArr.Length > 0)
            //        {
            //            for (int m = 0; m < menuNameArr.Length; m++)
            //            {
            //                if (m == 0)
            //                {
            //                    menuName = menuNameArr[m];
            //                }
            //                else
            //                {
            //                    menuName = menuName + "_" + menuNameArr[m];
            //                }
            //            }
            //        }
            //        //查询通知维护人
            //        var sendUserList = list_BLL.manageSendList(list[i].MID);
            //        string sendUserName = "";
            //        //根据userID查询会员名
            //        var sendUserList2 = sendUserList.GroupBy(d => new { d.userID }).Select(d => d.FirstOrDefault()).ToList();//去重
            //        var sendUserList3 = sendUserList.GroupBy(d => new { d.type }).Select(d => d.FirstOrDefault()).ToList();//去重
            //        if (sendUserList2.Count > 0)
            //        {
            //            for (int s = 0; s < sendUserList2.Count; s++)
            //            {
            //                var user = list_BLL.userList_U((int)sendUserList2[0].userID);
            //                if (s == 0)
            //                {
            //                    sendUserName = user[0].userName;
            //                }
            //                else
            //                {
            //                    sendUserName = sendUserName + "、" + user[0].userName;
            //                }
            //            }
            //        }
            //        //查询通知方式
            //        string sendType = "";
            //        if (sendUserList3.Count > 0)
            //        {
            //            for (int ss = 0; ss < sendUserList3.Count; ss++)
            //            {
            //                if (ss == 0)
            //                {
            //                    sendType = sendUserList3[ss].type;
            //                }
            //                else
            //                {
            //                    sendType = sendType + "、" + sendUserList3[ss].type;
            //                }
            //            }
            //        }

            //        this.LogView1.Rows[i].Cells[0].Value = list[i].MID;
            //        this.LogView1.Rows[i].Cells[1].Value = menuName;
            //        this.LogView1.Rows[i].Cells[2].Value = list[i].proName;
            //        this.LogView1.Rows[i].Cells[3].Value = list[i].DValue;
            //        this.LogView1.Rows[i].Cells[4].Value = list[i].yujingValue;
            //        this.LogView1.Rows[i].Cells[5].Value = list[i].lifeValue;
            //        this.LogView1.Rows[i].Cells[6].Value = list[i].unit;
            //        this.LogView1.Rows[i].Cells[7].Value = list[i].createTime;
            //        this.LogView1.Rows[i].Cells[8].Value = list[i].typeCL;
            //        this.LogView1.Rows[i].Cells[9].Value = list[i].userName;
            //        this.LogView1.Rows[i].Cells[10].Value = list[i].createTimeCL;
            //        this.LogView1.Rows[i].Cells[11].Value = list[i].mark;
            //    }
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int seach_menuID = 0;
            //判断是否是数字
            try
            {
                seach_menuID = int.Parse(com_menuID.Text);
            }
            catch (Exception)
            {
                seach_menuID = 0;
            }
            readList(6, 0, seach_menuID);
        }
    }
}
