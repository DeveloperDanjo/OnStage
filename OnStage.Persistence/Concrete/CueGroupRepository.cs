using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnStage.Entities;
using OnStage.Persistence.Abstract;

namespace OnStage.Persistence.Concrete
{
    public class CueGroupRepository : ICueGroupRepository
    {

        private OnStageEntitiesContainer context;

        public CueGroupRepository(OnStageEntitiesContainer context)
        {
            this.context = context;
        }

        public CueGroup GetCueGroup(string title)
        {
            throw new NotImplementedException();
        }

        public CueGroup GetCueGroup(int id)
        {
            return context.CueGroups.Where(s => s.Id == id).First();
        }

        public IEnumerable<CueGroup> GetAllCueGroups()
        {
            return context.CueGroups;
        }

        public void AddCueGroup(CueGroup CueGroup)
        {
            context.CueGroups.AddObject(CueGroup);
            context.SaveChanges();
        }

        public void DeleteCueGroup(int id)
        {
            context.CueGroups.DeleteObject(GetCueGroup(id));
            context.SaveChanges();
        }

        public void SaveCueGroup(CueGroup CueGroup)
        {
            if (CueGroup.EntityState == System.Data.EntityState.Detached)
            {
                context.CueGroups.AddObject(CueGroup);
            }
            context.ObjectStateManager.ChangeObjectState(CueGroup, System.Data.EntityState.Modified);
            context.SaveChanges();
        }

    }
}
