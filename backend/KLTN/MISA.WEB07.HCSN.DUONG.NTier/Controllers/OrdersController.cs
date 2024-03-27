﻿using KLTN.BussinesLayer;
using KLTN.Common.Entity;
using KLTN.Common.Entity.DTO;
using KLTN.Common.Enums;
using KLTN.NTier.Base;
using Microsoft.AspNetCore.Mvc;

namespace KLTN.NTier.Controllers
{
    public class OrdersController : BasesController<Order>
    {
        #region Field

        private IOrderBL _orderBL;

        #endregion

        #region Constructor

        public OrdersController(IOrderBL orderBL) : base(orderBL)
        {
            _orderBL = orderBL;
        }

        #endregion

        #region Method

        [HttpGet("{id}")]

        public override async Task<IActionResult> GetDataById(Guid id)
        {
            try
            {
                OrderData record = await _orderBL.GetOrderData(id);
                return StatusCode(StatusCodes.Status200OK, record);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ErrorCode.Exception);
            }
        }

        [HttpGet("GetOrderOfUser/{id}")]

        public async Task<IActionResult> GetOrderOfUser(Guid id)
        {
            try
            {
                List<Order> records = await _orderBL.GetOrderOfUser(id);
                return StatusCode(StatusCodes.Status200OK, records);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ErrorCode.Exception);
            }
        }

        #endregion
    }
}
