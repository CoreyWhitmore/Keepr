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



        internal object Create(Vault newVaultKeep)
        {
            return _repo.Create(newVaultKeep);
        }

        internal object Delete(int id, Profile userInfo)
        {

        }
    }
}