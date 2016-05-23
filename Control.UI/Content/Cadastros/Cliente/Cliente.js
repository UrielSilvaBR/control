$(document).ready(function () {

    AlterarTipoCliente(1);

    $('#cboTipoCliente').change(function () {
        var idTipoCliente = $(this).val();
        AlterarTipoCliente(idTipoCliente);
    });

    InicializarMascarasCampos();
});


function InicializarMascarasCampos() {
    $('#ZipCode').mask('99999-999');
}

function AlterarTipoCliente(idTipoCliente)
{
    if (idTipoCliente == 1) {
        $('#lblCnpjCpf').text('CPF');
        $('#Document').mask('999.999.999-99');
        $('#Document').focus();
    }
    else {
        $('#lblCnpjCpf').text('CNPJ');
        $('#Document').mask('99.999.999/9999-99');
        $('#Document').focus();
    }
}

