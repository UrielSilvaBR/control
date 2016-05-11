var giCount = 1;
$(document).ready(function () {

    InicializarModalItemPedido();

    $('#btnAdicionarItemPedido').click(function () {
        AdicionarItemPedido();
        $('#itemPedido').modal('hide');
        setTimeout(function () {
            LimparItemPedido();
        }, 800);
    })

    $('#ddlProdutoPedido').on('change', function (produto) {
        ObterValorUnitarioProduto(produto.val);
    });

    $('#OrderProduct_QuantityOrder').focusout(function () {
        var quantidade = $(this).val();
        CalcularItemPedido(quantidade);
    });

    $('#OrderProduct_ItemDiscount').focusout(function () {
        ValidarDescontoItemPedido($(this).val());
    });
});

function LimparItemPedido() {
    $("#ddlProdutoPedido").select2().select2("val", '0');
    $('#OrderProduct_QuantityOrder').val(1);
    $('#OrderProduct_UnitPrice').val(0);
    $('#OrderProduct_ItemDiscount').val(0);
    $('#OrderProduct_QuantityDeliver').val(0);
    $('#OrderProduct_TotalPrice').val(0);
}

function InicializarModalItemPedido() {
    IniciarlizarCamposItemPedido();
    InicializarMascaraItemPedido();
    InicializarCamposReadOnly();
}

function IniciarlizarCamposItemPedido() {

    //var valorNotaFiscal = $('#Invoice_Valor').val();
    //valorNotaFiscal = valorNotaFiscal.replace("R$", "").replace(",", "").replace(".", ",").trim();
    //valorNotaFiscal = Number(valorNotaFiscal.replace(/[^0-9\.]+/g, ""));

    //if (valorNotaFiscal <= 0)
    //    $('#Invoice_Valor').val(0);
    //else
    //    $('#Invoice_Valor').val(valorNotaFiscal);

    $('#OrderProduct_ItemDiscount').val(0);

    $('#OrderProduct_QuantityOrder').val(1);
    $('#OrderProduct_QuantityOrder').ForceNumericOnly();

    if (parseInt($('#OrderProduct_QuantityOrder').val()) == 0)
        $('#OrderProduct_QuantityOrder').val(1);
}

function CalcularItemPedido(quantidade) {
    var valorUnitario = $('#OrderProduct_UnitPrice').val();
    valorUnitario = valorUnitario.replace("R$", "").replace(",", "").replace(".", ",").trim();
    valorUnitario = Number(valorUnitario.replace(/[^0-9\.]+/g, ""));

    var valorTotal = quantidade * valorUnitario;

    //var valorDesconto = $('#OrderProduct_ItemDiscount').val();
    //valorDesconto = valorDesconto.replace("R$", "").replace(",", "").replace(".", ",").trim();
    //valorDesconto = Number(valorDesconto.replace(/[^0-9\.]+/g, ""));
    //valorTotal = valorTotal - valorDesconto;

    $('#OrderProduct_TotalPrice').val(valorTotal);
}

function ValidarDescontoItemPedido(pValorDesconto) {
    var valorDesconto = pValorDesconto;
    valorDesconto = valorDesconto.replace("R$", "").replace(",", "").replace(".", ",").trim();
    valorDesconto = Number(valorDesconto.replace(/[^0-9\.]+/g, ""));

    var valorUnitario = $('#OrderProduct_TotalPrice').val();
    valorUnitario = valorUnitario.replace("R$", "").replace(",", "").replace(".", ",").trim();
    valorUnitario = Number(valorUnitario.replace(/[^0-9\.]+/g, ""));

    if (valorDesconto > valorUnitario) {
        //ShowMessage('<b>Valor do Desconto não pode ser maior que </br>o Valor do Item!</b>', false);
        //$('#itemNotaFiscal').attr('style', 'display:block');
        //$('#itemNotaFiscal').modal('show');
    }



    var valorTotal = valorUnitario - valorDesconto;

    $('#OrderProduct_TotalPrice').val(valorTotal);
}

function InicializarMascaraItemPedido() {
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

    $('#OrderProduct_UnitPrice').maskMoney({
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

    $('#OrderProduct_ItemDiscount').maskMoney({
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

    $('#OrderProduct_TotalPrice').maskMoney({
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

    $('#Order_TotalValue').maskMoney({
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

    $('#Order_Discount').maskMoney({
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

    $('#OrderProduct_QuantityOrder').ForceNumericOnly();

    $('#Order_OrderDate').mask('99/99/9999');
}

function InicializarCamposReadOnly() {
    //$('#Order_TotalValue').attr('readonly', true);
    $('#OrderProduct_TotalPrice').attr('readonly', true);
    $('#OrderProduct_UnitPrice').attr('readonly', true);

}

function FinalizarInclusaoPedido(pedido) {

    var idPedido = pedido.split(';');

    if (idPedido.length > 1)
        $('#Order_Id').val(idPedido[1]);

    ShowMessage(idPedido[0], true);
}

function ObterValorUnitarioProduto(idProduto) {

    ObterProduto(idProduto, function (produto) {
        $('#OrderProduct_UnitPrice').val(produto.UnitPrice.toFixed(2));
        $('#OrderProduct_TotalPrice').val(produto.UnitPrice.toFixed(2));
        CalcularItemPedido($('#OrderProduct_QuantityOrder').val());
    });
}

function ObterProduto(idProduto, produto) {

    if (idProduto == 0) {
        $('#OrderProduct_UnitPrice').val(0);
        $('#OrderProduct_TotalPrice').val(0);
        return;
    }

    $.post("/Invoice/GetProducts",
        { ProductID: idProduto },
        function (result) {
            produto(result);
        });
}

function IniciarInclusaoPedido() {

    var gdvItens = $('#gdvItensPedido').dataTable();

    var Items = new Array();
    var arrayItens = gdvItens.fnGetData();

    for (var i = 0; i < arrayItens.length ; i++) {

        Items.push({
            Id: arrayItens[i][0],
            ProductID: arrayItens[i][1],
            SequencialItem: arrayItens[i][2],
            QuantityOrder: arrayItens[i][4],
            UnitPrice: arrayItens[i][5],
            ItemDiscount: arrayItens[i][6],
            TotalPrice: arrayItens[i][7]
        });
    }

    var itensPedido = JSON.stringify(Items);

    $('#itensPedido').val(itensPedido);
}

function AdicionarItemPedido() {

    //ValidarDescontoItemNotaFiscal($('#OrderProduct_ItemDiscount').val());

    var idProduto = $('#ddlProdutoPedido option:selected').val();

    if (idProduto == 0) {
        ShowMessage('Selecione o Produto para inclusão do Item!', false);
        return;
    }

    var rowEmpty = $('#gdvItensPedido > tbody > tr').find('.dataTables_empty').length == 1;
    var rowCount = $('#gdvItensPedido > tbody > tr').length;

    if (rowEmpty)
        rowCount = rowCount - 1;

    if (giCount > rowCount)
        giCount = giCount;
    else
        giCount = rowCount + 1;


    var descricaoProduto = $('#ddlProdutoPedido option:selected').text();
    var quantidade = $('#OrderProduct_QuantityOrder').val();
    quantidade = parseFloat(quantidade).toFixed(2);
    quantidade = quantidade.replace(".", ",");
    var precoUnitario = $('#OrderProduct_UnitPrice').val();
    var quantidadeEntregue = $('#OrderProduct_QuantityDeliver').val();
    var desconto = $('#OrderProduct_ItemDiscount').val();
    var precoTotal = $('#OrderProduct_TotalPrice').val();

    $('#gdvItensPedido').dataTable().fnAddData([
        "0",
        idProduto,
        '<a href="javascript:void(0);" onclick="EditarItemPedido(' + giCount + ');" ><i class="fa fa-pencil-square-o"  style="padding: 0px 8px;"></i></a>',
        giCount,
        descricaoProduto,
        quantidade,
        precoUnitario,
        desconto,
        precoTotal
    ]);

    giCount++;

    var valorTotalPedido = $('#Order_TotalValue').val();
    valorTotalPedido = valorTotalPedido.replace("R$", "").replace(",", "").replace(".", ",").trim();
    precoTotal = precoTotal.replace("R$", "").replace(",", "").replace(".", ",").trim();

    valorTotalPedido = Number(valorTotalPedido.replace(/[^0-9\.]+/g, ""));
    precoTotal = Number(precoTotal.replace(/[^0-9\.]+/g, ""));

    valorTotalPedido += precoTotal;

    var descontoTotalPedido = $('#Order_Discount').val();
    descontoTotalPedido = descontoTotalPedido.replace("R$", "").replace(",", "").replace(".", ",").trim();
    
    descontoTotalPedido = Number(descontoTotalPedido.replace(/[^0-9\.]+/g, ""));
    desconto = Number(desconto.replace(/[^0-9\.]+/g, ""));

    descontoTotalPedido += desconto;

    $('#Order_TotalValue').val(valorTotalPedido);
    $('#Order_Discount').val(descontoTotalPedido);

    $('#gdvItensPedido_filter').hide();
    //$('#gdvItensPedido_info').hide();
    $('#gdvItensPedido_length').hide();

}

function EditarItemPedido(sequencialItem)
{
    var gdvItens = $('#gdvItensPedido').dataTable();

    var rowIndex = sequencialItem - 1;

    gdvItens.fnUpdate($('#ddlProdutoPedido option:selected').text(), parseInt(rowIndex), 4);

}