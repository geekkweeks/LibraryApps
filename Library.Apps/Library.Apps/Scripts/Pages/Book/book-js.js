var table;
$(document).ready(function () {
    $("#formBook")[0].reset();
    $(function () {
        $('#formBook').submit(function () {
            if ($(this).valid()) {
                saveData();
            }
        });
    });

    $('#btnAdd').click(function () {
        $("#formBook")[0].reset();
        $("#formBook").val("");
        if ($('.area-form:visible').length == 0)
            $('.area-form').show();
        else
            $('.area-form').hide();
    })   
    loadTable();
});

var getBookById = function (id) {
    if (id !== "") {
        var data = GetData();
        $.ajax({
            type: "POST",
            url: "/Book/GetBookById/" + id,
            //contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            data: { __RequestVerificationToken: data.__RequestVerificationToken },
            success: function (response) {
                $('#Author').val(response.Data.Author)
                $('#CategoryName').val(response.Data.CategoryName)
                $('#Price').val(response.Data.Price)
            }, //End of AJAX Success function  

            failure: function (data) {
                alert(data.responseText);
            }, //End of AJAX failure function  
            error: function (data) {
                alert(data.responseText);
            } //End of AJAX error function  

        });
    } else {
        $('#Author').val("")
        $('#CategoryName').val("")
        $('#Price').val("")
    }

}


var loadTable = function () {
    var data = GetData();
    $.ajax({
        type: "POST",
        url: "/Book/GetBooks",
        //contentType: "application/json; charset=utf-8",
        data: { __RequestVerificationToken: data.__RequestVerificationToken },
        dataType: "json",
        success: function (response) {
            var data = response.Data;
            $.each(data, function (index) {
                var htmlString = '';
                var htmlCheckBox = data[index].IsAvailable ? '<label class="checkbox-inline"><input type="checkbox" checked>Active</label>' : '';
                htmlString += '<tr>';
                //htmlString += '<td>' + data[index].BookId + '</td>';
                htmlString += '<td>' + data[index].BookTitle + '</td>';
                htmlString += '<td>' + data[index].Author + '</td>';
                htmlString += '<td>' + data[index].CategoryName + '</td>';
                htmlString += '<td>' + data[index].Price + '</td>';
                htmlString += '<td>' + htmlCheckBox + '</td>';
                htmlString += '<td><button type="button" class="btn btn-info btn-sm" onclick="Edit(' + data[index].BookId + ')">Edit</button> <button type="button" class="btn btn-danger btn-sm" onclick="Delete(' + data[index].BookId + ')">Delete</button ></td >';
                htmlString += '</tr>';
                $('#tblBook tbody').append(htmlString);
            });

            table = $('#tblBook').DataTable({
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

var saveData = function () {
    var data = GetData();
    $.ajax({
        type: 'POST',
        url: "/Book/SaveMasterBook",
        //contentType: "application/json; charset=utf-8",
        dataType: 'json',
        async: false,
        data: data,
        success: function (response) {
            destroyTable();
            loadTable()
            $("#msg").text(response.Message)
            $('.message').modal('show');
            
            console.log(response);
        },
        error: function (xhr, textStatus, errorThrown) {
            console.log('Error in Operation');
        }
    });
}

var Edit = function (id) {
    $('#BookId').val(id);
    var data = GetData();
    $.ajax({
        type: "POST",
        url: "/Book/GetBookById/" + id,
        //contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        data: { __RequestVerificationToken: data.__RequestVerificationToken },
        success: function (response) {
            $("#formBook")[0].reset();
            $('#BookTitle').val(response.Data.BookTitle)
            $('#Author').val(response.Data.Author)
            $('#Price').val(response.Data.Price)
            if (response.Data.IsAvailable)
                $('#IsAvailable').prop('checked', true);
            $('#CategoryId').val(response.Data.CategoryId)
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
        BookId: $('#BookId').val(),
        BookTitle : $('#BookTitle').val(),
        Author: $('#Author').val(),
        CategoryId: $('#CategoryId').val(),
        Price: $('#Price').val(),
        //IsAvailable: $('#IsAvailable:checked').val()
        IsAvailable: $('#IsAvailable:checked').is(":checked")
    };
}

var Delete = function (id) {
    var answer = confirm('Are you sure want to delete this item ?')
    if (answer) {
        $.ajax({
            url: "/Book/DeleteBook/" + id,
            type: "POST",
            async: false,
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

var exportfunc = function (type) {
    if (type === "EXCEL")
        window.open(window.location.protocol + '//' + window.location.hostname + (window.location.port ? ':' + window.location.port : '') + '/Book/ExportMasterBookExcel', '_blank');
    else if (type === "PDF")
        window.open(window.location.protocol + '//' + window.location.hostname + (window.location.port ? ':' + window.location.port : '') + '/Book/ExportMasterBookPDF', '_blank');
}

