$.ajaxSetup({ cache: false });
function ConvertJsonDateString(jsonDate) {
    var shortDate = null;
    if (jsonDate) {
        var regex = /-?\d+/;
        var matches = regex.exec(jsonDate);
        var dt = new Date(parseInt(matches[0]));
        var month = dt.getMonth() + 1;
        var monthString = month > 9 ? month : '0' + month;
        var day = dt.getDate();
        var dayString = day > 9 ? day : '0' + day;
        var year = dt.getFullYear();
        shortDate = monthString + '-' + dayString + '-' + year;
    }
    return shortDate;
};
$(document).ready(function () {
    loadDATA();
});

function loadDATA() {
    $("#tab1").empty();
    $.ajaxSetup({ cache: false });

    $.getJSON(location.protocol + '//' + location.host + '/Shipment/ListShip', function (data) {
        for (var i = 0; i < data.length; i++) {
            $("#tab1").append(`
                <div class="card">
                    
                <div class="card-header">
                    <h2>Машина:`+ data[i].auto + `</h2>
                    <a class="heading-elements-toggle"><i class="icon-ellipsis font-medium-3"></i></a>
                </div>
                <div class="card-body">`);
            for (var j = 0; j < data[i].ar.length; j++) {
                $("#tab1").append(`
                <h4>Дата принятия заявки: `+ ConvertJsonDateString(data[i].ar[j].DatePrin) + ` Имя клиента:` + data[i].ar[j].NameClient + `</h4>
                 <h5>Общая масса заявки: `+ data[i].ar[j].ObchMasClaim + `</h5>  `);
            }
            }
            
        $("#tab1").append(`      </div></div >`);
       
    });
}