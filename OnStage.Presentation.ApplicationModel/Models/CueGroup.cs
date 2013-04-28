using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnStage.Presentation.ApplicationModel.Models
{
    public class CueGroup
    {
        public int Id { get; private set; }

        public string Number { get; private set; }

        public IEnumerable<Cue> Cues { get; private set; }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public long ScriptPosition { get; private set; }

        public CueGroup(int id, string number, string name, string description, long scriptPosition, IEnumerable<Cue> cues)
        {
            Id = id;
            Number = number;
            Name = name;
            Description = description;
            ScriptPosition = scriptPosition;
            Cues = cues;
        }

        public CueGroup(OnStage.Entities.CueGroup cg)
        {
            Id = cg.Id;
            Number = cg.Number;
            Name = cg.Name;
            Description = cg.Description;
            ScriptPosition = cg.ScriptPosition;
            var newCues = new List<Cue>();
            foreach (var cue in cg.Cues.OrderBy(c => c.Number))
            {
                newCues.Add(new Cue(cue));
            }
            Cues = newCues;
        }

    }
}
