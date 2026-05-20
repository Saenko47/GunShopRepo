import { CreateInput, CreateHiddenInput, CreateTextarea, CreateSelect } from "./UpdateProductHelper.js";

const form = document.getElementById("changeProductForm");

const FetchForCertainProduct = 
{
    "gun":"UpdateProductGun",
    "ammo":"UpdateProductAmmo",
    "accessory":"UpdateProductAccessory"
}

const updateFactories = {
    Gun: BuildGunFields,
    Ammo: BuildAmmoFields,
    Accessory: BuildAccessoryFields
};

function BuildGunFields(product)
{
    form.appendChild(
        CreateSelect(
            "Caliber",
            fromEnumToStringCaliber,
            product.caliber
        )
    );

    form.appendChild(
        CreateSelect(
            "GunType",
            fromEnumToStringGunType,
            product.gunType
        )
    );
}

function BuildAmmoFields(product)
{
    form.appendChild(
        CreateSelect(
            "Caliber",
            fromEnumToStringCaliber,
            product.caliber
        )
    );

    form.appendChild(
        CreateInput(
            "number",
            "AmountInBox",
            product.amountInBox
        )
    );
}



function BuildUpdateProductForm(product)
{
    form.innerHTML = "";

    const title = document.createElement("h2");
    title.textContent = "Edit Product";
    form.appendChild(title);
    // Id
    const idInput = CreateHiddenInput("Id", product.id);

    // Name
    const nameInput = CreateInput("text", "Name", product.name);

    // Description
    const descriptionInput = CreateTextarea("Description", product.description);
    descriptionInput.name = "Description";
    descriptionInput.placeholder = "Description";
    descriptionInput.value = product.description || "";

    // Price
    const priceInput = CreateInput("number", "Price", product.price, "0.01");
    priceInput.value = product.price || "";

    // SupplierName
    const supplierInput = CreateInput("text", "SupplierName", product.supplierName);

    // RequiredPermit
    const permitSelect = CreateSelect("RequiredPermit", fromEnumToStringRequiredPermit, product.requiredPermit);

    const permits = [
        "None",
        "ForPistol",
        "ForRifle",
        "ForShotgun"
    ];

    permits.forEach((permit, index) => {
        const option = document.createElement("option");

        option.value = index;
        option.textContent = permit;

        if (product.requiredPermit == index)
        {
            option.selected = true;
        }

        permitSelect.appendChild(option);
    });

    // Image
    const imageInput = document.createElement("input");
    imageInput.type = "file";
    imageInput.name = "Image";
    imageInput.accept = "image/*";

    // Submit
    const submitButton = document.createElement("button");
    submitButton.type = "submit";
    submitButton.textContent = "Update Product";

    
    form.appendChild(idInput);
    form.appendChild(nameInput);
    form.appendChild(descriptionInput);
    form.appendChild(priceInput);
    form.appendChild(supplierInput);
    form.appendChild(permitSelect);
    form.appendChild(imageInput);

    const type = fromEnumToStringProductType[product.productType];

    const factory = updateFactories[type];

    if (factory)
        {
            factory(product);
        }


    form.appendChild(submitButton);
}


async function changeProductCard(product) {
    const token = getCookie("AuthToken");
    if (!token) {
        alert("You are not authorized");
        return;
    }
    const payload = parseJwt(token);
    if (payload.role != "Admin") {
        alert("You do not have permission to edit products.");
        return;
    }
 const formData = new FormData(form);
    const res = await fetch(`/api/Product/${product.productType.toLowerCase()}`, {
        method: "PUT",
        headers: {
            "Authorization": `Bearer ${token}`,
          
        },
        
        body: formData
    })
    
    if (res.ok) {
        alert("Product updated successfully!");
        window.location.reload();
    } else {
        const error = await res.text();
        alert("Error updating product: " + error);
    }
}