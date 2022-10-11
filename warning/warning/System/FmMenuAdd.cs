using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entity;
using BLL;

namespace warning
{
    public partial class FmMenuAdd : Form
    {
        Menu_BLL menu_BLL = new Menu_BLL();
        int parentID = 0;
        public FmMenuAdd(int _parentID)
        {
            this.parentID = _parentID;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            easyYJEntities db = new easyYJEntities();
            string menuName = MenuName.Text;
            if (menuName == "")
            {
                MessageBox.Show("请输入菜单名称");
            }
            var menu1 = menu_BLL.menuOne(menuName, parentID);
            if (menu1.Count > 0)
            {
                MessageBox.Show("当前分类下菜单名称已存在");
                return;
            }
            //添加数据
            //if(menu_BLL.menuADD(menuName, parentID))
            //{
            //    MessageBox.Show("添加成功");
            //    this.Close();
            //}
            //else
            //{
            //    MessageBox.Show("添加失败");
            //}
        }
    }
}
