var controller;
$(function () {
    var service = null;
    var model = new fs.model.file(service);
    var view = new fs.views.file(model);
    controller = new fs.controllers.file(model, view);

});