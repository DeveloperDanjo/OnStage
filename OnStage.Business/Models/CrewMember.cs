using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnStage.Business.Models
{
    public class CrewMember
    {

        public string ConnectionId { get; private set; }

        public string Name { get; private set; }

        public CrewMember(string connectionId, string name)
        {
            ConnectionId = connectionId;
            Name = name;
        }

    }
}
