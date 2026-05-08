
const productFactories = {
    Gun: CreateGun,
    Accessory: CreateAccessory,
    Ammo: CreateAmmo
};


const fromEnumToStringProductType = 
{
    0 : "Gun",
    1: "Accessory",
    2: "Ammo",
    3: "None"
};
const fromEnumToStringRequiredPermit = 
{
    0 : "ForPistol",
    1 : "ForRifle",
    2 : "ForShotgun",
    3 : "None"
};

const fromEnumToStringCaliber = {
    0: "Caliber9mm",
    1: "Caliber45ACP",
    2: "Caliber556mm",
    3: "Caliber762mm",
    4: "Caliber12Gauge"
};

const fromEnumToStringGunType = {
    0: "Pistol",
    1: "Rifle",
    2: "Shotgun"
};

const fromEnumToStringAccessoryType =
{
    0: "Optics",
    1: "Tactical",
    2: "Grip",
    3: "Mount",
    4: "Muzzle",
    5: "Magazine",
    6: "Maintenance",
    7: "Carry",
    8: "Safety",
    9: "Other"
};

function GetProductsForRender(data)
{
    const result = [];

    for (const product of data)
    {
        const created = CreateProduct(product);
        if (created) {
            result.push(created);
        }
    }

    return result;
}

function CreateProduct(data)
{
    if(!data || data?.productType == null) return null;
    let type = fromEnumToStringProductType[data.productType];
    const factory = productFactories[type];

    return factory ? factory(data) : CreateBase(data);

}

function CreateGun(data)
{
 const gun = CreateBase(data);
    if (!gun) return null;

    if (data.caliber == null || data.gunType == null) return null;

    return {
        ...gun,
        caliber: fromEnumToStringCaliber[data.caliber] ?? "Unknown",
        gunType: fromEnumToStringGunType[data.gunType] ?? "Unknown"
    };
 
}
function CreateAmmo(data)
{
const ammo = CreateBase(data)
    if(!ammo) return null;
    if (data.caliber == null || data.amountInBox == null) return null;
    return {
        ...ammo,
        caliber: fromEnumToStringCaliber[data.caliber] ?? "Unknown",
        amountInBox: data.amountInBox
    };
}
function CreateAccessory(data)
{
const accessory = CreateBase(data)
    if(!accessory) return null;
    if (data.type == null) return null;
    return {
        ...accessory,
        type: fromEnumToStringAccessoryType[data.type] ?? "Unknown"
       
    };
}

function CreateBase(data)
{
    if(!data) return null;
    const {
        id,
        name,
        price,
        supplierName,
        requiredPermit,
        imageUrl,
        productType
    } = data;

    if (id == null || name == null || price == null) {
        return null;
    }

    return {
        id,
        name,
        price,
        supplierName,
        imageUrl,
        requiredPermit: fromEnumToStringRequiredPermit[requiredPermit] ?? "Unknown",
        productType: fromEnumToStringProductType[productType] ?? "Unknown"
    };
}