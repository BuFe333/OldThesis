namespace BFszakdolgozat
{
    partial class PenneyMain
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
            this.mcSimulateMaxStepsBox = new System.Windows.Forms.TextBox();
            this.mcSimulateTimesBox = new System.Windows.Forms.TextBox();
            this.markov1TimesBox = new System.Windows.Forms.TextBox();
            this.outputTextBox = new System.Windows.Forms.TextBox();
            this.markov2Button = new System.Windows.Forms.Button();
            this.markov1Button = new System.Windows.Forms.Button();
            this.mcSimulateButton = new System.Windows.Forms.Button();
            this.playGameButton = new System.Windows.Forms.Button();
            this.showGraphButton = new System.Windows.Forms.Button();
            this.showStatesButton = new System.Windows.Forms.Button();
            this.markov1StartBox = new System.Windows.Forms.TextBox();
            this.markov2StartBox = new System.Windows.Forms.TextBox();
            this.stepButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.clearOutputButton = new System.Windows.Forms.Button();
            this.infoTextBox = new System.Windows.Forms.TextBox();
            this.markov1_5EndBox = new System.Windows.Forms.TextBox();
            this.markov1_5StartBox = new System.Windows.Forms.TextBox();
            this.markov1_5Button = new System.Windows.Forms.Button();
            this.errorBox = new System.Windows.Forms.TextBox();
            this.errorLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // backButton
            // 
            this.backButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.backButton.Location = new System.Drawing.Point(12, 327);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(75, 23);
            this.backButton.TabIndex = 0;
            this.backButton.Text = "Back";
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // mcSimulateMaxStepsBox
            // 
            this.mcSimulateMaxStepsBox.Location = new System.Drawing.Point(255, 67);
            this.mcSimulateMaxStepsBox.Name = "mcSimulateMaxStepsBox";
            this.mcSimulateMaxStepsBox.Size = new System.Drawing.Size(75, 20);
            this.mcSimulateMaxStepsBox.TabIndex = 22;
            this.mcSimulateMaxStepsBox.Text = "Max Steps";
            // 
            // mcSimulateTimesBox
            // 
            this.mcSimulateTimesBox.Location = new System.Drawing.Point(255, 41);
            this.mcSimulateTimesBox.Name = "mcSimulateTimesBox";
            this.mcSimulateTimesBox.Size = new System.Drawing.Size(75, 20);
            this.mcSimulateTimesBox.TabIndex = 21;
            this.mcSimulateTimesBox.Text = "Times";
            // 
            // markov1TimesBox
            // 
            this.markov1TimesBox.Location = new System.Drawing.Point(336, 67);
            this.markov1TimesBox.Name = "markov1TimesBox";
            this.markov1TimesBox.Size = new System.Drawing.Size(75, 20);
            this.markov1TimesBox.TabIndex = 20;
            this.markov1TimesBox.Text = "Times";
            // 
            // outputTextBox
            // 
            this.outputTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.outputTextBox.Location = new System.Drawing.Point(12, 119);
            this.outputTextBox.Multiline = true;
            this.outputTextBox.Name = "outputTextBox";
            this.outputTextBox.ReadOnly = true;
            this.outputTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.outputTextBox.Size = new System.Drawing.Size(318, 202);
            this.outputTextBox.TabIndex = 19;
            this.outputTextBox.Text = "Output:";
            // 
            // markov2Button
            // 
            this.markov2Button.Location = new System.Drawing.Point(498, 12);
            this.markov2Button.Name = "markov2Button";
            this.markov2Button.Size = new System.Drawing.Size(75, 23);
            this.markov2Button.TabIndex = 18;
            this.markov2Button.Text = "Markov 2";
            this.markov2Button.UseVisualStyleBackColor = true;
            this.markov2Button.Click += new System.EventHandler(this.markov2Button_Click);
            // 
            // markov1Button
            // 
            this.markov1Button.Location = new System.Drawing.Point(336, 12);
            this.markov1Button.Name = "markov1Button";
            this.markov1Button.Size = new System.Drawing.Size(75, 23);
            this.markov1Button.TabIndex = 17;
            this.markov1Button.Text = "Markov 1";
            this.markov1Button.UseVisualStyleBackColor = true;
            this.markov1Button.Click += new System.EventHandler(this.markov1Button_Click);
            // 
            // mcSimulateButton
            // 
            this.mcSimulateButton.Location = new System.Drawing.Point(255, 12);
            this.mcSimulateButton.Name = "mcSimulateButton";
            this.mcSimulateButton.Size = new System.Drawing.Size(75, 23);
            this.mcSimulateButton.TabIndex = 16;
            this.mcSimulateButton.Text = "MC Simulate";
            this.mcSimulateButton.UseVisualStyleBackColor = true;
            this.mcSimulateButton.Click += new System.EventHandler(this.mcSimulateButton_Click);
            // 
            // playGameButton
            // 
            this.playGameButton.Location = new System.Drawing.Point(174, 12);
            this.playGameButton.Name = "playGameButton";
            this.playGameButton.Size = new System.Drawing.Size(75, 23);
            this.playGameButton.TabIndex = 15;
            this.playGameButton.Text = "Play Game";
            this.playGameButton.UseVisualStyleBackColor = true;
            this.playGameButton.Click += new System.EventHandler(this.playGameButton_Click);
            // 
            // showGraphButton
            // 
            this.showGraphButton.Location = new System.Drawing.Point(93, 12);
            this.showGraphButton.Name = "showGraphButton";
            this.showGraphButton.Size = new System.Drawing.Size(75, 23);
            this.showGraphButton.TabIndex = 14;
            this.showGraphButton.Text = "Graph";
            this.showGraphButton.UseVisualStyleBackColor = true;
            this.showGraphButton.Click += new System.EventHandler(this.showGraphButton_Click);
            // 
            // showStatesButton
            // 
            this.showStatesButton.Location = new System.Drawing.Point(12, 12);
            this.showStatesButton.Name = "showStatesButton";
            this.showStatesButton.Size = new System.Drawing.Size(75, 23);
            this.showStatesButton.TabIndex = 13;
            this.showStatesButton.Text = "States";
            this.showStatesButton.UseVisualStyleBackColor = true;
            this.showStatesButton.Click += new System.EventHandler(this.showStatesButton_Click);
            // 
            // markov1StartBox
            // 
            this.markov1StartBox.Location = new System.Drawing.Point(336, 41);
            this.markov1StartBox.Name = "markov1StartBox";
            this.markov1StartBox.Size = new System.Drawing.Size(75, 20);
            this.markov1StartBox.TabIndex = 23;
            this.markov1StartBox.Text = "Start State";
            // 
            // markov2StartBox
            // 
            this.markov2StartBox.Location = new System.Drawing.Point(498, 41);
            this.markov2StartBox.Name = "markov2StartBox";
            this.markov2StartBox.Size = new System.Drawing.Size(75, 20);
            this.markov2StartBox.TabIndex = 24;
            this.markov2StartBox.Text = "Start State";
            // 
            // stepButton
            // 
            this.stepButton.Enabled = false;
            this.stepButton.Location = new System.Drawing.Point(174, 41);
            this.stepButton.Name = "stepButton";
            this.stepButton.Size = new System.Drawing.Size(75, 46);
            this.stepButton.TabIndex = 25;
            this.stepButton.Text = "Next Step";
            this.stepButton.UseVisualStyleBackColor = true;
            this.stepButton.Visible = false;
            this.stepButton.Click += new System.EventHandler(this.stepButton_Click);
            // 
            // stopButton
            // 
            this.stopButton.Enabled = false;
            this.stopButton.Location = new System.Drawing.Point(174, 93);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(75, 23);
            this.stopButton.TabIndex = 26;
            this.stopButton.Text = "End Game";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Visible = false;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // clearOutputButton
            // 
            this.clearOutputButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.clearOutputButton.Location = new System.Drawing.Point(174, 327);
            this.clearOutputButton.Name = "clearOutputButton";
            this.clearOutputButton.Size = new System.Drawing.Size(75, 23);
            this.clearOutputButton.TabIndex = 28;
            this.clearOutputButton.Text = "Clear Output";
            this.clearOutputButton.UseVisualStyleBackColor = true;
            this.clearOutputButton.Click += new System.EventHandler(this.clearOutputButton_Click);
            // 
            // infoTextBox
            // 
            this.infoTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.infoTextBox.Location = new System.Drawing.Point(336, 119);
            this.infoTextBox.Multiline = true;
            this.infoTextBox.Name = "infoTextBox";
            this.infoTextBox.ReadOnly = true;
            this.infoTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.infoTextBox.Size = new System.Drawing.Size(220, 202);
            this.infoTextBox.TabIndex = 29;
            this.infoTextBox.Text = "Game info:";
            // 
            // markov1_5EndBox
            // 
            this.markov1_5EndBox.Location = new System.Drawing.Point(417, 67);
            this.markov1_5EndBox.Name = "markov1_5EndBox";
            this.markov1_5EndBox.Size = new System.Drawing.Size(75, 20);
            this.markov1_5EndBox.TabIndex = 40;
            this.markov1_5EndBox.Text = "End State";
            // 
            // markov1_5StartBox
            // 
            this.markov1_5StartBox.Location = new System.Drawing.Point(417, 41);
            this.markov1_5StartBox.Name = "markov1_5StartBox";
            this.markov1_5StartBox.Size = new System.Drawing.Size(75, 20);
            this.markov1_5StartBox.TabIndex = 39;
            this.markov1_5StartBox.Text = "Start State";
            // 
            // markov1_5Button
            // 
            this.markov1_5Button.Location = new System.Drawing.Point(417, 12);
            this.markov1_5Button.Name = "markov1_5Button";
            this.markov1_5Button.Size = new System.Drawing.Size(75, 23);
            this.markov1_5Button.TabIndex = 38;
            this.markov1_5Button.Text = "Markov 1.5";
            this.markov1_5Button.UseVisualStyleBackColor = true;
            this.markov1_5Button.Click += new System.EventHandler(this.markov1_5Button_Click);
            // 
            // errorBox
            // 
            this.errorBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.errorBox.Location = new System.Drawing.Point(314, 327);
            this.errorBox.Multiline = true;
            this.errorBox.Name = "errorBox";
            this.errorBox.ReadOnly = true;
            this.errorBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.errorBox.Size = new System.Drawing.Size(242, 30);
            this.errorBox.TabIndex = 45;
            // 
            // errorLabel
            // 
            this.errorLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.errorLabel.AutoSize = true;
            this.errorLabel.Location = new System.Drawing.Point(255, 332);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(53, 13);
            this.errorLabel.TabIndex = 44;
            this.errorLabel.Text = "Error Log:";
            // 
            // PenneyMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(584, 362);
            this.Controls.Add(this.errorBox);
            this.Controls.Add(this.errorLabel);
            this.Controls.Add(this.markov1_5EndBox);
            this.Controls.Add(this.markov1_5StartBox);
            this.Controls.Add(this.markov1_5Button);
            this.Controls.Add(this.infoTextBox);
            this.Controls.Add(this.clearOutputButton);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.stepButton);
            this.Controls.Add(this.markov2StartBox);
            this.Controls.Add(this.markov1StartBox);
            this.Controls.Add(this.mcSimulateMaxStepsBox);
            this.Controls.Add(this.mcSimulateTimesBox);
            this.Controls.Add(this.markov1TimesBox);
            this.Controls.Add(this.outputTextBox);
            this.Controls.Add(this.markov2Button);
            this.Controls.Add(this.markov1Button);
            this.Controls.Add(this.mcSimulateButton);
            this.Controls.Add(this.playGameButton);
            this.Controls.Add(this.showGraphButton);
            this.Controls.Add(this.showStatesButton);
            this.Controls.Add(this.backButton);
            this.Name = "PenneyMain";
            this.Text = "Penney\'s Game - Műveletek";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.TextBox mcSimulateMaxStepsBox;
        private System.Windows.Forms.TextBox mcSimulateTimesBox;
        private System.Windows.Forms.TextBox markov1TimesBox;
        private System.Windows.Forms.TextBox outputTextBox;
        private System.Windows.Forms.Button markov2Button;
        private System.Windows.Forms.Button markov1Button;
        private System.Windows.Forms.Button mcSimulateButton;
        private System.Windows.Forms.Button playGameButton;
        private System.Windows.Forms.Button showGraphButton;
        private System.Windows.Forms.Button showStatesButton;
        private System.Windows.Forms.TextBox markov1StartBox;
        private System.Windows.Forms.TextBox markov2StartBox;
        private System.Windows.Forms.Button stepButton;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Button clearOutputButton;
        private System.Windows.Forms.TextBox infoTextBox;
        private System.Windows.Forms.TextBox markov1_5EndBox;
        private System.Windows.Forms.TextBox markov1_5StartBox;
        private System.Windows.Forms.Button markov1_5Button;
        private System.Windows.Forms.TextBox errorBox;
        private System.Windows.Forms.Label errorLabel;
    }
}