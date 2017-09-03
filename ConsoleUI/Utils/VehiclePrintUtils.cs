using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GarageLogic;
using GarageLogic.VehicleParts;
using GarageLogic.VehicleParts.PowerSources;
using GarageLogic.VehicleTypes;

namespace ConsoleUI.Utils
{
    class VehiclePrintUtils
    {
        public static void PrintVehicle(Vehicle i_Vehicle)
        {
            BasicConsoleOperations.WriteString(string.Empty);
            printGeneralVehicleInfo(i_Vehicle);
            printSpecificVehicleInfo(i_Vehicle);
            printTiresInfo(i_Vehicle);
            printPowerSource(i_Vehicle);
        }

        private static void printSpecificVehicleInfo(Vehicle i_Vehicle)
        {
            StringBuilder outputBuilder = new StringBuilder();
            Car car = i_Vehicle as Car;
            Truck truck = i_Vehicle as Truck;
            Motorcycle motorcycle = i_Vehicle as Motorcycle;

            if (car != null)
            {
                outputBuilder.AppendLine("Vehicle Type: Car");
                outputBuilder.AppendFormat("Color: {1} {0}Doors Amount: {2} {0}", Environment.NewLine, car.Color.ToString(), car.DoorsAmount.ToString());
            }
            else if (truck != null)
            {
                outputBuilder.AppendLine("Vehicle Type: Truck");
                outputBuilder.AppendFormat("Is carrying dangerous materials: {1} {0}Max allowed carrying weight in Kg: {2} {0}", Environment.NewLine, truck.IsCarryingDangerousMaterials.ToString(), truck.MaxCarryingWeightAllowedKg.ToString());

            }
            else if (motorcycle != null)
            {
                outputBuilder.AppendLine("Vehicle Type: Motorcycle");
                outputBuilder.AppendFormat("License type: {1} {0}Engine Volume CC: {2} {0}", Environment.NewLine, motorcycle.LicenseType.ToString(), motorcycle.EngineVolumeCC.ToString());
            }

            BasicConsoleOperations.WriteString(outputBuilder.ToString());
        }

        private static void printPowerSource(Vehicle i_Vehicle)
        {
            PowerSource power = i_Vehicle.PowerSource;
            //todo: doing it twice!!!!! use as instead . here and everywhere else. according to video
            if (power is ElectricalSource)
            {
                printElectricalPowerSource((ElectricalSource)power);
            }
            else if (power is FuelSource)
            {
                printFuelPowerSource((FuelSource)power);
            }
            //todo: else?
        }

        //todo: reuse the code by electric..
        private static void printFuelPowerSource(FuelSource i_Source)
        {
            StringBuilder sourceStrBuilder = new StringBuilder();

            sourceStrBuilder.AppendLine("Power Source Type: Fuel");
            sourceStrBuilder.AppendFormat("Fuel Type: {0} {1}", i_Source.FuelType.ToString(), Environment.NewLine);
            sourceStrBuilder.AppendFormat("Current Fuel Level: {0} {1}", i_Source.CurrentPowerLevel, Environment.NewLine);
            sourceStrBuilder.AppendFormat("Fuel Capacity: {0} {1}", i_Source.PowerCapacity, Environment.NewLine);
            sourceStrBuilder.AppendFormat("Energy Percentage: {0:P2} {1}", i_Source.EnergyPercent, Environment.NewLine);

            BasicConsoleOperations.WriteString(sourceStrBuilder.ToString());
        }


        //todo: units?
        private static void printElectricalPowerSource(ElectricalSource i_Source)
        {
            StringBuilder sourceStrBuilder = new StringBuilder();

            sourceStrBuilder.AppendLine("Power Source Type: Electrical");
            sourceStrBuilder.AppendFormat("Current Battery Power Level: {0} {1}", i_Source.CurrentPowerLevel, Environment.NewLine);
            sourceStrBuilder.AppendFormat("Battery Capacity: {0} {1}", i_Source.PowerCapacity, Environment.NewLine);
            sourceStrBuilder.AppendFormat("Energy Percentage: {0:P2} {1}", i_Source.EnergyPercent, Environment.NewLine);

            BasicConsoleOperations.WriteString(sourceStrBuilder.ToString());
        }

        private static void printTiresInfo(Vehicle i_Vehicle)
        {
            StringBuilder tiresInfoBuilder = new StringBuilder();

            tiresInfoBuilder.AppendLine("Tires:");
            foreach (Tire tire in i_Vehicle.Tires)
            {
                tiresInfoBuilder.Append("\t Manufacture Name:");
                tiresInfoBuilder.AppendLine(tire.ManufacturerName);
                tiresInfoBuilder.Append("\t Pressure:");
                tiresInfoBuilder.AppendLine(tire.CurrentPSI.ToString());
                tiresInfoBuilder.Append("\t Max Pressure:");
                tiresInfoBuilder.AppendLine(tire.MaxPSI.ToString());
                tiresInfoBuilder.AppendLine(string.Empty);
            }

            BasicConsoleOperations.WriteString(tiresInfoBuilder.ToString());
        }

        private static void printGeneralVehicleInfo(Vehicle i_Vehicle)
        {
            string generalInfoStr = string.Format("Model Name: {0}", i_Vehicle.ModelName);

            BasicConsoleOperations.WriteString(generalInfoStr);
        }
    }
}
