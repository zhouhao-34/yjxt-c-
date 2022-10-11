using System;
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
    public partial class FmListImg : Form
    {
        List_BLL list_BLL = new List_BLL();
        Menu_BLL menu_BLL = new Menu_BLL();
        public FmListImg()
        {
            InitializeComponent();
            BindTreeView();
        }
        /// <summary>
        /// 加载左菜单
        /// </summary>
        private void BindTreeView()
        {
            try
            {
                this.treeView2.Nodes.Clear();
                this.treeView2.ImageList = imageList1;
                var list = menu_BLL.GetSysMenus();
                var parents = list.Where(o => o.parentID == 0);
                foreach (var item in parents)
                {
                    TreeNode tp = new TreeNode();
                    tp.Text = item.menuName;
                    tp.Tag = item.menuID;
                    tp.ImageIndex = 0;
                    FillTree(tp, list);
                    treeView2.Nodes.Add(tp);
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
                    TreeNode tpp = new TreeNode();
                    tpp.Text = item.menuName;
                    tpp.Tag = item.menuID;
                    tpp.ImageIndex = 0;
                    if (item.parentID == Convert.ToInt32(node.Tag.ToString()))
                    {
                        FillTree(tpp, list);
                    }
                    node.Nodes.Add(tpp);
                }

            }
        }
        private void treeView2_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //判断是否有子级
            if (this.treeView2.SelectedNode.Nodes.Count > 0)
            {
                //有子级展开
                treeView2.Nodes[0].Expand();
            }
            else
            {
                
                //没有子级加载右边数据
                var list = menu_BLL.GetSysMenuChilds(Convert.ToInt32(treeView2.SelectedNode.Tag.ToString()),0,10);
                
                //for (int i = 0; i < list.Count; i++)
                //{
                //    int meitian = threeSUM(list[i].model);
                //    //设置默认图片
                //    //string imgPath = Application.StartupPath + @"/Image/1.jpg";
                //    string imgPath = @"../../Resources/Noimg.jpg";                    
                //    //Image img = System.Drawing.Image.FromFile(@"../../Resources/Noimg.jpg");                    
                //    if (!string.IsNullOrEmpty(list[i].imgPath))//判断用户是否上传图片
                //    {
                //        imgPath = list[i].imgPath;
                //    }               
                //    //计算剩余寿命
                //    Double shengyu = 0;
                //    if (meitian > 0)
                //    {
                //        shengyu = Math.Round((double)(list[i].lifeValue - list[i].DValue) / (double)meitian, 2);
                //    }
                //    //图片转成base64
                //    string base64ImgString = "";
                //    try
                //    {
                //        Bitmap bmp = new Bitmap(imgPath);
                //        MemoryStream ms = new MemoryStream();
                //        bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                //        byte[] imgArr = new byte[ms.Length];
                //        ms.Position = 0;
                //        ms.Read(imgArr, 0, (int)ms.Length);
                //        ms.Close();
                //        base64ImgString = Convert.ToBase64String(imgArr);
                //    }
                //    catch
                //    {

                //    } 

                //    this.dataGridView2.Rows[i].Cells[0].Value = list[i].proID;
                //    this.dataGridView2.Rows[i].Cells[1].Value = list[i].menuName;
                //    this.dataGridView2.Rows[i].Cells[2].Value = list[i].proName;
                //    this.dataGridView2.Rows[i].Cells[3].Value = list[i].brand;
                //    this.dataGridView2.Rows[i].Cells[4].Value = list[i].model;
                //    this.dataGridView2.Rows[i].Cells[5].Value = list[i].lifeValue + list[i].unit;
                //    this.dataGridView2.Rows[i].Cells[6].Value = list[i].DValue;
                //    this.dataGridView2.Rows[i].Cells[7].Value = list[i].yujingValue;
                //    this.dataGridView2.Rows[i].Cells[8].Value = meitian;
                //    if (shengyu == 0)
                //    {
                //        this.dataGridView2.Rows[i].Cells[9].Value = "-";
                //    }
                //    else
                //    {
                //        this.dataGridView2.Rows[i].Cells[9].Value = shengyu;
                //    }
                //    this.dataGridView2.Rows[i].Cells[10].Value = list[i].shopTime + list[i].shopTimeType;
                //    this.dataGridView2.Rows[i].Cells[11].Value = list[i].createTime;
                //    //this.dataGridView2.Rows[i].Cells[12].Value = System.Drawing.Image.FromFile(imgPath);

                //}
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

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            FmList fmlist = new FmList();
            this.Close();
            fmlist.ShowDialog();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {

        }
    }
}
