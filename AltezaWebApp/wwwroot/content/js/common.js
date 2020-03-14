var isFileSubmit = "";
(function ($) {
    "use strict";




})(jQuery);

function GetFileExtension(filename) {
    var a = filename.split(".");
    if (a.length === 1 || (a[0] === "" && a.length === 2)) {
        return "";
    }
    return a.pop();
}

function SuccessAlertPopup(Title, Message) {
    if (Title == undefined || Title == '')
        Title = 'Success';
    $('#modelFileAlert').find('.modal-title').text(Title);
    $('#modelFileAlert').find('.modal-body').text(Message);
    $('#modelFileAlert').modal('show');
    $('#modelFileAlert').removeClass('alert');
}
function ErrorAlertPopup(Title, Message) {
    if (Title == undefined || Title == '')
        Title = 'Alert';
    $('#modelFileAlert').find('.modal-title').text(Title);
    $('#modelFileAlert').find('.modal-body').text(Message);
    $('#modelFileAlert').modal('show');
    $('#modelFileAlert').addClass('alert');
}

function SendMailAttach() {
    if ($('input[name="name"]').val() == "") {
        $('input[name="name"]').focus();
        return false;
    }
    if ($('input[name="email"]').val() == "") {
        $('input[name="email"]').focus();
        return false;
    }
    if ($('input[name="contact"]').val() == "") {
        $('input[name="contact"]').focus();
        return false;
    }
    if ($('input[name="services"]').val() == "") {
        $('input[name="services"]').focus();
        return false;
    }

    var enqiryForm = {
        Name: $('input[name="name"]').val(),
        Email: $('input[name="email"]').val(),
        ContactNo: $('input[name="contact"]').val(),
        ServiceType: $('select[name="services"]').val(),
        Message: $('textarea[name="message"]').val()
    }

    $.ajax({
        type: "POST",
        url: 'Service/SendEnquiryMail',
        data: enqiryForm,
        //contentType: "application/json; charset=utf-8",
        //dataType: 'json',
        //contentType: false,
        success: function (data) {
            if (data.indexOf('Success') == 0) {
                SuccessAlertPopup('Success', 'Mail sent successfully.');
            }
            else if (data.indexOf('Success') == 0) {
                SuccessAlertPopup('Alert', 'Mail sent failed.');
            }
        },
        error: function (res) {
            alert("Error:" + res.responseText);

        }
    });
}

var specialKeys = new Array();
specialKeys.push(8); //Backspace
specialKeys.push(43); //plus sign
function contactNoValidation(_this, e) {
    var regEx = /^[+-]?\d+$/;
    var keyCode = e.which ? e.which : e.keyCode
    var ret = (((keyCode >= 48 && keyCode <= 57) || specialKeys.indexOf(keyCode) != -1) )
    if (keyCode == 43) {
        ret = !$(_this).val().includes(String.fromCharCode(43));
    }
    if (ret) {
        $(_this).parents('.wrap-input100').attr("data-validate", "Contact no is required")
        var thisAlert = $(_this).parents('.wrap-input100');
        $(thisAlert).removeClass('alert-validate');
    }
    else {
        $(_this).parents('.wrap-input100').attr("data-validate","Allow Only Numbers")
        var thisAlert = $(_this).parents('.wrap-input100');
        $(thisAlert).addClass('alert-validate');
    }

    return ret;
}
function char_count(str, letter) {
    var letter_Count = 0;
    for (var position = 0; position < str.length; position++) {
        if (str.charAt(position) == letter) {
            letter_Count += 1;
        }
    }
    return letter_Count;
}