
namespace warning
{
    partial class FmMenuEdit
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.tex_menuID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tex_parentID = new System.Windows.Forms.TextBox();
            this.tex_menuName = new System.Windows.Forms.TextBox();
            this.botton1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(94, 104);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "菜单ID：";
            // 
            // tex_menuID
            // 
            this.tex_menuID.Location = new System.Drawing.Point(154, 98);
            this.tex_menuID.Name = "tex_menuID";
            this.tex_menuID.Size = new System.Drawing.Size(149, 21);
            this.tex_menuID.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(82, 177);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "菜单名称：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(82, 142);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "所属上级：";
            // 
            // tex_parentID
            // 
            this.tex_parentID.Location = new System.Drawing.Point(154, 137);
            this.tex_parentID.Name = "tex_parentID";
            this.tex_parentID.Size = new System.Drawing.Size(149, 21);
            this.tex_parentID.TabIndex = 3;
            // 
            // tex_menuName
            // 
            this.tex_menuName.Location = new System.Drawing.Point(153, 173);
            this.tex_menuName.Name = "tex_menuName";
            this.tex_menuName.Size = new System.Drawing.Size(149, 21);
            this.tex_menuName.TabIndex = 3;
            // 
            // botton1
            // 
            this.botton1.Location = new System.Drawing.Point(227, 225);
            this.botton1.Name = "botton1";
            this.botton1.Size = new System.Drawing.Size(75, 29);
            this.botton1.TabIndex = 4;
            this.botton1.Text = "提交";
            this.botton1.UseVisualStyleBackColor = true;
            this.botton1.Click += new System.EventHandler(this.botton1_Click);
            // 
            // FmMenuEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 344);
            this.Controls.Add(this.botton1);
            this.Controls.Add(this.tex_menuName);
            this.Controls.Add(this.tex_parentID);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tex_menuID);
            this.Controls.Add(this.label1);
            this.Name = "FmMenuEdit";
            this.Text = "FmMenuEdit";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tex_menuID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tex_parentID;
        private System.Windows.Forms.TextBox tex_menuName;
        private System.Windows.Forms.Button botton1;
    }
}