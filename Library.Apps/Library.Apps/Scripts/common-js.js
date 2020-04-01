var destroyTable = function () {
    table.rows().remove().draw(); //remove all rows in table
    table.destroy(); //remove jquery datatable
}

var GetDateFormatter = function (strDate) {
    strDate = strDate.replace(/[^0-9\.]/g, '');
    var dt = new Date();
    dt.setTime(strDate);
    var year, month, date;
    if (dt.getMonth() < 10)
        month = "0" + dt.getMonth()
    else
        month = dt.getMonth()

    if (dt.getDate() < 10)
        date = "0" + dt.getDate()
    else
        date = dt.getDate()
    year = dt.getFullYear();
    var res = year + "-" + month + "-" + date;
    return res;
}
