using ALR.Services.MainServices.Abstract.LandLordInterface;
using ALR.Services.MainServices.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ALR.WebAPI.Control.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchUserController : ControllerBase
    {

        private readonly IManageTenantService _manageTenantService;

       
        public SearchUserController(IManageTenantService manageTenantService)
        {
           
            _manageTenantService = manageTenantService;
           
        }
        [HttpGet]
        [Route("getlistuserbyname")]
        public async Task<IActionResult> GetListUserByName(string name)
        {
            var result = await _manageTenantService.GetUserByName(name);
            return Ok(result);
        }
    }
}
