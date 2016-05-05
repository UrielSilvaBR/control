$(document).ready(function () {
   
    $('#Invoice_Valor').maskMoney({
        prefix:'R$ ', 
        thousands:'.', 
        decimal: ',',
        precision: 2,
        affixesStay: true
    });

    //$('#Invoice_DataEmissao').prop('readonly', true);

    $('#Invoice_DataEmissao').mask('99/99/9999');

});

function FinalizarInclusaoNotaFiscal(message) {

    ShowMessage(message, true);
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

}