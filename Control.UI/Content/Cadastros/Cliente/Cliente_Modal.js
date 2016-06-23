$(document).ready(function () {

    InicializarMascarasCampos();

    if ($('#Customer_Id_Modal').val() == "0") {
        AlterarTipoCliente(1);
    }
    else {
        AlterarTipoCliente($('#cboTipoCliente_Modal option:selected').val());
    }

    $('#cboTipoCliente_Modal').change(function () {
        var idTipoCliente = $(this).val();
        AlterarTipoCliente(idTipoCliente);
    });

    $('#Customer_ZipCode_Modal').focusout(function () {

        var cep = $(this).val();
        cep = cep.replace("-", "");

        if (cep == "________")
            return;

        ObterEnderecoPorCEP(cep);
    });

    $('#Customer_CompanyName_Modal').focus();

    $('#Customer_AddressStateId_Modal').change(function () {
        ObterListaCidadePorEstado($(this).val());
    });
});

function InicializarMascarasCampos() {
    $('#Customer_ZipCode_Modal').mask('99999-999');
}

function AlterarTipoCliente(idTipoCliente) {
    if (idTipoCliente == 1) {
        $('#lblCnpjCpf_Modal').text('CPF');
        $('#Customer_Document_Modal').mask('999.999.999-99');
        $('#Customer_Document_Modal').focus();
    }
    else {
        $('#lblCnpjCpf_Modal').text('CNPJ');
        $('#Customer_Document_Modal').mask('99.999.999/9999-99');
        $('#Customer_Document_Modal').focus();
    }
}

function ObterEnderecoPorCEP(cep) {

    $('#modal-cadastro-cliente').modal('hide');

    waitingDialog.show('Buscando Endereço', { dialogSize: 'sm', progressType: 'success' });
    $('#Customer_AddressStreet_Modal').val('');
    $('#Customer_AddressDistrict_Modal').val('');

    setTimeout(function () {

        $.ajax({
            url: '/Cadastro/GetAddressByCep',
            data: { cep: cep },
            success: function (result) {
                $('#Customer_AddressStreet_Modal').val(result.Cidade.CEP.LogradouroCompleto);
                $('#Customer_AddressDistrict_Modal').val(result.Cidade.CEP.Bairro);

                var list = $("#Customer_AddressCityId_Modal");
                list.empty();
                list.append(new Option('SELECIONE...', '0'));
                $.each(result.ListaCidades, function (index, item) {
                    list.append(new Option(item.Name, item.Id));
                });

                $("#Customer_AddressCityId_Modal").select2().select2("val", result.Cidade.Id);
                $("#Customer_AddressStateId_Modal").select2().select2("val", result.Cidade.StateId);

                waitingDialog.hide();
                $('#modal-cadastro-cliente').modal('show');
                $('#Customer_AddressNumber_Modal').focus();
            }
        });

    }, 2000);
}

function ObterListaCidadePorEstado(idEstado) {

    if (typeof idCidade === 'undefined') { idCidade = 0; }

    $.ajax({
        url: '/Cadastro/GetCitiesByState',
        data: { StateID: idEstado },
        success: function (result) {

            var list = $("#Customer_AddressCityId_Modal");
            list.empty();
            list.append(new Option('SELECIONE...', '0'));
            $.each(result.Cidades, function (index, item) {
                list.append(new Option(item.Name, item.Id));
            });

            list.select2().select2("val", 0);
        }
    });
}