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

    setTimeout(function () {

        $.ajax({
            url: '/Cadastro/GetAddressByCep',
            data: { cep: cep },
            success: function (result) {
                $('#Customer_AddressStreet').val(result.Cidade.CEP.LogradouroCompleto);
                $('#Customer_AddressDistrict').val(result.Cidade.CEP.Bairro);

                var list = $("#Customer_AddressCityId");
                list.empty();
                $.each(result.ListaCidades, function (index, item) {
                    list.append(new Option(item.Name, item.Id));
                });

                $("#Customer_AddressCityId").select2().select2("val", result.Cidade.Id);
                $("#Customer_AddressStateId").select2().select2("val", result.Cidade.StateId);

                waitingDialog.hide();
            }
        });

    }, 2000);
}