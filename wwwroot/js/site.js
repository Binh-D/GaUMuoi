function addToCart(productId, name, image, price) {
    fetch(`/Cart/AddToCart?id=${productId}&name=${encodeURIComponent(name)}&image=${encodeURIComponent(image)}&price=${price}`, {
        method: 'GET'
    })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                alert(data.message);
            }
        });
}
