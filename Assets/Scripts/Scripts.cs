using System;
using UnityEngine;

namespace UsefulScript
{
    
    static class Scripts
    {
        public static String NumberToString(int number, int maxLength, int lengthDecimal = 1)
        {
            maxLength = maxLength < 3 ? 3 : maxLength;
            int numberLength = number.ToString().Length;
            String numberString = number.ToString();
            if (numberLength <= maxLength) return numberString;
            String charValue = "";
            switch (numberLength)
            {
                case < 7:
                    charValue = "K";
                    break;
                case < 10:
                    charValue = "M";
                    break;
                case < 13:
                    charValue = "T";
                    break;
                default:
                    charValue = "a" + ((numberLength - 12) / 3).ToString();
                    break;
            }

            String result = "";
            int value = numberLength % 3==0? 3 : numberLength % 3;
            for (int i = 0; i < value; i++)
            {
                result += numberString[i];
            }

            if (lengthDecimal > 0)
            {
                result += ".";
                for (int i = 0; i < lengthDecimal; i++)
                {
                    result += numberString[value + i];
                }
            }

            result += charValue;
            return result;
        }
    }
}
