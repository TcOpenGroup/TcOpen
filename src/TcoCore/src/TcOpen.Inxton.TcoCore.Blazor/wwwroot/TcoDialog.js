export function OpenTcoDialog(id) {
    $(id).modal('show');
    return true;
}


export function HideTcoDialog(id) {
    $(id).modal('hide')
    return true;
}