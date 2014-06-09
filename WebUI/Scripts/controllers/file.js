fs.controllers.file = function (model, view) {
    this._model = model;
    this._view = view;
};
fs.controllers.file.prototype = {
    confirm: function (delBtn) {
        this._view.showConfirmDialog(delBtn, this.removeFile.bind(this));
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
    showFileSizeError : function(elem) {
        this._view.showFileSizeError(elem);
    }
};