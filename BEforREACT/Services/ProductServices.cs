using BEforREACT.Data;
using BEforREACT.Data.Entities;
using BEforREACT.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BEforREACT.Services
{
    public class ProductServices
    {
        private readonly DataContext _context;
        public ProductServices(DataContext context)
        {
            _context = context;

        }

        public async Task<List<ProductsDTO>> GetProduct()
        {

            var productItems = await (from product in _context.Products
                                      join detail in _context.ProductDetails
                                      on product.DetailID equals detail.DetailID
                                      select new ProductsDTO
                                      {
                                          ProductID = product.ProductID,
                                          DetailID = detail.DetailID,
                                          Name = product.Name,
                                          Src = product.Src,
                                          PreImg = product.PreImg,
                                          detailDes = detail.detailDes,
                                          Description = product.Description,
                                          Price = detail.Price,
                                          Stock = detail.Stock,
                                          Rating = detail.Rating,
                                          //CategoryID = detail.CategoryID,
                                          //BrandID = detail.BrandID
                                      }).ToListAsync();



            return productItems;
        }


        public async Task<ProductsDTO> CreateProduct(ProductsDTO productDto)
        {
            var productDetail = new ProductDetail
            {
                DetailID = Guid.NewGuid(),
                //CategoryID = productDto.CategoryID,
                //BrandID = productDto.BrandID,
                Price = productDto.Price,
                Stock = productDto.Stock,
                Rating = productDto.Rating,
                detailDes = productDto.detailDes
            };

            var product = new Product
            {
                ProductID = Guid.NewGuid(),
                DetailID = productDetail.DetailID,
                Name = productDto.Name,
                Src = productDto.Src,
                PreImg = productDto.PreImg,
                Description = productDto.Description
            };


            _context.Products.Add(product);
            _context.ProductDetails.Add(productDetail);
            await _context.SaveChangesAsync();

            productDto.ProductID = product.ProductID;
            //productDto.DetailID = productDetail.DetailID;
            return productDto;
        }
    }
}
