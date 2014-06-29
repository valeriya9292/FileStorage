fs.controllers.user = function (model, view) {
    this._model = model;
    this._view = view;
};
fs.controllers.user.prototype = {
    confirm: function (delBtn) {
        this._view.showConfirmDeleteDialog(delBtn, this.removeUser.bind(this));
        return this;
    },
    removeUser: function (deleteBtn) {
        var tr = $(deleteBtn).parents().filter('tr').addClass('js-delete-marked');
        var userId = $('input[type=hidden]', tr).val();
        this._model.removeUser(userId);
        return this;
    },
};