using System;
using System.Collections.Generic;
using System.Reflection;
using ConsoleUI.Utils;
using GarageLogic.VehicleParts;
using GarageLogic;
using GarageLogic.Exceptions;

namespace ConsoleUI.VehicleInputOutput
{
    static class VehicleInput
    {
        private const uint k_MinNumDigitsInPhoneNumber = 3;
        private const int k_FirstLoopRun = 0;

        public static VehicleRecordData GetVehicleRecordData(VehicleFactory i_Factory, string i_PlateNumber)
        {
            string modelName = getModelName();
            VehicleRegistrationInfo registrationInfo = new VehicleRegistrationInfo(modelName, i_PlateNumber);
            Vehicle createdVehicle = createVehicleFromUserInput(i_Factory, registrationInfo);
            string ownerName = getOwnerName();
            string ownerPhoneNumber = getOwnerPhoneNumber();

            return new VehicleRecordData(ownerName, ownerPhoneNumber, createdVehicle);
        }

        private static string getOwnerPhoneNumber()
        {
            string outStr = string.Format("Please Insert Owner's Phone Number (atleast {0} digits):", k_MinNumDigitsInPhoneNumber);

            return BasicConsoleOperations.GetNumericStringOfMinimumLength(outStr, k_MinNumDigitsInPhoneNumber);
        }

        private static string getOwnerName()
        {
            return BasicConsoleOperations.GetString("Please insert Owner Name:");
        }

        private static string getModelName()
        {
            return BasicConsoleOperations.GetString("Please insert Model Name:");
        }

        private static Vehicle createVehicleFromUserInput(VehicleFactory i_Factory, VehicleRegistrationInfo i_RegistrationInfo)
        {
            VehicleFactory.eSupportedVehicle selectedVehicle =
                BasicConsoleOperations.GetEnumChoice<VehicleFactory.eSupportedVehicle>("Please Choose Vehicle Type:");
            TiresInfo tiresInfo = getUserInputAllTiresInfo(
                i_Factory.GetNumTiresInRecipe(selectedVehicle),
                i_Factory.GetTireMaxPsiInRecipe(selectedVehicle));
            float initialPowerSourceValue = BasicConsoleOperations.GetPositiveFloatFromUser(
                                                "Please Insert Initial Power Source Value ",
                                                i_Factory.GetPowerCapacityOfPowerSourceInRecipe(selectedVehicle));
            Vehicle unpopulatedVehicle = i_Factory.CreateUnpopulatedVehicle(selectedVehicle, i_RegistrationInfo, initialPowerSourceValue, tiresInfo);

            populateVehicle(unpopulatedVehicle);

            return unpopulatedVehicle;
        }

        // populate a new vehicle
        private static void populateVehicle(Vehicle i_UnpopulatedVehicle)
        {
            IList<PropertyInfo> props = new List<PropertyInfo>(i_UnpopulatedVehicle.GetType().GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance));
            foreach (PropertyInfo prop in props)
            {
                while (true)
                {
                    try
                    {
                        if (prop.CanWrite)
                        {
                            prop.SetValue(i_UnpopulatedVehicle, getUserInputAccordingToType(prop.Name, prop.PropertyType));
                        }

                        break;
                    }
                    catch (Exception excp)
                    {
                        Console.WriteLine("Bad value given");
                        Console.WriteLine(excp.InnerException.Message);
                    }
                }
            }
        }

        // get object name
        private static string getObjName(Type i_Type)
        {
            string retString = i_Type.Name;

            if (i_Type.IsEnum)
            {
                retString = "Enum";
            }
            else if (i_Type == typeof(float))
            {
                retString = "Float";
            }

            return retString;
        }

        // Get input from the user according to i_Type
        private static object getUserInputAccordingToType(string i_Name, Type i_Type)
        {
            object retObj = null;
            string formatStr = "Please Insert Vehicle's {0} ({1})";
            string requestStr = string.Format(formatStr, BasicConsoleOperations.SplitCamelCaseString(i_Name), getObjName(i_Type));

            if (i_Type.IsEnum)
            {
                retObj = BasicConsoleOperations.GetEnumChoice(requestStr, i_Type);
            }
            else if (i_Type == typeof(bool))
            {
                retObj = BasicConsoleOperations.PromptQuestion(requestStr);
            }
            else
            {
                retObj = BasicConsoleOperations.GetObjectFromUser(requestStr, i_Type);
            }

            return retObj;
        }
        
        // Get TiresInfo from the user
        private static TiresInfo getUserInputAllTiresInfo(int i_NumTires, float i_MaxPSI)
        {
            string[] tiresManufacturerNames = new string[i_NumTires];
            float[] tiresInitialAirValues = new float[i_NumTires];
            bool areAllTiresSameType = false;

            for (int i = 0; i < i_NumTires; i++)
            {
                if (!areAllTiresSameType)
                {
                    BasicConsoleOperations.WriteString("_______");
                    BasicConsoleOperations.WriteString(string.Format("Entering Data For Tire Number: {0}", i + 1));
                    tiresManufacturerNames[i] = BasicConsoleOperations.GetString("Please Insert Manufacturer Name:");
                    tiresInitialAirValues[i] = BasicConsoleOperations.GetPositiveFloatFromUser("Please Insert Initial Air Value", i_MaxPSI);

                    if (i == k_FirstLoopRun)
                    {
                        areAllTiresSameType = BasicConsoleOperations.PromptQuestion("Are All Tires The Same? (Automatic Fillup)");
                    }
                }
                else
                {
                    tiresManufacturerNames[i] = tiresManufacturerNames[0];
                    tiresInitialAirValues[i] = tiresInitialAirValues[0];
                }
            }

            return new TiresInfo(tiresManufacturerNames, tiresInitialAirValues);
        }
    }
}
