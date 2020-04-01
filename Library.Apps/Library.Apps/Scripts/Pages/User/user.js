$(document).ready(function () {
    $(function () {
        $('#formRegister').submit(function () {
            if ($(this).valid()) {
                saveData();
            }
        });
    });
});


var saveData = function () {
    var data = GetData();
    $.ajax({
        type: 'POST',
        url: "/User/SaveData",
        //contentType: "application/json; charset=utf-8",
        dataType: 'json',
        async: false,
        data: data,
        success: function (response) {
            $("#msg").text(response.Message)
            $('.message').modal('show');

            console.log(response);
        },
        error: function (xhr, textStatus, errorThrown) {
            console.log('Error in Operation');
        }
    });
}

var GetData = function () {
    var formToken = $('input[name="__RequestVerificationToken"]').val();
    return {
        __RequestVerificationToken: formToken,
        UserId: 0,
        Password: $('#Password').val(),
        RoleId: 2,
        UserName: $('#UserName').val(),
        FullName: $('#FullName').val()
    };
}