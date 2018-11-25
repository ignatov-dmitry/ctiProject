$.getJSON(location.protocol + '//' + location.host + '/Sklad/FinishedProductJson', function (data) {
    for (var i = 0; i < data.length; i++) {
        $("#AddFinishedProduct").append(
            ' <tr>'
            + '  <td>' + data[i].Name + '</td>'
            + '  <td>' + data[i].Count + '</td>'
            + '  <td>' + data[i].Time + '</td>'
            + '    </tr>'
        );
    }
});
function all() {
    $.getJSON(location.protocol + '//' + location.host + '/Sklad/FinishView', function (data) {
        for (var i = 0; i < data.length; i++) {

            $("#alert").append(`<div class="alert alert-danger mb-2" role="alert">
<button type="button" class="close" data-dismiss="alert" aria-label="Close">
										<span aria-hidden="true">×</span>
									</button>
        <strong>Внимание!</strong> На складе заканчивается `+ data[i].Name + `, для добавления перейдите в <a href="../Sklad/PackagingView" class="alert-link">упаковки</a> .
    </div>`);

        }
    });
}
$.getJSON(location.protocol + '//' + location.host + '/Sklad/FinishView', function (data) {
    for (var i = 0; i < data.length; i++) {

        $("#alert").append(`<div class="alert alert-danger mb-2" role="alert">
<button type="button" class="close" data-dismiss="alert" aria-label="Close">
										<span aria-hidden="true">×</span>
									</button>
        <strong>Внимание!</strong> На складе заканчивается `+ data[i].Name + `, для добавления перейдите в <a href="../Sklad/PackagingView" class="alert-link">упаковки</a> .
    </div>`);

    }
});
function okFinishedProduct(data) {
    AddFinished();
    all();
    data = data.responseJSON;
    $('#inlineForm .close').click();
    $("#clearFormFinish").click();


    var con = 0;
    let state = 1;
    var counte = 0;
    var id;
    $.get(location.protocol + '//' + location.host + '/Sklad/showIdFinish/' + data.PackagingId, function (one) {
        counte = one;
    }).done(function () {
        $.get(location.protocol + '//' + location.host + '/Sklad/ShowPackCount/' + data.PackagingId, function (s) { id = s; }).done(function () {

            con = Number(id) + Number(counte);
            con2 = Number(con) - Number(id);
            if (con2 < 0 && con2 !== 0) {
                state = 2;
                $("#alert").append(`<div class="alert alert-warning alert-dismissible fade in mb-2" role="alert">
									<button type="button" class="close" data-dismiss="alert" aria-label="Close">
										<span aria-hidden="true">×</span>
									</button>
									 Готовая продукция `+ data.Name + ` не добавлена на склад,количество упаковки ` + id + ` на складе.
								</div>`);

                return;
            } else if (con2 > 0 || con2 === 0) {
                $('#okSend .success').css('display', 'block').text('Позиция ' + data.Name + ' в количестве ' + data.Count + 'шт.');
                $('#succesSend').click();
                setInterval(function () {
                    $('#okSend .close').click();
                }, 50000);
            }
        });
            

        
        });
    
    


}
function Drop() {
    $('#Id').empty();
    $.getJSON(location.protocol + '//' + location.host + '/Sklad/DropSpec', function (data) {
        $('#Id').append('<option value="" style="display:none" disabled selected>Наименование</option>');
        console.log(data);
        for (var i = 0; i < data.length; i++) {

            $('#Id').append('<option value="' + data[i].Id + '">' + data[i].Name + '</option>');
        }
    });
}
function AddFinished() {
    $("#AddFinishedProduct").empty();
    $.getJSON(location.protocol + '//' + location.host + '/Sklad/FinishedProductJson', function (data) {
        for (var i = 0; i < data.length; i++) {
            $("#AddFinishedProduct").append(
                ' <tr>'
                + '  <td>' + data[i].Name + '</td>'
                + '  <td>' + data[i].Count + '</td>'
                + '  <td>' + data[i].Time + '</td>'
                + '    </tr>'
            );
        }
    });
}

function errorFinishedProduct() {
    $('#succesSend').click();
    $('#okSend .error').css('display', 'block');
    setInterval(function () {
        $('#okSend .close').click();
        $('#okSend .error').css('display', 'none');
    }, 50000);
}

$("#clearFormFinish").click(function (e) {
    e.preventDefault();
    $("#FinishedProduct")[0].reset();
});