import{getCookie, parseJwt} from "./cookieRepos.js";

const headerBox = document.getElementById("headerBoxId");

function init() {
    const token = getCookie("AuthToken");
    const payload = parseJwt(token);
    const isAdmin = payload && payload.role === "Admin";
    if (isAdmin) {
        const adminPanelLink = document.createElement("a");
        adminPanelLink.href = "CreateNewProduct.html";
        adminPanelLink.textContent = "Create Product";
        adminPanelLink.style.marginLeft = "20px";
        adminPanelLink.style.color = "white";
        adminPanelLink.style.textDecoration = "none";
        adminPanelLink.classList.add("searchInputForFilter");
       headerBox.insertBefore(adminPanelLink, headerBox.firstChild)
   }
    
}

document.addEventListener("DOMContentLoaded", init);