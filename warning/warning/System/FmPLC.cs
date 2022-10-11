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
    public partial class FmPLC : Form
    {
        User_BLL user_BLL = new User_BLL();
        public FmPLC()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string className = tex_className.Text;
            string PLC_name = tex_PLC_name.Text;
            string PLC_brand = com_PLC_brand.Text;
            string PLC_model = com_PLC_model.Text;
            string PLC_ip = tex_PLC_ip.Text;
            string PLC_port = tex_PLC_port.Text;
            if(className=="" || PLC_name=="" || PLC_brand=="" || PLC_model==""|| PLC_ip==""|| PLC_port == "")
            {
                MessageBox.Show("信息填写不完整");
            }
            bool reslut = user_BLL.plcAdd(className, PLC_name, PLC_brand, PLC_model, PLC_ip, PLC_port);
            if (reslut)
            {
                MessageBox.Show("添加成功");
            }
            else
            {
                MessageBox.Show("添加失败");
            }
        }
    }
}
