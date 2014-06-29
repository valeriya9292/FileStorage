fs.model.user = function (service) {
    this._service = service.addUserRemovedObserver(this._onUserRemoved.bind(this))
                           .addRemoveUserErrorObserver(this._onRemoveUserError.bind(this));
                           
    this._userRemovedObservers = [];
    this._removeUserErrorObservers = [];
};
fs.model.user.prototype = {
    removeUser: function (userId) {
        this._service.removeUser(userId);
        return this;
    },
    _onUserRemoved: function (data) {
        $.each(this._userRemovedObservers, function (key, val) {
            val(data);
        });
        return this;
    },
    _onRemoveUserError: function () {
        $.each(this._removeUserErrorObservers, function (key, val) {
            val();
        });
        return this;
    },
    addUserRemovedObserver: function (observer) {
        this._userRemovedObservers.push(observer);
        return this;
    },
    addRemoveUserErrorObserver: function (observer) {
        this._removeUserErrorObservers.push(observer);
        return this;
    }
};