using System;
using System.Collections.Generic;
using GarageLogic;
using GarageLogic.VehicleParts;
using GarageLogic.VehicleParts.PowerSources;
using ConsoleUI.Utils;
using System.Reflection;

namespace ConsoleUI
{
    class Garage
    {
        //todo:
        private const int k_FirstLoopRun = 0;
        private const uint k_MaxPlateNumber = 9999999; // 7 digit number
        private GarageManager m_GarageManager = new GarageManager();
        private readonly VehicleFactory m_Factory = new VehicleFactory();
        static readonly PositiveRange sr_plateNumberRange = new PositiveRange(1, k_MaxPlateNumber);


        public void Run()
        {
            while (true)
            {
                MenuOption.eOption menuOptionSelect = getMenuOption();

                if (menuOptionSelect == MenuOption.eOption.Exit)
                {
                    break;
                }

                if (!handleMenuOptionRequest(menuOptionSelect))
                {
                    BasicConsoleOperations.WriteString("Operation Has Failed!");
                    //todo: Write about operation failed
                }
                else
                {
                    BasicConsoleOperations.WriteString("Operation Succeeded!");
                }

                BasicConsoleOperations.NewLine();
            }
        }

        private MenuOption.eOption getMenuOption()
        {
            string[] menuOptionsArray = MenuOption.StringArray;

            return MenuOption.Get(BasicConsoleOperations.GetOption("Choose A Menu Option:", menuOptionsArray));
        }

        private VehicleRecord getVehicleRecordFromLicensePlate()
        {
            GarageLogic.VehicleRecord retVehicle = null;
            bool isFirstTime = true;
            string outStr = "Please Insert Vehicle's Plate Number:";

            do
            {
                uint plateNumber = BasicConsoleOperations.GetPositiveNumberFromUser(outStr, sr_plateNumberRange);

                if (isFirstTime)
                {
                    isFirstTime = !isFirstTime;
                    outStr = "Bad Vehicle's Plate Number. Please Insert Again:";
                }

                retVehicle = m_GarageManager.GetVehicleRecord(plateNumber.ToString());
            }
            while (retVehicle == null);

            return retVehicle;
        }


        //todo: In repair, Repaired, PAid
        private bool handleMenuOptionRequest(MenuOption.eOption i_Option)
        {
            bool hasOperationSucceeded = true;
            try //todo; support other types of exceptions aswell
            {
                //todo: need to catch all exception here
                //todo: should throw invalid input instead of the bool above

                if (i_Option != MenuOption.eOption.InsertNewVehicle && m_GarageManager.IsGarageEmpty)
                {
                    Console.WriteLine("Garage Is Empty, Perhaps You Want To Insert A Vehicle First?");
                }
                else
                {
                    switch (i_Option)
                    {
                        case MenuOption.eOption.InsertNewVehicle:
                            handleInsertNewVehicle();
                            break;
                        case MenuOption.eOption.ShowAllVehiclesId:
                            handleShowAllVehicles();
                            break;
                        case MenuOption.eOption.ModifyVehicleState:
                            handleModifyVehicleState(getVehicleRecordFromLicensePlate());
                            break;
                        case MenuOption.eOption.FillTireAir:
                            handleFillTireAir(getVehicleRecordFromLicensePlate());
                            break;
                        case MenuOption.eOption.RefuelVehicle:
                            handleRefuelVehicle(getVehicleRecordFromLicensePlate());
                            break;
                        case MenuOption.eOption.ChargeVehicle:
                            handleChargeVehicle(getVehicleRecordFromLicensePlate());
                            break;
                        case MenuOption.eOption.ShowVehicleInformation:
                            handleShowVehicleInformation(getVehicleRecordFromLicensePlate());
                            break;
                    }
                }
            }
            catch (ArgumentException aexcp)
            {
                BasicConsoleOperations.WriteString("Argument Exception: Bad Arguments!");
                hasOperationSucceeded = false;
            }

            return hasOperationSucceeded;
        }

        const uint k_NumDigitsInPhoneNumber = 10; //todo; Choose min and max and also perhaps remove it entirely
        private void handleInsertNewVehicle()
        {
            uint plateNumber = BasicConsoleOperations.GetPositiveNumberFromUser("Please Insert Vehicle's Plate Number:", sr_plateNumberRange);
            VehicleRecord existingRecord = m_GarageManager.GetVehicleRecord(plateNumber.ToString());

            if (existingRecord != null)
            {
                BasicConsoleOperations.WriteString("Vehicle Is Already In Garage. Moving To \"In Repair\"");
                existingRecord.Status = VehicleRecord.eStatus.InRepair;
            }
            else
            {
                string modelName = (string) getUserInputAccordingToType("Model Name", string.Empty.GetType());
                VehicleRegistrationInfo registrationInfo = new VehicleRegistrationInfo(modelName, plateNumber.ToString());
                Vehicle createdVehicle = createVehicleFromUserInput(registrationInfo);
                string ownerName = (string) getUserInputAccordingToType("Owner Name", string.Empty.GetType());
                string ownerPhoneNumber = BasicConsoleOperations.GetNumericStringOfLength(string.Format("Please Insert Owner's Phone Number ({0} digits):", k_NumDigitsInPhoneNumber), k_NumDigitsInPhoneNumber);

                m_GarageManager.InsertRecord(ownerName, ownerPhoneNumber, createdVehicle);
            }
        }

        //todo: reconsider writing better strings than those of the enum
        private Vehicle createVehicleFromUserInput(VehicleRegistrationInfo i_RegistrationInfo)
        {
            VehicleFactory.eSupportedVehicle selectedVehicle =
                BasicConsoleOperations.GetEnumChoice<VehicleFactory.eSupportedVehicle>("Please Choose Vehicle Type:");
            TiresInfo tiresInfo = getUserInputAllTiresInfo(
                m_Factory.GetNumTiresInRecipe(selectedVehicle),
                m_Factory.GetTireMaxPsiInRecipe(selectedVehicle));
            float initialPowerSourceValue = BasicConsoleOperations.GetPositiveFloatFromUserWithMaxVal(
                                                "Please Insert Initial Power Source Value ",
                                                m_Factory.GetPowerCapacityOfPowerSourceInRecipe(selectedVehicle)
                                            );
            Vehicle unpopulatedVehicle = m_Factory.CreateUnpopulatedVehicle(selectedVehicle, i_RegistrationInfo, initialPowerSourceValue, tiresInfo);
            populateVehicle(unpopulatedVehicle);

            return unpopulatedVehicle;
        }

        // populate a new vehicle
        private void populateVehicle(Vehicle i_UnpopulatedVehicle)
        {
            IList<PropertyInfo> props = new List<PropertyInfo>(i_UnpopulatedVehicle.GetType().GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance));
            foreach (PropertyInfo prop in props)
            {
                if (prop.CanWrite)
                {
                    prop.SetValue(i_UnpopulatedVehicle, getUserInputAccordingToType(prop.Name, prop.PropertyType));
                }
            }
        }

        // Get input from the user according to i_Type
        object getUserInputAccordingToType(string i_Name, Type i_Type)
        {
            object retObj = null;
            string requestStr = string.Format("Please Insert Vehicle's {0} {1}" ,BasicConsoleOperations.SplitCamelCaseString(i_Name, ' '), (i_Type.BaseType.Name == "Enum") ? "" : "("+i_Type.Name+")");

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
                retObj = Convert.ChangeType(BasicConsoleOperations.GetString(requestStr), i_Type);
            }

            return retObj;
        }

        // Get TiresInfo from the user
        private TiresInfo getUserInputAllTiresInfo(int i_NumTires, float i_MaxPSI)
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
                    tiresInitialAirValues[i] = BasicConsoleOperations.GetPositiveFloatFromUserWithMaxVal("Please Insert Initial Air Value", i_MaxPSI);

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

        // Print status of a specific vehicle, by its record
        private void printRecordStatus(string i_Msg, VehicleRecord i_VehicleRecord)
        {
            BasicConsoleOperations.WriteString(string.Format("{0} {1}", i_Msg, BasicConsoleOperations.SplitCamelCaseString(i_VehicleRecord.Status.ToString(), ' ')));
        }

        // Show vehicle information
        private void handleShowVehicleInformation(VehicleRecord i_VehicleRecord)
        {
            string ownerStr = string.Format("Vehicle's Owner: {0}{2}Vehicle's Owner Phone number: {1}", i_VehicleRecord.OwnerName, i_VehicleRecord.OwnerPhoneNumber, Environment.NewLine);

            printRecordStatus("Vehicle Garage Status:", i_VehicleRecord);
            BasicConsoleOperations.WriteString(ownerStr);
            VehiclePrintUtils.PrintVehicle(i_VehicleRecord.Vehicle);
        }

        // Handle mofidy vehicle state menu request
        private void handleModifyVehicleState(VehicleRecord i_VehicleRecord)
        {
            VehicleRecord.eStatus newStatus;

            printRecordStatus("Current Vehicle State:", i_VehicleRecord);
            //todo: test this. make sure if changing status to same or to invalid, throws?.. also make sure i catch that
            newStatus = BasicConsoleOperations.GetEnumChoice<VehicleRecord.eStatus>("Please Choose New State:");
            i_VehicleRecord.Status = newStatus;
        }

        // Handle fill tire air menu request
        private void handleFillTireAir(VehicleRecord i_VehicleRecord)
        {
            foreach (GarageLogic.VehicleParts.Tire tire in i_VehicleRecord.Vehicle.Tires)
            {
                tire.InflateAir(tire.MaxPSI - tire.CurrentPSI);
            }
        }

        // Handle charge car menu request
        private void handleChargeVehicle(VehicleRecord i_VehicleRecord)
        {
            PowerSource powerSource = i_VehicleRecord.Vehicle.PowerSource;
            ElectricalSource electricSource;
            float timeToCharge;

            if (powerSource.EnergyPercent > 0.999)
            {
                BasicConsoleOperations.WriteString("Already In Full Capacity.");
            }
            else
            {
                if (!(powerSource is ElectricalSource))
                {
                    throw new ArgumentException();
                }

                electricSource = (ElectricalSource)powerSource;
                //todo: sure needs in hours?
                timeToCharge = BasicConsoleOperations.GetPositiveFloatFromUserWithMaxVal("Please Insert Time To Charge In Hours:", powerSource.PowerCapacity - powerSource.CurrentPowerLevel);
                electricSource.Recharge(timeToCharge);
            }
        }

        // Handle refuel car menu request
        private void handleRefuelVehicle(VehicleRecord i_VehicleRecord)
        {
            PowerSource powerSource = i_VehicleRecord.Vehicle.PowerSource;
            FuelSource fuelSource;
            FuelSource.eFuelType usedFuelType;
            float amountToFill;

            //todo: const
            if (powerSource.EnergyPercent > 0.999)
            {
                BasicConsoleOperations.WriteString("Already In Full Fuel.");
            }
            else
            {
                if (!(powerSource is FuelSource))
                {
                    throw new ArgumentException();
                }

                fuelSource = (FuelSource)powerSource;
                usedFuelType = BasicConsoleOperations.GetEnumChoice<FuelSource.eFuelType>("Please Choose Fuel Type:");
                amountToFill = BasicConsoleOperations.GetPositiveFloatFromUserWithMaxVal("Please Insert Amount To Fuel In Liters:", powerSource.PowerCapacity - powerSource.CurrentPowerLevel);
                fuelSource.Refuel(usedFuelType, amountToFill);
            }
        }

        // Get amount of filter to be used
        private List<VehicleRecord.eStatus> getVehicleStatusFilterInformation()
        {
            List<VehicleRecord.eStatus> filterList = new List<VehicleRecord.eStatus>();

            if (BasicConsoleOperations.PromptQuestion("Would You Like To Filter Out Some Of The Results?"))
            {
                filterList = BasicConsoleOperations.GetMultipleEnumChoices<VehicleRecord.eStatus>("Please Choose Out Filters: (Multiple Filters Can Be Choosed My Using A Comma Between Each)");
            }

            return filterList;
        }

        // Show all vehicle plates
        private bool handleShowAllVehicles()
        {
            List<VehicleRecord.eStatus> nonAllowedVehicleFilter = getVehicleStatusFilterInformation();
            List<VehicleRecord> vehicleRecords = m_GarageManager.GetVehicleRecords(nonAllowedVehicleFilter);

            foreach (VehicleRecord vehicleRecord in vehicleRecords)
            {
                Console.WriteLine(vehicleRecord.Vehicle.PlateNumber);
            }

            return true;
        }
    }
}
