using AutoMapper;
using Market.Warehouse.Domain.Models;

namespace E_CommerceProjectDemo.Application.Services.CartServices;

public class CartAutoMapperProfile : Profile
{
    public CartAutoMapperProfile()
    {
        //CreateMap<User, CartDetailsDto>()
        //    .ForMember(d => d.UserId, cfg => cfg.MapFrom(e => e.Id))
        //    .ForMember(d => d.TotalDistinctProducts, cfg => cfg.MapFrom(e => e.CartItems.Count))
        //    .ForMember(d => d.TotalProductCount, cfg => cfg.MapFrom(e => e.CartItems.Sum(x => x.Quantity)))

            //.ForMember(d => d.TotalDiscount, cfg => cfg.MapFrom(e =>
            //e.CartItems.Sum(ci => ci.Product != null &&
            //                ci.Product.Discount != null &&
            //                ci.Product.Discount.DiscountEndDate > DateTime.Now ?
            //                ci.Product.Price * ci.Product.Discount.DiscountRate / 100 * ci.Quantity : 0)))

            //.ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(e =>
            //e.CartItems.Sum(ci =>
            //                (ci.Product.Discount != null && ci.Product.Discount.DiscountEndDate > DateTime.Now ? (ci.Product.Price - (ci.Product.Price * ci.Product.Discount.DiscountRate / 100)) : ci.Product.Price) * ci.Quantity)));

        CreateMap<CartItem, CartItemDto>()
            .ForMember(d => d.ProductName, cfg => cfg.MapFrom(e => e.Product.Name))
            .ForMember(d => d.Price, cfg => cfg.MapFrom(e => e.Product.Price))
            .ForMember(d => d.DiscountPrice, cfg => cfg.MapFrom(e =>
            e.Product.Discount.ExpirationDate > DateTime.Now ?
            e.Product.Price - e.Product.Price * e.Product.Discount.InterestRate / 100 : e.Product.Price));

    }
}
