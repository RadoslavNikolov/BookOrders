$(document).on("click", ".delete-role", function (e) {
    e.preventDefault();

    var btn = $(this),
        roleName = btn.data("rolename");

    if (!roleName) {
        app.showSweetMessage("sweet-error", app.invalidSelection);
        return;
    }

    app.confirmModal("Сигурни ли сте, че искате да изтриете роля <b>'" + roleName + "'</b>?")
        .then(function (isConfirmed) {
            if (!isConfirmed) {
                return;
            }

            btn.prop("disabled", true);
            $.ajax({
                cache: false,
                url: app.getPath('/Admin/Account/DeleteRole'),
                type: 'POST',
                data: {
                    roleName: roleName
                },
                success: function (result) {
                    if (result.status === 'success') {
                        app.showSweetMessage("sweet-success", result.msg);

                        window.location = app.getPath('/Admin/Role');
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