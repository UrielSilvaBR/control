$(document).ready(function () {

    $('#Contact_Phone_Modal').mask('(99) 9999-9999');

    $('#Contact_Mobile_Modal').mask('(99) 99999-9999');

    $('#Order_CustomerID').change(function () {
        $('#hdnCustomerID_Contato_Modal').val($(this).val());
    });

    var idCliente = $('#Order_CustomerID').val();

    if (idCliente > 0)
        $('#hdnCustomerID_Contato_Modal').val($('#Order_CustomerID').val());
});



