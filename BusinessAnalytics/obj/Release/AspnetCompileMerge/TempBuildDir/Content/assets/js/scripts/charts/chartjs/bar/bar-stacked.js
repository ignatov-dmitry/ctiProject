function initBar_stacked(){

    // Get the context of the Chart canvas element we want to select
    var ctx = $("#bar-stacked");

    // Chart Options
    var chartOptions = {
        title:{
            display:false,
            text:"Chart.js Bar Chart - Stacked"
        },
        tooltips: {
            mode: 'label'
        },
        responsive: true,
        maintainAspectRatio: false,
        responsiveAnimationDuration:500,
        scales: {
            xAxes: [{
                stacked: true,
                display: true,
                gridLines: {
                    color: "#f3f3f3",
                    drawTicks: false,
                },
                scaleLabel: {
                    display: true,
                }
            }],
            yAxes: [{
                stacked: true,
                display: true,
                gridLines: {
                    color: "#f3f3f3",
                    drawTicks: false,
                },
                scaleLabel: {
                    display: true,
                }
            }]
        }
    };

    // Chart Data
    var chartData = {
        labels: ["Январь", "Февраль", "Март", "Апрель", "Май"],
        datasets: [{
            label: "Процессоры",
            data: [400, 320, 250, 180, 92],
            backgroundColor: "#00BFA5",
            hoverBackgroundColor: "rgba(0,191,165,.8)",
            borderColor: "transparent"
        }, {
            label: "Видеокарты",
            data: [502, 400, 358, 240, 120],
            backgroundColor: "#1DE9B6",
            hoverBackgroundColor: "rgba(29,233,182,.8)",
            borderColor: "transparent"
        },
        {
            label: "Материнские платы",
            data: [440, 390, 350, 282, 100,],
            backgroundColor: "#64FFDA",
            hoverBackgroundColor: "rgba(100,255,218,.8)",
            borderColor: "transparent"
        }]
    };

    var config = {
        type: 'horizontalBar',

        // Chart Options
        options : chartOptions,

        data : chartData
    };

    // Create the chart
    var lineChart = new Chart(ctx, config);
}