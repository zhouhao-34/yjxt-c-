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
using DAL;
using Entity;

namespace warning
{
    public partial class FmList : Form
    {
        List_BLL list_BLL = new List_BLL();
        Menu_BLL menu_BLL = new Menu_BLL();
        public FmList()
        {            
            InitializeComponent();
            BindTreeView();
        }
        private void FmList_Load(object sender, EventArgs e)
        {
            //加载右边全部设备数据
            rightListAll();
        }
        //加载右边全部设备数据
        private void rightListAll()
        {
            var list = menu_BLL.GetSysMenuALL(0,10);
            this.dataGridView1.Rows.Clear();
            if (list.Length > 0)
            {
                //for (int i = 0; i < list.Length; i++)
                //{
                //    this.dataGridView1.Rows.Add();
                //    int meitian = threeSUM(list[i].model);
                //    //计算剩余寿命
                //    Double shengyu = 0;
                //    if (meitian > 0)
                //    {
                //        shengyu = Math.Round((double)(list[i].lifeValue - list[i].DValue) / (double)meitian, 2);
                //    }
                    
                //    string mName = menu_BLL.TreeChilds(Convert.ToInt32(list[i].menuID));
                //    this.dataGridView1.Rows[i].Cells[0].Value = list[i].proID;
                //    this.dataGridView1.Rows[i].Cells[1].Value = mName;
                //    this.dataGridView1.Rows[i].Cells[2].Value = list[i].proName;
                //    this.dataGridView1.Rows[i].Cells[3].Value = list[i].brand;
                //    this.dataGridView1.Rows[i].Cells[4].Value = list[i].model;
                //    this.dataGridView1.Rows[i].Cells[5].Value = list[i].lifeValue + list[i].unit;
                //    this.dataGridView1.Rows[i].Cells[6].Value = list[i].DValue;
                //    this.dataGridView1.Rows[i].Cells[7].Value = list[i].yujingValue;
                //    this.dataGridView1.Rows[i].Cells[8].Value = meitian;
                //    if (shengyu == 0)
                //    {
                //        this.dataGridView1.Rows[i].Cells[9].Value = "-";
                //    }
                //    else
                //    {
                //        this.dataGridView1.Rows[i].Cells[9].Value = shengyu;
                //    }
                //    this.dataGridView1.Rows[i].Cells[10].Value = list[i].shopTime + list[i].shopTimeType;
                //    this.dataGridView1.Rows[i].Cells[11].Value = list[i].createTime;
                //}
            }
        }
        
        /// <summary>
        /// 加载左菜单
        /// </summary>
        private void BindTreeView()
        {
            try
            {
                this.treeView1.Nodes.Clear();
                this.treeView1.ImageList = imageList1;
                var list = menu_BLL.GetSysMenus();
                var parents = list.Where(o => o.parentID == 0);
                foreach (var item in parents)
                {
                    TreeNode tn = new TreeNode();
                    tn.Text = item.menuName;
                    tn.Tag = item.menuID;
                    tn.ImageIndex = 0;
                    FillTree(tn, list);
                    treeView1.Nodes.Add(tn);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void FillTree(TreeNode node, List<YJ_Menu> list)
        {

            var childs = list.Where(o => o.parentID == Convert.ToInt32(node.Tag.ToString()));
            if (childs.Count() > 0)
            {
                foreach (var item in childs)
                {
                    TreeNode tnn = new TreeNode();
                    tnn.Text = item.menuName;
                    tnn.Tag = item.menuID;
                    tnn.ImageIndex = 0;
                    if (item.parentID == Convert.ToInt32(node.Tag.ToString()))
                    {
                        FillTree(tnn, list);
                    }
                    node.Nodes.Add(tnn);
                }

            }
        }
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //判断是否有子级
            if (this.treeView1.SelectedNode.Nodes.Count > 0)
            {
                //有子级展开
                treeView1.Nodes[0].Expand();
            }
            else
            {
                //没有子级加载右边数据
                var list = menu_BLL.GetSysMenuChilds(Convert.ToInt32(treeView1.SelectedNode.Tag.ToString()), 0, 10);
                this.dataGridView1.Rows.Clear();
                //if (list.Count > 0)
                //{
                //    for (int i = 0; i < list.Count; i++)
                //    {
                //        this.dataGridView1.Rows.Add();
                //        int meitian = threeSUM(list[i].model);
                //        //计算剩余寿命
                //        Double shengyu = 0;
                //        if (meitian > 0)
                //        {
                //            shengyu = Math.Round((double)(list[i].lifeValue - list[i].DValue) / (double)meitian, 2);
                //        }

                //        string mName = menu_BLL.TreeChilds(Convert.ToInt32(list[i].menuID));
                //        this.dataGridView1.Rows[i].Cells[0].Value = list[i].proID;
                //        this.dataGridView1.Rows[i].Cells[1].Value = mName;
                //        this.dataGridView1.Rows[i].Cells[2].Value = list[i].proName;
                //        this.dataGridView1.Rows[i].Cells[3].Value = list[i].brand;
                //        this.dataGridView1.Rows[i].Cells[4].Value = list[i].model;
                //        this.dataGridView1.Rows[i].Cells[5].Value = list[i].lifeValue + list[i].unit;
                //        this.dataGridView1.Rows[i].Cells[6].Value = list[i].DValue;
                //        this.dataGridView1.Rows[i].Cells[7].Value = list[i].yujingValue;
                //        this.dataGridView1.Rows[i].Cells[8].Value = meitian;
                //        if (shengyu == 0)
                //        {
                //            this.dataGridView1.Rows[i].Cells[9].Value = "-";
                //        }
                //        else
                //        {
                //            this.dataGridView1.Rows[i].Cells[9].Value = shengyu;
                //        }
                //        this.dataGridView1.Rows[i].Cells[10].Value = list[i].shopTime + list[i].shopTimeType;
                //        this.dataGridView1.Rows[i].Cells[11].Value = list[i].createTime;
                //    }
                //}


                //this.dataGridView1.Rows.Clear();
                //for (int i = 0; i < list.Count; i++)
                //{
                //    int meitian = threeSUM(list[i].model);
                //    //计算剩余寿命
                //    Double shengyu = 0;
                //    if (meitian >0)
                //    {
                //        shengyu = Math.Round((double)(list[i].lifeValue - list[i].DValue) / (double)meitian,2);
                //    }

                //    this.dataGridView1.Rows[i].Cells[0].Value = list[i].proID;
                //    this.dataGridView1.Rows[i].Cells[1].Value = list[i].menuName;
                //    this.dataGridView1.Rows[i].Cells[2].Value = list[i].proName;
                //    this.dataGridView1.Rows[i].Cells[3].Value = list[i].brand;
                //    this.dataGridView1.Rows[i].Cells[4].Value = list[i].model;
                //    this.dataGridView1.Rows[i].Cells[5].Value = list[i].lifeValue+list[i].unit;
                //    this.dataGridView1.Rows[i].Cells[6].Value = list[i].DValue;
                //    this.dataGridView1.Rows[i].Cells[7].Value = list[i].yujingValue;
                //    this.dataGridView1.Rows[i].Cells[8].Value = meitian;
                //    if (shengyu == 0)
                //    {
                //        this.dataGridView1.Rows[i].Cells[9].Value = "-";
                //    }
                //    else
                //    {
                //        this.dataGridView1.Rows[i].Cells[9].Value = shengyu;
                //    }                    
                //    this.dataGridView1.Rows[i].Cells[10].Value = list[i].shopTime+ list[i].shopTimeType;
                //    this.dataGridView1.Rows[i].Cells[11].Value = list[i].createTime;
                //}

                //this.dataGridView1.DataSource = menu_BLL.GetSysMenuChilds(Convert.ToInt32(treeView1.SelectedNode.Tag.ToString()));
                //this.dataGridView1.DataSource = user_bll.GetUserInfoByOrganization_Id(SqlWhere, IList_param);
            }
        }
        //计算每天使用量，取三天的数据求平均
        //public int threeSUM(string model)
        //{
        //    var plcLog = list_BLL.listThree(model);
        //    //读取3天的数据
        //    int junZhi = 0;
        //    if (plcLog.Count > 0)
        //    {
        //        int val = 0;
        //        foreach (var item in plcLog)
        //        {
        //            val = val + Convert.ToInt32(item.plcVal);
        //        }
        //        junZhi = val / 3;
                
        //    }
        //    return junZhi;
        //}
    
        /// <summary>
        /// 右键事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)//判断你点的是不是右键
            {
                Point ClickPoint = new Point(e.X, e.Y);
                TreeNode CurrentNode = treeView1.GetNodeAt(ClickPoint);
                if (CurrentNode != null)//判断你点的是不是一个节点
                {
                    switch (treeView1.SelectedNode.Tag.ToString())//根据不同节点显示不同的右键菜单，当然你可以让它显示一样的菜单
                    {
                        case "1":
                            CurrentNode.ContextMenuStrip = contextMenuStrip1;
                        break;
                    }
                    treeView1.SelectedNode = CurrentNode;//选中这个节点
                }
            }
        }
        /// <summary>
        /// 右键添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 添加ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int parentID = Convert.ToInt32(treeView1.SelectedNode.Tag.ToString());
            FmMenuAdd fmMenuAdd = new FmMenuAdd(parentID);
            fmMenuAdd.ShowDialog();
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            int parentID = Convert.ToInt32(treeView1.SelectedNode.Tag.ToString());
            FmListAdd fmadd = new FmListAdd(parentID);
            fmadd.ShowDialog();
        }


        private void toolStripButton6_Click_1(object sender, EventArgs e)
        {
            FmListImg fmlistImg = new FmListImg();
            fmlistImg.ShowDialog();
        }

        private void chuLi_Click(object sender, EventArgs e)
        {
            var dataselect= dataGridView1.SelectedRows;
            if (dataselect.Count == 0)
            {
                MessageBox.Show("请选择要编辑的行!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                int proID = Convert.ToInt32(dataselect[0].Cells["proID"].Value);
                //FmListCL fmListCL = new FmListCL(proID);
                //fmListCL.ShowDialog();
            }
        }

        private void proDel_Click(object sender, EventArgs e)
        {
            var dataselect = dataGridView1.SelectedRows;
            if (dataselect.Count == 0)
            {
                MessageBox.Show("请选择要编辑的行!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int proID = Convert.ToInt32(dataselect[0].Cells["proID"].Value);
            var reslut = list_BLL.proDel(proID);
            if (reslut = true)
            {
                MessageBox.Show("删除成功");
                return;
            }
            else
            {
                MessageBox.Show("删除失败");
            }
        }

        private void proEdit_Click(object sender, EventArgs e)
        {
            var dataselect = dataGridView1.SelectedRows;
            if (dataselect.Count == 0)
            {
                MessageBox.Show("请选择要编辑的行!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                int proID = Convert.ToInt32(dataselect[0].Cells["proID"].Value);
                FmListEdit fmListEdit = new FmListEdit(proID);
                fmListEdit.ShowDialog();
            }
        }

        private void menuEdit_Click(object sender, EventArgs e)
        {
            int menuID = Convert.ToInt32(treeView1.SelectedNode.Tag.ToString());
            FmMenuEdit fmMenuEdit = new FmMenuEdit(menuID);
            fmMenuEdit.ShowDialog();
        }

        private void menuDel_Click(object sender, EventArgs e)
        {
            int menuID = Convert.ToInt32(treeView1.SelectedNode.Tag.ToString());
            string res = "";
            if (menuID > 0)
            {
                res = menu_BLL.menuDel(menuID);
            }
            if (res == "成功")
            {
                MessageBox.Show("删除成功");
            }
            MessageBox.Show("操作失败");
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            FmUser fmUser = new FmUser();
            fmUser.ShowDialog();
            this.Close();
        }
    }
}
