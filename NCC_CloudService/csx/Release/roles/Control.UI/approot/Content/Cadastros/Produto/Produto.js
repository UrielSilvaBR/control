$(document).ready(function () {

    //CarregarMascaras();

    $('#txt_Lista').focusout(function () { CalculaPreco(); });
    $('#txt_IPI').focusout(function () { CalculaPreco(); });
    $('#txt_Markup').focusout(function () { CalculaPreco(); });
    $('#txt_ListDiscount').focusout(function () { CalculaPreco(); });

    $("#tabs").tabs();

    $('#btnAddFornecedorProduto').click(function () {
        AdicionarFornecedorProduto();
    });

    $('#btnAlterarFornecedorProduto').click(function () {



    });

});

function CalculaPreco() {
    var strlista = $('#txt_Lista').val();
    var strIPI = $('#txt_IPI').val();
    var strMarkup = $('#txt_Markup').val();
    var strListDiscount = $('#txt_ListDiscount').val();
    
    if (strlista == '' ||   strlista == '0,00' ) {

        return false;
    }
    var lista = parseFloat(strlista);

    var IPI = parseFloat(strIPI);
    
    var discount = parseFloat(strListDiscount);

    if (IPI == 0) {
        //alert("IPI Zerado");
    }
    else
    { 
        IPI = IPI / 100;
    }

    if (discount == 0) {
        //alert("discount Zerado");
        //discount = 1;
    }
    else {
        discount = discount / 100;
        //alert(discount)
    } 

    var Custo = lista * (1 + IPI);

    if(discount > 0)
        Custo = Custo - (Custo * discount);
    
    Custo = Custo.toFixed(2);

    if (Custo == 'NaN') {
        Custo = 0;
    }
    $('#txt_Custo').val(Custo.toString().replace(".", ","));

    if (strMarkup == '' || strMarkup == '0,00') {
        return false;
    }
    var Markup = parseFloat(strMarkup);

    Markup = Custo * Markup;
    Markup = Markup.toFixed(2);
    
    $('#txt_Unitario').val(Markup.toString().replace(".", ","));
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
    var modeloFabricante = $('#ModeloFabricante').val();


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
            { ProviderID: idFornecedor, ProductID: idProduto, codigoProdutoFornecedor: codigoProdutoFornecedor, modeloFabricante: modeloFabricante },
                function (result) {

                    if (!result.erro) {

                        $.ajax({
                            url: '/Cadastro/GetProductProviders',
                            data: { ProductID: idProduto },
                            type: 'GET',
                            success: function (lista) {
                                ShowSuccess('Vínculo realizado com <br> Sucesso!');
                                $('#table_wrapperz_FornecedorProduto').html(lista);
                                $("#ProviderID").select2().select2("val", '0');
                                $('#CodigoProdutoFornecedor').val('0');
                                $('#ModeloFabricante').val('');
                            }
                        });
                    }
                    else
                        ShowMessage(result.msg, false);
                });
        }
    });
}

function ExcluirFornecedorProduto(id)
{
    var idProduto = $('#Product_Id').val();

    if (idProduto == 0)
    {
        ShowMessage('Produto ainda não cadastrado!');
        return;
    }

    ShowConfirmation("Deseja realmente remover o Vínculo?", function () {

        $.post("/Cadastro/ExcluirVinculoFornecedorProduto",
        { ProductProviderID: id },
            function (result) {
                        
                if(!result.erro)
                {
                    ShowSuccess(result.msg);

                    $.ajax({
                        url: '/Cadastro/GetProductProviders',
                        data: { ProductID: idProduto },
                        type: 'GET',
                        success: function (lista) {
                            $('#table_wrapperz_FornecedorProduto').html(lista);
                        }
                    });
                }
                else
                {
                    ShowMessage(result.msg);
                }

        });
    });
}

function SalvarProduto()
{
    var ncm = $('#Product_NCMCode').val();

    if (ncm == '' || ncm == "0")
    {
        ShowMessage('NCM é obrigatório! <br>Favor informar o NCM do Produto.');
        return false;
    }
    else
    {
        $('#frmCadastroProduto').submit();
    }
}

function AbrirVinculoFornecedorProduto(indiceLinha)
{
    var gdvItens = $('#gdvFornecedorProduto').dataTable();

    var rowIndex = indiceLinha - 1;

    var items = gdvItens.fnGetData();
    item = items[rowIndex];

    $('#Provider_CompanyName').val(item[3]);
    $('#Code').val(item[5]);
    $('#ModelProvider').val(item[6]);
    $('#IsActive').prop('checked', item[9] == "1" ? true : false);
}