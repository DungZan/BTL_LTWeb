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
$(document).on('change', '#all', function () {
    let isChecked = $(this).is(':checked');
    $('.site-blocks-table tbody tr').each(function () {
        $(this).find('.product-checkbox').prop('checked', isChecked);
    });
    updateTotalAmount();
});
$(document).on('change', '.product-checkbox', function () {
    let isCheckAll = true;
    $('.site-blocks-table tbody tr').each(function () {
        if (!$(this).find('.product-checkbox').prop('checked')) {
            isCheckAll = false;
        }
    });

    $('#all').prop('checked', isCheckAll);
    updateTotalAmount();
});



function updateTotalAmount() {
    let totalAmount = 0;
    $('.product-checkbox:checked').each(function () {
        let row = $(this).closest('tr');
        let price = parseFloat(row.find('td:eq(5)').text()); 
        let quantity = parseInt(row.find('input:eq(2)').val());
        totalAmount += price * quantity; 
    });

    $('.total-amount').text(totalAmount);
}

function updateTotal(row) {
    let price = parseFloat(row.find('td:eq(5)').text());
    let quantity = parseInt(row.find('input:eq(2)').val()); 
    let total = price * quantity;
    row.find('td:eq(7)').text(total);

    let grandTotal = 0;
    $('.site-blocks-table tbody tr').each(function () {
        if ($(this).find('#check').prop('checked')) {
            let rowTotal = parseFloat($(this).find('td:eq(7)').text()) || 0; 
            grandTotal += rowTotal;
        }
    });
    $('.total-amount').text(grandTotal);
}
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
            $('#items').html(response);
        },
        error: function (xhr, status, error) {
            $('#cart-container').html('<h2>Không có sản phẩm nào trong giỏ hàng</h2>')
        }
    });
}