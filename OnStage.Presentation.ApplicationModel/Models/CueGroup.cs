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

        public string Name { get; private set; }

        public string Description { get; private set; }

        public CueGroup(string number, string name, string description, IEnumerable<Cue> cues)
        {
            Number = number;
            Name = name;
            Description = description;
            Cues = cues;
        }

        public CueGroup(OnStage.Entities.CueGroup cg)
        {
            Number = cg.Number;
            Name = cg.Name;
            Description = cg.Description;
            var newCues = new List<Cue>();
            foreach (var cue in cg.Cues.OrderBy(c => c.Number))
            {
                newCues.Add(new Cue(cue));
            }
            Cues = newCues;
        }

    }
}
