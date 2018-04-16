namespace BFszakdolgozat
{
    partial class PenneyOptions
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
            this.backButton = new System.Windows.Forms.Button();
            this.nextButton = new System.Windows.Forms.Button();
            this.addDiceButton = new System.Windows.Forms.Button();
            this.deleteLastDiceButton = new System.Windows.Forms.Button();
            this.addPatternAElementButton = new System.Windows.Forms.Button();
            this.deleteLastPatternAElementButton = new System.Windows.Forms.Button();
            this.addPatternBElementButton = new System.Windows.Forms.Button();
            this.deleteLastPatternBElementButton = new System.Windows.Forms.Button();
            this.errorBox = new System.Windows.Forms.TextBox();
            this.errorLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // backButton
            // 
            this.backButton.Location = new System.Drawing.Point(12, 227);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(75, 23);
            this.backButton.TabIndex = 0;
            this.backButton.Text = "Back";
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // nextButton
            // 
            this.nextButton.Location = new System.Drawing.Point(497, 227);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(75, 23);
            this.nextButton.TabIndex = 1;
            this.nextButton.Text = "Next";
            this.nextButton.UseVisualStyleBackColor = true;
            this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // addDiceButton
            // 
            this.addDiceButton.Location = new System.Drawing.Point(12, 12);
            this.addDiceButton.Name = "addDiceButton";
            this.addDiceButton.Size = new System.Drawing.Size(75, 23);
            this.addDiceButton.TabIndex = 2;
            this.addDiceButton.Text = "Add Die";
            this.addDiceButton.UseVisualStyleBackColor = true;
            this.addDiceButton.Click += new System.EventHandler(this.addDiceButton_Click);
            // 
            // deleteLastDiceButton
            // 
            this.deleteLastDiceButton.Location = new System.Drawing.Point(12, 41);
            this.deleteLastDiceButton.Name = "deleteLastDiceButton";
            this.deleteLastDiceButton.Size = new System.Drawing.Size(75, 23);
            this.deleteLastDiceButton.TabIndex = 3;
            this.deleteLastDiceButton.Text = "Remove Die";
            this.deleteLastDiceButton.UseVisualStyleBackColor = true;
            this.deleteLastDiceButton.Click += new System.EventHandler(this.deleteLastDiceButton_Click);
            // 
            // addPatternAElementButton
            // 
            this.addPatternAElementButton.Location = new System.Drawing.Point(140, 12);
            this.addPatternAElementButton.Name = "addPatternAElementButton";
            this.addPatternAElementButton.Size = new System.Drawing.Size(96, 23);
            this.addPatternAElementButton.TabIndex = 4;
            this.addPatternAElementButton.Text = "Add Pattern A";
            this.addPatternAElementButton.UseVisualStyleBackColor = true;
            this.addPatternAElementButton.Click += new System.EventHandler(this.addPatternAElementButton_Click);
            // 
            // deleteLastPatternAElementButton
            // 
            this.deleteLastPatternAElementButton.Location = new System.Drawing.Point(140, 41);
            this.deleteLastPatternAElementButton.Name = "deleteLastPatternAElementButton";
            this.deleteLastPatternAElementButton.Size = new System.Drawing.Size(96, 23);
            this.deleteLastPatternAElementButton.TabIndex = 5;
            this.deleteLastPatternAElementButton.Text = "Remove From A";
            this.deleteLastPatternAElementButton.UseVisualStyleBackColor = true;
            this.deleteLastPatternAElementButton.Click += new System.EventHandler(this.deleteLastPatternAElementButton_Click);
            // 
            // addPatternBElementButton
            // 
            this.addPatternBElementButton.Location = new System.Drawing.Point(260, 12);
            this.addPatternBElementButton.Name = "addPatternBElementButton";
            this.addPatternBElementButton.Size = new System.Drawing.Size(97, 23);
            this.addPatternBElementButton.TabIndex = 6;
            this.addPatternBElementButton.Text = "Add Pattern B";
            this.addPatternBElementButton.UseVisualStyleBackColor = true;
            this.addPatternBElementButton.Click += new System.EventHandler(this.addPatternBElementButton_Click);
            // 
            // deleteLastPatternBElementButton
            // 
            this.deleteLastPatternBElementButton.Location = new System.Drawing.Point(260, 41);
            this.deleteLastPatternBElementButton.Name = "deleteLastPatternBElementButton";
            this.deleteLastPatternBElementButton.Size = new System.Drawing.Size(97, 23);
            this.deleteLastPatternBElementButton.TabIndex = 7;
            this.deleteLastPatternBElementButton.Text = "Remove From B";
            this.deleteLastPatternBElementButton.UseVisualStyleBackColor = true;
            this.deleteLastPatternBElementButton.Click += new System.EventHandler(this.deleteLastPatternBElementButton_Click);
            // 
            // errorBox
            // 
            this.errorBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.errorBox.Location = new System.Drawing.Point(373, 34);
            this.errorBox.Multiline = true;
            this.errorBox.Name = "errorBox";
            this.errorBox.ReadOnly = true;
            this.errorBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.errorBox.Size = new System.Drawing.Size(199, 50);
            this.errorBox.TabIndex = 49;
            // 
            // errorLabel
            // 
            this.errorLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.errorLabel.AutoSize = true;
            this.errorLabel.Location = new System.Drawing.Point(370, 17);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(53, 13);
            this.errorLabel.TabIndex = 48;
            this.errorLabel.Text = "Error Log:";
            // 
            // PenneyOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(584, 262);
            this.Controls.Add(this.errorBox);
            this.Controls.Add(this.errorLabel);
            this.Controls.Add(this.deleteLastPatternBElementButton);
            this.Controls.Add(this.addPatternBElementButton);
            this.Controls.Add(this.deleteLastPatternAElementButton);
            this.Controls.Add(this.addPatternAElementButton);
            this.Controls.Add(this.deleteLastDiceButton);
            this.Controls.Add(this.addDiceButton);
            this.Controls.Add(this.nextButton);
            this.Controls.Add(this.backButton);
            this.Name = "PenneyOptions";
            this.Text = "Penney\'s Game - Beállítások";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.Button nextButton;
        private System.Windows.Forms.Button addDiceButton;
        private System.Windows.Forms.Button deleteLastDiceButton;
        private System.Windows.Forms.Button addPatternAElementButton;
        private System.Windows.Forms.Button deleteLastPatternAElementButton;
        private System.Windows.Forms.Button addPatternBElementButton;
        private System.Windows.Forms.Button deleteLastPatternBElementButton;
        private System.Windows.Forms.TextBox errorBox;
        private System.Windows.Forms.Label errorLabel;
    }
}