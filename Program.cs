namespace ALBRIGHT_ASSIGNMENT_7_4
{
    internal class Program
    {
        // MSSA CCAD16 - 19DEC2024
        // CHRIS ALBRIGHT
        // ASSIGNMENT 7.4 - WEEK 7
        static void Main(string[] args)
        {
            //Assignment 7.4.1.---------------------------------------------------------------------------------------------

            // Implement shell sort on an unsorted array of numbers. Take the array input from user.

            char hold1 = 'y';
            do
            {
                Console.WriteLine("Assignment 7.4.1: ---------------------------------------------------------------------");
                Console.WriteLine("PARKING LOT:");

                ParkingLot lot = new ParkingLot(3, 5, 1);
                //Car small = new Small("A123");
                //Car medium = new Medium("B456");
                //Car large = new Large("C789");
                bool exit = false;
                while (!exit)
                {
                    Console.WriteLine($"Choose an option:");
                    Console.WriteLine($"1. Park a Car / 2. Remove a Car / 3. Exit:");
                    string choice = Console.ReadLine();
                    switch (choice)
                    {
                        case "1":
                            Console.WriteLine("Enter car size (small, medium, large):");
                            string size = Console.ReadLine().ToLower();
                            Console.WriteLine("Enter license plate:");
                            string licensePlate = Console.ReadLine();
                            Car car = null;
                            switch (size)
                            {
                                case "small":
                                    car = new Small(licensePlate);
                                    break;
                                case "car":
                                    car = new Medium(licensePlate);
                                    break;
                                case "bus":
                                    car = new Large(licensePlate);
                                    break;
                                default:
                                    Console.WriteLine("Invalid vehicle size.");
                                    continue;
                            }
                            if (lot.ParkCar(car))
                            {
                                Console.WriteLine("Car parked successfully!");
                            }
                            else
                            {
                                Console.WriteLine("No available spot for your car...");
                            }
                            break;
                        case "2":
                            Console.WriteLine("Enter license plate of the vehicle to remove:");
                            string plateToRemove = Console.ReadLine();
                            Car vehicleToRemove = null;

                            foreach (var spot in lot.spots)
                            {
                                if (spot.ParkedCar != null && spot.ParkedCar.LicensePlate == plateToRemove)
                                {
                                    vehicleToRemove = spot.ParkedCar;
                                    break;
                                }
                            }

                            if (vehicleToRemove != null)
                            {
                                lot.RemoveCar(vehicleToRemove);
                                Console.WriteLine("Vehicle removed successfully.");
                            }
                            else
                            {
                                Console.WriteLine("Vehicle not found.");
                            }
                            break;

                        case "3":
                            exit = true;
                            break;

                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
                Console.WriteLine($"\n\nWant to run 7.4.1 again? type y/n");
                hold1 = Console.ReadKey().KeyChar;
                hold1 = Char.ToLower(hold1);

            }
            while (hold1 == 'y');
        }
        //-----------------------------------------Classes------------------------------------------------
        enum CarSize
        {
            Small,
            Medium,
            Large
        }

        abstract class Car
        {
            public CarSize Size { get; protected set; }
            public string LicensePlate { get; private set; }

            protected Car(string licensePlate)
            {
                LicensePlate = licensePlate;
            }
        }

        class Small : Car
        {
            public Small(string licensePlate) : base(licensePlate)
            {
                Size = CarSize.Small;
            }
        }

        class Medium : Car
        {
            public Medium(string licensePlate) : base(licensePlate)
            {
                Size = CarSize.Medium;
            }
        }

        class Large : Car
        {
            public Large(string licensePlate) : base(licensePlate)
            {
                Size = CarSize.Large;
            }
        }

        class ParkingSpot
        {
            public CarSize SpotSize { get; private set; }
            public Car ParkedCar { get; private set; }

            public ParkingSpot(CarSize size)
            {
                SpotSize = size;
            }

            public bool CanFitCar(Car carParam)
            {
                return ParkedCar == null && carParam.Size <= SpotSize;
            }

            public bool AddCar(Car carParam)
            {
                if (CanFitCar(carParam))
                {
                    ParkedCar = carParam;
                    return true;
                }
                return false;
            }

            public void RemoveCar()
            {
                ParkedCar = null;
            }
        }

        class ParkingLot
        {
            public List<ParkingSpot> spots;

            public ParkingLot(int numSmallSpots, int numMediumSpots, int numLargeSpots)
            {
                spots = new List<ParkingSpot>();

                for (int i = 0; i < numSmallSpots; i++)
                {
                    spots.Add(new ParkingSpot(CarSize.Small));
                }
                for (int i = 0; i < numMediumSpots; i++)
                {
                    spots.Add(new ParkingSpot(CarSize.Medium));
                }
                for (int i = 0; i < numLargeSpots; i++)
                {
                    spots.Add(new ParkingSpot(CarSize.Large));
                }
            }

            public bool ParkCar(Car carParam)
            {
                foreach (var spot in spots)
                {
                    if (spot.CanFitCar(carParam))
                    {
                        return spot.AddCar(carParam);
                    }
                }
                return false;
            }

            public void RemoveCar(Car carParam)
            {
                foreach (var spot in spots)
                {
                    if (spot.ParkedCar == carParam)
                    {
                        spot.RemoveCar();
                        break;
                    }
                }
            }
        }
    }
}
