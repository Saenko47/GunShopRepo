using GunShopBackPart.DTOs;

namespace GunShopBackPart.Models
{
    public enum AccessoryType
    {
        Optics,         // Прицелы (оптика, коллиматоры и т.д.)
        Tactical,       // ЛЦУ, фонари
        Grip,           // Рукоятки, приклады
        Mount,          // Крепления, планки
        Muzzle,         // Дульные устройства
        Magazine,       // Магазины и аксессуары к ним
        Maintenance,    // Чистка, уход
        Carry,          // Кобуры, ремни, кейсы
        Safety,         // Предохранительные аксессуары
        Other
    }
    public class Accessorie:BaseProduct
    {
        public AccessoryType Type { get; set; }
        public override ProductType ProductType => ProductType.Accessory;



    }
}
