using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GarageLogic;
using GarageLogic.VehicleParts;
using GarageLogic.VehicleParts.PowerSources;

namespace ConsoleUI.Utils
{
    class VehiclePrintUtils
    {
        public static void PrintVehicle(Vehicle i_Vehicle)
        {
            printGeneralVehicleInfo(i_Vehicle);
            printTiresInfo(i_Vehicle);
            printPowerSource(i_Vehicle);
        }

        private static void printPowerSource(Vehicle i_Vehicle)
        {
            PowerSource power = i_Vehicle.PowerSource;

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
            sourceStrBuilder.AppendFormat("Energy Percentage: {0} {1}", i_Source.EnergyPercent, Environment.NewLine);

            BasicConsoleOperations.WriteString(sourceStrBuilder.ToString());
        }


        //todo: units?
        private static void printElectricalPowerSource(ElectricalSource i_Source)
        {
            StringBuilder sourceStrBuilder = new StringBuilder();

            sourceStrBuilder.AppendLine("Power Source Type: Electrical");
            sourceStrBuilder.AppendFormat("Current Battery Power Level: {0} {1}", i_Source.CurrentPowerLevel, Environment.NewLine);
            sourceStrBuilder.AppendFormat("Battery Capacity: {0} {1}", i_Source.PowerCapacity, Environment.NewLine);
            sourceStrBuilder.AppendFormat("Energy Percentage: {0} {1}", i_Source.EnergyPercent, Environment.NewLine);

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
