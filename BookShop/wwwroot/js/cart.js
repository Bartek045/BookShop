function applyDiscount() {
    var discountInput = document.getElementById('code');
    var discountError = document.getElementById('discount-error');

    if (discountInput.value.trim() === '') {
        discountError.style.display = 'block';
    } else {
        discountError.style.display = 'none';

    }
}

function toggleDiscountInput() {
    var discountInputContainer = document.getElementById('discount-input-container');
    discountInputContainer.style.display = (discountInputContainer.style.display === 'none' || discountInputContainer.style.display === '') ? 'block' : 'none';
    var discountError = document.getElementById('discount-error');
    discountError.style.display = 'none';
}

function removeFromCart(productId) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You want to remove this item from the cart?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, remove it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: 'POST',
                url: '/Customer/Cart/RemoveFromCart',
                data: { productId: productId },
                success: function () {
                    // Reload the page or update the cart content as needed
                    location.reload();
                },
                error: function () {
                    // Handle errors if needed
                    console.log("Error removing item from cart.");
                }
            });
        }
    });
}

$(document).ready(function () {
    // Handle form submission
    $('form').submit(function (e) {
        e.preventDefault(); // Prevent the default form submission

        var form = $(this);

        $.ajax({
            type: form.attr('method'),
            url: form.attr('action'),
            data: form.serialize(),
            success: function () {
                // Show success notification
                Swal.fire({
                    icon: 'success',
                    title: 'Product added to cart!',
                    showConfirmButton: false,
                    timer: 1500 // Automatically close after 1.5 seconds
                });
            },
            error: function () {
                // Handle errors if needed
                console.log("Error adding product to cart.");
            }
        });
    });
});
