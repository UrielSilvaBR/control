$(document).ready(function () {

    var CustomerID = $('#PaymentTerm_Id_Modal').val();

    if (CustomerID > 0)
        $('#btnCadastrarModal').val('Alterar');
    else
        $('#btnCadastrarModal').val('Cadastrar');


    $('#AliquotaModal').maskMoney({
        prefix: '',
        allowZero: false,
        allowNegative: false,
        defaultZero: true,
        thousands: '.',
        decimal: ',',
        precision: 2,
        affixesStay: false,
        symbolPosition: 'left'
    });

    //$('#Order_CustomerID').change(function () {
    //    $('#hdnCustomerID_CondicaoPagamento_Modal').val($(this).val());
    //});

    var idCliente = $('#Order_CustomerID').val();

    if (idCliente > 0)
        $('#hdnCustomerID_CondicaoPagamento_Modal').val($('#Order_CustomerID').val());

    $('#ckbCondicaoPagamentoAtiva').prop('checked', true);

});