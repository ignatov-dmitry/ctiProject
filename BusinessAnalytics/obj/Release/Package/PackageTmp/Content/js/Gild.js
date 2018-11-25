$.getJSON(location.protocol + '//' + location.host + '/Sklad/GildJson', function (data) {
    for (var i = 0; i < data.length; i++) {
        $("#AddSyr").append(
            ' <tr>'
            + '  <td>' + data[i].Name + '</td>'
            + '  <td>' + data[i].Count + '</td>'
            + '  <td>' + data[i].Time + '</td>'
            + '    </tr>'
        );
    }
});
function all() {
    $("#alert").empty();
    $.getJSON(location.protocol + '//' + location.host + '/Sklad/SyrView', function (data) {
        for (var i = 0; i < data.length; i++) {

            $("#alert").append(`<div class="alert alert-danger mb-2" role="alert">
<button type="button" class="close" data-dismiss="alert" aria-label="Close">
										<span aria-hidden="true">×</span>
									</button>
        <strong>Внимание!</strong> На складе заканчивается `+ data[i].Name + `, для добавления перейдите в <a href="../Sklad/InSklad" class="alert-link">приход на склад</a> сырья.
    </div>`);

        }
    });
    $.getJSON(location.protocol + '//' + location.host + '/Sklad/SpecView', function (data) {
        for (var i = 0; i < data.length; i++) {

            $("#alert").append(`<div class="alert alert-danger mb-2" role="alert">
<button type="button" class="close" data-dismiss="alert" aria-label="Close">
										<span aria-hidden="true">×</span>
									</button>
        <strong>Внимание!</strong> На складе заканчивается `+ data[i].Name + `, для добавления перейдите в <a href="../Sklad/Condiments" class="alert-link">Специи</a> .
    </div>`);

        }
    });
}
$.getJSON(location.protocol + '//' + location.host + '/Sklad/SyrView', function (data) {
    for (var i = 0; i < data.length; i++) {

        $("#alert").append(`<div class="alert alert-danger mb-2" role="alert">
<button type="button" class="close" data-dismiss="alert" aria-label="Close">
										<span aria-hidden="true">×</span>
									</button>
        <strong>Внимание!</strong> На складе заканчивается `+ data[i].Name + `, для добавления перейдите в <a href="../Sklad/InSklad" class="alert-link">приход на склад</a> сырья.
    </div>`);

    }
});
$.getJSON(location.protocol + '//' + location.host + '/Sklad/SpecView', function (data) {
    for (var i = 0; i < data.length; i++) {

        $("#alert").append(`<div class="alert alert-danger mb-2" role="alert">
<button type="button" class="close" data-dismiss="alert" aria-label="Close">
										<span aria-hidden="true">×</span>
									</button>
        <strong>Внимание!</strong> На складе заканчивается `+ data[i].Name + `, для добавления перейдите в <a href="../Sklad/Condiments" class="alert-link">Специи</a> .
    </div>`);

    }
});
$.getJSON(location.protocol + '//' + location.host + '/Sklad/GildSpecJson', function (data) {
    for (var i = 0; i < data.length; i++) {
        $("#AddScpec").append(
            ' <tr>'
            + '  <td>' + data[i].Name + '</td>'
            + '  <td>' + data[i].Count + '</td>'
            + '  <td>' + data[i].Time + '</td>'
            + '    </tr>'
        );
    }
});

$("#clearFormGild").click(function (e) {
    e.preventDefault();
    $("#form0")[0].reset();
});
$("#clearFormGildSpec").click(function (e) {
    e.preventDefault();
    $("#Spec")[0].reset();
});
$('a[data-action="reloadGild"]').on('click', function () {
    var block_ele = $(this).closest('.card');
    $("#AddSyr").empty(); $("#AddScpec").empty();
    $.getJSON(location.protocol + '//' + location.host + '/Sklad/GildJson', function (data) {
        for (var i = 0; i < data.length; i++) {
            $("#AddGild").append(
                ' <tr>'
                + '  <td>' + data[i].Name + '</td>'
                + '  <td>' + data[i].Count + '</td>'
                + '  <td>' + data[i].Time + '</td>'
                + '    </tr>'
            );
        }
    })
    $.getJSON(location.protocol + '//' + location.host + '/Sklad/GildSpecJson', function (data) {
        for (var i = 0; i < data.length; i++) {
            $("#AddScpec").append(
                ' <tr>'
                + '  <td>' + data[i].Name + '</td>'
                + '  <td>' + data[i].Count + '</td>'
                + '  <td>' + data[i].Time + '</td>'
                + '    </tr>'
            );
        }
    });
    // Block Element
    block_ele.block({
        message: '<div class="icon-spinner9 icon-spin icon-lg"></div>',
        timeout: 2000, //unblock after 2 seconds
        overlayCSS: {
            backgroundColor: '#FFF',
            cursor: 'wait',
        },
        css: {
            border: 0,
            padding: 0,
            backgroundColor: 'none'
        }
    });
});
function okGildAdd(data) {
    $('#inlineForm .close').click();
    var dt = data.responseJSON;
    $("#clearFormGild").click();
    $("#clearFormGildSpec").click();
    console.log(dt.State);
    if (dt.ProductId !== null) {
        if (!dt.State) {
            $("#alert").append(`<div class="alert alert-warning alert-dismissible fade in mb-2" role="alert">
									<button type="button" class="close" data-dismiss="alert" aria-label="Close">
										<span aria-hidden="true">×</span>
									</button>
									 Сырье `+ dt.Name + ` не добавленно в цех.
								</div>`);
            return;

        } else {
            all();
            $('#okSend .success').css('display', 'block').text('Позиция ' + dt.Name + ' в количестве ' + dt.Count + ' кг.');
            $('#succesSend').click();
            setInterval(function () {
                $('#okSend .close').click();
            }, 50000);
            return;
        }
    }
    else {

        if (!dt.State) {
            $("#alert").append(`<div class="alert alert-warning alert-dismissible fade in mb-2" role="alert">
									<button type="button" class="close" data-dismiss="alert" aria-label="Close">
										<span aria-hidden="true">×</span>
									</button>
									Специя `+ dt.Name + ` не добавленно в цех.
								</div>`);

        }
        else {
            all();
            $('#okSend .success').css('display', 'block').text('Позиция ' + dt.Name + ' в количестве ' + dt.Count + 'кг.');
            $('#succesSend').click();
            setInterval(function () {
                $('#okSend .close').click();
            }, 50000);
        }

    }




}
function okGildUpdate(data) {
    $('div[id^="gildEdit"] .close').click();
    var dt = data.responseJSON;
    $('#okSend .success').css('display', 'block').text(dt.Name + ' успешно обновлен');
    $('#succesSend').click();
    setInterval(function () {
        $('#okSend .close').click();
    }, 50000);
}
function okGild() {
    $("#AddSyr").empty();
    $("#AddScpec").empty();
    $.getJSON(location.protocol + '//' + location.host + '/Sklad/GildSpecJson', function (data) {
        for (var i = 0; i < data.length; i++) {
            $("#AddScpec").append(
                ' <tr>'
                + '  <td>' + data[i].Name + '</td>'
                + '  <td>' + data[i].Count + '</td>'
                + '  <td>' + data[i].Time + '</td>'
                + '    </tr>'
            );
        }

    });
    $.getJSON(location.protocol + '//' + location.host + '/Sklad/GildJson', function (data) {
        for (var i = 0; i < data.length; i++) {
            $("#AddSyr").append(
                ' <tr>'
                + '  <td>' + data[i].Name + '</td>'
                + '  <td>' + data[i].Count + '</td>'
                + '  <td>' + data[i].Time + '</td>'
                + '    </tr>'
            );
        }
        $("#clearFormGild").click();
        $("#clearFormGildSpec").click();
        $('#inlineForm .close').click();
    });
}