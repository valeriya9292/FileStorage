fs.model.services.user = function (removeUserUri) {
    this._removeUserUri = removeUserUri;
    
    this._userRemovedObservers = [];
    this._removeUserErrorObservers = [];
};
fs.model.services.user.prototype = {
    removeUser: function (userId) {
        $.ajax(
          {
              type: 'POST',
              url: this._removeUserUri,
              data: { userId: userId },
              success: this._onUserRemoved.bind(this),
              error: this._onRemoveUserError.bind(this)
          });
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