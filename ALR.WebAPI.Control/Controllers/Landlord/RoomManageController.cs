using ALR.Data.Dto;
using ALR.Services.Common.Extentions;
using ALR.Services.MainServices.Abstract.LandLordInterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static ALR.Data.Base.EnumBase;

namespace ALR.WebAPI.Control.Controllers.Landlord
{
    [Route("api/[controller]/landlord")]
    [ApiController]
    public class RoomManageController : ControllerBase
    {
        private readonly ILandlordRoomServices _services;
        private readonly IHttpContextAccessor _context;
        public RoomManageController(ILandlordRoomServices services,
            IHttpContextAccessor context)
        {
            _context = context;
            _services = services;
        }

        [Route("create")]
        [HttpPost]
        public async Task<IActionResult> CreateNewRoom(Guid motelId, RoomDto dto)
        {
            var result = await _services.CreateNewRoom(motelId, dto);
            if (result.Item1 == null || result.Item2.Equals(AlrResult.Failed))
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            return StatusCode(StatusCodes.Status201Created, result.Item1);
        }

        [Route("updateroom")]
        [HttpPut]
        public async Task<IActionResult> UpdateRoom(RoomDto dto, Guid roomId)
        {
            var result = await _services.UpdateRoom(dto, roomId);
            if (result.Equals(AlrResult.Failed))
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [Route("addtenatintoroom")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> AddTenantIntoRoom(string phoneNumber, Guid roomId)
        {
            var result = await _services.AddTenantIntoRoom(phoneNumber, roomId);
            if (result.Item1 == null || result.Item2.Equals(AlrResult.Failed))
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            return StatusCode(StatusCodes.Status200OK, result.Item1);
        }

        [Route("delete")]
        [HttpDelete]
        public async Task<IActionResult> DeleteRoom(Guid roomId)
        {
            var result = await _services.DeleteRoom(roomId);
            if (result.Equals(AlrResult.Failed))
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [Route("searchRoom")]
        [HttpGet]
        public async Task<IActionResult> SearchRoom(Guid motelId, string roomNumber, float minPrice, float maxPrice, int status)
        {
            var result = await _services.GetRoomByCondition(motelId, roomNumber, minPrice, maxPrice, status);
            if (result.Item1 == null || result.Item2.Equals(AlrResult.Failed))
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            return StatusCode(StatusCodes.Status200OK, result.Item1);
        }

        [Route("searchRoomById")]
        [HttpGet]
        public async Task<IActionResult> SearchRoomByID(Guid roomId)
        {
            var result = await _services.GetRoomByID(roomId);
            if (result.Item1 == null || result.Item2.Equals(AlrResult.Failed))
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            return StatusCode(StatusCodes.Status200OK, result.Item1);
        }

        [Route("getRoomByMotelId")]
        [HttpGet]

        public async Task<IActionResult> SearchRoomByMotelId(Guid motelId, int startIndex, int pageSize)
        {
            var result = await _services.GetListRoomByMotelID(motelId, startIndex, pageSize);

            return StatusCode(StatusCodes.Status200OK, result);
        }
        [Route("GetMotelByLandlordID")]
        [HttpGet]
        public async Task<IActionResult> GetMotelByLandlordID(Guid id, int startindex, int pagesize)
        {
            var allPost = await _services.GetListMotelByLandlordId(id, startindex, pagesize);
            return Ok(allPost);
        }

        [Route("GetAllListMotel")]
        [HttpGet]
        public async Task<IActionResult> GetAllListMotel()
        {
            var userId = _context.HttpContext.GetUserId();
            if (userId == null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
            var allPost = await _services.GetAllListMotel(Guid.Parse(userId));
            return Ok(allPost);
        }

        [Route("getRoomByMotelIdNoPaging")]
        [HttpGet]

        public async Task<IActionResult> SearchRoomByMotelIdNoPaging(Guid motelId)
        {
            var result = await _services.GetListRoomByMotelIDNoPaging(motelId);

            return StatusCode(StatusCodes.Status200OK, result);
        }
    }
}
