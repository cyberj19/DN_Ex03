using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GarageLogic;
using GarageLogic.VehicleParts;
using GarageLogic.VehicleParts.PowerSources;
using ConsoleUI.Utils;
using GarageLogic.VehicleTypes;

namespace ConsoleUI
{
    class Garage
    {
        //todo:
        private const uint k_MaxPlateNumber = 9999999; // 7 digit number
        private GarageManager m_GarageManager = new GarageManager();
        private readonly VehicleFactory m_Factory = new VehicleFactory();
        static readonly PositiveRange sr_plateNumberRange = new PositiveRange(1, k_MaxPlateNumber);
        

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
                    BasicConsoleOperations.WriteString("Operation has failed!");
                    //todo: Write about operation failed
                }
                else
                {
                    BasicConsoleOperations.WriteString("Operation succeeded!");
                }

                BasicConsoleOperations.NewLine();
            }
        }

        private MenuOption.eOption getMenuOption()
        {
            string[] menuOptionsArray = MenuOption.StringArray;

            return MenuOption.Get(BasicConsoleOperations.GetOption("Choose a menu option:", menuOptionsArray));
        }

        private VehicleRecord getVehicleRecordFromLicensePlate()
        {
            GarageLogic.VehicleRecord retVehicle = null;
            bool isFirstTime = true;
            string outStr = "Please insert vehicle's plate number:";

            do
            {
                uint plateNumber = BasicConsoleOperations.GetPositiveNumberFromUser(outStr, sr_plateNumberRange);

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
            try //todo; support other types of exceptions aswell
            {
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
            }
            catch (ArgumentException aexcp)
            {
                BasicConsoleOperations.WriteString("Argument Exception: Bad arguments!");
                hasOperationSucceeded = false;
            }

            return hasOperationSucceeded;
        }

        const uint k_NumDigitsInPhoneNumber = 10;
        private void handleInsertNewVehicle()
        {
            uint plateNumber = BasicConsoleOperations.GetPositiveNumberFromUser("Please insert vehicle's plate number:", sr_plateNumberRange);
            VehicleRecord existingRecord = m_GarageManager.GetVehicleRecord(plateNumber.ToString());

            if (existingRecord != null)
            {
                BasicConsoleOperations.WriteString("Vehicle is already in garage. Moving to \"In Repair\"");
                existingRecord.Status = VehicleRecord.eStatus.InRepair;
            }
            else
            {
                string modelName = BasicConsoleOperations.GetString("Please insert model name:");
                VehicleRegistrationInfo registrationInfo = new VehicleRegistrationInfo(modelName, plateNumber.ToString());
                Vehicle createdVehicle = createVehicleFromUserInput(registrationInfo);
                string ownerName = BasicConsoleOperations.GetString("Please insert owner name:");
                string ownerPhoneNumber = BasicConsoleOperations.GetNumericStringOfLength(string.Format("Please insert owner's phone number ({0} digits):", k_NumDigitsInPhoneNumber), k_NumDigitsInPhoneNumber);

                m_GarageManager.InsertRecord(ownerName, ownerPhoneNumber, createdVehicle);
            }
        }

        //todo: reconsider writing better strings than those of the enum
        private Vehicle createVehicleFromUserInput(VehicleRegistrationInfo i_RegistrationInfo)
        {
            VehicleFactory.eSupportedVehicle selectedVehicle = BasicConsoleOperations.GetEnumChoice<VehicleFactory.eSupportedVehicle>("Please choose vehicle type:");
            Vehicle model = m_Factory.GetModel(selectedVehicle);
            object vehicleSpecificInfo = getUserSpecificInfo(model);
            TiresInfo tiresInfo = getUserTiresInfo(model);
            float initialPowerSourceValue = BasicConsoleOperations.GetPositiveFloatFromUserWithMaxVal(
                "Please insert initial power source value ", model.PowerSource.PowerCapacity
                );

            return m_Factory.Create(selectedVehicle, i_RegistrationInfo, vehicleSpecificInfo, initialPowerSourceValue, tiresInfo);
        }

        private object getUserSpecificInfo(Vehicle model)
        {
            object retObj = null;

            if (model is Car)
            {
                Car.eColor carColor = BasicConsoleOperations.GetEnumChoice<Car.eColor>("Please choose car color:");
                Car.eDoorsAmount doorAmount = BasicConsoleOperations.GetEnumChoice<Car.eDoorsAmount>("Please choose car door amounts:");

                retObj = new CarInfo(carColor, doorAmount);
            }
            else if (model is Motorcycle)
            {
                Motorcycle.eLicenseType licenseType = BasicConsoleOperations.GetEnumChoice<Motorcycle.eLicenseType>("Please choose license type:");
                int engineVolumeCC = (int)BasicConsoleOperations.GetPositiveNumberFromUser("Please insert engine volume cc:", new PositiveRange(0, 10000)); //todo: const..

                retObj = new MotorcycleInfo(licenseType, engineVolumeCC);
            }
            else if (model is Truck)
            {
                bool isCarryingDangerousMaterials = BasicConsoleOperations.PromptQuestion("Is carrying dangerous materials?");
                float maxCarryWeightAllowedKg = BasicConsoleOperations.GetPositiveFloatFromUserWithMaxVal("Please insert max allowed carrying weight in kg:", 100000); //todo: const..

                retObj = new TruckInfo(isCarryingDangerousMaterials, maxCarryWeightAllowedKg);
            }

            return retObj;
        }

        private TiresInfo getUserTiresInfo(Vehicle model)
        {
            int numTires = model.Tires.Count;
            string[] tiresManufacturerName = new string[numTires];
            float[] tiresInitialAirValue = new float[numTires];

            for (int i = 0; i < numTires; i++)
            {
                BasicConsoleOperations.WriteString("_______");
                BasicConsoleOperations.WriteString(string.Format("Entering data for tire number: {0}", i + 1));
                tiresManufacturerName[i] = BasicConsoleOperations.GetString("Please insert manufacturer name:");
                //todo: accessing first item, not sure if best way to do it..
                tiresInitialAirValue[i] = BasicConsoleOperations.GetPositiveFloatFromUserWithMaxVal("Please insert initial air value", model.Tires[0].MaxPSI);
            }

            return new TiresInfo(tiresManufacturerName, tiresInitialAirValue);
        }

        //GetPositiveFloatFromUserWithMaxVal

        private void printRecordStatus(string i_Msg, VehicleRecord i_VehicleRecord)
        {
            BasicConsoleOperations.WriteString(string.Format("{0} {1}", i_Msg, i_VehicleRecord.Status.ToString()));
        }

        //todo: Make sure all detatil in design show up
        private void handleShowVehicleInformation(VehicleRecord i_VehicleRecord)
        {
            string ownerStr = string.Format("Vehicle's Owner: {0}{2}Vehicle's Owner Phone number: {1}", i_VehicleRecord.OwnerName, i_VehicleRecord.OwnerPhoneNumber, Environment.NewLine);

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
