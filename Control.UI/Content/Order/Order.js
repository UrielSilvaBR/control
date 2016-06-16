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

    $('#btnEditarItemPedido').click(function () {
        EditarItemPedido();
    });

    $('#btnEditarItemPedido').attr('style', 'display:none');

    $('#btnAddItemPedido').click(function () {
        $('#tituloItemPedido').text('Adicionar Item');
        $('#btnAdicionarItemPedido').attr('style', 'display:block');
        $('#btnEditarItemPedido').attr('style', 'display:none');
    })

    $('#ddlProdutoPedido').on('change', function (produto) {
        ObterValorUnitarioProduto(produto.val);
    });

    $('#OrderProduct_QuantityOrder').focusout(function () {
        var quantidade = $(this).val();
        CalcularItemPedido(quantidade);
    });

    if (parseFloat($('#Order_TotalValue').val()) > 0)
        $('#Order_TotalValue').val($('#Order_TotalValue').val());

    $('#Order_Discount').focusout(function () {
        AplicarDesconto($(this).val());
    });

    $('#Order_CustomerID').change(function () {
        ObterDefinicaoClienteParaProposta($(this).val());
    });

    $('#btnGerarNF').hide();

    var idPedido = $('#Order_Id').val();
    if (idPedido == 0) {
        $('#btnCadastrar').show();
        $('#btnEditar').hide();
    }
    else {
        $('#btnCadastrar').hide();
        $('#btnEditar').show();
    }

    $('#itemPedido').on('hidden.bs.modal', function () {
        LimparItemPedido();
    })
});

function ObterListaContatoPorCliente(idCliente, defaultValue) {

    if (typeof defaultValue === 'undefined') { defaultValue = 0; }

    $.post("/Pedido/GetContactsByCustomer",
      { CustomerID: idCliente },
      function (result) {

          var listContact = $("#Order_ContactID");
          listContact.empty();
          //listContact.append(new Option('SELECIONE...', '0'));
          $.each(result.contactList, function (index, item) {
              listContact.append(new Option(item.ContatName, item.Id));
          });

          if (defaultValue > 0)
              listContact.select2().select2('val', defaultValue);
          else
              listContact.select2().select2('val', listContact.val());
      });
}

function ObterListaVendedorPorCliente(idCliente, defaultValue) {

    if (typeof defaultValue === 'undefined') { defaultValue = 0; }

    $.post("/Pedido/GetVendorsByCustomer",
      { CustomerID: idCliente },
      function (result) {

          var listVendor = $("#Order_VendorID");
          listVendor.empty();
          //listVendor.append(new Option('SELECIONE...', '0'));
          $.each(result.vendorList, function (index, item) {
              listVendor.append(new Option(item.Name, item.Id));
          });

          if (defaultValue > 0)
              listVendor.select2().select2('val', defaultValue);
          else
              listVendor.select2().select2('val', listVendor.val());
      });
}

function ObterListaVendedorPorContato(idContato) {
    $.post("/Pedido/GetVendorsByContact",
      { ContactID: idContato },
      function (result) {

          var listVendor = $("#Order_VendorID");
          listVendor.empty();
          //listVendor.append(new Option('SELECIONE...', '0'));
          $.each(result.vendorList, function (index, item) {
              listVendor.append(new Option(item.Name, item.Id));
          });

          listVendor.select2().select2('val', listVendor.val());
      });
}

function LimparItemPedido() {
    $("#ddlProdutoPedido").select2().select2("val", '0');
    $('#OrderProduct_QuantityOrder').val(1);
    $('#OrderProduct_DeadlineItem').val(0);
    $('#OrderProduct_UnitPrice').val(0);
    //$('#OrderProduct_ItemDiscount').val(0);
    $('#OrderProduct_DeadlineItem').val(0);
    $('#OrderProduct_QuantityDeliver').val(0);
    $('#OrderProduct_TotalPrice').val(0);
}

function InicializarModalItemPedido() {
    IniciarlizarCamposItemPedido();
    InicializarMascaraItemPedido();
    InicializarCamposReadOnly();
}

function IniciarlizarCamposItemPedido() {

    //$('#OrderProduct_ItemDiscount').val(0);

    $('#OrderProduct_UnitPrice').val(0);
    $('#OrderProduct_TotalPrice').val(0);

    if (parseInt($('#OrderProduct_QuantityOrder').val()) == 0)
        $('#OrderProduct_QuantityOrder').val(1);

    $('#OrderProduct_QuantityOrder').ForceNumericOnly();

    if (parseInt($('#OrderProduct_DeadlineItem').val()) == 0)
        $('#OrderProduct_DeadlineItem').val(0);

    $('#OrderProduct_DeadlineItem').ForceNumericOnly();
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

    //$('#OrderProduct_ItemDiscount').maskMoney({
    //    prefix: '',
    //    allowZero: false,
    //    allowNegative: false,
    //    defaultZero: true,
    //    thousands: '.',
    //    decimal: ',',
    //    precision: 2,
    //    affixesStay: false,
    //    symbolPosition: 'left'
    //});

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

    $('#Order_DeadlineItem').maskMoney({
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
}

function InicializarCamposReadOnly() {

    var valorTotalPedido = $('#Order_TotalValue').val();
    if (valorTotalPedido == 0) {
        $('#Order_TotalValue').val(0);
        $('#Order_Discount').val(0);
    }

    $("#Order_TotalValue").prop('readonly', true);
    $('#OrderProduct_TotalPrice').attr('disabled', true);
    $('#OrderProduct_UnitPrice').attr('disabled', true);

}

function IniciarInclusaoPedido() {

    var idPedido = $('#Order_Id').val();

    if (idPedido > 0) {
        IniciarEdicaoPedido();
        return;
    }

    var gdvItens = $('#gdvItensPedido').dataTable();

    var Items = new Array();
    var arrayItens = gdvItens.fnGetData();

    var idStatusPedido = $('#Order_Status').val();
    if (idStatusPedido == "0") {
        ShowMessage('É necessário selecionar o Status!', false);
        waitingDialog.hide();
        return false;
    }

    var idCliente = $('#Order_CustomerID').val();
    if (idCliente == "0") {
        ShowMessage('É necessário selecionar o Cliente!', false);
        waitingDialog.hide();
        return false;
    }

    var idContato = $('#Order_ContactID').val();
    if (idContato == "0") {
        ShowMessage('É necessário selecionar o Contato!', false);
        waitingDialog.hide();
        return false;
    }

    var idVendedor = $('#Order_VendorID').val();
    if (idVendedor == "0") {
        ShowMessage('É necessário selecionar o Vendedor!', false);
        waitingDialog.hide();
        return false;
    }

    var idCondicaoPagamento = $('#Order_PaymentTermID').val();
    if (idCondicaoPagamento == "0") {
        ShowMessage('É necessário selecionar a Condição de Pagamento!', false);
        waitingDialog.hide();
        return false;
    }

    if (arrayItens.length == 0) {
        ShowMessage('É necessário adicionar Itens!', false);
        waitingDialog.hide();
        return false;
    }

    waitingDialog.show('Criando Proposta', { dialogSize: 'sm', progressType: 'success' });

    for (var i = 0; i < arrayItens.length ; i++) {

        Items.push({
            Id: arrayItens[i][0],
            IdPedido: arrayItens[i][1],
            ProductID: arrayItens[i][2],
            SequencialItem: arrayItens[i][5],
            QuantityOrder: arrayItens[i][9],
            UnitPrice: arrayItens[i][10],
            //ItemDiscount: arrayItens[i][9],
            DeadlineItem: arrayItens[i][11],
            TotalPrice: arrayItens[i][12]
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
            $('#Order_Id').val(idPedido);

        waitingDialog.hide();
        ShowMessage(mensagemRetorno, true);

        window.location = '/Invoice/Invoice?InvoiceID=' + idPedido;

        if (parseInt(idPedido) > 0) {
            $.ajax({
                url: '/Pedido/GetOrderProducts',
                data: { OrderID: idPedido },
                type: 'GET',
                success: function (result) {
                    $('#table_wrapperz_ItensPedido').html(result);
                }
            });
        }

        if (idPedido == 0) {
            $('#btnCadastrar').show();
            $('#btnEditar').hide();
            $('#btnGerarNF').hide();
        }
        else {
            $('#btnCadastrar').hide();
            $('#btnEditar').show();
            $('#btnGerarNF').show();
        }

    }, 1000);
}

function IniciarEdicaoPedido() {

    var gdvItens = $('#gdvItensPedido').dataTable();

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
            QuantityOrder: arrayItens[i][9],
            UnitPrice: arrayItens[i][10],
            //ItemDiscount: arrayItens[i][9],
            DeadlineItem: arrayItens[i][11],
            TotalPrice: arrayItens[i][12]
        });
    }

    var itensPedido = JSON.stringify(Items);

    $('#itensPedido').val(itensPedido);
}

function FinalizarEdicaoPedido(pedido) {

    setTimeout(function () {

        var retornoPedido = pedido.split(';');
        var mensagemRetorno = retornoPedido[0];
        var idPedido = retornoPedido[1];

        if (retornoPedido.length > 1)
            $('#Order_Id').val(idPedido);

        waitingDialog.hide();
        ShowMessage(mensagemRetorno, true);

        if (parseInt(idPedido) > 0) {

            $.ajax({
                url: '/Pedido/GetOrderProducts',
                data: { OrderID: idPedido },
                type: 'GET',
                success: function (result) {
                    $('#table_wrapperz_ItensPedido').html(result);
                }
            });
        }


        if (idPedido > 0) {
            $('#btnCadastrar').hide();
            $('#btnEditar').show();
        }

    }, 1000);
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

function AdicionarItemPedido() {

    //ValidarDescontoItemNotaFiscal($('#OrderProduct_ItemDiscount').val());

    var idProduto = $('#ddlProdutoPedido option:selected').val();

    if (idProduto == 0) {
        ShowMessage('Selecione o Produto para inclusão do Item!', false);
        return;
    }

    var prazoEntrega = $('#OrderProduct_DeadlineItem').val();
   
    if (prazoEntrega == 0) {
        ShowMessage('Informe o Prazo de Entrega para <br/>inclusão do Item!', false);
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


    var descricaoProduto = $('#ddlProdutoPedido option:selected').text().split('-')[0].trim();
    var modeloProduto = $('#ddlProdutoPedido option:selected').text().split('-')[1].trim();
    var NCM = $('#ddlProdutoPedido option:selected').text().split('-')[2].trim();
    var quantidade = $('#OrderProduct_QuantityOrder').val();
    quantidade = parseFloat(quantidade).toFixed(2);
    quantidade = quantidade.replace(".", ",");
    var precoUnitario = $('#OrderProduct_UnitPrice').val();
    var quantidadeEntregue = $('#OrderProduct_QuantityDeliver').val();
    
    //var desconto = $('#OrderProduct_ItemDiscount').val();
    var precoTotal = $('#OrderProduct_TotalPrice').val();

    var gdvItens = $('#gdvItensPedido').dataTable();
    $(gdvItens).removeClass('table-striped');

    var indiceLinhaAdicionada = gdvItens.fnAddData([
        "0",
        "0",
        idProduto,
        '<a href="javascript:void(0);" onclick="AbrirItemPedido(' + giCount + ');" data-toggle="modal" data-target="#itemPedidoEditar" ><i class="fa fa-pencil-square-o"  style="padding: 0px 8px;"></i></a>',
        '<a href="javascript:void(0);" onclick="ExcluirItemPedido(' + giCount + ');"><i class="fa fa-times"  style="padding: 0px 8px;"></i></a>',
        giCount,
        descricaoProduto,
        modeloProduto,
        NCM,
        quantidade,
        precoUnitario,
        prazoEntrega,
        //desconto,
        precoTotal
    ]);

    var tr = gdvItens.fnGetNodes(indiceLinhaAdicionada);
    $(tr).effect("highlight", { color: "#9fdf9f" }, 2000);

    giCount++;

    AtualizarValorTotalPedido();

    $('#gdvItensPedido_filter').hide();
    //$('#gdvItensPedido_info').hide();
    $('#gdvItensPedido_length').hide();
}

function AbrirItemPedido(indiceLinha) {

    var gdvItens = $('#gdvItensPedido').dataTable();

    var rowIndex = indiceLinha - 1;

    var items = gdvItens.fnGetData();
    item = items[rowIndex];

    $('#tituloItemPedido').text('Alterar Item');
    $('#btnEditarItemPedido').attr('style', 'display:block');
    $('#btnAdicionarItemPedido').attr('style', 'display:none');

    $("#ddlProdutoPedido").select2().select2("val", item[2]);

    var quantidade = parseInt(item[9]);
    $('#OrderProduct_QuantityOrder').val(quantidade);

    $('#OrderProduct_UnitPrice').val(item[10]);
    //$('#OrderProduct_ItemDiscount').val(item[9]);
    $('#OrderProduct_DeadlineItem').val(item[11]);
    $('#OrderProduct_TotalPrice').val(item[12]);

    $('#rowIndexItemPedido').val(rowIndex);

    $("#itemPedido").modal('show');

    //gdvItens.fnUpdate($('#ddlProdutoPedido option:selected').text(), parseInt(rowIndex), 4);
}

function EditarItemPedido() {

    var gdvItens = $('#gdvItensPedido').dataTable();

    var rowIndex = $('#rowIndexItemPedido').val();

    gdvItens.fnUpdate($('#ddlProdutoPedido option:selected').val(), parseInt(rowIndex), 2);
    gdvItens.fnUpdate($('#ddlProdutoPedido option:selected').text().split('-')[0].trim(), parseInt(rowIndex), 6);
    gdvItens.fnUpdate($('#ddlProdutoPedido option:selected').text().split('-')[1].trim(), parseInt(rowIndex), 7);
    gdvItens.fnUpdate($('#ddlProdutoPedido option:selected').text().split('-')[2].trim(), parseInt(rowIndex), 8);

    var quantidade = parseFloat($('#OrderProduct_QuantityOrder').val()).toFixed(2);
    quantidade = quantidade.replace(".", ",");
    gdvItens.fnUpdate(quantidade, parseInt(rowIndex), 9);
    gdvItens.fnUpdate($('#OrderProduct_UnitPrice').val(), parseInt(rowIndex), 10);
    //gdvItens.fnUpdate($('#OrderProduct_ItemDiscount').val(), parseInt(rowIndex), 9);
    gdvItens.fnUpdate($('#OrderProduct_DeadlineItem').val(), parseInt(rowIndex), 11);
    gdvItens.fnUpdate($('#OrderProduct_TotalPrice').val(), parseInt(rowIndex), 12);

    AtualizarValorTotalPedido();
    gdvItens.fnDraw();

    LimparItemPedido();

    $(gdvItens).removeClass('table-striped');

    var tr = gdvItens.fnGetNodes(rowIndex);
    $(tr).effect("highlight", { color: "#80c1ff" }, 2000);

    $("#itemPedido").modal('hide');

}

function ExcluirItemPedido(indiceLinha) {

    ShowConfirmation("Deseja remover este Item?", function () {

        var gdvItens = $('#gdvItensPedido').dataTable();

        var rowIndex = indiceLinha - 1;

        var arrayItens = gdvItens.fnGetData();
        var idItem = arrayItens[rowIndex][0];

        $(gdvItens).removeClass('table-striped');

        var tr = gdvItens.fnGetNodes(rowIndex);
        $(tr).effect("highlight", { color: "#ff3333" }, 2000);

        setTimeout(function () {

            if (idItem > 0) {
                $.post('/Pedido/DeleteOrderProduct', { OrderProductID: idItem }, function (data) {
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
    var gdvItens = $('#gdvItensPedido').dataTable();
    var arrayItens = gdvItens.fnGetData();

    for (var i = 0; i < arrayItens.length; i++) {

        gdvItens.fnUpdate('<a href="javascript:void(0);" onclick="AbrirItemPedido(' + (i + 1) + ');" data-toggle="modal" data-target="#itemPedidoEditar" ><i class="fa fa-pencil-square-o"  style="padding: 0px 8px;"></i></a>', i, 3);
        gdvItens.fnUpdate('<a href="javascript:void(0);" onclick="ExcluirItemPedido(' + (i + 1) + ');"><i class="fa fa-times"  style="padding: 0px 8px;"></i></a>', i, 4);
        gdvItens.fnUpdate(i + 1, i, 5);
        //giCount++;
    }
}

function AtualizarValorTotalPedido(aplicarDesconto) {

    if (typeof aplicarDesconto === 'undefined') { aplicarDesconto = true; }

    var gdvItens = $('#gdvItensPedido').dataTable();

    var arrayItens = gdvItens.fnGetData();

    var valorTotalPedido = 0;
    var descontoTotalPedido = 0;

    for (var i = 0; i < arrayItens.length; i++) {

        var valorPedido = arrayItens[i][12];
        valorPedido = valorPedido.replace("R$", "").replace(",", "").replace(".", ",").trim();
        valorPedido = Number(valorPedido.replace(/[^0-9\.]+/g, ""));

        //var descontoPedido = arrayItens[i][9];
        //descontoPedido = descontoPedido.replace("R$", "").replace(",", "").replace(".", ",").trim();
        //descontoPedido = Number(descontoPedido.replace(/[^0-9\.]+/g, ""));

        valorTotalPedido += valorPedido;
        //descontoTotalPedido += descontoPedido;
    }

    if (aplicarDesconto) {

        var percentualDesconto = $('#Order_Discount').val();
        percentualDesconto = percentualDesconto.replace("R$", "").replace(".", "").replace(",", ".").trim();
        percentualDesconto = parseFloat(percentualDesconto).toFixed(2) / 100;

        var valorDesconto = (parseFloat(valorTotalPedido).toFixed(2) / 100) * percentualDesconto;

        valorTotalPedido = parseFloat(valorTotalPedido) / 100;
        valorTotalPedido = valorTotalPedido - valorDesconto;
        valorTotalPedido = parseFloat(valorTotalPedido).toFixed(2);
        valorTotalPedido = valorTotalPedido.toString().replace(".", "");
    }

    $('#Order_TotalValue').val(valorTotalPedido);
    //$('#Order_Discount').val(descontoTotalPedido);
}

function IncluirNotaFiscal(idPedido) {

    waitingDialog.show('Criando Nota Fiscal', { dialogSize: 'sm', progressType: 'success' });

    setTimeout(function () {
        $.ajax({
            url: '/Pedido/GerarNotaFiscal',
            data: { OrderID: idPedido },
            type: 'POST',
            success: function (result) {
                waitingDialog.hide();
                ShowSuccess(result);
            }
        });
    }, 1000);
}

function AplicarDesconto(percentualDesconto) {

    AtualizarValorTotalPedido(false);

    var valorTotal = $('#Order_TotalValue').val();
    valorTotal = valorTotal.replace("R$", "").replace(",", "").replace(".", ",").trim();
    valorTotal = parseFloat(valorTotal) / 100;

    percentualDesconto = percentualDesconto.replace("R$", "").replace(".", "").replace(",", ".").trim();

    var valorDesconto = parseFloat(valorTotal).toFixed(2) * (parseFloat(percentualDesconto).toFixed(2) / 100);

    valorTotal = valorTotal - valorDesconto;
    valorTotal = parseFloat(valorTotal).toFixed(2);
    valorTotal = valorTotal.toString().replace(".", "");

    $('#Order_TotalValue').val(valorTotal);
}

function AbrirModalCadastroCliente()
{
    $('#modal-cadastro-cliente').modal('show');
}

function AbrirModalCadastroVendedor() {
    $('#modal-cadastro-vendedor').modal('show');
}

function AbrirModalCadastroContato() {
    $('#modal-cadastro-contato').modal('show');
}

function FinalizarInclusaoVendedor(id) {

    $('#modal-cadastro-vendedor').modal('hide');

    ObterListaVendedorPorCliente($('#Order_CustomerID').val(), id);
}

function ValidarInclusaoVendedorModal()
{
    var idCliente = $('#Order_CustomerID option:selected').val();

    if(idCliente == 0)
    {
        ShowMessage('Selecione o Cliente para cadastrar o Vendedor!', false);
        return false;
    }
}

function FinalizarInclusaoContato(id) {

    $('#modal-cadastro-contato').modal('hide');

    ObterListaContatoPorCliente($('#Order_CustomerID').val(), id);
}

function ValidarInclusaoContatoModal() {
    var idCliente = $('#Order_CustomerID option:selected').val();

    if (idCliente == 0) {
        ShowMessage('Selecione o Cliente para cadastrar o Contato!', false);
        return false;
    }
}

// Metodo responsavel por Carregar todas as Definicoes de Cliente para criacao da Proposta
// Definicoes: Contato, Vendedor, Condicao de Pagamento e Descontos previamente cadastrados.
function ObterDefinicaoClienteParaProposta(idCliente)
{
    ObterListaVendedorPorCliente(idCliente);

    ObterListaContatoPorCliente(idCliente);
}