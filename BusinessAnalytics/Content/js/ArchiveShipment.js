$.ajaxSetup({ cache: false });

$(document).ready(function () {
    loadDATA();
});
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
function loadDATA() {
    $("#Arhive").empty();
    $.ajaxSetup({ cache: false });

    $.getJSON(location.protocol + '//' + location.host + '/Shipment/JsonArchive', function (data) {
        for (var i = 0; i < data.length; i++) {
            $("#Arhive").append(`
<table class="table table-hover mb-0">
                    <thead>
                        <tr>
                            <th>Машина</th>
                            <th>Дата доставки</th>
                            <th>Предприятие</th>
                            <th>Продукция</th>
                        </tr>
                    </thead>
                    <tbody >
                <tr>
                    <td>`+ data[i].auto +`</td>
                    <td>`+ ConvertJsonDateString(data[i].date) +`</td>
                    <td>`+ data[i].user + `</td>
                    <td><a data-toggle="collapse" href="#collapse`+ data[i].IdClaim + `" data-id="` + data[i].IdClaim+`" aria-expanded="true" aria-controls="collapse1" class="card-title lead prod" data-href="`+ location.protocol + '//' + location.host + '/Shipment/ViewClaimProduct/' + data[i].IdClaim +`">Продукция</a></td>
                </tr>
</tbody>
                </table>
                <div id="collapse`+ data[i].IdClaim+`" role="tabpanel" aria-labelledby="headingCollapse1" class="card-collapse collapse" aria-expanded="false">
					<div class="card-body">
						<div class="card-block">
				<table class="table table-hover mb-0">
                    <thead>
                        <tr>
                            <th>Продукция</th>
                            <th>Кол-во</th>
                        </tr>
                    </thead>
                    <tbody class="productClaim`+ data[i].IdClaim+`">

                    </tbody>
                </table>
						</div>
					</div>
				</div>
                `);
        }
    });
}
var ss;
$('#Arhive').on('click', 'a.prod', function () {
    var classProd = '.productClaim' + $(this).attr('data-id');
    $(classProd).empty();
    $.getJSON($(this).attr('data-href'), function (data) {
        
        ss = data;

    }).done(function () {
        for (var i = 0; i < ss.length; i++) {
            console.log(ss[i].ProdName);
            $(classProd).append(`
    <tr>
            <td>`+ ss[i].ProdName + `</td>
            <td>`+ ss[i].count + `</td>
    </tr>
        `);
        }
        });
    
});