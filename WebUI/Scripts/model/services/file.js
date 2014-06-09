fs.model.services.file = function (findFileUri, removeFileUri, checkExistingFileUri) {
    this._findFileUri = findFileUri;
    this._removeFileUri = removeFileUri;
    this._checkExistingFileUri = checkExistingFileUri;

    this._checkedForExistingObservers = [];
    this._filesFoundObservers = [];
    this._fileRemovedObservers = [];
    this._removeFileErrorObservers = [];
};
fs.model.services.file.prototype = {
    checkIfExists: function(fileName) {
        $.ajax({
            type: "POST",
            url: this._checkExistingFileUri,
            data: { fileName: fileName },
            success: this._onCheckedForExisting.bind(this),
            //error: 
        });
    },
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
    _onCheckedForExisting: function(isExisting) {
        $.each(this._checkedForExistingObservers, function (key, val) {
            val(isExisting);
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
    },
    addCheckedForExistingObserver : function(observer) {
        this._checkedForExistingObservers.push(observer);
        return this;
    }
};