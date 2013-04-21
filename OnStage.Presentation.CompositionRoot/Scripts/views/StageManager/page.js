require(['jquery', 'signalr', 'hubs'], function ($) {
    'use strict';

    $(function () {
        var showHub = $.connection.Show;

        $.connection.hub.start()
            .done(function () {
                showHub.server.stageManagerJoin(parseInt($('#showId').val(), 10))
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

        $('.cue-list > li').on('click', function () {
            showHub.server.runCueGroup({
                CueId: parseInt($(this).attr('id'), 10),
                Status: 'standby'
            })
                .done(function () {
                    console.log('cued');
                })
                .fail(function () {
                    console.log('could not cue');
                });
        });
    });
});