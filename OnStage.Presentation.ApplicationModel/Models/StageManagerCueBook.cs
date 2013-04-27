using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnStage.Presentation.ApplicationModel.Models
{
    public class StageManagerCueBook
    {

        public IEnumerable<CueGroup> Cues { get; private set; }

        public StageManagerCueBook(IEnumerable<CueGroup> cues)
        {
            Cues = cues;
        }

        public StageManagerCueBook(OnStage.Entities.StageManagerCueBook smcb)
        {
            var newCues = new List<CueGroup>();
            foreach (var cue in smcb.Cues.OrderBy(cg => cg.Number))
            {
                newCues.Add(new CueGroup(cue));
            }
            Cues = newCues;
        }

    }
}
