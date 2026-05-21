const form = document.getElementById("createProductForm");
const productType = document.getElementById("productType");
const dynamicFields = document.getElementById("dynamicFields");

RenderFields(productType.value);

productType.addEventListener("change", () => {
    RenderFields(productType.value);
});

function RenderFields(type)
{
    dynamicFields.innerHTML = "";

    switch(type)
    {
        case "gun":
            dynamicFields.innerHTML = `
                <input type="number" id="caliber" placeholder="Caliber">
                
                <select id="gunType">
                    <option value="0">Pistol</option>
                    <option value="1">Rifle</option>
                    <option value="2">Shotgun</option>
                </select>
            `;
            break;

        case "ammo":
            dynamicFields.innerHTML = `
                <input type="number" id="caliber" placeholder="Caliber">
                <input type="number" id="amountInBox" placeholder="Amount In Box">
            `;
            break;

        case "accessory":
            dynamicFields.innerHTML = `
                <select id="type">
                    <option value="0">Scope</option>
                    <option value="1">Grip</option>
                    <option value="2">Silencer</option>
                </select>
            `;
            break;
    }
}

form.addEventListener("submit", async (e) => {
    e.preventDefault();

    const type = productType.value;

    const formData = new FormData();

    formData.append("Name", document.getElementById("name").value);
    formData.append("Description", document.getElementById("description").value);
    formData.append("Price", document.getElementById("price").value);
    formData.append("SupplierName", document.getElementById("supplierName").value);
    formData.append("RequiredPermit", document.getElementById("requiredPermit").value);
    formData.append("SerialNumber", document.getElementById("serialNumber").value);

    const image = document.getElementById("image").files[0];

    if(image)
    {
        formData.append("Image", image);
    }

    switch(type)
    {
        case "gun":
            formData.append("Caliber", document.getElementById("caliber").value);
            formData.append("GunType", document.getElementById("gunType").value);
            break;

        case "ammo":
            formData.append("Caliber", document.getElementById("caliber").value);
            formData.append("AmountInBox", document.getElementById("amountInBox").value);
            break;

        case "accessory":
            formData.append("Type", document.getElementById("type").value);
            break;
    }

    try
    {
        const token = localStorage.getItem("token");

        const response = await fetch(`/api/Product/${type}`, {
            method: "POST",
            headers: {
                "Authorization": `Bearer ${token}`
            },
            body: formData
        });

        if(response.ok)
        {
            alert("Product created");

            form.reset();

            RenderFields(productType.value);
        }
        else
        {
            const error = await response.text();

            alert(error);
        }
    }
    catch(error)
    {
        console.error(error);

        alert("Server error");
    }
});