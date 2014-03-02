if (typeof PS == 'undefined') {
    PS = {};

}

var $bookmarkDialog = $('#bookmarkDialog');
var $bookmarkForm = $('#bookmarkForm');

var cancelConfirmationDialog = function () {
    $bookmarkDialog.unbind('keydown');
    $bookmarkDialog.dialog('close');
};

var isValid = function () {
    return $bookmarkForm.valid();
};

var bindDialogEnterKey = function (onSuccess) {
    $bookmarkDialog.keydown(function (e) {
        if (e.keyCode == 13) {
            onSuccess();
            return false;
        }
    });
};

var updateButtonCaption = function ($buttonInDialog, text) {
    var $span = $('span.ui-button-text', $buttonInDialog);
    $span.text(text);
};

var showBookmarkCreated = function (classToAdd, message) {
    $('#bookmarkNotifier').html(message).attr('class', classToAdd).slideToggle().delay(2000).slideToggle();
};

PS.bookmarkIt = function (courseName, moduleName, moduleAuthor, clipName, postUrl) {
    $bookmarkForm.validate();
    $bookmarkDialog.attr('title', 'Add Bookmark');

    var bookmark = function () {
        if (!isValid()) {
            return;
        }
        var $submitButton = $('#submitBookmarkBtn');
        $submitButton.attr('disabled', 'disabled');
        $('#cancelBookmarkBtn').attr('disabled', 'disabled');
        updateButtonCaption($submitButton, "Please wait...");

        var displayName = $("#txtDisplayName").val();

        $.ajax(
            {
                type: "POST",
                url: postUrl,
                data: { "courseName": courseName, "moduleName": moduleName, "moduleAuthor": moduleAuthor, "clipName": clipName, "displayName": displayName },
                success: function (result) {

                    cancelConfirmationDialog();

                    if (result.success)
                        showBookmarkCreated('success', 'Bookmark Created');
                    else
                        showBookmarkCreated('failure', 'Unable to create Bookmark: ' + result.message);
                },
                error: function (req, status, error) {
                    cancelConfirmationDialog();
                    showBookmarkCreated('failure', 'Unable to create Bookmark, status: ' + status);
                }
            });
    };

    bindDialogEnterKey(bookmark);
    $bookmarkDialog.dialog(
                {
                    modal: true,
                    resizable: false,
                    minWidth: 500,
                    minHeight: 200,
                    buttons: {
                        "Bookmark": {
                            click: bookmark,
                            id: "submitBookmarkBtn",
                            text: "Add"
                        },
                        "Cancel": {
                            click: cancelConfirmationDialog,
                            id: "cancelBookmarkBtn",
                            text: "Cancel"
                        }
                    }
                });
};

PS.renameBookmark = function (bookmarkId, postUrl) {
    $bookmarkForm.validate();
    $bookmarkDialog.attr('title', 'Rename Bookmark');

    var rename = function () {
        if (!isValid()) {
            return;
        }
        var $submitButton = $('#submitBookmarkBtn');
        $submitButton.attr('disabled', 'disabled');
        $('#cancelBookmarkBtn').attr('disabled', 'disabled');
        updateButtonCaption($submitButton, "Please wait...");

        var displayName = $("#txtDisplayName").val();

        $.ajax(
                    {
                        type: "POST",
                        url: postUrl,
                        data: { "bookmarkId": bookmarkId, "displayName": displayName },
                        success: function (result) {
                            cancelConfirmationDialog();
                            if (result.success) {
                                window.location.reload();
                            }
                            else
                                showBookmarkCreated('failure', 'Unable to update Bookmark: ' + result.message);
                        },
                        error: function (req, status, error) {
                            cancelConfirmationDialog();
                            showBookmarkCreated('failure', 'Unable to update Bookmark, status: ' + status);
                        }
                    });
    };

    bindDialogEnterKey(rename);
    $bookmarkDialog.dialog(
                {
                    modal: true,
                    resizable: false,
                    minWidth: 500,
                    minHeight: 200,
                    buttons: {
                        "Bookmark": {
                            click: rename,
                            id: "submitBookmarkBtn",
                            text: "Rename"
                        },
                        "Cancel": {
                            click: cancelConfirmationDialog,
                            id: "cancelBookmarkBtn",
                            text: "Cancel"
                        }
                    }
                });
};