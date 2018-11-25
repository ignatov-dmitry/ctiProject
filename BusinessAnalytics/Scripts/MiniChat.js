
var link = "";
var usr = "";
$("#txtMessage1").css("display", "none");
$(document).ready(function () {




    /*$(".usr").click(function (e) {
        var usr = $(".usr").attr("href");
        var loc = location.pathname + "Chat/MiniChat/ab6ced60-6f9c-4650-9305-8c52f72b93d6?logOn=true&enter=true";
        
        
    });*/



    /*notificationhub.client.RefreshUsersOnline = function () {
        Refresh();
    }*/
});

function cursor() {
    $("#txtMessage1").focus();
    $('#userList').css('display', 'none');


}

function asd() {
    usr = $(this).attr("href");
    link = $(this).attr("href") + "?enter=true&logOn=true";
    $("#LoginButton1").attr("href", usr + "?enter=true&logOn=true").click();
    $("#scr").css("display", "none");
    $("#txtMessage1").css("display", "block");
    $('#userDialog').click();
    $('#back').toggleClass('showBack');
}

function asdd() {
    $("#LoginButton1").attr("href", usr + "?enter=true&logOn=true").click();

}

function LoginOnSuccess1() {
    Scroll1();
    $("#txtMessage1").css("display", "block");
    $('#txtMessage1').keydown(function (e) {
        if (e.keyCode == 13) {
            e.preventDefault();
            $("#btnMessage1").click();
            $('#txtMessage1').val('');
        }
    });


    $("#btnMessage1").click(function () {
        var text = $("#txtMessage1").val();
        link = usr + "?enter=true&logOn=true";
        if (text) {
            var href = link + "&chatMessage=" + encodeURIComponent(text);
            $("#ActionLink1").attr("href", href).click();
            link = "";
        }
        $('#txtMessage1').val('');
        $("#LoginButton1").click();
        $("#LoginButton1").click();
        var disabled = "disabled"
        $("#txtMessage1").attr("disabled", disabled);
    });

    $("#btnLogOff").click(function () {


        var href = location.pathname;
        $("#ActionLink1").attr("href", href).click();

        document.location.href = "Chat";
    });
}


function LoginOnFailure1(result) {
    $("#Username1").val("");
    $("#Error1").text(result.responseText);
    setTimeout("$('#Error1').empty();", 2000);
}


function Refresh1() {
    var href = location.pathname + "?enter=false&logOn=true";
    $("#ActionLink1").attr("href", href).click();
    debugger;
}


function ChatOnFailure1(result) {
    $("#Error1").text(result.responseText);
    setTimeout("$('#Error1').empty();", 2000);
}


function ChatOnSuccess1(result) {
    Scroll1();
}


function Scroll1() {
    var win = $('#scr');
    var ms = $('#view-msga');
    var height = ms[0].scrollHeight;
    win.scrollTop(height);
    $("#scr").css("display", "block");
}


$("#openChat").click(function () {
    $(".chat-body").fadeIn("slow");
    $("#openChat").css("display", "none");
});

$("#exit").click(function () {
    $(".chat-body").fadeOut("slow");
    $("#openChat").css("display", "block");
});