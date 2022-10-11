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
    public partial class FmUser : Form
    {
        User_BLL user_BLL = new User_BLL();
        public FmUser()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string userName = username.Text;
            string Mobile = mobile.Text;
            string Email = email.Text;
            if (userName == "" || Mobile == "" || Email == "")
            {
                MessageBox.Show("信息填写不完整");
            }
            //bool reslut = user_BLL.userAdd(userName, Mobile, Email);
            //if (reslut)
            //{
            //    MessageBox.Show("添加成功");
            //}
            //else
            //{
            //    this.Close();
            //    MessageBox.Show("数据已存在或添加失败");
                
            //}
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FmPLC fmPLC = new FmPLC();
            fmPLC.ShowDialog();
            this.Close();
        }
    }
}
