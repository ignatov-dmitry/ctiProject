
var arr = [];
$.getJSON(location.href + "/json", function (data) {
    for (var i = 0; i < data.length; i++) {
        arr.push(data[i].Price);
    }
});

console.log(arr);
console.log(location.href + "/json");

$(window).on("load", function(){


    var ctx = $("#area-chart");

    // Chart Options
    var chartOptions = {
        responsive: true,
        maintainAspectRatio: false,
        legend: {
            position: 'bottom',
        },
        hover: {
            mode: 'label'
        },
        scales: {
            xAxes: [{
                display: true,
                gridLines: {
                    color: "#f3f3f3",
                    drawTicks: false,
                },
                scaleLabel: {
                    display: true,
                    labelString: 'Месяц'
                }
            }],
            yAxes: [{
                display: true,
                gridLines: {
                    color: "#f3f3f3",
                    drawTicks: false,
                },
                scaleLabel: {
                    display: true,
                    labelString: 'Value'
                }
            }]
        },
        title: {
            display: true,
            text: 'Продажи'
        }
    };
    
    

    var chartData = {
        labels: ["Январь", "Февраль", "Март", "Апрель", "Май", "Июнь", "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь"],
        datasets: [{
            label: "2016 год",
            data: arr,
            backgroundColor: "rgba(201,187,174,.3)",
            borderColor: "transparent",
            pointBorderColor: "#C9BBAE",
            pointBackgroundColor: "#FFF",
            pointBorderWidth: 2,
            pointHoverBorderWidth: 2,
            pointRadius: 4,
        }, {
            label: "2017 год",
            data: [2000, 1000, 1050, 1070, 1000, 2000, 7040, 1000, 1050, 2046, 1200, 5048],
            backgroundColor: "rgba(29,233,182,.6)",
            borderColor: "transparent",
            pointBorderColor: "#1DE9B6",
            pointBackgroundColor: "#FFF",
            pointBorderWidth: 2,
            pointHoverBorderWidth: 2,
            pointRadius: 4,
        }]
    };
   
    // Chart Data
    var config = {
        type: 'line',

        // Chart Options
        options : chartOptions,

        // Chart Data
        data : chartData
    };

    // Create the chart
    var areaChart = new Chart(ctx, config);

});