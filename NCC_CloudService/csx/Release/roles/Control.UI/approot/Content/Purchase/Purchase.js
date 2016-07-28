       var giCount = 1;
$(document).ready(function () {

    InicializarModalItemPedido();

    $('#btnAdicionarItemPedidoDeCompra').click(function () {
        AdicionarItemPedido();
        $('#itemPedidoDeCompra').modal('hide');
        setTimeout(function () {
            LimparItemPedido();
        }, 800);
    })

    $('#btnEditarItemPedidoDeCompra').click(function () {
        EditarItemPedido();
    });

    $('#btnEditarItemPedidoDeCompra').attr('style', 'display:none;float:right');

    $('#btnAddItemPedido').click(function () {
        $('#tituloItemPedido').text('Adicionar Item');
        $('#btnAdicionarItemPedidoDeCompra').attr('style', 'display:block;float:right');
        $('#btnEditarItemPedidoDeCompra').attr('style', 'display:none;float:right');
    })

    $('#ddlProdutoPedidoDeCompra').on('change', function (produto) {
        ObterValorUnitarioProduto(produto.val);
    });

    $('#PurchaseOrderItem_QuantityOrder').focusout(function () {
        CalcularItemPedido();
    });

    $('#PurchaseOrderItem_ItemDiscount').focusout(function () {
        CalcularItemPedido();
    });



    $('#itemPedidoDeCompra').on('hidden.bs.modal', function () {
        LimparItemPedido();
    })

});

function LimparItemPedido() {
    $("#ddlProdutoPedidoDeCompra").select2().select2("val", '0');
    $('#PurchaseOrderItem_QuantityOrder').val(1);
    $('#PurchaseOrderItem_UnitPrice').val(0);
    $('#PurchaseOrderItem_ItemDiscount').val(0);
    $('#PurchaseOrderItem_QuantityDeliver').val(0);
    $('#PurchaseOrderItem_TotalPrice').val(0);
}

function InicializarModalItemPedido() {
    IniciarlizarCamposItemPedido();
    InicializarMascaraItemPedido();
    InicializarCamposReadOnly();
}

function IniciarlizarCamposItemPedido() {

    $('#PurchaseOrderItem_ItemDiscount').val(0);
    $('#PurchaseOrderItem_UnitPrice').val(0);
    $('#PurchaseOrderItem_TotalPrice').val(0);

    $('#PurchaseOrderItem_QuantityOrder').val(1);
    $('#PurchaseOrderItem_QuantityOrder').ForceNumericOnly();

    if (parseInt($('#PurchaseOrderItem_QuantityOrder').val()) == 0)
        $('#PurchaseOrderItem_QuantityOrder').val(1);
}

function CalcularItemPedido() {

    var quantidade = $('#PurchaseOrderItem_QuantityOrder').val();


    var valorUnitario = $('#PurchaseOrderItem_UnitPrice').val();
    valorUnitario = valorUnitario.replace("R$", "").replace(",", "").replace(".", ",").trim();
    valorUnitario = Number(valorUnitario.replace(/[^0-9\.]+/g, ""));

    var valorTotal = quantidade * valorUnitario;

    var valorDesconto = $('#PurchaseOrderItem_ItemDiscount').val();
    valorDesconto = valorDesconto.replace("R$", "").replace(",", "").replace(".", ",").trim();
    valorDesconto = Number(valorDesconto.replace(/[^0-9\.]+/g, ""));

    valorTotal -= valorDesconto;

    $('#PurchaseOrderItem_TotalPrice').val(valorTotal);
}

function InicializarMascaraItemPedido() {

    $('#PurchaseOrderItem_UnitPrice').maskMoney({
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

    $('#PurchaseOrderItem_ItemDiscount').maskMoney({
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

    $('#PurchaseOrderItem_TotalPrice').maskMoney({
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

    $('#PurchaseOrder_TotalValue').maskMoney({
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

    $('#PurchaseOrder_Discount').maskMoney({
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

    $('#PurchaseOrderItem_QuantityOrder').ForceNumericOnly();

    $('#PurchaseOrder_ValidateDate').mask('99/99/9999');
}

function InicializarCamposReadOnly() {

    var valorTotalPedido = $('#PurchaseOrder_TotalValue').val();
    if (valorTotalPedido == 0)
        $('#PurchaseOrder_TotalValue').val(0);

    $("#PurchaseOrder_TotalValue").prop('readonly', true);
    $('#PurchaseOrderItem_TotalPrice').attr('disabled', true);
    $('#PurchaseOrderItem_UnitPrice').attr('disabled', true);

}

function IniciarInclusaoPedido() {

    var idPedido = $('#PurchaseOrder_Id').val();

    if (idPedido > 0) {
        IniciarEdicaoPedido();
        return;
    }

    var gdvItens = $('#gdvItensPedidoDeCompras').dataTable();

    var Items = new Array();
    var arrayItens = gdvItens.fnGetData();

    if (arrayItens.length == 0) {
        waitingDialog.hide
        ShowMessage('É necessário adicionar Itens!', false);
        return false;
    }

    waitingDialog.show('Salvando Ordem de Compra', { dialogSize: 'sm', progressType: 'success' });

    for (var i = 0; i < arrayItens.length ; i++) {

        Items.push({
            Id: arrayItens[i][0],
            IdPedido: arrayItens[i][1],
            ProductID: arrayItens[i][2],
            SequencialItem: arrayItens[i][5],
            QuantityOrder: arrayItens[i][7],
            UnitPrice: arrayItens[i][8],
            ItemDiscount: arrayItens[i][9],
            TotalPrice: arrayItens[i][10]
        });
    }

    var itensPedido = JSON.stringify(Items);

    $('#itensPedido').val(itensPedido);
}

function FinalizarInclusaoPedido(pedido) {

    setTimeout(function () {

        var retornoPedido = pedido.split(';');
        var mensagemRetorno = retornoPedido[0];
        var idPedido = retornoPedido[1];

        if (retornoPedido.length > 1)
            $('#PurchaseOrder_Id').val(idPedido);

        waitingDialog.hide();
        ShowMessage(mensagemRetorno, true);


    }, 1000);
}

function IniciarEdicaoPedido() {

    var gdvItens = $('#gdvItensPedidoDeCompras').dataTable();

    var Items = new Array();
    var arrayItens = gdvItens.fnGetData();

    if (arrayItens.length == 0) {
        waitingDialog.hide();
        ShowMessage('É necessário adicionar Itens!', false);
        return false;
    }

    waitingDialog.show('Alterando Proposta', { dialogSize: 'sm', progressType: 'success' });

    for (var i = 0; i < arrayItens.length ; i++) {

        Items.push({
            Id: arrayItens[i][0],
            IdPedido: arrayItens[i][1],
            ProductID: arrayItens[i][2],
            SequencialItem: arrayItens[i][5],
            QuantityOrder: arrayItens[i][7],
            UnitPrice: arrayItens[i][8],
            ItemDiscount: arrayItens[i][9],
            TotalPrice: arrayItens[i][10]
        });
    }

    var itensPedido = JSON.stringify(Items);

    $('#itensPedido').val(itensPedido);
}

function ObterValorUnitarioProduto(idProduto) {

    ObterProduto(idProduto, function (produto) {
        $('#PurchaseOrderItem_UnitPrice').val(produto.UnitPrice.toFixed(2));
        $('#PurchaseOrderItem_TotalPrice').val(produto.UnitPrice.toFixed(2));
        CalcularItemPedido();
    });
}

function ObterProduto(idProduto, produto) {

    if (idProduto == 0) {
        $('#PurchaseOrderItem_UnitPrice').val(0);
        $('#PurchaseOrderItem_TotalPrice').val(0);
        return;
    }

    $.post("/Compras/GetProducts",
        { ProductID: idProduto },
        function (result) {
            produto(result);
        });
}

function AdicionarItemPedido() {

    //ValidarDescontoItemNotaFiscal($('#OrderProduct_ItemDiscount').val());

    var idProduto = $('#ddlProdutoPedidoDeCompra option:selected').val();

    if (idProduto == 0) {
        ShowMessage('Selecione o Produto para inclusão do Item!', false);
        return;
    }

    var rowEmpty = $('#gdvItensPedidoDeCompras > tbody > tr').find('.dataTables_empty').length == 1;
    var rowCount = $('#gdvItensPedidoDeCompras > tbody > tr').length;

    if (rowEmpty)
        rowCount = rowCount - 1;

    if (giCount > rowCount)
        giCount = giCount;
    else
        giCount = rowCount + 1;


    var descricaoProduto = $('#ddlProdutoPedidoDeCompra option:selected').text();
    var quantidade = $('#PurchaseOrderItem_QuantityOrder').val();
    quantidade = parseFloat(quantidade).toFixed(2);
    quantidade = quantidade.replace(".", ",");
    var precoUnitario = $('#PurchaseOrderItem_UnitPrice').val();
    var quantidadeEntregue = $('#PurchaseOrderItem_QuantityDeliver').val();
    var desconto = $('#PurchaseOrderItem_ItemDiscount').val();
    var precoTotal = $('#PurchaseOrderItem_TotalPrice').val();

    var gdvItens = $('#gdvItensPedidoDeCompras').dataTable();
    $(gdvItens).removeClass('table-striped');

    var indiceLinhaAdicionada = gdvItens.fnAddData([
        "0",
        "0",
        idProduto,
        '<a href="javascript:void(0);" onclick="AbrirItemPedido(' + giCount + ');" data-toggle="modal" data-target="#itemPedidoDeCompra" ><i class="fa fa-pencil-square-o"  style="padding: 0px 8px;"></i></a>',
        '<a href="javascript:void(0);" onclick="ExcluirItemPedido(' + giCount + ');"><i class="fa fa-times"  style="padding: 0px 8px;"></i></a>',
        giCount,
        descricaoProduto,
        quantidade,
        precoUnitario,
        desconto,
        precoTotal
    ]);

    var tr = gdvItens.fnGetNodes(indiceLinhaAdicionada);
    $(tr).effect("highlight", { color: "#9fdf9f" }, 2000);

    giCount++;

    AtualizarValorTotalPedido();

    $('#gdvItensPedidoDeCompras_filter').hide();
    //$('#gdvItensPedido_info').hide();
    $('#gdvItensPedidoDeCompras_length').hide();
}

function AbrirItemPedido(indiceLinha) {

    var gdvItens = $('#gdvItensPedidoDeCompras').dataTable();

    var rowIndex = indiceLinha - 1;

    var items = gdvItens.fnGetData();
    item = items[rowIndex];

    $('#tituloItemPedido').text('Alterar Item');
    $('#btnEditarItemPedidoDeCompra').attr('style', 'display:block');
    $('#btnAdicionarItemPedidoDeCompra').attr('style', 'display:none');

    $("#ddlProdutoPedidoDeCompra").select2().select2("val", item[2]);

    var quantidade = parseInt(item[7]);
    $('#PurchaseOrderItem_QuantityOrder').val(quantidade);

    $('#PurchaseOrderItem_UnitPrice').val(item[8]);
    $('#PurchaseOrderItem_ItemDiscount').val(item[9]);
    $('#PurchaseOrderItem_TotalPrice').val(item[10]);

    $('#rowIndexItemPedido').val(rowIndex);

    $("#itemPedidoDeCompra").modal('show');
}

function EditarItemPedido() {

    var gdvItens = $('#gdvItensPedidoDeCompras').dataTable();

    var rowIndex = $('#rowIndexItemPedido').val();

    gdvItens.fnUpdate($('#ddlProdutoPedidoDeCompra option:selected').val(), parseInt(rowIndex), 2);
    gdvItens.fnUpdate($('#ddlProdutoPedidoDeCompra option:selected').text(), parseInt(rowIndex), 6);

    var quantidade = parseFloat($('#PurchaseOrderItem_QuantityOrder').val()).toFixed(2);
    quantidade = quantidade.replace(".", ",");
    gdvItens.fnUpdate(quantidade, parseInt(rowIndex), 7);
    gdvItens.fnUpdate($('#PurchaseOrderItem_UnitPrice').val(), parseInt(rowIndex), 8);
    gdvItens.fnUpdate($('#PurchaseOrderItem_ItemDiscount').val(), parseInt(rowIndex), 9);
    gdvItens.fnUpdate($('#PurchaseOrderItem_TotalPrice').val(), parseInt(rowIndex), 10);

    AtualizarValorTotalPedido();
    gdvItens.fnDraw();

    LimparItemPedido();

    $(gdvItens).removeClass('table-striped');

    var tr = gdvItens.fnGetNodes(rowIndex);
    $(tr).effect("highlight", { color: "#80c1ff" }, 2000);

    $("#itemPedidoDeCompra").modal('hide');

}

function ExcluirItemPedido(indiceLinha) {

    ShowConfirmation("Deseja remover este Item?", function () {

        var gdvItens = $('#gdvItensPedidoDeCompras').dataTable();

        var rowIndex = indiceLinha - 1;

        var arrayItens = gdvItens.fnGetData();
        var idItem = arrayItens[rowIndex][0];

        $(gdvItens).removeClass('table-striped');

        var tr = gdvItens.fnGetNodes(rowIndex);
        $(tr).effect("highlight", { color: "#ff3333" }, 2000);

        setTimeout(function () {

            if (idItem > 0) {
                $.post('/Compras/DeletePurchaseOrderItem', { OrderProductID: idItem }, function (data) {
                    gdvItens.fnDeleteRow(rowIndex);
                    AtualizarSequencialItemPedido();
                    AtualizarValorTotalPedido();
                    giCount--;
                });
            }
            else {
                gdvItens.fnDeleteRow(rowIndex);
                AtualizarSequencialItemPedido();
                AtualizarValorTotalPedido();
                giCount--;
            }

        }, 1000);

        $("#modal-confirmation").modal("hide");
    });
}

function AtualizarSequencialItemPedido() {
    var gdvItens = $('#gdvItensPedidoDeCompras').dataTable();
    var arrayItens = gdvItens.fnGetData();

    for (var i = 0; i < arrayItens.length; i++) {

        gdvItens.fnUpdate('<a href="javascript:void(0);" onclick="AbrirItemPedido(' + (i + 1) + ');" data-toggle="modal" data-target="#itemPedidoDeCompra" ><i class="fa fa-pencil-square-o"  style="padding: 0px 8px;"></i></a>', i, 3);
        gdvItens.fnUpdate('<a href="javascript:void(0);" onclick="ExcluirItemPedido(' + (i + 1) + ');"><i class="fa fa-times"  style="padding: 0px 8px;"></i></a>', i, 4);
        gdvItens.fnUpdate(i + 1, i, 5);
        //giCount++;
    }
}

function AtualizarValorTotalPedido() {
    var gdvItens = $('#gdvItensPedidoDeCompras').dataTable();

    var arrayItens = gdvItens.fnGetData();

    var valorTotalPedido = 0;
    var descontoTotalPedido = 0;

    for (var i = 0; i < arrayItens.length; i++) {

        var valorPedido = arrayItens[i][10];
        valorPedido = valorPedido.replace("R$", "").replace(",", "").replace(".", ",").trim();
        valorPedido = Number(valorPedido.replace(/[^0-9\.]+/g, ""));

        var descontoPedido = arrayItens[i][9];
        descontoPedido = descontoPedido.replace("R$", "").replace(",", "").replace(".", ",").trim();
        descontoPedido = Number(descontoPedido.replace(/[^0-9\.]+/g, ""));

        valorTotalPedido += valorPedido;
        descontoTotalPedido += descontoPedido;
    }

    $('#PurchaseOrder_TotalValue').val(valorTotalPedido);
    $('#PurchaseOrder_Discount').val(descontoTotalPedido);
}