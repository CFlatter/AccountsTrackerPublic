$('.showAddEditAccountPartialBtn').click(function () {
    var data = {
        id: $(this).data('id')
    };

    $.ajax({
        url: 'Account/AddEditAccount',
        type: 'POST',
        data: data,
        success: function (result) {
            $('#addEditAccountPartial').show();
            $('#addEditAccountPartial').html(result);
            $('#closeAddEditAccountPartial').click(function () {
                $('#addEditAccountPartial').hide();
            })
        },
        error: function (xhr, status, error) {
            var err = xhr.responseText;
        }
    })
})

$('.showAddEditAccountLogPartialBtn').click(function () {
    var data = {
        accountlogid: $(this).data('accountlogid')
    };

    $.ajax({
        url: 'Account/AddEditAccountLog',
        type: 'POST',
        data: data,
        success: function (result) {
            $('.modal-content').html(result);
            $('#addAccountLogPartial').modal('show')

            var date = new Date();
            var day = ("0" + date.getDate()).slice(-2);
            var month = ("0" + (date.getMonth() + 1)).slice(-2);
            var today = date.getFullYear() + "/" + (month) + "/" + (day);
            $('.date').val(today);
        },
        error: function (xhr, status, error) {
            var err = xhr.responseText;
        }

    })
})


const ctx = document.getElementById('accountChart').getContext('2d');

$(document).ready(function () {

    var chartOptions = {
        legend: {
            display: true,
            position: 'top',
            labels: {
                boxWidth: 80,
                fontColor: 'black'
            }
        },
        scales: {
            xAxes: {
                gridLines: {
                    display: true
                },
                time: {
                    minUnit: 'month'
                },
                ticks: {
                    display: true,
                    reverse: true,
                    fontSize: 12
                }
            }
        }
    };

    let chartData = {
        labels: [],
        datasets: []
    };

    const accountsChart = new Chart(ctx, {
        type: 'line',
        data: chartData,
        options: chartOptions
    });

    $.ajax({
        url: 'Account/GetChartData',
        type: 'GET',
        success: function (result) {

            for (let i in result) {

                let iterator = 1;

                function generateLightColorHsl() {
                    const hue = (1 + Math.random() * 3.6) * 137.508; // use golden angle approximation
                    return `hsl(${hue},100%,50%)`;
                }

                function generateDarkColorHsl() {
                    const hue = (Math.random() * 3.6) * 137.508; // use golden angle approximation
                    return `hsl(${hue},50%,50%)`;
                }

                let chartLineColour;

                if (iterator % 2 == 0) {
                   chartLineColour = generateLightColorHsl(Math.floor(Math.random() * 9), 10);
                } else {
                    chartLineColour = generateDarkColorHsl(Math.floor(Math.random() * 9), 10);
                }
                

                let newData = {
                    label: [i].toString(),
                    data: [] ,
                    lineTension: 0,
                    fill: false,
                    borderColor: chartLineColour
                }

                let date;
                let month;

                for (let j in result[i]) {

                    date = new Date(Date.parse(result[i][j]['logDate']));
                    month = date.toLocaleString('default', { month: 'short' });
                    year = date.toLocaleString('default', { year: '2-digit' });

                    let xAxisData = month + '/' + year

                    newData.data.push({x:xAxisData, y: result[i][j].accountBalance });

  
                }


                
                accountsChart.data.datasets.push(newData);
                
                iterator++;
            }
            
            accountsChart.update();





        }
    })
});


