using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GarageLogic;
using GarageLogic.VehicleParts;
using GarageLogic.VehicleParts.PowerSources;
using ConsoleUI.Utils;

namespace ConsoleUI
{
    class Garage
    {
        //todo:
        private const uint k_MaxPlateNumber = 9999999; // 7 digit number
        private GarageManager m_GarageManager = new GarageManager();

        public void Run()
        {
            while(true)
            {
                MenuOption.eOption menuOptionSelect = getMenuOption();
                
                if (menuOptionSelect == MenuOption.eOption.Exit)
                {
                    break;
                }

                if (!handleMenuOptionRequest(menuOptionSelect))
                {
                    //todo: Write about operation failed
                }
            }
        }

        private MenuOption.eOption getMenuOption()
        {
            string[] menuOptionsArray = MenuOption.StringArray;

            return MenuOption.Get(BasicConsoleOperations.GetOption("Choose a menu option:", menuOptionsArray));
        }

        private VehicleRecord getVehicleRecordFromLicensePlate()
        {
            PositiveRange plateNumberRange = new PositiveRange(1, k_MaxPlateNumber);
            GarageLogic.VehicleRecord retVehicle = null;
            bool isFirstTime = true;
            string outStr = "Please insert vehicle's plate number:";

            do
            {
                uint plateNumber = BasicConsoleOperations.GetPositiveNumberFromUser(outStr, plateNumberRange);

                if (isFirstTime)
                {
                    isFirstTime = !isFirstTime;
                    outStr = "Bad vehicle's plate number. Please insert again:";
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

            //todo: need to catch all exception here
            //todo: should throw invalid input instead of the bool above
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
               case MenuOption.eOption.RefuelCar:
                    handleRefuelCar(getVehicleRecordFromLicensePlate());
                    break;
               case MenuOption.eOption.ChargeCar:
                    handleChargeCar(getVehicleRecordFromLicensePlate());
                    break;
               case MenuOption.eOption.ShowVehicleInformation:
                    handleShowVehicleInformation(getVehicleRecordFromLicensePlate());
                    break;
            }

            return hasOperationSucceeded;
        }

        private void handleInsertNewVehicle()
        {
            //todo: Cant do this yet??...
            //                uint plateNumber = BasicConsoleOperations.GetPositiveNumberFromUser(outStr, plateNumberRange);

        }

        private void printRecordStatus(string i_Msg, VehicleRecord i_VehicleRecord)
        {
            BasicConsoleOperations.WriteString(i_Msg);
            BasicConsoleOperations.WriteString(i_VehicleRecord.Status.ToString());
        }

        //todo: Make sure all detatil in design show up
        private void handleShowVehicleInformation(VehicleRecord i_VehicleRecord)
        {
            string ownerStr = string.Format("Vehicle's Owner: {0} {2} Vehicle's Owner Phone number: {1}", i_VehicleRecord.OwnerName, i_VehicleRecord.OwnerPhoneNumber, Environment.NewLine);

            printRecordStatus("Vehicle Garage Status:", i_VehicleRecord);
            BasicConsoleOperations.WriteString(ownerStr);
            

            VehiclePrintUtils.PrintVehicle(i_VehicleRecord.Vehicle);
        }

        private void handleModifyVehicleState(VehicleRecord i_VehicleRecord)
        {
            VehicleRecord.eStatus newStatus;

            printRecordStatus("Current Vehicle State:", i_VehicleRecord);
            //todo: test this. make sure if changing status to same or to invalid, throws?.. also make sure i catch that
            newStatus = BasicConsoleOperations.GetEnumChoice<VehicleRecord.eStatus>("Please choose new state:");
            i_VehicleRecord.Status = newStatus;
        }

        private void handleFillTireAir(VehicleRecord i_VehicleRecord)
        {
            foreach (GarageLogic.VehicleParts.Tire tire in i_VehicleRecord.Vehicle.Tires)
            {
                tire.InflateAir(tire.MaxPSI - tire.CurrentPSI);
            }
        }

        private void handleChargeCar(VehicleRecord i_VehicleRecord)
        {
            PowerSource powerSource = i_VehicleRecord.Vehicle.PowerSource;
            ElectricalSource electricSource;
            float timeToCharge;

            if (!(powerSource is ElectricalSource))
            {
                throw new ArgumentException();
            }

            electricSource = (ElectricalSource)powerSource;
            //todo: sure needs in hours?
            timeToCharge = BasicConsoleOperations.GetPositiveFloatFromUser("Please insert time to charge in hours:");
            electricSource.Recharge(timeToCharge);
        }

        private void handleRefuelCar(VehicleRecord i_VehicleRecord)
        {
            PowerSource powerSource = i_VehicleRecord.Vehicle.PowerSource;
            FuelSource fuelSource;
            FuelSource.eFuelType usedFuelType;
            float amountToFill;

            if (!(powerSource is FuelSource))
            {
                throw new ArgumentException();
            }

            fuelSource = (FuelSource)powerSource;
            usedFuelType = BasicConsoleOperations.GetEnumChoice<FuelSource.eFuelType>("Please choose fuel type:");
            amountToFill = BasicConsoleOperations.GetPositiveFloatFromUser("Please insert amount to fuel in liters:");
            fuelSource.Refuel(usedFuelType, amountToFill);
        }


        private List<VehicleRecord.eStatus> getVehicleStatusFilterInformation()
        {
            List<VehicleRecord.eStatus> filterList = new List<VehicleRecord.eStatus>();

            if (BasicConsoleOperations.PromptQuestion("Would you like to filter out some of the results?"))
            {
                filterList = BasicConsoleOperations.GetMultipleEnumChoices<VehicleRecord.eStatus>("Please choose out filters: (Multiple filters can be choosed my using a comma between each)");
            }

            return filterList;
        }
        //todo: user input for initial values
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
