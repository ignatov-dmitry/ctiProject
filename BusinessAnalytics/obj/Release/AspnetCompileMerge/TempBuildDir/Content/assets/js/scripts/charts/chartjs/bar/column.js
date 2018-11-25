function columnBar() {
    var ctx = $("#column-chart");
    var chartOptions = {
        elements: {
            rectangle: {
                borderWidth: 2,
                borderColor: 'rgb(0, 255, 0)',
                borderSkipped: 'bottom'
            }
        },
        responsive: true,
        maintainAspectRatio: false,
        responsiveAnimationDuration:500,
        legend: {
            position: 'top',
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
                }
            }]
        },
        title: {
            display: false,
            text: 'Chart.js Bar Chart'
        }
    };

    // Chart Data
    var chartData = {
        labels: unique(sales.NameClient),
        datasets: [{
            label: "Общая сумма",
            data: sumClients(sales.NameClient, totalSumClients(sales.Price, sales.Count)),
            backgroundColor: "#673AB7",
            hoverBackgroundColor: "rgba(103,58,183,.9)",
            borderColor: "transparent"
        }]
    };

    var config = {
        type: 'bar',

        // Chart Options
        options : chartOptions,

        data : chartData
    };
    console.log(sumClients(sales.NameClient, sales.Price));
    // Create the chart
    var lineChart = new Chart(ctx, config);
}