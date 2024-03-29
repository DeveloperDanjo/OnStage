﻿require.config({
    baseUrl: "/Scripts",
    paths: {
        jquery: 'jquery-1.9.1',
        'jquery-unobtrusive': 'jquery.unobstrusive-ajax',
        'jquery-validate': 'jquery.validate',
        'jquery-validate-unobstrusive': 'jquery.validate.unobstrusive',
        signalr: 'jquery.signalR-1.0.1',
        hubs: '/signalr/hubs?',
        bootstrap: 'bootstrap',
        modernizr: 'modernizr-2.6.2',
        underscore: 'lodash',

        text: 'text'
    },
    map: {
        '*': {

        }
    },
    shim: {
        jquery: {
            init: function () { return this.$.noConflict(); }
        },
        'jquery-unobstrusive': {
            deps: ['jquery']
        },
        'jquery-validate': {
            deps: ['jquery']
        },
        'jquery-validate-unobstrusive': {
            deps: ['jquery']
        },
        signalr: {
            deps: ['jquery']
        },
        hubs: {
            deps: ['signalr']
        },
        bootstrap: {
            deps: ['jquery']
        },
        modernizr: {
            init: function () { var m = this.Modernizr; delete this.Modernizr; return m; }
        }
    }
});