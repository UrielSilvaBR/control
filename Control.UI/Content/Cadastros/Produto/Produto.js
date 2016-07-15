﻿$(document).ready(function () {

    //CarregarMascaras();

    $('#txt_Lista').change(CalculaPreco);
    $('#txt_IPI').change(CalculaPreco);
    $('#txt_Markup').change(CalculaPreco);

    $("#tabs").tabs();

    $('#btnAddFornecedorProduto').click(function () {
        AdicionarFornecedorProduto();
    });

});

function CalculaPreco() {
    var strlista = $('#txt_Lista').val();
    var strIPI = $('#txt_IPI').val();
    var strMarkup = $('#txt_Markup').val();
    var strListDiscount = $('#txt_ListDiscount').val();

    if (strlista == '' ||  strMarkup == '' || strlista == '0,00' || strMarkup == '0,00') {

        return false;
    }
    var lista = parseFloat(strlista);

    var IPI = parseFloat(strIPI);
    var Markup = parseFloat(strMarkup);
    var discount = parseFloat(strListDiscount);

    if (IPI == 0) {
        alert("IPI Zerado");
    }
    else
    { 
        IPI = IPI / 100;
    }

    if (discount == 0) {
        alert("discount Zerado");
        discount = 1;
    }
    else {
        discount = discount / 100;
        alert(discount)
    } 

    var Custo = lista * (1 + IPI);
    Custo = Custo * discount;

    
    Custo = Custo.toFixed(2);
    Markup = Custo * Markup;
    Markup = Markup.toFixed(2);
    $('#txt_Custo').val(Custo);
    $('#txt_Unitario').val(Markup);
}

function CarregarMascaras() {
    $('#txt_Lista').maskMoney({
        prefix: '',
        allowZero: false,
        allowNegative: false,
        defaultZero: true,
        thousands: '.',
        decimal: ',',
        //precision: 2,
        affixesStay: false,
        symbolPosition: 'left'
    });

    $('#txt_Custo').maskMoney({
        prefix: '',
        allowZero: false,
        allowNegative: false,
        defaultZero: true,
        thousands: '.',
        decimal: ',',
        //precision: 2,
        affixesStay: false,
        symbolPosition: 'left'
    });

    $('#txt_Unitario').maskMoney({
        prefix: '',
        allowZero: false,
        allowNegative: false,
        defaultZero: true,
        thousands: '.',
        decimal: ',',
        //precision: 2,
        affixesStay: false,
        symbolPosition: 'left'
    });

    $('#txt_IPI').maskMoney({
        prefix: '',
        allowZero: true,
        allowNegative: false,
        defaultZero: true,
        thousands: '.',
        //decimal: ',',
        affixesStay: false,
        symbolPosition: 'left'
    });

    $('#txt_Markup').maskMoney({
        prefix: '',
        allowZero: false,
        allowNegative: false,
        defaultZero: true,
        thousands: '.',
        decimal: ',',
        affixesStay: false,
        symbolPosition: 'left'
    });
}

function VerificarVinculoFornecedorProduto(idFornecedor, idProduto, callback) {
    $.post("/Cadastro/VerificarVinculoFornecedorProduto",
       { ProviderID: idFornecedor, ProductID: idProduto },
       function (result) {
           callback(result);
       });
}

function AdicionarFornecedorProduto() {

    var idFornecedor = $('#ProviderID option:selected').val();
    var idProduto = $('#Product_Id').val();
    var codigoProdutoFornecedor = $('#CodigoProdutoFornecedor').val();

    if (idFornecedor == 0)
    {
        ShowMessage('Selecione um Fornecedor para vincular <br>ao Produto!');
        return;
    }

    VerificarVinculoFornecedorProduto(idFornecedor, idProduto, function (callback) {

        if (callback == true) {
            ShowMessage('Fornecedor já está Vinculado ao Produto!<br>Caso queira alterar é necessário Editar o vínculo.', false);
            return;
        }
        else {
            $.post("/Cadastro/VincularFornecedorProduto",
            { ProviderID: idFornecedor, ProductID: idProduto, codigoProdutoFornecedor: codigoProdutoFornecedor },
                function (result) {

                    if (!result.erro) {

                        $.ajax({
                            url: '/Cadastro/GetProductProviders',
                            data: { ProductID: idProduto },
                            type: 'GET',
                            success: function (lista) {
                                $('#table_wrapperz_FornecedorProduto').html(lista);
                                $("#ProviderID").select2().select2("val", '0');
                                $('#CodigoProdutoFornecedor').val('0');
                            }
                        });
                    }
                    else
                        ShowMessage(result.msg, false);
                });
        }
    });
}

