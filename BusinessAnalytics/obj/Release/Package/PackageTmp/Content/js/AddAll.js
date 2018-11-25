$.ajaxSetup({ cache: false });

$(document).ready(function () {
    $.getJSON(location.protocol + '//' + location.host + '/Sklad/FinishTable', function (data) {
        for (var i = 0; i < data.length; i++) {
            $("#AddFinish").append(
                ' <tr>'
                + '  <td>' + data[i].Name + '</td>'
                + '  <td>' + data[i].Count + '</td>'
                + '  <td class="text-center"><a class="sec icon-trash3" data-target="#delete" data-toggle="modal" href="/Sklad/DeleteFinish/'+data[i].Id+'"> </a></td>'
                + '    </tr>'
            );
        }
    })
    
});