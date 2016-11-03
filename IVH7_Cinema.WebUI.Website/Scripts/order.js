$(document).ready(function () {
    var totalAvailableSeats = $("#totalAvailableSeats").val();
    $("#availableSeats").text(totalAvailableSeats);

    $(".increase").click(function () {
        var tariffName = $(this).parent().parent().children(".amountOfTickets").attr("name");
        var totalOrderedTickets = $("#totalOrderedTickets").val();
        var amount = $(this).parent().parent().children(".amountOfTickets").val();
        var totalAvailableSeats = $("#totalAvailableSeats").val();

        if (tariffName != "Popcorn Arrangement") {
            if ((totalAvailableSeats - 1) >= 0) {
                var pricePerTicket = $(this).parent().parent().parent().children(".pricePerTicket").text().replace(/[^a-z0-9.\s]/gi, '');
                var totalPrice = $(this).parent().parent().parent().children(".totalPrice").text().replace(/[^a-z0-9.\s]/gi, '');
                var pricePerTicketNumber = parseFloat(pricePerTicket);
                var totalPriceNumber = parseFloat(totalPrice);
                var newTotal = totalPriceNumber + pricePerTicketNumber;
                amount++;
                $(this).parent().parent().children(".amountOfTickets").val(amount);
                $(this).parent().parent().parent().children(".totalPrice").text("€" + newTotal.toFixed(2));

                //Update totaalprijs
                var totalPriceOrder = $(".totalPriceOrder").text().replace(/[^a-z0-9.\s]/gi, '');
                var totalPriceOrderNumber = parseFloat(totalPriceOrder);
                var newTotalPriceOrder = totalPriceOrderNumber + pricePerTicketNumber;
                $(".totalPriceOrder").text("€" + newTotalPriceOrder.toFixed(2));
                $("#totalPriceOrder").val(newTotalPriceOrder.toFixed(2));

                //Update TotalOrderedTickets
                if (tariffName != "3D Bril" && tariffName != "Popcorn Arrangement") {
                    totalOrderedTickets++;
                    totalAvailableSeats--;
                    $("#totalOrderedTickets").val(totalOrderedTickets);
                    $("#totalAvailableSeats").val(totalAvailableSeats);
                    $("#availableSeats").text(totalAvailableSeats);
                }

                //Enable button
                var readTotalPrice = $(".totalPriceOrder").text().replace(/[^a-z0-9.\s]/gi, '');
                if (readTotalPrice != 0) {
                    $("#continue").removeAttr('disabled')
                } else if (newTotal == 0) {
                    $("#continue").attr('disabled', 'disabled');
                }
            }
        } else {
            if (totalOrderedTickets != amount) {
                if ((totalAvailableSeats - 1) >= 0) {
                    var pricePerTicket = $(this).parent().parent().parent().children(".pricePerTicket").text().replace(/[^a-z0-9.\s]/gi, '');
                    var totalPrice = $(this).parent().parent().parent().children(".totalPrice").text().replace(/[^a-z0-9.\s]/gi, '');
                    var pricePerTicketNumber = parseFloat(pricePerTicket);
                    var totalPriceNumber = parseFloat(totalPrice);
                    var newTotal = totalPriceNumber + pricePerTicketNumber;
                    amount++;
                    $(this).parent().parent().children(".amountOfTickets").val(amount);
                    $(this).parent().parent().parent().children(".totalPrice").text("€" + newTotal.toFixed(2));

                    //Update totaalprijs
                    var totalPriceOrder = $(".totalPriceOrder").text().replace(/[^a-z0-9.\s]/gi, '');
                    var totalPriceOrderNumber = parseFloat(totalPriceOrder);
                    var newTotalPriceOrder = totalPriceOrderNumber + pricePerTicketNumber;
                    $(".totalPriceOrder").text("€" + newTotalPriceOrder.toFixed(2));
                    $("#totalPriceOrder").val(newTotalPriceOrder.toFixed(2));

                    //Update TotalOrderedTickets
                    if (tariffName != "3D Bril" && tariffName != "Popcorn Arrangement") {
                        totalOrderedTickets++;
                        totalAvailableSeats--;
                        $("#totalOrderedTickets").val(totalOrderedTickets);
                        $("#totalAvailableSeats").val(totalAvailableSeats);
                        $("#availableSeats").text(totalAvailableSeats);
                    }

                    //Enable button
                    var readTotalPrice = $(".totalPriceOrder").text().replace(/[^a-z0-9.\s]/gi, '');
                    if (readTotalPrice != 0) {
                        $("#continue").removeAttr('disabled')
                    } else if (newTotal == 0) {
                        $("#continue").attr('disabled', 'disabled');
                    }
                }
            }
        }
    });

    $(".decrease").click(function () {
        var amount = $(this).parent().parent().children(".amountOfTickets").val();
        var totalAvailableSeats = $("#totalAvailableSeats").val();
        if (amount > 0) {
            var tariffName = $(this).parent().parent().children(".amountOfTickets").attr("name");
            var pricePerTicket = $(this).parent().parent().parent().children(".pricePerTicket").text().replace(/[^a-z0-9.\s]/gi, '');
            var totalPrice = $(this).parent().parent().parent().children(".totalPrice").text().replace(/[^a-z0-9.\s]/gi, '');
            var pricePerTicketNumber = parseFloat(pricePerTicket);
            var totalPriceNumber = parseFloat(totalPrice);
            var newTotal = totalPriceNumber - pricePerTicketNumber;
            amount--;
            $(this).parent().parent().children(".amountOfTickets").val(amount);
            $(this).parent().parent().parent().children(".totalPrice").text("€" + newTotal.toFixed(2));

            //Update totaalprijs
            var totalPriceOrder = $(".totalPriceOrder").text().replace(/[^a-z0-9.\s]/gi, '');
            var totalPriceOrderNumber = parseFloat(totalPriceOrder);
            var newTotalPriceOrder = totalPriceOrderNumber - pricePerTicketNumber;
            $(".totalPriceOrder").text("€" + newTotalPriceOrder.toFixed(2));
            $("#totalPriceOrder").val(newTotalPriceOrder.toFixed(2));

            //Update TotalOrderedTickets
            if (tariffName != "3D Bril" && tariffName != "Popcorn Arrangement") {
                var totalOrderedTickets = $("#totalOrderedTickets").val();
                totalOrderedTickets--;
                totalAvailableSeats++;
                $("#totalOrderedTickets").val(totalOrderedTickets);
                $("#totalAvailableSeats").val(totalAvailableSeats);
                $("#availableSeats").text(totalAvailableSeats);
            }

            //Enable button
            var readTotalPrice = $(".totalPriceOrder").text().replace(/[^a-z0-9.\s]/gi, '');
            if (readTotalPrice != 0) {
                $("#continue").removeAttr('disabled')
            } else if (newTotal == 0) {
                $("#continue").attr('disabled', 'disabled');
            }
        }
    });

    $('#continue').click(function(e) {
        //e.preventDefault();
        $('#TariffForm').submit();       
    });
});