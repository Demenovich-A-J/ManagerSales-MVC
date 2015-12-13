$(document).ready(function () {
    $("#customer-form").validate({
        rules: {
            Name: {
                required: true,
                minlength: 4
            }
        },

        submitHandler: function (form) {
            form.submit();
        }
    });
    $("#product-form").validate({
        rules: {
            Name: {
                required: true,
                minlength: 4
            }
        },

        submitHandler: function (form) {
            form.submit();
        }
    });
    $("#manager-form").validate({
        rules: {
            LastName: {
                required: true,
                minlength: 4
            }
        },

        submitHandler: function (form) {
            form.submit();
        }
    });
    $("#sale-form").validate({
        rules: {
            Summ: {
                required: true,
                minlength: 1,
                digits: true
            },
            Date: {
                required: true,
                date: true
            }
        },

        submitHandler: function (form) {
            form.submit();
        }
    });
    $("#login-form").validate({
        rules: {
            Login: "required",
            Password: {
                required: true,
                minlength: 6
            }
        },
        messages: {
            Password: {
                required: "Please provide a password",
                minlength: "Your password must be at least 5 characters long"
            }

        },

        submitHandler: function (form) {
            form.submit();
        }
    });
    $("#register-form").validate({
        rules: {
            Login: {
                required: true,
                minlength: 3
            },
            Password: {
                required: true,
                minlength: 6,
                maxlength: 16
            },
            ConfirmPassword: {
                required: true,
                minlength: 6,
                maxlength: 16,
                equalTo: "#password"
            },
            Email: {
                required: true,
                email: true
            }
        },
        messages: {
            Password: {
                required: "Please provide a password",
                minlength: "Your password must be at least 6 characters long"
            },
            Email: {
                required: "Please provide a email"
            },
            ConfirmPassword: {
                equalTo: "Passwords do not match."
            }

        },

        submitHandler: function (form) {
            form.submit();
        }
    });
});