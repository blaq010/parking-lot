
namespace parkingLot_backend.Models;
public class ParkingLot
{
    public string? Name { get; set; }
    public Dictionary<string, int>? AvailableSpots { get; set; }
    public Dictionary<string, decimal>? FeeModel { get; set; }
}
