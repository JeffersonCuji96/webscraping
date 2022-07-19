const url = "https://apiwebscraping.azurewebsites.net/api/Scraping";

function GetReporte() {
    $("#info-scrap").hide();
    $(".loading").removeClass("d-none");
    fetch(url).then(res => res.json()).then(data => {
            data.map((x) => x.ValorMercado = FormatValorMercado(x.ValorMercado));
            $(".loading").fadeIn(1000).addClass("d-none");
            CreateBarChart(data);
        });
}

function CreateBarChart(dataScraping) {
    const ctx = document.getElementById('chartReport').getContext('2d');
    const myChart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: dataScraping.map((x) => x.Nombre),
            datasets: [
                {
                    label: 'Valor de Mercado',
                    data: dataScraping,
                    backgroundColor: 'rgba(75, 174, 51, 0.23)',
                },
            ],
        },
        options: {
            indexAxis: 'y',
            parsing: {
                xAxisKey: 'ValorMercado',
                yAxisKey: 'Nombre',
            },
            scales: {
                y: {
                    beginAtZero: true,
                },
                x: {
                    min: 20,
                    ticks: {
                        callback: function (value) {
                            return value + ' M €';
                        },
                        stepSize: 20,
                        maxRotation: 90,
                        minRotation: 90,
                    },
                },
            },
            plugins: {
                legend: {
                    labels: {
                        font: {
                            weight: 'bold',
                            size: 14,
                        },
                    },
                },
                tooltip: {
                    titleFont: {
                        size: 16,
                    },
                    callbacks: {
                        label: function (context) {
                            return (context.dataset.label + ': ' + context.parsed.x + ' Millones €');
                        },
                        afterLabel: function (context) {
                            return (
                                'Posición: ' + context.raw['Posicion'] + '\n' +
                                'Club: ' + context.raw['Club'] + '\n' +
                                'Edad: ' + context.raw['Edad']
                            );
                        },
                    },
                    titleAlign: 'center',
                    displayColors: false,
                },
            },
        },
    });
    $(".chartWrapper").removeClass("d-none");
}
function FormatValorMercado(value) {
    return parseInt(value.match(/(\d+)/));
}
    
    