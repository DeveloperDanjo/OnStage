define(['jquery', 'underscore', 'text!../../Content/Script.css'], function ($, _, cuePostCss) {
    'use strict';

    var cuePostTemplate = _.template('<div data-number="<%= number %>" class="cue-post"><%= number %>: <%= name %></div>');

    function ScriptViewer(options) {
        var that = this;

        this.options = _.defaults({}, options, {
            //showId: required
            cues: [],
            keys: {
                number: 'Number',
                name: 'Name',
                scriptPosition: 'ScriptPosition'
            }
        });
        this.$container = $('<div class="script-viewer-container" />');
        this.$iframe = $('<iframe class="script-viewer-iframe" />').appendTo(this.$container);

        this.setScript(this.options.showId);

        this.$iframe.on('load', function () {
            that._document = that.$iframe.get(0).contentWindow.document;
            var $body = that.$body = $(that._document.body);
            var $pre = $('pre', that._document);

            // Can't create cue markers if we aren't in a plain text situation or a browser that supports document.createRange
            if (!$pre.length || !document.createRange) return;

            $('<style type="text/css" />', that._document).html(cuePostCss).appendTo(that._document.head);

            var keys = that.options.keys;

            _.each(that.options.cues, function (cue) {
                var scriptPosition = cue[keys.scriptPosition];
                var range = that._document.createRange();
                range.setStart($pre.get(0).firstChild, scriptPosition); range.setEnd($pre.get(0).firstChild, scriptPosition);
                var rect = range.getClientRects().item(0);
                var $post = $(cuePostTemplate({
                    number: cue[keys.number],
                    name: cue[keys.name]
                }), that._document);
                $post.css('left', rect.left - 5); $post.css('top', rect.bottom);
                $post.appendTo($body);
            });
        });
    }

    function findCue($cuePosts, number) {
        return $cuePosts.filter('[data-number="' + number + '"]');
    }

    ScriptViewer.prototype = {
        setState: function (number, state) {
            var $cue = findCue($('.cue-post', this._document), number).attr('class', 'cue-post').addClass(state);

            if (state === 'go') {
                this.$body.scrollTop($cue.offset().top - (this.$iframe.height() / 4));
            }
        },

        setScript: function (showId) {
            this.$iframe.attr('src', '/Show/Script/' + showId);
        },

        highlightCue: function (number) {
            findCue($('.cue-post', this._document), number).addClass('visible');
        },

        unhighlightCue: function (number) {
            findCue($('.cue-post', this._document), number).removeClass('visible');
        },

        getElement: function () {
            return this.$container;
        }
    };

    return ScriptViewer;
});