
var cart = {
    init: function () {
        cart.regEvents();
    },
    regEvents: function () {
        $('#btnDatHang').off('click').on('click', function () {
            window.location.href = "/thanh-toan";
        });

        $('#btnCapNhat').off('click').on('click', function (e) {
            e.preventDefault();
            var listProduct = $('.txtQuantity');
            var cartList = [];
            $.each(listProduct, function (i, item) {
                cartList.push({
                    Quantity: $(item).val(),
                    product: {
                        ID: $(item).data('id')
                    }
                });
            });
            $.ajax({
                url: '/Cart/Update',
                data: { cartModel: JSON.stringify(cartList) },
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/gio-hang"
                    }
                }
            })
        });
        //$('#DeleteAll').off('click').on('click', function () {

        //    $.ajax({
        //        url: '/Cart/DeleteAll',
        //        dataType: 'json',
        //        type: 'POST',
        //        success: function (res) {
        //            if (res.status == true) {
        //                $('.count').text(0);
        //            }
        //        }
        //    })

        //});
        $('.btnXoa').off('click').on('click', function (e) {
            e.preventDefault();

            $.ajax({
                url: '/Cart/Delete',
                data: { id: $(this).data('id') },
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/gio-hang"
                    }
                }
            })

        });
        $('.item_add').off('click').on('click', function (e) {
            e.preventDefault();
            var count = parseInt($('.count').text());
            $.ajax({
                data: {
                    productId: $(this).data('id'),
                    Quantity: $(this).data('qty')
                },
                url: '/Cart/AddItem',
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        $('.count').text(count + 1);

                    }
                }
            })

        });
        $('#Thanhtoan').off('click').on('click', function (e) {
            e.preventDefault();
            var name = $('#txtName').val();
            var mobile = $('#txtPhone').val();
            var email = $('#txtEmail').val();
            var address = $('#txtAddress').val();

            $.ajax({
                data: {
                    shipName: name,
                    shipPhone: mobile,
                    shipAddress: address,
                    shipEmail: email
                },
                url: '/Cart/Payment',
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        alert('Đặt hàng thành công');
                        window.location.href = "/gio-hang";

                    }
                    else {
                        alert('Đặt hàng không thành công');
                    }
                }
            })

        });
    }
}
cart.init();