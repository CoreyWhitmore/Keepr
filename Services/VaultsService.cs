using System;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
    public class VaultsService
    {
        private readonly VaultsRepository _repo;

        public VaultsService(VaultsRepository repo)
        {
            _repo = repo;
        }

        internal Vault GetById(int id, Profile userInfo)
        {
            Vault data = _repo.GetById(id);
            if (data == null)
            {
                throw new Exception("Invalid Id");
            }
            if (data.IsPrivate == true && data.CreatorId != userInfo.Id)
            {
                throw new Exception("This Vault is Private");
            }
            return data;
        }

        internal Vault Create(Vault newVault)
        {
            return _repo.Create(newVault);
        }

        internal string Delete(int id, Profile userInfo)
        {
            var data = GetById(id, userInfo);
            if (data.CreatorId != userInfo.Id)
            {
                throw new Exception("You must own this Vault to delete it.");
            }
            _repo.Delete(id);
            return "Successfully Deleted";
        }

        internal object GetByProfileId(string profileId, Profile userInfo)
        {
            return _repo.GetByProfileId(profileId, userInfo);
        }
    }
}