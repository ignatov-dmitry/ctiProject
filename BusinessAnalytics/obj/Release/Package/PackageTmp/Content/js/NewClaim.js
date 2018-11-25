var isd = 0;
var sum = 0;
$(document).ready(function () {
    $('body').removeClass('menu-expanded');
    $('body').addClass('menu-collapsed');
});


$(function () {

    $('.check').on('click',function (e) {
        var nbr = $(this).attr('alt');
        var idProd = $('#tar_' + nbr + ' option:selected').val();
        var NameProd = " ";
        var Count = $('#col_' + nbr).val();
        var Price = 3;
        var countOld = 0;
        $("#col_" + nbr).val('');
        $(function () {
            $.get(location.protocol + '//' + location.host + '/Claim/CountFinish/' + idProd, function (data) {
                Price = data;

            }).done(function () {
                $.get(location.protocol + '//' + location.host + '/Claim/NameProd/' + idProd, function (dataName) {
                    NameProd = dataName;

                }).done(function () {
                    $.get(location.protocol + '//' + location.host + '/Claim/CountProdOld/' + idProd, function (dates) {
                        countOld = dates;
                    }).done(function () {
                        var dts = countOld - Count;
                        if (dts <= 0) {
                            console.log(dts);
                            $('#osd').click();
                            $('#inlineForm .success').css('display', 'block').text(`Товар не добавлен, осталось на складе ` + countOld);
                            setInterval(function () {
                                $('#inlineForm .close').on('click');
                            }, 30000);
                        }
                        else {
                            $('#AddClaims').append(' <tr>'
                                + '  <td>' + NameProd + '</td>'
                                + '  <td>' + Count + '</td>'
                                + '  <td>' + Price + ' р.</td>'
                                + '  <td class="text-center"><a class="icon-cross2" id="clear" href="#"> </a></td>'
                                + '    </tr>');
                            sum += (Count * Price);
                            $("#Sub").css('display', 'inline-block');
                            $('.ddd').css('display', 'block');
                            $('.Sum').text(sum + ' р.');
                            $('#AddNewClaim').append(`
            <input type="text" style="visibility: hidden;width: 0;height: 0;" name="FinishProductId[`+ isd + `]" value="` + idProd + `"/>
                    <input type="text"  style="visibility: hidden;width: 0;height: 0;" name="Count[`+ isd + `]"  value="` + Count + `"/>`);
                            isd = isd + 1;
                        }
                        countOld = 0;
                    });
                });
            });
            $('#Summ').val(sum);
        });
    });


});

$('body').on('click', '#clear', function (e) {
    e.preventDefault();
    $(this).closest('tr').remove();
});

$(document).on('ready', function () {
    var dtda = '';
    var count = $('.ad').length;
    for (var i = 0; i < count; i++) {
        var str = '#tar_' + i;
        var d = $('#st_' + i + ' .vidFin option:selected').val();
        $.getJSON(location.protocol + '//' + location.host + '/Claim/ViewTara/' + d, function (s) {
            for (var j = 0; j < s.length; j++) {
                $(str).append('<option value="' + s[j].Id + '">' + s[j].Name + '</option>');
            }
        });
    }

});
$('.vidFin').on('change', function () {
    var count = $(this).attr('alt');
    $('#tar_' + count).empty();
    $.getJSON(location.protocol + '//' + location.host + '/Claim/ViewTara/' + $(this).val(), function (data) {
        for (var i = 0; i < data.length; i++) {
            console.log('#tar_' + count);
            $('#tar_' + count).append('<option value="' + data[i].Id + '">' + data[i].Name + '</option>');
        }

    });
});
function errorNewClaim() {
    $('#succesSend').click();
    $("#clearFormCondiments").click();
    $('#okSend .error').css('display', 'block');
    setInterval(function () {
        $('#okSend .close').click();
        $('#okSend .error').css('display', 'none');
    }, 50000);
}
function okNewClaim(data) {
    $('#inlineForm .close').click();
    $("#form0")[0].reset();
    $('#AddClaims').empty();
    $('#Sub').css('display', 'none');
    $('.ddd').css('display', 'none');
    $('#AddNewClaim').empty();
    var dt = data;
    $('#okSend .success').css('display', 'block').text('Заявка отправлена на одобрение менеджеру');
    $('#succesSend').click();
    setInterval(function () {
        $('#okSend .close').click();
    }, 50000);
    isd = 0;
}
