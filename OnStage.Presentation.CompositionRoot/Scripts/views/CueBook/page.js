require(['jquery', 'signalr', 'hubs'], function ($) {
    'use strict';

    $(function () {
        var showHub = $.connection.Show;

        $.connection.hub.error(function () {
            console.error('an error occurred');
        });

        showHub.client.runCue = function (cueState) {
            console.log(cueState);
        };

        $.connection.hub.start()
            .done(function () {
                showHub.server.join(parseInt($('#showId').val(), 10), parseInt($('#cuebookId').val(), 10))
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
    });
});