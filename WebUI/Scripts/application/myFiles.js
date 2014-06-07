var controller;
$(function () {
    var service = new fs.model.services.file("/File/FindMyFilesByName", '/File/Delete');
    var model = new fs.model.file(service);
    var view = new fs.views.file(model, '.js-table', '.js-delete-marked');
    controller = new fs.controllers.file(model, view);

    $('#searchBtn').click(function (e) {
        e.preventDefault();
        controller.findFilesByName("#stringForSearch");
    });
    $("#stringForSearch").keydown(function (e) {
        if (e.keyCode == 13)
            controller.findFilesByName(this);
    });
});