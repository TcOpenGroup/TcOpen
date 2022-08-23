export function OpenTcoDialog(id) {
    $(id).modal('show');
    return true;
}


export function HideTcoDialog(id) {
    $(id).modal('hide')
    return true;
}

//export function ClickSend() {
//    $("#sendDialogInvoke").click();
//    return true;
//}

export function ClickSendClose() {
    $("#SendCloseButtonId").click();
    return true;
}
