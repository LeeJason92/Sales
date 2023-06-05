using ProductServices.Context;
using ProductServices.Interfaces;
using ProductServices.Models;
using System.Transactions;

namespace ProductServices.Repositories
{
    public class ProductRepository : IProduct
    {
        private ProductContext _productContext;

        public ProductRepository(ProductContext productContext)
        {
            _productContext = productContext;
        }

        public async Task<ServiceResponse<string>> AddProduct(MProduct IProduct)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            try
            {
                await _productContext.AddAsync(IProduct);
                Commit();
                response.Data = "Save Success";
                response.Success();
            }
            catch (Exception e)
            {
                response.Fail(e);
            }

            return response;

        }

        public void Commit()
        {
            _productContext.SaveChanges();
        }

        public async Task<ServiceResponse<string>> DeleteProduct(int ProductId)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            try
            {
                var responseData = await GetById(ProductId);
                _productContext.MProducts.Remove(responseData.Data);
                Commit();
                response.Data = "Delete Success";
                response.Success();
            }
            catch (Exception e)
            {
                response.Fail(e);
            }

            return response;
        }

        public async Task<ServiceResponse<string>> EditProduct(int ProductId, MProduct mProductNew)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            try
            {
                var responseData = await GetById(ProductId);
                MProduct mProductOld = responseData.Data;
                ModelHelper.CopyProductProperty(mProductNew, ref mProductOld);
                Commit();
                response.Data = "Edit Success";
                response.Success();
            }
            catch (Exception e)
            {
                response.Fail(e);
            }
            await Task.Yield();

            return response;
        }

        public async Task<ServiceResponse<string>> EditProductStock(int ProductId, int countUsed)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            try
            {
                var responseData = await GetById(ProductId);
                MProduct mProduct = responseData.Data;
                mProduct.Stock -= Math.Abs(countUsed); 
                
                Commit();
                response.Data = "Edit Stock Success";
                response.Success();
            }
            catch (Exception e)
            {
                response.Fail(e);
            }
            await Task.Yield();

            return response;
        }

        public async Task<IQueryable<MProduct>> GetAllProduct()
        {
            await Task.Yield();
            return _productContext.MProducts;
        }

        public async Task<ServiceResponse<MProduct>> GetById(int ProductId)
        {
            ServiceResponse<MProduct> response = new ServiceResponse<MProduct>();
            try
            {
                MProduct ProductData = _productContext.MProducts.Where(w => w.ProductNo == ProductId).FirstOrDefault();
                response.Data = ProductData;

                if (ProductData != null)
                {
                    response.TotalData = 1;
                }

                await Task.Yield();
                response.Success();
            }
            catch (Exception e)
            {
                response.Fail(e);
            }

            return response;
        }

        public async Task<ServiceResponse<List<MProduct>>> GetByDesc(string keyword)
        {
            ServiceResponse<List<MProduct>> response = new ServiceResponse<List<MProduct>>();
            try
            {
                List<MProduct> ProductData = _productContext.MProducts.Where(w => w.ProductDesc.Trim().ToLower().Contains(keyword.Trim().ToLower())).ToList();
                response.Data = ProductData;
                response.TotalData = ProductData.Count;

                await Task.Yield();
                response.Success();
            }
            catch (Exception e)
            {
                response.Fail(e);
            }

            return response;
        }

    }
}
