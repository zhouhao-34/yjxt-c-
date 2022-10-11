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
    public partial class FmMenuEdit : Form
    {
        Menu_BLL menu_BLL = new Menu_BLL();
        int menuID = 0;
        public FmMenuEdit(int _menuID)
        {
            this.menuID = _menuID;
            InitializeComponent();
            list();
        }
        private void list()
        {
            var menuList = menu_BLL.dlist(menuID);
            if (menuList.Count == 0)
            {
                MessageBox.Show("没有找到信息");
                return;
            }
            tex_menuID.Text = menuList[0].menuID.ToString();
            tex_parentID.Text = menuList[0].parentID.ToString();
            tex_menuName.Text = menuList[0].menuName;
            
        }

        private void botton1_Click(object sender, EventArgs e)
        {
            int menuID = Convert.ToInt32(tex_menuID.Text);
            int parentID = Convert.ToInt32(tex_parentID.Text);
            string menuName = tex_menuName.Text;
            //bool reslut = menu_BLL.menuEdit(menuID, parentID, menuName);
            //if (reslut)
            //{
            //    MessageBox.Show("修改成功");
            //    this.Close();
            //}
            //else
            //{
            //    MessageBox.Show("修改失败");
            //}
        }
    }
}
