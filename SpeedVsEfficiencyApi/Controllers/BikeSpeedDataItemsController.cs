using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpeedVsEfficiencyApi.Models;

namespace SpeedVsEfficiencyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BikeSpeedDataItemsController : ControllerBase
    {
        private readonly BikeSpeedDataContext _context;

        public BikeSpeedDataItemsController(BikeSpeedDataContext context)
        {
            _context = context;
        }

        // GET: api/BikeSpeedDataItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BikeSpeedDataItem>>> GetBikeSpeedDataItems()
        {
            return await _context.BikeSpeedDataItems.ToListAsync();
        }

        // GET: api/BikeSpeedDataItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BikeSpeedDataItem>> GetBikeSpeedDataItem(long id)
        {
            var bikeSpeedDataItem = await _context.BikeSpeedDataItems.FindAsync(id);

            if (bikeSpeedDataItem == null)
            {
                return NotFound();
            }

            return bikeSpeedDataItem;
        }
        
        // POST: api/BikeSpeedDataItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BikeSpeedDataItem>> PostBikeSpeedDataItem(BikeSpeedDataItem bikeSpeedDataItem)
        {
            _context.BikeSpeedDataItems.Add(bikeSpeedDataItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBikeSpeedDataItem), new { id = bikeSpeedDataItem.Id }, bikeSpeedDataItem);
        }
    }
}
