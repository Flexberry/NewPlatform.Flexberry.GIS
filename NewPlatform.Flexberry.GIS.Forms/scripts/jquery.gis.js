function generateTriggersClick() {

    var validatorCheckResult = true;
    if (window.Page_ClientValidate !== undefined && $.isFunction(window.Page_ClientValidate)) {
        validatorCheckResult = window.Page_ClientValidate('savedoc');
    }

    if (!validatorCheckResult) return false;

    if (document.WgeSaveHandlers !== undefined) {
        var resHandler;
        $.each(document.WgeSaveHandlers,
            function(i, handler) {
                resHandler = handler();
                if (resHandler === false) {
                    validatorCheckResult = false;
                }
            });
    }

    if (!validatorCheckResult) return false;

    var dialog = $.ics.dialog.modal({
        title: 'Выполняется генерация',
        width: 310,
        height: 110,
        content: "<div />",
        resizable: false
    });

    return true;
}

function generateTriggersOnListClick() {
    var dialog = $.ics.dialog.modal({
        title: 'Выполняется генерация',
        width: 310,
        height: 110,
        content: "<div />",
        resizable: false
    });
}