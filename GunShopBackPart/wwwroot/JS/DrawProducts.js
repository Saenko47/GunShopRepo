
const container = document.getElementById("productContainerId");

function renderProduct(product) {
    if (!container || !product) return;

    const element = createProductElement(product);
    if (element) container.appendChild(element);
}

function renderProducts(products) {
    if (!container || !Array.isArray(products)) return;

    const fragment = document.createDocumentFragment();

    for (const product of products) {
        const el = createProductElement(product);
        if (el) fragment.appendChild(el);
    }

    container.appendChild(fragment);
}

// router
function createProductElement(product) {
    switch (product.productType) {
        case "Gun":
            return BuildCard(createGunCard(product));
        case "Ammo":
            return BuildCard(createAmmoCard(product));
        case "Accessory":
            return BuildCard(createAccessoryCard(product));
        default:
            return BuildCard(createBaseCard(product));
    }
}
function BuildCard(card)
{
    const button = document.createElement("button");
    button.className = "buy-button searchInput";
    button.textContent = "Buy";

    card.appendChild(document.createElement("br"));
    card.appendChild(button);

    return card;
}
// base
function createBaseCard(product) {
    const div = document.createElement("div");
    div.className = "product-card base";

    const img = document.createElement("img");
    img.src = product.imageUrl || "";
    img.alt = product.name;

    const content = document.createElement("div");
    content.className = "product-content";

    content.innerHTML = `
        <h3>${product.name}</h3>
        <p><b>Price:</b> ${product.price}</p>
        <p><b>Supplier:</b> ${product.supplierName || "Unknown"}</p>
        <p><b>Permit:</b> ${product.requiredPermit}</p>
        <p><b>Type:</b> ${product.productType}</p>
    `;

    div.appendChild(img);
    div.appendChild(content);

    return div;
}

function createGunCard(product) {
    const card = createBaseCard(product);
    card.classList.add("gun");

    const extra = document.createElement("div");
    extra.className = "product-extra";
    extra.innerHTML = `
        <p><b>Caliber:</b> ${product.caliber}</p>
        <p><b>Gun type:</b> ${product.gunType}</p>
    `;

    card.appendChild(extra);
    return card;
}

function createAmmoCard(product) {
    const card = createBaseCard(product);
    card.classList.add("ammo");

    const extra = document.createElement("div");
    extra.className = "product-extra";
    extra.innerHTML = `
        <p><b>Caliber:</b> ${product.caliber}</p>
        <p><b>Amount in box:</b> ${product.amountInBox}</p>
    `;

    card.appendChild(extra);
    return card;
}

function createAccessoryCard(product) {
    const card = createBaseCard(product);
    card.classList.add("accessory");

    const extra = document.createElement("div");
    extra.className = "product-extra";
    extra.innerHTML = `
        <p><b>Type:</b> ${product.type}</p>
    `;

    card.appendChild(extra);
    return card;
}