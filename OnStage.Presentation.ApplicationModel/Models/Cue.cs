using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnStage.Presentation.ApplicationModel.Models
{
    public class Cue
    {

        public string Name { get; private set; }

        public string Number { get; private set; }

        public long ScriptPosition { get; private set; }

        public string Description { get; private set; }

        public Cue(string name, string number, long scriptPosition, string description)
        {
            Name = name;
            Number = number;
            ScriptPosition = scriptPosition;
            Description = description;
        }

        public Cue(OnStage.Entities.Cue cue)
        {
            Name = cue.Name;
            Number = cue.Number;
            ScriptPosition = cue.ScriptPosition;
            Description = cue.Description;
        }

    }
}
