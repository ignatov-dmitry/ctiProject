var notificationhub = $.connection.notificationHub;

function soundMessage() {
    var audio = new Audio();
    audio.src = '/Scripts/Adara.ogg';
    audio.autoplay = true;
}
$(function () {
    notificationhub.client.UserConnect = function (name) {
        soundMessage();
        $("#hub").fadeIn(1000);
        $("#hub").text(name + " в сети");

        setTimeout(function () {
            $("#hub").fadeOut(1000);
        }, 4500);
    }

    notificationhub.client.UserSend = function (name) {
        $("#hub").fadeIn(1000);
        $("#hub").text(name + " отправил вам сообщение");
        soundMessage();
        setTimeout(function () {
            $("#hub").fadeOut(1000);
        }, 4500);
    }
    notificationhub.client.updt = function () {
        Refresh();
    }
    notificationhub.client.RefreshUsersOnline = function () {
        Refresh();
    }
});
function Refresh() { }
$.connection.hub.start();