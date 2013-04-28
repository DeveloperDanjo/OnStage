define(['jquery', 'underscore', 'bootstrap'], function ($, _) {
    'use strict';

    var alertTemplate = _.template('<div class="alert-modal modal hide fade" tabindex="-1" role="dialog" aria-hidden="true" data-backdrop="static">' +
                                   '<div class="modal-header">' +
                                       '<h3><%= title %></h3>' +
                                   '</div>' +
                                   '<div class="modal-body">' +
                                       '<p><%= message %></p>' +
                                   '</div>' +
                                   '<div class="modal-footer">' +
                                       '<button data-dismiss="modal" class="btn btn-primary">OK</button>' +
                                   '</div></div>');

    function AlertDialog(options) {
        var options = _.defaults({}, options, {
            title: 'Alert',
            message: 'Something went wrong',
            showOk: true
        });

        var $alert = $(alertTemplate(options)).modal().on('hidden', function () {
            $(this).remove();
            if (options.hidden) { options.hidden(); }
        });

        if (!options.showOk) { $alert.find('.modal-footer button').hide(); }
    }

    return AlertDialog;
});