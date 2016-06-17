$(document).ready(function () {
});

function ConfirmarPedido(idPedido) {
    alert('teste');

    waitingDialog.show('Proposta confirmada', { dialogSize: 'sm', progressType: 'success' });

    setTimeout(function () {
        $.ajax({
            url: '/Pedido/ConfirmarProposta',
            data: { OrderID: idPedido },
            type: 'POST',
            success: function (result) {
                waitingDialog.hide();
                ShowSuccess(result);
            }
        });
    }, 1000);
}