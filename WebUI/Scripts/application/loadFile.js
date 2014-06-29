var controller;
$(function () {
    var service = new fs.model.services.file("/File/FindMyFilesByName", '/File/Delete', '/File/IsFileExisting');
    var model = new fs.model.file(service);
    var dialog = new fs.views.utils.dialog();
    var view = new fs.views.file(model, '.js-table', '.js-delete-marked', ".js-load-form", dialog);
    controller = new fs.controllers.file(model, view);

    $("#file").change(function () {
        $(this).blur().focus();
    });

    var maxSize = 52428800;
    $(".js-load-form").validate({
        rules: {
            file: {
                fileSize: maxSize,
                required: true
            }
        },
        errorClass: 'field-validation-error'
    });
    $.validator.addMethod('fileSize', function (value, element, param) {
        return (element.files[0].size <= param);
    }, "File must be less then " + maxSize / 1048576 + " MB!");

    $(".js-load-button").click(function (event) {
        event.preventDefault();
        if ($("#file")[0].files.length == 0)
            $(".js-load-form").submit();
        else {
            var name = $("#file")[0].files[0].name;
            controller.checkIfExists(name);
        }
    });
});