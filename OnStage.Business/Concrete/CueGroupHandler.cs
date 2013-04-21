using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnStage.Business.Abstract;
using OnStage.Entities;
using OnStage.Persistence.Abstract;

namespace OnStage.Business.Concrete
{
    public class CueGroupHandler : ICueGroupHandler
    {

        public ICueGroupRepository repository;

        public CueGroupHandler(ICueGroupRepository repository)
        {
            this.repository = repository;
        }

        public CueGroup GetCueGroup(int id)
        {
            return repository.GetCueGroup(id);
        }

        public IEnumerable<CueGroup> GetAllCueGroups()
        {
            return repository.GetAllCueGroups();
        }

        public void AddCueGroup(CueGroup CueGroup)
        {
            repository.AddCueGroup(CueGroup);
        }

        public void DeleteCueGroup(int id)
        {
            repository.DeleteCueGroup(id);
        }

        public void SaveCueGroup(CueGroup CueGroup)
        {
            repository.SaveCueGroup(CueGroup);
        }

    }
}
