var table;
$(document).ready(function () {
    $("#formTransaction")[0].reset();
    $(function () {
        $('#formTransaction').submit(function () {
            if ($(this).valid()) {
                saveData();
            }
        });
    });

    $('#btnAdd').click(function () {
        $("#formTransaction")[0].reset();
        $("#TransactionId").val("");
        if ($('.area-form:visible').length == 0)
            $('.area-form').show();
        else
            $('.area-form').hide();
    })
    getMasterBooks();
    loadTable();

    $('#BookId').on('change', function () {
        getBookById(this.value);
    });

    $('#EndDate').on('change', function () {
        $('#Total').val('');
        validationDate();
    });

    $('#StartDate').on('change', function () {
        $('#Total').val('');
        validationDate();
    });    
    
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
                $('#Total').val("")
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
        $('#Total').val("")
    }
    
}

var loadTable = function () {
    var data = GetData();
    $.ajax({
        type: "POST",
        url: "/Book/GetTransactionDetail",
        //contentType: "application/json; charset=utf-8",
        data: { __RequestVerificationToken: data.__RequestVerificationToken },
        dataType: "json",
        success: function (response) {
            var data = response.Data;
            $.each(data, function (index) {
                var htmlString = '';
                htmlString += '<tr>';
                htmlString += '<td></td>'; //for no
                htmlString += '<td>' + data[index].BookTitle + '</td>';
                htmlString += '<td>' + data[index].Author + '</td>';
                htmlString += '<td>' + data[index].CategoryName + '</td>';
                htmlString += '<td>' + data[index].Price + '</td>';
                htmlString += '<td>' + data[index].Days + '</td>';
                htmlString += '<td>' + data[index].Total + '</td>';
                //htmlString += '<td>' + data[index].StatusName + '</td>';
                if ($('#hiddenRole').val() === "Customer")
                    htmlString += '<td><button type="button" class="btn btn-info btn-sm" onclick="Edit(' + data[index].TransactionId + ')">Edit</button> <button type="button" class="btn btn-danger btn-sm" onclick="Delete(' + data[index].TransactionId + ')">Delete</button ></td >';
                else
                    htmlString += '<td></td>'
                htmlString += '</tr>';
                $('#tblBook tbody').append(htmlString);
            });

            table = $('#tblBook').DataTable({
                "searching": false,
                "bDestroy": true,
                "lengthChange": false,
                "columnDefs": [{
                    "targets": '_all',
                    "defaultContent": ""
                }]
            });

            table.on('order.dt search.dt', function () {
                table.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                    cell.innerHTML = i + 1;
                });
            }).draw();

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
    $('#TransactionId').val(id);
    var data = GetData();
    $.ajax({
        type: "POST",
        url: "/Book/GetTransactionDetailByID/" + id,
        //contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        data: { __RequestVerificationToken: data.__RequestVerificationToken },
        success: function (response) {
            $("#formTransaction")[0].reset();
            $('#BookId').val(response.Data.BookId)
            $('#BookTitle').val(response.Data.BookTitle)
            $('#Author').val(response.Data.Author)
            $('#CategoryName').val(response.Data.CategoryName)
            $('#Price').val(response.Data.Price)
            $('#StartDate').val(GetDateFormatter(response.Data.StartDate))
            $('#EndDate').val(GetDateFormatter(response.Data.EndDate))
            $('#Total').val(response.Data.Total)
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

var getMasterBooks = function () {
    //selectBook
    var data = GetData();
    $.ajax({
        type: "POST",
        url: "/Book/GetBooks/",
        //contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        data: { __RequestVerificationToken: data.__RequestVerificationToken },
        success: function (response) {
            var data = response.Data;
            $('#BookId')
                .append($("<option></option>")
                    .attr("value", "")
                    .text("--Please Select"))
            $.each(data, function (index) {
                $('#BookId')
                    .append($("<option></option>")
                        .attr("value", data[index].BookId)
                        .text(data[index].BookTitle));
            });
        }, //End of AJAX Success function  

        failure: function (data) {
            alert(data.responseText);
        }, //End of AJAX failure function  
        error: function (data) {
            alert(data.responseText);
        } //End of AJAX error function  

    });
}

var validationDate = function () {
    if ($('#StartDate').val() === "") {
        $("#msg").text("Please to select start date first")
        $('.message').modal('show');
        $("#EndDate").val("")
        return;
    }

    var startDate = new Date($('#StartDate').val());
    var endDate = new Date($('#EndDate').val());
    
    if (startDate > endDate) {
        $("#msg").text("End date should be greated than Start Date")
        $('.message').modal('show');
        $("#EndDate").val("")
    }
}

var getTotalTransaction = function () {
    var data = GetData();
    if (data.StartDate !== "" && data.EndDate !== "" && data.Price !== "") {
        $.ajax({
            type: "POST",
            url: "/Book/GetTotal?price=" + data.Price + "&startDate=" + data.StartDate + "&endDate=" + data.EndDate,
            //contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            data: { __RequestVerificationToken: data.__RequestVerificationToken },
            success: function (response) {
                $('#Total').val(response.Data)
            }, //End of AJAX Success function  

            failure: function (data) {
                alert(data.responseText);
            }, //End of AJAX failure function  
            error: function (data) {
                alert(data.responseText);
            } //End of AJAX error function  

        });
    } else {
        $("#msg").text("Price should not be empty")
        $('.message').modal('show');
        $("#EndDate").val("")
    }
}


var saveData = function () {
    var data = GetData();
    $.ajax({
        type: 'POST',
        url: "/Book/SaveTransaction",
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
            url: "/Book/DeleteTransaction/" + id,
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

var exportfunc = function(type) {
    if(type === "EXCEL")
        window.open(window.location.protocol + '//' + window.location.hostname + (window.location.port ? ':' + window.location.port : '') + '/Book/ExportExcel', '_blank');
    else if (type === "PDF")
        window.open(window.location.protocol + '//' + window.location.hostname + (window.location.port ? ':' + window.location.port : '') + '/Book/ExportPDF', '_blank');
}

var GetData = function () {
    var formToken = $('input[name="__RequestVerificationToken"]').val();
    return {
        __RequestVerificationToken: formToken,
        TransactionId: $('#TransactionId').val(),
        BookId: $('#BookId').val(),
        CategoryId: $('#CategoryId').val(),
        StartDate: $('#StartDate').val(),
        EndDate: $('#EndDate').val(),
        Price: $('#Price').val(),
        Total: $('#Total').val(),
    };
}


