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
}
$('a[href="#tab2"]').click(function () {
    loadDATA();
});

$('#tab2').on('click', '.sending', function (e) {
    e.preventDefault();
    $.ajaxSetup({ cache: false });
    $.get(this.href);
    $.get(location.protocol + '//' + location.host + '/Shipment/EditClaim/' + $(this).attr('data-id'));
    $(this).text('Отправлено');
    $(this).addClass('disabled');
});
function loadDATA() {
    $("#tab2").empty();
    $.ajaxSetup({ cache: false });

    $.getJSON(location.protocol + '//' + location.host + '/Shipment/ListSend', function (data) {
        for (var i = 0; i < data.length; i++) {
            console.log(data);
            $("#tab2").append(`
                <div class="card">
                    
                <div class="card-header">
                    <h2>Машина:`+ data[i].auto + `</h2>
                    <a class="heading-elements-toggle"><i class="icon-ellipsis font-medium-3"></i></a>
                <h5>Машина заполнена на: `+ data[i].proc + `%</h5>
                <a class="btn sending btn-warning" href="`+ location.protocol + '//' + location.host + '/Shipment/Otpr/' + data[i].Id + `" data-id="` + data[i].Id+`">Отправить</a>
                </div>
                `);
        }

        $("#tab2").append(`      </div></div >`);

    });
}