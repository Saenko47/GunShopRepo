
export function GetForm(typeOfProduct)
{
    switch(typeOfProduct)
    {
        case "product":
            return { html: GiveProductForm(), type: "products" };
            break;

        case "gun":
            return { html: GiveGunForm(), type: "guns" };
            break;

        case "ammo":
            return { html: GiveAmmoForm(), type: "ammos" };
            break;

        case "accesorie":
            return { html: GiveAccessoryForm(), type: "accessories" };
            break;

        default:
            return "";
            break;
    }
}

function GiveProductForm()
{
    return `

        <label>Min Price:</label>
        <input type="number" name="minPrice"  class="searchInputForFilter" />
        <br>
        <label>Max Price:</label>
        <input type="number" name="maxPrice"  class="searchInputForFilter" />
        <br>
        <label>Exact Price:</label>
        <input type="number" name="price" step="0.01"  class="searchInputForFilter" />
        <br>
        <label>Required Permit:</label>
        <select name="requiredPermit" class="searchInputForFilter">
            <option value="">None</option>
            <option value="ForPistol">ForPistol</option>
            <option value="ForRifle">ForRifle</option>
            <option value="ForShotgun">ForShotgun</option>
        </select>
        <br>
        <label>Supplier Name:</label>
        <input type="text" name="supplierName"  class="searchInputForFilter" />
         <br>
         <input type="text"
               class="searchInputForFilter"
               placeholder="Type name of product"
               name="name">
    `;
}

function GiveGunForm() {
    return `
       

            ${GiveProductForm()}
            <br>
            <label>Gun Type:</label>
            <select name="gunType" class="searchInputForFilter">
                <option value="">Any</option>
                <option value="Pistol">Pistol</option>
                <option value="Rifle">Rifle</option>
                <option value="Shotgun">Shotgun</option>
            </select>
            <br>
            <label>Caliber:</label>
            <select name="caliber" class="searchInputForFilter">
                <option value="">Any</option>
                <option value="Caliber9mm">9mm</option>
                <option value="Caliber45ACP">.45 ACP</option>
                <option value="Caliber556mm">5.56</option>
                <option value="Caliber762mm">7.62</option>
                <option value="Caliber12Gauge">12 Gauge</option>
            </select>

      
    `;
}

function GiveAmmoForm() {
    return `
      

            ${GiveProductForm()}
            <br>
            <label>Caliber:</label>
            <select name="caliber" class="searchInputForFilter">
                <option value="">Any</option>
                <option value="Caliber9mm">9mm</option>
                <option value="Caliber45ACP">.45 ACP</option>
                <option value="Caliber556mm">5.56</option>
                <option value="Caliber762mm">7.62</option>
                <option value="Caliber12Gauge">12 Gauge</option>
            </select>
            <br>
            <label>Quantity:</label>
            <input type="number" name="quantity" class="searchInputForFilter"/>

       
    `;
}

function GiveAccessoryForm() {
    return `
        

            ${GiveProductForm()}
             <br>
            <label>Accessory Type:</label>
            <select name="accessoryType" class="searchInputForFilter">
                <option value="">Any</option>
                <option value="Optics">Optics</option>
                <option value="Tactical">Tactical</option>
                <option value="Grip">Grip</option>
                <option value="Mount">Mount</option>
                <option value="Muzzle">Muzzle</option>
                <option value="Magazine">Magazine</option>
                <option value="Maintenance">Maintenance</option>
                <option value="Carry">Carry</option>
                <option value="Safety">Safety</option>
                <option value="Other">Other</option>
            </select>

        
    `;
}