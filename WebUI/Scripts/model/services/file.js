fs.model.services.file = function (findFileUri, removeFileUri) {
    this._findFileUri = findFileUri;
    this._removeFileUri = removeFileUri;
    
    this._filesFoundObservers = [];
    this._fileRemovedObservers = [];
    this._removeFileErrorObservers = [];
};
fs.model.services.file.prototype = {
    findFilesByName: function (stringForSearch) {
        $.ajax(
            {
                type: 'POST',
                url: this._findFileUri,
                data: { fileName: stringForSearch },
                success: this._onFilesFound.bind(this),
                    //error: this._onFilesFoundError.bind(this)
            });
        return this;
    },
    removeFile: function (fileId) {
        $.ajax(
          {
              type: 'POST',
              url: this._removeFileUri,
              data: { id: fileId },
              success: this._onFileRemoved.bind(this),
              error: this._onRemoveFileError.bind(this)
          });
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
    addFileRemovedObserver: function (observer) {
        this._fileRemovedObservers.push(observer);
        return this;
    },
    addRemoveFileErrorObserver: function (observer) {
        this._removeFileErrorObservers.push(observer);
        return this;
    }
};