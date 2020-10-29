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
    public class ProfilesController : ControllerBase
    {
        private readonly ProfilesService _ps;
        private readonly KeepsService _ks;
        private readonly VaultsService _vs;
        public ProfilesController(ProfilesService ps, KeepsService ks, VaultsService vs)
        {
            _ps = ps;
            _ks = ks;
            _vs = vs;
        }


        [HttpGet]
        [Authorize]
        public async Task<ActionResult<Profile>> Get()
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                return Ok(_ps.GetOrCreateProfile(userInfo));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Profile> GetById(string id)
        {
            try
            {
                return Ok(_ps.GetProfileById(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}/keeps")]
        public async Task<ActionResult<IEnumerable<Keep>>> GetKeepsById(string id)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                return Ok(_ks.GetByProfileId(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}/vaults")]
        public async Task<ActionResult<IEnumerable<Vault>>> GetVaultsById(string id)
        {
            try
            {
                Profile userInfo;
                try
                {
                    userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                }
                catch (System.Exception)
                {
                    userInfo = null;
                }
                return Ok(_vs.GetByProfileId(id, userInfo));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}