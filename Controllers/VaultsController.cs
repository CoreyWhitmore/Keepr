using System;
using System.Collections.Generic;
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
    public class VaultsController : ControllerBase
    {
        private readonly VaultsService _vs;
        private readonly KeepsService _ks;
        private readonly VaultKeepService _vks;
        public VaultsController(VaultsService vs, KeepsService ks, VaultKeepService vks)
        {
            _vs = vs;
            _ks = ks;
            _vks = vks;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Vault>> GetByIdAsync(int id)
        {
            try
            {
                Profile userInfo;
                try
                {
                    userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                }
                catch
                {
                    userInfo = null;
                }
                return Ok(_vs.GetById(id, userInfo));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{vaultId}/keeps")]
        public async Task<ActionResult<IEnumerable<VaultKeepViewModel>>> GetKeepsByIdAsync(int vaultId)
        {
            try
            {
                return Ok(_vks.GetByVaultId(vaultId));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Vault>> Create([FromBody] Vault newVault)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                newVault.CreatorId = userInfo.Id;
                newVault.Creator = userInfo;
                return Ok(_vs.Create(newVault));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<Vault>> Delete(int id)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                return Ok(_vs.Delete(id, userInfo));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }

}