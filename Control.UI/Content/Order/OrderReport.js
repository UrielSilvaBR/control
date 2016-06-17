$(document).ready(function () {
    $(".show-sidebar").click();
});

function ConfirmarPedido(idPedido) {
    

    waitingDialog.show('Proposta confirmada', { dialogSize: 'sm', progressType: 'success' });

    setTimeout(function () {
        $.ajax({
            url: '/Pedido/ConfirmarProposta',
            data: { OrderID: idPedido },
            type: 'POST',
            success: function (result) {
                waitingDialog.hide();
                ShowSuccess(result);
                window.location = '/Invoice/Invoice?InvoiceID=' + idPedido;
            }
        });
    }, 1000);
};

function ImprimirPedido() {
    

    var printContents = document.getElementById('dvPrintArea').innerHTML;
    var originalContents = document.body.innerHTML;

    document.body.innerHTML = printContents;

    window.print();

    document.body.innerHTML = originalContents;
}

function ExportarPDF(idPedido) {

    alert('teste');
    waitingDialog.show('Gerando Arquivo...', { dialogSize: 'sm', progressType: 'success' });

    setTimeout(function () {
        $.ajax({
            url: '/Pedido/ExportarPDF',
            data: { OrderID: idPedido },
            type: 'POST',
            success: function (result) {
                waitingDialog.hide();
                //ShowSuccess(result);
                //window.location = '/Invoice/Invoice?InvoiceID=' + idPedido;
            }
        });
    }, 1000);
};

function AbrirModalPedido(idPedido)
{
    $('#modal-converter-pedido').modal('show');
}

function AbrirModalEmail(idPedido)
{
    $('#modal-email-proposta').modal('show');
}

function FinalizarConverterPedido() {

    $('#modal-converter-pedido').modal('hide');
    ShowSuccess('Pedido Cadastrado.');
}

function FinalizarEnvioEmail(idPedido) {

    $('#modal-email-proposta').modal('hide');
    //incluir rotina envio email
}

function ValidarConvertPedidoModal() {
        
    var idOrder = $('#hdOrderId').val();
    alert('teste=>' + idOrder);
    if (idOrder == 0) {
        ShowMessage('Não foi possivel gravar o pedido para esta proposta, tente novamente.', false);
        return false;
    }
}