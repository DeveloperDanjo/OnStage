define(['jquery', 'underscore'], function ($, _) {
    'use strict';

    var cueListTemplate = _.template('<ol class="cue-list" />');
    var cueTemplate = _.template('<li data-number="<%= number %>" data-id="<%= id %>"><span class="cue-name"><%= number %> - <%= name %></span><span class="cue-description"><%= description %></span></li>');
    var cueControlTemplate = _.template('<ul class="cue-controls clearfix"><li><a href="#" data-state="standby" class="cue-control btn btn-mini btn-danger">S</a></li><li><a href="#" data-state="warning" class="cue-control btn btn-mini btn-warning">W</a></li><li><a href="#" data-state="go" class="cue-control btn btn-mini btn-success">G</a></li></ul>');

    function CueList(options) {
        var that = this;

        this.options = _.defaults({}, options, {
            cues: [],
            stageManager: false,
            keys: {
                number: 'Number',
                name: 'Name',
                description: 'Description',
                cues: 'Cues',
                shortBookName: 'ShortBookName'
            },
            selectCue: $.noop,
            triggerCue: $.noop,
            mouseoverCue: $.noop,
            mouseoutCue: $.noop
        });

        this.$cueList = $(cueListTemplate())
            .on('click', '> li', function (e) {
                var $this = $(this);
                e.stopPropagation();
                that.options.selectCue($this.data('number'), parseInt($this.data('id'), 10));
            })
            .on('mouseover', '> li', function (e) {
                var $this = $(this);
                e.stopPropagation();
                that.options.mouseoverCue($this.data('number'), parseInt($this.data('id'), 10));
            })
            .on('mouseout', '> li', function (e) {
                var $this = $(this);
                e.stopPropagation();
                that.options.mouseoutCue($this.data('number'), parseInt($this.data('id'), 10));
            });

        _.each(options.cues, function (cue, index) {
            that.addCue(cue, index);
        });
    }

    function findCue($cueList, number) {
        return $cueList.find('[data-number="' + number + '"]');
    }

    CueList.prototype = {
        addCue: function (cue, id, afterCueNumber) {
            var that = this;
            var keys = this.options.keys;

            var $newCue = $(cueTemplate({
                name: cue[keys.name],
                number: cue[keys.number],
                description: cue[keys.description],
                id: id
            }));

            if (!_.isUndefined(cue[keys.cues])) {
                $newCue.append('<span class="cue-description">(' + _.reduce(cue[keys.cues], function (m, c) { return m + c[keys.shortBookName] + c[keys.number] + ', ' }, '').slice(0, -2) + ')</span>');
            }

            if (this.options.stageManager) {
                var $cct = $(cueControlTemplate());
                $newCue.append($cct);
                $cct.on('click', '.cue-control', function (e) {
                    var $this = $(this);
                    e.preventDefault();
                    e.stopPropagation();
                    that.options.triggerCue(cue[keys.number], id, $this.data('state'));
                });
            }

            if (_.isUndefined(afterCueNumber)) {
                $newCue.appendTo(this.$cueList);
            } else {
                $newCue.insertAfter(findCue(this.$cueList, afterCueNumber));
            }
        },

        setState: function (number, state) {
            findCue(this.$cueList, number).attr('class', '').addClass(state);
        },

        getElement: function () {
            return this.$cueList;
        }
    };

    return CueList;
});