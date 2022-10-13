﻿

using MagicVilla_VillaAPI.Logging;

namespace MagicVilla_VillaAPI.Controllers
{
    [ApiController]
    [Route("api/VillaAPI")]
    public class VillaAPIController : ControllerBase
    {
        private readonly ILogging _logger;

        public VillaAPIController(ILogging logger)
        {
            _logger = logger;
        }



        [HttpGet]
        [ProducesResponseType(200)]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            _logger.Log("Getting all villas!", "");
            return Ok(VillaStore.villaList);
        }



        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<VillaDTO> GetVilla(int id)
        {
            if(id == 0)
            {
                _logger.Log("Villa id == 0", "error");
                return BadRequest();
            }

            var villa = VillaStore.villaList.FirstOrDefault(x => x.Id == id);

            if(villa == null)
            {
                return NotFound();
            }
            return Ok(villa);
        }



        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(201)]
        public ActionResult<VillaDTO> CreateVilla([FromBody] VillaDTO villaDTO)
        {
            if(villaDTO == null)
            {
                return BadRequest(villaDTO);
            }
            if(villaDTO.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            villaDTO.Id = VillaStore.villaList.OrderByDescending(x => x.Id).FirstOrDefault().Id + 1;
            VillaStore.villaList.Add(villaDTO);

            return CreatedAtRoute("GetVilla", new { id = villaDTO.Id}, villaDTO);
        }



        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        public IActionResult DeleteVilla(int id)
        {
            if(id == 0)
            {
                _logger.Log("Villa == 0", "warning");
                return BadRequest();
            }

            var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);

            if(villa == null)
            {
                return NotFound();
            }

            VillaStore.villaList.Remove(villa);

            return NoContent();
        }



        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [HttpPut("{id:int}", Name = "UpdateVilla")]
        public IActionResult UpdateVilla(int id, [FromBody] VillaDTO villaDTO)
        {
            if(villaDTO == null || id != villaDTO.Id)
            {
                return BadRequest();
            }

            var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);

            villa.Name = villaDTO.Name;
            villa.Sqft = villaDTO.Sqft;
            villa.Occupancy = villaDTO.Occupancy;

            return NoContent();
        }


            
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDTO> patchDTO)
        {
            if(id == 0)
            {
                return BadRequest();
            }

            var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);

            if(villa == null)
            {
                return NotFound();
            }    

            patchDTO.ApplyTo(villa, ModelState);

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return NoContent();
        }
    }
}
