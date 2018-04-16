using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BFszakdolgozat
{
    public partial class PenneyOptions : Form
    {

        private MainMenu originForm;  //Az a Form amelyről ezt az ablakot elértük, a visszalépésnél ide kerülünk vissza.
        private int[] diceArray;  //A felhasználó által szimulálni kívánt játékban használt kockák.
        private int[] patternA;  //A felhasználó által szimulálni kívánt játékban az 1. (A) játékos által használt lista.
        private int[] patternB;  //A felhasználó által szimulálni kívánt játékban a 2. (B) játékos által használt lista.

        private int diceBoxes;  //diceArray beviteléhez használt szövegmezők száma.
        private List<System.Windows.Forms.TextBox> diceInputBoxes = new List<System.Windows.Forms.TextBox>();  //diceArray beviteléhez használt szövegmezők.

        private int patternABoxes;  //patternA beviteléhez használt szövegmezők száma.
        private List<System.Windows.Forms.TextBox> patternAInputBoxes = new List<System.Windows.Forms.TextBox>();  //patternA beviteléhez használt szövegmezők.

        private int patternBBoxes;  //patternB beviteléhez használt szövegmezők száma.
        private List<System.Windows.Forms.TextBox> patternBInputBoxes = new List<System.Windows.Forms.TextBox>();  //patternB beviteléhez használt szövegmezők.


        //Constructors

        //Inicializálja az osztályt, megadja az értéket az originForm-nak.
        public PenneyOptions(MainMenu origin)
        {
            originForm = origin;
            InitializeComponent();
        }



        //dice input

        //Létrehoz, és visszaad egy új szövegdobozt, aminek koordinátáit a diceBoxes változó segítségével határoz meg, majd növeli a diceBoxes változó értékét.
        private System.Windows.Forms.TextBox AddNewDice()
        {
            System.Windows.Forms.TextBox txt = new System.Windows.Forms.TextBox();
            this.Controls.Add(txt);
            txt.Top = 30 + (diceBoxes + 2) * 25;
            txt.Left = 20;
            txt.Text = "6";
            diceBoxes = diceBoxes + 1;
            return txt;
        }

        //Az AddNewDice() függvénnyel létrehoz egy új elemet a diceInputBoxes listában.
        private void addDiceButton_Click(object sender, EventArgs e)
        {
            diceInputBoxes.Add(AddNewDice());
            this.Controls.Add(diceInputBoxes[diceInputBoxes.Count - 1]);
        }

        //Amennyiben pozitív a diceBoxes változó, csökkenti, és eltávolítja és törli a diceInputBoxes lista utolsó elemét.
        private void deleteLastDiceButton_Click(object sender, EventArgs e)
        {
            if (diceBoxes > 0)
            {
                System.Windows.Forms.TextBox remove = diceInputBoxes[diceInputBoxes.Count - 1];
                diceInputBoxes.RemoveAt(diceInputBoxes.Count - 1);
                remove.Dispose();
                diceBoxes = diceBoxes - 1;
            }
        }



        //patternA input

        //Létrehoz, és visszaad egy új szövegdobozt, aminek koordinátáit a patternABoxes változó segítségével határoz meg, majd növeli a patternABoxes változó értékét.
        private System.Windows.Forms.TextBox AddNewElementA()
        {
            System.Windows.Forms.TextBox txt = new System.Windows.Forms.TextBox();
            this.Controls.Add(txt);
            txt.Top = 30 + (patternABoxes + 2) * 25;
            txt.Left = 140;
            txt.Text = "1";
            patternABoxes = patternABoxes + 1;
            return txt;
        }

        //Az AddNewElementA () függvénnyel létrehoz egy új elemet a patternAInputBoxes listában.
        private void addPatternAElementButton_Click(object sender, EventArgs e)
        {
            patternAInputBoxes.Add(AddNewElementA());
            this.Controls.Add(patternAInputBoxes[patternAInputBoxes.Count - 1]);
        }

        //Amennyiben pozitív a patternABoxes változó, csökkenti, és eltávolítja és törli a patternAInputBoxes lista utolsó elemét.
        private void deleteLastPatternAElementButton_Click(object sender, EventArgs e)
        {
            if (patternABoxes > 0)
            {
                System.Windows.Forms.TextBox remove = patternAInputBoxes[patternAInputBoxes.Count - 1];
                patternAInputBoxes.RemoveAt(patternAInputBoxes.Count - 1);
                remove.Dispose();
                patternABoxes = patternABoxes - 1;
            }
        }



        //patternB input

        //Létrehoz, és visszaad egy új szövegdobozt, aminek koordinátáit a patternBBoxes változó segítségével határoz meg, majd növeli a patternBBoxes változó értékét.
        private System.Windows.Forms.TextBox AddNewElementB()
        {
            System.Windows.Forms.TextBox txt = new System.Windows.Forms.TextBox();
            this.Controls.Add(txt);
            txt.Top = 30 + (patternBBoxes + 2) * 25;
            txt.Left = 260;
            txt.Text = "1";
            patternBBoxes = patternBBoxes + 1;
            return txt;
        }

        //Az AddNewElementB () függvénnyel létrehoz egy új elemet a patternBInputBoxes listában.
        private void addPatternBElementButton_Click(object sender, EventArgs e)
        {
            patternBInputBoxes.Add(AddNewElementB());
            this.Controls.Add(patternBInputBoxes[patternBInputBoxes.Count - 1]);
        }

        //Amennyiben pozitív a patternBBoxes változó, csökkenti, és eltávolítja és törli a patternBInputBoxes lista utolsó elemét.
        private void deleteLastPatternBElementButton_Click(object sender, EventArgs e)
        {
            if (patternBBoxes > 0)
            {
                System.Windows.Forms.TextBox remove = patternBInputBoxes[patternBInputBoxes.Count - 1];
                patternBInputBoxes.RemoveAt(patternBInputBoxes.Count - 1);
                remove.Dispose();
                patternBBoxes = patternBBoxes - 1;
            }
        }



        //button clicks

        //Bezárja ezt az ablakot és megjeleníti a korábban elrejtett, originForm-ban tárolt ablakot.
        private void backButton_Click(object sender, EventArgs e)
        {
            this.Close();
            originForm.Show();
        }

        //Meghívja az InputIsCorrect függvényt, és amennyiben az igaz értéket ad vissza, elrejti ezt az ablakot és a megadott játék paraméterekkel létrehoz egy PenneyMain Form-ot.
        private void nextButton_Click(object sender, EventArgs e)
        {
            if (InputIsCorrect())
            {
                PenneyMain window = new PenneyMain(this, ref diceArray, ref patternA, ref patternB);
                this.Hide();
                window.Show();
            }
        }



        //other functions

        //Amennyiben helyesek az játéknak a diceArray, patternA, és patternB-ként megadni kívánt adatok,
        //(amelyeket rend szerint a diceInputBoxes, patternAInputBoxes, és patternBInputBoxes-ben találhatók,)
        //akkor feltölti ezeket a tömböket a megfelelő adatokkal, és igaz értéket ad vissza.
        //Ellenkező esetben az a ShowErrorMessage metódus segítségével jelzi a hibát, majd hamis értéket ad vissza.
        private bool InputIsCorrect()
        {
            int maxRoll = 0;
            int minRoll = 0;

            //Dice check

            if(diceBoxes <= 0)
            {
                ShowErrorMessage("No dice pool given!");
                return false;
            }

            this.diceArray = new int[diceBoxes];

            for(int i=0; i<diceBoxes; i++)
            {
                if(int.TryParse(diceInputBoxes[i].Text, out this.diceArray[i]))
                {
                    if(this.diceArray[i] > 0)
                    {
                        maxRoll += this.diceArray[i];
                        minRoll++;
                    }
                    else
                    {
                        ShowErrorMessage("Die value must be positive!");
                        return false;
                    }
                }
                else
                {
                    ShowErrorMessage("Dice pool values must be numbers!");
                    return false;
                }
            }

            //Pattern A check

            if (patternABoxes <= 0)
            {
                ShowErrorMessage("No Pattern A given!");
                return false;
            }

            patternA = new int[patternABoxes];

            for (int i = 0; i < patternABoxes; i++)
            {
                if (int.TryParse(patternAInputBoxes[i].Text, out patternA[i]))
                {
                    if (patternA[i] >= minRoll && patternA[i] <= maxRoll)
                    {
                        patternA[i]--;
                    }
                    else
                    {
                        ShowErrorMessage("Invalid Pattern A value!");
                        return false;
                    }

                }
                else
                {
                    ShowErrorMessage("Pattern A values must be numbers!");
                    return false;
                }
            }

            //Pattern B check

            if (patternBBoxes <= 0)
            {
                ShowErrorMessage("No Pattern B given!");
                return false;
            }

            patternB = new int[patternBBoxes];

            for (int i = 0; i < patternBBoxes; i++)
            {
                if (int.TryParse(patternBInputBoxes[i].Text, out patternB[i]))
                {
                    if (patternB[i] >= minRoll && patternB[i] <= maxRoll)
                    {
                        patternB[i]--;
                    }
                    else
                    {
                        ShowErrorMessage("Invalid Pattern B value!");
                        return false;
                    }
                }
                else
                {
                    ShowErrorMessage("Pattern B values must be numbers!");
                    return false;
                }
            }

            //check for similarities

            int shorter = patternA.Length < patternB.Length ? patternA.Length : patternB.Length;
            bool same = true;
            for(int i=1; i<=shorter; i++)
            {
                if(patternA[patternA.Length-i] != patternB[patternB.Length - i])
                {
                    same = false;
                    break;
                }
            }

            if (same)
            {
                ShowErrorMessage("The two patterns are too similar!");
                return false;
            }

            Console.WriteLine("OK!");
            return true;
        }

        //A megadott hibaüzenetet jeleníti meg az errorBox szövegdobozban.
        private void ShowErrorMessage(String errorDescription)
        {
            errorBox.AppendText(Environment.NewLine);
            errorBox.AppendText("ERROR! " + errorDescription);
        }

    }
}
