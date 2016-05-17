var originalVal = $.fn.val;

$(document).ready(function () {
    $('.datepicker').datepicker({
        setDate: new Date(),
        onSelect: function (date) {
            $(this).valid();
        },
        dateFormat: 'dd/mm/yy',
        language: 'pt',
        dateFormat: 'dd/mm/yy',
        dayNames: ['Domingo', 'Segunda', 'Terça', 'Quarta', 'Quinta', 'Sexta', 'Sábado'],
        dayNamesMin: ['D', 'S', 'T', 'Q', 'Q', 'S', 'S', 'D'],
        dayNamesShort: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sáb', 'Dom'],
        monthNames: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
        monthNamesShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'],
        nextText: 'Próximo',
        prevText: 'Anterior',
        showOn: "button",
        buttonImage: "../content/img/calendar.png",
        buttonImageOnly: true
    });
});

$.fn.val = function (value) {
    if (typeof value == 'undefined') {
        return originalVal.call(this);
    } else {
        setTimeout(function () {
            this.trigger('mask.maskMoney');
        }.bind(this), 100);
        return originalVal.call(this, value);
    }
};



jQuery.fn.extend({
    setMessage: function (value) {
        var o = $(this);
        o.find(".modal-body > p").html(value);
        return o;
    },
    hideAlertTitle: function () {
        var o = $(this);
        o.find(".modal-body > h3").hide();
        return o;
    },
    singleMsgClick: function (fn) {
        var o = $(this);
        o.find(".modal-footer > button").one("click", fn);
        return o;
    },
    setConfirmationClose: function (fn) {
        var o = $(this);
        o.find("#btnConfirmNo").one("click", fn);
        return o;
    },
    isMobile: function () {
        //Detecta se o useragent é um dispositivo mobile.
        var userAgent = navigator.userAgent.toLowerCase();
        return userAgent.search(/(android|avantgo|blackberry|bolt|boost|cricket|docomo|fone|hiptop|mini|mobi|palm|phone|pie|up\.browser|up\.link|webos|wos)/i) != -1;
    },
    //--------------------------------------------------------------------------------------
    // Criado por: Pablo Tavares
    // Data de criação: 2015-01-07
    // Função: verificar se o navegador é Internet Explorer, caso positivo retorna a versão numérica do navegador.
    //--------------------------------------------------------------------------------------
    isInternetExplorer: function () {
        if (!!navigator.userAgent.match(/Trident\/7\./) || navigator.appVersion.indexOf("MSIE") > -1) {
            var ua = window.navigator.userAgent;
            var msie = ua.indexOf('MSIE ');
            var trident = ua.indexOf('Trident/');

            //IE 10 ou antigo...
            if (msie > 0) return parseInt(ua.substring(msie + 5, ua.indexOf('.', msie)), 10);
                //IE 11 ou mais recente...
            else if (trident > 0) {
                var rv = ua.indexOf('rv:');
                return parseInt(ua.substring(rv + 3, ua.indexOf('.', rv)), 10);
            }
        }
        else return false;
    },
    checkJavaCompatibilityWithChrome: function () {
        var ua = navigator.userAgent,
            nv = ua.replace(/^.+(Chrome\/\d+)[^$]+$/i, "$1").split("/"),
            nav = nv[0],
            ver = parseInt(nv[1]);
        if (isNaN(ver)) {
            ver = 0;
        };
        if (nav == 'Chrome' && ver >= 42) {
            location.href = URLChromeUnsupported;
            return false;
        };
        return true;
    },
    checkCompatibility: function () {
        var versaoIE = $(document).isInternetExplorer();
        if (versaoIE && versaoIE < 9)
            ShowMessage("Você está usando o Internet Explorer em uma versão inferior à 9.\nRecomendamos atualizar seu navegador, pois o portal é suportado apenas por versões superiores à 9.");
    },
    contadorConsultas: function (operacao) {
        SetAjaxLoading(false);

        if (operacao == 1) $(".contador-consultas").hide();

        $.post("/Consulta/ContadorConsultas", function (data) {
            if (data.Retorno) {

                //operacao: 1 - Inicializar; 2 - Atualizar
                if (operacao == 1) {
                    var el = document.querySelector('.odometer');
                    od = new Odometer({
                        el: el,
                        format: '(.ddd),dd',
                        theme: 'car',
                        duration: 2000
                    });
                    od.update(data.Quantidade);
                    setTimeout(function () { $(document).contadorConsultas(2); }, 2400000);
                    $(".contador-consultas").show();
                } else if (operacao == 2) {
                    od.update(data.Quantidade);
                    setTimeout(function () { $(document).contadorConsultas(2); }, 2400000);
                }
                SetAjaxLoading(true);

            } else {
                $(".contador-consultas").remove();
                SetAjaxLoading(true);
            }
        })
        .fail(function () {
            $(".contador-consultas").remove();
            SetAjaxLoading(true);
        });

        return $(this);
    },
    verificarAppletCertisign: function () {
        //Se não for Internet Explorer, a verificação do applet é um pouco mais completa.
        if (!$(document).isInternetExplorer()) return CertisignerApplet && CertisignerApplet.appletObject && CertisignerApplet.appletObject.getJSONRepositories;
        else return CertisignerApplet && CertisignerApplet.appletObject;
    },
    setFocusOnTextEnd: function () {
        var elem = $(this)[0];
        var elemLen = elem.value.length;
        // For IE Only
        if (document.selection) {
            elem.focus();
            var oSel = document.selection.createRange();
            oSel.moveStart('character', -elemLen);
            oSel.moveStart('character', elemLen);
            oSel.moveEnd('character', 0);
            oSel.select();
        } else if (elem.selectionStart || elem.selectionStart == '0') {
            elem.selectionStart = elemLen;
            elem.selectionEnd = elemLen;
            elem.focus();
        }
    },
    containsNumber: function () {
        var matches = $(this).val().match(/\d+/g);
        return matches != null;
    },
    notificarComarcaIndisponivel: function () {
        var seletorComarcas = $(this);
        seletorComarcas.click(function () {
            if ($(this).find("option").length <= 1) ShowMessage("Não constam comarcas para selecionar devido à indisponibilidade no serviço de comarcas.");
        });
    },
    bloquearCartorioSemComarca: function (idComarca) {
        $(this).click(function () {
            if ($(idComarca).val() == "") {
                ShowMessage("É preciso selecionar uma comarca antes de selecionar um cartório.");
            }
        });
    },
    bloquearDocumentoSemTipo: function (idCampoTipoDocumento, mensagem) {
        $(this).keydown(function () {
            var tipoDocumento = eval($("#" + idCampoTipoDocumento + " option:selected").val());
            if (tipoDocumento == 0) {
                ShowMessage(mensagem);
                return false;
            }
        });
    },
    validarNomeSobrenome: function () {
        var personRule = new RegExp("[a-zA-ZàáâäãåąčćęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšžÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹ'-.]+[ ]+[a-zA-Z0-9àáâäãåąčćęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšžÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹ'-.]+");
        return personRule.test($(this).val());
    },
    //--------------------------------------------------------------------------------------
    // Criado por: Pablo Tavares
    // Data de criação: 2014-12-29
    // Função: validação tanto de CPF quanto de CNPJ considerando possibilidade de ter pontuações no valor informado.
    //--------------------------------------------------------------------------------------
    validarCPFCNPJ: function () {
        var campo = $(this);
        var documento = campo.val();
        documento = documento.replace(/\./g, '')
				        .replace(/\-/g, '')
				        .replace(/\//g, '');

        var numeros, digitos, soma, i, resultado, digitos_iguais, pos, tamanho;
        digitos_iguais = 1;

        if (documento.length != 11 && documento.length != 14)
            return false;

        for (i = 0; i < documento.length - 1; i++) {
            if (documento.charAt(i) != documento.charAt(i + 1)) {
                digitos_iguais = 0;
                break;
            }
        }

        if (!digitos_iguais) {
            //VALIDAÇÃO DO CPF.
            if (documento.length == 11) {
                numeros = documento.substring(0, 9);
                digitos = documento.substring(9);
                soma = 0;

                for (i = 10; i > 1; i--) {
                    soma += numeros.charAt(10 - i) * i;
                }

                resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
                if (resultado != digitos.charAt(0)) return false;

                numeros = documento.substring(0, 10);
                soma = 0;

                for (i = 11; i > 1; i--) {
                    soma += numeros.charAt(11 - i) * i;
                }

                resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
                if (resultado != digitos.charAt(1)) return false;

                return true;
            }
                //VALIDAÇÃO DO CNPJ.
            else {
                tamanho = documento.length - 2
                numeros = documento.substring(0, tamanho);
                digitos = documento.substring(tamanho);
                soma = 0;
                pos = tamanho - 7;

                for (i = tamanho; i >= 1; i--) {
                    soma += numeros.charAt(tamanho - i) * pos--;
                    if (pos < 2) pos = 9;
                }

                resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
                if (resultado != digitos.charAt(0)) return false;

                tamanho = tamanho + 1;
                numeros = documento.substring(0, tamanho);
                soma = 0;
                pos = tamanho - 7;

                for (i = tamanho; i >= 1; i--) {
                    soma += numeros.charAt(tamanho - i) * pos--;
                    if (pos < 2) pos = 9;
                }

                resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;

                if (resultado != digitos.charAt(1))
                    return false;

                return true;
            }
        }
        else return false;
    },
    formValidateRGRNE: function (showErrorMessage) {
        var campoDocumento = $(this);
        if (campoDocumento.val().length == 0 || campoDocumento.val() == '________________') return true;
        else if (!campoDocumento.containsNumber()) {
            if (showErrorMessage) ShowMessage("O RG ou RNE informado é inválido.");
            campoDocumento
                .val("")
                .focus();
            return false;
        }
        else return true;
    }
});

function ShowSuccess(message) {
    return ShowMessage(message, true);
}

function ShowFinish(message) {
    //Habilitar modal e exibir mensagem.
    $("#modal-sucesso").setMessage(message)
         .modal("show")
         .find(".modal-body > h3").show();
    return;
}

function ShowMessage(message, success) {
    var idModal = success == true ? "#modal-sucesso" : "#modal-erro-cenprot";
    var modal = $(idModal);

    //Ocultar outros modais que estejam ativos.
    $(".modal:not(" + idModal + ")").filter(function () { return $(this).data('bs.modal') != null && $(this).data('bs.modal').isShown; }).modal("hide");

    //Habilitar modal e exibir mensagem.
    modal.setMessage(message)
         .modal("show")
         .find(".modal-body > h3").show();

    return modal;
}

function ShowConfirmation(message, callback) {
    var modal = $("#modal-confirmation");
    modal.find("#btnConfirmYes")
        .unbind("click")
        .bind("click", callback);
    modal.setMessage(message).modal("show");
    return modal;
}

function ShowHTTPException(httpex) {
    var message = "";
    if (httpex.responseText.indexOf("<html") >= 0) {
        //Se for uma exception gerada pelo .NET, busca no HTML a mensagem de erro específica.
        if (httpex.responseText.indexOf(".NET Framework") > -1) message = $($.parseHTML(httpex.responseText)).find("i").html();
        else message = "Não foi possível realizar a operação solicitada.";
    }
    else message = httpex.responseText;
    return ShowMessage(message);
}

// Numeric only control handler
jQuery.fn.ForceNumericOnly =
function () {
    return this.each(function () {
        $(this).keydown(function (e) {
            var key = e.charCode || e.keyCode || 0;
            // allow backspace, tab, delete, enter, arrows, numbers and keypad numbers ONLY
            // home, end, period, and numpad decimal
            return (
                key == 8 ||
                key == 9 ||
                key == 13 ||
                key == 46 ||
                key == 110 ||
                key == 190 ||
                (key >= 35 && key <= 40) ||
                (key >= 48 && key <= 57) ||
                (key >= 96 && key <= 105));
        });
    });
};