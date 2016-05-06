var giCount = 1;
$(document).ready(function () {

    InicializarModalItemNotaFiscal();

    $('#btnAdicionarItemNf').click(function () {
        AdicionarItemNotaFiscal();
    })

    ObterValorUnitarioProduto($('#ddlProduto option:selected').val());

    $('#ddlProduto').on('change', function (produto) {
        ObterValorUnitarioProduto(produto.val);
    });

    $('#InvoiceItem_ItemDiscount').focusout(function () {
        ValidarDescontoItemNotaFiscal($(this).val());
    });

});

function InicializarModalItemNotaFiscal()
{
    IniciarlizarCamposItemNotaFiscal();
    InicializarMascaraItemNotaFiscal();
    InicializarCamposReadOnly();
}

function IniciarlizarCamposItemNotaFiscal()
{
    $('#Invoice_Valor').val(0);

    if (parseInt($('#InvoiceItem_QuantityOrder').val()) == 0)
        $('#InvoiceItem_QuantityOrder').val(1);
}

function ValidarDescontoItemNotaFiscal(pValorDesconto)
{
    var valorDesconto = pValorDesconto;
    valorDesconto = valorDesconto.replace("R$", "").replace(",", "").replace(".", ",").trim();
    valorDesconto = Number(valorDesconto.replace(/[^0-9\.]+/g, ""));

    var valorUnitario = $('#InvoiceItem_UnitPrice').val();
    valorUnitario = valorUnitario.replace("R$", "").replace(",", "").replace(".", ",").trim();
    valorUnitario = Number(valorUnitario.replace(/[^0-9\.]+/g, ""));

    if (valorDesconto > valorUnitario) {
        //ShowMessage('<b>Valor do Desconto não pode ser maior que </br>o Valor do Item!</b>', false);
        //$('#itemNotaFiscal').attr('style', 'display:block');
        //$('#itemNotaFiscal').modal('show');
    }

    var valorTotal = valorUnitario - valorDesconto;

    $('#InvoiceItem_TotalPrice').val(valorTotal);
}

function InicializarMascaraItemNotaFiscal()
{
    $('#Invoice_Valor').maskMoney({
        prefix: 'R$ ',
        thousands: '.',
        decimal: ',',
        precision: 2,
        affixesStay: true
    });

    $('#InvoiceItem_UnitPrice').maskMoney({
        prefix: 'R$ ',
        thousands: '.',
        decimal: ',',
        precision: 2,
        affixesStay: true
    });

    $('#InvoiceItem_ItemDiscount').maskMoney({
        prefix: 'R$ ',
        thousands: '.',
        decimal: ',',
        precision: 2,
        affixesStay: true
    });

    $('#InvoiceItem_TotalPrice').maskMoney({
        prefix: 'R$ ',
        thousands: '.',
        decimal: ',',
        precision: 2,
        affixesStay: true
    });

    $('#InvoiceItem_QuantityOrder').ForceNumericOnly();

    $('#Invoice_DataEmissao').mask('99/99/9999');
}

function InicializarCamposReadOnly()
{
    $('#Invoice_Valor').attr('readonly', true);
    $('#InvoiceItem_TotalPrice').attr('readonly', true);
    $('#InvoiceItem_UnitPrice').attr('readonly', true);
    
}

function FinalizarInclusaoNotaFiscal(notaFiscal) {
    ShowMessage(notaFiscal, true);
}

function ObterValorUnitarioProduto(idProduto) {

    ObterProduto(idProduto, function (produto) {
        $('#InvoiceItem_UnitPrice').val(produto.UnitPrice.toFixed(2));
    });
}

function ObterProduto(idProduto, produto) {

    $.post("/Invoice/GetProducts",
        { ProductID: idProduto },
        function (result) {
            produto(result);
        });
}

function IniciarInclusaoNotaFiscal() {
    //var valorNf = $('#Invoice_Valor').val();
    //valorNf = valorNf.replace(".", "").replace("R$", "").trim();
    //alert(valorNf);
    //$('#Invoice_Valor').val(valorNf);
}

function AdicionarItemNotaFiscal() {

    //ValidarDescontoItemNotaFiscal($('#InvoiceItem_ItemDiscount').val());

    var rowEmpty = $('#gdvItensNotaFiscal > tbody > tr').find('.dataTables_empty').length == 1;
    var rowCount = $('#gdvItensNotaFiscal > tbody > tr').length;

    if (rowEmpty)
        rowCount = rowCount - 1;

    if (giCount > rowCount)
        giCount = giCount;
    else
        giCount = rowCount + 1;


    var descricaoProduto = $('#ddlProduto option:selected').text();
    var quantidade = $('#InvoiceItem_QuantityOrder').val();
    var precoUnitario = $('#InvoiceItem_UnitPrice').val();
    var quantidadeEntregue = $('#InvoiceItem_QuantityDeliver').val();
    var desconto = $('#InvoiceItem_ItemDiscount').val();
    var precoTotal = $('#InvoiceItem_TotalPrice').val();

    $('#gdvItensNotaFiscal').dataTable().fnAddData([
        giCount,
        descricaoProduto,
        quantidade,
        precoUnitario,
        desconto,
        precoTotal
    ]);

    giCount++;

    var valorTotalNf = $('#Invoice_Valor').val();
    valorTotalNf = valorTotalNf.replace("R$", "").replace(",", "").replace(".", ",").trim();
    precoTotal = precoTotal.replace("R$", "").replace(",", "").replace(".", ",").trim();

    valorTotalNf = Number(valorTotalNf.replace(/[^0-9\.]+/g, ""));
    precoTotal = Number(precoTotal.replace(/[^0-9\.]+/g, ""));

    valorTotalNf += precoTotal;

    $('#Invoice_Valor').val(valorTotalNf);

    $('#gdvItensNotaFiscal_filter').hide();
    //$('#gdvItensNotaFiscal_info').hide();
    $('#gdvItensNotaFiscal_length').hide();

}