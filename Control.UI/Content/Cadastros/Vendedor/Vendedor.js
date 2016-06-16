$(document).ready(function () {

    $('#PercentCommission').maskMoney({
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

    $('#Order_CustomerID').change(function () {
        $('#hdnCustomerID_Vendedor_Modal').val($(this).val());
    });

    var idCliente = $('#Order_CustomerID').val();

    if (idCliente > 0)
        $('#hdnCustomerID_Vendedor_Modal').val($('#Order_CustomerID').val());

    $('#ckbVendedorAtivo').prop('checked', true);

});
