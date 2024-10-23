$(document).ready(function () {
    $(document).on('click', '.js-btn-plus', function () {
        let input = $(this).closest('tr').find('input:eq(2)');
        updateTotal(input.closest('tr'));
    });

    $(document).on('click', '.js-btn-minus', function () {
        let input = $(this).closest('tr').find('input:eq(2)');
        let value = parseInt(input.val());
        if (value > 0) {
            updateTotal(input.closest('tr'));
        } else {
            input.val(value + 1);
        }
    });


});

$(document).on('change', '.product-checkbox', function () {
    let isCheckALl = true;
    $('.site-blocks-table tbody tr').each(function () {
        if (!$(this).find('#check').prop('checked')) {
            isCheckALl = false;
        }
    });
    $('#all').prop('checked', isCheckALl);
    updateTotalAmount();
});

$(document).on('change', '#all', function () {
    let isChecked = $(this).is(':checked');
    let grandTotal = 0;
    $('.site-blocks-table tbody tr').each(function () {
        $(this).find('#check').prop('checked', isChecked);
    });


    if (isChecked) {
        let grandTotal = 0;
        $('.site-blocks-table tbody tr').each(function () {
            if ($(this).find('#check').prop('checked')) {
                let rowTotal = parseFloat($(this).find('td:eq(7)').text()) || 0; // Lấy tổng tiền của hàng
                grandTotal += rowTotal; // Cộng vào grand total
            }
        });
        $('#total').text(grandTotal.toString());
    }
    else
        $('.total-amount').text('0.00');
});

function updateAllTotals(isChecked) {
    let grandTotal = 0;
    $('.site-blocks-table tbody tr').each(function () {
        $(this).find('#check').prop('checked', isChecked); // K
        let price = parseFloat($(this).find('td:eq(5)').text()) || 0; // Đảm bảo giá trị số
        let quantity = parseInt($(this).find('input:eq(2)').val()) || 0; // Đảm bảo giá trị số
        grandTotal += price * quantity;
    });
}

function updateTotalAmount() {
    let totalAmount = 0;
    $('.product-checkbox:checked').each(function () {
        let row = $(this).closest('tr');
        let price = parseFloat(row.find('td:eq(5)').text()); // Giá sản phẩm
        let quantity = parseInt(row.find('input:eq(2)').val()); // Số lượng
        totalAmount += price * quantity; // Tính tổng
    });

    $('.total-amount').text(totalAmount.toFixed(2));

}

function updateTotal(row) {
    // Lấy giá trị của price và quantity
    let price = parseFloat(row.find('td:eq(5)').text());
    let quantity = parseInt(row.find('input:eq(2)').val()); // Xác định đúng input
    let total = price * quantity;
    row.find('td:eq(7)').text(total);

    // Tính tổng cho tất cả hàng
    let grandTotal = 0;
    $('.site-blocks-table tbody tr').each(function () {
        if ($(this).find('#check').prop('checked')) {
            let rowTotal = parseFloat($(this).find('td:eq(7)').text()) || 0; // Lấy tổng tiền của hàng
            grandTotal += rowTotal; // Cộng vào grand total
        }
    });
    $('.total-amount').text(grandTotal.toFixed(2));

}


// create ajax call api remove item from cart follow email and product id
$(document).on('click', '#remove', function () {
    let row = $(this).closest('tr');
    let cartId = parseInt(row.find('input:eq(1)').val());
    $.ajax({
        url: '/api/cartapi/remove',
        type: 'DELETE',
        contentType: 'application/json',
        data: JSON.stringify(cartId),
        success: function (response) {
            updateCart();
        },
        error: function (xhr, status, error) {
            alert(xhr.status + " " + cartId);
        }
    });
});

function updateCart() {
    let id = parseInt($('#customer').val());

    $.ajax({
        url: `/items?makhachhang=${id}`,
        type: 'GET',
        success: function (response) {
            // Xử lý kết quả trả về
            $('#items').html(response);
            updateTotalAmount();
        },
        error: function (xhr, status, error) {
            $('#cart-container').html('<h2>Không có sản phẩm nào trong giỏ hàng</h2>')
        }
    });
}