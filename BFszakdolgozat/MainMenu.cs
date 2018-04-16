using System;
using System.Windows.Forms;

namespace BFszakdolgozat
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        //Létrehozza és megjeleníti a PenneyOptions egy példányát, ezt az ablakot pedig elrejti.
        private void PenneyButton_Click(object sender, EventArgs e)
        {
            PenneyOptions window = new PenneyOptions(this);
            this.Hide();
            window.Show();
        }

        //Létrehozza és megjeleníti a SnLOptions egy példányát, ezt az ablakot pedig elrejti.
        private void SnLButton_Click(object sender, EventArgs e)
        {
            SnLOptions window = new SnLOptions(this);
            this.Hide();
            window.Show();
        }

        //Létrehozza és megjeleníti a KNaVOptions egy példányát, ezt az ablakot pedig elrejti.
        private void KNaVButton_Click(object sender, EventArgs e)
        {
            KNaVOptions window = new KNaVOptions(this);
            this.Hide();
            window.Show();
        }

        //Bezárja ezt az ablakot, ezzel a program futását befejezve.
        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
