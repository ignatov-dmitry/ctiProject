function initBar() {
    var COLORS = [
        '#4dc9f6',
        '#f67019',
        '#f53794',
        '#537bc4',
        '#acc236',
        '#166a8f',
        '#00a950',
        '#58595b',
        '#8549ba',
        '#4852f6',
        '#df5019',
        '#f587fd',
        '#537bc4',
        '#acc236',
        '#166a8f',
        '#005240',
        '#585482',
        '#854dd2'
    ];

    var ctx = $("#bar-chart");

    var chartOptions = {
        elements: {
            rectangle: {
                borderWidth: 2,
                borderColor: 'rgb(0, 255, 0)',
                borderSkipped: 'left'
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
            text: 'Chart.js Horizontal Bar Chart'
        }
    };

    var data_bar = [];
    var aa = category.TypeProduct;
    var a = aa.length;
    for (var i = 0; i < a; i++) {
        data_bar.push({
            label: category.TypeProduct[i],
            data: [category.Count[i]],
            backgroundColor: COLORS[i],
            hoverBackgroundColor: COLORS[i],
            borderColor: "transparent"
        });
    }
    var chartData = {
        labels: ["Остатки"],
        datasets: data_bar
    };

    
    var config = {
        type: 'horizontalBar',
        options : chartOptions,
        data : chartData
    };

    // Create the chart
    var lineChart = new Chart(ctx, config);

}
