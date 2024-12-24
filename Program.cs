using parking_system.Services;

class Program
{
    static void Main(string[] args)
    {
        ParkingLotManager parkingLotManager = new ParkingLotManager();

        while (true)
        {
            var input = Console.ReadLine();
            var command = input.Split(' ');
            try
            {
                switch (command[0])
                {
                    case "create_parking_lot":
                        parkingLotManager.CreateParkingLot(int.Parse(command[1]));
                        break;

                    case "park":
                        parkingLotManager.ParkVehicle(command[1], command[2], command[3]);
                        break;

                    case "leave":
                        parkingLotManager.LeaveSlot(int.Parse(command[1]));
                        break;

                    case "status":
                        parkingLotManager.DisplayStatus();
                        break;

                    case "exit":
                        return;

                    default:
                        Console.WriteLine("Invalid command");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
