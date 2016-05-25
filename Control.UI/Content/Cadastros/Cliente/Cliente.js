$(document).ready(function () {

    InicializarMascarasCampos();

    if ($('#Customer_Id').val() == "0")
        AlterarTipoCliente(1);
    else
        AlterarTipoCliente($('#cboTipoCliente option:selected').val());

    $('#cboTipoCliente').change(function () {
        var idTipoCliente = $(this).val();
        AlterarTipoCliente(idTipoCliente);
    });

    $('#Customer_ZipCode').focusout(function () {

        var cep = $(this).val();
        cep = cep.replace("-", "");

        if (cep == "________")
            return;

        ObterEnderecoPorCEP(cep);
    });

    $('#Customer_CompanyName').focus();

    $('#Customer_AddressStateId').change(function () {
        ObterListaCidadePorEstado($(this).val());
    });
});

function InicializarMascarasCampos() {
    $('#Customer_ZipCode').mask('99999-999');
}

function AlterarTipoCliente(idTipoCliente) {
    if (idTipoCliente == 1) {
        $('#lblCnpjCpf').text('CPF');
        $('#Customer_Document').mask('999.999.999-99');
        $('#Customer_Document').focus();
    }
    else {
        $('#lblCnpjCpf').text('CNPJ');
        $('#Customer_Document').mask('99.999.999/9999-99');
        $('#Customer_Document').focus();
    }
}

function ObterEnderecoPorCEP(cep) {

    waitingDialog.show('Buscando Endereço', { dialogSize: 'sm', progressType: 'success' });
    $('#Customer_AddressStreet').val('');
    $('#Customer_AddressDistrict').val('');

    setTimeout(function () {

        $.ajax({
            url: '/Cadastro/GetAddressByCep',
            data: { cep: cep },
            success: function (result) {
                $('#Customer_AddressStreet').val(result.Cidade.CEP.LogradouroCompleto);
                $('#Customer_AddressDistrict').val(result.Cidade.CEP.Bairro);
               
                var list = $("#Customer_AddressCityId");
                list.empty();
                list.append(new Option('SELECIONE...', '0'));
                $.each(result.ListaCidades, function (index, item) {
                    list.append(new Option(item.Name, item.Id));
                });

                $("#Customer_AddressCityId").select2().select2("val", result.Cidade.Id);
                $("#Customer_AddressStateId").select2().select2("val", result.Cidade.StateId);

                waitingDialog.hide();
                $('#Customer_AddressNumber').focus();
            }
        });

    }, 2000);
}

function ObterListaCidadePorEstado(idEstado)
{
    $.ajax({
        url: '/Cadastro/GetCitiesByState',
        data: { StateID: idEstado },
        success: function (result) {

            var list = $("#Customer_AddressCityId");
            list.empty();
            list.append(new Option('SELECIONE...', '0'));
            $.each(result.Cidades, function (index, item) {
                list.append(new Option(item.Name, item.Id));
            });

            list.select2().select2("val", 0);
        }
    });
}