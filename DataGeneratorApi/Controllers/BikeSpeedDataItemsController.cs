using Microsoft.AspNetCore.Mvc;
using DataGeneratorApi.Models;
using System.Text.Json;
using DataGeneratorApi.BikeSpeedDataHelpers;
using System.Text;
using Microsoft.Extensions.Options;

namespace DataGeneratorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BikeSpeedDataItemsController : ControllerBase
    {
        private readonly BikeSpeedDataContext _context;
        private readonly CustomSettings _customSettings;

        public BikeSpeedDataItemsController(BikeSpeedDataContext context, IOptions<CustomSettings> options)
        {
            _context = context;
            _customSettings = options.Value;  // Get settings from configuration
        }

        // POST: api/BikeSpeedDataItems
        [HttpPost]
        public async Task<ActionResult<BikeSpeedDataItem>> PostBikeSpeedDataItem(BikeSpeedDataItem bikeSpeedDataItem)
        {
            List<BikeSpeedDataItemDTO> bikeSpeedDataItemList = new BikeSpeedDataHelper().GenerateBikeSpeedData();
            await PushDataToBackend(bikeSpeedDataItemList);

            return Content(JsonSerializer.Serialize(bikeSpeedDataItemList), "application/json");
        }

        private async Task PushDataToBackend(List<BikeSpeedDataItemDTO> bikeSpeedDataItemList)
        {
            for (int i = 0; i < bikeSpeedDataItemList.Count; i++)
            {
                var json = JsonSerializer.Serialize(bikeSpeedDataItemList[i]);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var url = _customSettings.SpeedVsEfficiencyWebApiBaseUrl + "/api/BikeSpeedDataItems";
                using var client = new HttpClient();

                var response = await client.PostAsync(url, data);
                string result = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(result);
            }
        }
    }
}
