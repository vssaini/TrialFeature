namespace TrialFeature
{
    partial class FrmMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.btnShowFirstDate = new System.Windows.Forms.Button();
            this.txtMessage = new System.Windows.Forms.RichTextBox();
            this.btnShowLastDate = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // btnShowFirstDate
            // 
            this.btnShowFirstDate.Location = new System.Drawing.Point(12, 12);
            this.btnShowFirstDate.Name = "btnShowFirstDate";
            this.btnShowFirstDate.Size = new System.Drawing.Size(110, 23);
            this.btnShowFirstDate.TabIndex = 1;
            this.btnShowFirstDate.Text = "Using first date";
            this.toolTip1.SetToolTip(this.btnShowFirstDate, "Show the date of using software");
            this.btnShowFirstDate.UseVisualStyleBackColor = true;
            this.btnShowFirstDate.Click += new System.EventHandler(this.btnShowDataFromRegistry_Click);
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(12, 51);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(304, 96);
            this.txtMessage.TabIndex = 2;
            this.txtMessage.Text = "";
            this.toolTip1.SetToolTip(this.txtMessage, "Show some notification");
            // 
            // btnShowLastDate
            // 
            this.btnShowLastDate.Location = new System.Drawing.Point(128, 12);
            this.btnShowLastDate.Name = "btnShowLastDate";
            this.btnShowLastDate.Size = new System.Drawing.Size(110, 23);
            this.btnShowLastDate.TabIndex = 3;
            this.btnShowLastDate.Text = "Using last date";
            this.toolTip1.SetToolTip(this.btnShowLastDate, "Show last date when software was used");
            this.btnShowLastDate.UseVisualStyleBackColor = true;
            this.btnShowLastDate.Click += new System.EventHandler(this.btnShowLastDate_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(328, 159);
            this.Controls.Add(this.btnShowLastDate);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.btnShowFirstDate);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(344, 198);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(344, 198);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "30 day Trial Feature";
            this.Shown += new System.EventHandler(this.FrmMain_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnShowFirstDate;
        private System.Windows.Forms.RichTextBox txtMessage;
        private System.Windows.Forms.Button btnShowLastDate;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}

