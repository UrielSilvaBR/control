var giCount = 1;
$(document).ready(function () {

    InicializarModalItemNotaFiscal();

    $('#btnAdicionarItemNf').click(function () {
        AdicionarItemNotaFiscal();
        $('#itemNotaFiscal').modal('hide');
        setTimeout(function () {
            LimparItemNotaFiscal();
        }, 1000);
    })

    $('#ddlProduto').on('change', function (produto) {
        ObterValorUnitarioProduto(produto.val);
    });

    $('#InvoiceItem_QuantityOrder').focusout(function () {
        var quantidade = $(this).val();
        CalcularItemNotaFiscal(quantidade);
    });

    $('#InvoiceItem_ItemDiscount').focusout(function () {
        ValidarDescontoItemNotaFiscal($(this).val());
    });
});

function LimparItemNotaFiscal() {
    $("#ddlProduto").select2().select2("val", '0');
    $('#InvoiceItem_QuantityOrder').val(1);
    $('#InvoiceItem_UnitPrice').val(0);
    $('#InvoiceItem_ItemDiscount').val(0);
    $('#InvoiceItem_QuantityDeliver').val(0);
    $('#InvoiceItem_TotalPrice').val(0);
}

function InicializarModalItemNotaFiscal() {
    IniciarlizarCamposItemNotaFiscal();
    InicializarMascaraItemNotaFiscal();
    InicializarCamposReadOnly();
}

function IniciarlizarCamposItemNotaFiscal() {

    var valorNotaFiscal = $('#Invoice_Valor').val();
    valorNotaFiscal = valorNotaFiscal.replace("R$", "").replace(",", "").replace(".", ",").trim();
    valorNotaFiscal = Number(valorNotaFiscal.replace(/[^0-9\.]+/g, ""));

    if (valorNotaFiscal <= 0)
        $('#Invoice_Valor').val(0);
    else
        $('#Invoice_Valor').val(valorNotaFiscal);

    $('#InvoiceItem_QuantityOrder').ForceNumericOnly();

    if (parseInt($('#InvoiceItem_QuantityOrder').val()) == 0)
        $('#InvoiceItem_QuantityOrder').val(1);
}

function CalcularItemNotaFiscal(quantidade) {
    var valorUnitario = $('#InvoiceItem_UnitPrice').val();
    valorUnitario = valorUnitario.replace("R$", "").replace(",", "").replace(".", ",").trim();
    valorUnitario = Number(valorUnitario.replace(/[^0-9\.]+/g, ""));

    var valorTotal = quantidade * valorUnitario;

    //var valorDesconto = $('#InvoiceItem_ItemDiscount').val();
    //valorDesconto = valorDesconto.replace("R$", "").replace(",", "").replace(".", ",").trim();
    //valorDesconto = Number(valorDesconto.replace(/[^0-9\.]+/g, ""));
    //valorTotal = valorTotal - valorDesconto;

    $('#InvoiceItem_TotalPrice').val(valorTotal);
}

function ValidarDescontoItemNotaFiscal(pValorDesconto) {
    var valorDesconto = pValorDesconto;
    valorDesconto = valorDesconto.replace("R$", "").replace(",", "").replace(".", ",").trim();
    valorDesconto = Number(valorDesconto.replace(/[^0-9\.]+/g, ""));

    var valorUnitario = $('#InvoiceItem_TotalPrice').val();
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

function InicializarMascaraItemNotaFiscal() {
    $('#Invoice_Valor').maskMoney({
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

    $('#InvoiceItem_UnitPrice').maskMoney({
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

    $('#InvoiceItem_ItemDiscount').maskMoney({
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

    $('#InvoiceItem_TotalPrice').maskMoney({
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

    $('#InvoiceItem_QuantityOrder').ForceNumericOnly();

    $('#Invoice_DataEmissao').mask('99/99/9999');
}

function InicializarCamposReadOnly() {
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
        $('#InvoiceItem_TotalPrice').val(produto.UnitPrice.toFixed(2));
        CalcularItemNotaFiscal($('#InvoiceItem_QuantityOrder').val());
    });
}

function ObterProduto(idProduto, produto) {

    if (idProduto == 0) {
        $('#InvoiceItem_UnitPrice').val(0);
        $('#InvoiceItem_TotalPrice').val(0);
        return;
    }

    $.post("/Invoice/GetProducts",
        { ProductID: idProduto },
        function (result) {
            produto(result);
        });
}

function IniciarInclusaoNotaFiscal() {

    var gdvItens = $('#frmCreateInvoice').find('#gdvItensNotaFiscal tbody');

    var Items = new Array();
    var index = 0;

    gdvItens.find('tr').each(function (i, el) {
        var $tds = $(this).find('td'),
            Id = $tds.find('input').val() == undefined ? 0 : $tds.find('input').val(),
            index = Id > 0 ? 1 : 0,
            SequencialItem = $tds[index].innerHTML,
            QuantityOrder = $tds[2].innerHTML

        Items.push({ Id: Id, SequencialItem: SequencialItem, QuantityOrder: QuantityOrder });
    });

    var itensNotaFiscal = JSON.stringify(Items);

    $('#itensNotaFiscal').val(itensNotaFiscal);
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
    quantidade = parseFloat(quantidade).toFixed(2);
    quantidade = quantidade.replace(".", ",");
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