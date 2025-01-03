using System;

namespace LearningCSharp
{
    // Root class: Vehicle (Base Class)
    public class Vehicle
    {
        public string Maker;
        public int Year;

        public void Start()
        {
            Console.WriteLine("Vehicle is starting...");
        }
        public void Stop()
        {
            Console.WriteLine("Vehicle is stopping...");
        }
    }

    // Single Inheritance: Car inherits from Vehicle
    public class Car : Vehicle
    {
        public string CarType;

        public void Drive()
        {
            Console.WriteLine("Car is driving...");
        }
    }

    // Hierarchical Inheritance: Bus inherits from Vehicle
    public class Bus : Vehicle
    {
        public int SeatingCapacity;

        public void TakePassengers()
        {
            Console.WriteLine("Passengers are Takeing the bus...");
        }
    }
    public class Plane : Vehicle
    {
        public int NumberOfEngines;

        public void Fly()
        {
            Console.WriteLine("Plane is flying...");
        }
    }

    // Multilevel Inheritance: ElectricCar inherits from Car (derived class of Vehicle)
    public class ElectricCar : Car
    {
        public int BatteryCapacity;

        public void Charge()
        {
            Console.WriteLine("Charging electric car...");
        }
    }

    // Hybrid Inheritance: PetrolCar inherits from Car and Vehicle (simulated via interfaces)
    public interface IFuelPowered
    {
        void Refuel();
    }

    public class PetrolCar : Car, IFuelPowered
    {
        public void Refuel()
        {
            Console.WriteLine("Refueling the petrol car...");
        }
    }

    internal class InheritanceDemo
    {
        public static void RunInheritanceDemo()
        {
            // Demonstrating Single Inheritance
            Console.WriteLine("=== Single Inheritance: Car ===");
            Car myCar = new Car { Maker = "Toyota", Year = 2022, CarType = "Sedan" };
            myCar.Start();
            myCar.Drive();
            myCar.Stop();

            // Demonstrating Multilevel Inheritance
            Console.WriteLine("\n=== Multilevel Inheritance: Electric Car ===");
            ElectricCar myElectricCar = new ElectricCar { Maker = "Tesla", Year = 2023, CarType = "Electric", BatteryCapacity = 100 };
            myElectricCar.Start();
            myElectricCar.Drive();
            myElectricCar.Charge();
            myElectricCar.Stop();

            // Demonstrating Hierarchical Inheritance
            Console.WriteLine("\n=== Hierarchical Inheritance: Plane ===");
            Plane myPlane = new Plane { Maker = "Boeing", Year = 2020, NumberOfEngines = 4 };
            myPlane.Start();
            myPlane.Fly();
            myPlane.Stop();

            // Demonstrating Hybrid Inheritance (using interface)
            Console.WriteLine("\n=== Hybrid Inheritance: Petrol Car ===");
            PetrolCar myPetrolCar = new PetrolCar { Maker = "Ford", Year = 2021, CarType = "SUV" };
            myPetrolCar.Start();
            myPetrolCar.Drive();
            myPetrolCar.Refuel();
            myPetrolCar.Stop();

            // Demonstrating Multilevel Inheritance: Bus
            Console.WriteLine("\n=== Multilevel Inheritance: Bus ===");
            Bus myBus = new Bus { Maker = "Mercedes", Year = 2019, SeatingCapacity = 50 };
            myBus.Start();
            myBus.TakePassengers();
            myBus.Stop();
        }
    }
}
