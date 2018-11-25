
function errorSkald() {
    $('#succesSend').click();
    $('#okSend .error').css('display', 'block');
    setInterval(function () {
        $('#okSend .close').click();
        $('#okSend .error').css('display', 'none');
    }, 50000);
}
function okSyr(data) {
    okSklad();
    Drop();
    
    $('#inlineForm .close').click();
    
    var dt = data;
    $('#okSend .success').css('display', 'block').text('Позиция ' + dt.Name + ' в количестве ' + dt.Count + 'кг.');
    $('#succesSend').click();
    setInterval(function () {
        $('#okSend .close').click();
    }, 50000);

}
function okSyre(data) {
    okSklad();
    Drop();
    $('#inlineForm .close').click();
    $("#form0")[0].reset();
    var dt = data;
    $('#okSend .success').css('display', 'block').text('Позиция ' + dt.Name + ' успешно добавлена');
    $('#succesSend').click();
    setInterval(function () {
        $('#okSend .close').click();
    }, 50000);

}
function Drop() {
    $('#Id').empty();
    $.getJSON(location.protocol + '//' + location.host + '/Sklad/DropSyr', function (data) {
        $('#Id').append('<option value="" style="display:none" disabled selected>Наименование</option>');
        console.log(data);
        for (var i = 0; i < data.length; i++) {

            $('#Id').append('<option value="' + data[i].Id + '">' + data[i].Name + '</option>');
        }
    });
}
function okSklad() {
    $("#AddSklad").empty();
    $.getJSON(location.protocol + '//' + location.host + '/Sklad/InSkladJson', function (data) {
        for (var i = 0; i < data.length; i++) {
            $("#AddSklad").append(
                ' <tr>'
                + '  <td>' + data[i].Name + '</td>'
                + '  <td>' + data[i].Count + '</td>'
                + '  <td>' + data[i].Time + '</td>'
                + '    </tr>'
            );
        }
        $("#clearFormProduct").click();
       
    });

}

$(document).ready(function () {
    $.getJSON(location.protocol + '//' + location.host + '/Sklad/InSkladJson', function (data) {
        for (var i = 0; i < data.length; i++) {
            $("#AddSklad").append(
                ' <tr>'
                + '  <td>' + data[i].Name + '</td>'
                + '  <td>' + data[i].Count + '</td>'
                + '  <td>' + data[i].Time + '</td>'
                + '    </tr>'
            );
        }
    });
    $("#clearFormProduct").click(function (e) {
        e.preventDefault();
        $("#Product")[0].reset();
    });
});