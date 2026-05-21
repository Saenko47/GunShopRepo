

    import { GetProductsForRender } from "./ProductFabric.js";
    import { renderProducts } from "./DrawProducts.js";

    export function sendToServer(page, pageSize, filter, type) {
    const params = new URLSearchParams();

    params.append("page", page);
    params.append("pageSize", pageSize);

    if (filter && typeof filter === "object") {
        Object.entries(filter).forEach(([key, value]) => {
            if (value !== null && value !== undefined && value !== "") {
                params.append(key, value);
            }
        });
    }
    

    fetch(`/api/Product/${type}?${params.toString()}`, {
        method: "GET"
    })
    .then(async (r) => {
        if (!r.ok) {
            throw new Error(`HTTP error: ${r.status}`);
        }
        return await r.json();
    })
    .then(data => {
        const products = GetProductsForRender(data);
        renderProducts(products);
        console.log(data)
        console.log(products);
    })
    .catch(err => {
        console.error("Fetch error:", err);
    });
}


