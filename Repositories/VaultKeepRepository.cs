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


        internal void Create(VaultKeep vaultKeep)
        {
            string sql = @"
            INSERT INTO vaultKeeps
            (creatorId, vaultId, keepId)
            VALUES
            (@CreatorId, @VaultId, @KeepId);";
            _db.Execute(sql, vaultKeep);
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

        internal void Delete(int id)
        {
            string sql = "DELETE FROM vaultkeeps WHERE vaultkeeps.id = @id";
            _db.Execute(sql, new { id });
        }
    }
}