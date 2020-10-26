using System;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
    public class VaultKeepService
    {
        private readonly VaultKeepRepository _repo;

        public VaultKeepService(VaultKeepRepository repo)
        {
            _repo = repo;
        }

        internal VaultKeep GetById(int id, Profile userInfo)
        {
            VaultKeep data = _repo.GetById(id);
            if (data == null)
            {
                throw new Exception("Invalid Id");
            }
            return data;
        }


        internal void Create(VaultKeep newVaultKeep)
        {
            _repo.Create(newVaultKeep);
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
    }
}