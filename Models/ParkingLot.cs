namespace parking_system.Models;

public class ParkingLot
{
    private readonly Dictionary<int, Vehicle> slots;
    private readonly int capacity;

    public ParkingLot(int size)
    {
        capacity = size;
        slots = new Dictionary<int, Vehicle>(size);
        for (int i = 1; i <= size; i++)
        {
            slots.Add(i, null);
        }
    }

    public void Park(Vehicle vehicle)
    {
        var emptySlot = slots.FirstOrDefault(s => s.Value == null);
        if (emptySlot.Key == 0)
        {
            Console.WriteLine("Sorry, parking lot is full");
        }
        else
        {
            slots[emptySlot.Key] = vehicle;
            Console.WriteLine($"Allocated slot number: {emptySlot.Key}");
        }
    }

    public void Leave(int slot)
    {
        if (slots.ContainsKey(slot) && slots[slot] != null)
        {
            slots[slot] = null;
            Console.WriteLine($"Slot number {slot} is free");
        }
        else
        {
            Console.WriteLine("Slot is already empty or does not exist.");
        }
    }

    public void Status()
    {
        Console.WriteLine("Slot\tNo.\t\tType\tRegistration No\tColour");
        foreach (var slot in slots)
        {
            if (slot.Value != null)
            {
                Console.WriteLine($"{slot.Key}\t{slot.Value.RegistrationNumber}\t{slot.Value.Type}\t{slot.Value.Color}");
            }
        }
    }
}