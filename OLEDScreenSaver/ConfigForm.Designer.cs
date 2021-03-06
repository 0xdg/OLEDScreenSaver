﻿namespace OLEDScreenSaver
{
    partial class ConfigForm
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
            this.timeoutTextbox = new System.Windows.Forms.TextBox();
            this.cancelButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.screenNameTextbox = new System.Windows.Forms.TextBox();
            this.startupCheckbox = new System.Windows.Forms.CheckBox();
            this.timeoutLabel = new System.Windows.Forms.Label();
            this.screenLabel = new System.Windows.Forms.Label();
            this.pollRateLabel = new System.Windows.Forms.Label();
            this.pollRateTextbox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // timeoutTextbox
            // 
            this.timeoutTextbox.Location = new System.Drawing.Point(273, 6);
            this.timeoutTextbox.Name = "timeoutTextbox";
            this.timeoutTextbox.Size = new System.Drawing.Size(266, 26);
            this.timeoutTextbox.TabIndex = 0;
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(273, 168);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(130, 50);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(409, 168);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(130, 50);
            this.saveButton.TabIndex = 2;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // screenNameTextbox
            // 
            this.screenNameTextbox.Enabled = false;
            this.screenNameTextbox.Location = new System.Drawing.Point(273, 94);
            this.screenNameTextbox.Name = "screenNameTextbox";
            this.screenNameTextbox.Size = new System.Drawing.Size(266, 26);
            this.screenNameTextbox.TabIndex = 3;
            // 
            // startupCheckbox
            // 
            this.startupCheckbox.AutoSize = true;
            this.startupCheckbox.Location = new System.Drawing.Point(16, 182);
            this.startupCheckbox.Name = "startupCheckbox";
            this.startupCheckbox.Size = new System.Drawing.Size(188, 24);
            this.startupCheckbox.TabIndex = 4;
            this.startupCheckbox.Text = "Launch with Windows";
            this.startupCheckbox.UseVisualStyleBackColor = true;
            // 
            // timeoutLabel
            // 
            this.timeoutLabel.AutoSize = true;
            this.timeoutLabel.Location = new System.Drawing.Point(12, 9);
            this.timeoutLabel.Name = "timeoutLabel";
            this.timeoutLabel.Size = new System.Drawing.Size(193, 20);
            this.timeoutLabel.TabIndex = 5;
            this.timeoutLabel.Text = "Screen timeout in minutes";
            this.timeoutLabel.Click += new System.EventHandler(this.Label1_Click);
            // 
            // screenLabel
            // 
            this.screenLabel.AutoSize = true;
            this.screenLabel.Location = new System.Drawing.Point(12, 97);
            this.screenLabel.Name = "screenLabel";
            this.screenLabel.Size = new System.Drawing.Size(104, 20);
            this.screenLabel.TabIndex = 6;
            this.screenLabel.Text = "Screen name";
            // 
            // pollRateLabel
            // 
            this.pollRateLabel.AutoSize = true;
            this.pollRateLabel.Location = new System.Drawing.Point(12, 53);
            this.pollRateLabel.Name = "pollRateLabel";
            this.pollRateLabel.Size = new System.Drawing.Size(171, 20);
            this.pollRateLabel.TabIndex = 7;
            this.pollRateLabel.Text = "Poll rate in milliseconds";
            // 
            // pollRateTextbox
            // 
            this.pollRateTextbox.Location = new System.Drawing.Point(273, 50);
            this.pollRateTextbox.Name = "pollRateTextbox";
            this.pollRateTextbox.Size = new System.Drawing.Size(266, 26);
            this.pollRateTextbox.TabIndex = 8;
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 230);
            this.Controls.Add(this.pollRateTextbox);
            this.Controls.Add(this.pollRateLabel);
            this.Controls.Add(this.screenLabel);
            this.Controls.Add(this.timeoutLabel);
            this.Controls.Add(this.startupCheckbox);
            this.Controls.Add(this.screenNameTextbox);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.timeoutTextbox);
            this.Name = "ConfigForm";
            this.Text = "Config";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox timeoutTextbox;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.TextBox screenNameTextbox;
        private System.Windows.Forms.CheckBox startupCheckbox;
        private System.Windows.Forms.Label timeoutLabel;
        private System.Windows.Forms.Label screenLabel;
        private System.Windows.Forms.Label pollRateLabel;
        private System.Windows.Forms.TextBox pollRateTextbox;
    }
}