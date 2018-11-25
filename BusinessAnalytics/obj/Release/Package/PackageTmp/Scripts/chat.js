$(document).ready(function () {
    var href = "/Chat?logOn=true";
    $("#LoginButton").attr("href", href).click();



    notificationhub.client.RefreshUsersOnline = function () {
        Refresh();
    }
   
});


function LoginOnSuccess(result) {
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


                        var href = "/Chat?user=" + encodeURIComponent($("#Username").text());
                        href = href + "&chatMessage=" + encodeURIComponent(text);
                        $("#ActionLink").attr("href", href).click();
                        console.log(href);
                }
                $('#txtMessage').val('');
        });

        $("#btnLogOff").click(function () {


                var href = "/Chat?user=" + encodeURIComponent($("#Username").text());
                href = href + "&logOff=true";
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
    var href = "/Chat?user=" + encodeURIComponent($("#Username").text());
    $("#ActionLink").attr("href", href).click();
}


function ChatOnFailure(result) {
        $("#Error").text(result.responseText);
        setTimeout("$('#Error').empty();", 2000);
}


function ChatOnSuccess(result) {}

