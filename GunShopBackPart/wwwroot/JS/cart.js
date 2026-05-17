
import {ToggleGlassEffect} from "./glassEffect.js";
import{Cart} from "./cartContainer.js";
import {parseJwt, getCookie} from "./cookieRepos.js"

const cartButton = document.getElementById("cartId");
const cartItemsContainer = document.getElementById("cartItemsContainerId");
const cartItemCount = document.getElementById("cartItemCount");
const cartItemsList = document.getElementById("cartItemsId");

cartItemCount.textContent = Cart.get().length;

function renderCartItems() {
   var cart = Cart.get();
    cartItemsList.innerHTML = ""; 
    cart.forEach(item => {
        const li = document.createElement("li");
        li.textContent = `${item.name} - $${item.price}`;
        cartItemsList.appendChild(li);
        const removeButton = document.createElement("button");
        removeButton.textContent = "Remove";
        removeButton.className = "searchButton";
       removeButton.addEventListener("click", (e) => {
    e.preventDefault();
    e.stopPropagation();

    if (!confirm("Are you sure you want to remove this item from the cart?")) {
        return;
    }

    Cart.remove(item.id);
    renderCartItems();
    updateCartItemCount();
});
        li.appendChild(removeButton);
    });
    
}

cartButton.addEventListener('click', (e) => {
    e.stopPropagation();

    cartItemsContainer.classList.toggle('hidden');

    console.log("Cart button clicked");

    ToggleGlassEffect();
    renderCartItems();
});

cartItemsContainer.addEventListener("click", (e) => {
    e.stopPropagation();
});


document.addEventListener('click', () => {
    if (!cartItemsContainer.classList.contains('hidden')) {
        cartItemsContainer.classList.add('hidden');
        ToggleGlassEffect();
    }
});

