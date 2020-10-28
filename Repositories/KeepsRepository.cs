using System.Data;
using Keepr.Models;
using Dapper;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Keepr.Repositories
{
    public class KeepsRepository
    {
        private readonly IDbConnection _db;

        public KeepsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal IEnumerable<Keep> GetAll()
        {
            string sql = @"
            SELECT 
            keep.*,
            prof.*
            FROM keeps keep
            JOIN profiles prof ON keep.creatorId = prof.id";
            return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
            {
                keep.Creator = profile;
                return keep;
            }, splitOn: "id");
        }

        internal Keep GetById(int id)
        {
            string sql = @" 
            SELECT 
            keep.*,
            prof.*
            FROM keeps keep
            JOIN profiles prof ON keep.creatorId = prof.id
            WHERE keep.id = @id";
            return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
            {
                keep.Creator = profile;
                return keep;
            }, new { id }, splitOn: "id").FirstOrDefault();
        }

        internal Keep Create(Keep newKeep)
        {
            string sql = @"
            INSERT INTO keeps 
            (creatorId, name, description, img, views, shares, keeps) 
            VALUES 
            (@CreatorId, @Name, @Description, @Img, @Views, @Shares, @Keeps);
            SELECT LAST_INSERT_ID();";
            int id = _db.ExecuteScalar<int>(sql, newKeep);
            newKeep.Id = id;
            return newKeep;
        }


        internal IEnumerable<Keep> GetByProfileId(string id)
        {
            string sql = @"
            SELECT 
            keep.*,
            prof.*
            FROM keeps keep
            JOIN profiles prof ON keep.creatorId = prof.id
            WHERE prof.id = @id";
            return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
            {
                keep.Creator = profile;
                return keep;
            }, new { id }, splitOn: "id");
        }

        internal Keep Edit(Keep updated)
        {
            string sql = @"
            UPDATE keeps
            SET
            name = @Name,
            description = @Description,
            img = @Img,
            views = @views,
            keeps = @keeps,
            shares = @shares
            WHERE id = @Id;";
            _db.Execute(sql, updated);
            return updated;
        }

        internal void Delete(int id)
        {
            string sql = "DELETE FROM keeps WHERE keeps.id = @id";
            _db.Execute(sql, new { id });
        }
    }
}