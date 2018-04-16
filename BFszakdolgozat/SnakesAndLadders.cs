using System.Collections.Generic;

namespace BFszakdolgozat
{
    public class SnakesAndLadders
    {
        private Dice dicePool;  //Játék által használt kockákat tartalmazó példány a Dice osztálynak.
        public List<int[]> gameStates;  //Lehetséges játékállapotokat tartalmazó lista. A játékállapotokat 1 hosszú integer tömbökként ábrázoljuk.
        public List<int>[] gameGraph;  //Tömb, amely az egyes játékállapotok közötti átmeneteket tárolja. Minden lista az egyik állapotból lehetséges összes átmenetet tárolja, az elérésükhöz szükséges dobás szerint növekvő sorrendben.

        private Graph actualGraph;  //A játék szimulálásához használt példánya a Graph osztálynak.
        private Markov actualMarkov;  //A játék elemzéséhez használt példánya a Markov osztálynak.



        //Default konstruktor, amely egy előre megadott Kígyók és Létrák játékot hoz létre.
        public SnakesAndLadders()
        {
            int boardLength = 100;
            List<int[]> ladders = new List<int[]>();
            List<int[]> snakes = new List<int[]>();

            int[] s1 = { 20, 11 };
            int[] s2 = { 34, 1 };
            int[] s3 = { 53, 32 };
            int[] s4 = { 60, 50 };
            int[] s5 = { 88, 42 };
            snakes.Add(s1);
            snakes.Add(s2);
            snakes.Add(s3);
            snakes.Add(s4);
            snakes.Add(s5);

            int[] l1 = { 2, 10 };
            int[] l2 = { 17, 30 };
            int[] l3 = { 36, 64 };
            int[] l4 = { 58, 62 };
            int[] l5 = { 70, 86 };
            ladders.Add(l1);
            ladders.Add(l2);
            ladders.Add(l3);
            ladders.Add(l4);
            ladders.Add(l5);

            dicePool = new Dice();

            int[] helpArray = new int[boardLength];

            CreateHelpArray(ref helpArray, ref ladders, ref snakes);

            gameStates = new List<int[]>();
            CreateStates(ref helpArray);

            gameGraph = new List<int>[gameStates.Count];
            CreateGraph(ref helpArray, 6);

            

            actualGraph = new Graph(ref gameStates, ref gameGraph, 0, ref dicePool);

            actualMarkov = null;
        }

        //Alternatív konstruktor, amely lehetővé teszi tetszőleges kockák, játéktábla hossz, kígyók és létrák megadását.
        //A konstruktor ezen kívül rendezi a snakes és ladders listákat kezdőpozíció szerint növekvő sorrendbe.
        public SnakesAndLadders(ref int[] diceArray, int boardLength, ref List<int[]> ladders, ref List<int[]> snakes)
        {
            dicePool = new Dice(ref diceArray);
            int possibleRolls = dicePool.MaxRoll() - dicePool.MinRoll() + 1;

            int[] helpArray = new int[boardLength];

            //order ladders
            for(int i=0; i<ladders.Count-1; i++)
            {
                int min = i;
                for(int j=i+1; j<ladders.Count; j++)
                {
                    if(ladders[j][0] < ladders[min][0])
                    {
                        min = j;
                    }
                }
                if(min != i)
                {
                    int[] swi = ladders[i];
                    ladders[i] = ladders[min];
                    ladders[min] = swi;
                }
            }

            //order snakes
            for (int i = 0; i < snakes.Count-1; i++)
            {
                int min = i;
                for (int j = i + 1; j < snakes.Count; j++)
                {
                    if (snakes[j][0] < snakes[min][0])
                    {
                        min = j;
                    }
                }
                if (min != i)
                {
                    int[] swi = snakes[i];
                    snakes[i] = snakes[min];
                    snakes[min] = swi;
                }
            }


            CreateHelpArray(ref helpArray, ref ladders, ref snakes);

            gameStates = new List<int[]>();
            CreateStates(ref helpArray);

            gameGraph = new List<int>[gameStates.Count];

            CreateGraph(ref helpArray, possibleRolls);

            

            actualGraph = new Graph(ref gameStates, ref gameGraph, 0, ref dicePool);

            actualMarkov = null;
        }




        //Segéd metódus, amit a konstruktorok használnak a lokális helpArray értékeinek beállítására.
        //A helpArray a tábla összes mezőjéhez tárolja, hogy arra a mezőre lépre, melyik mezőről indíthatjuk a következő lépést.
        //(Ami persze megegyezik a mezővel, ha nem indul onnan kígyó vagy létra.)
        private void CreateHelpArray(ref int[] helpArray, ref List<int[]> ladders, ref List<int[]> snakes)
        {
            if(helpArray == null)
            {
                //error
            }
            else
            {
                int ladderInd = 0;
                int snakeInd = 0;

                for (int i = 0; i < helpArray.Length; i++)
                {
                    if(ladderInd < ladders.Count && snakeInd < snakes.Count)
                    {
                        if (ladders[ladderInd][0] == i)
                        {
                            helpArray[i] = ladders[ladderInd][1];
                            ladderInd++;
                        }
                        else if (snakes[snakeInd][0] == i)
                        {
                            helpArray[i] = snakes[snakeInd][1];
                            snakeInd++;
                        }
                        else
                        {
                            helpArray[i] = i;
                        }
                    }
                    else
                    {
                        if(ladderInd < ladders.Count)
                        {
                            if (ladders[ladderInd][0] == i)
                            {
                                helpArray[i] = ladders[ladderInd][1];
                                ladderInd++;
                            }
                            else
                            {
                                helpArray[i] = i;
                            }
                        }
                        else if(snakeInd < snakes.Count)
                        {
                            if (snakes[snakeInd][0] == i)
                            {
                                helpArray[i] = snakes[snakeInd][1];
                                snakeInd++;
                            }
                            else
                            {
                                helpArray[i] = i;
                            }
                        }
                        else
                        {
                            helpArray[i] = i;
                        }
                    }
                }
            }
        }

        //Metódus, amit a konstruktorokban hívunk meg, gameStates értékeit állítja be a korábban a beállított helpArray segítségével.
        private void CreateStates(ref int[] helpArray)
        {
            for (int i = 0; i < helpArray.Length; i++)
            {
                if (helpArray[i] == i)
                {
                    int[] newArray = new int[1];
                    newArray[0] = i;
                    gameStates.Add(newArray);
                }
            }

        }

        //Segéd függvény, amit a CreateGraph metódus használ, megadja  a paraméterként megadott mezőszámhoz tartozó gameStates beli indexet.
        private int GetState(int squareNumber)
        {
            if (gameStates != null)
            {
                for(int i=0; i<gameStates.Count; i++)
                {
                    if(gameStates[i][0] == squareNumber)
                    {
                        return i;
                    }
                }
            }
            
            //error!
            return -1;
        }

        //Függvény, ami egy gameStates-beli indexre visszaadja, hogy végállapot-e.
        public bool isEndStateIndex(int stateIndex)
        {
            if (stateIndex == (gameStates.Count - 1))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Metódus, amit a konstruktorokban hívunk meg, gameGraph értékeit állítja be. (Amennyiben a gameStates már be van állítva.)
        private void CreateGraph(ref int[] helpArray, int possibleRolls)
        {
            if (gameStates == null)
            {
                //error
            }
            else
            {
                int minRoll = dicePool.MinRoll();
                for (int i = 0; i < gameStates.Count - 1; i++)
                {
                    gameGraph[i] = new List<int>();
                    int currentPos = helpArray[gameStates[i][0]];
                    int newPos;
                    for (int j = 0; j < possibleRolls; j++)
                    {
                        newPos = currentPos + minRoll + j;
                        if (newPos < helpArray.Length)
                        {
                            gameGraph[i].Add(GetState(helpArray[newPos]));
                        }
                        else
                        {
                            gameGraph[i].Add(GetState(helpArray[helpArray.Length - 1]));
                        }

                    }
                }
                gameGraph[gameStates.Count - 1] = new List<int>();
            }
        }

        //Metódus, ami ténylegesen létrehozza az elemző Markov osztály példányát.
        public void CreateMarkov()
        {
            //create transition matrix from the game graph + dice pool -> use it to initialize the actualMarkov
            double[,] trMat = new double[gameStates.Count, gameStates.Count];
            double[] odds = dicePool.GetOdds();

            for (int i = 0; i < gameGraph.Length; i++)
            {
                for (int j = 0; j < gameGraph[i].Count; j++)
                {
                    trMat[i, gameGraph[i][j]] += odds[j];
                }

                if (gameGraph[i].Count == 0)
                {
                    trMat[i, i] = 1.0;
                }

            }

            actualMarkov = new Markov(ref trMat);
        }



        //Metódus, amin keresztül a jelenleg Graph osztályon belül zajló szimulációt le tudjuk állítani.
        public void ResetGame()
        {
            actualGraph.Reset();
        }

        //Függvény, ami lépteti a szimulációt, majd visszaadja a jelenlegi játékállapotot.
        public int[] TakeStep()
        {
            int[] newState = null;

            if (actualGraph.Step())
            {
                newState = actualGraph.GetGameState();
            }

            return newState;
        }

        //Függvény, ami egy lehetséges játékállapotról  visszaadja, hogy végállapot-e.
        //(0: nem végállapot, 1: végállapot, -1: error)
        public int EndGameCheck(ref int[] currentState)
        {
            if (currentState == null)
            {
                //unexpected end
                return -1;
            }

            if (currentState[0] == gameStates[gameStates.Count-1][0])
            {
                //Player Won
                return 1;
            }
            else
            {
                //Not ending state
                return 0;
            }
        }

        //Függvény, ami visszaadja a dicePool legutolsó dobásának eredményét.
        public int LastRollResult()
        {
            return dicePool.GetLastRollResult();
        }



        //Függvény, ami visszaadja a játék sokszori futtatásának eredményét, egy maxSteps+1 elemszámú tömbként,
        //amelyben a tömb n. elemének értéke:
        // n = 0 : Hány olyan eset volt, ahol a játék nem fejeződött be maxSteps lépésen belül.
        // n>0 : Hány olyan eset volt, ahol a játék pontosan n eset alatt fejeződött be.
        public int[] MonteCarloSimulation(int numberOfTests, int maxSteps)
        {
            int[] results = new int[maxSteps+1];
            
            for (int i = 0; i < numberOfTests; i++)
            {
                actualGraph.Reset();

                bool ended = false;

                for (int j = 0; j <= maxSteps && !ended; j++)
                {
                    if (!actualGraph.Step())
                    {
                        ended = true;   //Step() returns false if we reached the end, meaning no further steps are possible
                        results[j]++;
                    }
                }

                if (!ended)
                {
                    results[0]++;
                }
            }

            return results;
        }


        //Függvény, ami létrehozza a játékhoz tartozó Markov osztály példányt, amennyiben az még nem létezett,
        //majd Markov osztály transitionMatrix változójának másolatát (steps-1)-ik hatványára emeli.
        //Ennek adja vissza a startingStateIndex állapotnak megfelelő sorát
        //(Emlékeztető: Markov létrehozásakor a sorok és oszlopok sorrendje változott!)
        public double[] Markov1(int startingStateIndex, int steps)
        {
            double[] resultVector = new double[gameStates.Count];

            if (actualMarkov == null)
            {
                CreateMarkov();
            }

            double[,] currentMatrix = new double[gameStates.Count, gameStates.Count];
            for (int i = 0; i < gameStates.Count; i++)
            {
                currentMatrix[i, i] = 1.0;
            }


            for (int i = 0; i < steps; i++)
            {
                currentMatrix = Markov.MultiplyMatrices(ref currentMatrix, ref actualMarkov.transitionMatrix);
            }

            //try to find where our starting state ended up after the markov class switched rows and columns
            int startingStateNewIndex = -1;
            for (int i = 0; i < gameStates.Count; i++)
            {
                if (actualMarkov.originalOrder[i] == startingStateIndex)
                {
                    startingStateNewIndex = i;
                    break;
                }
            }

            for (int i = 0; i < gameStates.Count; i++)
            {
                resultVector[actualMarkov.originalOrder[i]] = currentMatrix[startingStateNewIndex, i];
            }

            return resultVector;
        }


        //Függvény, ami létrehozza a játékhoz tartozó Markov osztály példányt, amennyiben az még nem létezett,
        //majd Markov osztály GetFundamentalMatrix által visszaadott mátrixának a startingState állapotnak megfelelő sorának az otherState állapotnak megfelelő elemét adja vissza.
        //(Emlékeztető: Markov létrehozásakor a sorok és oszlopok sorrendje változott!)
        public double Markov1_5(int startingState, int otherState)
        {
            if (actualMarkov == null)
            {
                CreateMarkov();
            }

            double[,] result = actualMarkov.GetFundamentalMatrix();

            if (result == null)
            {
                //error
                return 0;
            }

            int newStart = -1;
            int newOther = -1;

            for (int i = 0; i < actualMarkov.originalOrder.Length; i++)
            {
                if (actualMarkov.originalOrder[i] == startingState)
                {
                    newStart = i;
                }

                if (actualMarkov.originalOrder[i] == otherState)
                {
                    newOther = i;
                }
            }

            return result[newStart, newOther];
        }


        //Függvény, ami létrehozza a játékhoz tartozó Markov osztály példányt, amennyiben az még nem létezett,
        //majd Markov osztály GetEndStateChance által visszaadott tömböt adja vissza, az endStates-nek értékül adva a végállapotok eredeti indexeit.
        //(Emlékeztető: Markov létrehozásakor a sorok és oszlopok sorrendje változott!)
        public double[] Markov2(int startingState, out int[] endStates)
        {
            if (actualMarkov == null)
            {
                CreateMarkov();
            }

            double[] resultArray = actualMarkov.GetEndStateChance(startingState);

            if (resultArray == null)
            {
                //error
                endStates = null;
                return null;
            }

            endStates = new int[resultArray.Length];

            for (int i = 0; i < resultArray.Length; i++)
            {
                endStates[i] = actualMarkov.originalOrder[actualMarkov.absorbingStatesStart + i];
            }

            return resultArray;
        }

    }
}

