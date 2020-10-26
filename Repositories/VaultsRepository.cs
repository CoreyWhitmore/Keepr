using System.Data;
using Keepr.Models;
using Dapper;
using System.Linq;

namespace Keepr.Repositories
{
    public class VaultsRepository
    {
        private readonly IDbConnection _db;

        public VaultsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal Vault GetById(int id)
        {
            string sql = @" 
            SELECT 
            vault.*,
            prof.*
            FROM vaults vault
            JOIN profiles prof ON vault.creatorId = prof.id
            WHERE vault.id = @id";
            Vault result = _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
            {
                vault.Creator = profile;
                return vault;
            }, new { id }, splitOn: "id").FirstOrDefault();
            return result;

        }

        internal Vault Create(Vault newVault)
        {
            string sql = @"
            INSERT INTO vaults 
            (creatorId, name, isPrivate) 
            VALUES 
            (@CreatorId, @Name, @IsPrivate);
            SELECT LAST_INSERT_ID();";
            int id = _db.ExecuteScalar<int>(sql, newVault);
            newVault.Id = id;
            return newVault;
        }

        internal object GetByProfileId(string profileId, Profile userInfo)
        {
            string sql = @"
            SELECT 
            vault.*,
            prof.*
            FROM vaults vault
            JOIN profiles prof ON vault.creatorId = prof.id
            WHERE prof.id = @profileId OR isPrivate = false";
            return _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
            {
                vault.Creator = profile;
                return vault;
            }, new { profileId }, splitOn: "id");
        }

        internal void Delete(int id)
        {
            string sql = "DELETE FROM vaults WHERE vaults.id = @id";
            _db.Execute(sql, new { id });
        }
    }
}