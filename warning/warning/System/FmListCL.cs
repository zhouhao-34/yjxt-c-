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
    public partial class FmListCL : Form
    {
        int proID = 0;
        int MID = 0;
        List_BLL list_BLL = new List_BLL();
        Menu_BLL menu_BLL = new Menu_BLL();
        public FmListCL(int _MID,int _proID)
        {
            this.MID = _MID;
            this.proID = _proID;
            InitializeComponent();
        }
        private void FmListCL_Load(object sender, EventArgs e)
        {
            readFrist();
            //添加失去焦点委托
            tex_proName.LostFocus += new EventHandler(textBox2_LostFocus);
        }
        //初始化数据
        public void readFrist()
        {
            var proList = list_BLL.proListFrist(proID);
            tex_lifeValue.Text = proList[0].lifeValue.ToString();
            tex_proID.Text = proID.ToString();
            tex_unit.Text = proList[0].unit;
            tex_yujingValue.Text = proList[0].yujingValue.ToString();
            //查询维护者
            List<YJ_User> userList = new User_BLL().userList();
            this.ProcessPanel.Controls.Clear();// 移除 panel1内的所有控件
            CheckBox[] processCB = new CheckBox[userList.Count];

            for (int i = 0; i < userList.Count; i++)
            {
                processCB[i] = new CheckBox();
                processCB[i].Text = userList[i].userName.ToString();
                processCB[i].Tag = userList[i].userID.ToString();
                processCB[i].AutoSize = true;
                //processCB.Font = new Font("微软雅黑", 13F);
                //processCB[i].Margin = new Padding(20, 2, 0, 0);
                ProcessPanel.Controls.Add(processCB[i]);
            }
        }
        //动态添加多选
        private void textBox2_LostFocus(object sender, EventArgs e)
        {
            string modelName = tex_proName.Text;
            int yujingVal = 0;
            if (tex_lifeValue.Text != "")
            {
                yujingVal = Convert.ToInt32(tex_lifeValue.Text);
            }
            //读取3天的数据
            int junZhi = 0;
            if (modelName != "")
            {
                var prolist = new easyYJEntities().YJ_Product.Where(p => p.proID == proID).FirstOrDefault();
                var plcLog = list_BLL.listThree(prolist.proID);
                if (plcLog.Count > 0)
                {
                    int val = 0;
                    foreach (var item in plcLog)
                    {
                        val = val + Convert.ToInt32(item.xiaohao);
                    }
                    junZhi = val / 3;
                    //计算采购周期内需要消耗
                    var product = new easyYJEntities().YJ_Product.Where(o => o.proID == plcLog[0].proID).FirstOrDefault();
                    int shoppingVal = Convert.ToInt32(product.shopTime);
                    int shopYJ = shoppingVal * junZhi;//计算采购期间需要消耗数据
                    int YJval = yujingVal - shopYJ;//推荐预警值=设定寿命-采购消耗
                    if (YJval > 0 || YJval == yujingVal)
                    {
                        label8.Text = YJval.ToString();
                    }
                }
            }
        }
        //提交维护记录
        
        private void button1_Click(object sender, EventArgs e)
        {
            System.DateTime currentTime = new System.DateTime();
            currentTime = System.DateTime.Now;
            int dval = 0;
            ArrayList weihuzhe = new ArrayList();
            var product = new easyYJEntities().YJ_Product.Where(o => o.proID == proID).FirstOrDefault();
            if (product.unit == "自然日")
            {
                DateTime createTime = Convert.ToDateTime(product.createTime);
                TimeSpan span = currentTime.Subtract(createTime);
                dval = span.Days + 1;
            }
            else
            {
                dval = (int)product.DValue;
            }
            if (tex_lifeValue.Text=="" || tex_unit.Text=="" || tex_yujingValue.Text=="" || tex_typeCL.Text == "")
            {
                MessageBox.Show("信息填写不完整");
                return;
            }
            int Life = Convert.ToInt32(tex_lifeValue.Text);
            string Unit = tex_unit.Text;
            int Yjvalue = Convert.ToInt32(tex_yujingValue.Text);
            string TypeCL = tex_typeCL.Text.ToString();
            string Mark = tex_mark.Text;
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
            Object[] obj = new Object[7] { proID, Life, Unit, Yjvalue, TypeCL, Mark,MID };
            if (weihuzhe.Count == 0)
            {
                MessageBox.Show("请选择维保操作人");
                return;
            }
            if (TypeCL == "已维保设备")
            {
                if(Life< product.lifeValue || Life < dval)
                {
                    MessageBox.Show("维保后的设备寿命设定必须大于原设定寿和大于当前已使用寿命");
                    return;
                }
                if (Yjvalue < product.yujingValue)
                {
                    MessageBox.Show("维保后的设备预警值必须大于原始预警值");
                    return;
                }                

            }
            //保存维保数据
            bool reslut = list_BLL.listWeibaoAdd(obj, weihuzhe);
            if (reslut)
            {
                MessageBox.Show("保存成功");
            }
            else
            {
                MessageBox.Show("保存失败");
            }
        }

        private void tex_typeCL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(tex_typeCL.Text== "已更换设备")
            {
                FmListCL_proAdd proAdd = new FmListCL_proAdd(MID,proID);
                this.Close();
                proAdd.ShowDialog();
            }
        }
    }
}
