function initBarLine() {
    var line_data = {};
    line_data = $.ajax(location.protocol + '//' + location.host + '/Sales/Sl', {
        async: false
    }).responseJSON;

    var mounths = [
        "Январь",
        "Февраль",
        "Март",
        "Апрель",
        "Май",
        "Июнь",
        "Июль",
        "Август",
        "Сентябрь",
        "Октябрь",
        "Ноябрь",
        "Декабрь"
    ];
    var ctx = $('#line-stacked-area');
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
                    labelString: 'Значение'
                }
            }]
        },
        title: {
            display: true,
            text: 'Продажи'
        }
    };
    var chartData = {
        labels: mounths,
        datasets: line_data
    };
    var config = {
        type: 'line',
        options: chartOptions,
        data: chartData
    };

    var stackedAreaChart = new Chart(ctx, config);
}