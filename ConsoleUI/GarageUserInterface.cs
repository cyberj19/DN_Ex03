using System;
using System.Collections.Generic;
using ConsoleUI.Utils;
using ConsoleUI.VehicleInputOutput;
using GarageLogic;
using GarageLogic.VehicleParts;
using GarageLogic.VehicleParts.PowerSources;
using GarageLogic.Exceptions;

namespace ConsoleUI
{
    class GarageUserInterface
    {
        private const float k_MaxEnergyThreshold = 0.999f;
        private const uint k_MaxPlateNumber = 9999999; // 7 digit number
        static readonly PositiveRange sr_plateNumberRange = new PositiveRange(1, k_MaxPlateNumber);
        private readonly VehicleFactory m_Factory = new VehicleFactory();
        private GarageManager m_GarageManager = new GarageManager();

        public void Run()
        {
            while (true)
            {
                eMenuOption menuOptionSelect = getMenuOption();

                if (menuOptionSelect == eMenuOption.Exit)
                {
                    break;
                }

                if (!handleMenuOptionRequest(menuOptionSelect))
                {
                    BasicConsoleOperations.WriteString("Operation Has Failed!");
                }
                else
                {
                    BasicConsoleOperations.WriteString("Operation Succeeded!");
                }

                BasicConsoleOperations.NewLine();
            }
        }

        private eMenuOption getMenuOption()
        {
            return BasicConsoleOperations.GetEnumChoice<eMenuOption>("Please Choose A Menu Option:");
        }

        private VehicleRecord getVehicleRecordFromLicensePlate()
        {
            GarageLogic.VehicleRecord retVehicle = null;
            bool isFirstTime = true;
            string outStr = "Please Insert Vehicle's Plate Number:";

            do
            {
                uint plateNumber = BasicConsoleOperations.GetPositiveIntInRange(outStr, sr_plateNumberRange);

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

        private bool handleMenuOptionRequest(eMenuOption i_Option)
        {
            bool hasOperationSucceeded = true;

            try
            {
                if (i_Option != eMenuOption.InsertNewVehicle && m_GarageManager.IsGarageEmpty)
                {
                    Console.WriteLine("Garage Is Empty, Perhaps You Want To Insert A Vehicle First?");
                }
                else
                {
                    switch (i_Option)
                    {
                        case eMenuOption.InsertNewVehicle:
                            handleInsertNewVehicle();
                            break;
                        case eMenuOption.ShowAllVehiclesLicensingPlate:
                            handleShowAllVehicles();
                            break;
                        case eMenuOption.ModifyVehicleState:
                            handleModifyVehicleState(getVehicleRecordFromLicensePlate());
                            break;
                        case eMenuOption.FillTiresAir:
                            handleFillTireAir(getVehicleRecordFromLicensePlate());
                            break;
                        case eMenuOption.RefuelVehicle:
                            handleRefuelVehicle(getVehicleRecordFromLicensePlate());
                            break;
                        case eMenuOption.ChargeVehicle:
                            handleChargeVehicle(getVehicleRecordFromLicensePlate());
                            break;
                        case eMenuOption.ShowVehicleInformation:
                            handleShowVehicleInformation(getVehicleRecordFromLicensePlate());
                            break;
                    }
                }
            }
            catch (ArgumentException)
            {
                BasicConsoleOperations.WriteString("Argument Exception: Bad Arguments!");
                hasOperationSucceeded = false;
            }
            catch (ValueOutOfRangeException excp)
            {
                BasicConsoleOperations.WriteString("Value out of range:");
                BasicConsoleOperations.WriteString(excp.Message);
            }

            return hasOperationSucceeded;
        }

        private void handleInsertNewVehicle()
        {
            uint plateNumber = BasicConsoleOperations.GetPositiveIntInRange("Please Insert Vehicle's Plate Number:", sr_plateNumberRange);
            VehicleRecord existingRecord = m_GarageManager.GetVehicleRecord(plateNumber.ToString());

            if (existingRecord != null)
            {
                BasicConsoleOperations.WriteString("Vehicle Is Already In Garage. Moving To \"In Repair\"");
                existingRecord.Status = VehicleRecord.eStatus.InRepair;
            }
            else
            {
                VehicleRecordData recordData = VehicleInput.GetVehicleRecordData(m_Factory, plateNumber.ToString());

                m_GarageManager.InsertRecord(recordData.OwnerName, recordData.OwnerPhoneNumber, recordData.Vehicle);
            }
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
            VehicleOutput.PrintVehicle(i_VehicleRecord.Vehicle);
        }

        // Handle mofidy vehicle state menu request
        private void handleModifyVehicleState(VehicleRecord i_VehicleRecord)
        {
            VehicleRecord.eStatus newStatus;

            printRecordStatus("Current Vehicle State:", i_VehicleRecord);
            newStatus = BasicConsoleOperations.GetEnumChoice<VehicleRecord.eStatus>("Please Choose New State:");
            i_VehicleRecord.Status = newStatus;
        }

        // Handle fill tire air menu request
        private void handleFillTireAir(VehicleRecord i_VehicleRecord)
        {
            foreach (Tire tire in i_VehicleRecord.Vehicle.Tires)
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
                timeToCharge = BasicConsoleOperations.GetPositiveFloatFromUser("Please Insert Time To Charge In Hours:", powerSource.PowerCapacity - powerSource.CurrentPowerLevel);
                electricSource.Recharge(timeToCharge);
            }
        }

        // Handle refuel car menu request
        private void handleRefuelVehicle(VehicleRecord i_VehicleRecord)
        {
            PowerSource powerSource = i_VehicleRecord.Vehicle.PowerSource;
            FuelSource.eFuelType usedFuelType;
            float amountToFill;

            if (powerSource.EnergyPercent > k_MaxEnergyThreshold)
            {
                BasicConsoleOperations.WriteString("Already In Full Fuel.");
            }
            else
            {
                FuelSource fuelSource = powerSource as FuelSource;

                if (fuelSource == null)
                {
                    throw new ArgumentException();
                }

                usedFuelType = BasicConsoleOperations.GetEnumChoice<FuelSource.eFuelType>("Please Choose Fuel Type:");
                amountToFill = BasicConsoleOperations.GetPositiveFloatFromUser("Please Insert Amount To Fuel In Liters:", powerSource.PowerCapacity - powerSource.CurrentPowerLevel);
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
