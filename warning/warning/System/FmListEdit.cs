using System;
using System.Collections;
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
    public partial class FmListEdit : Form
    {
        List_BLL list_BLL = new List_BLL();
        int proID = 0;
        public FmListEdit(int _proID)
        {
            this.proID = _proID;
            InitializeComponent();
            shuju();
        }
        private void shuju()
        {
            ArrayList weihuzhe = new ArrayList();
            var list = list_BLL.proEdit_list(proID);
            if (list.Count == 0)
            {
                MessageBox.Show("没有查到数据");
                this.Close();
                return;
            }
            tex_proID.Text = list[0].proID.ToString();
            tex_menu.Text = list[0].menuID.ToString();
            tex_barnd.Text = list[0].brand.ToString();
            tex_model.Text = list[0].model.ToString();
            tex_proName.Text = list[0].proName.ToString();
            tex_shopDay.Text = list[0].shopTime.ToString();
            tex_shopUnit.Text = list[0].shopTimeType.ToString();
            tex_plcListID.Text = list[0].plcListID.ToString();
            imageBox.Text = list[0].imgPath.ToString();

            //查询维护人
            var ManageUser = new easyYJEntities().YJ_ManageUser.Where(m => m.status == 1 && m.proID == proID).ToList();
            List<YJ_User> userList = new User_BLL().userList();
            this.ProcessPanel.Controls.Clear();// 移除 panel1内的所有控件
            CheckBox[] processCB = new CheckBox[userList.Count];

            for (int i = 0; i < userList.Count; i++)
            {
                processCB[i] = new CheckBox();
                processCB[i].Text = userList[i].userName.ToString();
                processCB[i].Tag = userList[i].userID.ToString();
                processCB[i].AutoSize = true;
                foreach(var mmm in ManageUser)
                {
                    if (userList[i].userID == mmm.userID)
                    {
                        processCB[i].Checked = true;
                    }
                }
                ProcessPanel.Controls.Add(processCB[i]);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ArrayList weihuzhe = new ArrayList();
            string Tex_menu = tex_menu.Text;
            string Tex_barnd = tex_barnd.Text;
            string Tex_model = tex_model.Text;
            string Tex_proName = tex_proName.Text;
            string Tex_shopDay = tex_shopDay.Text;
            string Tex_shopUnit = tex_shopUnit.SelectedItem.ToString();
            string ImageBox = imageBox.Text;
            string plcListID = tex_plcListID.Text;
            string proID = tex_proID.Text;

            if (Tex_barnd == "" || Tex_model == "" || Tex_proName == ""  || plcListID == "")
            {
                MessageBox.Show("信息填写不完整或没有设置PLC");
                return;
            }
            Object[] obj = new Object[9] { Tex_menu, Tex_barnd, Tex_model, Tex_proName, Tex_shopDay, Tex_shopUnit, ImageBox,proID, plcListID };
            foreach (Control ctl in this.ProcessPanel.Controls)
            {
                if (ctl is CheckBox)
                {
                    CheckBox cb = ctl as CheckBox;
                    string temp = string.Empty;
                    if (cb.Checked == true)
                    {
                        weihuzhe.Add(cb.Tag);
                    }
                }
            }
            bool reslut = list_BLL.proEdit(obj, weihuzhe);
            if (reslut)
            {
                MessageBox.Show("修改成功");
                this.Close();
            }
            else
            {
                MessageBox.Show("添加失败");
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            createPLC_list plcADD = new createPLC_list();
            plcADD.plcListIDEvent += plcADD_TransfEvent;
            plcADD.ShowDialog();
        }
        //事件处理方法
        void plcADD_TransfEvent(int value)
        {
            tex_plcListID.Text = value.ToString();
            //查询刚刚添加的plcList信息
            var list = new PLC_BLL().PLC_listList(value);
            label10.Text = "读取：" + list[0].PLC_ip + "[" + list[0].PLC_adress + "]的数据后" + list[0].chuli;
        }
    }
}
