namespace DataGeneratorApi.Models;

public class BikeSpeedDataItemDTO
{
    public int Speed { get; set; }
    public double Mileage { get; set; }
    public DateTime DateTime { get; set; }

    public BikeSpeedDataItemDTO(int Speed, double Mileage, DateTime DateTime) {
        this.Speed = Speed;
        this.Mileage = Mileage;
        this.DateTime = DateTime;
    }

}