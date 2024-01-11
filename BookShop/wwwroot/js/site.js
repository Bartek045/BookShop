function loadProduct(category) {
    window.location.href = `/Customer/Product/Index?category=${category}`;
    ViewBag.Category = category;
}

$(document).ready(function () {
    // ...

    $('#product-add-form').submit(function (e) {
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

                // Update the cart item count in real-time
                updateCartItemCount();
            },
            error: function () {
                // Handle errors if needed
                console.log("Error adding product to cart.");
            }
        });
    });

    // Function to update the cart item count
    function updateCartItemCount() {
        $.ajax({
            type: 'GET',
            url: '/Customer/Cart/GetCartItemCount', // Change the URL accordingly
            success: function (data) {
                // Update the cart item count
                $('#cartItemCount').text(data);
            },
            error: function () {
                // Handle errors if needed
                console.log("Error updating cart item count.");
            }
        });
    }

    // Initial call to update the cart item count on page load
    updateCartItemCount();
});

