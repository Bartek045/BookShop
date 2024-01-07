function loadProduct(category) {
    window.location.href = `/Customer/Product/Index?category=${category}`;
    ViewBag.Category = category;
}