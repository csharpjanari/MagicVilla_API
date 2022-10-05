

namespace MagicVilla_VillaAPI.Controllers
{
    [ApiController]
    [Route("api/VillaAPI")]
    public class VillaAPIController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<VillaDTO> GetVillas()
        {
            return new List<VillaDTO>
            {
                new VillaDTO {Id = 1, Name = "First Villa"},
                new VillaDTO { Id = 2, Name = "Second Villa"}
            };
        }
    }
}
