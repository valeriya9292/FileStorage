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

    $("#file").change(function () {
        $(this).blur().focus();
    });

    var maxSize = 52428800;
    $("form").validate({
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
});