using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BFszakdolgozat
{
    public partial class KNaVOptions : Form
    {
        private MainMenu originForm;  //Az a Form amelyről ezt az ablakot elértük, a visszalépésnél ide kerülünk vissza.
        int boardLength;  //A körpálya hossza (pozitív páros szám).
        int bonusSteps;  //A körút megtétele után hátralévő lépések száma.
        int figuresA;  //Az 1. (A) játékos figuráinak száma.
        int figuresB;  //A 2. (B) játékos figuráinak száma.
        int[] diceArray;  //A felhasználó által szimulálni kívánt játékban használt kockák.
        bool playerStarts;  //Igaz, ha az 1. (A) játékos kezd, Hamis, ha a 2. (B) játékos kezd.
        int inputEnemyTactic;  //A 2. (B) játékos által használt taktika számkódja. (1,2,3, vagy 4)

        private int diceBoxes;  //diceArray beviteléhez használt szövegmezők száma.
        private List<System.Windows.Forms.TextBox> diceInputBoxes = new List<System.Windows.Forms.TextBox>();  //diceArray beviteléhez használt szövegmezők.



        //constructor

        //Inicializálja az osztályt, megadja az értéket az originForm-nak.
        public KNaVOptions(MainMenu origin)
        {
            originForm = origin;
            InitializeComponent();
            enemyTacticComboBox.SelectedIndex = 0;
        }



        //dice input

        //Létrehoz és visszaad egy új szövegdobozt, aminek koordinátáit a diceBoxes változó segítségével határoz meg, majd növeli a diceBoxes változó értékét.
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
        private void deleteLastDice_Click(object sender, EventArgs e)
        {
            if (diceBoxes > 0)
            {
                System.Windows.Forms.TextBox remove = diceInputBoxes[diceInputBoxes.Count - 1];
                diceInputBoxes.RemoveAt(diceInputBoxes.Count - 1);
                remove.Dispose();
                diceBoxes = diceBoxes - 1;
            }
        }



        //button clicks

        //Bezárja ezt az ablakot és megjeleníti a korábban elrejtett, originForm-ban tárolt ablakot.
        private void backButton_Click(object sender, EventArgs e)
        {
            this.Close();
            originForm.Show();
        }

        //Meghívja az InputIsCorrect függvényt, és amennyiben az igaz értéket ad vissza, elrejti ezt az ablakot és a megadott játék paraméterekkel létrehoz egy KNaVMain Form-ot.
        private void nextButton_Click(object sender, EventArgs e)
        {
            if (InputIsCorrect())
            {
                KNaVMain window = new KNaVMain(this, boardLength, bonusSteps, figuresA, figuresB, ref diceArray, playerStarts, inputEnemyTactic);
                this.Hide();
                window.Show();
            }
        }



        //other functions

        //Amennyiben helyesek az játéknak a boardLength, bonusSteps, figuresA, figuresB, diceArray, playerStarts, és inputEnemyTactic-ként megadni kívánt adatok,
        //(amelyeket rend szerint a boardLengthBox, bonusStepsBox, figuresABox, figuresBBox, diceInputBoxes, playerStartsCheckBox, és enemyTacticComboBox-ban találhatók,)
        //akkor feltölti ezeket az elemeket a megfelelő adatokkal, és igaz értéket ad vissza.
        //Ellenkező esetben az a ShowErrorMessage metódus segítségével jelzi a hibát, majd hamis értéket ad vissza.
        private bool InputIsCorrect()
        {
            //board length check (min 1, div 2)
            if (int.TryParse(boardLengthBox.Text, out boardLength))
            {
                if (boardLength <= 0 || boardLength % 2 == 1)
                {
                    ShowErrorMessage("Board length must be a positive number that is divisible by 2!");
                    return false;
                }
            }
            else
            {
                ShowErrorMessage("Board length must be a positive number!");
                return false;
            }

            //bonus steps check (min 1)
            //check board length
            if (int.TryParse(bonusStepsBox.Text, out bonusSteps))
            {
                if (bonusSteps <= 0)
                {
                    ShowErrorMessage("Bonus steps must be a positive number!");
                    return false;
                }
            }
            else
            {
                ShowErrorMessage("Bonus steps must be a positive number!");
                return false;
            }

            //Dice check

            if (diceBoxes <= 0)
            {
                ShowErrorMessage("No dice pool given!");
                return false;
            }

            this.diceArray = new int[diceBoxes];

            for (int i = 0; i < diceBoxes; i++)
            {
                if (int.TryParse(diceInputBoxes[i].Text, out this.diceArray[i]))
                {
                    if (this.diceArray[i] <= 0)
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

            //figures A check
            if (int.TryParse(figuresABox.Text, out figuresA))
            {
                if (figuresA <= 0)
                {
                    ShowErrorMessage("Figures A must be a positive number!");
                    return false;
                }
            }
            else
            {
                ShowErrorMessage("Figures A must be a positive number!");
                return false;
            }

            //figures B check
            if (int.TryParse(figuresBBox.Text, out figuresB))
            {
                if (figuresB <= 0)
                {
                    ShowErrorMessage("Figures B must be a positive number!");
                    return false;
                }
            }
            else
            {
                ShowErrorMessage("Figures B must be a positive number!");
                return false;
            }

            //playerStarts assignment
            playerStarts = playerStartsCheckBox.Checked;

            //tactics assignment
            inputEnemyTactic = enemyTacticComboBox.SelectedIndex + 1;

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
