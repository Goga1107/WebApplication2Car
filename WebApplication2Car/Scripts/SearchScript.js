<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
<script>
    $(document).ready(function () {
        $("#searchForm input").on("input", function () {
            searchCars();
        });
    });

    function searchCars() {
        var manufacturer = $("#manufacturer").val();
        var model = $("#model").val();
        var year = $("#year").val();
        var engine = $("#engine").val();
        var price = $("#price").val();

        $.ajax({
            url: '/Cars/SearchCars',
            type: 'GET',
            data: {
                manufacturer: manufacturer,
                model: model,
                year: year,
                engine: engine,
                price: price
            },
            success: function (data) {
                displayCars(data);
            }
        });
    }

    function displayCars(cars) {
        var resultsContainer = $("#results");
        resultsContainer.empty();

        cars.forEach(function (car) {
            var carElement = $("<div></div>").text(`${car.Manufacturer} ${car.Model} (${car.Year}) - $${car.Price}`);
            resultsContainer.append(carElement);
        });
    }
</script>
