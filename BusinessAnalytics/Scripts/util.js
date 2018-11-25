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
    notificationhub.client.Prih = function (ves, productName) {
        $('#okSend').html(
            `<div class="modal-dialog" role="document">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">×</span>
                                        </button>
                                        <label class="modal-title text-text-bold-600" id="myModalLabel33">Успешно</label>
                                    </div>
                                    <h4 class="success text-center">`+ productName + `: +` + ves +` кг </h4>
                                </div>
                            </div>`
        );
        $('#succesSend').click();
    }
    notificationhub.client.RefreshUsersOnline = function () {
        Refresh();
    }
});
function Refresh() { }
$.connection.hub.start();