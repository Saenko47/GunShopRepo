function sendToServerByName(name, page, pageSize) {
    const params = new URLSearchParams();

    params.append("page", page);
    params.append("pageSize", pageSize);
    params.append("query", name);

    fetch(`/api/Product/search?${params.toString()}`, {
        method: "GET"
    })
    .then(async (r) => {
        if (!r.ok) throw new Error(`HTTP error: ${r.status}`);
        return await r.json();
    })
    .then(data => {
        const products = GetProductsForRender(data);
        renderProducts(products);

        console.log(data);
        console.log(products);
    })
    .catch(err => {
        console.error("Fetch error:", err);
    });
}
