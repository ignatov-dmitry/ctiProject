$(document).ready(function () {
    var loc = location.pathname + "?logOn=true&enter=true";
    $("#LoginButton").attr("href", loc).click();

    notificationhub.client.RefreshUsersOnline = function () {
        Refresh();
    }
});




function LoginOnSuccess(result) {

    
    Scroll();

    //setTimeout("Refresh();", 5000);


    $('#txtMessage').keydown(function (e) {
        if (e.keyCode == 13) {
            e.preventDefault();
            $("#btnMessage").click();
            $('#txtMessage').val('');
        }
    });


    $("#btnMessage").click(function () {
        var text = $("#txtMessage").val();
        if (text) {

            
            var href = location.pathname;
            href = href + "?enter=false&logOn=true&chatMessage=" + encodeURIComponent(text);
            $("#ActionLink").attr("href", href).click();
            
        }
        $('#txtMessage').val('');
    });

    $("#btnLogOff").click(function () {


        var href = location.pathname;
        $("#ActionLink").attr("href", href).click();

        document.location.href = "Chat";
    });
}


function LoginOnFailure(result) {
    $("#Username").val("");
    $("#Error").text(result.responseText);
    setTimeout("$('#Error').empty();", 2000);
}


function Refresh() {
    var href = location.pathname +"?enter=false&logOn=true";
    $("#ActionLink").attr("href", href).click();
}


function ChatOnFailure(result) {
    $("#Error").text(result.responseText);
    setTimeout("$('#Error').empty();", 2000);
}


function ChatOnSuccess(result) {
    Scroll();
}


function Scroll() {
    var win = $('#Messages');
    var height = win[0].scrollHeight;
    win.scrollTop(height);
}

