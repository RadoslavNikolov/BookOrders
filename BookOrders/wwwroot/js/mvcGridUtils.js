[].forEach.call(document.getElementsByClassName('mvc-grid'), function (element) {
    // Triggered before grid starts loading
    element.addEventListener('reloadstart', function (e) {
        var imgPath = app.getPath("/images/loading.gif"),
            img = '<img alt="loading" class="loading-img" src="' + imgPath + '" />',
            container = $(e.detail.grid.element).closest(".mvc-table-content");

        if (container) {
            container.prepend(img);
        }
    });
    new MvcGrid(element);
});

//// Triggered before grid starts loading
document.addEventListener('reloadstart', function (e) {
    var imgPath = app.getPath("/images/loading.gif"),
        img = '<img alt="loading" class="loading-img" src="' + imgPath + '" />',
        container = $(e.detail.grid.element).closest(".mvc-table-content");

    if (container) {
        container.prepend(img);
    }
});

// Triggered after grid stop loading
document.addEventListener('reloadend', function (e) {
    var img = $(e.detail.grid.element).closest(".mvc-table-content").find(".loading-img"),
        tableWrapper = $(e.detail.grid.element).closest(".mvc-grid-utils"),
        togglebtn = tableWrapper.find(".mvc-grid-toggle-btn");

    if (img) {
        img.remove();
    }

    if (togglebtn && togglebtn.hasClass("fa-plus")) {
        console.log("chech for toggle btn after reload ended");
        togglebtn.removeClass("fa-plus");
        togglebtn.addClass("fa-minus");
    }
});

// Triggered after grid reload fails
document.addEventListener('reloadfail', function (e) {
    var img = $(e.detail.grid.element).closest(".mvc-table-content").find(".loading-img"),
        tableWrapper = $(e.detail.grid.element).closest(".mvc-grid-utils"),
        togglebtn = tableWrapper.find(".mvc-grid-toggle-btn");

    if (img) {
        img.remove();
    }

    if (togglebtn && togglebtn.hasClass("fa-plus")) {
        //console.log("chech for toggle btn after reload failed");
        togglebtn.removeClass("fa-plus");
        togglebtn.addClass("fa-minus");
    }
});

function exportGridToExcel(btn, e) {
    var $btn = $(this),
        uniqueId = $btn.data("uniqueid"),
        action = $btn.data("action"),
        controller = $btn.data("controller");

    var grid = new MvcGrid(document.querySelector('#' + uniqueId + ' .mvc-grid'));
    if (!grid || !action || !controller) {
        return;
    }

    console.log(grid.query);
    window.location = app.getPath("/" + controller + "/" + action) + grid.query;
}

function reloadAjaxMvcGrid() {
    var tableWrapper = $(this).closest(".mvc-grid-utils"),
        containerDiv = tableWrapper.first(".mvc-table-content"),
        grid = containerDiv.find(".mvc-grid"),
        togglebtn = tableWrapper.find(".mvc-grid-toggle-btn");

    if (grid[0]) {
        new MvcGrid(grid[0]).reload();
    }
}

function toggleMvcGrid() {
    var tableWrapper = $(this).closest(".mvc-grid-utils"),
        containerDiv = tableWrapper.first(".mvc-table-content"),
        grid = containerDiv.find(".mvc-grid"),
        togglebtn = tableWrapper.find(".mvc-grid-toggle-btn");

    if (grid) {
        if (tableWrapper.hasClass('table-wrapper')) {
            tableWrapper.removeClass('table-wrapper');
        } else {
            tableWrapper.addClass('table-wrapper');
        }

        if (togglebtn) {
            if (togglebtn.hasClass("fa-minus")) {
                togglebtn.removeClass("fa-minus");
                togglebtn.addClass("fa-plus");
            } else if (togglebtn.hasClass("fa-plus")) {
                togglebtn.removeClass("fa-plus");
                togglebtn.addClass("fa-minus");
            }
        }

        grid.toggle();
    }
}

$(document).on("click", ".mvc-grid-reload-btn", reloadAjaxMvcGrid);
$(document).on("click", ".mvc-grid-toggle-btn", toggleMvcGrid);
$(document).on("click", ".mvc-grid-excel-export-btn", exportGridToExcel);