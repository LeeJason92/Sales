using ProductServices.Context;
using ProductServices.Interfaces;
using ProductServices.Models;
using System.Transactions;

namespace ProductServices.Repositories
{
    public class ProductTypeRepository : IProductType
    {
        private ProductContext _productContext;

        public ProductTypeRepository(ProductContext productContext)
        {
            _productContext = productContext;
        }

        public async Task<ServiceResponse<string>> AddProductType(MProductType IProductType)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            try
            {
                await _productContext.AddAsync(IProductType);
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

        public async Task<ServiceResponse<string>> DeleteProductType(int ProductTypeId)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            try
            {
                var responseData = await GetById(ProductTypeId);
                _productContext.MProductTypes.Remove(responseData.Data);
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

        public async Task<ServiceResponse<string>> EditProductType(int ProductTypeId, MProductType mProductTypeNew)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            try
            {
                var responseData = await GetById(ProductTypeId);
                MProductType mProductTypeOld = responseData.Data;
                ModelHelper.CopyProperty(mProductTypeNew, ref mProductTypeOld);
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

        public async Task<IQueryable<MProductType>> GetAllProductType()
        {
            await Task.Yield();
            return _productContext.MProductTypes;
        }

        public async Task<ServiceResponse<MProductType>> GetById(int ProductTypeId)
        {
            ServiceResponse<MProductType> response = new ServiceResponse<MProductType>();
            try
            {
                MProductType ProductTypeData = _productContext.MProductTypes.Where(w => w.ProductTypeNo == ProductTypeId).FirstOrDefault();
                response.Data = ProductTypeData;

                if (ProductTypeData != null)
                {
                    response.TotalData = 1;
                }

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
