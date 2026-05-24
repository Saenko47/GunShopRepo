
import {parseJwt, getCookie} from "./cookieRepos.js"
import {ToggleGlassEffect} from "./glassEffect.js";



const btnLogin = document.getElementById("UserInfoId");
const UserInfoForm = document.getElementById("UserLoginFormId");
const overlay = document.getElementById("overlay");
const form = document.getElementById("customerFormId");
const exitBtn = document.getElementById("UserLoginExitBtn");

var isUserIn = false;
var isAdmin = false;



function initAuth() {
    const token = getCookie("AuthToken");

    if (!token) {
        isUserIn = false;
        return;
    }

    const payload = parseJwt(token);

    if (!payload) {
        isUserIn = false;
        return;
    }
    
    const role =
  payload.role ||
  payload["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
    isAdmin = role == "Admin";
    isUserIn = true;
    btnLogin.innerText = payload.username;
}

initAuth();

function ShowLoginForm() {
    UserInfoForm.classList.remove("hidden");
}

function HideLoginForm()
{
    ToggleGlassEffect();
    UserInfoForm.classList.add("hidden");
}

exitBtn.addEventListener("click", () =>
    {
       HideLoginForm();
    });

btnLogin.addEventListener("click", (e) => {
    if(isUserIn)
        {
            if(isAdmin)
                {
                     window.location.href = "AdminPanel.html";
                }
            else{
                
             
                  window.location.href = "Profile.html";
            }
            return;
        }
     
    ToggleGlassEffect();
    e.stopPropagation();
    ShowLoginForm();
});

UserInfoForm.addEventListener("click", (e) => {
    e.stopPropagation();
});

document.addEventListener("click", () => {
    if(UserInfoForm.classList.contains("hidden"))
        return;
    UserInfoForm.classList.add("hidden");
  HideLoginForm();
});



form.addEventListener("submit", async (e) => {
    e.preventDefault();

    const params = new URLSearchParams();
    params.append("login", document.getElementById("login").value);
    params.append("password", document.getElementById("password").value);
    const typeOfUser = document.getElementById("IsAdminId").checked ? "Admin" : "Customer";
   
    const res = await fetch(`/api/${typeOfUser}/login`, {
        method: "POST",
        headers: {
            "Content-Type": "application/x-www-form-urlencoded"
        },
        body: params.toString(),
        credentials: "include"
    });

    if (res.ok) {
        isUserIn = true;
        console.log("login success");

        // 🔥 ВАЖНО: берем JWT правильно
        const token = getCookie("AuthToken");
        const payload = parseJwt(token);

        btnLogin.innerText = payload.username;
        
    
        console.log(payload.username);
        HideLoginForm();
       
        window.location.reload();

    }
    else
        {
            alert("Login failed! Check your credentials.");
            
        }
});

