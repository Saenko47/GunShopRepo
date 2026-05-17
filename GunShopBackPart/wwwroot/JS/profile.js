import { getCookie, parseJwt } from "./cookieRepos.js";

const licensesContainer = document.getElementById("licenseContainer");

function VisualizeProfile(data) 
{
    document.getElementById("nameValue").textContent = data.name;
    document.getElementById("surnameValue").textContent = data.surname;
    document.getElementById("gmailValue").textContent = data.gmail;
    document.getElementById("phoneValue").textContent = data.phoneNumber;
    document.getElementById("loginValue").textContent = data.login;
    document.getElementById("createdAtValue").textContent = data.createdAt;

    document.getElementById("avatarCircleId").textContent = data.name[0] + data.surname[0];
    document.getElementById("userName").textContent = data.name + " " + data.surname;
    document.getElementById("userLogin").textContent = data.login;
    for (const license of data.licens) {
        const div = document.createElement("div");
        div.className = "licenseItem";
        div.textContent = license;
        licensesContainer.appendChild(div);
    }

}

async function GetProfileInfo() 
{
    const token = getCookie("AuthToken");

    if (!token) {
        alert("You are not authorized!");
        return;
    }

    var payload = parseJwt(token);

    if (!payload) {
        alert("Invalid token!");
        return;
    }

    const res = await fetch("/api/Customer/customerProfile", {
        method: "GET",
        headers: {
            "Authorization": `Bearer ${token}`
        }
    });

    if (!res.ok) {
        alert("Failed to load profile");
        return;
    }

    const data = await res.json();

    VisualizeProfile(data);
}

GetProfileInfo();

document.getElementById("LogoutBtnId").addEventListener("click", () => {
    document.cookie = "AuthToken=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
    window.location.href = "MainPage.html";
});