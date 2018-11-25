$.ajaxSetup({ cache: false });
$(document).ready(function () {
    loadDATA();
});
$('a[data-action="reload"]').click(function () {
    loadDATA();
});
function loadDATA() {
    $("#AutoListTable").empty();
    $.ajaxSetup({ cache: false });
    $.getJSON(location.protocol + '//' + location.host + '/AutoPark/AutoListTable', function (data) {
        for (var i = 0; i < data.length; i++) {
            $("#AutoListTable").append(
                ' <tr class="clicks" data-idnumb="' + data[i].Id +'" data-target="#ss" data-toggle="modal">'
                + '  <td data-toggle="tooltip" data-original-title="Нажмите для изменения или удаления" data-trigger="hover">' + data[i].Marka + '</td>'
                + '  <td>' + data[i].Model + '</td>'
                + '  <td>' + data[i].Year + '</td>'
                + '  <td>' + data[i].TypeTopl + '</td>'
                + '  <td>' + data[i].ObmDvig + '</td>'
                + '  <td>' + data[i].ObmKuz + '</td>'
                + '  <td>' + data[i].NumberAuto + '</td>'
                + '    </tr>'
            );
        }
    });
}
$('#AutoListTable').on('click', '.clicks', function (e) {
    e.preventDefault();
    var Id = this.getAttribute('data-idnumb');
    $.ajaxSetup({ cache: false });
    $.get(location.protocol + '//' + location.host + '/AutoPark/EditAutoNew/' + Id, function (data) {
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
function okAutoPark(data) {
    $('.close').click();
    loadDATA();
    var dt = data.responseJSON;
    $('#okSend .success').css('display', 'block').text(data.Marka + ' ' + data.Model + ' успешно добавлено');
    $('.dts').text('Добавлено авто ' + data.Marka);
    $('#succesSend').click();
    setInterval(function () {
        $('#okSend .close').click();
    }, 50000);
}
function okAutoParkEdit(data) {
    $('.close').click();
    loadDATA();
    var dt = data.responseJSON;
    $('#okSend .success').css('display', 'block').text(data.Marka + ' ' + data.Model + ' успешно обнавленино');
    $('.dts').text('Обнавлено авто ' + data.Marka);
    $('#succesSend').click();
    setInterval(function () {
        $('#okSend .close').click();
    }, 50000);
}
$('#ModelInfo').on('click','#del', function (e) {
    
    e.preventDefault();
    console.log(this.href);
    $.ajaxSetup({ cache: false });
    $.get(this.href, function (data) {
        console.log(data);
        $('#ss').html(data);
    });
    
});