export const Cart = {
    get() {
        return JSON.parse(sessionStorage.getItem("cart")) || [];
    },

    add(item) {
        const cart = this.get();
        cart.push(item);
        sessionStorage.setItem("cart", JSON.stringify(cart));
    },

    remove(id) {
    let cart = this.get();

   const index = cart.findIndex(p => p.id == id);
    console.log("Removing item with id:", id, "Found index:", index);

    if (index === -1) return;

    cart.splice(index, 1);

    sessionStorage.setItem("cart", JSON.stringify(cart));

},
    clear() {
        sessionStorage.removeItem("cart");
    }
};

