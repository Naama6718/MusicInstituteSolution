using Microsoft.AspNetCore.Mvc;
using MusicInstitute.BL.Api;
using MusicInstitute.BL.Mapping;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicInstitute.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstrumentController : ControllerBase
    {
        private readonly Instrument_Manager_BL _instrumentManagerBL;

        public InstrumentController(Instrument_Manager_BL instrumentManagerBL)
        {
            _instrumentManagerBL = instrumentManagerBL;
        }

        // GET api/instrument
        [HttpGet]
        public async Task<ActionResult<List<InstrumentDTO>>> GetAllInstruments()
        {
            var instruments = await _instrumentManagerBL.GetAllInstruments();
            return Ok(instruments);
        }

        // POST api/instrument
        [HttpPost]
        public async Task<ActionResult> AddInstrument([FromBody] InstrumentDTO instrumentDTO)
        {
            await _instrumentManagerBL.AddInstrumentAsync(instrumentDTO);
            return CreatedAtAction(nameof(GetAllInstruments), new { id = instrumentDTO.InstrumentId }, instrumentDTO);
        }

        // DELETE api/instrument/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteInstrument(int id)
        {
            await _instrumentManagerBL.DeleteInstrumentAsync(id);
            return NoContent();
        }
    }
}
