
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleUI.Utils
{
    class BasicConsoleOperations
    {
        private const string k_YesStr = "yes";
        private const string k_NoStr = "no";
        private const int k_DifferenceBetweenIndexAndSize = 1;

        // generates choice string for printing
        private static string generateChoiceStrFromArray(string[] i_StringArr)
        {
            StringBuilder arrayStringBuilder = new StringBuilder();

            for (uint i = 0; i < i_StringArr.Length; i++)
            {
                arrayStringBuilder.AppendFormat("({0}) {1}{2}", i, BasicConsoleOperations.SplitCamelCaseString(i_StringArr[i], ' '), Environment.NewLine);
            }

            return arrayStringBuilder.ToString();
        }

        // Get a string from the user
        public static string GetString(string i_MsgStr)
        {
            Console.WriteLine(i_MsgStr);

            return System.Console.ReadLine();
        }

        private static bool isNumericString(string i_Str)
        {
            return i_Str.All(char.IsDigit);
        }
        private static bool isNumericStringOfLength(string i_Str, uint i_NumDigits)
        {
            return (i_Str.Length == i_NumDigits) && isNumericString(i_Str);
        }

        public static string GetNumericStringOfLength(string i_MsgStr, uint i_NumDigits)
        {
            string currInput = null;
            string currMsgStr = i_MsgStr;

            do
            {
                currInput = GetString(currMsgStr);

                if (isNumericStringOfLength(currInput, i_NumDigits))
                {
                    break;
                }

                if (currMsgStr == i_MsgStr)
                {
                    currMsgStr = "Bad Number. Please insert again: ";
                }
            }
            while (true);

            return currInput;
        }

        // Creates a string array from enum names
        private static string[] createStrArrFromEnum(Type i_EnumType)
        {
            return Enum.GetNames(i_EnumType);
        }

        //todo: enum must start from 0 !
        // Choose an enum value. Shows the user all enum options first
        public static T GetEnumChoice<T>(string i_UserMsg)
        {
            return (T)GetEnumChoice(i_UserMsg, typeof(T));
        }

        public static object GetEnumChoice(string i_UserMsg, Type i_EnumType)
        {
            uint enumVal = GetOption(i_UserMsg, createStrArrFromEnum(i_EnumType));

            return Enum.ToObject(i_EnumType, enumVal);
        }

        //todo: refactor perhaps move some of the logic to positive number from user
        // Get Multiple Enum choices from the user.
        public static List<T> GetMultipleEnumChoices<T>(string i_UserMsg)
        {
            Type typeOfGeneric = typeof(T);
            string[] enumNameArr = createStrArrFromEnum(typeOfGeneric);
            string enumChoicesStr = generateChoiceStrFromArray(enumNameArr);
            List<T> retEnumList = new List<T>();
            List<uint> rawEnumChoices;

            Console.WriteLine(i_UserMsg);
            rawEnumChoices = GetMultiplePositiveNumbersFromUser(enumChoicesStr, new PositiveRange(0, (uint)(enumNameArr.Length) - k_DifferenceBetweenIndexAndSize)); //todo: test max val
            foreach (uint enumVal in rawEnumChoices)
            {
                retEnumList.Add((T)Enum.ToObject(typeOfGeneric, enumVal));
            }

            return retEnumList;
        }

        // Print a string array
        public static void PrintStrArray(string[] i_StrArray)
        {
            foreach (string currStr in i_StrArray)
            {
                Console.WriteLine(currStr);
            }
        }

        //todo: old line, make sure nowhere else             PositiveRange validRange = new PositiveRange(0, (uint)optionsStr.Length - k_DifferenceBetweenIndexAndSize);
        //todo: the length is of chars..

        // Get an option from a string array
        public static uint GetOption(string i_UserMsg, string[] i_Options)
        {
            string optionsStr = generateChoiceStrFromArray(i_Options);
            PositiveRange validRange = new PositiveRange(0, (uint)i_Options.Length - k_DifferenceBetweenIndexAndSize);

            Console.WriteLine(i_UserMsg);

            return GetPositiveNumberFromUser(optionsStr, validRange);
        }

        // Get multiple options from a string array
        public static List<uint> GetMultipleOptions(string i_UserMsg, string[] i_Options)
        {
            string optionsStr = generateChoiceStrFromArray(i_Options);
            PositiveRange validRange = new PositiveRange(0, (uint)i_Options.Length - k_DifferenceBetweenIndexAndSize);

            Console.WriteLine(i_UserMsg);

            return GetMultiplePositiveNumbersFromUser(optionsStr, validRange);
        }

        //todo: consider refactoring
        // Get multiple positive numbers from the user
        public static List<uint> GetMultiplePositiveNumbersFromUser(string i_MessageForUser, PositiveRange i_InputRange)
        {
            string userInputStr;
            uint currUserNumericInput;
            bool isValidInput;
            List<uint> retList = new List<uint>();

            System.Console.WriteLine(i_MessageForUser);
            do
            {
                userInputStr = System.Console.ReadLine().Trim();

                //todo: still need to check what happens when no delimeter presented. i expect it on "abc" to return "abc"
                foreach (string subStr in userInputStr.Split(','))
                {
                    isValidInput = uint.TryParse(subStr, out currUserNumericInput) && i_InputRange.IsInRange(currUserNumericInput);
                    if (!isValidInput)
                    {
                        System.Console.WriteLine("Invalid input! Please try again:");
                        retList.Clear();
                        break;
                    }
                    else
                    {
                        retList.Add(currUserNumericInput);
                    }
                }
            }
            while (retList.Count == 0);

            return retList;
        }

        // Get a positive number from the user. If the user inserts 'i_ExcludingStr', the function will return null instead
        public static uint GetPositiveNumberFromUser(string i_MessageForUser, PositiveRange i_InputRange)
        {
            string userInputStr;
            uint currUserNumericInput;
            bool isValidInput;
            uint? retUserInput = null;

            System.Console.WriteLine(string.Format("{0} (Range: {1}-{2})", i_MessageForUser, i_InputRange.Min, i_InputRange.Max));
            do
            {
                userInputStr = System.Console.ReadLine();
                isValidInput = uint.TryParse(userInputStr, out currUserNumericInput) && i_InputRange.IsInRange(currUserNumericInput);
                if (!isValidInput)
                {
                    System.Console.WriteLine("Invalid input! Please try again:");
                }
                else
                {
                    retUserInput = currUserNumericInput;
                }
            }
            while (!isValidInput);

            return retUserInput.Value;
        }


        public static void NewLine()
        {
            WriteString(string.Empty);
        }

        //todo: Make sure in the calling that its ok to enter MaxVal! (Max val == max val and not max val == max val - 1)
        public static float GetPositiveFloatFromUserWithMaxVal(string i_MessageForUser, float i_MaxVal)
        {

            string initialMsgToUser = string.Format("{0} (Max value is {1})", i_MessageForUser, i_MaxVal.ToString());
            string currMsg = initialMsgToUser;
            float retVal;

            do
            {
                retVal = GetPositiveFloatFromUser(currMsg);

                if (retVal <= i_MaxVal)
                {
                    break;
                }

                if (currMsg == initialMsgToUser)
                {
                    currMsg = "Number is above range. Please insert again:";
                }
            }
            while (true);

            return retVal;
        }
        //todo: Duplication of code
        public static float GetPositiveFloatFromUser(string i_MessageForUser)
        {
            string userInputStr;
            float currUserNumericInput;
            bool isValidInput;
            float? retUserInput = null;

            System.Console.WriteLine(i_MessageForUser);
            do
            {
                userInputStr = System.Console.ReadLine();
                isValidInput = float.TryParse(userInputStr, out currUserNumericInput) && (currUserNumericInput > 0.0f);
                if (!isValidInput)
                {
                    System.Console.WriteLine("Invalid input! Please try again:");
                }
                else
                {
                    retUserInput = currUserNumericInput;
                }
            }
            while (!isValidInput);

            return retUserInput.Value;
        }

        public static void WriteString(string i_Str)
        {
            Console.WriteLine(i_Str);
        }

        // Prompt user for yes/no question
        public static bool PromptQuestion(string i_Question)
        {
            string lastInput = null;

            Console.Write(i_Question);
            Console.WriteLine(" Please choose: yes/no:");
            while ((lastInput == null) || ((lastInput != k_YesStr) && (lastInput != k_NoStr)))
            {
                if (lastInput != null)
                {
                    Console.WriteLine("Bad value. Please insert yes/no:");
                }

                lastInput = System.Console.ReadLine().ToLower();
            }

            return lastInput == k_YesStr;
        }

        public static string SplitCamelCaseString(string i_CamelCaseStr, char i_NewDelimiter)
        {
            List<char> chars = new List<char>();
            int counter = 0;
            bool isCapitalSequence = false;

            foreach (char c in i_CamelCaseStr)
            {
                if (char.IsUpper(c) && !isCapitalSequence)
                {
                    if (counter > 0)
                    {
                        chars.Add(i_NewDelimiter);
                    }
                    chars.Add(c);
                    isCapitalSequence = true;
                }
                else if (c != i_NewDelimiter)
                {
                    chars.Add(c);
                    isCapitalSequence = false;
                }
                counter++;
            }

            return new string(chars.ToArray());
        }
    }
}

