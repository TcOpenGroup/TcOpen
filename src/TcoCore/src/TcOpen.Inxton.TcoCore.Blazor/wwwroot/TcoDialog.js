const getLastItem = thePath => thePath.substring(thePath.lastIndexOf('/') + 1);
export function OpenTcoDialog(id) {

    var currentUrl = window.location.href;
    var url = '#' + getLastItem(currentUrl) ;
    if (url === id) {
        $(id).modal('show');
    }
    return true;
}


export function HideTcoDialog(id) {
    var currentUrl = window.location.href;
    var url = '#' + getLastItem(currentUrl);
    if (url === id) {
        console.log('close ' + url);
        console.log('close ' + id);
        $(id).modal('hide')
    }
    return true;
}

export function ClickSend() {
    $("#sendDialogInvoke").click();
    return true;
}

export function ClickSendClose() {
    $("#SendCloseButtonId").click();
    return true;
}

export function AddToGroup() {
    $("#AddToGroupButtonId").click();
    return true;
}