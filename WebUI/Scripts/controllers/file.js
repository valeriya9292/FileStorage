fs.controllers.file = function (model, view) {
    this._model = model;
    this._view = view;
};
fs.controllers.file.prototype = {
    checkIfExists: function (fileName) {
        this._model.checkIfExists(fileName);
        return this;
    },
    confirm: function (delBtn) {
        this._view.showConfirmDeleteDialog(delBtn, this.removeFile.bind(this));
        return this;
    },
    findFilesByName: function (input) {
        var stringForSearch = $(input).val();
        this._model.findFilesByName(stringForSearch);
        return this;
    },
    removeFile: function (deleteBtn) {
        var tr = $(deleteBtn).parents().filter('tr').addClass('js-delete-marked');
        var fileId = $('input[type=hidden]', tr).val();
        this._model.removeFile(fileId);
        return this;
    },
};