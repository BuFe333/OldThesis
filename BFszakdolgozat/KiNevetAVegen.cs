using System;
using System.Collections.Generic;

namespace BFszakdolgozat
{
    public class KiNevetAVegen
    {
        private Dice dicePool;  //Játék által használt kockákat tartalmazó példány a Dice osztálynak.
        public List<int[]> gameStates;  //Lehetséges játékállapotokat tartalmazó lista. A játékállapotokat figuresA+figuresB hosszú integer tömbként ábrázoljuk.

        public List<int>[] playGameGraph;  //A játék játszásához használt gráf. Az első felében (ami az 1. játékos lépéseihez tartozó játékállapotokat tartalmazza), minden lehetséges lépés el van tárolva, míg a 2. játékos lépései a megadott taktikának megfelelőek.
        public List<int>[] analysisGameGraph;  //A játék szimulálásához és elemzéséhez használt gráf. Mindkét játékos lépései a megadott taktikáknak megfelelőek.

        int boardLength;  //Megadott tábla hossz.
        int bonusSteps;  //Tábla végének elérése után hány lépést kell még a figuráknak lépniük a célba beérés előtt.

        public int currentPlayStateIndex;  //A játék játszása alatt a jelenlegi állapot indexe
        bool playerStarts;  //Igaz, ha az 1. játékos kezdi a játékot, és hamis, ha a 2. játékos kezd

        int enemyTactic;  //A 2. játékos által használt taktika száma.
        int lastTacticUsed;  //A legutóbb létrehozott analysisGameGraph-hoz használt 1. játékos taktika

        int figuresA;  //Az "A" (1.) játékos figuráinak száma
        int figuresB;  //A "B" (2.) játékos figuráinak száma

        private Graph playGraph;  //A játék játszásához használt példánya a Graph osztálynak.
        private Graph analysisGraph;  //A játék Monte-Carlo szimulációjánál használt példánya a Graph osztálynak.
        private Markov analysisMarkov;  //A játék elemzéséhez használt példánya a Markov osztálynak.



        //Default konstruktor, amely egy előre megadott Ki Nevet a Végén játékot hoz létre.
        public KiNevetAVegen()
        {
            boardLength = 4;
            bonusSteps = 2;

            figuresA = 2;
            figuresB = 2;
            int[] diceArray = new int[1];
            diceArray[0] = 2;
            dicePool = new Dice(ref diceArray);
            playerStarts = true;
            enemyTactic = 1;

            CreateStates();

            for (int i = 0; i < gameStates.Count; i++)
            {
                Console.WriteLine("Game State " + (i + 1));
                for (int j = 0; j < gameStates[i].Length; j++)
                {
                    Console.Write(gameStates[i][j] + "|");
                }
                Console.WriteLine();
                Console.WriteLine();
            }

            CreatePlayGraph();

            currentPlayStateIndex = playerStarts ? 0 : gameStates.Count;

            playGraph = new Graph(ref gameStates, ref playGameGraph, currentPlayStateIndex, ref dicePool);
        }

        //Alternatív konstruktor, amely lehetővé teszi tetszőleges kockák, … megadását.
        public KiNevetAVegen(int boardLength, int bonusSteps, int figuresA, int figuresB, ref int[] diceArray, bool playerStarts, int inputEnemyTactic)
        {
            this.boardLength = boardLength;
            this.bonusSteps = bonusSteps;
            this.figuresA = figuresA;
            this.figuresB = figuresB;
            dicePool = new Dice(ref diceArray);
            this.playerStarts = playerStarts;
            
            enemyTactic = inputEnemyTactic;

            CreateStates();

            CreatePlayGraph();

            currentPlayStateIndex = playerStarts ? 0 : gameStates.Count;

            playGraph = new Graph(ref gameStates, ref playGameGraph, currentPlayStateIndex, ref dicePool);
        }




        //Rekurzív metódus, ami a gameStates elemeinek létrehozásánál használt.
        private void HelpCreateStatesLeft(ref int[] currentNewState, int currentFigNum)
        {
            if(currentFigNum != figuresA)
            {
                if(currentFigNum == 0)
                {
                    for(int i=0; i<=boardLength+bonusSteps; i++)
                    {
                        currentNewState[0] = i;
                        HelpCreateStatesLeft(ref currentNewState, currentFigNum + 1);
                    }
                }
                else
                {
                    for (int i = 0; i < currentNewState[currentFigNum - 1] || (boardLength + bonusSteps == i && boardLength + bonusSteps == currentNewState[currentFigNum - 1]) || (0 == i && 0 == currentNewState[currentFigNum - 1]); i++)
                    {
                        currentNewState[currentFigNum] = i;
                        HelpCreateStatesLeft(ref currentNewState, currentFigNum + 1);
                    }
                }
            }
            else
            {
                HelpCreateStatesRight(ref currentNewState, figuresA+figuresB-1);
            }
        }

        //Rekurzív metódus, ami a gameStates elemeinek létrehozásánál használt.
        private void HelpCreateStatesRight(ref int[] currentNewState, int currentFigNum)
        {
            if (currentFigNum != figuresA-1)
            {
                if (currentFigNum == figuresA + figuresB - 1)
                {
                    for (int i = 0; i <= boardLength + bonusSteps; i++)
                    {
                        currentNewState[figuresA + figuresB - 1] = i;
                        //if it doesn't conflict with other player's figures
                        bool ok = true;
                        if(i <= boardLength / 2 && i != 0)
                        {
                            for (int j = 0; j < figuresA; j++)
                            {
                                if (currentNewState[j]-(boardLength/2) == i)
                                {
                                    ok = false;
                                    break;
                                }
                            }
                        }else if(i <= boardLength && i != 0)
                        {
                            for(int j=0; j< figuresA; j++)
                            {
                                if(currentNewState[j]+(boardLength/2) == i)
                                {
                                    ok = false;
                                    break;
                                }
                            }
                        }
                        
                        if(ok)
                        {
                            HelpCreateStatesRight(ref currentNewState, currentFigNum - 1);
                        }

                    }
                }
                else
                {
                    for (int i = 0; i < currentNewState[currentFigNum + 1] || (boardLength + bonusSteps == i && boardLength + bonusSteps == currentNewState[currentFigNum + 1]) || (0 == i && 0 == currentNewState[currentFigNum + 1]); i++)
                    {
                        currentNewState[currentFigNum] = i;
                        //if it doesn't conflict with other player's figures
                        bool ok = true;
                        if (i <= boardLength / 2 && i != 0)
                        {
                            for (int j = 0; j < figuresA; j++)
                            {
                                if (currentNewState[j] - (boardLength / 2) == i)
                                {
                                    ok = false;
                                    break;
                                }
                            }
                        }
                        else if (i <= boardLength && i != 0)
                        {
                            for (int j = 0; j < figuresA; j++)
                            {
                                if (currentNewState[j] + (boardLength / 2) == i)
                                {
                                    ok = false;
                                    break;
                                }
                            }
                        }

                        if (ok)
                        {
                            HelpCreateStatesRight(ref currentNewState, currentFigNum - 1);
                        }

                    }
                }
            }
            else
            {
                int[] trueNewState = new int[figuresA + figuresB];
                for(int i=0; i < trueNewState.Length; i++)
                {
                    trueNewState[i] = currentNewState[i];
                }

                //both players can't win at the same time
                if(trueNewState[figuresA-1] != boardLength+bonusSteps || trueNewState[figuresA] != boardLength + bonusSteps)
                {
                    gameStates.Add(trueNewState);
                }               
            }
        }

        //Metódus, ami létrehozza a gameStates elemeit.
        private void CreateStates()
        {
            gameStates = new List<int[]>();
            int[] newState = new int[figuresA+figuresB];

            HelpCreateStatesLeft(ref newState, 0);
        }



        //Függvény, ami egy játékállás helyét keresi meg a gameStates listában.
        private int SearchForState(ref int[] searchFor)
        {
            for (int i = 0; i < gameStates.Count; i++)
            {
                bool ok = true;
                for (int j = 0; j < gameStates[i].Length; j++)
                {
                    if (gameStates[i][j] != searchFor[j])
                    {
                        ok = false;
                        break;
                    }
                }

                if (ok)
                {
                    return i;
                }
            }
            return -1;
        }

        //Függvény, ami visszaadja annak a játékhelyzetnek az indexét, amelyet azzal érünk el, ha a megadott currentState állapotból rollResult dobás után a figureAttempt figurát próbáljuk léptetni. (-1-et ad vissza amennyiben ez a lépés nem lehetséges)
        private int AttemptStepPlayer(int currentState, int figureAttempt, int rollResult, bool isPlayerA)
        {
            int[] newState = new int[figuresA + figuresB];
            for(int i=0; i<newState.Length; i++)
            {
                newState[i] = gameStates[currentState][i];
            }

            //FAIL if we tried to step from an end position
            if (newState[figureAttempt] == boardLength + bonusSteps)
            {
                return -1;
            }
            
            //stepping from the starting position
            if(newState[figureAttempt] == 0)
            {
                newState[figureAttempt] = 1;

                //FAIL if we tried to step from the starting position, but we didn't roll maximum
                if (rollResult != dicePool.MaxRoll())
                {
                    return -1;
                }
            }
            else
            {
                newState[figureAttempt] += rollResult;
            }
            
            //if we overstepped the end, we still reached it - NOT USING THAT OPTIMAL RULE
            if(newState[figureAttempt] > boardLength + bonusSteps)
            {
                newState[figureAttempt] = boardLength + bonusSteps;
            }

            if (isPlayerA)
            {
                //if we stepped into a square with an opponent's square - reset that figure
                for (int i = figuresA + figuresB - 1; i >= figuresA; i--)
                {
                    if (newState[i] <= boardLength && newState[i] != 0 && (((newState[i] > boardLength / 2) && (newState[i] - boardLength / 2 == newState[figureAttempt]))) || ((newState[i] <= boardLength / 2) && (newState[i] == newState[figureAttempt] - boardLength / 2)))
                    {
                        newState[i] = 0;
                        //preform switches until order is correct
                        for (int j = i - 1; j >= figuresA; j--)
                        {
                            if (newState[j] > newState[j + 1])
                            {
                                int swi = newState[j];
                                newState[j] = newState[j + 1];
                                newState[j + 1] = swi;
                            }
                        }

                        break;
                    }
                }

                //get the state order right again
                for (int i = figureAttempt - 1; i >= 0; i--)
                {
                    //FAIL if we stepped into the same square as one of our other figures
                    if (newState[i + 1] == newState[i] && newState[i] != boardLength + bonusSteps)
                    {
                        return -1;
                    }
                    if (newState[i] < newState[i + 1])
                    {
                        int swi = newState[i];
                        newState[i] = newState[i + 1];
                        newState[i + 1] = swi;
                    }
                }
            }
            else
            {
                //if we stepped into a square with an opponent's square - reset that figure
                for (int i = 0; i < figuresA; i++)
                {
                    if (newState[i] <= boardLength && newState[i] != 0 && (((newState[i] > boardLength / 2) && (newState[i] - boardLength / 2 == newState[figureAttempt]))) || ((newState[i] <= boardLength / 2) && (newState[i] == newState[figureAttempt] - boardLength / 2)))
                    {
                        newState[i] = 0;
                        //preform switches until order is correct
                        for (int j = i + 1; j < figuresA; j++)
                        {
                            if (newState[j] > newState[j - 1])
                            {
                                int swi = newState[j];
                                newState[j] = newState[j - 1];
                                newState[j - 1] = swi;
                            }
                        }

                        break;
                    }
                }

                //get the state order right again
                for (int i = figureAttempt + 1; i < figuresA+figuresB; i++)
                {
                    //FAIL if we stepped into the same square as one of our other figures
                    if (newState[i - 1] == newState[i] && newState[i] != boardLength + bonusSteps)
                    {
                        return -1;
                    }
                    if (newState[i] < newState[i - 1])
                    {
                        int swi = newState[i];
                        newState[i] = newState[i - 1];
                        newState[i - 1] = swi;
                    }
                }
            }

            int returnIndex = SearchForState(ref newState);

            if (isPlayerA  && returnIndex != -1) { returnIndex += gameStates.Count; }

            //search for state and return it
            return returnIndex;
        }

        //Függvény, ami egy gameStates-beli indexre visszaadja, hogy végállapot-e.
        public bool isEndStateIndex(int stateIndex)
        {
            if(gameStates[stateIndex % gameStates.Count][figuresA-1] == boardLength+bonusSteps || gameStates[stateIndex % gameStates.Count][figuresA] == boardLength + bonusSteps)
            {
                return true;
            }
            return false; 
        }


        //Függvény, ami visszaadja annak a játékhelyzetnek az indexét, amelyet azzal érünk el, ha a megadott currentState állapotból rollResult dobás után a megadott taktikát követve lépünk.
        private int GetGraphConnectionForTactic(int tactic, int curentStatePos, int rollResult, bool isPlayerA)
        {
            //Tactic 1: Step with the furthest figure (that hasn't reached the end yet)
            if(tactic == 1)
            {
                int destination = -1;

                if (isPlayerA)
                {
                    int i = 0;
                    while(i<figuresA && destination == -1)
                    {
                        destination = AttemptStepPlayer(curentStatePos, i, rollResult, isPlayerA);
                        i++;
                    }
                }
                else
                {
                    int i = figuresA+figuresB-1;
                    while(i>=figuresA && destination == -1)
                    {
                        destination = AttemptStepPlayer(curentStatePos, i, rollResult, isPlayerA);
                        i--;
                    }
                }

                //If you have no means to preform a step, SKIP your turn
                if(destination == -1)
                {
                    if (isPlayerA)
                    {
                        destination = curentStatePos + gameStates.Count;
                    }
                    else
                    {
                        destination = curentStatePos;
                    }
                }

                return destination;
            }

            //Tactic 2: Step with the figure closest to the start (that is able to make a legal move)
            if (tactic == 2)
            {
                int destination = -1;

                if (isPlayerA)
                {
                    int i = figuresA-1;
                    while (i >= 0 && destination == -1)
                    {
                        destination = AttemptStepPlayer(curentStatePos, i, rollResult, isPlayerA);
                        i--;
                    }
                }
                else
                {
                    int i = figuresA;
                    while (i < figuresA + figuresB && destination == -1)
                    {
                        destination = AttemptStepPlayer(curentStatePos, i, rollResult, isPlayerA);
                        i++;
                    }
                }

                //If you have no means to preform a step, SKIP your turn
                if (destination == -1)
                {
                    if (isPlayerA)
                    {
                        destination = curentStatePos + gameStates.Count;
                    }
                    else
                    {
                        destination = curentStatePos;
                    }
                }

                return destination;
            }

            //Tactic 3: Step with the furthest figure (that hasn't reached the end yet) - BUT! Prioritize taking enemy figures
            if (tactic == 3)
            {
                int[] destinations;
                bool[] figureTaken;

                if (isPlayerA)
                {
                    destinations = new int[figuresA];
                    figureTaken = new bool[figuresA];

                    for (int i=0; i < figuresA; i++)
                    {
                        destinations[i] = AttemptStepPlayer(curentStatePos, i, rollResult, isPlayerA);

                        //check if there was a figure taken from the enemy - player 2
                        if(destinations[i] != -1)
                        {
                            for (int j = figuresA; j < figuresA + figuresB; j++)
                            {
                                if(gameStates[curentStatePos][j] != gameStates[destinations[i] - gameStates.Count][j])
                                {
                                    figureTaken[i] = true;
                                    break;
                                }
                            }
                        }
                    }
                }
                else
                {
                    destinations = new int[figuresB];
                    figureTaken = new bool[figuresB];

                    for(int i = 0; i < figuresB; i++)
                    {
                        destinations[i] = AttemptStepPlayer(curentStatePos, figuresA + figuresB - 1 - i, rollResult, isPlayerA);

                        //check if there was a figure taken from the enemy - player 1
                        if (destinations[i] != -1)
                        {
                            for (int j = 0; j < figuresA; j++)
                            {
                                if (gameStates[curentStatePos][j] != gameStates[destinations[i]][j])
                                {
                                    figureTaken[i] = true;
                                    break;
                                }
                            }
                        }

                    }
                }

                int destination = -1;

                //try to pick a step that results in taking an enemy figure
                for(int i=0; i<destinations.Length; i++)
                {
                    if(figureTaken[i])
                    {
                        destination = destinations[i];
                        break;
                    }
                }

                //lacking such step, select the first good step (arrays are already ordered according to the tactics priorities)
                if(destination == -1)
                {
                    for (int i = 0; i < destinations.Length; i++)
                    {
                        if (destinations[i] > -1)
                        {
                            destination = destinations[i];
                            break;
                        }
                    }
                }


                //If you have no means to preform a step, SKIP your turn
                if (destination == -1)
                {
                    if (isPlayerA)
                    {
                        destination = curentStatePos + gameStates.Count;
                    }
                    else
                    {
                        destination = curentStatePos;
                    }
                }

                return destination;
            }

            //Tactic 4: Step with the figure closest to the start (that is able to make a legal move) - BUT! Prioritize taking enemy figures
            if (tactic == 4)
            {
                int[] destinations;
                bool[] figureTaken;

                if (isPlayerA)
                {
                    destinations = new int[figuresA];
                    figureTaken = new bool[figuresA];

                    for (int i = 0; i < figuresA; i++)
                    {
                        destinations[i] = AttemptStepPlayer(curentStatePos, figuresA - i, rollResult, isPlayerA);

                        //check if there was a figure taken from the enemy - player 2
                        if (destinations[i] != -1)
                        {
                            for (int j = figuresA; j < figuresA + figuresB; j++)
                            {
                                if (gameStates[curentStatePos][j] != gameStates[destinations[i] - gameStates.Count][j])
                                {
                                    figureTaken[i] = true;
                                    break;
                                }
                            }
                        }
                    }
                }
                else
                {
                    destinations = new int[figuresB];
                    figureTaken = new bool[figuresB];

                    for (int i = 0; i < figuresB; i++)
                    {
                        destinations[i] = AttemptStepPlayer(curentStatePos, figuresA + i, rollResult, isPlayerA);

                        //check if there was a figure taken from the enemy - player 1
                        if (destinations[i] != -1)
                        {
                            for (int j = 0; j < figuresA; j++)
                            {
                                if (gameStates[curentStatePos][j] != gameStates[destinations[i]][j])
                                {
                                    figureTaken[i] = true;
                                    break;
                                }
                            }
                        }

                    }
                }

                int destination = -1;

                //try to pick a step that results in taking an enemy figure
                for (int i = 0; i < destinations.Length; i++)
                {
                    if (figureTaken[i])
                    {
                        destination = destinations[i];
                        break;
                    }
                }

                //lacking such step, select the first good step (arrays are already ordered according to the tactics priorities)
                if (destination == -1)
                {
                    for (int i = 0; i < destinations.Length; i++)
                    {
                        if (destinations[i] > -1)
                        {
                            destination = destinations[i];
                            break;
                        }
                    }
                }


                //If you have no means to preform a step, SKIP your turn
                if (destination == -1)
                {
                    if (isPlayerA)
                    {
                        destination = curentStatePos + gameStates.Count;
                    }
                    else
                    {
                        destination = curentStatePos;
                    }
                }

                return destination;
            }

            return -1;
        }


        //Metódus, ami létrehozza a playGraph tömböt.
        private void CreatePlayGraph()
        {
            if (gameStates == null)
            {
                //error
            }
            else
            {
                playGameGraph = new List<int>[2 * gameStates.Count];    // 0->N-1: player A ; N->N-2: player B
                //player A will get to choose - we add all possible steps
                for (int i = 0; i < gameStates.Count; i++)
                {
                    playGameGraph[i] = new List<int>();
                    if (!isEndStateIndex(i))
                    {
                        //array for keeping track if there are any dice roll results where we have no valid steps availiable
                        bool[] goodRoll = new bool[dicePool.MaxRoll()-dicePool.MinRoll()+1];
                        for (int k = 0; k < goodRoll.Length; k++)
                        {
                            goodRoll[k] = false;
                        }

                        for (int k = 0; k < figuresA; k++)
                        {
                            for (int j = dicePool.MinRoll(); j <= dicePool.MaxRoll(); j++)
                            {
                                int newStatePos = AttemptStepPlayer(i, k, j, true);
                                //next player's turn!
                                if (newStatePos != -1)
                                {
                                    newStatePos += gameStates.Count;
                                }
                                playGameGraph[i].Add(newStatePos);

                                if (newStatePos != -1)
                                {
                                    goodRoll[j-dicePool.MinRoll()] = true;
                                }
                            }
                        }

                        //for dicerolls where all options are invalid - SKIP your turn
                        for (int k = 0; k < goodRoll.Length; k++)
                        {
                            if (!goodRoll[k])
                            {
                                playGameGraph[i][k] = i + gameStates.Count;
                            }
                        }

                    }
                }

                //player B steps are determined by enemyTactic variable
                for (int i = gameStates.Count; i < playGameGraph.Length; i++)
                {
                    playGameGraph[i] = new List<int>();
                    if (!isEndStateIndex(i-gameStates.Count))
                    {
                        for(int j = dicePool.MinRoll(); j <= dicePool.MaxRoll(); j++)
                        {
                            playGameGraph[i].Add(GetGraphConnectionForTactic(enemyTactic, i - gameStates.Count, j, false));
                        }
                    }
                }
            }
        }

        //Metódus, ami a megadott taktika (és a playGraph) segítségével létrehozza az analysisGraph-ot.
        private void CreateAnalysisGraph(int tacticUsed)
        {
            if (playGameGraph == null)
            {
                //error
            }
            else
            {
                analysisGameGraph = new List<int>[2 * gameStates.Count];    // 0->N-1: player A ; N->N-2: player B

                for (int i=0; i<gameStates.Count; i++)
                {
                    analysisGameGraph[i] = new List<int>();
                    if (!isEndStateIndex(i))
                    {
                        for (int j = dicePool.MinRoll(); j <= dicePool.MaxRoll(); j++)
                        {
                            analysisGameGraph[i].Add(GetGraphConnectionForTactic(enemyTactic, i, j, true));
                        }
                    }
                }


                for (int i = gameStates.Count; i < playGameGraph.Length; i++)
                {
                    analysisGameGraph[i] = new List<int>();
                    for(int j=0; j<playGameGraph[i].Count; j++)
                    {
                        analysisGameGraph[i].Add(playGameGraph[i][j]);
                    }
                }

                lastTacticUsed = tacticUsed;
            }
        }


        //Metódus, ami a megadott taktika és az analysisGraph segítségével létrehozza az actualMarkov elemet.
        public void CreateMarkov(int tacticUsed)
        {
            //create transition matrix from the game graph + dice pool -> use it to initialize the actualMarkov

            double[,] trMat = new double[gameStates.Count * 2, gameStates.Count * 2];
            double[] odds = dicePool.GetOdds();

            //create the analyzeGameGraph array if needed
            if(lastTacticUsed != tacticUsed)
            {
                CreateAnalysisGraph(tacticUsed);

                int startFrom = playerStarts ? 0 : gameStates.Count;

                analysisGraph = new Graph(ref gameStates, ref analysisGameGraph, startFrom, ref dicePool);
            }

            for (int i = 0; i < analysisGameGraph.Length; i++)
            {
                for (int j = 0; j < analysisGameGraph[i].Count; j++)
                {
                    trMat[i, analysisGameGraph[i][j]] += odds[j];
                }

                if (analysisGameGraph[i].Count == 0)
                {
                    trMat[i, i] = 1.0;
                }

            }

            analysisMarkov = new Markov(ref trMat);
        }



        //Metódus, ami visszaállítja a kezdőállapotát a playGraph-nak.
        public void ResetGame()
        {
            playGraph.Reset();
            currentPlayStateIndex = playerStarts ? 0 : gameStates.Count;
        }


        public int[] PlayRound(int playerChoiceFromLastRound, int playerRollResult)
        {
            int newStateIndex = AttemptStepPlayer(currentPlayStateIndex, playerChoiceFromLastRound, playerRollResult, true);

            if(newStateIndex != -1)
            {
                playGraph.SetGameStateIndex(newStateIndex);
            }
            else
            {
                return null;
            }

            int[] returnState = (int[])playGraph.GetGameState().Clone();

            currentPlayStateIndex = playGraph.GetGameStateIndex();

            return returnState;
        }


        //Függvény, ami lépteti a szimulációt, majd visszaadja a jelenlegi játékállapotot.
        public int[] TakeStep()
        {
            if(playGraph != null)
            {
                int[] newState = null;

                if (playGraph.Step())
                {
                    newState = playGraph.GetGameState();
                }

                currentPlayStateIndex = playGraph.GetGameStateIndex();

                return newState;
            }
            else
            {
                //error
                return null;
            }
        }


        //Függvény, ami visszaadja a dicePool egy dobását.
        public int PlayerRoll()
        {
            return dicePool.Roll();
        }


        //Függvény, ami visszaadja a dicePool legutolsó dobásának eredményét.
        public int LastRollResult()
        {
            return dicePool.GetLastRollResult();
        }




        //Függvény, ami visszaadja a játék sokszori futtatásának eredményét egy 3 elemű tömbként. 
        //0. elem -> hányszor volt, hogy a játék nem fejeződött be a maximális lépésszám elérése előtt.
        //1. elem -> hányszor volt, hogy a játékot az 1. játékos nyerte.
        //2. elem -> hányszor volt, hogy a játékot a 2. játékos nyerte.
        public int[] MonteCarloSimulation(int numberOfTests, int maxSteps, int tacticUsed)
        {
            int[] results = new int[3]; // 0 : game unfinished ; 1 : pattern A won ; 2 : pattern B won

            results[0] = 0;
            results[1] = 0;
            results[2] = 0;

            //create the analyzeGameGraph array if needed
            if (lastTacticUsed != tacticUsed)
            {
                CreateAnalysisGraph(tacticUsed);

                //check if starting position is the one we wanted
                int startFrom = playerStarts ? 0 : gameStates.Count;

                analysisGraph = new Graph(ref gameStates, ref analysisGameGraph, startFrom, ref dicePool);
            }

            for (int i = 0; i < numberOfTests; i++)
            {
                analysisGraph.Reset();

                bool ended = false;

                for (int j = 0; j < maxSteps && !ended; j++)
                {
                    if (!analysisGraph.Step())
                    {
                        ended = true;   //Step() returns false if we reached the end, meaning no further steps are possible
                    }
                }

                if (ended)
                {
                    int[] currentState = analysisGraph.GetGameState();
                    bool AWon = true;
                    

                    for (int j=0; j<figuresA; j++)
                    {
                        if(currentState[j] != boardLength + bonusSteps)
                        {
                            AWon = false;
                        }
                    }


                    if (AWon)
                    {
                        results[1]++;
                    }
                    else
                    {
                        results[2]++;
                    }
                }
                else
                {
                    results[0]++;
                }
            }

            return results;
        }


        //Függvény, ami létrehozza a játékhoz és a tacticUsed-hoz tartozó Markov osztály példányt, amennyiben az még nem létezett,
        //majd Markov osztály transitionMatrix változójának másolatát (steps-1)-ik hatványára emeli.
        //Ennek adja vissza a startingStateIndex állapotnak megfelelő sorát
        //(Emlékeztető: Markov létrehozásakor a sorok és oszlopok sorrendje változott!)
        public double[] Markov1(int startingStateIndex, int steps, int tacticUsed)
        {

            double[] resultVector = new double[gameStates.Count * 2];

            if (analysisMarkov == null || lastTacticUsed != tacticUsed)
            {
                CreateMarkov(tacticUsed);
            }

            double[,] currentMatrix = new double[gameStates.Count * 2, gameStates.Count * 2];
            for (int i = 0; i < gameStates.Count * 2; i++)
            {
                currentMatrix[i, i] = 1.0;
            }


            for (int i = 0; i < steps; i++)
            {
                currentMatrix = Markov.MultiplyMatrices(ref currentMatrix, ref analysisMarkov.transitionMatrix);
            }

            //try to find where our starting state ended up after the markov class switched rows and columns
            int startingStateNewIndex = -1;
            for (int i = 0; i < gameStates.Count; i++)
            {
                if (analysisMarkov.originalOrder[i] == startingStateIndex)
                {
                    startingStateNewIndex = i;
                    break;
                }
            }

            for (int i = 0; i < gameStates.Count; i++)
            {
                resultVector[analysisMarkov.originalOrder[i]] = currentMatrix[startingStateNewIndex, i];
            }

            return resultVector;
        }


        //Függvény, ami létrehozza a játékhoz és a tacticUsed-hoz tartozó Markov osztály példányt, amennyiben az még nem létezett,
        //majd Markov osztály GetFundamentalMatrix által visszaadott mátrixának a startingState állapotnak megfelelő sorának az otherState állapotnak megfelelő elemét adja vissza.
        //(Emlékeztető: Markov létrehozásakor a sorok és oszlopok sorrendje változott!)
        public double Markov1_5(int startingState, int otherState, int tacticUsed)
        {

            if (analysisMarkov == null || lastTacticUsed != tacticUsed)
            {
                CreateMarkov(tacticUsed);
            }

            double[,] result = analysisMarkov.GetFundamentalMatrix();

            if (result == null)
            {
                //error
                return 0;
            }

            int newStart = -1;
            int newOther = -1;

            for (int i = 0; i < analysisMarkov.originalOrder.Length; i++)
            {
                if (analysisMarkov.originalOrder[i] == startingState)
                {
                    newStart = i;
                }

                if (analysisMarkov.originalOrder[i] == otherState)
                {
                    newOther = i;
                }
            }

            return result[newStart, newOther];
        }


        //Függvény, ami létrehozza a játékhoz és a tacticUsed-hoz tartozó Markov osztály példányt, amennyiben az még nem létezett,
        //majd Markov osztály GetEndStateChance által visszaadott tömböt adja vissza, az endStates-nek értékül adva a végállapotok eredeti indexeit.
        //(Emlékeztető: Markov létrehozásakor a sorok és oszlopok sorrendje változott!)
        public double[] Markov2(int startingState, int tacticUsed, out int[] endStates)
        {

            if (analysisMarkov == null || lastTacticUsed != tacticUsed)
            {
                CreateMarkov(tacticUsed);
            }

            double[] resultArray = analysisMarkov.GetEndStateChance(startingState);

            if (resultArray == null)
            {
                //error
                endStates = null;
                return null;
            }

            endStates = new int[resultArray.Length];

            for (int i = 0; i < resultArray.Length; i++)
            {
                endStates[i] = analysisMarkov.originalOrder[analysisMarkov.absorbingStatesStart + i];
            }

            return resultArray;
        }


        //Metódus, aminek segítségével a játék játszásánál átugorhatjuk a játékos lépését, a gráfon belüli pozíciót megfelelően állítva. (Amennyiben nincs a játékosnak valid lépése)
        public void PlayerSkipTurn()
        {
            currentPlayStateIndex = currentPlayStateIndex + gameStates.Count;
            playGraph.SetGameStateIndex(currentPlayStateIndex);
        }
    }
}
