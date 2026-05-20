const form = document.querySelector(".registerForm");

form.addEventListener("submit", async (e) => {
    e.preventDefault();

    const formData = new FormData(form);

    try {
        const response = await fetch("/api/Customer/register", {
            method: "POST",
            body: formData,
            credentials: "include"
        });

        if (response.ok) {
            alert("Account created successfully!");
            window.location.href = "/index.html";
        } 
        else {
            const error = await response.text();
            alert("Registration error: " + error);
        }
    } 
    catch (err) {
        console.error(err);
        alert("Server connection error");
    }
});