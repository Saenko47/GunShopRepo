import{Cart} from "./cartContainer.js";
import {parseJwt, getCookie} from "./cookieRepos.js"
import {renderCartItems} from "./cart.js";

const buyButton = document.getElementById("BuyBtnId");

buyButton.addEventListener("click", (e) => {
  
    const products = JSON.parse(sessionStorage.getItem("cart")) || [];
    if (products.length === 0) {
        alert("Your cart is empty!");
        return;
    }
    const token = getCookie("AuthToken");
    if (!token) {
        alert("You must be logged in to make a purchase.");
        return;
    }
    const payload = parseJwt(token);
    if (!payload) {
        alert("Invalid token. Please log in again.");
        return;
    }
    var ProductIds = products.map(p => p.id);
    fetch("/api/InventoryItem/buy", {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "Authorization": `Bearer ${token}`
        },
      body: JSON.stringify(ProductIds)
    })
    .then(response => {
        if (response.ok) {
            alert("Purchase successful!");
            sessionStorage.removeItem("cart");
            updateCartItemCount();
            Cart.clear();
            renderCartItems();

        } else {
            alert("Purchase failed. Please try again.");
        }
    })
    .catch(error => {
        console.error("Error during purchase:", error);
        alert("An error occurred. Please try again.");
    });
});