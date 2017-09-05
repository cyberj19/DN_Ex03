using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using ConsoleUI.Utils;
using GarageLogic;
using GarageLogic.VehicleParts;
using GarageLogic.VehicleParts.PowerSources;

namespace ConsoleUI.VehicleInputOutput
{
    static class VehicleOutput
    {
        // Print a vehicle instance
        public static void PrintVehicle(Vehicle i_Vehicle)
        {
            BasicConsoleOperations.WriteString(string.Empty);
            printGeneralVehicleInfo(i_Vehicle);
            printSpecificVehicleInfo(i_Vehicle);
            printTiresInfo(i_Vehicle);
            printPowerSource(i_Vehicle);
        }

        // print specific vehicle-type information
        private static void printSpecificVehicleInfo(Vehicle i_Vehicle)
        {
            StringBuilder outputBuilder = new StringBuilder();
            IList<PropertyInfo> props = new List<PropertyInfo>(i_Vehicle.GetType().GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance));

            outputBuilder.AppendFormat("Vehicle Type: {0}{1}", i_Vehicle.GetType().Name, Environment.NewLine);

            foreach (PropertyInfo prop in props)
            {
                object propValue = prop.GetValue(i_Vehicle, null);
                outputBuilder.AppendFormat("{0}: {1} {2}", BasicConsoleOperations.SplitCamelCaseString(prop.Name), propValue.ToString(), Environment.NewLine);
            }

            BasicConsoleOperations.WriteString(outputBuilder.ToString());
        }

        // print power source information
        private static void printPowerSource(Vehicle i_Vehicle)
        {
            printPowerSource(i_Vehicle.PowerSource);            
        }

        // print power source information
        private static void printPowerSource(PowerSource i_Source)
        {
            StringBuilder sourceStrBuilder = new StringBuilder();

            sourceStrBuilder.AppendFormat("Power Source Type: {0} {1}", i_Source.ToString(), Environment.NewLine);
            sourceStrBuilder.AppendFormat("Current Battery Power Level: {0} ({1}) {2}", i_Source.CurrentPowerLevel, i_Source.Units, Environment.NewLine);
            sourceStrBuilder.AppendFormat("PowerSource Capacity: {0} ({1}) {2}", i_Source.PowerCapacity, i_Source.Units, Environment.NewLine);
            sourceStrBuilder.AppendFormat("Energy Percentage: {0:P2} ({1}) {2}", i_Source.EnergyPercent, i_Source.Units, Environment.NewLine);

            BasicConsoleOperations.WriteString(sourceStrBuilder.ToString());
        }

        // Print tires information
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

        // Print general vehicle information
        private static void printGeneralVehicleInfo(Vehicle i_Vehicle)
        {
            string generalInfoStr = string.Format("Model Name: {0}", i_Vehicle.ModelName);

            BasicConsoleOperations.WriteString(generalInfoStr);
        }
    }
}
