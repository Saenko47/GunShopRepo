import{getCookie, parseJwt} from "./cookieRepos.js";

export async function DeleteProduct(id) {
    const token = getCookie("AuthToken");
    if (!token) {
        alert("You are not authorized");
        return;
    }
    const payload = parseJwt(token);
    const role =
  payload.role ||
  payload["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
    if (role != "Admin") {
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
