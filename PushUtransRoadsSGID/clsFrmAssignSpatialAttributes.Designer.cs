namespace PushUtransRoadsSGID
{
    partial class clsFrmAssignSpatialAttributes
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cboCityRight = new System.Windows.Forms.ComboBox();
            this.cboCityLeft = new System.Windows.Forms.ComboBox();
            this.cboZipRight = new System.Windows.Forms.ComboBox();
            this.cboZipLeft = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.cboCounty = new System.Windows.Forms.ComboBox();
            this.cboAddressSystem = new System.Windows.Forms.ComboBox();
            this.cboUspsName = new System.Windows.Forms.ComboBox();
            this.cboAddressQuad = new System.Windows.Forms.ComboBox();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cboChooseLayer);
            this.groupBox3.Location = new System.Drawing.Point(12, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(343, 61);
            this.groupBox3.TabIndex = 9;
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
            this.groupBox1.Controls.Add(this.cboAddressQuad);
            this.groupBox1.Controls.Add(this.cboUspsName);
            this.groupBox1.Controls.Add(this.cboAddressSystem);
            this.groupBox1.Controls.Add(this.cboCounty);
            this.groupBox1.Controls.Add(this.cboZipLeft);
            this.groupBox1.Controls.Add(this.cboZipRight);
            this.groupBox1.Controls.Add(this.cboCityLeft);
            this.groupBox1.Controls.Add(this.cboCityRight);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(13, 96);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(343, 255);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Choose Fields to Assign";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "City Right";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 133);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "County";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Zip Right";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(182, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Zip Left";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 187);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "USPS Name";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(182, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "City Left";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(182, 133);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Address System";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(182, 187);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "Address Quad";
            // 
            // cboCityRight
            // 
            this.cboCityRight.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCityRight.FormattingEnabled = true;
            this.cboCityRight.Location = new System.Drawing.Point(23, 45);
            this.cboCityRight.Name = "cboCityRight";
            this.cboCityRight.Size = new System.Drawing.Size(139, 21);
            this.cboCityRight.TabIndex = 8;
            // 
            // cboCityLeft
            // 
            this.cboCityLeft.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCityLeft.FormattingEnabled = true;
            this.cboCityLeft.Location = new System.Drawing.Point(184, 45);
            this.cboCityLeft.Name = "cboCityLeft";
            this.cboCityLeft.Size = new System.Drawing.Size(139, 21);
            this.cboCityLeft.TabIndex = 9;
            // 
            // cboZipRight
            // 
            this.cboZipRight.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboZipRight.FormattingEnabled = true;
            this.cboZipRight.Location = new System.Drawing.Point(23, 95);
            this.cboZipRight.Name = "cboZipRight";
            this.cboZipRight.Size = new System.Drawing.Size(139, 21);
            this.cboZipRight.TabIndex = 10;
            // 
            // cboZipLeft
            // 
            this.cboZipLeft.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboZipLeft.FormattingEnabled = true;
            this.cboZipLeft.Location = new System.Drawing.Point(184, 95);
            this.cboZipLeft.Name = "cboZipLeft";
            this.cboZipLeft.Size = new System.Drawing.Size(139, 21);
            this.cboZipLeft.TabIndex = 11;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(114, 369);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(142, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "Assign Attributes";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // cboCounty
            // 
            this.cboCounty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCounty.FormattingEnabled = true;
            this.cboCounty.Location = new System.Drawing.Point(23, 149);
            this.cboCounty.Name = "cboCounty";
            this.cboCounty.Size = new System.Drawing.Size(139, 21);
            this.cboCounty.TabIndex = 12;
            // 
            // cboAddressSystem
            // 
            this.cboAddressSystem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAddressSystem.FormattingEnabled = true;
            this.cboAddressSystem.Location = new System.Drawing.Point(183, 149);
            this.cboAddressSystem.Name = "cboAddressSystem";
            this.cboAddressSystem.Size = new System.Drawing.Size(139, 21);
            this.cboAddressSystem.TabIndex = 13;
            // 
            // cboUspsName
            // 
            this.cboUspsName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboUspsName.FormattingEnabled = true;
            this.cboUspsName.Location = new System.Drawing.Point(23, 203);
            this.cboUspsName.Name = "cboUspsName";
            this.cboUspsName.Size = new System.Drawing.Size(139, 21);
            this.cboUspsName.TabIndex = 14;
            // 
            // cboAddressQuad
            // 
            this.cboAddressQuad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAddressQuad.FormattingEnabled = true;
            this.cboAddressQuad.Location = new System.Drawing.Point(183, 203);
            this.cboAddressQuad.Name = "cboAddressQuad";
            this.cboAddressQuad.Size = new System.Drawing.Size(139, 21);
            this.cboAddressQuad.TabIndex = 15;
            // 
            // clsFrmAssignSpatialAttributes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(368, 420);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "clsFrmAssignSpatialAttributes";
            this.ShowIcon = false;
            this.Text = "Assign Attributes Spatially";
            this.Load += new System.EventHandler(this.clsFrmAssignSpatialAttributes_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cboChooseLayer;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboCityLeft;
        private System.Windows.Forms.ComboBox cboCityRight;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cboAddressQuad;
        private System.Windows.Forms.ComboBox cboUspsName;
        private System.Windows.Forms.ComboBox cboAddressSystem;
        private System.Windows.Forms.ComboBox cboCounty;
        private System.Windows.Forms.ComboBox cboZipLeft;
        private System.Windows.Forms.ComboBox cboZipRight;
        private System.Windows.Forms.Button button1;
    }
}