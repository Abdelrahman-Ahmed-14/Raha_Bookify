$(document).ready(function () {
    // Set minimum dates
    const today = new Date().toISOString().split('T')[0];
    $('.cart-start').attr('min', today);

    // Update end date min when start date changes
    $('.cart-start').on('change', function () {
        const $row = $(this).closest('tr');
        const startDate = $(this).val();
        const $endInput = $row.find('.cart-end');

        if (startDate) {
            const minEndDate = new Date(startDate);
            minEndDate.setDate(minEndDate.getDate() + 1);
            $endInput.attr('min', minEndDate.toISOString().split('T')[0]);

            if ($endInput.val() && $endInput.val() <= startDate) {
                $endInput.val(minEndDate.toISOString().split('T')[0]);
            }
        }
        updateRowTotal($row);
    });

    // Update total when end date changes
    $('.cart-end').on('change', function () {
        const $row = $(this).closest('tr');
        updateRowTotal($row);
    });

    function updateRowTotal($row) {
        const startDate = $row.find('.cart-start').val();
        const endDate = $row.find('.cart-end').val();
        const pricePerNight = parseFloat($row.data('price'));

        if (startDate && endDate) {
            const start = new Date(startDate);
            const end = new Date(endDate);
            const nights = Math.ceil((end - start) / (1000 * 60 * 60 * 24));

            if (nights > 0) {
                const total = nights * pricePerNight;
                $row.find('.cart-nights strong').text(nights);
                $row.find('.cart-total strong').text('EGP ' + total.toLocaleString());
                updateGrandTotal();
            }
        }
    }

    function updateGrandTotal() {
        let totalNights = 0;
        let totalAmount = 0;

        $('.cart-row').each(function () {
            const nights = parseInt($(this).find('.cart-nights strong').text()) || 0;
            const totalText = $(this).find('.cart-total strong').text().replace(/[^0-9.]/g, '');
            const total = parseFloat(totalText) || 0;

            totalNights += nights;
            totalAmount += total;
        });

        $('#totalNights').text(totalNights);
        $('#subtotalAmount').text(totalAmount.toLocaleString());
        $('#totalAmount').text(totalAmount.toLocaleString());
    }

    // Initialize totals on page load
    $('.cart-row').each(function () {
        updateRowTotal($(this));
    });
});
