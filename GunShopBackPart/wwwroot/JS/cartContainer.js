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
        cart = cart.filter(i => i.id !== id);
        sessionStorage.setItem("cart", JSON.stringify(cart));
    }
};

