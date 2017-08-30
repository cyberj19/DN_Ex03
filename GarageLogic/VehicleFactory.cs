using System.Collections.Generic;

namespace GarageLogic
{
    class VehicleFactory
    {
        public enum eSupportedVehicle //todo: reconsider
        {
            ElectricCar,
            ElectricMotorcycle,
            RegularCar,
            RegularMotorcycle,
            Truck
        }

        Dictionary<Vehicle, PowerSource> m_CarRecipes; //todo:reconsider

        public void CreateVehicle(eSupportedVehicle i_Vehicle) //todo: reconsider
        {
            
        }
        public Car MakeCar()
        {
            const FuelSource.eFuelType k_CarFuelType = FuelSource.eFuelType.Octan98;
            const float k_CarTankCapacity = 50f;

            return new Car(new FuelSource(k_CarFuelType, k_CarTankCapacity));
        }
        public Truck MakeTrack()
        {
            const FuelSource.eFuelType k_TrackFuelType = FuelSource.eFuelType.Soler;
            const float k_TruckTankCapacity = 130f;

            return new Truck(new FuelSource(k_TrackFuelType, k_TruckTankCapacity));
        }
        public Motorcycle MakeMotorcycle()
        {
            const FuelSource.eFuelType k_MotorcycleFuelType = FuelSource.eFuelType.Octan95;
            const float k_MotorcycleTankCapacity = 5.5f;

            return new Motorcycle(new FuelSource(k_MotorcycleFuelType, k_MotorcycleTankCapacity));
        }
        public Car MakeElectricCar()
        {
            const float k_MaxBattaryCapacityInHours = 2.8f;

            return new Car(new ElectricalSource(k_MaxBattaryCapacityInHours));
        }
        public Motorcycle MakeElectricMotorcycle()
        {
            const float k_MaxBattaryCapacityInHours = 1.6f;

            return new Motorcycle(new ElectricalSource(k_MaxBattaryCapacityInHours));
        }
    }
}
