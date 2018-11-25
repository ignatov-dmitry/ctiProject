//--------Преправы----------------
function okAddCondiments() {
    $('#succesSend').click();
    $('#inlineForm .close').click();
    Drop();
    okSkladAddCondiments();
    $("#form1")[0].reset();
    $("#clearFormCondiments").click();
    $('#okSend .success').css('display', 'block').text('Позиция ' + dt.Name + ' добавлена' );
    setInterval(function () {
        $('#okSend .close').click();
        $('#okSend .success').css('display', 'none');
    }, 50000);
}
function errorAddCondiments() {
    $('#succesSend').click();
    $("#clearFormCondiments").click();
    $('#okSend .error').css('display', 'block');
    setInterval(function () {
        $('#okSend .close').click();
        $('#okSend .error').css('display', 'none');
    }, 50000);
}
function okCond(data) {
    Drop();
    okSkladAddCondiments();
    $('#inlineForm .close').click();
    $("#form0")[0].reset();
    var dt = data;
    $('#okSend .success').css('display', 'block').text('Позиция ' + dt.Name + ' в количестве ' + dt.Count + 'кг.');
    $('#succesSend').click();
    setInterval(function () {
        $('#okSend .close').click();
    }, 50000);

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
function okSkladAddCondiments(data) {
    $("#AddCondiments").empty();
    $.getJSON(location.protocol + '//' + location.host + '/Sklad/CondimentsJson', function (data) {
        for (var i = 0; i < data.length; i++) {
            $("#AddCondiments").append(
                ' <tr>'
                + '  <td>' + data[i].Name + '</td>'
                + '  <td>' + data[i].Count + '</td>'
                + '    </tr>'
            );
        }
        $("#clearFormCondiments").click();
        $('#inlineForm .close').click();
    });
   

}
$(document).ready(function () {
    $.getJSON(location.protocol + '//' + location.host + '/Sklad/CondimentsJson', function (data) {
        for (var i = 0; i < data.length; i++) {
            $("#AddCondiments").append(
                ' <tr>'
                + '  <td>' + data[i].Name + '</td>'
                + '  <td>' + data[i].Count + '</td>'
                + '    </tr>'
            );
        }
    })

    $("#clearFormCondiments").click(function (e) {
        e.preventDefault();
        $("#Condiments")[0].reset();
    });
});