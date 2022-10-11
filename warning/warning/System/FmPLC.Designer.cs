
namespace warning
{
    partial class FmPLC
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tex_className = new System.Windows.Forms.TextBox();
            this.tex_PLC_name = new System.Windows.Forms.TextBox();
            this.com_PLC_brand = new System.Windows.Forms.ComboBox();
            this.com_PLC_model = new System.Windows.Forms.ComboBox();
            this.tex_PLC_ip = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.tex_PLC_port = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(215, 136);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "所属车间：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(221, 178);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "PLC名称：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(239, 220);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "品牌：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(239, 261);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "型号：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(227, 304);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "IP地址：";
            // 
            // tex_className
            // 
            this.tex_className.Location = new System.Drawing.Point(286, 131);
            this.tex_className.Name = "tex_className";
            this.tex_className.Size = new System.Drawing.Size(171, 21);
            this.tex_className.TabIndex = 1;
            // 
            // tex_PLC_name
            // 
            this.tex_PLC_name.Location = new System.Drawing.Point(286, 171);
            this.tex_PLC_name.Name = "tex_PLC_name";
            this.tex_PLC_name.Size = new System.Drawing.Size(171, 21);
            this.tex_PLC_name.TabIndex = 1;
            // 
            // com_PLC_brand
            // 
            this.com_PLC_brand.FormattingEnabled = true;
            this.com_PLC_brand.Items.AddRange(new object[] {
            "Omron",
            "Siemens"});
            this.com_PLC_brand.Location = new System.Drawing.Point(286, 214);
            this.com_PLC_brand.Name = "com_PLC_brand";
            this.com_PLC_brand.Size = new System.Drawing.Size(171, 20);
            this.com_PLC_brand.TabIndex = 2;
            // 
            // com_PLC_model
            // 
            this.com_PLC_model.FormattingEnabled = true;
            this.com_PLC_model.Items.AddRange(new object[] {
            "Fins-200",
            "S1200"});
            this.com_PLC_model.Location = new System.Drawing.Point(286, 257);
            this.com_PLC_model.Name = "com_PLC_model";
            this.com_PLC_model.Size = new System.Drawing.Size(171, 20);
            this.com_PLC_model.TabIndex = 2;
            // 
            // tex_PLC_ip
            // 
            this.tex_PLC_ip.Location = new System.Drawing.Point(286, 300);
            this.tex_PLC_ip.Name = "tex_PLC_ip";
            this.tex_PLC_ip.Size = new System.Drawing.Size(171, 21);
            this.tex_PLC_ip.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(286, 370);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(90, 35);
            this.button1.TabIndex = 4;
            this.button1.Text = "提交";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(239, 342);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "端口：";
            // 
            // tex_PLC_port
            // 
            this.tex_PLC_port.Location = new System.Drawing.Point(286, 337);
            this.tex_PLC_port.Name = "tex_PLC_port";
            this.tex_PLC_port.Size = new System.Drawing.Size(171, 21);
            this.tex_PLC_port.TabIndex = 3;
            // 
            // FmPLC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tex_PLC_port);
            this.Controls.Add(this.tex_PLC_ip);
            this.Controls.Add(this.com_PLC_model);
            this.Controls.Add(this.com_PLC_brand);
            this.Controls.Add(this.tex_PLC_name);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tex_className);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FmPLC";
            this.Text = "FmPLC";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tex_className;
        private System.Windows.Forms.TextBox tex_PLC_name;
        private System.Windows.Forms.ComboBox com_PLC_brand;
        private System.Windows.Forms.ComboBox com_PLC_model;
        private System.Windows.Forms.TextBox tex_PLC_ip;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tex_PLC_port;
    }
}