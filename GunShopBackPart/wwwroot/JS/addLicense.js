import { getCookie, parseJwt } from "./cookieRepos.js";
import { EnableGlassEffect, DisableGlassEffect } from "./glassEffect.js";

const addLicenseForm = document.getElementById("addLicenseFormId");
const addLicenseBtn = document.getElementById('addLicenseBtnId');
const licensekey = document.getElementById("licenseKeyInputId");
const licenseType = document.getElementById("licenseTypeSelect");
const licenseSubmitBtn = document.getElementById("addLicenseSubmitBtn");

addLicenseBtn.addEventListener('click', (e) => 
    {
         e.stopPropagation();
        addLicenseForm.classList.remove("hidden");
        EnableGlassEffect();
    });

addLicenseForm.addEventListener("click", (e)=>
    {
        e.stopPropagation();
    })

document.addEventListener("click", () => {
    if (!addLicenseForm.classList.contains("hidden")) {
        addLicenseForm.classList.add("hidden");
        DisableGlassEffect();
    }
});


licenseSubmitBtn.addEventListener("click", async (e) =>
    {
            e.preventDefault();
        if(!licensekey.value)
            {
                alert("Theres problem with a key of license");
                return;
            }
        const token = getCookie("AuthToken");

        
        try{
        const res = await fetch("/api/Customer/addlicense",
            {
                method: "PUT",
    headers: {
        "Authorization": `Bearer ${token}`,
        "Content-Type": "application/json"
    },
    body: JSON.stringify(Number(licenseType.value))
            }
        );
        if(!res)
            {
                alert("Error");
                return;
            }
        }
        catch (error) {
        console.error(error);
        alert("Error while adding license");
    }
       alert("License Was Added!");
       window.location.reload();

    });

