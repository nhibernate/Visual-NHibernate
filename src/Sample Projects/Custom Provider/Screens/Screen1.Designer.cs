namespace Demo.Providers.Test.Screens
{
    partial class Screen1
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Screen1));
            this.txtSchool = new System.Windows.Forms.TextBox();
            this.btnAddSchool = new System.Windows.Forms.Button();
            this.txtPupil = new System.Windows.Forms.TextBox();
            this.btnAddPupil = new System.Windows.Forms.Button();
            this.btnDeleteSchool = new System.Windows.Forms.Button();
            this.btnDeletePupil = new System.Windows.Forms.Button();
            this.labelSchools = new System.Windows.Forms.Label();
            this.labelPupils = new System.Windows.Forms.Label();
            this.listSchools = new System.Windows.Forms.ListView();
            this.listPupils = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // txtSchool
            // 
            this.txtSchool.Location = new System.Drawing.Point(77, 33);
            this.txtSchool.Name = "txtSchool";
            this.txtSchool.Size = new System.Drawing.Size(120, 20);
            this.txtSchool.TabIndex = 2;
            // 
            // btnAddSchool
            // 
            this.btnAddSchool.Location = new System.Drawing.Point(203, 33);
            this.btnAddSchool.Name = "btnAddSchool";
            this.btnAddSchool.Size = new System.Drawing.Size(36, 23);
            this.btnAddSchool.TabIndex = 3;
            this.btnAddSchool.Text = "Add";
            this.btnAddSchool.UseVisualStyleBackColor = true;
            this.btnAddSchool.Click += new System.EventHandler(this.btnAddSchool_Click);
            // 
            // txtPupil
            // 
            this.txtPupil.Location = new System.Drawing.Point(77, 218);
            this.txtPupil.Name = "txtPupil";
            this.txtPupil.Size = new System.Drawing.Size(120, 20);
            this.txtPupil.TabIndex = 4;
            // 
            // btnAddPupil
            // 
            this.btnAddPupil.Location = new System.Drawing.Point(203, 218);
            this.btnAddPupil.Name = "btnAddPupil";
            this.btnAddPupil.Size = new System.Drawing.Size(36, 23);
            this.btnAddPupil.TabIndex = 5;
            this.btnAddPupil.Text = "Add";
            this.btnAddPupil.UseVisualStyleBackColor = true;
            this.btnAddPupil.Click += new System.EventHandler(this.btnAddPupil_Click);
            // 
            // btnDeleteSchool
            // 
            this.btnDeleteSchool.Location = new System.Drawing.Point(245, 59);
            this.btnDeleteSchool.Name = "btnDeleteSchool";
            this.btnDeleteSchool.Size = new System.Drawing.Size(52, 23);
            this.btnDeleteSchool.TabIndex = 6;
            this.btnDeleteSchool.Text = "Delete";
            this.btnDeleteSchool.UseVisualStyleBackColor = true;
            this.btnDeleteSchool.Click += new System.EventHandler(this.btnDeleteSchool_Click);
            // 
            // btnDeletePupil
            // 
            this.btnDeletePupil.Location = new System.Drawing.Point(245, 244);
            this.btnDeletePupil.Name = "btnDeletePupil";
            this.btnDeletePupil.Size = new System.Drawing.Size(52, 23);
            this.btnDeletePupil.TabIndex = 7;
            this.btnDeletePupil.Text = "Delete";
            this.btnDeletePupil.UseVisualStyleBackColor = true;
            this.btnDeletePupil.Click += new System.EventHandler(this.btnDeletePupil_Click);
            // 
            // labelSchools
            // 
            this.labelSchools.AutoSize = true;
            this.labelSchools.Location = new System.Drawing.Point(74, 17);
            this.labelSchools.Name = "labelSchools";
            this.labelSchools.Size = new System.Drawing.Size(45, 13);
            this.labelSchools.TabIndex = 8;
            this.labelSchools.Text = "Schools";
            // 
            // labelPupils
            // 
            this.labelPupils.AutoSize = true;
            this.labelPupils.Location = new System.Drawing.Point(74, 202);
            this.labelPupils.Name = "labelPupils";
            this.labelPupils.Size = new System.Drawing.Size(35, 13);
            this.labelPupils.TabIndex = 9;
            this.labelPupils.Text = "Pupils";
            // 
            // listSchools
            // 
            this.listSchools.FullRowSelect = true;
            this.listSchools.Location = new System.Drawing.Point(77, 59);
            this.listSchools.MultiSelect = false;
            this.listSchools.Name = "listSchools";
            this.listSchools.Size = new System.Drawing.Size(121, 97);
            this.listSchools.TabIndex = 10;
            this.listSchools.UseCompatibleStateImageBehavior = false;
            this.listSchools.View = System.Windows.Forms.View.List;
            this.listSchools.SelectedIndexChanged += new System.EventHandler(this.listSchools_SelectedIndexChanged);
            // 
            // listPupils
            // 
            this.listPupils.FullRowSelect = true;
            this.listPupils.Location = new System.Drawing.Point(77, 244);
            this.listPupils.MultiSelect = false;
            this.listPupils.Name = "listPupils";
            this.listPupils.Size = new System.Drawing.Size(121, 97);
            this.listPupils.TabIndex = 11;
            this.listPupils.UseCompatibleStateImageBehavior = false;
            this.listPupils.View = System.Windows.Forms.View.List;
            // 
            // Screen1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listPupils);
            this.Controls.Add(this.listSchools);
            this.Controls.Add(this.labelPupils);
            this.Controls.Add(this.labelSchools);
            this.Controls.Add(this.btnDeletePupil);
            this.Controls.Add(this.btnDeleteSchool);
            this.Controls.Add(this.btnAddPupil);
            this.Controls.Add(this.txtPupil);
            this.Controls.Add(this.btnAddSchool);
            this.Controls.Add(this.txtSchool);
            this.Name = "Screen1";
            this.NavBarIcon = ((System.Drawing.Image)(resources.GetObject("$this.NavBarIcon")));
            this.Size = new System.Drawing.Size(596, 432);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSchool;
        private System.Windows.Forms.Button btnAddSchool;
        private System.Windows.Forms.TextBox txtPupil;
        private System.Windows.Forms.Button btnAddPupil;
        private System.Windows.Forms.Button btnDeleteSchool;
        private System.Windows.Forms.Button btnDeletePupil;
        private System.Windows.Forms.Label labelSchools;
        private System.Windows.Forms.Label labelPupils;
        private System.Windows.Forms.ListView listSchools;
        private System.Windows.Forms.ListView listPupils;
    }
}
