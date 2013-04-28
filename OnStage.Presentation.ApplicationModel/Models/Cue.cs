using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnStage.Presentation.ApplicationModel.Models
{
    public class Cue
    {
        public string ShortBookName { get; private set; }

        public string Name { get; private set; }

        public string Number { get; private set; }

        public int Id { get; private set; }

        public long ScriptPosition { get; private set; }

        public string Description { get; private set; }

        public Cue(int id, string name, string number, long scriptPosition, string description, string shortBookName)
        {
            Id = id;
            Name = name;
            Number = number;
            ScriptPosition = scriptPosition;
            Description = description;
            ShortBookName = shortBookName;
        }

        public Cue(OnStage.Entities.Cue cue)
        {
            Id = cue.Id;
            Name = cue.Name;
            Number = cue.Number;
            ScriptPosition = cue.ScriptPosition;
            Description = cue.Description;
            ShortBookName = cue.CueBook.ShortName;
        }

    }
}
