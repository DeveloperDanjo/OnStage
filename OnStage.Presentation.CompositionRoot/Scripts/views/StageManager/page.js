define(['jquery', 'underscore', 'shared/cueList', 'shared/scriptViewer', 'signalr', 'hubs'], function ($, _, CueList, ScriptViewer) {
    'use strict';

    $(function () {
        var showId = parseInt($('#showId').val(), 10);
        var showHub = $.connection.Show;

        $.connection.hub.start()
            .done(function () {
                showHub.server.stageManagerJoin(showId)
                    .done(function () {
                        console.log('joined');
                    })
                    .fail(function () {
                        console.log('could not join');
                    });
            })
            .fail(function () {
                console.log('could not start');
            });

        $.getJSON('/StageManager/Details/' + showId)
            .done(function (stageManagerCueList) {
                var cueList = new CueList({
                    cues: stageManagerCueList.Cues
                });

                cueList.getElement().appendTo($('#cueList'));

                window.cueList = cueList;
            })
            .fail(function () {
                console.log('could not get cues');
            });

        var scriptViewer = new ScriptViewer({
            showId: showId
        });

        scriptViewer.getElement().appendTo($('#scriptViewer'));

    });
});