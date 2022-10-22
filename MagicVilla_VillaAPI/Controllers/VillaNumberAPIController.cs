using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace MagicVilla_VillaAPI.Controllers
{
    [ApiController]
    [Route("api/VillaNumberAPI")]
    public class VillaNumberAPIController : ControllerBase
    {
        protected readonly APIResponse _response;
        private readonly IVillaNumberRepository _dbVilla;
        private readonly IMapper _mapper;

        public VillaNumberAPIController(IVillaNumberRepository dbVilla, IMapper mapper)
        {
            _dbVilla = dbVilla;
            _mapper = mapper;
            this._response = new();
        }



        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<APIResponse>> GetVillaNumbers()
        {
            try
            {
                IEnumerable<VillaNumber> villaNumbers = await _dbVilla.GetAllAsync();

                _response.Result = _mapper.Map<List<VillaNumberDTO>>(villaNumbers);
                _response.StatusCode = HttpStatusCode.OK;

                return Ok(_response);
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                    = new List<string>() { ex.ToString() };
            }
            return _response;   
        }
    }
}
