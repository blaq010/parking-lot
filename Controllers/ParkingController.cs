using Microsoft.AspNetCore.Mvc;
using parkingLot_backend.Models;

[ApiController]
[Route("api/parking")]
public class ParkingController : ControllerBase
{
    private readonly List<ParkingLot> parkingLots;

    public ParkingController()
    {
        // Initialize parking lots with their configurations
        parkingLots = new List<ParkingLot>
        {
            new ParkingLot
            {
                Name = "Small motorcycle/scooter parking lot",
                AvailableSpots = new Dictionary<string, int>
                {
                    { "Motorcycles/scooters", 2 },
                    { "Cars/SUVs/Buses/Trucks", 0 }
                },
                FeeModel = new Dictionary<string, decimal>()
            },
            new ParkingLot
            {
                Name = "Mall parking lot",
                AvailableSpots = new Dictionary<string, int>
                {
                    { "Motorcycles/scooters", 100 },
                    { "Cars/SUVs", 80 },
                    { "Buses/Trucks", 10 }
                },
                FeeModel = new Dictionary<string, decimal>
                {
                    { "Motorcycle-3h30m", 42 },
                    { "Car-SUV-11h30m", 0 } // Add your fee model here
                }
            },
            new ParkingLot
            {
                Name = "Stadium Parking Lot",
                AvailableSpots = new Dictionary<string, int>
                {
                    { "Motorcycles/scooters", 1000 },
                    { "Cars/SUVs", 1500 }
                },
                FeeModel = new Dictionary<string, decimal>
                {
                    { "Motorcycle-3h40m", 30 },
                    { "Motorcycle-14h59m", 390 },
                    { "Electric SUV-11h30m", 180 } // Add your fee model here
                }
            },
            new ParkingLot
            {
                Name = "Airport Parking Lot",
                AvailableSpots = new Dictionary<string, int>
                {
                    { "Motorcycles/scooters", 200 },
                    { "Cars/SUVs", 500 },
                    { "Buses/Trucks", 100 }
                },
                FeeModel = new Dictionary<string, decimal>
                {
                    { "Motorcycle-55m", 0 },
                    { "Motorcycle-14h59m", 60 },
                    { "Motorcycle-1d12h", 160 },
                    { "Car-50m", 60 },
                    { "SUV-23h59m", 80 },
                    { "Car-3d1h", 400 } // Add your fee model here
                }
            }
        };
    }

    [HttpGet("parking-lots")]
    public ActionResult<List<ParkingLot>> GetParkingLots()
    {
        return Ok(parkingLots);
    }

    [HttpPost("calculate-fee")]
  public ActionResult<decimal> CalculateFee(VehicleType vehicle)
{
    var parkingLot = parkingLots.FirstOrDefault(pl => pl.AvailableSpots != null && pl.AvailableSpots.ContainsKey(vehicle.Type));
    if (parkingLot == null)
        return NotFound("Parking lot not found.");

    var feeModelKey = $"{vehicle.Type}-{vehicle.DurationInMinutes}m";
    if (parkingLot.FeeModel != null && parkingLot.FeeModel.TryGetValue(feeModelKey, out var fee))
        return Ok(fee);

    return NotFound("Fee model not found for the given vehicle and duration.");
}
}