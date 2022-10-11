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
    public partial class Fmalarm : Form
    {
        Alarm_BLL alarm_Bll = new Alarm_BLL();
        Menu_BLL menu_BLL = new Menu_BLL();
        int proID = 0;
        int type = 0;//type=1是报警，type=2是预警提醒
        public Fmalarm(int _proID,int _type)
        {
            this.proID = _proID;
            this.type = _type;
            InitializeComponent();
        }

        private void Fmalarm_Load(object sender, EventArgs e)
        {
            //根据ID查询当前报警/预警产品信息
            var list = alarm_Bll.alarmTask(proID);            

            //获取设备所属当前及所有父级菜单数组
            int menuID = Convert.ToInt32(list[0].menuID.ToString());
            string[] menuNameArr = menu_BLL.GetSysMenu(menuID);
            string menuName = null;
            for(int i = 0; i < menuNameArr.Length; i++)
            {
                menuName = menuName + "_" + menuNameArr[i];
            }
            //拼接输出数据
            string sInput = "";
            if (type == 1)
            {
                sInput = "这是报警信息：";
                sInput = sInput + menuName;
                //计算设定寿命与当前值差
                int chaVal = (int)(list[0].DValue - list[0].lifeValue);
                sInput = sInput + list[0].proName + "-已超过设定寿命"+ chaVal+list[0].unit;
            }
            else
            {
                sInput = "这是预警信息：";
                sInput = sInput + menuName;
                sInput = sInput + list[0].proName + "-已超过预警值" + list[0].DValue + list[0].unit;
            }
            
            
            textBox1.AppendText(sInput);
        }
    }
}
