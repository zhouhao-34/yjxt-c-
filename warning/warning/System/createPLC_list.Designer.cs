
namespace warning
{
    partial class createPLC_list
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
            this.button1 = new System.Windows.Forms.Button();
            this.PLCID = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.PLC_adress = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.addressLength = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.chufa = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.where_PLC_address = new System.Windows.Forms.TextBox();
            this.where_content = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.returnVal = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.com_chuli = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.PLC_addressType_w = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.where_tiaojian = new System.Windows.Forms.ComboBox();
            this.PLC_addressType = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.where_returnVal = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(88, 119);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "数据类型：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(82, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "PLC读地址：";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(156, 423);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(78, 31);
            this.button1.TabIndex = 11;
            this.button1.Text = "提交";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // PLCID
            // 
            this.PLCID.FormattingEnabled = true;
            this.PLCID.Items.AddRange(new object[] {
            "Omron"});
            this.PLCID.Location = new System.Drawing.Point(159, 48);
            this.PLCID.Name = "PLCID";
            this.PLCID.Size = new System.Drawing.Size(153, 20);
            this.PLCID.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(94, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "PLC名称：";
            // 
            // PLC_adress
            // 
            this.PLC_adress.Location = new System.Drawing.Point(159, 82);
            this.PLC_adress.Name = "PLC_adress";
            this.PLC_adress.Size = new System.Drawing.Size(99, 21);
            this.PLC_adress.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(88, 150);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 7;
            this.label7.Text = "数据长度：";
            // 
            // addressLength
            // 
            this.addressLength.Location = new System.Drawing.Point(159, 146);
            this.addressLength.Name = "addressLength";
            this.addressLength.Size = new System.Drawing.Size(75, 21);
            this.addressLength.TabIndex = 8;
            this.addressLength.Text = "1";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(64, 191);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(89, 12);
            this.label10.TabIndex = 15;
            this.label10.Text = "触发数据读取：";
            // 
            // chufa
            // 
            this.chufa.FormattingEnabled = true;
            this.chufa.Items.AddRange(new object[] {
            "请选择",
            "数据不为空",
            "与上一次不同",
            "指定信号",
            "等于true",
            "等于false"});
            this.chufa.Location = new System.Drawing.Point(159, 186);
            this.chufa.Name = "chufa";
            this.chufa.Size = new System.Drawing.Size(153, 20);
            this.chufa.TabIndex = 12;
            this.chufa.SelectedIndexChanged += new System.EventHandler(this.chufa_SelectedIndexChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(37, 326);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(113, 12);
            this.label12.TabIndex = 17;
            this.label12.Text = "指定信号读取条件：";
            // 
            // where_PLC_address
            // 
            this.where_PLC_address.Location = new System.Drawing.Point(156, 323);
            this.where_PLC_address.Name = "where_PLC_address";
            this.where_PLC_address.Size = new System.Drawing.Size(64, 21);
            this.where_PLC_address.TabIndex = 4;
            // 
            // where_content
            // 
            this.where_content.Location = new System.Drawing.Point(316, 323);
            this.where_content.Name = "where_content";
            this.where_content.Size = new System.Drawing.Size(64, 21);
            this.where_content.TabIndex = 4;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(52, 267);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(101, 12);
            this.label13.TabIndex = 17;
            this.label13.Text = "读取成功后回写：";
            // 
            // returnVal
            // 
            this.returnVal.Location = new System.Drawing.Point(159, 263);
            this.returnVal.Name = "returnVal";
            this.returnVal.Size = new System.Drawing.Size(153, 21);
            this.returnVal.TabIndex = 4;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.ForeColor = System.Drawing.Color.Red;
            this.label14.Location = new System.Drawing.Point(154, 347);
            this.label14.MaximumSize = new System.Drawing.Size(300, 0);
            this.label14.MinimumSize = new System.Drawing.Size(0, 30);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(197, 30);
            this.label14.TabIndex = 14;
            this.label14.Text = "例：D100             =         1";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label15.ForeColor = System.Drawing.Color.Red;
            this.label15.Location = new System.Drawing.Point(154, 290);
            this.label15.MaximumSize = new System.Drawing.Size(300, 0);
            this.label15.MinimumSize = new System.Drawing.Size(0, 30);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(221, 30);
            this.label15.TabIndex = 14;
            this.label15.Text = "读取成功后回写数据，不需要回写请留空";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(40, 227);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(113, 12);
            this.label4.TabIndex = 17;
            this.label4.Text = "读取后的数据处理：";
            // 
            // com_chuli
            // 
            this.com_chuli.FormattingEnabled = true;
            this.com_chuli.Items.AddRange(new object[] {
            "累加",
            "替换"});
            this.com_chuli.Location = new System.Drawing.Point(159, 223);
            this.com_chuli.Name = "com_chuli";
            this.com_chuli.Size = new System.Drawing.Size(153, 20);
            this.com_chuli.TabIndex = 12;
            this.com_chuli.SelectedIndexChanged += new System.EventHandler(this.chufa_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(37, 385);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(113, 12);
            this.label5.TabIndex = 17;
            this.label5.Text = "指定信号读取类型：";
            // 
            // PLC_addressType_w
            // 
            this.PLC_addressType_w.FormattingEnabled = true;
            this.PLC_addressType_w.Items.AddRange(new object[] {
            "",
            "byte",
            "int16",
            "Dint32",
            "Uint16",
            "UDint32",
            "real",
            "bool",
            "string"});
            this.PLC_addressType_w.Location = new System.Drawing.Point(156, 382);
            this.PLC_addressType_w.Name = "PLC_addressType_w";
            this.PLC_addressType_w.Size = new System.Drawing.Size(153, 20);
            this.PLC_addressType_w.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(284, 139);
            this.label6.MaximumSize = new System.Drawing.Size(300, 0);
            this.label6.MinimumSize = new System.Drawing.Size(0, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(299, 30);
            this.label6.TabIndex = 14;
            this.label6.Text = "int数据处理可以是累加或替换，其它类型只能是累加且使用次数+1";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(284, 405);
            this.label8.MaximumSize = new System.Drawing.Size(300, 0);
            this.label8.MinimumSize = new System.Drawing.Size(0, 30);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(299, 30);
            this.label8.TabIndex = 14;
            this.label8.Text = "条件是等于读取类型全部可选，如果不等于只能选int型";
            // 
            // where_tiaojian
            // 
            this.where_tiaojian.FormattingEnabled = true;
            this.where_tiaojian.Items.AddRange(new object[] {
            "=",
            ">",
            "<"});
            this.where_tiaojian.Location = new System.Drawing.Point(226, 323);
            this.where_tiaojian.Name = "where_tiaojian";
            this.where_tiaojian.Size = new System.Drawing.Size(84, 20);
            this.where_tiaojian.TabIndex = 12;
            this.where_tiaojian.SelectedIndexChanged += new System.EventHandler(this.chufa_SelectedIndexChanged);
            // 
            // PLC_addressType
            // 
            this.PLC_addressType.FormattingEnabled = true;
            this.PLC_addressType.Items.AddRange(new object[] {
            "",
            "byte",
            "int16",
            "Dint32",
            "Uint16",
            "UDint32",
            "real",
            "bool",
            "string"});
            this.PLC_addressType.Location = new System.Drawing.Point(159, 114);
            this.PLC_addressType.Name = "PLC_addressType";
            this.PLC_addressType.Size = new System.Drawing.Size(153, 20);
            this.PLC_addressType.TabIndex = 2;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(401, 326);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(77, 12);
            this.label9.TabIndex = 18;
            this.label9.Text = "指信号回写：";
            // 
            // where_returnVal
            // 
            this.where_returnVal.Location = new System.Drawing.Point(485, 323);
            this.where_returnVal.Name = "where_returnVal";
            this.where_returnVal.Size = new System.Drawing.Size(75, 21);
            this.where_returnVal.TabIndex = 19;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.Red;
            this.label11.Location = new System.Drawing.Point(459, 356);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(101, 12);
            this.label11.TabIndex = 20;
            this.label11.Text = "不需要回写请为空";
            // 
            // createPLC_list
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(595, 485);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.where_returnVal);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.where_tiaojian);
            this.Controls.Add(this.com_chuli);
            this.Controls.Add(this.chufa);
            this.Controls.Add(this.PLC_addressType_w);
            this.Controls.Add(this.PLC_addressType);
            this.Controls.Add(this.PLCID);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.addressLength);
            this.Controls.Add(this.where_content);
            this.Controls.Add(this.returnVal);
            this.Controls.Add(this.where_PLC_address);
            this.Controls.Add(this.PLC_adress);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label1);
            this.Name = "createPLC_list";
            this.Text = "添加PLC数据读取";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox PLCID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox PLC_adress;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox addressLength;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox chufa;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox where_PLC_address;
        private System.Windows.Forms.TextBox where_content;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox returnVal;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox com_chuli;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox PLC_addressType_w;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox where_tiaojian;
        private System.Windows.Forms.ComboBox PLC_addressType;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox where_returnVal;
        private System.Windows.Forms.Label label11;
    }
}