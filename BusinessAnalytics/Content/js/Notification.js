$(function () {
    $.ajaxSetup({ cache: false });
    $.get(location.protocol + '//' + location.host +'/Notification/ColNew', function (data) {
        $('#countNotNew').html(data + ' новых');
    });
    $.get(location.protocol + '//' + location.host +'/Notification/ColNew', function (data) {
        $('#countNot').html(data);
    });
    $.get(location.protocol + '//' + location.host +'/Notification/ViewL', function (data) {
        for (var i = 0; i < data.length; i++) {
            $('#sas').append(' <li class="list-group scrollable-container">'
                + ' <a href="' + location.origin + '/Notification/ViewId/' + data[i].id + '" class="list-group-item">'
                + '  <div class="media">'
                + '   <div class="media-left valign-middle"><i class="icon-cart3 icon-bg-circle bg-cyan"></i></div>'
                + '  <div class="media-body">'
                + ' <p class="notification-text font-small-3 text-muted">' + data[i].Message + '</p>'
                + ' </div>'
                + ' </div>'
                + ' </a>'
                + ' </li>');
        }
    });
});