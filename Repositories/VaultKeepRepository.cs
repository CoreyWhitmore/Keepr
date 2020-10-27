using System.Data;
using Keepr.Models;
using Dapper;
using System.Linq;
using System;
using System.Collections.Generic;

namespace Keepr.Repositories
{
    public class VaultKeepRepository
    {
        private readonly IDbConnection _db;

        public VaultKeepRepository(IDbConnection db)
        {
            _db = db;
        }


        internal VaultKeep Create(VaultKeep newVaultKeep)
        {
            string sql = @"
            INSERT INTO vaultKeeps
            (creatorId, vaultId, keepId)
            VALUES
            (@CreatorId, @VaultId, @KeepId);
            SELECT LAST_INSERT_ID();";
            int id = _db.ExecuteScalar<int>(sql, newVaultKeep);
            newVaultKeep.Id = id;
            return newVaultKeep;
        }


        internal VaultKeep GetById(int id)
        {
            string sql = @" 
            SELECT 
            vaultkeep.*,
            prof.*
            FROM vaultkeeps vaultkeep
            JOIN profiles prof ON vaultkeep.creatorId = prof.id
            WHERE vaultkeep.id = @id";
            VaultKeep result = _db.Query<VaultKeep, Profile, VaultKeep>(sql, (vaultkeep, profile) =>
            {
                return vaultkeep;
            }, new { id }, splitOn: "id").FirstOrDefault();
            return result;
        }

        internal IEnumerable<VaultKeep> GetByVaultId(int vaultId, Profile userInfo)
        {
            var userId = userInfo.Id;
            string sql = @"
            SELECT 
            vaultkeep.*,
            prof.*
            FROM vaultkeeps vaultkeep
            JOIN profiles prof ON vaultkeep.creatorId = prof.id
            WHERE vaultId = @vaultId";
            return _db.Query<VaultKeep, Profile, VaultKeep>(sql, (vaultKeep, profile) =>
            {
                return vaultKeep;
            }, new { vaultId, userId }, splitOn: "id");
        }

        internal void Delete(int id)
        {
            string sql = "DELETE FROM vaultkeeps WHERE vaultkeeps.id = @id";
            _db.Execute(sql, new { id });
        }
    }
}