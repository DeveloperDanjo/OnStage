define(['jquery', 'shared/alertDialog', 'signalr', 'hubs'], function ($, AlertDialog) {
    'use strict';

    return function hubStateChangeSetup() {
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
    }
});