using System;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VaultKeepsController : ControllerBase
    {
        private readonly VaultKeepService _vks;
        public VaultKeepsController(VaultKeepService vks)
        {
            _vks = vks;


        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<string>> Create([FromBody] VaultKeep newVaultKeep)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                newVaultKeep.CreatorId = userInfo.Id;
                _vks.Create(newVaultKeep);
                return Ok("Success");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<VaultKeep>> Delete(int id)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                return Ok(_vks.Delete(id, userInfo));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



    }
}