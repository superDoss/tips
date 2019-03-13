function renderGraph(data)
{
    var myChart = Highcharts.chart('tips-per-dey', {
        chart: {
            type: 'spline'
        },
        title: {
            text: 'TipsPerDay'
        },
        xAxis: {
            categories: Data.Dates
        },
        yAxis: {
            title: {
                text: 'Amount'
            }
        },
        series: {
            data : Data.Sum
        }
    });
}

document.addEventListener('DOMContentLoaded', function () {
    $.get( "api/stat", renderGraph(res.data));
})