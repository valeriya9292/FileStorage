﻿var controller;
$(function () {
    var service = new fs.model.services.file("/File/FindPublicFilesByName", '/File/Delete', '/File/IsFileExisting');
    var model = new fs.model.file(service);
    var dialog = new fs.views.utils.dialog();
    var view = new fs.views.file(model, '.js-table', '.js-delete-marked', ".js-load-form", dialog);
    controller = new fs.controllers.file(model, view);

    $('#searchBtn').click(function (e) {
        e.preventDefault();
        controller.findFilesByName("#stringForSearch");
    });
    $("#stringForSearch").keydown(function(e) {
        if (e.keyCode == 13)
            controller.findFilesByName(this);
    });
    $("#stringForSearch").autocomplete({
        source: function (request, response) {
            $.get("/File/FindFilesForAutoCompl", {
                fileName: request.term
            }, function (data) {
                response(data);
            });
        },
    });
});