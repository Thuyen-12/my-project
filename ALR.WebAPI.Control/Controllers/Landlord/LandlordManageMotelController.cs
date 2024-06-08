using ALR.Data.Dto;
using ALR.Services.Common.Extentions;
using ALR.Services.MainServices.Abstract.LandLordInterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static ALR.Data.Base.EnumBase;

namespace ALR.WebAPI.Control.Controllers.Landlord
{
    [Route("api/lanlord/[controller]")]
    [ApiController]
    [Authorize]
    public class LandlordManageMotelController : ControllerBase
    {
        private readonly ILandlordManageMotelServices _motelServices;
        private readonly IHttpContextAccessor _context;

        public LandlordManageMotelController(ILandlordManageMotelServices motelServices, IHttpContextAccessor context)
        {
            _motelServices = motelServices;
            _context = context;
        }

        [HttpGet]
        [Route("getallmotelbyId")]
        public async Task<IActionResult> GetAllMotelById(int startIndex, int pageSize)
        {
            
            var userId = _context.HttpContext.GetUserId();
            if(userId == null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
            var result = await _motelServices.GetAllMotelByLandlordID(Guid.Parse(userId),startIndex,pageSize);
            if(result == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            return StatusCode(StatusCodes.Status200OK,result);
        }

        [HttpGet]
        [Route("getmotelbyId")]
        public async Task<IActionResult> GetMotelByid(Guid motelId)
        {
            var motel = await _motelServices.GetMotelById(motelId);
            if(motel == null)
            {
                return NotFound();
            }
            return StatusCode(StatusCodes.Status200OK, motel);
        }

        [HttpPost]
        [Route("createnewmotel")]
        public async Task<IActionResult> CreateNewMotelId(CreateMotelDto dto)
        {
            var lanlordId =  _context.HttpContext.GetUserId();
            if (lanlordId == null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
            var result = await _motelServices.CreateNewMotel(dto, Guid.Parse(lanlordId));
            if(result.Equals(AlrResult.Failed))
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            return StatusCode(StatusCodes.Status201Created, result);
        }

        [HttpPut]
        [Route("updateMotel")]
        public async Task<IActionResult> UpdateMotel(Guid motelId, CreateMotelDto dto)
        {
            var result = await _motelServices.UpdateMotel(motelId, dto);
            if(result.Equals(AlrResult.Failed))
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            return StatusCode(StatusCodes.Status200OK,result);
        }

        [HttpDelete]
        [Route("deleteMotel")]
        public async Task<IActionResult> DeleteMotel(Guid motelId)
        {
            var result = await _motelServices.DeleteMotel(motelId);
            if(result.Equals(AlrResult.Failed))
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            return StatusCode(StatusCodes.Status200OK, result);
        }


    }
}
