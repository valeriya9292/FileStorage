fs.views.utils.dialog = function () {
    
};
fs.views.utils.dialog.prototype = {
    showConfirmDialog: function (params, onYes, htmlMsg, title) {
        $(htmlMsg).dialog({
            modal: true,
            title: title,
            dialogClass: 'css-doubleBtnDialog',
            resizable: false,
            open: function () {
                $(".ui-dialog-titlebar-close").hide();
                $(":button:contains('No')").focus();
            },
            buttons:
            {
                'Yes': function (e) {
                    e.preventDefault();
                    $(this).dialog("destroy");
                    onYes(params);
                },
                'No': function (e) {
                    e.preventDefault();
                    $(this).dialog("destroy");
                }
            }
        });
        $('.ui-button:last').focus(htmlMsg, title);
    },
    showInformDialog: function (htmlMsg, title) {
        $(htmlMsg).dialog({
            modal: true,
            title: title,
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