

const container = document.getElementById("productContainerId");
const cartItemCount = document.getElementById("cartItemCount");

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
function updateCartItemCount() {
    const cart = JSON.parse(sessionStorage.getItem("cart")) || [];
    cartItemCount.textContent = cart.length;
    console.log("Cart item count updated:", cart.length);
    
}

function addToCart(product) {
    let cart = JSON.parse(sessionStorage.getItem("cart")) || [];

    cart.push(product);
    console.log(cart);
    sessionStorage.setItem("cart", JSON.stringify(cart));
    updateCartItemCount();
}
// router
function createProductElement(product) {
    switch (product.productType) {
        case "Gun":
            return BuildCard(createGunCard(product), product);
        case "Ammo":
            return BuildCard(createAmmoCard(product), product);
        case "Accessory":
            return BuildCard(createAccessoryCard(product), product  );
        default:
            return BuildCard(createBaseCard(product), product);
    }
}

function getCookie(name, parseAsJwt = false) {
    const cookies = document.cookie.split("; ");

    for (const cookie of cookies) {
        const [key, value] = cookie.split("=");

        if (key === name) {

            const decodedValue = decodeURIComponent(value);

            if (parseAsJwt) {
                return parseJwt(decodedValue);
            }

            return decodedValue;
        }
    }

    return null;
}

function BuildCard(card, product)
{

    if(product.isAvailable && getCookie("AuthToken")) {
    const button = document.createElement("button");
    button.className = "buy-button searchInput";
    button.textContent = "Buy"
    button.addEventListener("click", () => {
        addToCart(product);
        console.log("Product added to cart:", product);
          
    });    
    card.appendChild(button);;
    }
    else if(!getCookie("AuthToken"))     
        {
            const logInToBuy = document.createElement("p");
            logInToBuy.className = "buy-button searchInput";
            logInToBuy.textContent = "Log in to buy";
            card.appendChild(logInToBuy);
        }
    else
        {
            const unavailable = document.createElement("p");
             
            unavailable.className = "buy-button searchInput";
            unavailable.textContent = "Unavailable";
            card.appendChild(unavailable);
        }


    card.appendChild(document.createElement("br"));
    

    return card;
}

// base
function createBaseCard(product) {
    const div = document.createElement("div");
    div.className = "product-card base";
    div.dataset.productId = product.id; // для идентификации при клике
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