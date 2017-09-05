using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleUI.Utils
{
    class BasicConsoleOperations
    {
        private const char k_DefaultCamelDelimiter = ' ';
        private const string k_YesStr = "yes";
        private const string k_NoStr = "no";
        private const int k_DifferenceBetweenIndexAndSize = 1;
//todo: Where to throw format exception?
        // generates choice string for printing
        private static string generateChoiceStrFromArray(string[] i_StringArr)
        {
            StringBuilder arrayStringBuilder = new StringBuilder();

            for (uint i = 0; i < i_StringArr.Length; i++)
            {
                string splittedCamelCaseStr = BasicConsoleOperations.SplitCamelCaseString(i_StringArr[i], ' ');

                arrayStringBuilder.AppendFormat("({0}) {1}{2}", i, splittedCamelCaseStr, Environment.NewLine);
            }

            return arrayStringBuilder.ToString();
        }

        // Get a string from the user
        public static string GetString(string i_MsgStr)
        {
            Console.WriteLine(i_MsgStr);

            return System.Console.ReadLine();
        }

        // Check whether a string is of numeric type
        private static bool isNumericString(string i_Str)
        {
            return i_Str.All(char.IsDigit);
        }

        // Check whether a string is numeric and of a certain length
        private static bool isNumericStringOfMinLength(string i_Str, uint i_NumDigits)
        {
            return (i_Str.Length >= i_NumDigits) && isNumericString(i_Str);
        }

        // Get a numeric string of 'i_NumDigits' Length
        public static string GetNumericStringOfMinimumLength(string i_MsgStr, uint i_NumMinDigits)
        {
            string currInput = null;
            string currMsgStr = i_MsgStr;

            do
            {
                currInput = GetString(currMsgStr);

                if (isNumericStringOfMinLength(currInput, i_NumMinDigits))
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

        // Get an enum choice With casting
        public static T GetEnumChoice<T>(string i_UserMsg)
        {
            return (T)GetEnumChoice(i_UserMsg, typeof(T));
        }

        // Get an enum choice
        public static object GetEnumChoice(string i_UserMsg, Type i_EnumType)
        {
            uint enumVal = GetOption(i_UserMsg, createStrArrFromEnum(i_EnumType));

            return Enum.ToObject(i_EnumType, enumVal);
        }

        // Get Multiple Enum choices from the user.
        public static List<T> GetMultipleEnumChoices<T>(string i_UserMsg)
        {
            Type typeOfGeneric = typeof(T);
            string[] enumNameArr = createStrArrFromEnum(typeOfGeneric);
            string enumChoicesStr = generateChoiceStrFromArray(enumNameArr);
            List<T> retEnumList = new List<T>();
            List<uint> rawEnumChoices;

            Console.WriteLine(i_UserMsg);
            rawEnumChoices = GetMultiplePositiveNumbersFromUser(enumChoicesStr, new PositiveRange(0, (uint)enumNameArr.Length - k_DifferenceBetweenIndexAndSize));
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
            PositiveRange validRange = new PositiveRange(0, (uint)i_Options.Length - k_DifferenceBetweenIndexAndSize);

            Console.WriteLine(i_UserMsg);

            return GetPositiveIntInRange(optionsStr, validRange);
        }

        // Get multiple options from a string array
        public static List<uint> GetMultipleOptions(string i_UserMsg, string[] i_Options)
        {
            string optionsStr = generateChoiceStrFromArray(i_Options);
            PositiveRange validRange = new PositiveRange(0, (uint)i_Options.Length - k_DifferenceBetweenIndexAndSize);

            Console.WriteLine(i_UserMsg);

            return GetMultiplePositiveNumbersFromUser(optionsStr, validRange);
        }

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

                foreach (string subStr in userInputStr.Split(','))
                {
                    isValidInput = uint.TryParse(subStr, out currUserNumericInput) && i_InputRange.IsInRange(currUserNumericInput);
                    if (!isValidInput)
                    {
                        Console.WriteLine("Invalid input! Please try again:");
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
        public static uint GetPositiveIntInRange(string i_MessageForUser, PositiveRange i_InputRange)
        {
            string outStr = string.Format("{0} (Range: {1}-{2})", i_MessageForUser, i_InputRange.Min, i_InputRange.Max);
            uint retNum = GetObjectFromUser<uint>(outStr);

            while (!i_InputRange.IsInRange(retNum))
            {
                retNum = GetObjectFromUser<uint>("Input is not in range. Please insert again:");
            }

            return retNum;
        }

        // Write new line
        public static void NewLine()
        {
            WriteString(string.Empty);
        }

        // Get positive float from the user
        public static float GetPositiveFloatFromUser(string i_MessageForUser)
        {
            return GetPositiveFloatFromUser(i_MessageForUser, null);
        }

        // Get positive float from the user with an optional max value
        public static float GetPositiveFloatFromUser(string i_MessageForUser, float? i_MaxVal)
        {
            string outStr = i_MaxVal.HasValue ? getMaxValStr(i_MessageForUser, i_MaxVal) : i_MessageForUser;
            float retFloat = GetObjectFromUser<float>(outStr);

            while ((retFloat < 0.0f) || (i_MaxVal.HasValue && (retFloat > i_MaxVal.Value)))
            {
                retFloat = GetObjectFromUser<float>("Input is not in range. Please insert again:");
            }

            return retFloat;
        }

        // Get max value range string
        private static string getMaxValStr(string i_AdditionalStr, object i_MaxVal)
        {
            return string.Format("{0} (Max value is {1})", i_AdditionalStr, i_MaxVal.ToString());
        }

        // Write a string
        public static void WriteString(string i_Str)
        {
            Console.WriteLine(i_Str);
        }

        // Get type from user (With casting)
        public static T GetObjectFromUser<T>(string i_RequestStr)
        {
            return (T)GetObjectFromUser(i_RequestStr, typeof(T));
        }

        // Get type from user
        public static object GetObjectFromUser(string i_RequestStr, Type i_Type)
        {
            object retObj = null;
            string requestStr = i_RequestStr;
            string errStr = "Bad input was inserted. Please insert again:";

            while (true)
            {
                try
                {
                    retObj = Convert.ChangeType(GetString(requestStr), i_Type);
                    break;
                }
                catch (Exception)
                {
                    requestStr = errStr;
                }
            }

            return retObj;
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

        // Split a string according to camel case
        public static string SplitCamelCaseString(string i_CamelCaseStr)
        {
            return SplitCamelCaseString(i_CamelCaseStr, k_DefaultCamelDelimiter);
        }

        // Split a string according to camel case
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
                else
                {
                    if (c != i_NewDelimiter)
                    {
                        chars.Add(c);
                    }

                    isCapitalSequence = false;
                }

                counter++;
            }

            return new string(chars.ToArray());
        }
    }
}