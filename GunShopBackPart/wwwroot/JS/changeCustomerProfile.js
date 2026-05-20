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
   document.getElementById("editPassword").value = "";

}


updateForm.addEventListener("submit", async (e) => {
    e.preventDefault();

    const token = getCookie("AuthToken");

    if (!token) {
        alert("You are not authorized");
        return;
    }

    const payload = parseJwt(token);

    const formData = new FormData();

    formData.append("Id", payload.Id);

    const name = document.getElementById("editName").value;
    const surname = document.getElementById("editSurname").value;
    const gmail = document.getElementById("editGmail").value;
    const phone = document.getElementById("editPhone").value;
    const password = document.getElementById("editPassword").value;

    if (name.trim() !== "")
        formData.append("Name", name);

    if (surname.trim() !== "")
        formData.append("Surname", surname);

    if (gmail.trim() !== "")
        formData.append("gmail", gmail);

    if (phone.trim() !== "")
        formData.append("PhoneNumber", phone);

    if (password.trim() !== "")
        formData.append("Password", password);

    try {

        const response = await fetch("https://localhost:5001/api/customer/update", {
            method: "PUT",
            headers: {
                "Authorization": `Bearer ${token}`
            },
            body: formData
        });

        if (!response.ok) {
            throw new Error("Failed to update profile");
        }

        alert("Profile updated successfully");

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
