﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BFszakdolgozat
{
    public partial class SnLMain : Form
    {
        private SnLOptions originForm;  //Az a Form amelyről ezt az ablakot elértük, a visszalépésnél ide kerülünk vissza.
        private SnakesAndLadders currentGame;  //A műveletek végrehajtásához a konstruktor paraméterei alapján lérehozott SnakesAndLadders osztály példány.


        //Inicializálja az osztályt, megadja az értéket az originForm-nak,
        //és a paraméterek alapján inicializálja a currentGame SnakesAndLadders játékot.
        //Végül magát a form-ot inicializálja és kitölti az infoTextBox-ot.
        public SnLMain(SnLOptions origin, ref int[] diceArray, int boardLength, ref List<int[]> ladders, ref List<int[]> snakes)
        {
            originForm = origin;
            currentGame = new SnakesAndLadders(ref diceArray, boardLength, ref ladders, ref snakes);
            InitializeComponent();
            outputTextBox.AppendText(Environment.NewLine);

            infoTextBox.AppendText(Environment.NewLine);
            infoTextBox.AppendText("Board Length: " + boardLength);
            infoTextBox.AppendText(Environment.NewLine);
            infoTextBox.AppendText(Environment.NewLine);

            infoTextBox.AppendText("Snake List:");
            infoTextBox.AppendText(Environment.NewLine);
            for(int i=0; i<snakes.Count; i++)
            {
                infoTextBox.AppendText("Snake " + (i+1).ToString() + " - Start: " + (snakes[i][0]+1).ToString() + ", End: " + (snakes[i][1]+1).ToString());
                infoTextBox.AppendText(Environment.NewLine);
            }
            infoTextBox.AppendText(Environment.NewLine);

            infoTextBox.AppendText("Ladder List:");
            infoTextBox.AppendText(Environment.NewLine);
            for (int i = 0; i < ladders.Count; i++)
            {
                infoTextBox.AppendText("Ladder " + (i+1).ToString() + " - Start: " + (ladders[i][0]+1).ToString() + ", End: " + (ladders[i][1]+1).ToString());
                infoTextBox.AppendText(Environment.NewLine);
            }
        }



        //Bezárja ezt az ablakot és megjeleníti a korábban elrejtett, originForm-ban tárolt ablakot.
        private void backButton_Click(object sender, EventArgs e)
        {
            this.Close();
            originForm.Show();
        }

        //Kiírja az összes lehetséges játékeset (a currentGame gameStates listájának felhasználásával) az outputTextBox szövegdobozba.
        private void showStatesButton_Click(object sender, EventArgs e)
        {
            outputTextBox.AppendText("---GAME STATES---");
            outputTextBox.AppendText(Environment.NewLine);

            for (int i = 0; i < currentGame.gameStates.Count; i++)
            {
                outputTextBox.AppendText("Game state number " + i.ToString() + ": ");
                outputTextBox.AppendText("Figure is on Square " + (currentGame.gameStates[i][0] + 1).ToString());
                outputTextBox.AppendText(Environment.NewLine);
            }
        }

        //Kiírja az összes lehetséges átmenetet, amivel egy játékesetből egy másikba juthatunk (a currentGame gameGraph tömbjének felhasználásával) az outputTextBox szövegdobozba.
        private void showGraphButton_Click(object sender, EventArgs e)
        {
            outputTextBox.AppendText("---GAME GRAPH---");
            outputTextBox.AppendText(Environment.NewLine);

            for (int i = 0; i < currentGame.gameGraph.Length; i++)
            {
                for (int j = 0; j < currentGame.gameGraph[i].Count; j++)
                {
                    outputTextBox.AppendText("There exists a way from state " + i.ToString() + " to state number: " + currentGame.gameGraph[i][j].ToString());
                    outputTextBox.AppendText(Environment.NewLine);
                }
            }
        }

        //Elindítja a játék játszását, a többi művelet gombját lezárja, és megjeleníti a játékhoz használt stepButton és stopButton gombokat.
        private void playGameButton_Click(object sender, EventArgs e)
        {
            //disable other buttons and textboxes
            showStatesButton.Enabled = false;
            showGraphButton.Enabled = false;
            playGameButton.Enabled = false;
            mcSimulateButton.Enabled = false;
            mcSimulateTimesBox.Enabled = false;
            mcSimulateMaxStepsBox.Enabled = false;
            markov1Button.Enabled = false;
            markov1StartBox.Enabled = false;
            markov1TimesBox.Enabled = false;
            markov1_5Button.Enabled = false;
            markov1_5StartBox.Enabled = false;
            markov1_5EndBox.Enabled = false;
            markov2Button.Enabled = false;
            markov2StartBox.Enabled = false;

            //reset graph
            currentGame.ResetGame();

            //enable and show step and stop game button
            stepButton.Enabled = true;
            stopButton.Enabled = true;
            stepButton.Visible = true;
            stopButton.Visible = true;

            //Enter text to output field
            outputTextBox.AppendText("---GAME START---");
            outputTextBox.AppendText(Environment.NewLine);

            //start state
            outputTextBox.AppendText("Starting square: ");
            outputTextBox.AppendText("1");
            outputTextBox.AppendText(Environment.NewLine);

        }

        //Játék játszásában tesz egy lépést, és annak részleteit kiírja az outputTextBox-ba.
        private void stepButton_Click(object sender, EventArgs e)
        {
            //take step
            int[] newState = currentGame.TakeStep();

            //what did we roll?
            outputTextBox.AppendText("Dice roll result: " + currentGame.LastRollResult());
            outputTextBox.AppendText(Environment.NewLine);

            //new state out
            outputTextBox.AppendText("New Square:");

            outputTextBox.AppendText((newState[0] + 1).ToString());

            outputTextBox.AppendText(Environment.NewLine);

            //end game? check
            GameEndedCheck(ref newState);
        }

        //Leállítja a játékot az EndGame metódus meghívásával.
        private void stopButton_Click(object sender, EventArgs e)
        {
            EndGame();
            outputTextBox.AppendText("---GAME ENDED BY USER---");
            outputTextBox.AppendText(Environment.NewLine);
        }


        //Eldönti a jelenlegi játékállapotról, hogy a játék végállapota-e
        //(a currentGame EndGameCheck függvényét használva), és amennyiben végállapot,
        //automatikusan kiírja az outputTextBox-ba, hogy vége a játéknak és meghívja az EndGame metódust.
        private void GameEndedCheck(ref int[] currentState)
        {
            //check if game ended, if yes, call endGame:
            int status = currentGame.EndGameCheck(ref currentState);

            if (status < 0)
            {
                ShowErrorMessage("Tried to step into null state!");
                EndGame();
            }
            else if (status > 0)
            {
                outputTextBox.AppendText("---PLAYER WON---");
                EndGame();
            }
        }

        //A játék játszásához használt gombokat (stepButton, stopButton) újra elrejti,
        //és a többi művelet gombjait és szövegdobozait újra elérhetővé teszi.
        private void EndGame()
        {
            //disable and hide step and stop game button
            stepButton.Enabled = false;
            stopButton.Enabled = false;
            stepButton.Visible = false;
            stopButton.Visible = false;

            //enable other buttons and textboxes
            showStatesButton.Enabled = true;
            showGraphButton.Enabled = true;
            playGameButton.Enabled = true;
            mcSimulateButton.Enabled = true;
            mcSimulateTimesBox.Enabled = true;
            mcSimulateMaxStepsBox.Enabled = true;
            markov1Button.Enabled = true;
            markov1StartBox.Enabled = true;
            markov1TimesBox.Enabled = true;
            markov1_5Button.Enabled = true;
            markov1_5StartBox.Enabled = true;
            markov1_5EndBox.Enabled = true;
            markov2Button.Enabled = true;
            markov2StartBox.Enabled = true;
        }



        //Meghívja a SimulateCorrect függvényt, és amennyiben igaz értéket kap vissza,
        //Monte Carlo szimulációt hajt végre a függvény által értéket kapott paraméterekkel
        //a currentGame MonteCarloSimulation függvényével.
        private void mcSimulateButton_Click(object sender, EventArgs e)
        {
            int times = 0;
            int maxSteps = 0;
            if (SimulateCorrect(ref times, ref maxSteps))
            {
                int[] result = currentGame.MonteCarloSimulation(times, maxSteps);

                outputTextBox.AppendText(Environment.NewLine);
                outputTextBox.AppendText("Monte Carlo Simulation Results (Ran: " + times.ToString() + " times, Max Steps Allowed: " + maxSteps.ToString() + ")");
                outputTextBox.AppendText(Environment.NewLine);

                outputTextBox.AppendText("Times the test has failed to complete within given max steps: " + result[0].ToString());
                outputTextBox.AppendText(Environment.NewLine);
                outputTextBox.AppendText(Environment.NewLine);

                for (int i=1; i<result.Length; i++)
                {
                    if(result[i] > 0)
                    {
                        outputTextBox.AppendText("Number of tests that took exactly " + i.ToString() + " steps to complete: " + result[i].ToString());
                        outputTextBox.AppendText(Environment.NewLine);
                    }
                }

                outputTextBox.AppendText(Environment.NewLine);
            }
        }

        //Meghívja a MarkovCorrect függvényt, és amennyiben igaz értéket kap vissza,
        //a kapott paraméterekkel a currentGame Markov1 függvényével elemzi,
        //hogy egy játékállapotból kiindulva megadott számú lépés után
        //mennyi eséllyel leszünk az egyes játékállapotokban.
        private void markov1Button_Click(object sender, EventArgs e)
        {
            int times = 0;
            int start = -1;
            if (MarkovCorrect(ref start, ref times))
            {
                outputTextBox.AppendText(Environment.NewLine);
                outputTextBox.AppendText("Chance to be in game states after " + times + " steps if currently in game state: " + start);
                outputTextBox.AppendText(Environment.NewLine);

                double[] result = currentGame.Markov1(start, times);

                for(int i=0; i<result.Length; i++)
                {
                    outputTextBox.AppendText("Chance to be in state " + i.ToString() + ": " + result[i].ToString());
                    outputTextBox.AppendText(Environment.NewLine);
                }

            }
        }

        //Meghívja a Markov1_5Correct függvényt, és amennyiben igaz értéket kap vissza,
        //a kapott paraméterekkel a currentGame Markov1_5 függvényével elemzi,
        //hogy egy játékállapotból kiindulva átlagosan hányszor fogunk áthaladni
        //egy megadott játékállapoton (nem végállapot) amíg a játék befejeződik.
        private void markov1_5Button_Click(object sender, EventArgs e)
        {
            int start = -1;
            int end = -1;
            if (Markov1_5Correct(ref start, ref end))
            {
                outputTextBox.AppendText(Environment.NewLine);
                outputTextBox.AppendText("If we're in state " + start + ", the avarage number of times we're expected to pass state " + end + " during this game:");
                outputTextBox.AppendText(Environment.NewLine);

                double result = currentGame.Markov1_5(start, end);

                outputTextBox.AppendText("Estimated Avg. times state " + end + " will be visited if we're in state " + start + ": " + result);
                outputTextBox.AppendText(Environment.NewLine);
            }
        }

        //Meghívja a Markov2Correct függvényt, és amennyiben igaz értéket kap vissza,
        //a kapott paraméterekkel a currentGame Markov2 függvényével elemzi,
        //hogy egy játékállapotból kiindulva mennyi eséllyel fejezzük be a játékot az egyes végállapotokban.
        private void markov2Button_Click(object sender, EventArgs e)
        {
            int start = -1;
            if (Markov2Correct(ref start))
            {
                outputTextBox.AppendText(Environment.NewLine);
                outputTextBox.AppendText("Chance to be in the end states if currently in game state: " + start);
                outputTextBox.AppendText(Environment.NewLine);

                int[] endStates;

                double[] result = currentGame.Markov2(start, out endStates);

                for (int i = 0; i < result.Length; i++)
                {
                    outputTextBox.AppendText("Chance to be in end state " + endStates[i].ToString() + ": " + result[i].ToString());
                    outputTextBox.AppendText(Environment.NewLine);
                }

            }
        }



        //A paramétereknek értékül adja mcSimulateTimesBox és mcSimulateMaxStepsBox tartalmait és
        //igaz értéket ad vissza, amennyiben azok megfelelnek a feladat követelményeinek.
        //(amennyiben nem, hibaüzenetet ír a ShowErrorMessage metódussal és hamis értéket ad vissza)
        private bool SimulateCorrect(ref int times, ref int maxSteps)
        {
            //simulate times number check (min 1)
            if (int.TryParse(mcSimulateTimesBox.Text, out times))
            {
                if (times <= 0)
                {
                    ShowErrorMessage("Sim times must be a positive number!");
                    return false;
                }
            }
            else
            {
                ShowErrorMessage("Sim times must be a positive number!");
                return false;
            }

            //max steps check (min 1)
            if (int.TryParse(mcSimulateMaxStepsBox.Text, out maxSteps))
            {
                if (maxSteps <= 0)
                {
                    ShowErrorMessage("Max steps must be a positive number!");
                    return false;
                }
            }
            else
            {
                ShowErrorMessage("Max steps must be a positive number!");
                return false;
            }

            return true;
        }

        //A paramétereknek értékül adja markov1StartBox és markov1TimesBox tartalmait és
        //igaz értéket ad vissza, amennyiben azok megfelelnek a feladat követelményeinek.
        //(amennyiben nem, hibaüzenetet ír a ShowErrorMessage metódussal és hamis értéket ad vissza)
        private bool MarkovCorrect(ref int start, ref int times)
        {
            //simulate start state check (min 0, less than gameState.Count)
            if (int.TryParse(markov1StartBox.Text, out start))
            {
                if (start < 0 || start >= currentGame.gameStates.Count)
                {
                    ShowErrorMessage("Start state not valid!");
                    return false;
                }
            }
            else
            {
                ShowErrorMessage("Start state must be a number!");
                return false;
            }


            //simulate times number check (min 1)
            if (int.TryParse(markov1TimesBox.Text, out times))
            {
                if (times <= 0)
                {
                    ShowErrorMessage("Sim times must be a positive number!");
                    return false;
                }
            }
            else
            {
                ShowErrorMessage("Sim times must be a positive number!");
                return false;
            }

            return true;

        }

        //A paramétereknek értékül adja markov1_5StartBox és markov1_5EndBox tartalmait és
        //igaz értéket ad vissza, amennyiben azok megfelelnek a feladat követelményeinek.
        //(amennyiben nem, hibaüzenetet ír a ShowErrorMessage metódussal és hamis értéket ad vissza)
        private bool Markov1_5Correct(ref int start, ref int end)
        {
            //simulate start state check (min 0, less than gameState.Count)
            if (int.TryParse(markov1_5StartBox.Text, out start))
            {
                if (start < 0 || start >= currentGame.gameStates.Count)
                {
                    ShowErrorMessage("Start state not valid!");
                    return false;
                }
                else
                {
                    if (currentGame.isEndStateIndex(start))
                    {
                        ShowErrorMessage("Start is an end state!");
                        return false;
                    }
                }
            }
            else
            {
                ShowErrorMessage("Start state must be a number!");
                return false;
            }

            //simulate end state check (min 0, less than gameState.Count)
            if (int.TryParse(markov1_5EndBox.Text, out end))
            {
                if (end < 0 || end >= currentGame.gameStates.Count)
                {
                    ShowErrorMessage("End state not valid!");
                    return false;
                }
                else
                {
                    if (currentGame.isEndStateIndex(end))
                    {
                        ShowErrorMessage("End can't be an actual end state!");
                        return false;
                    }
                }
            }
            else
            {
                ShowErrorMessage("End state must be a number!");
                return false;
            }

            return true;
        }

        //A paramétereknek értékül adja markov2StartBox tartalmát és
        //igaz értéket ad vissza, amennyiben az megfelel a feladat követelményeinek.
        //(amennyiben nem, hibaüzenetet ír a ShowErrorMessage metódussal és hamis értéket ad vissza)
        private bool Markov2Correct(ref int start)
        {
            //simulate start state check (min 0, less than gameState.Count)
            if (int.TryParse(markov2StartBox.Text, out start))
            {
                if (start < 0 || start >= currentGame.gameStates.Count)
                {
                    ShowErrorMessage("Start state not valid!");
                    return false;
                }
                else
                {
                    if (currentGame.isEndStateIndex(start))
                    {
                        ShowErrorMessage("Start is an end state!");
                        return false;
                    }
                }
            }
            else
            {
                ShowErrorMessage("Start state must be a number!");
                return false;
            }

            return true;

        }



        //A megadott hibaüzenetet jeleníti meg az errorBox szövegdobozban.
        private void ShowErrorMessage(String errorDescription)
        {
            errorBox.AppendText(Environment.NewLine);
            errorBox.AppendText("ERROR! " + errorDescription);
        }

        //Visszaállítja az outputTextBox tartalmát a kezdeti értékére.
        private void clearOutputButton_Click(object sender, EventArgs e)
        {
            outputTextBox.Clear();
            outputTextBox.AppendText("Output:");
            outputTextBox.AppendText(Environment.NewLine);
        }
        
    }
}
