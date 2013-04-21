using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using OnStage.Business.Abstract;
using OnStage.Business.Models;

namespace OnStage.Business.Hubs
{
    public class ShowHub : Hub
    {

        private ICueGroupHandler cuegroupHandler;

        public ShowHub(ICueGroupHandler cuegroupHandler)
        {
            this.cuegroupHandler = cuegroupHandler;
        }

        private string GetStageManagerGroupName(int showId)
        {
            return GetShowGroupName(showId) + "SM";
        }

        private string GetShowGroupName(int showId)
        {
            return showId.ToString();
        }

        private string GetCrewGroupName(int showId, int cuebookId)
        {
            return GetShowGroupName(showId) + "-" + cuebookId.ToString();
        }

        public void StageManagerJoin(int showId)
        {
            Groups.Add(Context.ConnectionId, GetStageManagerGroupName(showId));
            Groups.Add(Context.ConnectionId, GetShowGroupName(showId));
        }

        public void Join(int showId, int cuebookId)
        {
            Groups.Add(Context.ConnectionId, GetShowGroupName(showId));
            Groups.Add(Context.ConnectionId, GetCrewGroupName(showId, cuebookId));
        }

        public void RunCueGroup(CueState cueState)
        {
            var cueGroup = cuegroupHandler.GetCueGroup(cueState.CueId);
            foreach (var cue in cueGroup.Cues)
            {
                var owningCueBook = cue.CueBook;
                Clients.Group(GetCrewGroupName(owningCueBook.Show.Id, owningCueBook.Id)).runCue(new CueState(cue.Id, cueState.Status));
            }
        }

    }
}
