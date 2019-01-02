$(document).on("click", ".disable-user", function (e) {
    e.preventDefault();

    var btn = $(this),
        username = btn.data("username");

    if (!username) {
        app.showSweetMessage("sweet-error", app.invalidSelection);
        return;
    }

    app.confirmModal("Сигурни ли сте, че искате да деактивирате потребител <b>'" + username + "'</b>?")
        .then(function (isConfirmed) {
            if (!isConfirmed) {
                return;
            }

            btn.prop("disabled", true);
            $.ajax({
                cache: false,
                url: app.getPath('/Admin/Account/DisableUser'),
                type: 'POST',
                data: {
                    username: username
                },
                success: function (result) {
                    if (result.status === 'success') {
                        app.showSweetMessage("sweet-success", result.msg);

                        new MvcGrid(document.querySelector(".mvc-grid")).reload();
                    } else if (result.status === "warning") {
                        app.showSweetMessage("sweet-warning", result.msg);
                    } else {
                        app.showSweetMessage("sweet-error", result.msg);
                    }
                },
                complete: function () {
                    btn.prop("disabled", false);
                }
            });       
        });
});

$(document).on("click", ".enable-user", function (e) {
    e.preventDefault();

    var btn = $(this),
        username = btn.data("username");

    if (!username) {
        app.showSweetMessage("sweet-error", app.invalidSelection);
        return;
    }

    app.confirmModal("Сигурни ли сте, че искате да активирате потребител <b>'" + username + "'</b>?")
        .then(function (isConfirmed) {
            if (!isConfirmed) {
                return;
            }

            btn.prop("disabled", true);
            $.ajax({
                cache: false,
                url: app.getPath('/Admin/Account/EnableUser'),
                type: 'POST',
                data: {
                    username: username
                },
                success: function (result) {
                    if (result.status === 'success') {
                        app.showSweetMessage("sweet-success", result.msg);

                        new MvcGrid(document.querySelector(".mvc-grid")).reload();
                    } else if (result.status === "warning") {
                        app.showSweetMessage("sweet-warning", result.msg);
                    } else {
                        app.showSweetMessage("sweet-error", result.msg);
                    }
                },
                complete: function () {
                    btn.prop("disabled", false);
                }
            });
        });
});