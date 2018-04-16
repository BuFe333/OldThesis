namespace BFszakdolgozat
{
    partial class SnLOptions
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
            this.deleteLastDice = new System.Windows.Forms.Button();
            this.addSnakeButton = new System.Windows.Forms.Button();
            this.deleteLastSnakeButton = new System.Windows.Forms.Button();
            this.addLadderButton = new System.Windows.Forms.Button();
            this.deleteLastLadderButton = new System.Windows.Forms.Button();
            this.boardLengthBox = new System.Windows.Forms.TextBox();
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
            this.nextButton.Location = new System.Drawing.Point(673, 227);
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
            // deleteLastDice
            // 
            this.deleteLastDice.Location = new System.Drawing.Point(12, 41);
            this.deleteLastDice.Name = "deleteLastDice";
            this.deleteLastDice.Size = new System.Drawing.Size(75, 23);
            this.deleteLastDice.TabIndex = 3;
            this.deleteLastDice.Text = "Remove Die";
            this.deleteLastDice.UseVisualStyleBackColor = true;
            this.deleteLastDice.Click += new System.EventHandler(this.deleteLastDice_Click);
            // 
            // addSnakeButton
            // 
            this.addSnakeButton.Location = new System.Drawing.Point(140, 12);
            this.addSnakeButton.Name = "addSnakeButton";
            this.addSnakeButton.Size = new System.Drawing.Size(95, 23);
            this.addSnakeButton.TabIndex = 4;
            this.addSnakeButton.Text = "Add Snake";
            this.addSnakeButton.UseVisualStyleBackColor = true;
            this.addSnakeButton.Click += new System.EventHandler(this.addSnakeButton_Click);
            // 
            // deleteLastSnakeButton
            // 
            this.deleteLastSnakeButton.Location = new System.Drawing.Point(140, 41);
            this.deleteLastSnakeButton.Name = "deleteLastSnakeButton";
            this.deleteLastSnakeButton.Size = new System.Drawing.Size(95, 23);
            this.deleteLastSnakeButton.TabIndex = 5;
            this.deleteLastSnakeButton.Text = "Remove Snake";
            this.deleteLastSnakeButton.UseVisualStyleBackColor = true;
            this.deleteLastSnakeButton.Click += new System.EventHandler(this.deleteLastSnakeButton_Click);
            // 
            // addLadderButton
            // 
            this.addLadderButton.Location = new System.Drawing.Point(370, 12);
            this.addLadderButton.Name = "addLadderButton";
            this.addLadderButton.Size = new System.Drawing.Size(96, 23);
            this.addLadderButton.TabIndex = 6;
            this.addLadderButton.Text = "Add Ladder";
            this.addLadderButton.UseVisualStyleBackColor = true;
            this.addLadderButton.Click += new System.EventHandler(this.addLadderButton_Click);
            // 
            // deleteLastLadderButton
            // 
            this.deleteLastLadderButton.Location = new System.Drawing.Point(370, 41);
            this.deleteLastLadderButton.Name = "deleteLastLadderButton";
            this.deleteLastLadderButton.Size = new System.Drawing.Size(96, 23);
            this.deleteLastLadderButton.TabIndex = 7;
            this.deleteLastLadderButton.Text = "Remove Ladder";
            this.deleteLastLadderButton.UseVisualStyleBackColor = true;
            this.deleteLastLadderButton.Click += new System.EventHandler(this.deleteLastLadderButton_Click);
            // 
            // boardLengthBox
            // 
            this.boardLengthBox.Location = new System.Drawing.Point(254, 27);
            this.boardLengthBox.Name = "boardLengthBox";
            this.boardLengthBox.Size = new System.Drawing.Size(100, 20);
            this.boardLengthBox.TabIndex = 8;
            this.boardLengthBox.Text = "-Board Length-";
            // 
            // errorBox
            // 
            this.errorBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.errorBox.Location = new System.Drawing.Point(496, 34);
            this.errorBox.Multiline = true;
            this.errorBox.Name = "errorBox";
            this.errorBox.ReadOnly = true;
            this.errorBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.errorBox.Size = new System.Drawing.Size(276, 30);
            this.errorBox.TabIndex = 47;
            // 
            // errorLabel
            // 
            this.errorLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.errorLabel.AutoSize = true;
            this.errorLabel.Location = new System.Drawing.Point(493, 12);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(53, 13);
            this.errorLabel.TabIndex = 46;
            this.errorLabel.Text = "Error Log:";
            // 
            // SnLOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(784, 262);
            this.Controls.Add(this.errorBox);
            this.Controls.Add(this.errorLabel);
            this.Controls.Add(this.boardLengthBox);
            this.Controls.Add(this.deleteLastLadderButton);
            this.Controls.Add(this.addLadderButton);
            this.Controls.Add(this.deleteLastSnakeButton);
            this.Controls.Add(this.addSnakeButton);
            this.Controls.Add(this.deleteLastDice);
            this.Controls.Add(this.addDiceButton);
            this.Controls.Add(this.nextButton);
            this.Controls.Add(this.backButton);
            this.Name = "SnLOptions";
            this.Text = "Kígyók és Létrák - Beállítások";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.Button nextButton;
        private System.Windows.Forms.Button addDiceButton;
        private System.Windows.Forms.Button deleteLastDice;
        private System.Windows.Forms.Button addSnakeButton;
        private System.Windows.Forms.Button deleteLastSnakeButton;
        private System.Windows.Forms.Button addLadderButton;
        private System.Windows.Forms.Button deleteLastLadderButton;
        private System.Windows.Forms.TextBox boardLengthBox;
        private System.Windows.Forms.TextBox errorBox;
        private System.Windows.Forms.Label errorLabel;
    }
}