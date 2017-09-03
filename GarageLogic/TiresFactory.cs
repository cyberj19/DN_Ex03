using System.Collections.Generic;
using GarageLogic.VehicleParts;
using System;

namespace GarageLogic
{
    class TiresFactory
    {
        //todo: staic is ok?..
        public static List<Tire> ProduceTires(string i_ManufacturerName, int i_TiresAmount, float i_MaxAllowedAirPressure)
        {
            List<Tire> tires = new List<Tire>(i_TiresAmount);

            for(int i = 0; i< tires.Count; ++i)
            {
                tires[i] = new Tire(i_MaxAllowedAirPressure, i_ManufacturerName);
            }

            return tires;
        }

        public static List<Tire> ProductTiresAccordingToExisting(string[] i_NewManufacturerNames, List<Tire> i_ExistingTires)
        {
            List<Tire> newTires = new List<Tire>(i_NewManufacturerNames.Length);

            if (i_ExistingTires.Count != i_NewManufacturerNames.Length)
            {
                throw new ArgumentException();
            }

            for (int i = 0; i < i_NewManufacturerNames.Length; i++)
            {
                newTires.Add(new Tire(i_ExistingTires[i].MaxPSI, i_NewManufacturerNames[i]));
            }

            return newTires;
        }
    }
}
