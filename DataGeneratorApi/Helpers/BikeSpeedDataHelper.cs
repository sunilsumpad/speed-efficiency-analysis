using DataGeneratorApi.Models;

namespace DataGeneratorApi.BikeSpeedDataHelpers {
    public class BikeSpeedDataHelper {

        public List<BikeSpeedDataItemDTO> GenerateBikeSpeedData() {
            List<BikeSpeedDataItemDTO> bikeSpeedDataItemList = [];
            for(int i=0; i<10; i++) {
                int randomSpeed = RandomSpeedGenerator();
                BikeSpeedDataItemDTO bikeSpeedDataItem1 = new(randomSpeed, MileageGenerator(randomSpeed), TimeGenerator(i)); 
                bikeSpeedDataItemList.Add(bikeSpeedDataItem1);  
            }
            return bikeSpeedDataItemList;
        }

        private static int RandomSpeedGenerator() {
            Random r = new();
            int randomSpeed = r.Next(20, 100);
            return randomSpeed;
        }

        private static double MileageGenerator(int Speed) {
            double mileage;
            if(Speed > 60) {
                mileage = 100 - Speed;
            }
            else if(Speed >= 50 && Speed <= 60) {
                mileage = Speed * 1.25;
            }
            else if(Speed >= 30 && Speed < 50) {
                mileage = Speed * 2;
            }
            else {
                mileage = Speed + 10;
            }
            return mileage;
        }

        private static DateTime TimeGenerator(int count) {
            DateTime currentTime = DateTime.Now;
            return currentTime.AddSeconds(count * 60 * 5);    
        }
    }
}