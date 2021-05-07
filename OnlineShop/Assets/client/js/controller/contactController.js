var contact = {
    init: function () {
        contact.registerEvent();
    },
    registerEvent: function () {
        $('#send').off('click').on('click', function (e) {
            e.preventDefault();
            var name = $('#txtName').val();
            var mobile = $('#txtPhone').val();
            var email = $('#txtEmail').val();
            var address = $('#txtAddress').val();
            var content = $('#txtContent').val();

            $.ajax({
                data: {
                    feedbackName: name,
                    feedbackPhone: mobile,
                    feedbackAddress: address,
                    feedbackEmail: email,
                    feedbackContent: content
                },
                url: '/Contact/Feedback',
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        alert('Phản hồi thành công');
                        window.location.href = "/";

                    }
                    else {
                        alert('Lỗi');
                    }
                }
            })

        });
    }
}
contact.init();