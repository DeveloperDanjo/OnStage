using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnStage.Presentation.ApplicationModel.Models
{
    public class CueGroup
    {

        public string Number { get; private set; }

        public IEnumerable<Cue> Cues { get; private set; }

        public CueGroup(string number, IEnumerable<Cue> cues)
        {
            Number = number;
            Cues = cues;
        }

        public CueGroup(OnStage.Entities.CueGroup cg)
        {
            Number = cg.Number;
            var newCues = new List<Cue>();
            foreach (var cue in cg.Cues.OrderBy(c => c.Number))
            {
                newCues.Add(new Cue(cue));
            }
            Cues = newCues;
        }

    }
}
