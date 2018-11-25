$(document).ready(function () {
    loadDATA();
});
$('a[data-action="reload"]').click(function () {
    loadDATA();
});
function loadDATA() {
    $("#AutoListTable").empty();
    $.ajaxSetup({ cache: false });
    $.getJSON(location.protocol + '//' + location.host + '/AutoPark/DriverListTable', function (data) {
        console.log(data);
        for (var i = 0; i < data.length; i++) {
            $.ajaxSetup({ cache: false });
            $("#AutoListTable").append(
                ' <tr class="clicks" data-idnumb="' + data[i].Id + '" data-target="#ss" data-toggle="modal">'
                + '  <td>' + data[i].FIO + '</td>'
                + '  <td>+' + data[i].Phone + '</td>'
                + '  <td>' + data[i].NumprPrav + '</td>'
                + '  <td>' + data[i].Auto + '</td>'
                + '    </tr>'
            );
        }
    });
}
$('#AutoListTable').on('click', '.clicks', function (e) {
    e.preventDefault();
    var Id = this.getAttribute('data-idnumb');
    console.log(Id);
    $.ajaxSetup({ cache: false });
    $.get(location.protocol + '//' + location.host + '/AutoPark/EditDriverNew/' + Id, function (data) {
        console.log(data);
        $('#ss').html(data);
    });

});
$('.AddNew').click(function (e) {
    e.preventDefault();
    $.ajaxSetup({ cache: false });
    $.get(this.href, function (data) {
        console.log(data);
        $('#ss').html(data);
    });

});
function okDriverPark(data) {
    $('.close').click();
    loadDATA();
    var dt = data.responseJSON;
    $('#okSend .success').css('display', 'block').text(data.FIO + ' успешно добавлен');
    $('.dts').text('Добавлен водитель ');
    $('#succesSend').click();
    setInterval(function () {
        $('#okSend .close').click();
    }, 50000);
}
function okDriverParkEdit(data) {
    $('.close').click();
    loadDATA();
    var dt = data.responseJSON;
    $('#okSend .success').css('display', 'block').text(data.FIO  + ' успешно обнавленена информация');
    $('.dts').text('Обнавлена информация ');
    $('#succesSend').click();
    setInterval(function () {
        $('#okSend .close').click();
    }, 50000);
}
$('#ModelInfo').on('click', '#del', function (e) {

    e.preventDefault();
    console.log(this.href);
    $.ajaxSetup({ cache: false });
    $.get(this.href, function (data) {
        console.log(data);
        $('#ss').html(data);
    });

});