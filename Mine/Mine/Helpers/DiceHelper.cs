using System;
using System.Collections.Generic;
using System.Text;

namespace Mine.Helpers
{
    public static class DiceHelper
    {
        private static Random rnd = new Random();

        public static bool ForceRollsToNotRandom = false;

        public static int ForcedRandomValue = 1;

        /**
         * 
         * Rolls a Dice a set Number of times
         * RollDice(2, 6)  Roll two 6 sided dice.  Values (2-12)
         * RollDice(1,10) Roll a 10 sided dice one time.  Values (1-10)
         * 
         */
        public static int RollDice(int rolls, int dice)
        {
            if (ForceRollsToNotRandom)
            {
                return rolls * ForcedRandomValue;
            }

            if (rolls < 1)
            {
                return 0;
            }

            if(dice < 1)
            {
                return 0;
            }

            var myReturn = 0;

            for(var i = 0;i <rolls; i++)
            {
                myReturn += rnd.Next(1, dice + 1);
            }

            return myReturn;
        }
    }
}
