app = window.app || {};
app.workingMessage = "Обработване на заявката. Моля, изчакайте...";
app.errorTryAgain = "Възникна грешка! Моля, опитайте отново.";
app.invalidSelection = "Невалиден избор.";
app.commonObj = {};
app.participants = [];


app.getPath = function (uri) {
    var protocol = location.protocol;
    var slashes = protocol.concat("//");
    var host = slashes.concat(window.location.host);
    return host.concat(uri);
};

app.showUpdateMessage = function (type, message, fadeOutTime, shouldDismiss) {
    var div, typeClass,
        $messageWrapper = $('#messageWrapper'),
        dismiss = shouldDismiss === undefined ? true : shouldDismiss;

    fadeOutTime = fadeOutTime || 7000;

    if (type === 'success') {
        typeClass = 'alert-success';
    } else if (type === 'error') {
        typeClass = 'alert-danger';
    } else if (type === 'warning') {
        typeClass = 'alert-warning';
    } else if (type === 'info') {
        typeClass = 'alert-info';
    }

    div = '<div class="alert ' + typeClass + ' alert-dismissable updateMessage" style="display: none;">' +
        '<button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>' +
        '<div class="messageHolder text-center"><strong>' + message + '</strong></div>' +
        '</div>';

    $messageWrapper.prepend($(div));

    var $node = $messageWrapper.find(".updateMessage").first();
    $node.fadeIn(function () {
        if (dismiss) {
            setTimeout(function () {
                $node.fadeOut(function () { $(this).remove(); });
            }, fadeOutTime);
        }
    });
};

app.showSweetMessage = function (type, message) {
    var icon;

    if (type === 'sweet-success') {
        icon = $.sweetModal.ICON_SUCCESS;
    } else if (type === 'sweet-warning') {
        icon = $.sweetModal.ICON_WARNING;
    } else if (type === 'sweet-error') {
        icon = $.sweetModal.ICON_ERROR;
    }

    $.sweetModal({
        content: message,
        icon: icon
    });
};

app.pleaseWaitDiv = $('<div class="modal fade" tabindex="-1" role="dialog"  aria-hidden="true" id="pleaseWaitDialog" data-backdrop="static" data-keyboard="false"> \
                                        <div class="modal-dialog"> \
                                            <div class="modal-content"> \
                                                <div style="text-align: center;"> \
                                                    <h3></h3> \
                                                </div> \
                                                <div class="modal-body"> \
                                                    <div class="progress progress-striped active"> \
                                                        <div class="bar progress-bar progress-bar-info" style="width: 100%;"> \
                                                        </div> \
                                                    </div> \
                                                </div> \
                                            </div> \
                                        </div> \
                                    </div>');
app.showPleaseWait = function (customMessage) {
    var message = customMessage || "Моля, изчакайте...";
    if (message) {
        app.pleaseWaitDiv.find(".modal-content h3").text(message);
    }
    app.pleaseWaitDiv.modal();
};

app.hidePleaseWait = function () {
    app.pleaseWaitDiv.modal("hide");
};
app.changePleaseWait = function (message) {
    if (message) {
        app.pleaseWaitDiv.find(".modal-header h1").text(message);
    }
};

$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip({
        trigger: "hover"
    });
});

$(document).ajaxComplete(function () {
    $('[data-toggle="tooltip"]').tooltip({
        trigger: "hover"
    });
});

$(document).on("click", ".blockBtn", function (e) {
    $btn = $(this);
    $btn.prop("disabled", true);
});

app.showTimer = function (stat, end, step) {
    var div,
        typeClass = "alert-info",
        elapsed = false,
        startTime = new Date(start),
        endTime = new Date(end),
        warningTIme = 15 * 60 * 1000,
        warned = false,
        $timerWrapper = $("#timerWrapper");

    div = '<div class="alert ' + typeClass + ' alert-dismissable timerContainer" style="display: none;">' +
        '<button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>' +
        '<div class="messageHolder text-center"><p><strong><span id="remaining-time" class="label label-success"></span></strong></p>' +
        '<p><span class="label label-default">Начало: ' + moment(startTime).format('DD/MM/YYYY hh:mm:ss') + '</span></p>' +
        '<p><span class="label label-default">Край: ' + moment(endTime).format('DD/MM/YYYY hh:mm:ss') + '</span></p>' +
        '<p><span class="label label-default">Стъпка: ' + step + '%</span></p>' +
        '</div>' +
        '</div>';

    $timerWrapper.prepend($(div));

    var $node = $timerWrapper.find(".timerContainer").first();
    $node.fadeIn(function () {
        setInterval(function () {
            if (elapsed) {
                return;
            }

            var diff = moment(endTime - new Date());
            var remainingTimeSpan = $("#timerWrapper").find("#remaining-time");
            if (diff <= warningTIme) {
                if (!warned) {
                    remainingTimeSpan.removeClass("label-success");
                    remainingTimeSpan.addClass("label-warning");

                    warned = true;
                }
                remainingTimeSpan.text("- " + app.padLeft(diff.days(), 2) + ":" + app.padLeft(diff.hours(), 2) + ":"
                    + app.padLeft(diff.minutes(), 2) + ":" + app.padLeft(diff.seconds(), 2));
            } else if (Math.abs(diff) <= 0) {

                remainingTimeSpan.text("00:00:00");
                remainingTimeSpan.removeClass("label-warning");
                remainingTimeSpan.addClass("label-danger");
                elapsed = true;
            }
            else {
                remainingTimeSpan.text("- " + app.padLeft(diff.days(), 2) + ":" + app.padLeft(diff.hours(), 2) + ":"
                    + app.padLeft(diff.minutes(), 2) + ":" + app.padLeft(diff.seconds(), 2));
            }
        }, 1000);

    });
};

app.padLeft = function (str, max) {
    str = str.toString();
    return str.length < max ? app.padLeft("0" + str, max) : str;
};

app.confirmModal = function (msg) {
    var deferred = $.Deferred();

    $.sweetModal({
        content: msg,
        buttons: {
            cancel: {
                label: "Отказ",
                classes: "redB bordered flat",
                action: function () {
                    deferred.resolve(false);
                }
            },

            add: {
                label: "Да",
                classes: "greenB",
                action: function () {
                    deferred.resolve(true);
                }
            }
        }
    });

    return deferred.promise();
};



