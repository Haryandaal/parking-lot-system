using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        ParkingLot parkingLot = null;
        while (true)
        {
            var input = Console.ReadLine();
            var command = input.Split(' ');
            try
            {
                switch (command[0])
                {
                    case "create_parking_lot":
                        int size = int.Parse(command[1]);
                        parkingLot = new ParkingLot(size);
                        Console.WriteLine($"Created a parking lot with {size} slots");
                        break;

                    case "park":
                        if (parkingLot == null) throw new Exception("Parking lot is not created yet.");
                        string regNo = command[1];
                        string color = command[2];
                        string type = command[3];
                        parkingLot.Park(new Vehicle(regNo, color, type));
                        break;

                    case "leave":
                        if (parkingLot == null) throw new Exception("Parking lot is not created yet.");
                        int slot = int.Parse(command[1]);
                        parkingLot.Leave(slot);
                        break;

                    case "status":
                        if (parkingLot == null) throw new Exception("Parking lot is not created yet.");
                        parkingLot.Status();
                        break;

                    case "type_of_vehicles":
                        if (parkingLot == null) throw new Exception("Parking lot is not created yet.");
                        string vehicleType = command[1];
                        parkingLot.TypeOfVehicles(vehicleType);
                        break;

                    case "registration_numbers_for_vehicles_with_ood_plate":
                        if (parkingLot == null) throw new Exception("Parking lot is not created yet.");
                        parkingLot.RegistrationNumbersByOddPlate();
                        break;

                    case "registration_numbers_for_vehicles_with_event_plate":
                        if (parkingLot == null) throw new Exception("Parking lot is not created yet.");
                        parkingLot.RegistrationNumbersByEvenPlate();
                        break;

                    case "registration_numbers_for_vehicles_with_colour":
                        if (parkingLot == null) throw new Exception("Parking lot is not created yet.");
                        string regColor = command[1];
                        parkingLot.RegistrationNumbersByColor(regColor);
                        break;

                    case "slot_numbers_for_vehicles_with_colour":
                        if (parkingLot == null) throw new Exception("Parking lot is not created yet.");
                        string slotColor = command[1];
                        parkingLot.SlotNumbersByColor(slotColor);
                        break;

                    case "slot_number_for_registration_number":
                        if (parkingLot == null) throw new Exception("Parking lot is not created yet.");
                        string registrationNo = command[1];
                        parkingLot.SlotNumberByRegistration(registrationNo);
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

class ParkingLot
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

    public void TypeOfVehicles(string type)
    {
        var count = slots.Values.Count(v => v != null && v.Type.Equals(type, StringComparison.OrdinalIgnoreCase));
        Console.WriteLine(count);
    }

    public void RegistrationNumbersByOddPlate()
    {
        var oddPlates = slots.Values
            .Where(v => v != null && int.TryParse(v.RegistrationNumber.Split('-')[1], out int number) && number % 2 != 0)
            .Select(v => v.RegistrationNumber);
        Console.WriteLine(string.Join(", ", oddPlates));
    }

    public void RegistrationNumbersByEvenPlate()
    {
        var evenPlates = slots.Values
            .Where(v => v != null && int.TryParse(v.RegistrationNumber.Split('-')[1], out int number) && number % 2 == 0)
            .Select(v => v.RegistrationNumber);
        Console.WriteLine(string.Join(", ", evenPlates));
    }

    public void RegistrationNumbersByColor(string color)
    {
        var registrations = slots.Values
            .Where(v => v != null && v.Color.Equals(color, StringComparison.OrdinalIgnoreCase))
            .Select(v => v.RegistrationNumber);
        Console.WriteLine(string.Join(", ", registrations));
    }

    public void SlotNumbersByColor(string color)
    {
        var slotNumbers = slots
            .Where(s => s.Value != null && s.Value.Color.Equals(color, StringComparison.OrdinalIgnoreCase))
            .Select(s => s.Key);
        Console.WriteLine(string.Join(", ", slotNumbers));
    }

    public void SlotNumberByRegistration(string registrationNumber)
    {
        var slot = slots.FirstOrDefault(s => s.Value != null && s.Value.RegistrationNumber.Equals(registrationNumber, StringComparison.OrdinalIgnoreCase));
        if (slot.Key != 0)
        {
            Console.WriteLine(slot.Key);
        }
        else
        {
            Console.WriteLine("Not found");
        }
    }
}

class Vehicle
{
    public string RegistrationNumber { get; }
    public string Color { get; }
    public string Type { get; }

    public Vehicle(string registrationNumber, string color, string type)
    {
        if (!type.Equals("Mobil", StringComparison.OrdinalIgnoreCase) && !type.Equals("Motor", StringComparison.OrdinalIgnoreCase))
        {
            throw new Exception("Only small cars (Mobil) and Motorbikes (Motor) are allowed.");
        }

        RegistrationNumber = registrationNumber;
        Color = color;
        Type = type;
    }
}
