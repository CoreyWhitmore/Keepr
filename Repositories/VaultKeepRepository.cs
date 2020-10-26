using System.Data;
using Keepr.Models;
using Dapper;
using System.Linq;
using System;

namespace Keepr.Repositories
{
    public class VaultKeepRepository
    {
        private readonly IDbConnection _db;

        public VaultKeepRepository(IDbConnection db)
        {
            _db = db;
        }

        internal object Create(Vault newVaultKeep)
        {
            string sql = @"
            INSERT INTO vaultkeeps 
            (creatorId, vaultId, keepId) 
            VALUES 
            (@CreatorId, @vaultId, @keepId);
            SELECT LAST_INSERT_ID();";
            int id = _db.ExecuteScalar<int>(sql, newVaultKeep);
            newVaultKeep.Id = id;
            return newVaultKeep;
        }
    }
}