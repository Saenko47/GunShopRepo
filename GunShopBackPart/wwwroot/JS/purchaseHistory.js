import {ToggleGlassEffect} from "./glassEffect.js";

const showPurchaseHistoryBtn = document.getElementById("showPurchaseHistoryBtn");

const productPurchaseContainer = document.getElementById("ProductPurchaseId");

showPurchaseHistoryBtn.addEventListener("click", async (e) => {
    console.log("Show Purchase History button clicked");

    e.stopPropagation();

    productPurchaseContainer.classList.toggle("hidden");
    ToggleGlassEffect();

    try {
        const response = await fetch("/api/ProductPurchase/Purchases", {
            method: "GET",
            headers: {
                "Content-Type": "application/json"
            }
        });

        const res = await response.json();

        if (res && res.length > 0) {
            renderPurchaseHistory(res);
        } else {
            productPurchaseContainer.innerHTML = "";

            const noPurchasesMessage = document.createElement("p");
            noPurchasesMessage.textContent = "You have no purchase history.";
            noPurchasesMessage.style.textAlign = "center";
            noPurchasesMessage.style.margin = "20px";

            productPurchaseContainer.appendChild(noPurchasesMessage);
        }

    } catch (error) {
        console.error("Error loading purchase history:", error);
    }
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