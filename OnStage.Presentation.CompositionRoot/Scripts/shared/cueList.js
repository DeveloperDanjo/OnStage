define(['jquery', 'underscore'], function ($, _) {
    'use strict';

    var cueListTemplate = _.template('<ol class="cue-list" />');
    var cueTemplate = _.template('<li data-number="<%= number %>"><span class="cue-name"><%= number %> - <%= name %></span><span class="cue-description"><%= description %></span></li>');

    function CueList(options) {
        var that = this;

        this.options = _.defaults({}, options, {
            cues: [],
            keys: {
                number: 'Number',
                name: 'Name',
                description: 'Description'
            }
        });

        this.$cueList = $(cueListTemplate());

        _.each(options.cues, function (cue) {
            that.addCue(cue);
        });
    }

    function findCue($cueList, number) {
        return $cueList.find('[data-number="' + number + '"]');
    }

    CueList.prototype = {
        addCue : function (cue, afterCueNumber) {
            var keys = this.options.keys;

            var $newCue = $(cueTemplate({
                name : cue[keys.name],
                number : cue[keys.number],
                description : cue[keys.description],
            }));

            if (_.isUndefined(afterCueNumber)) {
                $newCue.appendTo(this.$cueList);
            } else {
                $newCue.insertAfter(findCue(this.$cueList, afterCueNumber));
            }
        },

        setState : function (number, state) {
            findCue(this.$cueList, number).attr('class', '').addClass(state);
        },

        getElement : function () {
            return this.$cueList;
        }
    };

    return CueList;
});