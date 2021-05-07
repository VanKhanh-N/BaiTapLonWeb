

var statusController = {
    init: function () {
        statusController.registerevents();
    },
    registerevents: function () {
        $('.btn-active').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: "/admin/product/changestatus",
                data: { id: id },
                datatype: "json",
                type: "post",
                success: function (response) {
                    console.log(response);
                    if (response.status) {
                        btn.text('Kích hoạt');
                    }
                    else {
                        btn.text('Khóa');
                    }
                }
            });
        });
        $('.btn-active-pc').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: "/admin/productcategory/changestatus",
                data: { id: id },
                datatype: "json",
                type: "post",
                success: function (response) {
                    console.log(response);
                    if (response.status) {
                        btn.text('Kích hoạt');
                    }
                    else {
                        btn.text('Khóa');
                    }
                }
            });
        });
        $('.btn-active-category').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: "/admin/category/changestatus",
                data: { id: id },
                datatype: "json",
                type: "post",
                success: function (response) {
        
                    if (response.status) {
                        btn.text('Kích hoạt');
                    }
                    else {
                        btn.text('Khóa');
                    }
                }
            });
        });
        $('.btn-active-content').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: "/admin/content/changestatus",
                data: { id: id },
                datatype: "json",
                type: "post",
                success: function (response) {
   
                    if (response.status) {
                        btn.text('Kích hoạt');
                    }
                    else {
                        btn.text('Khóa');
                    }
                }
            });
        });
        $('.btn-active-about').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: "/admin/about/changestatus",
                data: { id: id },
                datatype: "json",
                type: "post",
                success: function (response) {

                    if (response.status) {
                        btn.text('Kích hoạt');
                    }
                    else {
                        btn.text('Khóa');
                    }
                }
            });
        });
        $('.btn-active-menu').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: "/admin/menu/changestatus",
                data: { id: id },
                datatype: "json",
                type: "post",
                success: function (response) {

                    if (response.status) {
                        btn.text('Kích hoạt');
                    }
                    else {
                        btn.text('Khóa');
                    }
                }
            });
        });
        $('.btn-active-slide').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: "/admin/slide/changestatus",
                data: { id: id },
                datatype: "json",
                type: "post",
                success: function (response) {

                    if (response.status) {
                        btn.text('Kích hoạt');
                    }
                    else {
                        btn.text('Khóa');
                    }
                }
            });
        });
        $('.btn-active-footer').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: "/admin/footer/changestatus",
                data: { id: id },
                datatype: "json",
                type: "post",
                success: function (response) {

                    if (response.status) {
                        btn.text('Kích hoạt');
                    }
                    else {
                        btn.text('Khóa');
                    }
                }
            });
        });
    }
}
statusController.init();
