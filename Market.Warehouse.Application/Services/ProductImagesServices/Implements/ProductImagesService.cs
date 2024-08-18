//using Market.Warehouse.Application.Dto;
//using Market.Warehouse.Application.Extensions;
//using Market.Warehouse.DataAccess.Repository.ProductImagesRepository;
//using Market.Warehouse.Domain.Models;
//using Market.Warehouse.ViewModel.ProductImage;

//namespace Market.Warehouse.Application.Services.ProductImagesServices;

//public class ProductImagesService : IProductImagesService
//{
//    private readonly IProductImagesRepository repository;

//    public ProductImagesService(IProductImagesRepository repository)
//    {
//        this.repository = repository;
//    }

//    public async Task<ProductImageBaseDto> Get(int id)
//    {
//        var image = await repository.GetByIdAsync(id);
//        return (ProductImageBaseDto)image;
//    }
//    public IQueryable<ProductImageBaseDto> GetList(BaseSortFilterDto dto)
//    {
//        var image = repository.GetAll().Select(x => (ProductImageBaseDto)x).SortFilter(dto);

//        return image;
//    }
//    public async Task<int> Create(ProductImageBaseDto dto)
//    {
//        var imageEntity = (ProductImage)dto;
//        var imageId = await repository.CreateAsync(imageEntity);

//        return imageId;
//    }

//    public async Task<int> Update(ProductImagesUpdateDto dto)
//    {
//        var imageEntity = (ProductImage)dto;
//        var imageId = await repository.UpdateAsync(imageEntity);

//        return imageId;
//    }
//    public async Task Delete(int id)
//    {
//        await repository.DeleteAsync(id);
//    }
//}
