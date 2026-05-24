import { getCookie, parseJwt } from "./cookieRepos.js";
import { EnableGlassEffect, DisableGlassEffect } from "./glassEffect.js";
import {GetCustomerInfo} from "./profile.js";

const updateForm = document.getElementById("updateProfileForm");

const updateDiv = document.getElementById("updateProfileFormId");

const updateButton = document.getElementById("editProfileBtnId");


async function  SetBase()
{
    const data = await GetCustomerInfo();

    if (!data) {
        alert("Failed to load profile data.");
        return;
    }

   document.getElementById("editName").value = data.name;
   document.getElementById("editSurname").value = data.surname;
   document.getElementById("editGmail").value = data.gmail;
   document.getElementById("editPhone").value = data.phoneNumber;


}


updateForm.addEventListener("submit", async (e) => {
    e.preventDefault();

    const token = getCookie("AuthToken");

    if (!token) {
        alert("You are not authorized");
        return;
    }

    const payload = parseJwt(token);

   
    const name = document.getElementById("editName").value;
    const surname = document.getElementById("editSurname").value;
    const gmail = document.getElementById("editGmail").value;
    const phone = document.getElementById("editPhone").value;

    console.log("Payload ID:", payload.id);

   const body = {
    Id: payload.id,
    Name: name,
    Surname: surname,
    Gmail: gmail,
    PhoneNumber: phone
};
console.log("Request body:", body);

    try {

        const response = await fetch("/api/Customer/update", {
    method: "PUT",
    headers: {
        "Authorization": `Bearer ${token}`,
        "Content-Type": "application/json"
    },
    body: JSON.stringify(body)
});
        console.log("Profile update response:", response);

        if (!response.ok) {
            throw new Error("Failed to update profile");
        }

        alert("Profile updated successfully");
        window.location.reload();

    } catch (error) {
        console.error(error);
        alert("Error while updating profile");
    }
});

updateButton.addEventListener('click', (e) => {
    e.stopPropagation();

    const updateForm = document.getElementById("updateProfileForm");
    updateDiv.classList.toggle('hidden');

    console.log("Edit profile button clicked");
    EnableGlassEffect();
    
});

updateForm.addEventListener("click", (e) => {
    e.stopPropagation();
});


document.addEventListener('click', () => {
    if (!updateForm.classList.contains('hidden')) {
        updateDiv.classList.add('hidden');
          DisableGlassEffect();
    }
});

SetBase();
