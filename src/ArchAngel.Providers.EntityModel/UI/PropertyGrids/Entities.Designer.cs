namespace ArchAngel.Providers.EntityModel.UI.PropertyGrids
{
    partial class Entities
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Entities));
            this.labelNamespaces = new DevComponents.DotNetBar.LabelX();
            this.highlighter1 = new DevComponents.DotNetBar.Validator.Highlighter();
            this.labelAttributes = new DevComponents.DotNetBar.LabelX();
            this.labelFunctions = new DevComponents.DotNetBar.LabelX();
            this.labelProperties = new DevComponents.DotNetBar.LabelX();
            this.labelInterfaces = new DevComponents.DotNetBar.LabelX();
            this.buttonAddNamespace = new System.Windows.Forms.Button();
            this.buttonAddAttribute = new System.Windows.Forms.Button();
            this.buttonAddBaseName = new System.Windows.Forms.Button();
            this.buttonAddFunction = new System.Windows.Forms.Button();
            this.buttonAddProperty = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelNamespaces
            // 
            this.labelNamespaces.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelNamespaces.BackgroundStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(110)))), ((int)(((byte)(110)))));
            this.labelNamespaces.BackgroundStyle.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.labelNamespaces.BackgroundStyle.BackColorGradientAngle = 90;
            this.labelNamespaces.BackgroundStyle.Class = "";
            this.labelNamespaces.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelNamespaces.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.labelNamespaces.Location = new System.Drawing.Point(25, 14);
            this.labelNamespaces.Name = "labelNamespaces";
            this.labelNamespaces.Size = new System.Drawing.Size(75, 23);
            this.labelNamespaces.TabIndex = 6;
            this.labelNamespaces.Text = "Namespaces";
            this.labelNamespaces.TextAlignment = System.Drawing.StringAlignment.Center;
            this.labelNamespaces.Click += new System.EventHandler(this.labelNamespaces_Click);
            // 
            // highlighter1
            // 
            this.highlighter1.ContainerControl = this;
            this.highlighter1.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            // 
            // labelAttributes
            // 
            this.labelAttributes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            // 
            // 
            // 
            this.labelAttributes.BackgroundStyle.Class = "";
            this.labelAttributes.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelAttributes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.labelAttributes.Location = new System.Drawing.Point(158, 14);
            this.labelAttributes.Name = "labelAttributes";
            this.labelAttributes.Size = new System.Drawing.Size(75, 23);
            this.labelAttributes.TabIndex = 7;
            this.labelAttributes.Text = "Attributes";
            this.labelAttributes.TextAlignment = System.Drawing.StringAlignment.Center;
            this.labelAttributes.Click += new System.EventHandler(this.labelAttributes_Click);
            // 
            // labelFunctions
            // 
            this.labelFunctions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            // 
            // 
            // 
            this.labelFunctions.BackgroundStyle.Class = "";
            this.labelFunctions.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelFunctions.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.labelFunctions.Location = new System.Drawing.Point(560, 15);
            this.labelFunctions.Name = "labelFunctions";
            this.labelFunctions.Size = new System.Drawing.Size(75, 23);
            this.labelFunctions.TabIndex = 8;
            this.labelFunctions.Text = "Functions";
            this.labelFunctions.TextAlignment = System.Drawing.StringAlignment.Center;
            this.labelFunctions.Click += new System.EventHandler(this.labelMethods_Click);
            // 
            // labelProperties
            // 
            this.labelProperties.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            // 
            // 
            // 
            this.labelProperties.BackgroundStyle.Class = "";
            this.labelProperties.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelProperties.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.labelProperties.Location = new System.Drawing.Point(425, 14);
            this.labelProperties.Name = "labelProperties";
            this.labelProperties.Size = new System.Drawing.Size(75, 23);
            this.labelProperties.TabIndex = 7;
            this.labelProperties.Text = "Properties";
            this.labelProperties.TextAlignment = System.Drawing.StringAlignment.Center;
            this.labelProperties.Click += new System.EventHandler(this.labelProperties_Click);
            // 
            // labelInterfaces
            // 
            this.labelInterfaces.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            // 
            // 
            // 
            this.labelInterfaces.BackgroundStyle.Class = "";
            this.labelInterfaces.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelInterfaces.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.labelInterfaces.Location = new System.Drawing.Point(291, 14);
            this.labelInterfaces.Name = "labelInterfaces";
            this.labelInterfaces.Size = new System.Drawing.Size(75, 23);
            this.labelInterfaces.TabIndex = 7;
            this.labelInterfaces.Text = "Base Names";
            this.labelInterfaces.TextAlignment = System.Drawing.StringAlignment.Center;
            this.labelInterfaces.Click += new System.EventHandler(this.labelInterfaces_Click);
            // 
            // buttonAddNamespace
            // 
            this.buttonAddNamespace.Image = ((System.Drawing.Image)(resources.GetObject("buttonAddNamespace.Image")));
            this.buttonAddNamespace.Location = new System.Drawing.Point(106, 15);
            this.buttonAddNamespace.Name = "buttonAddNamespace";
            this.buttonAddNamespace.Size = new System.Drawing.Size(24, 24);
            this.buttonAddNamespace.TabIndex = 10;
            this.buttonAddNamespace.UseVisualStyleBackColor = true;
            this.buttonAddNamespace.Click += new System.EventHandler(this.buttonAddNamespace_Click);
            // 
            // buttonAddAttribute
            // 
            this.buttonAddAttribute.Image = ((System.Drawing.Image)(resources.GetObject("buttonAddAttribute.Image")));
            this.buttonAddAttribute.Location = new System.Drawing.Point(239, 13);
            this.buttonAddAttribute.Name = "buttonAddAttribute";
            this.buttonAddAttribute.Size = new System.Drawing.Size(24, 24);
            this.buttonAddAttribute.TabIndex = 11;
            this.buttonAddAttribute.UseVisualStyleBackColor = true;
            this.buttonAddAttribute.Click += new System.EventHandler(this.buttonAddAttribute_Click);
            // 
            // buttonAddBaseName
            // 
            this.buttonAddBaseName.Image = ((System.Drawing.Image)(resources.GetObject("buttonAddBaseName.Image")));
            this.buttonAddBaseName.Location = new System.Drawing.Point(372, 12);
            this.buttonAddBaseName.Name = "buttonAddBaseName";
            this.buttonAddBaseName.Size = new System.Drawing.Size(24, 24);
            this.buttonAddBaseName.TabIndex = 12;
            this.buttonAddBaseName.UseVisualStyleBackColor = true;
            this.buttonAddBaseName.Click += new System.EventHandler(this.buttonAddBaseName_Click);
            // 
            // buttonAddFunction
            // 
            this.buttonAddFunction.Image = ((System.Drawing.Image)(resources.GetObject("buttonAddFunction.Image")));
            this.buttonAddFunction.Location = new System.Drawing.Point(641, 13);
            this.buttonAddFunction.Name = "buttonAddFunction";
            this.buttonAddFunction.Size = new System.Drawing.Size(24, 24);
            this.buttonAddFunction.TabIndex = 13;
            this.buttonAddFunction.UseVisualStyleBackColor = true;
            this.buttonAddFunction.Click += new System.EventHandler(this.buttonAddFunction_Click);
            // 
            // buttonAddProperty
            // 
            this.buttonAddProperty.Image = ((System.Drawing.Image)(resources.GetObject("buttonAddProperty.Image")));
            this.buttonAddProperty.Location = new System.Drawing.Point(506, 12);
            this.buttonAddProperty.Name = "buttonAddProperty";
            this.buttonAddProperty.Size = new System.Drawing.Size(24, 24);
            this.buttonAddProperty.TabIndex = 14;
            this.buttonAddProperty.UseVisualStyleBackColor = true;
            this.buttonAddProperty.Click += new System.EventHandler(this.buttonAddProperty_Click);
            // 
            // Entities
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.buttonAddProperty);
            this.Controls.Add(this.buttonAddFunction);
            this.Controls.Add(this.buttonAddBaseName);
            this.Controls.Add(this.buttonAddAttribute);
            this.Controls.Add(this.buttonAddNamespace);
            this.Controls.Add(this.namespaceEditor1);
            this.Controls.Add(this.labelInterfaces);
            this.Controls.Add(this.labelProperties);
            this.Controls.Add(this.labelFunctions);
            this.Controls.Add(this.labelAttributes);
            this.Controls.Add(this.labelNamespaces);
            this.Name = "Entities";
            this.Size = new System.Drawing.Size(701, 535);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelNamespaces;
        private DevComponents.DotNetBar.Validator.Highlighter highlighter1;
        private DevComponents.DotNetBar.LabelX labelInterfaces;
        private DevComponents.DotNetBar.LabelX labelProperties;
        private DevComponents.DotNetBar.LabelX labelFunctions;
        private DevComponents.DotNetBar.LabelX labelAttributes;
        private System.Windows.Forms.Button buttonAddProperty;
        private System.Windows.Forms.Button buttonAddFunction;
        private System.Windows.Forms.Button buttonAddBaseName;
        private System.Windows.Forms.Button buttonAddAttribute;
        private System.Windows.Forms.Button buttonAddNamespace;
    }
}
