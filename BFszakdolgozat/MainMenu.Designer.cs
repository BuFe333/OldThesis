namespace BFszakdolgozat
{
    partial class MainMenu
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
            this.PenneyButton = new System.Windows.Forms.Button();
            this.SnLButton = new System.Windows.Forms.Button();
            this.KNaVButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.titleLabel = new System.Windows.Forms.Label();
            this.authorLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // PenneyButton
            // 
            this.PenneyButton.Location = new System.Drawing.Point(12, 41);
            this.PenneyButton.Name = "PenneyButton";
            this.PenneyButton.Size = new System.Drawing.Size(130, 23);
            this.PenneyButton.TabIndex = 11;
            this.PenneyButton.Text = "Penney\'s Game";
            this.PenneyButton.UseVisualStyleBackColor = true;
            this.PenneyButton.Click += new System.EventHandler(this.PenneyButton_Click);
            // 
            // SnLButton
            // 
            this.SnLButton.Location = new System.Drawing.Point(12, 70);
            this.SnLButton.Name = "SnLButton";
            this.SnLButton.Size = new System.Drawing.Size(130, 23);
            this.SnLButton.TabIndex = 12;
            this.SnLButton.Text = "Kígyók és Létrák";
            this.SnLButton.UseVisualStyleBackColor = true;
            this.SnLButton.Click += new System.EventHandler(this.SnLButton_Click);
            // 
            // KNaVButton
            // 
            this.KNaVButton.Location = new System.Drawing.Point(12, 99);
            this.KNaVButton.Name = "KNaVButton";
            this.KNaVButton.Size = new System.Drawing.Size(130, 23);
            this.KNaVButton.TabIndex = 13;
            this.KNaVButton.Text = "Ki Nevet a Végén";
            this.KNaVButton.UseVisualStyleBackColor = true;
            this.KNaVButton.Click += new System.EventHandler(this.KNaVButton_Click);
            // 
            // exitButton
            // 
            this.exitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.exitButton.Location = new System.Drawing.Point(12, 177);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(75, 23);
            this.exitButton.TabIndex = 15;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.titleLabel.Location = new System.Drawing.Point(12, 9);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(204, 16);
            this.titleLabel.TabIndex = 16;
            this.titleLabel.Text = "Valószínűséget használó játékok";
            // 
            // authorLabel
            // 
            this.authorLabel.AutoSize = true;
            this.authorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.authorLabel.Location = new System.Drawing.Point(12, 25);
            this.authorLabel.Name = "authorLabel";
            this.authorLabel.Size = new System.Drawing.Size(72, 13);
            this.authorLabel.TabIndex = 17;
            this.authorLabel.Text = "Bujtás Ferenc";
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(234, 212);
            this.Controls.Add(this.authorLabel);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.KNaVButton);
            this.Controls.Add(this.SnLButton);
            this.Controls.Add(this.PenneyButton);
            this.Name = "MainMenu";
            this.Text = "Főmenü";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button PenneyButton;
        private System.Windows.Forms.Button SnLButton;
        private System.Windows.Forms.Button KNaVButton;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label authorLabel;
    }
}

