using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnStage.Presentation.ApplicationModel.Models
{
    public class CueBook
    {

        public IEnumerable<Cue> Cues { get; private set; }

        public string Name { get; private set; }

        public string ShortName { get; private set; }

        public CueBook(string name, string shortName, IEnumerable<Cue> cues)
        {
            Name = name;
            ShortName = shortName;
            Cues = cues;
        }

        public CueBook(OnStage.Entities.CueBook cueBook)
        {
            Name = cueBook.Name;
            ShortName = cueBook.ShortName;
            var newCues = new List<Cue>();
            foreach (var cue in cueBook.Cues.OrderBy(c => c.Number))
            {
                newCues.Add(new Cue(cue));
            }
            Cues = newCues;
        }

    }
}
