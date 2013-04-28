define(['jquery', 'underscore', 'shared/hubStateChange', 'shared/cueList', 'shared/scriptViewer', 'shared/cueDetails', 'shared/alertDialog', 'signalr', 'hubs'], function ($, _, hubStateChangeSetup, CueList, ScriptViewer, CueDetails, AlertDialog) {
    'use strict';

    $(function () {
        var showId = parseInt($('#showId').val(), 10);
        var cueBookId = parseInt($('#cuebookId').val(), 10);
        var showHub = $.connection.Show;

        $.getJSON('/CueBook/Details/' + showId)
            .done(function (crewCueList) {

                showHub.client.runCue = function (cueState) {
                    cueList.setState(cueState.Number, cueState.Status);
                    scriptViewer.setState(cueState.Number, cueState.Status);
                };

                $.connection.hub.start()
                    .done(function () {
                        showHub.server.join(showId, cueBookId)
                            .fail(function () {
                                AlertDialog({ title: 'Error', message: 'Could not join the crew for this show.' });
                            });

                        hubStateChangeSetup();
                    })
                    .fail(function () {
                        AlertDialog({ title: 'Error', message: 'Could not join the crew for this show.' });
                    });

                var cueList = new CueList({
                    cues: crewCueList.Cues,
                    selectCue: function (number, id) {
                        cueDetails.setCue(crewCueList.Cues[id]);
                    },
                    mouseoverCue: function (number, id) {
                        scriptViewer.highlightCue(number);
                    },
                    mouseoutCue: function (number, id) {
                        scriptViewer.unhighlightCue(number);
                    }
                });

                cueList.getElement().appendTo($('#cueList'));

                var scriptViewer = new ScriptViewer({
                    showId: showId,
                    cues: crewCueList.Cues
                });

                scriptViewer.getElement().appendTo($('#scriptViewer'));

                var cueDetails = new CueDetails();

                cueDetails.setCue(crewCueList.Cues[0]);
                cueDetails.getElement().appendTo($('#cueDetails'));
            })
            .fail(function () {
                AlertDialog({ title: 'Fatal Error', message: 'Could not load the cues for the specified show.' });
            });
    });
});