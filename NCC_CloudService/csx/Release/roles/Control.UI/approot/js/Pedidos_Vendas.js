var giCount = 1;
$(document).ready(function () {

    $('#btnAddItem').click(function () {
        AdicionarItemNotaFiscal();
    });
    
});

function LimparItemPedido() {

}

function AdicionarItemNotaFiscal() {
    

    var rowEmpty = $('#gdvItensNotaFiscal > tbody > tr').find('.dataTables_empty').length == 1;
    var rowCount = $('#gdvItensNotaFiscal > tbody > tr').length;

    if (rowEmpty)
        rowCount = rowCount - 1;

    if (giCount > rowCount)
        giCount = giCount;
    else
        giCount = rowCount + 1;

    

    var idProduto = $('#ddlProduto option:selected').val();
    var descricaoProduto = $('#ddlProduto option:selected').text();
    var quantidade = $('#ItemPedido_QuantityOrder').val();
    quantidade = parseFloat(quantidade).toFixed(2);
    quantidade = quantidade.replace(".", ",");
    var precoUnitario = $('#ItemPedido_UnitPrice').val();
    //var quantidadeEntregue = $('#InvoiceItem_QuantityDeliver').val();
    //var desconto = $('#InvoiceItem_ItemDiscount').val();
    var precoTotal = $('#ItemPedido_TotalPrice').val();

    $('#gdvItensNotaFiscal').dataTable().fnAddData([
       0,   
       descricaoProduto,
       quantidade,
       precoUnitario,       
       precoTotal
    ]);
    alert('Adicionar peddido');
}
