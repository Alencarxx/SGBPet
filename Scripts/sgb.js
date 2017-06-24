jQuery.validator.setDefaults({
    highlight: function (element, errorClass, validClass) {
        $(element).closest('.control-group').addClass('error');
    },
    unhighlight: function (element, errorClass, validClass) {
        $(element).closest('.control-group').removeClass('error');
    }
});


jQuery.validator.addMethod("regex",
    function (value, element, regexp) {
        return this.optional(element) || new RegExp(regexp).test(value);
    });