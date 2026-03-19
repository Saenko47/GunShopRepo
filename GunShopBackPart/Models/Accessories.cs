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
    public class Accessories:BaseProduct
    {
        public string Name { get; set; } = string.Empty;
        public AccessoryType Type { get; set; }

        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; } = null!;

    }
}
