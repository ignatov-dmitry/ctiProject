$.getJSON(location.protocol + '//' + location.host + '/Sklad/PackagingViewJson', function (data) {
    for (var i = 0; i < data.length; i++) {
        $("#AddPack").append(
            ' <tr>'
            + '  <td>' + data[i].Name + '</td>'
            + '  <td>' + data[i].Count + '</td>'
            + '    </tr>'
        );
    }
})
$('a[data-action="reloadPack"]').on('click', function () {
    var block_ele = $(this).closest('.card');
    $("#AddPack").empty();
    $.getJSON(location.protocol + '//' + location.host + '/Sklad/PackagingViewJson', function (data) {
        for (var i = 0; i < data.length; i++) {
            $("#AddPack").append(
                ' <tr>'
                + '  <td>' + data[i].Name + '</td>'
                + '  <td>' + data[i].Count + '</td>'

                + '    </tr>'
            );
        }
    })
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
function okPack(data) {
    $('#inlineForm .close').click();
    console.log(data.Name);
    $("#form2")[0].reset();
    $('#okSend .success').css('display', 'block').text('Позиция ' + data.Name + ' добавлена');
    $('#succesSend').click();
    setInterval(function () {
        $('#okSend .close').click();
    }, 50000);
}
function okPackUpdate(data) {
    $('#inlineForm .close').click();
    $('#okSend .success').css('display', 'block').text('Позиция ' + data.Name + ' в количестве ' + data.Count + ' шт.');
    $('#succesSend').click();
    setInterval(function () {
        $('#okSend .close').click();
    }, 50000);
}
function errorPack() {
    $('#inlineForm .close').click();
    $('.succesSend .error').css('display', 'block');
    setInterval(function () {
        $('#okSend .close').click();
    }, 50000);
}
function okSkladPack() {
    
    $("#AddPack").empty();
    $.getJSON(location.protocol + '//' + location.host + '/Sklad/PackagingViewJson', function (data) {
        for (var i = 0; i < data.length; i++) {
            $("#AddPack").append(
                ' <tr>'
                + '  <td>' + data[i].Name + '</td>'
                + '  <td>' + data[i].Count + '</td>'
                + '    </tr>'
            );
        }
    });
    
}