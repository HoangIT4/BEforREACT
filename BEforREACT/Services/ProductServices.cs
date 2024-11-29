using BEforREACT.Data;
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
        public async Task<List<ProductItemViewDTO>> GetProductItemsAsync(ProductQuerryParams productParam)
        {
            var querry = _context.Products
                .Include(p => p.Brand)
                .AsQueryable();
            if (productParam.BrandID != Guid.Empty)
            {
                querry = querry.Where(p => p.BrandID == productParam.BrandID);
            }
            if (productParam.CategoryID != Guid.Empty)
            {
                querry = querry.Where(p => p.ProductCategories.Any(pc => pc.CategoryID == productParam.CategoryID));
            }



            if (!string.IsNullOrEmpty(productParam.Name))
            {
                querry = querry.Where(p => p.Name.Contains(productParam.Name));
            }


            if (productParam.IsHotDeal)
            {
                querry = querry.Where(p => p.Detail.IsHotDeal);
            }

            if (productParam.IsNew)
            {
                querry = querry.Where(p => p.Detail.IsNew);
            }

            if (productParam.IsBestSeller)
            {
                querry = querry.Where(p => p.Detail.IsBestSeller);
            }




            var products = await _context.Products
            .Include(p => p.Detail) // Bao gồm thông tin chi tiết từ bảng ProductDetail
            .ToListAsync();

            var result = products.Select(p => new ProductItemViewDTO
            {
                ProductID = p.ProductID,
                Name = p.Name,
                Src = p.Src,
                PreImg = p.PreImg,
                Price = p.Detail?.Price ?? 0,
                //FormattedPrice = p.Detail?.Price ?? 0,
            }).ToList();

            return result;
        }

        public async Task<ProductsDetailDTO> GetProductDetailAsync(Guid productId)
        {
            var product = await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Detail)
                .Include(p => p.ProductCategories)
                    .ThenInclude(pc => pc.Category)
                .FirstOrDefaultAsync(p => p.ProductID == productId);

            if (product == null)
            {
                return null;  // Nếu không tìm thấy sản phẩm
            }

            // Chuyển đổi dữ liệu thành DTO
            var productDetailDTO = new ProductsDetailDTO
            {
                ProductID = product.ProductID,
                Name = product.Name,
                Src = product.Src,
                PreImg = product.PreImg,
                detailDes = product.Detail?.detailDes,
                Description = product.Detail?.Description,
                Price = product.Detail?.Price ?? 0,
                Stock = product.Detail?.Stock ?? 0,
                Brands = new BrandDTO
                {
                    BrandID = product.Brand?.BrandID ?? Guid.Empty,
                    BrandName = product.Brand?.BrandName
                },
                Categories = product.ProductCategories?.Select(pc => new CategoryDTO
                {
                    CategoryID = pc.Category.CategoryID,
                    CategoryName = pc.Category.CategoryName
                }).ToList()
            };

            return productDetailDTO;
        }
        public bool AddProduct(ProductCreateRequest request)
        {
            var products = new Product()
            {
                ProductID = Guid.NewGuid(),
                Name = request.Name,
                //CategoryID = request.CategoryID,
                BrandID = request.BrandID,
                Src = request.Src,
                PreImg = request.PreImg,
            };

            var productDetails = new List<ProductDetail>();

            foreach (var item in request.ProductDetailsRequest)
            {
                var productDetail = new ProductDetail()
                {
                    ProductID = products.ProductID,
                    Description = item.Description,
                    detailDes = item.detailDes,
                    Price = (decimal)item.Price,
                    Stock = item.Stock
                };
                productDetails.Add(productDetail);
            }


            var productCategories = new List<ProductCategory>();
            foreach (var categoryId in request.CategoryIDs)
            {
                var productCategory = new ProductCategory()
                {
                    ProductID = products.ProductID,
                    CategoryID = categoryId
                };
                productCategories.Add(productCategory);
            }

            _context.Products.Add(products);
            _context.ProductDetails.AddRange(productDetails);
            _context.ProductCategories.AddRange(productCategories);
            _context.SaveChanges();
            return true;
        }

    }


}
