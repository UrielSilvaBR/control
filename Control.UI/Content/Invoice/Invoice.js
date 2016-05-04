$(document).ready(function () {
   
    $('#Invoice_Valor').maskMoney({
        prefix:'', 
        thousands:'.', 
        decimal: ',',
        precision: 2,
        affixesStay: true
    });

    $('#Invoice_DataEmissao').prop('readonly', true);

});

function FinalizarInclusaoNotaFiscal(message) {

    ShowMessage(message, true);
}