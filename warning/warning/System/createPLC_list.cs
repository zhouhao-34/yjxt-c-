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
using Entity;

namespace warning
{
    //申明委托传值PLClistID
    public delegate void plcListID(int plcListID);
    public partial class createPLC_list : Form
    {
        User_BLL user_BLL = new User_BLL();
        PLC_BLL PLC_Bll = new PLC_BLL();
        
        public event plcListID plcListIDEvent;
        public createPLC_list()
        {
            InitializeComponent();
            this.label12.Visible = false;
            label14.Visible = false;
            label15.Visible = false;
            label5.Visible = false;

            PLC_addressType_w.Visible = false;
            where_PLC_address.Visible = false;
            where_tiaojian.Visible = false;
            where_content.Visible = false;
            var dingji = user_BLL.plcList();
            PLCID.DataSource = dingji;
            PLCID.DisplayMember = "PLC_name";//显示名
            PLCID.ValueMember = "PLCID";//对应值SelectedValue.ToString()取出
           
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //string tableNameCNS = tableNameCN.Text == "请选择数据接收表" ? "" : tableNameCN.Text;
            if (chufa.Text == "" || PLCID.Text == "" || PLC_adress.Text.ToString() == "" || PLC_addressType.Text == "")
            {
                MessageBox.Show("参数不全！");
            }
            else
            {
                string[] dataList = new string[12];
                //判断触发信号
                string where_PLC_address1 = null;
                string where_tiaojian1 = null;
                string where_content1 = null;
                string chufa1 = "0";
                string where_PLC_address_w = null;
                //1数据不为空2与上一次不同3指定信号4跟随创建数据时读取
                if (chufa.Text == "指定信号")
                {
                    if (where_PLC_address.Text == "" || where_tiaojian.Text == "" || where_content.Text == "" || PLC_addressType_w.Text =="")
                    {
                        MessageBox.Show("指定信号条件参数设置不全！");
                    }
                    else
                    {
                        where_PLC_address1 = where_PLC_address.Text;
                        where_tiaojian1 = where_tiaojian.Text;
                        where_content1 = where_content.Text;
                        where_PLC_address_w = PLC_addressType_w.Text;
                    }
                    chufa1 = "3";
                }                
                else if (chufa.Text == "数据不为空")
                {
                    chufa1 = "1";
                }
                else if (chufa.Text == "与上一次不同")
                {
                    chufa1 = "2";
                }
                else if (chufa.Text == "等于true")
                {
                    chufa1 = "4";
                }
                else if (chufa.Text == "等于false")
                {
                    chufa1 = "5";
                }

                dataList[0] = PLCID.SelectedValue.ToString();
                dataList[1] = PLC_adress.Text;
                dataList[2] = PLC_addressType.Text;
                dataList[3] = addressLength.Text;
                dataList[4] = chufa1;
                dataList[5] = where_PLC_address1;
                dataList[6] = where_tiaojian1;
                dataList[7] = where_content1;
                dataList[8] = returnVal.Text;
                dataList[9] = com_chuli.Text;
                dataList[10] = where_PLC_address_w;
                dataList[11] = where_returnVal.Text;
                //分组写入数据库,返回plcListID
                int plcListID = PLC_Bll.PLC_list_add(dataList);
                if(plcListID > 0)
                {
                    MessageBox.Show("添加成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    plcListIDEvent(plcListID);
                }
                else
                {
                    MessageBox.Show("添加失败！");
                }
            }
        }

        private void chufa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chufa.Text == "指定信号")
            {
                label12.Visible = true;
                label14.Visible = true;
                label15.Visible = true;
                label5.Visible = true;
                where_PLC_address.Visible = true;
                where_tiaojian.Visible = true;
                where_content.Visible = true;
                PLC_addressType_w.Visible = true;
            }
            else
            {
                label12.Visible = false;
                label14.Visible = false;
                label15.Visible = false;
                label5.Visible = false;
                where_PLC_address.Visible = false;
                where_tiaojian.Visible = false;
                where_content.Visible = false;
                PLC_addressType_w.Visible = false;
            }
        }
    }
}
