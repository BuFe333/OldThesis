using System.Collections.Generic;

namespace BFszakdolgozat
{
    //Markov class is used to make predictions about games based on their transition matrix
    class Markov
    {
        public double[,] transitionMatrix;  //Markov lánc tranzíciós (átmeneti) mátrixa. A különböző játékállapotok közötti átmenetek valószínűségét tárolja.
        public int[] originalOrder;  //Tömb, amiben tároltuk, hogy a mi volt a transitionMatrix sorainak és oszlopainak sorrendje. (A tömb n-edik eleme tárolja, hogy a jelenleg n. sor/oszlop hányadik sor/oszlop volt az átrendezés előtt.)
        public int absorbingStatesStart;  //Tárolja, hogy a tömb hányadik indexű eleme az első végállapoté a transitionMatrix-ben.
        private double[,] fundamentalMatrix;  //A Markov lánc fundamentális mátrixa ((I-Q)-1). Mérete (átmenti állapotok száma X átmeneti állapotok száma)



        //konstruktor, ami egy előre elkészített (de még nem rendezett) tranzíciós mátrix (i. sor j. eleme = mennyi a valószínűsége, hogy az i. helyzetből egy lépéssel a j. helyzetbe jutunk), felhasználásával létrehozza a Markov osztályt.
        public Markov(ref double[,] inputTransitionMatrix)
        {
            if(inputTransitionMatrix.GetUpperBound(0) == inputTransitionMatrix.GetUpperBound(1))
            {
                int size = inputTransitionMatrix.GetUpperBound(0) + 1;

                transitionMatrix = new double[size,size];

                List<int> absorbingStates = new List<int>();
                originalOrder = new int[transitionMatrix.GetUpperBound(0) + 1];

                for (int i = 0; i <= inputTransitionMatrix.GetUpperBound(0); i++)
                {
                    //save the absorbing states' rows for later
                    if (inputTransitionMatrix[i, i] == 1.0)
                    {
                        absorbingStates.Add(i);
                    }
                }

                absorbingStatesStart = inputTransitionMatrix.GetUpperBound(0) + 1 - absorbingStates.Count;
                int offset = 0;

                for (int i = 0; i <= inputTransitionMatrix.GetUpperBound(0); i++)
                {
                    if (offset < absorbingStates.Count)
                    {
                        if (absorbingStates[offset] == i)
                        {
                            originalOrder[absorbingStatesStart + offset] = i;
                            offset++;
                        }
                        else
                        {
                            originalOrder[i - offset] = i;
                        }
                    }
                    else
                    {
                        originalOrder[i - offset] = i;
                    }

                }

                //switch rows and columns so that absorbing states are at the ends

                int offsetX = 0;
                for (int i=0; i<=inputTransitionMatrix.GetUpperBound(0); i++)
                {
                    int offsetY = 0;

                    if (offsetX < absorbingStates.Count)
                    {
                        if (absorbingStates[offsetX] == i)
                        {
                            for(int j=0; j<=inputTransitionMatrix.GetUpperBound(0); j++)
                            {
                                if(offsetY < absorbingStates.Count)
                                {
                                    if(absorbingStates[offsetY] == j)
                                    {
                                        transitionMatrix[absorbingStatesStart + offsetX, absorbingStatesStart + offsetY] = inputTransitionMatrix[i, j];
                                        offsetY++;
                                    }else
                                    {
                                        transitionMatrix[absorbingStatesStart + offsetX, j - offsetY] = inputTransitionMatrix[i, j];
                                    }
                                }else
                                {
                                    transitionMatrix[absorbingStatesStart + offsetX, j - offsetY] = inputTransitionMatrix[i, j];
                                }
                            }
                            offsetX++;
                        }
                        else
                        {
                            for (int j = 0; j <= inputTransitionMatrix.GetUpperBound(0); j++)
                            {
                                if (offsetY < absorbingStates.Count)
                                {
                                    if (absorbingStates[offsetY] == j)
                                    {
                                        transitionMatrix[i - offsetX, absorbingStatesStart + offsetY] = inputTransitionMatrix[i, j];
                                        offsetY++;
                                    }
                                    else
                                    {
                                        transitionMatrix[i - offsetX, j - offsetY] = inputTransitionMatrix[i, j];
                                    }
                                }
                                else
                                {
                                    transitionMatrix[i - offsetX, j - offsetY] = inputTransitionMatrix[i, j];
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int j = 0; j <= inputTransitionMatrix.GetUpperBound(0); j++)
                        {
                            if (offsetY < absorbingStates.Count)
                            {
                                if (absorbingStates[offsetY] == j)
                                {
                                    transitionMatrix[i - offsetX, absorbingStatesStart + offsetY] = inputTransitionMatrix[i, j];
                                    offsetY++;
                                }
                                else
                                {
                                    transitionMatrix[i - offsetX, j - offsetY] = inputTransitionMatrix[i, j];
                                }
                            }
                            else
                            {
                                transitionMatrix[i - offsetX, j - offsetY] = inputTransitionMatrix[i, j];
                            }
                        }
                    }
                }

                fundamentalMatrix = null;
            }
            else
            {
                //error
            }    
        }


        //A transitionMatrix és a fundamentalMatrix, értékeinek a használatával (amiben a GetFundamentalMatrix függvény segít) kiszámolja azt, hogy ha a játékban éppen a paraméterben (eredeti indexeléssel) megadott játékállapotban (ami nem lehet végállapot) vagyunk, mennyi az esélye, hogy az egyes végállapotokban végezzük a játékot. ((I-Q)-1*R) – Ez a függvény van használva a KiNevetAVegen, Penney, és SnakesAndLadders osztályok Markov2műveleihez.
        public double[] GetEndStateChance(int startingState)
        {
            //get the starting state corrected
            int correctedStartingState = -1;
            for(int i=0; i<originalOrder.Length; i++)
            {
                if(originalOrder[i] == startingState)
                {
                    correctedStartingState = i;
                    break;
                }
            }

            //return null, if it is an ending state
            if(correctedStartingState >= absorbingStatesStart)
            {
                return null;
            }

            double[,] RMatrix = new double[absorbingStatesStart, transitionMatrix.GetUpperBound(0) + 1 - absorbingStatesStart];
            double[,] resultMatrix = new double[absorbingStatesStart, transitionMatrix.GetUpperBound(0) + 1 - absorbingStatesStart];


            for (int i=0; i<absorbingStatesStart; i++)
            {
                for (int j=0; j<=transitionMatrix.GetUpperBound(0)-absorbingStatesStart; j++)
                {
                    RMatrix[i, j] = transitionMatrix[i, absorbingStatesStart + j];
                }
            }

            double[,] fundamental = GetFundamentalMatrix();

            for (int i = 0; i < absorbingStatesStart; i++)
            {
                for (int j = 0; j <= transitionMatrix.GetUpperBound(0)-absorbingStatesStart; j++)
                {
                    resultMatrix[i, j] = 0.0;
                    for(int k=0; k<absorbingStatesStart; k++)
                    {
                        resultMatrix[i, j] += fundamental[i, k] * RMatrix[k, j];
                    }
                }
            }

            double[] returnArray = new double[resultMatrix.GetUpperBound(1) + 1];

            for(int i=0; i<returnArray.Length; i++)
            {
                //corrected starting state, because we switched rows and columns in the constructor
                returnArray[i] = resultMatrix[correctedStartingState, i];
            }

            return returnArray;
        }


        //Amennyiben még nem lett érték adva a fundamentalMatrix-nak, meghívja a CalculateFundamentalMatrix-ot és annak eredményét eltárolja a fundamentalMatrix változóban, majd visszaadja egy másolatát. Ha már van értéke, a függvény visszaadja egy másolatát a mátrixnak. – Ez a függvény van használva a KiNevetAVegen, Penney, és SnakesAndLadders osztályok Markov1_5 műveleteihez.
        public double[,] GetFundamentalMatrix()
        {
            double[,] resultMatrix = new double[absorbingStatesStart, absorbingStatesStart];

            if (fundamentalMatrix == null)
            {
                double[,] QMatrix = new double[absorbingStatesStart, absorbingStatesStart];

                for (int i = 0; i < absorbingStatesStart; i++)
                {
                    for (int j = 0; j < absorbingStatesStart; j++)
                    {
                        QMatrix[i, j] = transitionMatrix[i, j];
                    }
                }

                fundamentalMatrix = CalculateFundamentalMatrix(ref QMatrix);
            }

            for(int i=0; i<absorbingStatesStart; i++)
            {
                for(int j=0; j<absorbingStatesStart; j++)
                {
                    resultMatrix[i, j] = fundamentalMatrix[i, j];
                }
            }

            return resultMatrix;
        }


        //Függvény, ami kiszámolja a Markov lánc fundamentális mátrixát (((I-Q)-1), ahol a Q a transitonMatrix legnagyobb, csak átmeneti állapotokat tartalmazó („bal felső”) részmátrixa. Az inverz kiszámítására a SolveFundamental metódust használja.
        private double[,] CalculateFundamentalMatrix(ref double[,] QMatrix)
        {
            int size = QMatrix.GetUpperBound(0)+1;
            double[,] workMatrix = new double[size, size];
            double[,] inverseMatrix = new double[size, size];

            for (int i=0; i < size; i++)
            {
                for(int j=0; j < size; j++)
                {
                    workMatrix[i, j] = -1.0 * QMatrix[i, j];
                }
            }
            
            for (int j = 0; j < size; j++)
            {
                workMatrix[j, j] += 1.0;
                inverseMatrix[j, j] = 1.0;
            }

            SolveFundamental(ref workMatrix, ref inverseMatrix, size);

            return inverseMatrix;
        }


        //Segédmetódus, amit a CalculateFundamentalMatrix függvény használ az (I-Q) mátrix inverzének kiszámítására.
        private void SolveFundamental(ref double[,] workMatrix, ref double[,] inverseMatrix, int size)
        {
            for (int i = 0; i < size-1; i++)
            {
                //a maradék sorokon végigmenve
                for (int j = i+1; j < size; j++)
                {
                    double multipliedBy;
                    multipliedBy = workMatrix[j, i] / workMatrix[i, i];

                    workMatrix[j, i] = workMatrix[j, i] - (multipliedBy * workMatrix[i, i]);

                    for (int k = i+1; k < size; k++)
                    {
                        workMatrix[j, k] = workMatrix[j, k] - (multipliedBy * workMatrix[i, k]);
                    }
                    //inverz mtx számítása
                    for(int k=0; k< size; k++)
                    {
                        inverseMatrix[j, k] = inverseMatrix[j, k] - (multipliedBy * inverseMatrix[i, k]);
                    }
                }
            }


            //now go and make it diagonal
            for (int i = size - 1; i > 0; i--)
            {
                //diagonalisban 1 legyen
                double mult;
                mult = 1.0 / workMatrix[i, i];
                workMatrix[i, i] *= mult;
                //inverz mtx módosítása
                for(int j=0; j<size; j++)
                {
                    inverseMatrix[i, j] *= mult;
                }

                //a maradék sorokon végigmenve
                for (int j = i - 1; j >= 0; j--)
                {
                    //oszlop lenullázása
                    double multipliedBy;
                    multipliedBy = workMatrix[j, i] / workMatrix[i, i];
                    workMatrix[j, i] = 0.0;

                    //inverz mtx módosítása
                    for(int k=0; k<size; k++)
                    {
                        inverseMatrix[j,k] = inverseMatrix[j,k] - (multipliedBy * inverseMatrix[i,k]);
                    }
                }
            }

            //diagonalisban 1 legyen, inverz mtx módosítása
            double lastMult = 1.0 / workMatrix[0, 0];
            for(int i=0; i<size; i++)
            {
                inverseMatrix[0,i] *= lastMult;
            }
            workMatrix[0, 0] = 1.0;

        }



        //Két mátrix összeszorzására használt statikus függvény. (matrixA van a szorzás bal oldalán) – Ez a függvény van használva a KiNevetAVegen, Penney, és SnakesAndLadders osztályok Markov1 műveleteihez.
        public static double[,] MultiplyMatrices(ref double[,] matrixA, ref double[,] matrixB)
        {
            double[,] resultMatrix = null;
            //check if size of matrices is correct
            if (matrixA.GetUpperBound(0) == matrixA.GetUpperBound(1) && matrixB.GetUpperBound(0) == matrixB.GetUpperBound(1) && matrixA.GetUpperBound(0) == matrixB.GetUpperBound(0))
            {
                int N = matrixA.GetUpperBound(0)+1;
                resultMatrix = new double[N, N];

                for (int i = 0; i < N; i++)
                {

                    for (int j = 0; j < N; j++)
                    {
                        double sum = 0;

                        for (int k = 0; k < N; k++)
                        {
                            sum += matrixA[i, k] * matrixB[k, j];
                        }

                        resultMatrix[i, j] = sum;
                    }
                }

            }
            else
            {
                //error
            }

            return resultMatrix;
        }
    }
}