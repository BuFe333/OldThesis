using System;


namespace BFszakdolgozat
{

    //Dice class is used for all the dice rolls the games require
    class Dice
    {
        private int[] diceArray;  //Az osztály megalkotásakor megadott tömb, amely az egy dobáshoz használt összes kocka lapszámát tárolja. Pl.: ha diceArray = { 6, 10 }, akkor egy dobásnál egyszerre dobunk egy 6 és 10 oldalú kockával (amelyek 1-től vannak számozva).
        private double[] odds;  //Tömb, amely olyan hosszú ahány különböző értéke lehet egy dobásnak, benne pedig a különböző dobás értékek valószínűségei vannak (dobás érték szerint növekvő sorrendben, 0 indextől kezdve) Az osztályon belül, a GetOdds() függvény által van kiszámolva.
        private Random rnd;  //A random dobások eredményének kiszámolására használjuk.
        private int lastRollResult;  //A Roll() függvény által utoljára dobott érték

        //Default konstruktor, egy egyszerű, egy 6-oldalú dobókockát használó osztályt hoz létre.
        public Dice()
        {
            lastRollResult = -1;

            diceArray = new int[1];
            diceArray[0] = 6;
            odds = new double[6];
            odds[0] = 1.0 / 6.0;
            odds[1] = 1.0 / 6.0;
            odds[2] = 1.0 / 6.0;
            odds[3] = 1.0 / 6.0;
            odds[4] = 1.0 / 6.0;
            odds[5] = 1.0 / 6.0;
            rnd = new Random();
        }

        //Alternatív konstruktor, ami lehetővé teszi tetszőleges kocka kombinációk használatát.
        public Dice(ref int[] inputArray)
        {
            lastRollResult = -1;

            diceArray = inputArray;
            odds = null;
            rnd = new Random();
        }




        //Függvény, ami visszaadja egy véletlenszerű dobás értékét (az összes kockán dobott számok összegét).
        public int Roll()
        {
            int sum = 0;
            for(int i = 0; i < diceArray.Length; i++)
            {
                sum += rnd.Next(1, diceArray[i]+1);
            }

            lastRollResult = sum;
            return sum;
        }

        //Függvény, ami visszaadja az osztályhoz tartozó kockákkal kidobható legkisebb értéket.
        public int MinRoll()
        {
            return diceArray.Length;
        }

        //Függvény, ami visszaadja az osztályhoz tartozó kockákkal kidobható legnagyobb értéket.
        public int MaxRoll()
        {
            int sum = 0;
            for(int i=0; i<diceArray.Length ; i++)
            {
                sum += diceArray[i];
            }
            return sum;
        }



        //Függvény, ami visszaadja az odds tömböt, és amennyiben az odds tömb még nem volt létrehozva, azt létrehozza.
        public double[] GetOdds()
        {
            if(odds == null)
            {
                int numberOfRolls = MaxRoll() - MinRoll() + 1;
                int[] occured = new int[numberOfRolls];
                for(int i=0; i<numberOfRolls; i++)
                {
                    occured[i] = 0;
                }

                int times = 0;
                odds = new double[numberOfRolls];

                int[] copyArray = new int[diceArray.Length];

                for (int i = 0; i < diceArray.Length; i++)
                {
                    copyArray[i] = 1;
                }

                CreateOdds(ref copyArray, 0, diceArray.Length, ref times, ref occured);

                for(int i=0; i<numberOfRolls; i++)
                {
                    odds[i] = (double)occured[i] / (double)times;
                }

            }
            return odds;
        }

        //Rekurzív segéd metódus az odds tömb kiszámításához a GetOdds() függvényen belül.
        private void CreateOdds(ref int[] copyArray, int currentLength, int maxLength, ref int times, ref int[] occured)
        {
            if (currentLength < maxLength)
            {
                for (int i = 1; i <= diceArray[currentLength]; i++)
                {
                    copyArray[currentLength] = i;
                    CreateOdds(ref copyArray, currentLength + 1, maxLength, ref times, ref occured);
                }
            }
            else
            {
                times++;
                int currentSum = -1 * MinRoll();
                for(int i=0; i<copyArray.Length; i++)
                {
                    currentSum += copyArray[i];
                }

                try
                {
                    occured[currentSum]++;
                }
                catch (Exception ex)
                {
                    
                }
                
            }
        }

        //Visszaadja a lastRollResult-ban tárolt értéket
        public int GetLastRollResult()
        {
            return lastRollResult;
        }
    }
}
