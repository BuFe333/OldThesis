using System;
using System.Collections.Generic;
using System.Linq;

namespace BFszakdolgozat
{
    public class Penney
    {
        private Dice dicePool;  //Játék által használt kockákat tartalmazó példány a Dice osztálynak.
        public List<int[]> gameStates;  //Lehetséges játékállapotokat tartalmazó lista. A játékállapotokat 2 hosszú integer tömbökként ábrázoljuk.
        private int[] gameStateBreakpoints;  //Ennek a tömbnek az n. eleme azt tárolja, hogy a gameStates lista hányadik elemétől kezdődnekk azok a játékállapotok, ahol a tömb 0. eleme legalább n.
        public List<int>[] gameGraph;  //o	Tömb, amely az egyes játékállapotok közötti átmeneteket tárolja. Minden lista az egyik állapotból lehetséges összes átmenetet tárolja, az elérésükhöz szükséges dobás szerint növekvő sorrendben.

        private Graph actualGraph;  //A játék szimulálásához használt példánya a Graph osztálynak.
        private Markov actualMarkov;  //A játék elemzéséhez használt példánya a Markov osztálynak.

        private int[] patternA;  //Az „A” játékos által használt lista. (A konstruktorban a kettő megadott lista közül az első)
        int patternACode;  //A patternA beli listához tartozó játékállapot gameStates-beli tömbjének 1 helyén álló érték
        int patternAModulo;  //D^k (ahol D legyen az összes különböző összértékű dobás, k pedig a patternA hossza)
        private int[] patternB;  //Az „B” játékos által használt lista. (A konstruktorban a kettő megadott lista közül az második)
        int patternBCode;  //A patternB beli listához tartozó játékállapot gameStates-beli tömbjének 1 helyén álló érték
        int patternBModulo;  //D^k (ahol D legyen az összes különböző összértékű dobás, k pedig a patternB hossza)



        //Default konstruktor, amely egy egy elöre megadott Penney’s game játékot hoz létre.
        public Penney()
        {
            int[] defaultDice = new int[1];
            defaultDice[0] = 2;
            dicePool = new Dice(ref defaultDice);

            patternA = new int[3];
            patternA[0] = 0;
            patternA[1] = 0;
            patternA[2] = 1;

            patternACode = 1;
            patternAModulo = 8;

            patternB = new int[3];
            patternB[0] = 0;
            patternB[1] = 1;
            patternB[2] = 0;

            patternBCode = 2;
            patternBModulo = 8;

            gameStates = new List<int[]>();
            gameStateBreakpoints = new int[4];

            CreateStates(0, 3, 2);

            CreateGraph(2);

            actualGraph = new Graph(ref gameStates,ref gameGraph, 0,ref dicePool);

            actualMarkov = null;
        }

        //Alternatív konstruktor, amely lehetővé teszi tetszőleges kockák és játékosok által használt listák megadását.
        public Penney(ref int[] diceArray, ref int[] inputPatternA, ref int[] inputPatternB)
        {
            dicePool = new Dice(ref diceArray);
            int possibleRolls = dicePool.MaxRoll() - dicePool.MinRoll() + 1;

            patternA = inputPatternA;
            patternACode = 0;
            patternAModulo = 1;
            for(int i= patternA.Length-1; i>=0; i--)
            {
                patternACode += patternA[patternA.Length - 1 - i] * patternAModulo;
                patternAModulo *= possibleRolls;
            }

            patternB = inputPatternB;
            patternBCode = 0;
            patternBModulo = 1;
            for (int i = patternB.Length - 1; i >= 0; i--)
            {
                patternBCode += patternB[patternB.Length - 1 - i] * patternBModulo;
                patternBModulo *= possibleRolls;
            }


            int maxLength = patternA.Length;
            if (patternB.Length > maxLength)
            {
                maxLength = patternB.Length;
            }

            gameStates = new List<int[]>();
            gameStateBreakpoints = new int[maxLength + 1];
            

            CreateStates(0, maxLength, possibleRolls);

            CreateGraph(possibleRolls);

            actualGraph = new Graph(ref gameStates, ref gameGraph, 0, ref dicePool);

            actualMarkov = null;
        }




        //Rekurzív metódus, amit a konstruktorokban hívunk meg, gameStates értékeit állítja be.
        private void CreateStates(int currentLength, int maxLength, int possibleRolls)
        {
            gameStateBreakpoints[currentLength] = gameStates.Count;

            int target = 1;
            for(int i=0; i<currentLength; i++)
            {
                target *= possibleRolls;
            }

            for(int i=0; i<target; i++)
            {
                int[] newState = new int[2];
                newState[0] = currentLength;
                newState[1] = i;
                gameStates.Add(newState);
            }

            if (currentLength < maxLength)
            {
                CreateStates(currentLength + 1, maxLength, possibleRolls);
            }
        }

        //Függvény, ami egy gameStates-beli indexre visszaadja, hogy végállapot-e.
        public bool isEndStateIndex(int stateIndex)
        {
            if ((((gameStates[stateIndex][1] % patternAModulo) == patternACode) && (gameStates[stateIndex][0] >= patternA.Length)) || (((gameStates[stateIndex][1] % patternBModulo) == patternBCode) && (gameStates[stateIndex][0] >= patternB.Length)))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        //Metódus, amit a konstruktorokban hívunk meg, gameGraph értékeit állítja be. (Amennyiben a gameStates már be van állítva.)
        private void CreateGraph(int possibleRolls)
        {
            if(gameStates == null)
            {
                //error
            }else
            {
                gameGraph = new List<int>[gameStates.Count];

                for (int i=0; i < gameStates.Count; i++)
                {
                    gameGraph[i] = new List<int>();

                    //check if the state we are currently in is an ending state for the game
                    if((((gameStates[i][1] % patternAModulo)  != patternACode) || (gameStates[i][0] < patternA.Length)) && (((gameStates[i][1] % patternBModulo) != patternBCode) || (gameStates[i][0] < patternB.Length)))
                    {
                        //if not, then proceed
                        if (gameStates[i][0] < gameStateBreakpoints.Length - 1)
                        {
                            int newStart = gameStateBreakpoints[gameStates[i][0] + 1] + gameStates[i][1] * possibleRolls;
                            for (int j = 0; j < possibleRolls; j++)
                            {
                                gameGraph[i].Add(newStart + j);
                            }
                        }
                        else
                        {
                            int newStart = gameStateBreakpoints[gameStates[i][0]] + ((gameStates[i][1] * possibleRolls) % (gameStates.Last()[1] + 1));
                            for (int j = 0; j < possibleRolls; j++)
                            {
                                gameGraph[i].Add(newStart + j);
                            }
                        }
                    }
                }
            }
        }

        //Metódus, ami ténylegesen létrehozza az elemző Markov osztály példányát.
        private void CreateMarkov()
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

        //Függvény, ami egy lehetséges játékállapotról  visszaadja, hogy végállapot-e, és ha végállapot, akkor melyik játékos nyert.
        //(0: nem végállapot, 1: patternA nyert, 2: patternB nyert, -1: error)
        public int EndGameCheck(ref int[] currentState)
        {
            if(currentState == null)
            {
                //unexpected end
                return -1;
            }

            if (((currentState[1] % patternBModulo) == patternBCode) && (currentState[0] >= patternB.Length))
            {
                //Player B Won
                return 2;
            }
            else if(((currentState[1] % patternAModulo) == patternACode) && (currentState[0] >= patternA.Length))
            {
                //Player A Won
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



        //Függvény, ami visszaadja a játék sokszori futtatásának eredményét egy 3 elemű tömbként. 
        //0. elem -> hányszor volt, hogy a játék nem fejeződött be a maximális lépésszám elérése előtt.
        //1. elem -> hányszor volt, hogy a játékot a patternA-hez tartozó játékos nyerte.
        //2. elem -> hányszor volt, hogy a játékot a patternB-hez tartozó játékos nyerte.
        public int[] MonteCarloSimulation(int numberOfTests, int maxSteps)
        {
            int[] results = new int[3]; // 0 : game unfinished ; 1 : pattern A won ; 2 : pattern B won

            results[0] = 0;
            results[1] = 0;
            results[2] = 0;

            for (int i = 0; i < numberOfTests; i++)
            {
                actualGraph.Reset();

                bool ended = false;

                for (int j = 0; j < maxSteps && !ended; j++)
                {
                    if (!actualGraph.Step())
                    {
                        ended = true;   //Step() returns false if we reached the end, meaning no further steps are possible
                    }
                }

                if (ended)
                {
                    int[] currentState = actualGraph.GetGameState();
                    if((currentState[1] % patternAModulo) == patternACode)
                    {
                        results[1]++;
                    } else if((currentState[1] % patternBModulo) == patternBCode)
                    {
                        results[2]++;
                    } else
                    {
                        //if the program is unable to decide for whatever reason, increment results[0]
                        results[0]++;
                    }
                }
                else
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
            //check if too big?

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
            for(int i=0; i<gameStates.Count; i++)
            {
                if(actualMarkov.originalOrder[i] == startingStateIndex)
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
            //check if too big?

            if (actualMarkov == null)
            {
                CreateMarkov();
            }

            double[,] result = actualMarkov.GetFundamentalMatrix();

            if (result == null)
            {
                //error
                //Console.WriteLine("ERROR in Markov1_5!");
                return 0.0;
            }

            int newStart=-1;
            int newOther=-1;

            for (int i = 0; i < actualMarkov.originalOrder.Length; i++)
            {
                if(actualMarkov.originalOrder[i] == startingState)
                {
                    newStart = i;
                }

                if (actualMarkov.originalOrder[i] == otherState)
                {
                    newOther = i;
                }
            }

            return result[newStart,newOther];
        }


        //Függvény, ami létrehozza a játékhoz tartozó Markov osztály példányt, amennyiben az még nem létezett,
        //majd Markov osztály GetEndStateChance által visszaadott tömböt adja vissza, az endStates-nek értékül adva a végállapotok eredeti indexeit.
        //(Emlékeztető: Markov létrehozásakor a sorok és oszlopok sorrendje változott!)
        public double[] Markov2(int startingState, out int[] endStates)
        {
            //check if too big?

            if (actualMarkov == null)
            {
                CreateMarkov();
            }

            double[] resultArray = actualMarkov.GetEndStateChance(startingState);

            if (resultArray == null)
            {
                //error
                Console.WriteLine("ERROR in Markov2!");
                endStates = null;
                return null;
            }

            endStates = new int[resultArray.Length];

            for (int i=0; i<resultArray.Length; i++)
            {
                endStates[i] = actualMarkov.originalOrder[actualMarkov.absorbingStatesStart + i];
            }

            return resultArray;
        }

    }
}
