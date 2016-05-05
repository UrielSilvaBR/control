var giCount = 1;
$(document).ready(function () {
   
    $('#Invoice_Valor').maskMoney({
        prefix:'R$ ', 
        thousands:'.', 
        decimal: ',',
        precision: 2,
        affixesStay: true
    });

    $('#Invoice_Numero').val('');
    $('#Invoice_Valor').val('');

    $('#Invoice_DataEmissao').mask('99/99/9999');


    $('#btnAdicionarItemNf').click(function()
    {
        AdicionarItemNotaFiscal();
    })

});

function FinalizarInclusaoNotaFiscal(notaFiscal) {


    ShowMessage(notaFiscal, true);
}


function IniciarInclusaoNotaFiscal()
{
    //var valorNf = $('#Invoice_Valor').val();
    //valorNf = valorNf.replace(".", "").replace("R$", "").trim();
    //alert(valorNf);
    //$('#Invoice_Valor').val(valorNf);
}

function AdicionarItemNotaFiscal()
{
    

    var quantidade = $('#QuantityOrder').val();
    var precoUnitario = $('#UnitPrice').val();
    var quantidadeEntregue = $('#QuantityDeliver').val();
    var desconto = $('#ItemDiscount').val();
    var precoTotal = $('#TotalPrice').val();

    $('#gdvItensNotaFiscal').dataTable().fnAddData([
        giCount,
        quantidade,
        precoUnitario,
        quantidadeEntregue,
        desconto,
        precoTotal
    ]);

    giCount++;

    $('#gdvItensNotaFiscal_filter').hide();
    $('#gdvItensNotaFiscal_info').hide();
    $('#gdvItensNotaFiscal_length').hide();

}