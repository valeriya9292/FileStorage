fs.views.file = function (model, filesContainer, deleteMarkClass, loadForm, dialog) {
    this._container = filesContainer;
    this._deleteMarkClass = deleteMarkClass;
    this._loadForm = loadForm;
    this._model = model.addFilesFoundObserver(this._filesFound.bind(this))
                       .addFileRemovedObserver(this._fileRemoved.bind(this))
                       .addRemoveFileErrorObserver(this._removeFileError.bind(this))
                       .addCheckedForExistingObserver(this._fileCheckedIfExists.bind(this));
    this._dialog = dialog;

};
fs.views.file.prototype = {
    showConfirmDeleteDialog: function (params, onYes) {
        var htmlMsg = '<div>Do you really want to delete this file?<\div>';
        var title = "Delete File";
        this._dialog.showConfirmDialog(params, onYes, htmlMsg, title);
    },
    _filesFound: function (data) {
        $(this._container).empty().append(data);
        return this;
    },
    _fileRemoved: function () {
        $(this._deleteMarkClass).remove();
    },
    _fileCheckedIfExists: function (isExisting) {
        if (isExisting == 'False')
            $(this._loadForm).submit();
        else {
            this._showLoadDialog();
        }
    },
    _removeFileError: function () {
        var htmlMsg = '<div>Sorry! This file can not be removed now, try later.<\div>';
        var title = "Error Message";
        this._dialog.showInformDialog(htmlMsg, title);
    },
    _showLoadDialog: function () {
        var htmlMsg = '<div>These file already exits in your file storage. Do you want to replace it?<\div>';
        var title = "Replace File";
        var onYes = this._sendForm.bind(this);
        var params;
        this._dialog.showConfirmDialog(params, onYes, htmlMsg, title);
    },
    _sendForm: function() {
        $(this._loadForm).submit();
    }
};