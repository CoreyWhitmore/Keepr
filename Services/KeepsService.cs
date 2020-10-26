using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
    public class KeepsService
    {
        private readonly KeepsRepository _repo;

        public KeepsService(KeepsRepository repo)
        {
            _repo = repo;
        }
        internal IEnumerable<Keep> GetAll()
        {
            return _repo.GetAll();
        }

        internal Keep GetById(int id)
        {
            Keep data = _repo.GetById(id);
            if (data == null)
            {
                throw new Exception("Invalid Id");
            }
            return data;
        }

        internal Keep Create(Keep newKeep)
        {
            return _repo.Create(newKeep);
        }

        internal object Edit(Keep updated)
        {
            var data = GetById(updated.Id);
            if (data.CreatorId != updated.CreatorId)
            {
                throw new Exception("You must own this Keep to edit it.");
            }
            return _repo.Edit(updated);
        }

        internal string Delete(int id, string userId)
        {
            var data = GetById(id);
            if (data.CreatorId != userId)
            {
                throw new Exception("You must own this Keep to delete it.");
            }
            _repo.Delete(id);
            return "Successfully Deleted";
        }

        internal IEnumerable<Keep> GetByProfileId(string id)
        {
            return _repo.GetByProfileId(id);
        }
    }
}