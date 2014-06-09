fs.views.file = function (model, filesContainer, deleteMarkClass) {
    this._container = filesContainer;
    this._deleteMarkClass = deleteMarkClass;
    this._model = model.addFilesFoundObserver(this._filesFound.bind(this))
                       .addFileRemovedObserver(this._fileRemoved.bind(this))
                       .addRemoveFileErrorObserver(this._removeFileError.bind(this));

};
fs.views.file.prototype = {
    showConfirmDialog: function (params, onYes, onNo) {
        $('<div>Do you really want to delete this file?<\div>').dialog({
            modal: true,
            title: "Delete File",
            dialogClass: 'css-doubleBtnDialog',
            resizable: false,
            open: function () { $(".ui-dialog-titlebar-close").hide(); },
            buttons:
            {
                'Yes': function () {
                    $(this).dialog("destroy");
                    onYes(params);
                },
                'No': function () {
                    $(this).dialog("destroy");
                }
            }
        });
        $('.ui-button:last').focus();
    },
    showFileSizeError: function(elem) {
        $("<div>The file size should be less then 50 MB</div>").insertAfter(elem);
    },
    _filesFound: function (data) {
        $(this._container).empty().append(data);
        return this;
    },
    _fileRemoved: function () {
        $(this._deleteMarkClass).remove();
    },
    _removeFileError: function () {
        $('<div>Sorry! This file can not be removed now, try later.<\div>').dialog({
            modal: true,
            title: "Error Message",
            resizable: false,
            open: function () { $(".ui-dialog-titlebar-close").hide(); },
            buttons: [
                {
                text: 'Close',
                click: function () {
                    $(this).dialog("destroy");
                }
            }]
        });
        $(".ui-dialog-titlebar-close ").focus();
    }
};