using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    class MenuOption
    {//todo; There's a class doing this.. but without the strings
        public enum eOption
        {
            InsertNewVehicle = 0,
            ShowAllVehiclesId,
            ModifyVehicleState,
            FillTireAir,
            RefuelVehicle,
            ChargeVehicle,
            ShowVehicleInformation,
            Exit
        }

        private const uint k_NumItems = (uint)eOption.Exit - (uint)eOption.InsertNewVehicle + 1;

        //todo: better way for the amount of items..
        private static readonly string[] sr_OptionsStrArray = new string[]
        {
            "Insert A New Vehicle",
            "Show All Vehicle's Licensing Plate",
            "Modify Vehicle State",
            "Fill Tire's Air",
            "Refuel Vehicle",
            "Charge Vehicle",
            "Show Vehicle's Information",
            "Exit"
        };

        public static string[] StringArray
        {
            get
            {
                return sr_OptionsStrArray;
            }
        }

        public static uint Length
        {
            get
            {
                return k_NumItems;
            }
        }

        public static eOption Get(uint i_Option)
        {
            return (eOption)i_Option;
        }
    }
}
