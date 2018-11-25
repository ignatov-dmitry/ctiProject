$("#Price").on("click", "td a.tt", function (e) {
    e.preventDefault();
    var elem = $(this).first().parent().attr("Id").toString().split("_")[1];
    $("#price_" + elem + " a").css("display", "none");
    $("#price_" + elem + " .fs").css("display", "inline-block");
    $("#clc_" + elem).css("display", "inline-block");
});
$("#Price").on("click", "td div a", function (e) {
    e.preventDefault();
    var elem = $(this).attr("Id").toString().split("_")[1];
    $("#price_" + elem + " a").css("display", "inline-block");
    $("#price_" + elem + " .fs").css("display", "none");
});