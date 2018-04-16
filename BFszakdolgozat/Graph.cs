using System.Collections.Generic;

namespace BFszakdolgozat
{

    //Graph class is used in playing and using Monte-Carlo simulation on games
    class Graph
    {
        List<int[]> gameStates;  //Lehetséges játékállapotokat tartalmazó lista. A játékállapotokat integer tömbökként ábrázoljuk, értelmezésük az éppen szimulált játéktól függ.
        List<int>[] gameGraph;  //Tömb, amely az egyes játékállapotok közötti átmeneteket tárolja.

        int gameStateLength;  //Azt tárolja, hogy az aktuálisan szimulált játék állapotának tárolására hány integerből álló tömböt használunk.

        public int currentPosIndex;  //Annak a játékállapotnak a gameStates-beli indexét tárolja, ahol a szimuláció éppen tart.
        int startingPosIndex;   //Annak a játékállapotnak a gameStates-beli indexét tárolja, amelyik a játék kezdőpozíciója.

        Dice dicePool;  //A játékhoz egy dobásnál használt kockákat tárolására és minden lépésnél a kockadobások eredményeinek kiszámítására szolgál.


        //konstruktor, ami a megadott kezdőpozíció (startingPosIndex) és előre elkészített gameGraph, gameState és Dice segítségével létrehozza a Graph osztályt
        public Graph(ref List<int[]> inputGameStates,
                     ref List<int>[] inputGraph,
                     int inputStartingPosIndex,
                     ref Dice inputDicePool)
        {
            gameStates = inputGameStates;
            gameStateLength = inputGameStates[0].Length;
            gameGraph = inputGraph;
            startingPosIndex = inputStartingPosIndex;
            currentPosIndex = startingPosIndex;
            dicePool = inputDicePool;
        }



        //Metódus, ami visszaállítja a játék szimulációt a kezdőpozícióba.
        public void Reset()
        {
            currentPosIndex = startingPosIndex;
        }

        //Függvény, ami visszaadja a játékállapotot, ahol a szimuláció jelenleg tart.
        public int[] GetGameState()
        {
            int[] currentGameState = new int[gameStateLength];
            for(int i=0; i<gameStateLength; i++)
            {
                currentGameState[i] = gameStates[currentPosIndex % gameStates.Count][i];
            }
            return currentGameState;
        }

        //Függvény, ami végrehajt egy  dobást, és a szimulációt ennek megfelelően lépteti. Amennyiben rendben lezajlott a lépés, igazat ad vissza, amennyiben egy végállapotból próbáltunk ellépni, hamisat ad vissza, lépés nem történik.
        public bool Step()
        {
            int newIndex = dicePool.Roll() - dicePool.MinRoll();

            if (gameGraph[currentPosIndex].Count < newIndex + 1) return false;

            currentPosIndex = gameGraph[currentPosIndex][newIndex];

            return true;
        }



        //Ami egy megadott állásba állítja a gráfbejárást. (Ki Nevet a Végén játszásához használjuk)
        public void SetGameStateIndex(int newStatePosIndex)
        {
            currentPosIndex = newStatePosIndex;
        }

        //Ami visszaadja a gráf melyik csúcsában tart a bejárás. (Ki Nevet a Végén játszásához használjuk)
        public int GetGameStateIndex()
        {
            return currentPosIndex;
        }
    }
}