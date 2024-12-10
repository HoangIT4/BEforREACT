using BEforREACT.Data;
using BEforREACT.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BEforREACT.Services
{
    public class ProductServices
    {
        private readonly DataContext _context;
        private FileStorageServices _fileStorageServices;
        public ProductServices(DataContext context, FileStorageServices fileStorageServices)
        {
            _context = context;
            _fileStorageServices = fileStorageServices;
        }
        public async Task<List<ProductItemViewDTO>> GetProductItemsAsync(ProductQuerryParams productParam)
        {
            var querry = _context.Products
                .Include(p => p.Brand)
                .Include(p => p.ProductCategories) // Bao gồm danh mục sản phẩm
                .ThenInclude(pc => pc.Category) // Bao gồm thông tin danh mục từ ProductCategories
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

            var result = products
                .GroupBy(p => p.ProductID)
                .Select(g => new ProductItemViewDTO
                {
                    ProductID = g.Key,
                    Name = g.First().Name,
                    Src = g.First().Src,
                    PreImg = g.First().PreImg,
                    Price = g.First().Detail?.Price ?? 0,
                    Categories = g.SelectMany(p => p.ProductCategories)
                      .Select(pc => pc.Category.CategoryName)
                      .Distinct()
                      .ToList(),
                }).ToList();

            return result;
        }


        public async Task<List<ProductItemViewDTO>> GetAllProducts()
        {
            var querry = _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Detail)
                .Include(p => p.ProductCategories)
                    .ThenInclude(pc => pc.Category)
                .AsQueryable();

            var products = await querry.ToListAsync();

            var result = products.Select(p => new ProductItemViewDTO
            {
                ProductID = p.ProductID,
                Name = p.Name,
                Src = p.Src,
                PreImg = p.PreImg,
                Price = p.Detail?.Price ?? 0,
                Stock = p.Detail?.Stock ?? 0,
                IsBestSeller = p.Detail?.IsBestSeller ?? false,
                IsHotDeal = p.Detail?.IsHotDeal ?? false,
                IsNew = p.Detail?.IsNew ?? false,
                Brands = new BrandDTO
                {
                    BrandID = p.Brand?.BrandID ?? Guid.Empty,
                    BrandName = p.Brand?.BrandName
                },
                Categories = p.ProductCategories?.Select(pc => pc.Category.CategoryName).ToList(),


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
                return null;
            }


            var productDetailDTO = new ProductsDetailDTO
            {
                ProductID = product.ProductID,
                Name = product.Name,
                Src = product.Src,
                PreImg = product.PreImg,
                detailDes = product.Detail?.detailDes,
                Description = product.Detail?.Description,
                IsBestSeller = product.Detail?.IsBestSeller ?? false,
                IsHotDeal = product.Detail?.IsHotDeal ?? false,
                IsNew = product.Detail?.IsNew ?? false,
                Weight = product.Detail?.Weight,
                Origin = product.Detail?.Origin,
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
        public async Task<bool> AddProductAsync(ProductCreateRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "Product creation request cannot be null.");
            }

            // Initialize the product object
            var products = new Product()
            {
                ProductID = Guid.NewGuid(),
                Name = request.Name,
                BrandID = request.BrandID,
                Src = string.Empty, // Initialize for the image path
                PreImg = string.Empty // Initialize for the preview image path
            };

            // Handle image uploads
            if (request.Src != null)
            {
                products.Src = await _fileStorageServices.UploadImageFileAsync(request.Src, "products");
            }

            if (request.PreImg != null)
            {
                products.PreImg = await _fileStorageServices.UploadImageFileAsync(request.PreImg, "products");
            }

            // Create product details
            var productDetails = new List<ProductDetail>();
            foreach (var item in request.ProductDetailsRequest)
            {
                var productDetail = new ProductDetail()
                {
                    ProductID = products.ProductID,
                    Description = item.Description,
                    detailDes = item.detailDes,
                    Price = (decimal)item.Price,
                    IsBestSeller = item.IsBestSeller,
                    IsHotDeal = item.IsHotDeal,
                    IsNew = item.IsNew,
                    Stock = item.Stock,
                    Weight = item.Weight,
                    Origin = item.Origin,
                };
                productDetails.Add(productDetail);
            }

            // Create product categories
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

            // Add to the context and save changes
            _context.Products.Add(products);
            _context.ProductDetails.AddRange(productDetails);
            _context.ProductCategories.AddRange(productCategories);

            try
            {
                await _context.SaveChangesAsync(); // Use asynchronous save
            }
            catch (Exception ex)
            {
                // Handle the exception as needed
                throw new Exception("Failed to save product to the database.", ex);
            }

            return true;
        }


        public async Task<bool> UpdateProductAsync(Guid productId, ProductsDetailDTO request)
        {
            var product = await _context.Products
                .Include(p => p.Detail)
                .Include(p => p.ProductCategories)
                .FirstOrDefaultAsync(p => p.ProductID == productId);

            if (product == null)
                return false; // Sản phẩm không tồn tại

            if (!string.IsNullOrEmpty(request.Name))
                product.Name = request.Name;

            if (!string.IsNullOrEmpty(request.Src))
                product.Src = request.Src;

            if (!string.IsNullOrEmpty(request.PreImg))
                product.PreImg = request.PreImg;

            if (request.Price > 0 && product.Detail != null)
                product.Detail.Price = request.Price;

            if (!string.IsNullOrEmpty(request.Description) && product.Detail != null)
                product.Detail.Description = request.Description;

            if (!string.IsNullOrEmpty(request.detailDes) && product.Detail != null)
                product.Detail.detailDes = request.detailDes;

            if (request.Stock > 0 && product.Detail != null)
                product.Detail.Stock = request.Stock;

            if (request.IsBestSeller != product.Detail?.IsBestSeller && product.Detail != null)
                product.Detail.IsBestSeller = request.IsBestSeller;

            if (request.IsHotDeal != product.Detail?.IsHotDeal && product.Detail != null)
                product.Detail.IsHotDeal = request.IsHotDeal;

            if (request.IsNew != product.Detail?.IsNew && product.Detail != null)
                product.Detail.IsNew = request.IsNew;

            // Cập nhật danh mục sản phẩm (nếu có)
            if (request.Categories != null && request.Categories.Any())
            {
                _context.ProductCategories.RemoveRange(product.ProductCategories); // Xóa danh mục cũ

                foreach (var category in request.Categories)
                {
                    _context.ProductCategories.Add(new ProductCategory
                    {
                        ProductID = product.ProductID,
                        CategoryID = category.CategoryID
                    });
                }
            }

            // Lưu thay đổi vào cơ sở dữ liệu
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<bool> DeleteProductAsync(Guid productID)
        {
            var product = await _context.Products
                .Include(p => p.Detail)
                .Include(p => p.ProductCategories)
                .FirstOrDefaultAsync(p => p.ProductID == productID);

            if (product == null)
                return false;

            _context.ProductCategories.RemoveRange(product.ProductCategories);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }


    }



}
