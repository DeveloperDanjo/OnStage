define(['jquery', 'underscore'], function ($, _) {
    'use strict';

    function ScriptViewer(options) {
        this.options = _.defaults({}, options, {
            //showId: required
        });
        this.$container = $('<div class="script-viewer-container" />');
        this.$iframe = $('<iframe class="script-viewer-iframe" />').appendTo(this.$container);

        this.setScript(this.options.showId);
    }

    ScriptViewer.prototype = {
        setScript: function (showId) {
            this.$iframe.attr('src', '/Show/Script/' + showId);
        },

        getElement: function () {
            return this.$container;
        }
    };

    return ScriptViewer;
});