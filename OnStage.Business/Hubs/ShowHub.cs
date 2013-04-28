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
    [HubName("Show")]
    public class ShowHub : Hub
    {

        private ICueGroupHandler cuegroupHandler;
        private ICueBookHandler cuebookHandler;
        private static IDictionary<string, CrewState> showDictionary = new Dictionary<string, CrewState>();

        public ShowHub(ICueGroupHandler cuegroupHandler, ICueBookHandler cuebookHandler)
        {
            this.cuegroupHandler = cuegroupHandler;
            this.cuebookHandler = cuebookHandler;
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
            ShowHub.showDictionary.Add(Context.ConnectionId, new CrewState() { ShowId = showId, CueBookName = "Stage Manager", IsStageManager = true });

            foreach (var kvp in ShowHub.showDictionary.Where(kvp => !kvp.Value.IsStageManager))
            {
                Clients.Caller.crewJoined(new CrewMember(kvp.Key, kvp.Value.CueBookName));
            }
        }

        public void Join(int showId, int cuebookId)
        {
            var cueBook = cuebookHandler.GetCueBook(cuebookId);

            Groups.Add(Context.ConnectionId, GetShowGroupName(showId));
            Groups.Add(Context.ConnectionId, GetCrewGroupName(showId, cuebookId));
            ShowHub.showDictionary.Add(Context.ConnectionId, new CrewState() { ShowId = showId, CueBookName = cueBook.Name, IsStageManager = false });

            Clients.Group(GetStageManagerGroupName(showId)).crewJoined(new CrewMember(Context.ConnectionId, cueBook.Name));
        }

        public void RunCueGroup(CueState cueState)
        {
            var cueGroup = cuegroupHandler.GetCueGroup(cueState.CueId);
            foreach (var cue in cueGroup.Cues)
            {
                var owningCueBook = cue.CueBook;
                Clients.Group(GetCrewGroupName(owningCueBook.Show.Id, owningCueBook.Id)).runCue(new CueState(cue.Id, cue.Number, cueState.Status));
            }
        }

        public override Task OnDisconnected()
        {
            CrewState crewState;
            if (ShowHub.showDictionary.TryGetValue(Context.ConnectionId, out crewState))
            {
                Clients.Group(GetStageManagerGroupName(crewState.ShowId)).crewDisconnected(new CrewMember(Context.ConnectionId, null));
                ShowHub.showDictionary.Remove(Context.ConnectionId);
            }
            else
            {
                //
            }
            return base.OnDisconnected();
        }

    }
}
