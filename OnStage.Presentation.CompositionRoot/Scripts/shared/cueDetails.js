define(['jquery', 'underscore'], function ($, _) {
    'use strict';

    var cueDetailsTemplate = '<div class="cue-details"><div class="details-header"></div>' +
                                                      '<div class="cue-header"><span class="cue-name"></span> (<span class="cue-number"></span>)</div>' +
                                                      '<div class="cue-description"></div>' +
                                                      '<div class="subcue-header">Sub Cues:</div>' +
                                                      '<ul class="sub-cues"></ul></div>';

    var subCueTemplate = _.template('<li><%= shortBookName %><%= number %> - <%= name %></li>');

    function CueDetails(options) {
        this.options = _.defaults({}, options, {
            header: 'Cue Details',
            keys: {
                number: 'Number',
                name: 'Name',
                description: 'Description',
                cues: 'Cues',
                shortBookName: 'ShortBookName'
            }
        });

        this.$cueDetails = $(cueDetailsTemplate);
        this.$cueDetails.find('.details-header').text(this.options.header);
    }

    CueDetails.prototype = {
        setCue: function (cue, state) {
            var keys = this.options.keys;
            var that = this;
            var sm = !_.isUndefined(cue[keys.cues]);

            if (state) {
                this.$cueDetails.find('.details-header').removeClass('standby warning go stage-manager').addClass(state);
            } else {
                this.$cueDetails.find('.details-header').removeClass('standby warning go').addClass('stage-manager');
            }

            if (_.isNull(cue)) { return; }

            this.$cueDetails.find('.cue-name').text(cue[keys.name]);
            this.$cueDetails.find('.cue-number').text(cue[keys.number]);
            this.$cueDetails.find('.cue-description').text(cue[keys.description]);
            if (sm) {
                this.$cueDetails.find('.subcue-header').show();
                var $subCues = this.$cueDetails.find('.sub-cues').empty().show();
                _.each(cue[keys.cues], function (subCue) {
                    $subCues.append(subCueTemplate({
                        shortBookName: subCue[keys.shortBookName],
                        number: subCue[keys.number],
                        name: subCue[keys.name]
                    }));
                });
            } else {
                this.$cueDetails.find('.subcue-header').hide();
                this.$cueDetails.find('.sub-cues').empty().hide();
            }
        },

        getElement: function () {
            return this.$cueDetails;
        }
    };

    return CueDetails;
});