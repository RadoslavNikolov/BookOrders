$(document).on("click", ".disable-category", function (e) {
    e.preventDefault();

    var btn = $(this),
        name = btn.data("name");

    if (!name) {
        app.showSweetMessage("sweet-error", app.invalidSelection);
        return;
    }

    app.confirmModal("Сигурни ли сте, че искате да деактивирате категория <b>'" + name + "'</b>?")
        .then(function (isConfirmed) {
            if (!isConfirmed) {
                return;
            }

            btn.prop("disabled", true);
            $.ajax({
                cache: false,
                url: app.getPath('/Admin/Category/DisableCategory'),
                type: 'POST',
                data: {
                    name: name
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

$(document).on("click", ".enable-category", function (e) {
    e.preventDefault();

    var btn = $(this),
        name = btn.data("name");

    if (!name) {
        app.showSweetMessage("sweet-error", app.invalidSelection);
        return;
    }

    app.confirmModal("Сигурни ли сте, че искате да активирате категория <b>'" + name + "'</b>?")
        .then(function (isConfirmed) {
            if (!isConfirmed) {
                return;
            }

            btn.prop("disabled", true);
            $.ajax({
                cache: false,
                url: app.getPath('/Admin/Category/EnableCategory'),
                type: 'POST',
                data: {
                    name: name
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