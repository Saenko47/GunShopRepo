import {BuildUpdateProductForm} from "./changeProductCard.js";

function init() {
    const product = sessionStorage.getItem("productToChange");

    if (!product) {
        console.error("No product in sessionStorage");
        return;
    }

    BuildUpdateProductForm();
}

document.addEventListener("DOMContentLoaded", init);