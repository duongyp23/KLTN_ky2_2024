﻿using KLTN.Common.Entity;
using KLTN.Common.Entity.DTO;

namespace KLTN.BussinesLayer
{
    public interface IOrderBL : IBaseBL<Order>
    {
        Task<OrderData> GetOrderData(Guid id);
        Task<List<Order>> GetOrderOfUser(Guid id);
    }
}