import {ToggleGlassEffect} from "./glassEffect.js";

const showPurchaseHistoryBtn = document.getElementById("showPurchaseHistoryBtn");

const productPurchaseContainer = document.getElementById("ProductPurchaseId");

showPurchaseHistoryBtn.addEventListener("click", (e) => {
    console.log("Show Purchase History button clicked");
    e.stopPropagation();
    productPurchaseContainer.classList.toggle("hidden");
    ToggleGlassEffect();

    fetch("/api/ProductPurchase/Purchases", {
        method: "GET",
        headers: {
            "Content-Type": "application/json"
        }
    })
    .then(response => response.json())
    .then(data => {
        renderPurchaseHistory(data);
    })
    .catch(error => {
        console.error("Error fetching purchase history:", error);
        alert("An error occurred while fetching purchase history. Please try again.");
    });
});

productPurchaseContainer.addEventListener("click", (e) => {
    e.stopPropagation();
});

document.addEventListener("click", () => {
    if (!productPurchaseContainer.classList.contains("hidden")) {
        productPurchaseContainer.classList.add("hidden");
        ToggleGlassEffect();
    }
});


function renderPurchaseHistory(purchases) 
{
    productPurchaseContainer.innerHTML = "";
    if (purchases.length === 0) {
        const noPurchasesMessage = document.createElement("p");
        noPurchasesMessage.textContent = "You have no purchase history.";
        productPurchaseContainer.appendChild(noPurchasesMessage);
        return;
    }
    purchases.forEach(purchase => {
        const purchaseDiv = document.createElement("div");
        purchaseDiv.className = "purchaseItem";
        purchaseDiv.innerHTML = `
            <h3>${purchase.productName}</h3>
            <p>Purchased on: ${new Date(purchase.purchaseAt).toLocaleDateString()}</p>
        `;
        productPurchaseContainer.appendChild(purchaseDiv);
    });
}