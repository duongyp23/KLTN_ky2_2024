using KLTN.Common.Entity;
using KLTN.Common.Entity.DTO;
using KLTN.DataLayer;

namespace KLTN.BussinesLayer
{
    public class ProductBL : BaseBL<Product>, IProductBL
    {
        #region Field

        private IProductDL _productDL;
        private IProductCategoryDL _productCategoryDL;
        private IOrderDL _orderDL;
        private IOrderDetailDL _orderDetailDL;
        private IUserDL _userDL;

        #endregion

        #region Constructor

        public ProductBL(IProductDL productDL, IProductCategoryDL productCategoryDL, IOrderDL orderDL, IOrderDetailDL orderDetailDL, IUserDL userDL) : base(productDL)
        {
            _productDL = productDL;
            _productCategoryDL = productCategoryDL;
            _orderDL = orderDL;
            _orderDetailDL = orderDetailDL;
            _userDL = userDL;
        }

        public async Task<bool> AddProductToCart(Guid productId, Guid userId)
        {
            Product product = await _productDL.GetDataById(productId);
            if (product != null)
            {
                Order waitOrder = await _orderDL.GetWaitOrder(userId);
                if (waitOrder != null)
                {
                    OrderDetail orderDetail = new OrderDetail()
                    {
                        order_id = waitOrder.order_id,
                        product_id = productId,
                        product_code = product.product_code,
                        product_name = product.product_name,
                        product_deposit = product.sell_price,
                        product_payment = product.rental_price,
                        product_rental = product.rental_price,
                        product_return = product.sell_price - product.rental_price,
                        product_image_url = product.product_image_url,
                        order_type = 0,
                    };
                    await _orderDetailDL.Insert(orderDetail);
                }
                else
                {
                    User user = await _userDL.GetDataById(userId);
                    Order order = new Order()
                    {
                        user_id = userId,
                        phone_number = user.phone_number,
                        address = user.user_address,
                        status = 1,
                        payment_type = 0,
                    };
                    Guid orderId = await _orderDL.Insert(order);
                    OrderDetail orderDetail = new OrderDetail()
                    {
                        order_id = orderId,
                        product_id = productId,
                        product_code = product.product_code,
                        product_name = product.product_name,
                        product_deposit = product.sell_price,
                        product_payment = product.rental_price,
                        product_rental = product.rental_price,
                        product_return = product.sell_price - product.rental_price,
                        product_image_url = product.product_image_url,
                        order_type = 0,
                    };
                    await _orderDetailDL.Insert(orderDetail);
                }
                return true;
            }
            return false;
        }

        public async Task<Guid> InsertProduct(ProductData data)
        {
            Guid productId = await _productDL.Insert(data.product);
            if(data.selectCategory.Count > 0)
            {
                foreach (var item in data.selectCategory)
                {
                    ProductCategory productCategory = new ProductCategory()
                    {
                        product_id = productId,
                        product_code = data.product.product_code,
                        product_name = data.product.product_name,
                        category_id = (Guid)item.category_id,
                        category_code = item.category_code,
                    };
                    await _productCategoryDL.Insert(productCategory);
                }
            }

            return productId;
        }

        public async Task<bool> UpdateProduct(ProductData data)
        {
            bool update = await _productDL.Update(data.product);
            if (update && data.selectCategory.Count > 0)
            {
                await _productCategoryDL.RemoveOldCategory(data.product.product_id);
                foreach (var item in data.selectCategory)
                {
                    ProductCategory productCategory = new ProductCategory()
                    {
                        product_id = data.product.product_id,
                        product_code = data.product.product_code,
                        product_name = data.product.product_name,
                        category_id = (Guid)item.category_id,
                        category_code = item.category_code,
                    };
                    await _productCategoryDL.Insert(productCategory);
                }
            }
            return true;
        }

        #endregion
    }
}