fs.views.user = function (model, deleteMarkClass, dialog) {
    this._deleteMarkClass = deleteMarkClass;
    this._model = model.addUserRemovedObserver(this._userRemoved.bind(this))
                       .addRemoveUserErrorObserver(this._removeUserError.bind(this));
                       
    this._dialog = dialog;

};
fs.views.user.prototype = {
    showConfirmDeleteDialog: function (params, onYes) {
        var htmlMsg = '<div>Do you really want to delete this user?<\div>';
        var title = "Delete User";
        this._dialog.showConfirmDialog(params, onYes, htmlMsg, title);
    },
    _userRemoved: function () {
        $(this._deleteMarkClass).remove();
    },
    _removeUserError: function () {
        var htmlMsg = '<div>Sorry! This user can not be removed now, try later.<\div>';
        var title = "Error Message";
        this._dialog.showInformDialog(htmlMsg, title);
    }
};