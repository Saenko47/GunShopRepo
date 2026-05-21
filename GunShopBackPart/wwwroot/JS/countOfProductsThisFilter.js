

export function GetCountOfProductsThisFilter(filter, type, count) 
{
    if(filter == null || type == null) return Promise.resolve(0);

    const params = new URLSearchParams();

    if (typeof filter === "object") {
        Object.entries(filter).forEach(([key, value]) => {
            if (value !== null && value !== undefined && value !== "") {
                params.append(key, value);
            }
        });
    }

    params.append("count", count);


    return fetch(`/api/Product/countForPagination/${type}?${params.toString()}`)
        .then(r => {
            if (!r.ok) throw new Error(`HTTP error: ${r.status}`);
            return r.json();
        })
        .catch(err => {
            console.error("Fetch error:", err);
            return 0;
        });
}

