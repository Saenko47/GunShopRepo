import{getCookie, parseJwt} from "./cookieRepos.js";

export async function DeleteProduct(id) {
    const token = getCookie("AuthToken");
    if (!token) {
        alert("You are not authorized");
        return;
    }
    const payload = parseJwt(token);
    if (payload.role != "Admin") {
        alert("You do not have permission to delete products.");
        return;
    }   
    const response = await fetch(`/api/Product/${id}`, {
        method: "DELETE",
        headers: {
            "Authorization": `Bearer ${token}`
        }
    });
    if (response.ok) {
        alert("Product deleted successfully.");
        window.location.reload();
    } else {
        alert("Failed to delete product.");
        
    }
    
}
