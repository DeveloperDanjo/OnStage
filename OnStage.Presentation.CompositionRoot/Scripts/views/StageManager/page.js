define(['jquery', 'underscore', 'shared/cueList', 'shared/scriptViewer', 'shared/cueDetails', 'shared/alertDialog', 'signalr', 'hubs'], function ($, _, CueList, ScriptViewer, CueDetails, AlertDialog) {
    'use strict';

    $(function () {
        var showId = parseInt($('#showId').val(), 10);
        var showHub = $.connection.Show;

        $.getJSON('/StageManager/Details/' + showId)
            .done(function (stageManagerCueList) {
                $.connection.hub.start()
                    .done(function () {
                        showHub.server.stageManagerJoin(showId)
                            .fail(function () {
                                AlertDialog({ title: 'Error', message: 'Could not join the crew for this show.' });
                            });

                        $.connection.hub.stateChanged(function (change) {
                            if (change.newState === $.signalR.connectionState.reconnecting) {
                                AlertDialog({ title: 'Error', message: 'You were disconnected from the server. Attempting to reconnect.', showOk: false });
                            } else if (change.newState === $.signalR.connectionState.connected) {
                                $('.alert-modal').modal('hide').on('hidden', function () { $(this).remove(); });
                            } else if (change.newState === $.signalR.connectionState.disconnected) {
                                var $modal = $('.alert-modal');
                                if ($modal.length) {
                                    $modal.modal('hide').on('hidden', function () {
                                        $(this).remove();
                                        AlertDialog({ title: 'Error',
                                            message: 'You were disconnected from the server. Press "OK" to reload, or close this page.',
                                            hidden: function () {
                                                location.reload();
                                            }
                                        });
                                    });
                                } else {
                                    AlertDialog({ title: 'Error',
                                        message: 'You were disconnected from the server. Press "OK" to reload, or close this page.',
                                        hidden: function () {
                                            location.reload();
                                        }
                                    });
                                }
                            }
                        });
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
                        scriptViewer.highlightCue(number);
                        cueDetails.setCue(stageManagerCueList.Cues[id]);
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