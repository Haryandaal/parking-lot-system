namespace parking_system.Models;

public class Vehicle
{
    public string RegistrationNumber { get; }
    public string Color { get; }
    public string Type { get; }

    public Vehicle(string registrationNumber, string color, string type)
    {
        if (!type.Equals("Mobil", StringComparison.OrdinalIgnoreCase) && 
            !type.Equals("Motor", StringComparison.OrdinalIgnoreCase))
        {
            throw new Exception("Only small cars (Mobil) and Motorbikes (Motor) are allowed.");
        }

        RegistrationNumber = registrationNumber;
        Color = color;
        Type = type;
    }
}