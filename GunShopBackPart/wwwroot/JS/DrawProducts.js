

const container = document.getElementById("productContainerId");
const cartItemCount = document.getElementById("cartItemCount");

export function renderProduct(product) {
    if (!container || !product) return;

    const element = createProductElement(product);
    if (element) container.appendChild(element);
}

export function renderProducts(products) {
    if (!container || !Array.isArray(products)) return;

    const fragment = document.createDocumentFragment();

    for (const product of products) {
        const el = createProductElement(product);
        if (el) fragment.appendChild(el);
    }

    container.appendChild(fragment);
}
export function updateCartItemCount() {
    const cart = JSON.parse(sessionStorage.getItem("cart")) || [];
    cartItemCount.textContent = cart.length;
    console.log("Cart item count updated:", cart.length);
    
}

export function addToCart(product, quantity) {
    let cart = JSON.parse(sessionStorage.getItem("cart")) || [];

   for (let i = 0; i < quantity; i++) {
        cart.push(product);
   }
        
        
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

function BuildCard(card, product) {
    const isAuth = !!getCookie("AuthToken");
    const available = product.quantity > 0;

    if (!available) {
        const unavailable = document.createElement("p");
        unavailable.className = "buy-button searchInput";
        unavailable.textContent = "Unavailable";
        card.appendChild(unavailable);

        card.appendChild(document.createElement("br"));
        return card;
    }

    if (!isAuth) {
        const logInToBuy = document.createElement("p");
        logInToBuy.className = "buy-button searchInput";
        logInToBuy.textContent = "Log in to buy";
        card.appendChild(logInToBuy);

        card.appendChild(document.createElement("br"));
        return card;
    }

    // ===== Quantity UI =====
    let currentQty = 1;

    const wrapper = document.createElement("div");
    wrapper.className = "quantity-wrapper";

    const minusBtn = document.createElement("button");
    minusBtn.textContent = "<-";

    const qtyLabel = document.createElement("span");
    qtyLabel.textContent = currentQty;
    qtyLabel.style.margin = "0 10px";

    const plusBtn = document.createElement("button");
    plusBtn.textContent = "->";

    const updateQty = () => {
        
        qtyLabel.textContent = currentQty;
    };

    const updateAfterBuy = () => {
        let cart = JSON.parse(sessionStorage.getItem("cart")) || [];
        const inCart = cart.filter(p => p.id == product.id).length;
        qtyLabel.textContent = product.quantity - inCart;
        if(product.quantity - inCart <= 0) {
            card.querySelector(".buy-button").textContent = "Unavailable";
            plusBtn.disabled = true;
            minusBtn.disabled = true;
        }
        currentQty = parseInt(qtyLabel.textContent);
    }



    minusBtn.addEventListener("click", () => {
        if (currentQty > 1) {
            currentQty--;
            updateQty();
        }
    });

    plusBtn.addEventListener("click", () => {
         let cart = JSON.parse(sessionStorage.getItem("cart")) || [];
          cart = cart.filter(p => p.id == product.id);
        if (currentQty < product.quantity-cart.length) {
            currentQty++;
            updateQty();
        }
    });

    wrapper.appendChild(minusBtn);
    wrapper.appendChild(qtyLabel);
    wrapper.appendChild(plusBtn);

    // ===== Buy button =====
    const buyButton = document.createElement("button");
    buyButton.className = "buy-button searchInput";
    buyButton.textContent = "Buy";

    buyButton.addEventListener("click", () => {
        let cart = JSON.parse(sessionStorage.getItem("cart")) || [];
        cart = cart.filter(p => p.id == product.id);
        if(cart.length + currentQty > product.quantity) {
            alert(`Cannot add ${product.name} items to cart`);
            return;
        }
        addToCart(product, currentQty);
        updateAfterBuy();
        console.log("Added to cart:", product, "qty:", currentQty);
    });

    card.appendChild(wrapper);
    card.appendChild(document.createElement("br"));
    card.appendChild(buyButton);
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