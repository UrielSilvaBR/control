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

    $('#Aliquota').maskMoney({
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

    $('#Days_Modal').focusout(function () {
        ObterCalculoAliquota($(this).val());
    });

    $('#Days').focusout(function () {
        ObterCalculoAliquota($(this).val());
    });

});

function ObterCalculoAliquota(qtdDias)
{
    $.post("/Cadastro/ObterCalculoAliquota",
     { quantidadeDias: qtdDias },
     function (result) {
        
         if(!result.erro)
         {
             $('#AliquotaModal').val(parseFloat(result.aliquota).toFixed(2) * 100);
             $('#Aliquota').val(parseFloat(result.aliquota).toFixed(2) * 100);
         }
         else
         {
             ShowMessage(result.msg);
         }
     });
}