// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.
document.addEventListener('reloadend', function (e) {
    $('[data-toggle="tooltip"]').tooltip();
});

$(function () {
    $('[data-toggle="tooltip"]').tooltip();
});

[].forEach.call(document.getElementsByClassName('mvc-grid'), function (element) {
    new MvcGrid(element);
});

MvcGrid.prototype.lang = {
    text: {
        'contains': 'Съдържа',
        'not-equals': 'Не е равно на',
        'equals': 'Равно на',
        'starts-with': 'Започва с',
        'ends-with': 'Завършва на'
    },
    number: {
        'equals': 'Равно на',
        'not-equals': 'Не е равно на',
        'less-than': 'По-малко от',
        'greater-than': 'По-голямо от',
        'less-than-or-equal': 'По-малко или равно на',
        'greater-than-o-requal': 'По-голямо или равно на'
    },
    date: {
        'equals': 'Равно на',
        'not-equals': 'Не е равно на',
        'earlier-than': 'По-малко от',
        'later-than': 'По-голямо от',
        'earlier-than-or-equal': 'По-малко или равно на',
        'later-than-or-equal': 'По-голямо или равно на'
    },
    enum: {
        'equals': 'Равно на',
        'not-equals': 'Не е равно на'
    },
    boolean: {
        'equals': 'Равно на',
        'not-equals': 'Не е равно на',
        'yes': 'Да',
        'no': 'Не'
    },
    bool: {
        'yes': 'Да',
        'no': 'Не'
    },
    guid: {
        'equals': 'Равно на',
        'not-equals': 'Не е равно на'
    },
    filter: {
        'apply': '✔',
        'remove': '✘'
    },
    operator: {
        'select': '',
        'and': 'и',
        'or': 'или'
    }
};