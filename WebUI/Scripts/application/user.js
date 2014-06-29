var controller;
$(function () {
    var service = new fs.model.services.user("/Admin/DeleteUser");
    var model = new fs.model.user(service);
    var dialog = new fs.views.utils.dialog();
    var view = new fs.views.user(model, '.js-delete-marked', dialog);
    controller = new fs.controllers.user(model, view);
    
    $(".js-delete-button").click(function () {
        controller.confirm(this);
    });
});