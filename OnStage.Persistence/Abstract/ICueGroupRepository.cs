using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnStage.Entities;

namespace OnStage.Persistence.Abstract
{
    public interface ICueGroupRepository
    {

        CueGroup GetCueGroup(int id);

        IEnumerable<CueGroup> GetAllCueGroups();

        void AddCueGroup(CueGroup CueGroup);

        void DeleteCueGroup(int id);

        void SaveCueGroup(CueGroup CueGroup);

    }
}
