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

            int vaultId = newVaultKeep.VaultId;
            string sql2 = @" 
            SELECT 
            vault.*,
            prof.*
            FROM vaults vault
            JOIN profiles prof ON vault.creatorId = prof.id
            WHERE vault.id = @vaultId";
            Vault resVault = _db.Query<Vault, Profile, Vault>(sql2, (vault, profile) =>
             {
                 vault.Creator = profile;
                 return vault;
             }, new { vaultId }, splitOn: "id").FirstOrDefault();

            if (resVault.CreatorId == newVaultKeep.CreatorId)
            {
                return newVaultKeep;
            }
            return null;
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

        internal IEnumerable<VaultKeepViewModel> GetByVaultId(int vaultId)
        {
            string sql = @"
            SELECT k.*, vaultKeeps.id AS VaultKeepId, prof.*
            FROM vaultKeeps
            JOIN keeps k ON k.id = vaultKeeps.keepId
            WHERE vaultId = @vaultId
            ";
            // return _db.Query<VaultKeepViewModel>(sql, new { vaultId });
            return _db.Query<VaultKeepViewModel, Profile, VaultKeepViewModel>(sql, (keep, profile) =>
                        {
                            keep.Creator = profile;
                            return keep;
                        }, new { vaultId }, splitOn: "id");

        }

        internal void Delete(int id)
        {
            string sql = "DELETE FROM vaultkeeps WHERE vaultkeeps.id = @id";
            _db.Execute(sql, new { id });
        }
    }
}