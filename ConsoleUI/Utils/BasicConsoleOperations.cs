
using System;
using System.Collections.Generic;
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
                arrayStringBuilder.AppendFormat("({0}) {1}{2}", i, i_StringArr[i], Environment.NewLine);
            }

            return arrayStringBuilder.ToString();
        }

        // Get a string from the user
        public static string GetString(string i_MsgStr)
        {
            Console.WriteLine(i_MsgStr);

            return System.Console.ReadLine();
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
            Type typeOfGeneric = typeof(T);
            uint enumVal = GetOption(i_UserMsg, createStrArrFromEnum(typeOfGeneric));

            return (T)Enum.ToObject(typeOfGeneric, enumVal);
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

        // Get an option from a string array
        public static uint GetOption(string i_UserMsg, string[] i_Options)
        {
            string optionsStr = generateChoiceStrFromArray(i_Options);
            PositiveRange validRange = new PositiveRange(0, (uint)optionsStr.Length - k_DifferenceBetweenIndexAndSize);

            Console.WriteLine(i_UserMsg);

            return GetPositiveNumberFromUser(optionsStr, validRange);
        }

        // Get multiple options from a string array
        public static List<uint> GetMultipleOptions(string i_UserMsg, string[] i_Options)
        {
            string optionsStr = generateChoiceStrFromArray(i_Options);
            PositiveRange validRange = new PositiveRange(0, (uint)optionsStr.Length - k_DifferenceBetweenIndexAndSize);

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

            System.Console.WriteLine(i_MessageForUser);
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
                isValidInput = float.TryParse(userInputStr, out currUserNumericInput) && currUserNumericInput > 0.0f);
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
    }
}

