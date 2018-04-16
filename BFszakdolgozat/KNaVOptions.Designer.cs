namespace BFszakdolgozat
{
    partial class KNaVOptions
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
            this.boardLengthBox = new System.Windows.Forms.TextBox();
            this.bonusStepsBox = new System.Windows.Forms.TextBox();
            this.figuresABox = new System.Windows.Forms.TextBox();
            this.figuresBBox = new System.Windows.Forms.TextBox();
            this.enemyTacticComboBox = new System.Windows.Forms.ComboBox();
            this.playerStartsCheckBox = new System.Windows.Forms.CheckBox();
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
            // boardLengthBox
            // 
            this.boardLengthBox.Location = new System.Drawing.Point(112, 14);
            this.boardLengthBox.Name = "boardLengthBox";
            this.boardLengthBox.Size = new System.Drawing.Size(100, 20);
            this.boardLengthBox.TabIndex = 4;
            this.boardLengthBox.Text = "-Board Length-";
            // 
            // bonusStepsBox
            // 
            this.bonusStepsBox.Location = new System.Drawing.Point(112, 40);
            this.bonusStepsBox.Name = "bonusStepsBox";
            this.bonusStepsBox.Size = new System.Drawing.Size(100, 20);
            this.bonusStepsBox.TabIndex = 5;
            this.bonusStepsBox.Text = "-Bonus Steps-";
            // 
            // figuresABox
            // 
            this.figuresABox.Location = new System.Drawing.Point(218, 15);
            this.figuresABox.Name = "figuresABox";
            this.figuresABox.Size = new System.Drawing.Size(100, 20);
            this.figuresABox.TabIndex = 6;
            this.figuresABox.Text = "-Figures A-";
            // 
            // figuresBBox
            // 
            this.figuresBBox.Location = new System.Drawing.Point(218, 41);
            this.figuresBBox.Name = "figuresBBox";
            this.figuresBBox.Size = new System.Drawing.Size(100, 20);
            this.figuresBBox.TabIndex = 7;
            this.figuresBBox.Text = "-Figures B-";
            // 
            // enemyTacticComboBox
            // 
            this.enemyTacticComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.enemyTacticComboBox.FormattingEnabled = true;
            this.enemyTacticComboBox.Items.AddRange(new object[] {
            "Tactic 1",
            "Tactic 2",
            "Tactic 3",
            "Tactic 4"});
            this.enemyTacticComboBox.Location = new System.Drawing.Point(451, 12);
            this.enemyTacticComboBox.Name = "enemyTacticComboBox";
            this.enemyTacticComboBox.Size = new System.Drawing.Size(121, 21);
            this.enemyTacticComboBox.TabIndex = 9;
            // 
            // playerStartsCheckBox
            // 
            this.playerStartsCheckBox.AutoSize = true;
            this.playerStartsCheckBox.Location = new System.Drawing.Point(324, 29);
            this.playerStartsCheckBox.Name = "playerStartsCheckBox";
            this.playerStartsCheckBox.Size = new System.Drawing.Size(85, 17);
            this.playerStartsCheckBox.TabIndex = 10;
            this.playerStartsCheckBox.Text = "Player Starts";
            this.playerStartsCheckBox.UseVisualStyleBackColor = true;
            // 
            // errorBox
            // 
            this.errorBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.errorBox.Location = new System.Drawing.Point(249, 227);
            this.errorBox.Multiline = true;
            this.errorBox.Name = "errorBox";
            this.errorBox.ReadOnly = true;
            this.errorBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.errorBox.Size = new System.Drawing.Size(242, 30);
            this.errorBox.TabIndex = 49;
            // 
            // errorLabel
            // 
            this.errorLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.errorLabel.AutoSize = true;
            this.errorLabel.Location = new System.Drawing.Point(190, 232);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(53, 13);
            this.errorLabel.TabIndex = 48;
            this.errorLabel.Text = "Error Log:";
            // 
            // KNaVOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(584, 262);
            this.Controls.Add(this.errorBox);
            this.Controls.Add(this.errorLabel);
            this.Controls.Add(this.playerStartsCheckBox);
            this.Controls.Add(this.enemyTacticComboBox);
            this.Controls.Add(this.figuresBBox);
            this.Controls.Add(this.figuresABox);
            this.Controls.Add(this.bonusStepsBox);
            this.Controls.Add(this.boardLengthBox);
            this.Controls.Add(this.deleteLastDice);
            this.Controls.Add(this.addDiceButton);
            this.Controls.Add(this.nextButton);
            this.Controls.Add(this.backButton);
            this.Name = "KNaVOptions";
            this.Text = "Ki Nevet a Végén - Beállítások";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.Button nextButton;
        private System.Windows.Forms.Button addDiceButton;
        private System.Windows.Forms.Button deleteLastDice;
        private System.Windows.Forms.TextBox boardLengthBox;
        private System.Windows.Forms.TextBox bonusStepsBox;
        private System.Windows.Forms.TextBox figuresABox;
        private System.Windows.Forms.TextBox figuresBBox;
        private System.Windows.Forms.ComboBox enemyTacticComboBox;
        private System.Windows.Forms.CheckBox playerStartsCheckBox;
        private System.Windows.Forms.TextBox errorBox;
        private System.Windows.Forms.Label errorLabel;
    }
}