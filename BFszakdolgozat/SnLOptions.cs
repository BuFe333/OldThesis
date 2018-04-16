using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BFszakdolgozat
{
    public partial class SnLOptions : Form
    {
        private MainMenu originForm;  //Az a Form amelyről ezt az ablakot elértük, a visszalépésnél ide kerülünk vissza.
        private int[] diceArray;  //A felhasználó által szimulálni kívánt játékban használt kockák.
        private int boardLength;  //A felhasználó által szimulálni kívánt játékban a tábla hossza.
        private List<int[]> ladders;  //A felhasználó által szimulálni kívánt játékban a „kígyók” listája. A tömbök mind kételeműek, előbb a kiinduló, majd a végpont mezőszámát tárolják.
        private List<int[]> snakes;  //A felhasználó által szimulálni kívánt játékban a „létrák” listája. A tömbök mind kételeműek, előbb a kiinduló, majd a végpont mezőszámát tárolják.

        private int diceBoxes;  //diceArray beviteléhez használt szövegmezők száma.
        private List<System.Windows.Forms.TextBox> diceInputBoxes = new List<System.Windows.Forms.TextBox>();  //diceArray beviteléhez használt szövegmezők.

        private int snakeBoxes;  //snakes beviteléhez használt szövegmező-párok száma.
        private List<System.Windows.Forms.TextBox> snakeInputBoxes = new List<System.Windows.Forms.TextBox>();  //patternA beviteléhez használt szövegmezők.

        private int ladderBoxes;  //ladders beviteléhez használt szövegmező-párok száma.
        private List<System.Windows.Forms.TextBox> ladderInputBoxes = new List<System.Windows.Forms.TextBox>();  //patternB beviteléhez használt szövegmezők.



        //constructor

        //Inicializálja az osztályt, megadja az értéket az originForm-nak.
        public SnLOptions(MainMenu origin)
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



        //snake input

        //Létrehoz, és visszaad egy új szövegdobozt, aminek koordinátáit a snakeBoxes változó segítségével határoz meg.
        private System.Windows.Forms.TextBox AddNewSnakeStartBox()
        {
            System.Windows.Forms.TextBox txt = new System.Windows.Forms.TextBox();
            this.Controls.Add(txt);
            txt.Top = 30 + (snakeBoxes + 2) * 25;
            txt.Left = 140;
            txt.Text = "-Snake Start-";
            return txt;
        }

        //Létrehoz, és visszaad egy új szövegdobozt, aminek koordinátáit a snakeBoxes változó segítségével határoz meg, majd növeli a snakeBoxes változó értékét.
        private System.Windows.Forms.TextBox AddNewSnakeEndBox()
        {
            System.Windows.Forms.TextBox txt = new System.Windows.Forms.TextBox();
            this.Controls.Add(txt);
            txt.Top = 30 + (snakeBoxes + 2) * 25;
            txt.Left = 240;
            txt.Text = "-Snake End-";
            snakeBoxes = snakeBoxes + 1;
            return txt;
        }

        //Az AddNewSnakeStartBox() és AddNewSnakeEndBox() függvényekkel létrehoz két új elemet a snakeInputBoxes listában.
        private void addSnakeButton_Click(object sender, EventArgs e)
        {
            snakeInputBoxes.Add(AddNewSnakeStartBox());
            this.Controls.Add(snakeInputBoxes[snakeInputBoxes.Count - 1]);
            snakeInputBoxes.Add(AddNewSnakeEndBox());
            this.Controls.Add(snakeInputBoxes[snakeInputBoxes.Count - 1]);
        }

        //Amennyiben pozitív a snakeBoxes változó, csökkenti, és eltávolítja és törli a snakeInputBoxes lista utolsó két elemét.
        private void deleteLastSnakeButton_Click(object sender, EventArgs e)
        {
            if (snakeBoxes > 0)
            {
                System.Windows.Forms.TextBox remove = snakeInputBoxes[snakeInputBoxes.Count - 1];
                snakeInputBoxes.RemoveAt(snakeInputBoxes.Count - 1);
                remove.Dispose();
                remove = snakeInputBoxes[snakeInputBoxes.Count - 1];
                snakeInputBoxes.RemoveAt(snakeInputBoxes.Count - 1);
                remove.Dispose();
                snakeBoxes = snakeBoxes - 1;
            }
        }



        //ladder input

        //Létrehoz, és visszaad egy új szövegdobozt, aminek koordinátáit a ladderBoxes változó segítségével határoz meg.
        private System.Windows.Forms.TextBox AddNewLadderStartBox()
        {
            System.Windows.Forms.TextBox txt = new System.Windows.Forms.TextBox();
            this.Controls.Add(txt);
            txt.Top = 30 + (ladderBoxes + 2) * 25;
            txt.Left = 360;
            txt.Text = "-Ladder Start-";
            return txt;
        }

        //Létrehoz, és visszaad egy új szövegdobozt, aminek koordinátáit a ladderBoxes változó segítségével határoz meg, majd növeli a ladderBoxes változó értékét.
        private System.Windows.Forms.TextBox AddNewLadderEndBox()
        {
            System.Windows.Forms.TextBox txt = new System.Windows.Forms.TextBox();
            this.Controls.Add(txt);
            txt.Top = 30 + (ladderBoxes + 2) * 25;
            txt.Left = 460;
            txt.Text = "-Ladder End-";
            ladderBoxes = ladderBoxes + 1;
            return txt;
        }

        //Az AddNewLadderStartBox() és AddNewLadderEndBox() függvényekkel létrehoz két új elemet a ladderInputBoxes listában.
        private void addLadderButton_Click(object sender, EventArgs e)
        {
            ladderInputBoxes.Add(AddNewLadderStartBox());
            this.Controls.Add(ladderInputBoxes[ladderInputBoxes.Count - 1]);
            ladderInputBoxes.Add(AddNewLadderEndBox());
            this.Controls.Add(ladderInputBoxes[ladderInputBoxes.Count - 1]);
        }

        //Amennyiben pozitív a ladderBoxes változó, csökkentti, és eltávolítja és törli a ladderInputBoxes lista utolsó két elemét.
        private void deleteLastLadderButton_Click(object sender, EventArgs e)
        {
            if (ladderBoxes > 0)
            {
                System.Windows.Forms.TextBox remove = ladderInputBoxes[ladderInputBoxes.Count - 1];
                ladderInputBoxes.RemoveAt(ladderInputBoxes.Count - 1);
                remove.Dispose();
                remove = ladderInputBoxes[ladderInputBoxes.Count - 1];
                ladderInputBoxes.RemoveAt(ladderInputBoxes.Count - 1);
                remove.Dispose();
                ladderBoxes = ladderBoxes - 1;
            }
        }



        //button clicks

        //Bezárja ezt az ablakot és megjeleníti a korábban elrejtett, originForm-ban tárolt ablakot.
        private void backButton_Click(object sender, EventArgs e)
        {
            this.Close();
            originForm.Show();
        }

        //Meghívja az InputIsCorrect függvényt, és amennyiben az igaz értéket ad vissza, elrejti ezt az ablakot és a megadott játék paraméterekkel létrehoz egy SnLMain Form-ot.
        private void nextButton_Click(object sender, EventArgs e)
        {
            if (InputIsCorrect())
            {
                SnLMain window = new SnLMain(this, ref diceArray, boardLength, ref ladders, ref snakes);
                this.Hide();
                window.Show();
            }
        }



        //other functions

        //Amennyiben helyesek az játéknak a diceArray, boardLength, snakes, és ladders-ként megadni kívánt adatok,
        //(amelyeket rend szerint a diceInputBoxes, boardLengthBox, snakeInputBoxes, és ladderInputBoxes-ben találhatók,)
        //akkor feltölti ezeket a tömböket a megfelelő adatokkal, és igaz értéket ad vissza.
        //Ellenkező esetben az a ShowErrorMessage metódus segítségével jelzi a hibát, majd hamis értéket ad vissza.
        private bool InputIsCorrect()
        {
            //check board length
            if (int.TryParse(boardLengthBox.Text, out boardLength))
            {
                if (boardLength <= 1)
                {
                    ShowErrorMessage("Board length must be a positive number greater than 1!");
                    return false;
                }
            }
            else
            {
                ShowErrorMessage("Board length must be a positive number!");
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

            //Snakes check
            snakes = new List<int[]>();

            for (int i = 0; i < snakeBoxes; i++)
            {
                int[] newSnake = new int[2];

                //check snake start
                if (int.TryParse(snakeInputBoxes[2 * i].Text, out newSnake[0]))
                {
                    if (newSnake[0] >= 2 && newSnake[0] <= boardLength - 1)
                    {
                        //fix indexing;
                        newSnake[0]--;
                    }
                    else
                    {
                        ShowErrorMessage("Snake Start in impossible position!");
                        return false;
                    }
                }
                else
                {
                    ShowErrorMessage("Snake Start position not a number!");
                    return false;
                }

                //check snake end
                if (int.TryParse(snakeInputBoxes[2 * i + 1].Text, out newSnake[1]))
                {
                    if (newSnake[1] >= 1 && newSnake[1] <= boardLength - 2)
                    {
                        //fix indexing;
                        newSnake[1]--;
                    }
                    else
                    {
                        ShowErrorMessage("Snake End in impossible position!");
                        return false;
                    }
                }
                else
                {
                    ShowErrorMessage("Snake End position not a number!");
                    return false;
                }

                //check if valid snake
                if (newSnake[0] <= newSnake[1])
                {
                    ShowErrorMessage("Invalid snake: start must be further on the board then the end!");
                    return false;
                }

                //check if it conflicts with previous snakes
                for (int j = 0; j < snakes.Count; j++)
                {
                    if (snakes[j][0] == newSnake[0] || snakes[j][0] == newSnake[1] || newSnake[0] == snakes[j][1])
                    {
                        ShowErrorMessage("Conflict! A starting position can only be a start position for one snake or ladder, and it can't be an end position.");
                        return false;
                    }
                }

                snakes.Add(newSnake);
            }

            //Ladders check
            ladders = new List<int[]>();

            for (int i = 0; i < ladderBoxes; i++)
            {
                int[] newLadder = new int[2];

                //check ladder start
                if (int.TryParse(ladderInputBoxes[2 * i].Text, out newLadder[0]))
                {
                    if (newLadder[0] >= 2 && newLadder[0] <= boardLength - 1)
                    {
                        //fix indexing;
                        newLadder[0]--;
                    }
                    else
                    {
                        ShowErrorMessage("Ladder Start in impossible position!");
                        return false;
                    }
                }
                else
                {
                    ShowErrorMessage("Ladder Start position not a number!");
                    return false;
                }

                //check ladder end
                if (int.TryParse(ladderInputBoxes[2 * i + 1].Text, out newLadder[1]))
                {
                    if (newLadder[1] >= 3 && newLadder[1] <= boardLength)
                    {
                        //fix indexing;
                        newLadder[1]--;
                    }
                    else
                    {
                        ShowErrorMessage("Ladder End in impossible position!");
                        return false;
                    }
                }
                else
                {
                    ShowErrorMessage("Ladder End position not a number!");
                    return false;
                }

                //check if valid ladder
                if (newLadder[1] <= newLadder[0])
                {
                    ShowErrorMessage("Invalid ladder: end must be further on the board then the start!");
                    return false;
                }

                //check if it conflicts with snakes
                for (int j = 0; j < snakes.Count; j++)
                {
                    if (snakes[j][0] == newLadder[0] || snakes[j][0] == newLadder[1] || newLadder[0] == snakes[j][1])
                    {
                        ShowErrorMessage("Conflict! A starting position can only be a start position for one snake or ladder, and it can't be an end position.");
                        return false;
                    }
                }

                //check if it conflicts with previous ladders
                for (int j = 0; j < ladders.Count; j++)
                {
                    if (ladders[j][0] == newLadder[0] || ladders[j][0] == newLadder[1] || newLadder[0] == ladders[j][1])
                    {
                        ShowErrorMessage("Conflict! A starting position can only be a start position for one snake or ladder, and it can't be an end position.");
                        return false;
                    }
                }

                ladders.Add(newLadder);
            }

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
