using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using Entity;

namespace warning
{
    public partial class FmListAdd : Form
    {
        int menuID = 0;
        List_BLL list_BLL = new List_BLL();
        Menu_BLL menu_BLL = new Menu_BLL();
        
        public FmListAdd(int _menuID)
        {

            this.menuID = _menuID;
            InitializeComponent();

        }
        private void FmListAdd_Load(object sender, EventArgs e)
        {
            readFrist();
            //添加失去焦点委托
            tex_proName.LostFocus += new EventHandler(textBox2_LostFocus);
        }
        //初始化数据
        public void readFrist()
        {
            var menuOnly = menu_BLL.dlist(menuID);
            menuOnly = menuOnly.OrderBy(a => a.menuID).ToList();
            tex_menu.DataSource = menuOnly;
            tex_menu.DisplayMember = "menuName";//显示名
            tex_menu.ValueMember = "menuID";//对应值SelectedValue.ToString()取出
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
            int menuID = this.menuID;
            int yujingVal = 0;
            if (tex_life.Text != "")
            {
                yujingVal = Convert.ToInt32(tex_life.Text);
            }
            //读取3天的数据
            int junZhi = 0;
            if (modelName != "")
            {
                var prolist = new easyYJEntities().YJ_Product.Where(p => p.model == modelName && p.menuID== menuID).FirstOrDefault();
                if (prolist == null)
                {
                    label8.Text = junZhi.ToString();
                    return;
                }
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            imagUplode();
        }
        private void imagUplode()
        {
            System.DateTime currentTime = new System.DateTime();
            currentTime = System.DateTime.Now;
            //选择照片
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;//该值确定是否可以选择多个文件
            dialog.Title = "请选择文件夹"; //窗体标题
            dialog.Filter = "图片文件(*.jpg,*.png)|*.jpg;*.png"; //文件筛选
                                                             //默认路径设置为我的电脑文件夹

            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string file = dialog.FileName;//文件夹路径
                string path = file.Substring(file.LastIndexOf("\\") + 1); //格式化处理，提取文件名
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;  //使图像拉伸或收缩，以适合PictureBox
                this.pictureBox1.ImageLocation = file;

                //不过这样会有重复,可以给Random一个系统时间做为参数，以此产生随机数，就不会重复了
                System.Random a = new Random();
                string RandKey = DateTime.Now.ToString("yyyyMMdd") + a.Next(0, 9999);
                //判断文件夹是否存
                DirectoryInfo TheFolder = new DirectoryInfo(Application.StartupPath + "\\Image\\");
                if (!TheFolder.Exists)
                {
                    Directory.CreateDirectory(Application.StartupPath + "\\Image\\"); //新建文件夹  
                }
                File.Copy(dialog.FileName, Application.StartupPath + "\\Image\\" + RandKey + ".jpg");
                this.imageBox.Text = "\\Image\\" + RandKey + ".jpg";
                //    Bitmap bmp = new Bitmap(image);
                //    MemoryStream ms = new MemoryStream();
                //    bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                //    byte[] arr = new byte[ms.Length];
                //    ms.Position = 0;
                //    ms.Read(arr, 0, (int)ms.Length);
                //    ms.Close();
                //    string base64string = Convert.ToBase64String(arr);
                //    MessageBox.Show(base64string);
            }
        }
        //保存数据
        private void button2_Click(object sender, EventArgs e)
        {
            ArrayList weihuzhe = new ArrayList();
            string Tex_menu = tex_menu.SelectedValue.ToString();
            string Tex_barnd = tex_barnd.Text;
            string Tex_model = tex_model.Text;
            string Tex_proName = tex_proName.Text;
            string Tex_life = tex_life.Text;
            string Tex_unit = tex_unit.SelectedItem.ToString();
            string Tex_shopDay = tex_shopDay.Text;
            string Tex_shopUnit = tex_shopUnit.SelectedItem.ToString();
            string Tex_yujingVal = tex_yujingVal.Text;
            string ImageBox = imageBox.Text;
            string plcListID = tex_plcListID.Text;

            if (Tex_barnd == ""|| Tex_model==""|| Tex_proName==""|| Tex_life=="" || Tex_unit==""|| Tex_yujingVal=="" )
            {
                MessageBox.Show("信息填写不完整或没有设置PLC");
                return;
            }
            Object[] obj = new Object[11]{Tex_menu, Tex_barnd, Tex_model, Tex_proName, Tex_life, Tex_unit, Tex_shopDay, Tex_shopUnit, Tex_yujingVal, ImageBox, plcListID };
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
            bool reslut = list_BLL.listAdd(obj, weihuzhe);
            if (reslut)
            {
                MessageBox.Show("添加成功");
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
