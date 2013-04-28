define(['jquery', 'underscore', 'shared/hubStateChange', 'shared/cueList', 'shared/scriptViewer', 'shared/cueDetails', 'shared/alertDialog', 'signalr', 'hubs'], function ($, _, hubStateChangeSetup, CueList, ScriptViewer, CueDetails, AlertDialog) {
    'use strict';

    $(function () {
        var showId = parseInt($('#showId').val(), 10);
        var showHub = $.connection.Show;

        $.getJSON('/StageManager/Details/' + showId)
            .done(function (stageManagerCueList) {

                showHub.client.crewJoined = function (id) {
                    $('.connected-crew').append($('<li class="crew-member" data-id="' + id.ConnectionId + '">' + id.Name + '</li>'));
                };

                showHub.client.crewDisconnected = function (id) {
                    $('.connected-crew').find('[data-id="' + id.ConnectionId + '"]').remove();
                };

                $.connection.hub.start()
                    .done(function () {
                        showHub.server.stageManagerJoin(showId)
                            .fail(function () {
                                AlertDialog({ title: 'Error', message: 'Could not join the crew for this show.' });
                            });

                        hubStateChangeSetup();
                    })
                    .fail(function () {
                        AlertDialog({ title: 'Error', message: 'Could not join the crew for this show.' });
                    });

                var currentGo = { number: '', id: -1 };

                function triggerCue(number, id, state) {
                    if (id === -1) return;

                    if (state === 'go') {
                        triggerCue(currentGo.number, currentGo.id, 'finished');
                        currentGo.number = number;
                        currentGo.id = id;
                    }

                    var cue = stageManagerCueList.Cues[id];

                    showHub.server.runCueGroup({ cueId: cue.Id, status: state })
                        .done(function () {
                            cueList.setState(number, state);
                            scriptViewer.setState(number, state);
                        })
                        .fail(function () {
                            AlertDialog({ title: 'Error', message: 'Cue signal could not be sent to the crew.' });
                        });
                }

                var cueList = new CueList({
                    cues: stageManagerCueList.Cues,
                    stageManager: true,
                    selectCue: function (number, id) {
                        cueDetails.setCue(stageManagerCueList.Cues[id]);
                    },
                    mouseoverCue: function (number, id) {
                        scriptViewer.highlightCue(number);
                    },
                    mouseoutCue: function (number, id) {
                        scriptViewer.unhighlightCue(number);
                    },
                    triggerCue: triggerCue
                });

                cueList.getElement().appendTo($('#cueList'));

                var scriptViewer = new ScriptViewer({
                    showId: showId,
                    cues: stageManagerCueList.Cues
                });

                scriptViewer.getElement().appendTo($('#scriptViewer'));

                var cueDetails = new CueDetails();

                cueDetails.setCue(stageManagerCueList.Cues[0]);
                cueDetails.getElement().appendTo($('#cueDetails'));
            })
            .fail(function () {
                AlertDialog({ title: 'Fatal Error', message: 'Could not load the cues for the specified show.' });
            });
    });
});