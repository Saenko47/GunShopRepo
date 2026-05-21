export function collectFilter(form) {
    const data = {};
    const formData = new FormData(form);
    

    for (let [key, value] of formData.entries()) {
        if (value === "") {
            data[key] = null;
            continue;
        }

        // приведение типов
        switch (key) {
            case "minPrice":
            case "maxPrice":
            case "quantity":
                data[key] = parseInt(value);
                break;

            case "price":
                data[key] = parseFloat(value);
                break;

            default:
                data[key] = value;
                break;
        }
    }

    return data;
}