
namespace warning
{
    partial class FmListCL
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
            this.tex_menuName = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tex_typeCL = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tex_lifeValue = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tex_yujingValue = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.ProcessPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.tex_proID = new System.Windows.Forms.TextBox();
            this.tex_proName = new System.Windows.Forms.Label();
            this.tex_unit = new System.Windows.Forms.ComboBox();
            this.tex_tuijian = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tex_mark = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(122, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "所属分类：";
            // 
            // tex_menuName
            // 
            this.tex_menuName.AutoSize = true;
            this.tex_menuName.Location = new System.Drawing.Point(194, 63);
            this.tex_menuName.Name = "tex_menuName";
            this.tex_menuName.Size = new System.Drawing.Size(53, 12);
            this.tex_menuName.TabIndex = 1;
            this.tex_menuName.Text = "分类名称";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(122, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "设备名称：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(122, 133);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "处理方式：";
            // 
            // tex_typeCL
            // 
            this.tex_typeCL.FormattingEnabled = true;
            this.tex_typeCL.Items.AddRange(new object[] {
            "已维保设备",
            "已更换设备"});
            this.tex_typeCL.Location = new System.Drawing.Point(196, 130);
            this.tex_typeCL.Name = "tex_typeCL";
            this.tex_typeCL.Size = new System.Drawing.Size(121, 20);
            this.tex_typeCL.TabIndex = 2;
            this.tex_typeCL.Text = "请选择";
            this.tex_typeCL.SelectedIndexChanged += new System.EventHandler(this.tex_typeCL_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(122, 175);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "设定寿命：";
            // 
            // tex_lifeValue
            // 
            this.tex_lifeValue.Location = new System.Drawing.Point(196, 170);
            this.tex_lifeValue.Name = "tex_lifeValue";
            this.tex_lifeValue.Size = new System.Drawing.Size(123, 21);
            this.tex_lifeValue.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(134, 216);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "预警值：";
            // 
            // tex_yujingValue
            // 
            this.tex_yujingValue.Location = new System.Drawing.Point(196, 210);
            this.tex_yujingValue.Name = "tex_yujingValue";
            this.tex_yujingValue.Size = new System.Drawing.Size(123, 21);
            this.tex_yujingValue.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(110, 259);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "推荐预警值：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(110, 301);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 12);
            this.label8.TabIndex = 0;
            this.label8.Text = "本次维护人：";
            // 
            // ProcessPanel
            // 
            this.ProcessPanel.Location = new System.Drawing.Point(193, 301);
            this.ProcessPanel.Name = "ProcessPanel";
            this.ProcessPanel.Size = new System.Drawing.Size(348, 87);
            this.ProcessPanel.TabIndex = 12;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(455, 516);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(86, 27);
            this.button1.TabIndex = 13;
            this.button1.Text = "提交";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tex_proID
            // 
            this.tex_proID.Location = new System.Drawing.Point(193, 22);
            this.tex_proID.Name = "tex_proID";
            this.tex_proID.Size = new System.Drawing.Size(123, 21);
            this.tex_proID.TabIndex = 3;
            // 
            // tex_proName
            // 
            this.tex_proName.AutoSize = true;
            this.tex_proName.Location = new System.Drawing.Point(196, 98);
            this.tex_proName.Name = "tex_proName";
            this.tex_proName.Size = new System.Drawing.Size(53, 12);
            this.tex_proName.TabIndex = 14;
            this.tex_proName.Text = "设备名称";
            // 
            // tex_unit
            // 
            this.tex_unit.FormattingEnabled = true;
            this.tex_unit.Items.AddRange(new object[] {
            "工作次数",
            "通电小时",
            "工作小时",
            "自然日"});
            this.tex_unit.Location = new System.Drawing.Point(325, 171);
            this.tex_unit.Name = "tex_unit";
            this.tex_unit.Size = new System.Drawing.Size(80, 20);
            this.tex_unit.TabIndex = 15;
            // 
            // tex_tuijian
            // 
            this.tex_tuijian.AutoSize = true;
            this.tex_tuijian.Location = new System.Drawing.Point(198, 259);
            this.tex_tuijian.Name = "tex_tuijian";
            this.tex_tuijian.Size = new System.Drawing.Size(11, 12);
            this.tex_tuijian.TabIndex = 16;
            this.tex_tuijian.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(134, 411);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "备注：";
            // 
            // tex_mark
            // 
            this.tex_mark.Location = new System.Drawing.Point(193, 411);
            this.tex_mark.Multiline = true;
            this.tex_mark.Name = "tex_mark";
            this.tex_mark.Size = new System.Drawing.Size(348, 81);
            this.tex_mark.TabIndex = 17;
            // 
            // FmListCL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 555);
            this.Controls.Add(this.tex_mark);
            this.Controls.Add(this.tex_tuijian);
            this.Controls.Add(this.tex_unit);
            this.Controls.Add(this.tex_proName);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ProcessPanel);
            this.Controls.Add(this.tex_yujingValue);
            this.Controls.Add(this.tex_proID);
            this.Controls.Add(this.tex_lifeValue);
            this.Controls.Add(this.tex_typeCL);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tex_menuName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label1);
            this.Name = "FmListCL";
            this.Text = "FmListCL";
            this.Load += new System.EventHandler(this.FmListCL_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label tex_menuName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox tex_typeCL;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tex_lifeValue;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tex_yujingValue;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.FlowLayoutPanel ProcessPanel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox tex_proID;
        private System.Windows.Forms.Label tex_proName;
        private System.Windows.Forms.ComboBox tex_unit;
        private System.Windows.Forms.Label tex_tuijian;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tex_mark;
    }
}