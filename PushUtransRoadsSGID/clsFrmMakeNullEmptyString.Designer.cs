namespace PushUtransRoadsSGID
{
    partial class clsFrmMakeNullEmptyString
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
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cboChooseFields = new System.Windows.Forms.ComboBox();
            this.btnSelectBlankNulls = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cboChooseFieldToUpdate = new System.Windows.Forms.ComboBox();
            this.btnUpdateFieldValuesEmptyString = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(333, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "The user must select the targeted layer in ArcMap\'s table of contents.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(264, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "In order to update the values, the user must be editing.";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(530, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "This tool checks for blank and null values in the specified fields, with the opti" +
    "on to convert them to empty string.";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.btnSelectBlankNulls);
            this.groupBox1.Controls.Add(this.cboChooseFields);
            this.groupBox1.Location = new System.Drawing.Point(12, 113);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(527, 106);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select Features that have Blanks or Nulls";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(220, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "The tool will honor the layer\'s definition query.";
            // 
            // cboChooseFields
            // 
            this.cboChooseFields.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboChooseFields.FormattingEnabled = true;
            this.cboChooseFields.Items.AddRange(new object[] {
            "ADDR_SYS",
            "CARTOCODE",
            "FULLNAME",
            "PREDIR",
            "STREETNAME",
            "STREETTYPE",
            "SUFDIR",
            "ALIAS1",
            "ALIAS1TYPE",
            "ALIAS2",
            "ALIAS2TYPE",
            "ACSALIAS",
            "ACSNAME",
            "ACSSUF",
            "USPS_PLACE",
            "CLASS",
            "VERTLEVEL",
            "HWYNAME",
            "DOT_RTNAME",
            "DOT_RTPART"});
            this.cboChooseFields.Location = new System.Drawing.Point(23, 52);
            this.cboChooseFields.MaxDropDownItems = 25;
            this.cboChooseFields.Name = "cboChooseFields";
            this.cboChooseFields.Size = new System.Drawing.Size(240, 21);
            this.cboChooseFields.TabIndex = 0;
            // 
            // btnSelectBlankNulls
            // 
            this.btnSelectBlankNulls.Location = new System.Drawing.Point(307, 52);
            this.btnSelectBlankNulls.Name = "btnSelectBlankNulls";
            this.btnSelectBlankNulls.Size = new System.Drawing.Size(148, 23);
            this.btnSelectBlankNulls.TabIndex = 1;
            this.btnSelectBlankNulls.Text = "Select Blank or Nulls";
            this.btnSelectBlankNulls.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(23, 33);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(114, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Choose Field to Check";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnUpdateFieldValuesEmptyString);
            this.groupBox2.Controls.Add(this.cboChooseFieldToUpdate);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Location = new System.Drawing.Point(12, 235);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(527, 100);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Convert the Selected Features\' Blanks and Nulls to Empty String";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(23, 40);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(153, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Choose Field to Update Values";
            // 
            // cboChooseFieldToUpdate
            // 
            this.cboChooseFieldToUpdate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboChooseFieldToUpdate.FormattingEnabled = true;
            this.cboChooseFieldToUpdate.Items.AddRange(new object[] {
            "ADDR_SYS",
            "CARTOCODE",
            "FULLNAME",
            "PREDIR",
            "STREETNAME",
            "STREETTYPE",
            "SUFDIR",
            "ALIAS1",
            "ALIAS1TYPE",
            "ALIAS2",
            "ALIAS2TYPE",
            "ACSALIAS",
            "ACSNAME",
            "ACSSUF",
            "USPS_PLACE",
            "CLASS",
            "VERTLEVEL",
            "HWYNAME",
            "DOT_RTNAME",
            "DOT_RTPART"});
            this.cboChooseFieldToUpdate.Location = new System.Drawing.Point(23, 57);
            this.cboChooseFieldToUpdate.MaxDropDownItems = 25;
            this.cboChooseFieldToUpdate.Name = "cboChooseFieldToUpdate";
            this.cboChooseFieldToUpdate.Size = new System.Drawing.Size(240, 21);
            this.cboChooseFieldToUpdate.TabIndex = 1;
            // 
            // btnUpdateFieldValuesEmptyString
            // 
            this.btnUpdateFieldValuesEmptyString.Location = new System.Drawing.Point(307, 54);
            this.btnUpdateFieldValuesEmptyString.Name = "btnUpdateFieldValuesEmptyString";
            this.btnUpdateFieldValuesEmptyString.Size = new System.Drawing.Size(148, 23);
            this.btnUpdateFieldValuesEmptyString.TabIndex = 2;
            this.btnUpdateFieldValuesEmptyString.Text = "Update Selected Fields";
            this.btnUpdateFieldValuesEmptyString.UseVisualStyleBackColor = true;
            // 
            // clsFrmMakeNullEmptyString
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(572, 366);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "clsFrmMakeNullEmptyString";
            this.ShowIcon = false;
            this.Text = "Convert Nulls/Blanks to Empty String";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnSelectBlankNulls;
        private System.Windows.Forms.ComboBox cboChooseFields;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnUpdateFieldValuesEmptyString;
        private System.Windows.Forms.ComboBox cboChooseFieldToUpdate;
        private System.Windows.Forms.Label label7;
    }
}