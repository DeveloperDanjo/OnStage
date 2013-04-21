using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNet.SignalR;

namespace OnStage.Business.Hubs
{
    class GroupManager
    {

        public IDictionary<string, IList<string>> GroupMembers { get; set; }

        private IGroupManager Groups { get; set; }

        public GroupManager(IHub hub)
        {
            Groups = hub.Groups;
            GroupMembers = new Dictionary<string, IList<string>>();
        }

        public Task Add(string connectionId, string groupName)
        {
            if (!GroupMembers.ContainsKey(groupName))
            {
                GroupMembers.Add(groupName, new List<string>());
            }
            GroupMembers[groupName].Add(connectionId);
            return Groups.Add(connectionId, groupName);
        }

        public Task Remove(string connectionId, string groupName)
        {
            if (GroupMembers.ContainsKey(groupName))
            {
                GroupMembers[groupName].Remove(connectionId);
            }
            return Groups.Remove(connectionId, groupName);
        }

    }
}
