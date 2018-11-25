

$(function () {
    $.ajaxSetup({ cache: false });
    $(".del").click(function (e) {
        e.preventDefault();
        $.get(this.href, function (data) {
            $('#inlineForm').html(data);
        });
    });

    $.ajaxSetup({ cache: false });
    $(".edt").click(function (e) {
        e.preventDefault();
        $.get(this.href, function (data) {
            $('#inlineForm').html(data);
        });
    });
    $.ajaxSetup({ cache: false });
    $(".edtTask").click(function (e) {
        e.preventDefault();
        $.get(this.href, function (data) {
            $('#edit').html(data);
        });
    });
})

$(function () {
    $("[data-autocomplete-source]").each(function () {
        var target = $(this);
        console.log(target);
        target.autocomplete({ source: target.attr("data-autocomplete-source") });
    });
});


var category = {};
var sales = {};
var data_length = 0;
var data_length_sales = 0;
category.TypeProduct = [];
category.Count = [];
sales.NameClient = [];
sales.Tovar = [];
sales.Price = [];
sales.Count = [];
sales.Date = [];




if (location.href == location.protocol + '//' + location.host + '/Sklad/Chart') {

    $.getJSON(location.protocol + '//' + location.host + '/Sklad/json', function (data) {
        data_length = data.length;
        for (var i = 0; i < data.length; i++) {
            category.TypeProduct.push(data[i].NameProduct);
            category.Count.push(data[i].Count);
        }
        initBar();
        initBar_stacked();
    });

}

if (location.href == location.protocol + '//' + location.host + '/Sales/Chart') {

    $.getJSON(location.protocol + '//' + location.host + '/Sales/json', function (data) {
        data_length_sales = data.length;
        for (var i = 0; i < data.length; i++) {
            sales.NameClient.push(data[i].NameClient);
            sales.Tovar.push(data[i].Tovar);
            sales.Price.push(data[i].Price);
            sales.Count.push(data[i].Count);
            sales.Date.push(data[i].Date);
        }
        columnBar();
        initBarLine();
    });


}
// Убирает повторы
function unique(arr) {
    var result = [];
    next:
    for (var i = 0; i < arr.length; i++) {
        var str = arr[i];
        for (var j = 0; j < result.length; j++) {
            if (result[j] == str)
                continue next;
        }
        result.push(str);
    }

    return result;
}

function sumClients(array, array2) {
    arr = array.slice();
    arr2 = array2.slice();
    var result = [];
    var resultSum = [];
    var totalSum = 0;
    next:
    for (var i = 0; i < arr.length; i++) {
        if (arr[i] != undefined)
            var str = arr[i];
        else
            continue next;
        for (var j = 0; j < arr.length; j++) {
            if (arr[j] == str && str != undefined) {

                totalSum += arr2[j];
                delete arr[j];
                delete arr2[j];
            }
        }

        resultSum.push(totalSum);
        totalSum = 0;
        result.push(str);
    }
    return resultSum;
}


function sumDate(array, array2) {
    arr = array.slice();
    arr2 = array2.slice();
    var result = [];
    var resultSum = [];
    next:
    for (var i = 0; i < arr.length; i++) {
        if (arr[i] != undefined)
            var str = arr[i];
        else
            continue next;
        for (var j = 0; j < arr.length; j++) {
            if (arr[j] == str && str != undefined) {
                resultSum.push(arr2[j]);
                delete arr[j];
                delete arr2[j];
            }
        }

        result.push(resultSum);
        resultSum = [];

    }
    return result;
}

function totalSumClients(arr1, arr2) {
    var result = [];
    for (var i = 0; i < arr1.length; i++) {
        result.push(arr1[i] * arr2[i]);
    }
    return result;
}

var count = $('.event').length;
var maxHeight = 0;
for (var i = 0; i < count; i++) {
    var height = $($('.event')[i]).height();
    if (maxHeight < height)
        maxHeight = height;
}

$(".event").height(maxHeight);