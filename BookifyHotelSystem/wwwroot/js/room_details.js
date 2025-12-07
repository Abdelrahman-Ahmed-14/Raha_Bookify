$(document).ready(function () {
    console.log('Room details script loaded');

    // Get price per night from the page
    const roomPriceText = $('#roomPrice').text().trim();
    const pricePerNight = parseFloat(roomPriceText) || 0;


    // Calculate nights and total price
    function calculateTotal() {
        const startDateVal = $('#StartDate').val();
        const endDateVal = $('#EndDate').val();

        if (startDateVal && endDateVal) {
            const startDate = new Date(startDateVal);
            const endDate = new Date(endDateVal);
            const diffTime = endDate - startDate;
            const nights = Math.ceil(diffTime / (1000 * 60 * 60 * 24));


            if (nights > 0) {
                const totalPrice = pricePerNight * nights;
                $('#numberOfNights').text(nights);
                $('#totalPrice').text('EGP ' + totalPrice.toFixed(2));
            } else {
                $('#numberOfNights').text('1');
                $('#totalPrice').text('EGP ' + pricePerNight.toFixed(2));
            }
        } else {
            // Default values when dates are not selected
            $('#numberOfNights').text('1');
            $('#totalPrice').text('EGP ' + pricePerNight.toFixed(2));
        }
    }

    // Update when dates change
    $('#StartDate').on('change input', function () {

        // Update EndDate minimum when StartDate changes
        const startDate = new Date($(this).val());
        const minEndDate = new Date(startDate);
        minEndDate.setDate(minEndDate.getDate() + 1);
        $('#EndDate').attr('min', minEndDate.toISOString().split('T')[0]);

        // If EndDate is before new minimum, update it
        const currentEndDate = $('#EndDate').val();
        if (currentEndDate && new Date(currentEndDate) <= startDate) {
            $('#EndDate').val(minEndDate.toISOString().split('T')[0]);
        }

        calculateTotal();
    });

    $('#EndDate').on('change input', function () {
        console.log('End Date changed:', $(this).val());
        calculateTotal();
    });

    // Initial calculation
    calculateTotal();
});