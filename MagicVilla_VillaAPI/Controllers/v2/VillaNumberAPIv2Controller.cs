using Microsoft.AspNetCore.Authorization;

namespace MagicVilla_VillaAPI.Controllers.v2
{
    [ApiController]
    [Route("api/v{version:apiVersion}/VillaNumberAPIv2")]
    [ApiVersion("2.0")]
    public class VillaNumberAPIv2Controller : ControllerBase
    {
        protected readonly APIResponse _response;
        private readonly IVillaNumberRepository _dbVillaNumber;
        private readonly IMapper _mapper;
        private readonly IVillaRepository _dbVilla;

        public VillaNumberAPIv2Controller(IVillaNumberRepository dbVillaNumber, IMapper mapper, IVillaRepository dbVilla)
        {
            _dbVillaNumber = dbVillaNumber;
            _mapper = mapper;
            _dbVilla = dbVilla;
            _response = new();
        }



        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
    }
}
