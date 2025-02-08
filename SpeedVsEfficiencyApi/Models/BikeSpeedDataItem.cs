namespace SpeedVsEfficiencyApi.Models;

public class BikeSpeedDataItem
{
    public long Id { get; set; }
    public int Speed { get; set; }
    public double Mileage { get; set; }
    public DateTime DateTime { get; set; }

    public BikeSpeedDataItem(long Id, int Speed, double Mileage, DateTime DateTime) {
        this.Id = Id;
        this.Speed = Speed;
        this.Mileage = Mileage;
        this.DateTime = DateTime;
    }

}