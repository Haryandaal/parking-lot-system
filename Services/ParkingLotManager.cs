using parking_system.Models;

namespace parking_system.Services;

public class ParkingLotManager
{
    private ParkingLot parkingLot;

    public void CreateParkingLot(int size)
    {
        parkingLot = new ParkingLot(size);
        Console.WriteLine($"Created a parking lot with {size} slots");
    }

    public void ParkVehicle(string regNo, string color, string type)
    {
        if (parkingLot == null) throw new Exception("Parking lot is not created yet.");
        parkingLot.Park(new Vehicle(regNo, color, type));
    }

    public void LeaveSlot(int slot)
    {
        if (parkingLot == null) throw new Exception("Parking lot is not created yet.");
        parkingLot.Leave(slot);
    }

    public void DisplayStatus()
    {
        if (parkingLot == null) throw new Exception("Parking lot is not created yet.");
        parkingLot.Status();
    }
}