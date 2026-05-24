
import {ToggleGlassEffect} from "./glassEffect.js";
import{Cart} from "./cartContainer.js";



const cartButton = document.getElementById("cartId");
const cartItemsContainer = document.getElementById("cartItemsContainerId");
const cartItemCount = document.getElementById("cartItemCount");
const cartItemsList = document.getElementById("cartItemsId");
const buyButton = document.getElementById("BuyBtnId");
const cartTitle = document.getElementById("cartTitleId");
const exitBtn = document.getElementById("exitCartBtn");


var cartCount = 0;

cartItemCount.textContent = Cart.get().length;

export function exitFromCart()
{
    cartItemsContainer.classList.add('hidden');
    ToggleGlassEffect();
}


function updateCartItemCount()
{
    const cart = Cart.get();
    cartItemCount.textContent = cart.length;
    console.log("Cart item count updated:", cart.length);
}

export function renderCartItems() {
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
   

   

    Cart.remove(item.id);
    renderCartItems();
    updateCartItemCount();
});
        li.appendChild(removeButton);
    });
    
}

exitBtn.addEventListener("click", () =>
    {
        exitFromCart();
    })

cartButton.addEventListener('click', (e) => {
    e.stopPropagation();
    if(Cart.get().length === 0) 
        {
            buyButton.classList.add("hidden");
                cartTitle.innerText = "Your cart is empty";
        }
    else
        {
            buyButton.classList.remove("hidden");
            cartTitle.innerText = "Cart";
        }
    cartItemsContainer.classList.toggle('hidden');

    console.log("Cart button clicked");

    ToggleGlassEffect();
    renderCartItems();
    cartCount = Cart.get().length;
});

cartItemsContainer.addEventListener("click", (e) => {
    e.stopPropagation();
});


document.addEventListener('click', () => {
    if (!cartItemsContainer.classList.contains('hidden')) {
        cartItemsContainer.classList.add('hidden');
        ToggleGlassEffect();
         if (cartCount != Cart.get().length) {
       window.location.reload();
    }

       
    }
   
});



