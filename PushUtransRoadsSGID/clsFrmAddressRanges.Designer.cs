namespace PushUtransRoadsSGID
{
    partial class clsFrmAddressRanges
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cboChooseLayer = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboToRight = new System.Windows.Forms.ComboBox();
            this.cboFromRight = new System.Windows.Forms.ComboBox();
            this.cboToLeft = new System.Windows.Forms.ComboBox();
            this.cboFromLeft = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnRangeOrder = new System.Windows.Forms.Button();
            this.btnOddEvenCheck = new System.Windows.Forms.Button();
            this.btnDecimalsCheck = new System.Windows.Forms.Button();
            this.cboCheckDecimals = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cboChooseLayer);
            this.groupBox3.Location = new System.Drawing.Point(12, 28);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(343, 61);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Choose GIS layer to work with";
            // 
            // cboChooseLayer
            // 
            this.cboChooseLayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboChooseLayer.FormattingEnabled = true;
            this.cboChooseLayer.Location = new System.Drawing.Point(23, 20);
            this.cboChooseLayer.Name = "cboChooseLayer";
            this.cboChooseLayer.Size = new System.Drawing.Size(300, 21);
            this.cboChooseLayer.TabIndex = 0;
            this.cboChooseLayer.SelectedIndexChanged += new System.EventHandler(this.cboChooseLayer_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cboToRight);
            this.groupBox1.Controls.Add(this.btnRangeOrder);
            this.groupBox1.Controls.Add(this.cboFromRight);
            this.groupBox1.Controls.Add(this.btnOddEvenCheck);
            this.groupBox1.Controls.Add(this.cboToLeft);
            this.groupBox1.Controls.Add(this.cboFromLeft);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(13, 96);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(342, 193);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Check Address Ranges";
            // 
            // cboToRight
            // 
            this.cboToRight.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboToRight.FormattingEnabled = true;
            this.cboToRight.Location = new System.Drawing.Point(201, 87);
            this.cboToRight.Name = "cboToRight";
            this.cboToRight.Size = new System.Drawing.Size(121, 21);
            this.cboToRight.TabIndex = 7;
            // 
            // cboFromRight
            // 
            this.cboFromRight.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFromRight.FormattingEnabled = true;
            this.cboFromRight.Location = new System.Drawing.Point(201, 43);
            this.cboFromRight.Name = "cboFromRight";
            this.cboFromRight.Size = new System.Drawing.Size(121, 21);
            this.cboFromRight.TabIndex = 6;
            // 
            // cboToLeft
            // 
            this.cboToLeft.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboToLeft.FormattingEnabled = true;
            this.cboToLeft.Location = new System.Drawing.Point(22, 87);
            this.cboToLeft.Name = "cboToLeft";
            this.cboToLeft.Size = new System.Drawing.Size(121, 21);
            this.cboToLeft.TabIndex = 5;
            // 
            // cboFromLeft
            // 
            this.cboFromLeft.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFromLeft.FormattingEnabled = true;
            this.cboFromLeft.Location = new System.Drawing.Point(22, 43);
            this.cboFromLeft.Name = "cboFromLeft";
            this.cboFromLeft.Size = new System.Drawing.Size(121, 21);
            this.cboFromLeft.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(198, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Right To";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Left To";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(198, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Right From";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Left From";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.cboCheckDecimals);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.btnDecimalsCheck);
            this.groupBox2.Location = new System.Drawing.Point(13, 311);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(342, 110);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Check for Decimals in Field";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(170, 68);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(152, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Round Decimals";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnRangeOrder
            // 
            this.btnRangeOrder.Location = new System.Drawing.Point(178, 147);
            this.btnRangeOrder.Name = "btnRangeOrder";
            this.btnRangeOrder.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnRangeOrder.Size = new System.Drawing.Size(152, 23);
            this.btnRangeOrder.TabIndex = 2;
            this.btnRangeOrder.Text = "Check Range Value Order";
            this.btnRangeOrder.UseVisualStyleBackColor = true;
            this.btnRangeOrder.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnOddEvenCheck
            // 
            this.btnOddEvenCheck.Location = new System.Drawing.Point(13, 147);
            this.btnOddEvenCheck.Name = "btnOddEvenCheck";
            this.btnOddEvenCheck.Size = new System.Drawing.Size(152, 23);
            this.btnOddEvenCheck.TabIndex = 1;
            this.btnOddEvenCheck.Text = "Check for Odd/Even Parity";
            this.btnOddEvenCheck.UseVisualStyleBackColor = true;
            // 
            // btnDecimalsCheck
            // 
            this.btnDecimalsCheck.Location = new System.Drawing.Point(170, 36);
            this.btnDecimalsCheck.Name = "btnDecimalsCheck";
            this.btnDecimalsCheck.Size = new System.Drawing.Size(152, 23);
            this.btnDecimalsCheck.TabIndex = 0;
            this.btnDecimalsCheck.Text = "Check for Decimals";
            this.btnDecimalsCheck.UseVisualStyleBackColor = true;
            this.btnDecimalsCheck.Click += new System.EventHandler(this.btnDecimalsCheck_Click);
            // 
            // cboCheckDecimals
            // 
            this.cboCheckDecimals.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCheckDecimals.FormattingEnabled = true;
            this.cboCheckDecimals.Location = new System.Drawing.Point(22, 51);
            this.cboCheckDecimals.Name = "cboCheckDecimals";
            this.cboCheckDecimals.Size = new System.Drawing.Size(121, 21);
            this.cboCheckDecimals.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Choose Field:";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // clsFrmAddressRanges
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 445);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "clsFrmAddressRanges";
            this.ShowIcon = false;
            this.Text = "Check Address Ranges";
            this.Load += new System.EventHandler(this.clsFrmAddressRanges_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cboChooseLayer;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboToRight;
        private System.Windows.Forms.ComboBox cboFromRight;
        private System.Windows.Forms.ComboBox cboToLeft;
        private System.Windows.Forms.ComboBox cboFromLeft;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnOddEvenCheck;
        private System.Windows.Forms.Button btnDecimalsCheck;
        private System.Windows.Forms.Button btnRangeOrder;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboCheckDecimals;
    }
}