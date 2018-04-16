using System;
using System.Windows.Forms;

namespace BFszakdolgozat
{
    public partial class KNaVMain : Form
    {
        private KNaVOptions originForm;  //Az a Form amelyről ezt az ablakot elértük, a visszalépésnél ide kerülünk vissza.
        private KiNevetAVegen currentGame;  //A műveletek végrehajtásához a konstruktor paraméterei alapján lérehozott KiNevetAVegen osztály példány.
        private int figuresA;  //Az 1. (A) játékos figuráinak száma.
        private int figuresB;  //Az 2. (B) játékos figuráinak száma.
        private int boardLength;  //A körpálya hossza (pozitív páros szám).
        private int bonusSteps;  //A körút megtétele után hátralévő lépések száma.
        private bool playerStarts;  //Igaz, ha az 1. (A) játékos kezd, Hamis, ha a 2. (B) játékos kezd.
        private int currentRollResult;  //A játék játszása során használt változó, amiben minden dobása után a felhasználó (1. játékos) által dobott eredményt tároljuk.


        //Megadja az értéket az originForm-nak, figuresA-nak, figuresB-nek, boardLength-nek, bonusStepts-nek, és a playerStarts-nak.
        //A megadott paraméterek alapján inicializálja a currentGame KiNevetAVegen játékot.
        //Végül magát a form-ot inicializálja és kitölti az infoTextBox-ot.
        public KNaVMain(KNaVOptions origin, int boardLength, int bonusSteps, int figuresA, int figuresB, ref int[] diceArray, bool playerStarts, int inputEnemyTactic)
        {
            originForm = origin;
            this.figuresA = figuresA;
            this.figuresB = figuresB;
            this.boardLength = boardLength;
            this.bonusSteps = bonusSteps;
            this.playerStarts = playerStarts;
            currentGame = new KiNevetAVegen(boardLength, bonusSteps, figuresA, figuresB, ref diceArray, playerStarts, inputEnemyTactic);
            InitializeComponent();
            outputTextBox.AppendText(Environment.NewLine);

            infoTextBox.AppendText(Environment.NewLine);
            infoTextBox.AppendText(Environment.NewLine);
            //board
            infoTextBox.AppendText("Game Board:");
            infoTextBox.AppendText(Environment.NewLine);
            infoTextBox.AppendText("-> |");
            for(int i=1; i<=boardLength; i++)
            {
                infoTextBox.AppendText(" " + i +" |");
            }
            infoTextBox.AppendText(" ->");
            infoTextBox.AppendText(Environment.NewLine);
            infoTextBox.AppendText(Environment.NewLine);
            //other info

            infoTextBox.AppendText("Player 1 figures: " + figuresA);
            infoTextBox.AppendText(Environment.NewLine);
            infoTextBox.AppendText("Player 2 figures: " + figuresB);
            infoTextBox.AppendText(Environment.NewLine);
            infoTextBox.AppendText(Environment.NewLine);

            if (playerStarts)
            {
                infoTextBox.AppendText("Player 1 Starts");
            }
            else
            {
                infoTextBox.AppendText("Player 2 Starts");
            }
            
            infoTextBox.AppendText(Environment.NewLine);

            infoTextBox.AppendText("Player 2 follows Tactic " + inputEnemyTactic);
            infoTextBox.AppendText(Environment.NewLine);
            infoTextBox.AppendText(Environment.NewLine);

            infoTextBox.AppendText("Player 1 Start Base (S1) is connected to Square 1");
            infoTextBox.AppendText(Environment.NewLine);
            infoTextBox.AppendText("Player 2 Start Base (S2) is connected to Square " + ((boardLength/2)+1).ToString());
            infoTextBox.AppendText(Environment.NewLine);
            infoTextBox.AppendText(Environment.NewLine);

            infoTextBox.AppendText("Player 1 End Base (E1) is connected to Square " + boardLength.ToString());
            infoTextBox.AppendText(Environment.NewLine);
            infoTextBox.AppendText("Player 2 End Base (E2) is connected to Square " + (boardLength/2).ToString());
            infoTextBox.AppendText(Environment.NewLine);
            infoTextBox.AppendText(Environment.NewLine);

            infoTextBox.AppendText("Each End Base has  " + (bonusSteps-1).ToString() + " bonus squares leading up to it (B1_1, B1_2, ...)");
            infoTextBox.AppendText(Environment.NewLine);
            infoTextBox.AppendText(Environment.NewLine);

            infoTextBox.AppendText("Note: When describing game states or deciding which figures to step with, the figures are always ordered. (closest to end - farthest to end)");
            infoTextBox.AppendText(Environment.NewLine);
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
                outputTextBox.AppendText(Environment.NewLine);
                AppendGameState(currentGame.gameStates[i]);
                outputTextBox.AppendText("And Next Step: Player 2");
                outputTextBox.AppendText(Environment.NewLine);
                outputTextBox.AppendText(Environment.NewLine);
            }

            for (int i = 0; i < currentGame.gameStates.Count; i++)
            {
                outputTextBox.AppendText("Game state number " + (currentGame.gameStates.Count + i).ToString() + ": ");
                outputTextBox.AppendText(Environment.NewLine);
                AppendGameState(currentGame.gameStates[i]);
                outputTextBox.AppendText("And Next Step: Player 1");
                outputTextBox.AppendText(Environment.NewLine);
                outputTextBox.AppendText(Environment.NewLine);
            }
        }

        //Kiírja az összes lehetséges átmenetet, amivel egy játékesetből egy másikba juthatunk (a currentGame playGameGraph tömbjének felhasználásával) az outputTextBox szövegdobozba.
        private void showGraphButton_Click(object sender, EventArgs e)
        {
            outputTextBox.AppendText("---GAME GRAPH---");
            outputTextBox.AppendText(Environment.NewLine);

            for (int i = 0; i < currentGame.playGameGraph.Length; i++)
            {
                for (int j = 0; j < currentGame.playGameGraph[i].Count; j++)
                {
                    outputTextBox.AppendText("There exists a way from state " + i.ToString() + " to state number: " + currentGame.playGameGraph[i][j].ToString());
                    outputTextBox.AppendText(Environment.NewLine);
                }
            }
        }



        //Elindítja a játék játszását, a többi művelet gombját lezárja, és
        //megjeleníti a játékhoz használt stepButton, stopButton, figureStepButton, skipTurnButton gombokat és a figureSelectBox szövegdobozt.
        //Elindítja a játékot a playerStarts értékének megfelelő módon.
        private void playGameButton_Click(object sender, EventArgs e)
        {
            //disable other buttons and textboxes
            showStatesButton.Enabled = false;
            showGraphButton.Enabled = false;
            playGameButton.Enabled = false;
            mcSimulateButton.Enabled = false;
            mcSimulateTimesBox.Enabled = false;
            mcSimulateMaxStepsBox.Enabled = false;
            mcSimulateTacticBox.Enabled = false;
            markov1Button.Enabled = false;
            markov1StartBox.Enabled = false;
            markov1TimesBox.Enabled = false;
            markov1TacticBox.Enabled = false;
            markov1_5Button.Enabled = false;
            markov1_5StartBox.Enabled = false;
            markov1_5EndBox.Enabled = false;
            markov1_5TacticBox.Enabled = false;
            markov2Button.Enabled = false;
            markov2StartBox.Enabled = false;
            markov2TacticBox.Enabled = false;

            //reset graph
            currentGame.ResetGame();

            //enable and show step button
            //stepButton.Enabled = true;
            stopButton.Enabled = true;
            stepButton.Visible = true;
            stopButton.Visible = true;
            figureStepButton.Visible = true;
            figureSelectBox.Visible = true;
            skipTurnButton.Visible = true;

            //Enter text to output field
            outputTextBox.AppendText("---GAME START---");
            outputTextBox.AppendText(Environment.NewLine);

            //start state
            outputTextBox.AppendText("Starting game state: ");
            if (playerStarts)
            {
                outputTextBox.AppendText(Environment.NewLine);
                outputTextBox.AppendText("-PLAYER 1 TURN-");
                outputTextBox.AppendText(Environment.NewLine);
                outputTextBox.AppendText("Current State: ");
                outputTextBox.AppendText(Environment.NewLine);

                outputTextBox.AppendText("Player 1 Figures: |");
                for (int i = 0; i < figuresA; i++) { outputTextBox.AppendText(" S1 |"); }
                outputTextBox.AppendText(Environment.NewLine);

                outputTextBox.AppendText("Player 2 Figures: |");
                for (int i = 0; i < figuresB; i++) { outputTextBox.AppendText(" S2 |"); }
                outputTextBox.AppendText(Environment.NewLine);
                outputTextBox.AppendText(Environment.NewLine);

                //wait for player to roll
                outputTextBox.AppendText("Waiting for player roll...");
                outputTextBox.AppendText(Environment.NewLine);
                stepButton.Enabled = true;
                
            }
            else
            {
                enemyStep();
            }
        }


        //Játékos végrehajt egy dobást a currentGame PlayerRoll függvényével, ezt kiírja az outputTextBox-ba,
        //majd elérhetővé teszi a figureStepButton, figureSelectBox, és skipTurnButton elemeket.
        private void stepButton_Click(object sender, EventArgs e)
        {
            currentRollResult = currentGame.PlayerRoll();
            outputTextBox.AppendText("Player 1 rolled: " + currentRollResult);
            outputTextBox.AppendText(Environment.NewLine);
            outputTextBox.AppendText("Waiting for figure choice...");
            outputTextBox.AppendText(Environment.NewLine);
            stepButton.Enabled = false;
            figureStepButton.Enabled = true;
            figureSelectBox.Enabled = true;
            skipTurnButton.Enabled = true;
        }

        //Megpróbálja léptetni a játékos által a figureSelectBox-ban megadott figurát (a dobás már korábban megtörtént).
        //Amennyiben sikertelen a lépés, ShowErrorMessage metódus használatával hibát jelzi a hibát.
        //Sikeres lépés végén meghívja az enemyStep metódust.
        private void figureStepButton_Click(object sender, EventArgs e)
        {
            int target;
            if (int.TryParse(figureSelectBox.Text, out target))
            {
                if (target <= 0 || target > figuresA)
                {
                    ShowErrorMessage("Figure doesn't exist!");
                }
                else
                {
                    int[] newState = currentGame.PlayRound(target-1, currentRollResult);
                    if (newState == null)
                    {
                        ShowErrorMessage("Not a valid step!");
                    }
                    else
                    {
                        figureStepButton.Enabled = false;
                        skipTurnButton.Enabled = false;
                        figureSelectBox.Text = "Step with which figure?";
                        figureSelectBox.Enabled = false;

                        outputTextBox.AppendText("Choose to step with figure: " + target);
                        outputTextBox.AppendText(Environment.NewLine);
                        outputTextBox.AppendText(Environment.NewLine);

                        //Game end Check
                        if (!GameEndedCheck(ref newState, true))
                        {
                            //enemy step
                            enemyStep();
                        }
                        else
                        {
                            outputTextBox.AppendText(Environment.NewLine);
                            outputTextBox.AppendText("-GAME OVER - PLAYER 1 WON-");
                            outputTextBox.AppendText(Environment.NewLine);
                            outputTextBox.AppendText("Ending State: ");
                            outputTextBox.AppendText(Environment.NewLine);
                            AppendGameState(newState);
                            outputTextBox.AppendText("--------------------------");
                            outputTextBox.AppendText(Environment.NewLine);
                        }
                    }
                }
            }
            else
            {
                ShowErrorMessage("Figures have to be identified by numbers!");
            }
        }

        //Lépés nélkül véget vet a játékos a körének (currentGame PlayerSkipTurn metódusával).
        //Majd meghívja az enemyStep metódust.
        private void skipTurnButton_Click(object sender, EventArgs e)
        {
            figureStepButton.Enabled = false;
            skipTurnButton.Enabled = false;
            figureSelectBox.Text = "Step with which figure?";
            figureSelectBox.Enabled = false;
            outputTextBox.AppendText("Chose to skip turn.");

            currentGame.PlayerSkipTurn();
            
            outputTextBox.AppendText(Environment.NewLine);
            outputTextBox.AppendText(Environment.NewLine);
            enemyStep();
        }

        //Végrehajtja az ellenfél (2. játékos) lépését a játék játszásánál,
        //a currentGame TakeStep függvényét használva.
        private void enemyStep()
        {
            outputTextBox.AppendText(Environment.NewLine);
            outputTextBox.AppendText("-PLAYER 2 TURN-");
            outputTextBox.AppendText(Environment.NewLine);
            outputTextBox.AppendText("Current State: ");
            outputTextBox.AppendText(Environment.NewLine);
            AppendGameState(currentGame.gameStates[currentGame.currentPlayStateIndex - currentGame.gameStates.Count]);

            int[] newState = currentGame.TakeStep();

            outputTextBox.AppendText("Player 2 rolled: " + currentGame.LastRollResult());
            outputTextBox.AppendText(Environment.NewLine);
            outputTextBox.AppendText("Player 2 steps.");
            outputTextBox.AppendText(Environment.NewLine);
            outputTextBox.AppendText(Environment.NewLine);

            if (!GameEndedCheck(ref newState, false))
            {
                stepButton.Enabled = true;
                outputTextBox.AppendText("-PLAYER 1 TURN-");
                outputTextBox.AppendText(Environment.NewLine);
                outputTextBox.AppendText("Current State: ");
                outputTextBox.AppendText(Environment.NewLine);
                AppendGameState(newState);
            }
            else
            {
                outputTextBox.AppendText(Environment.NewLine);
                outputTextBox.AppendText("-GAME OVER - PLAYER 2 WON-");
                outputTextBox.AppendText(Environment.NewLine);
                outputTextBox.AppendText("Ending State: ");
                outputTextBox.AppendText(Environment.NewLine);
                AppendGameState(newState);
                outputTextBox.AppendText("--------------------------");
                outputTextBox.AppendText(Environment.NewLine);
            }
        }

        //Leállítja a játékot az EndGame metódus meghívásával.
        private void stopButton_Click(object sender, EventArgs e)
        {
            EndGame();
            outputTextBox.AppendText(Environment.NewLine);
            outputTextBox.AppendText("---GAME ENDED BY USER---");
            outputTextBox.AppendText(Environment.NewLine);
        }

        //Eldönti a jelenlegi játékállapotról, hogy a játék végállapota-e és amennyiben végállapot,
        //automatikusan kiírja az outputTextBox-ba, hogy vége a játéknak és meghívja az EndGame metódust.
        private bool GameEndedCheck(ref int[] state, bool isPlayerA)
        {
            bool ended = true;

            for(int i=0; i<figuresA; i++)
            {
                if(state[i] != boardLength + bonusSteps)
                {
                    ended = false;
                    break;
                }
            }

            if (ended)
            {
                EndGame();
                return true;
            }

            ended = true;

            for (int i = figuresA; i < state.Length; i++)
            {
                if (state[i] != boardLength + bonusSteps)
                {
                    ended = false;
                    break;
                }
            }

            if (ended)
            {
                EndGame();
                return true;
            }
            else
            {
                return false;
            }
        }

        //A játék játszásához használt gombokat (stepButton, stopButton, figureStepButton, skipTurnButton) és szövegdobozt (figureSelectBox) újra elrejti,
        //majd a többi művelet gombjait és szövegdobozait újra elérhetővé teszi.
        private void EndGame()
        {
            //disable and hide step and stop game button
            stepButton.Enabled = false;
            stopButton.Enabled = false;
            stepButton.Visible = false;
            stopButton.Visible = false;
            figureStepButton.Enabled = false;
            figureSelectBox.Enabled = false;
            figureStepButton.Visible = false;
            figureSelectBox.Visible = false;
            skipTurnButton.Enabled = false;
            skipTurnButton.Visible = false;

            //enable other buttons and textboxes
            showStatesButton.Enabled = true;
            showGraphButton.Enabled = true;
            playGameButton.Enabled = true;
            mcSimulateButton.Enabled = true;
            mcSimulateTimesBox.Enabled = true;
            mcSimulateMaxStepsBox.Enabled = true;
            mcSimulateTacticBox.Enabled = true;
            markov1Button.Enabled = true;
            markov1StartBox.Enabled = true;
            markov1TimesBox.Enabled = true;
            markov1TacticBox.Enabled = true;
            markov1_5Button.Enabled = true;
            markov1_5StartBox.Enabled = true;
            markov1_5EndBox.Enabled = true;
            markov1_5TacticBox.Enabled = true;
            markov2Button.Enabled = true;
            markov2StartBox.Enabled = true;
            markov2TacticBox.Enabled = true;
        }




        //Meghívja a SimulateCorrect függvényt, és amennyiben igaz értéket kap vissza,
        //Monte Carlo szimulációt hajt végre a függvény által értéket kapott paraméterekkel
        //a currentGame MonteCarloSimulation függvényével.
        private void mcSimulateButton_Click(object sender, EventArgs e)
        {
            int times = 0;
            int maxSteps = 0;
            int tactic = 0;
            if(SimulateCorrect(ref times, ref maxSteps, ref tactic))
            {
                int[] result = currentGame.MonteCarloSimulation(times, maxSteps, tactic);

                outputTextBox.AppendText(Environment.NewLine);
                outputTextBox.AppendText("Monte Carlo Simulation Results (Ran: " + times.ToString() + " times, Max Total Steps Allowed: " + maxSteps.ToString() + " Player 1 using tactic: " + tactic + ")");
                outputTextBox.AppendText(Environment.NewLine);

                outputTextBox.AppendText("Times Player 1 Won: " + result[1].ToString());
                outputTextBox.AppendText(Environment.NewLine);

                outputTextBox.AppendText("Times Player 2 Won: " + result[2].ToString());
                outputTextBox.AppendText(Environment.NewLine);

                outputTextBox.AppendText("Times Where the game didn't complete within the Max Total Steps limit: " + result[0].ToString());
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
            int tactic = 0;
            int start = -1;
            if (MarkovCorrect(ref start ,ref times, ref tactic))
            {
                outputTextBox.AppendText(Environment.NewLine);
                outputTextBox.AppendText("Chance to be in each game state after " + times + " steps if currently in game state: " + start + ", if using tactic " + tactic);
                outputTextBox.AppendText(Environment.NewLine);

                double[] result = currentGame.Markov1(start, times, tactic);

                for (int i = 0; i < result.Length; i++)
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
            int tactic = 0;
            int start = -1;
            int end = -1;
            if (Markov1_5Correct(ref start, ref end, ref tactic))
            {
                outputTextBox.AppendText(Environment.NewLine);
                outputTextBox.AppendText("If we're in state " + start + ", the avarage number of times we're expected to pass state " + end + " during this game: (if using tactic " + tactic + ")");
                outputTextBox.AppendText(Environment.NewLine);
                
                double result = currentGame.Markov1_5(start, end, tactic);

                outputTextBox.AppendText("Estimated Avg. times state " + end + " will be visited if we're in state " + start + ": " + result);
                outputTextBox.AppendText(Environment.NewLine);
                
            }
        }

        //Meghívja a Markov2Correct függvényt, és amennyiben igaz értéket kap vissza,
        //a kapott paraméterekkel a currentGame Markov2 függvényével elemzi,
        //hogy egy játékállapotból kiindulva mennyi eséllyel fejezzük be a játékot az egyes végállapotokban.
        private void markov2Button_Click(object sender, EventArgs e)
        {
            int tactic = 0;
            int start = -1;
            if (Markov2Correct(ref start, ref tactic))
            {
                outputTextBox.AppendText(Environment.NewLine);
                outputTextBox.AppendText("Chance to be in the end states if currently in game state: " + start + ", if using tactic " + tactic);
                outputTextBox.AppendText(Environment.NewLine);

                int[] endStates;

                double[] result = currentGame.Markov2(start, tactic, out endStates);

                for (int i = 0; i < result.Length; i++)
                {
                    outputTextBox.AppendText("Chance to be in game state " + endStates[i].ToString() + " (end state): " + result[i].ToString());
                    outputTextBox.AppendText(Environment.NewLine);
                }

            }
        }



        //A paramétereknek értékül adja mcSimulateTimesBox, mcSimulateMaxStepsBox, és mcSimulateTacticBox tartalmait és
        //igaz értéket ad vissza, amennyiben azok megfelelnek a feladat követelményeinek.
        //(amennyiben nem, hibaüzenetet ír a ShowErrorMessage metódussal és hamis értéket ad vissza)
        private bool SimulateCorrect(ref int times, ref int maxSteps, ref int tactic)
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

            //tactics check
            if (int.TryParse(mcSimulateTacticBox.Text, out tactic))
            {
                if (tactic <= 0 || tactic > 4)
                {
                    ShowErrorMessage("Tactic must be a positive number from 1 to 4!");
                    return false;
                }
            }
            else
            {
                ShowErrorMessage("Tactic must be given as a number!");
                return false;
            }

            return true;
        }

        //A paramétereknek értékül adja markov1StartBox markov1TimesBox és markov1TacticBox tartalmait és
        //igaz értéket ad vissza, amennyiben azok megfelelnek a feladat követelményeinek.
        //(amennyiben nem, hibaüzenetet ír a ShowErrorMessage metódussal és hamis értéket ad vissza)
        private bool MarkovCorrect(ref int start, ref int times, ref int tactic)
        {
            //simulate start state check (min 0, less than gameState.Count*2)
            if (int.TryParse(markov1StartBox.Text, out start))
            {
                if (start < 0 || start >= currentGame.gameStates.Count*2)
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

            //tactics check
            if (int.TryParse(markov1TacticBox.Text, out tactic))
            {
                if (tactic <= 0 || tactic > 3)
                {
                    ShowErrorMessage("Tactic must be a positive number from 1 to 3!");
                    return false;
                }
            }
            else
            {
                ShowErrorMessage("Tactic must be given as a number!");
                return false;
            }

            return true;

        }

        //A paramétereknek értékül adja markov1_5StartBox markov1_5EndBox, és markov1_5TacticBox tartalmait és
        //igaz értéket ad vissza, amennyiben azok megfelelnek a feladat követelményeinek.
        //(amennyiben nem, hibaüzenetet ír a ShowErrorMessage metódussal és hamis értéket ad vissza)
        private bool Markov1_5Correct(ref int start, ref int end, ref int tactic)
        {
            //simulate start state check (min 0, less than gameState.Count*2)
            if (int.TryParse(markov1_5StartBox.Text, out start))
            {
                if (start < 0 || start >= currentGame.gameStates.Count * 2)
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

            //simulate end state check (min 0, less than gameState.Count*2)
            if (int.TryParse(markov1_5EndBox.Text, out end))
            {
                if (end < 0 || end >= currentGame.gameStates.Count * 2)
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

            //tactics check
            if (int.TryParse(markov1_5TacticBox.Text, out tactic))
            {
                if (tactic <= 0 || tactic > 3)
                {
                    ShowErrorMessage("Tactic must be a positive number from 1 to 3!");
                    return false;
                }
            }
            else
            {
                ShowErrorMessage("Tactic must be given as a number!");
                return false;
            }

            return true;
        }

        //o	A paramétereknek értékül adja markov2StartBox és markov2TacticBox tartalmait és
        //igaz értéket ad vissza, amennyiben azok megfelelnek a feladat követelményeinek.
        //(amennyiben nem, hibaüzenetet ír a ShowErrorMessage metódussal és hamis értéket ad vissza)
        private bool Markov2Correct(ref int start, ref int tactic)
        {
            //simulate start state check (min 0, less than gameState.Count*2)
            if (int.TryParse(markov2StartBox.Text, out start))
            {
                if (start < 0 || start >= currentGame.gameStates.Count * 2)
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

            //tactics check
            if (int.TryParse(markov2TacticBox.Text, out tactic))
            {
                if (tactic <= 0 || tactic > 3)
                {
                    ShowErrorMessage("Tactic must be a positive number from 1 to 3!");
                    return false;
                }
            }
            else
            {
                ShowErrorMessage("Tactic must be given as a number!");
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

        //A megadott játékállapotot kiírja az outputTextBox szövegdobozba.
        private void AppendGameState(int[] gameState)
        {
            String output = "Player 1 Figure Positions: |";

            for (int j = 0; j < figuresA; j++)
            {
                if (gameState[j] == 0)
                {
                    output += " S1 |";
                }
                else if (gameState[j] == bonusSteps + boardLength)
                {
                    output += " E1 |";
                }
                else if (gameState[j] > boardLength)
                {
                    output += " B1_" + (gameState[j] - boardLength).ToString() + " |";
                }
                else
                {
                    output += " " + gameState[j].ToString() + " |";
                }
            }

            String output2 = "Player 2 Figure Positions: |";


            for (int j = gameState.Length-1; j >= figuresA; j--)
            {
                if (gameState[j] == 0)
                {
                    output2 += " S2 |";
                }
                else if (gameState[j] == bonusSteps + boardLength)
                {
                    output2 += " E2 |";
                }
                else if (gameState[j] > boardLength)
                {
                    output2 += " B2_" + (gameState[j] - boardLength).ToString() + " |";
                }
                else if (gameState[j] > (boardLength / 2))
                {
                    output2 += " " + (gameState[j] - (boardLength / 2)).ToString() + " |";
                }
                else
                {
                    output2 += " " + (gameState[j] + (boardLength / 2)).ToString() + " |";
                }
            }

            outputTextBox.AppendText(output);
            outputTextBox.AppendText(Environment.NewLine);
            outputTextBox.AppendText(output2);
            outputTextBox.AppendText(Environment.NewLine);
            outputTextBox.AppendText(Environment.NewLine);
        }

    }
}
