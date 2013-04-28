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
        private static IDictionary<int, IList<CueState>> cueDictionary = new Dictionary<int, IList<CueState>>();

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


        private static void UpdateCueGroupState(CueState state, ShowHub hub)
        {
            var show = hub.cuegroupHandler.GetCueGroup(state.CueId).StageManagerCueBook.Show;
            if (!cueDictionary.ContainsKey(show.Id))
            {
                cueDictionary.Add(show.Id, new List<CueState>());
            }
            var internalCueState = cueDictionary[show.Id].FirstOrDefault(cs => cs.CueId == state.CueId);
            if (internalCueState == null)
            {
                cueDictionary[show.Id].Add(state);
            }
            else
            {
                internalCueState.Status = state.Status;
            }
        }

        private static void EmptyCueGroupState(int showId)
        {
            cueDictionary.Remove(showId);
        }

        private static IEnumerable<CueState> GetPreviousCues(int showId)
        {
            if (cueDictionary.ContainsKey(showId))
            {
                return cueDictionary[showId];
            }
            return Enumerable.Empty<CueState>();
        }

        private static IEnumerable<CueState> GetPreviousCues(int showId, int cuebookId, ShowHub hub)
        {
            foreach (var cs in GetPreviousCues(showId))
            {
                var realCues = hub.cuegroupHandler.GetCueGroup(cs.CueId).Cues.Where(c => c.CueBook.Id == cuebookId);
                foreach (var c in realCues)
                {
                    yield return new CueState(c.Id, c.Number, cs.Status);
                }
            }
        }


        public void StageManagerJoin(int showId)
        {
            ShowHub.EmptyCueGroupState(showId);

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

            foreach (var cs in ShowHub.GetPreviousCues(showId, cuebookId, this))
            {
                Clients.Caller.runCue(cs);
            }
        }

        public void RunCueGroup(CueState cueState)
        {
            var cueGroup = cuegroupHandler.GetCueGroup(cueState.CueId);
            foreach (var cue in cueGroup.Cues)
            {
                var owningCueBook = cue.CueBook;
                Clients.Group(GetCrewGroupName(owningCueBook.Show.Id, owningCueBook.Id)).runCue(new CueState(cue.Id, cue.Number, cueState.Status));
            }

            ShowHub.UpdateCueGroupState(cueState, this);
        }

        public override Task OnDisconnected()
        {
            CrewState crewState;
            if (ShowHub.showDictionary.TryGetValue(Context.ConnectionId, out crewState))
            {
                Clients.Group(GetStageManagerGroupName(crewState.ShowId)).crewDisconnected(new CrewMember(Context.ConnectionId, null));
                ShowHub.showDictionary.Remove(Context.ConnectionId);
            }
            return base.OnDisconnected();
        }

    }
}
