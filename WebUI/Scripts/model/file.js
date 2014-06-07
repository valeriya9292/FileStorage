fs.model.file = function (service) {
    this._service = service.addFilesFoundObserver(this._onFilesFound.bind(this))
                           .addFileRemovedObserver(this._onFileRemoved.bind(this))
                           .addRemoveFileErrorObserver(this._onRemoveFileError.bind(this));
    this._filesFoundObservers = [];
    this._fileRemovedObservers = [];
    this._removeFileErrorObservers = [];
};
fs.model.file.prototype = {
    findFilesByName: function (stringForSearch) {
        this._service.findFilesByName(stringForSearch);
        return this;
    },
    removeFile: function (fileId) {
        this._service.removeFile(fileId);
        return this;
    },
    _onFilesFound: function (data) {
        $.each(this._filesFoundObservers, function (key, val) {
            val(data);
        });
        return this;
    },
    _onFileRemoved: function (data) {
        $.each(this._fileRemovedObservers, function (key, val) {
            val(data);
        });
        return this;
    },
    _onRemoveFileError: function() {
        $.each(this._removeFileErrorObservers, function (key, val) {
            val();
        });
        return this;
    },
    addFilesFoundObserver: function (observer) {
        this._filesFoundObservers.push(observer);
        return this;
    },
    addFileRemovedObserver: function(observer) {
        this._fileRemovedObservers.push(observer);
        return this;
    },
    addRemoveFileErrorObserver: function (observer) {
        this._removeFileErrorObservers.push(observer);
        return this;
    }
};