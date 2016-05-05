/*
 * Localized default methods for the jQuery validation plugin.
 * Locale: PT_BR
 */
jQuery.extend(jQuery.validator.methods, {
    date: function (value, element) {
        //return this.optional(element) || /^\d\d?\/\d\d?\/\d\d\d?\d?$/.test(value);
        return value.match(/^(0?[1-9]|[12][0-9]|3[0-1])[/., -](0?[1-9]|1[0-2])[/., -](19|20)?\d{2}$/);
    },
    number: function (value, element) {

        value = value.replace("R$", "").trim();

        var valor = parseFloat(value);

        if (valor == 0) {
            value = 0;
            
            var id = element.id;
            $('#' + id).removeClass("valid");

            return this.optional(element) || /^-?(?:\d+|\d{1,3}(?:\.\d{3})+)(?:,\d+)?$/.test(value);
        }

        return this.optional(element) || /^-?(?:\d+|\d{1,3}(?:\.\d{3})+)(?:,\d+)?$/.test(value);
    }
});