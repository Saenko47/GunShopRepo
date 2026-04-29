using GunShopBackPart.DTOs;
using GunShopBackPart.Models;

namespace GunShopBackPart.Tool.DTO
{
    public static class CustomerDTOHelper
    {
        public static CustomerDTO ToCustomerDTO(this Customer customer)
        {
            var dto = new CustomerDTO
            {
                Name = customer.Name,
                Surname = customer.Surname,
                Login = customer.Login,
                gmail = customer.gmail,
                PhoneNumber = customer.PhoneNumber,
                CreatedAt = customer.CreatedAt,
                Licens = customer.Licenses.Select(l => l.PermitType.ToString()).ToList()
            };
            return dto;
        }
    }
}