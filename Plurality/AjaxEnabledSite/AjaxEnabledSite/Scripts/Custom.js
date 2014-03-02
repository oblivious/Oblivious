function ShowMessage(msg) {
    var span = document.getElementById('OutputSpan');
    span.innerHTML = msg;
}

function pageLoad() {
    ShowMessage("Hello AJAX!");
}