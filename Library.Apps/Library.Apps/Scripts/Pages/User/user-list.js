var table;
$(document).ready(function () {    
    $(function () {
        $('#formRegister').submit(function () {
            if ($(this).valid()) {
                saveData();
            }
        });
    });

    $('#btnAdd').click(function () {
        $("#formRegister")[0].reset();
        $("#formRegister").val("");
        if ($('.area-form:visible').length == 0)
            $('.area-form').show();
        else
            $('.area-form').hide();
    })

    loadTable();
});


var loadTable = function () {
    $.ajax({
        type: "GET",
        url: "/User/GetUserWithRole",
        contentType: "application/json; charset=utf-8",
        async: false,
        dataType: "json",
        success: function (response) {
            var data = response.Data;
            $.each(data, function (index) {
                var htmlString = '';
                var htmlCheckBox = data[index].IsActive ? '<label class="checkbox-inline"><input type="checkbox" checked>Active</label>' : '';
                htmlString += '<tr>';
                htmlString += '<td>' + data[index].UserName + '</td>';
                htmlString += '<td>' + data[index].FullName + '</td>';
                htmlString += '<td>' + data[index].RoleName + '</td>';
                htmlString += '<td>' + htmlCheckBox + '</td>';
                htmlString += '<td><button type="button" class="btn btn-info btn-sm" onclick="Edit(' + data[index].UserId + ')">Edit</button> <button type="button" class="btn btn-danger btn-sm" onclick="Delete(' + data[index].UserId + ')">Delete</button ></td >';
                htmlString += '</tr>';
                $('#tblUser tbody').append(htmlString);
            });

            table = $('#tblUser').DataTable({
                "searching": false,
                "bDestroy": true,
                "lengthChange": false
            });

            //table.on('order.dt search.dt', function () {
            //    table.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            //        cell.innerHTML = i + 1;
            //    });
            //}).draw();

        }, //End of AJAX Success function  

        failure: function (data) {
            alert(data.responseText);
        }, //End of AJAX failure function  
        error: function (data) {
            alert(data.responseText);
        } //End of AJAX error function  

    });
}


var Edit = function (id) {
    $('#UserId').val(id);
    $.ajax({
        type: "GET",
        url: "/User/GetUserByID/" + id,
        contentType: "application/json; charset=utf-8",
        async: false,
        dataType: "json",
        success: function (response) {
            $('#UserName').val(response.Data.UserName)
            $('#FullName').val(response.Data.FullName)
            $('#Password').val(response.Data.Password)
            if (response.Data.IsActive)
                $('#IsActive').prop('checked', true);            
            $('#RoleId').val(response.Data.RoleId)
            $('.area-form').show();
            
        }, //End of AJAX Success function  

        failure: function (data) {
            alert(data.responseText);
        }, //End of AJAX failure function  
        error: function (data) {
            alert(data.responseText);
        } //End of AJAX error function  

    });

}


var GetData = function () {
    var formToken = $('input[name="__RequestVerificationToken"]').val();
    return {
        __RequestVerificationToken: formToken,
        UserId: $('#UserId').val(),
        Password: $('#Password').val(),
        UserName: $('#UserName').val(),
        FullName: $('#FullName').val(),
        RoleId: $('#RoleId').val(),
        IsActive: $('#IsActive:checked').is(":checked")
    };
}

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
            destroyTable();
            loadTable();
            $("#msg").text(response.Message)
            $('.message').modal('show');

            console.log(response);
        },
        error: function (xhr, textStatus, errorThrown) {
            console.log('Error in Operation');
        }
    });
}

var Delete = function (id) {    
    var answer = confirm('Are you sure want to delete this item ?')
    if (answer) {
        $.ajax({
            url: "/User/DeleteUser/" + id,
            type: "POST",
            data: { __RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val() },
            success: function (response) {
                $("#msg").text(response.Message)
                $('.message').modal('show');
                destroyTable();
                //rebinding data
                loadTable();    
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}
