﻿// Copyright (c) 2017 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT licence. See License.txt in the project root for license information.

using System.Collections.Immutable;
using System.Linq;
using BizDbAccess.Orders;
using DataLayer.EfClasses;
using GenericBizRunner;

namespace BizLogic.Orders.Concrete
{
    public class PlaceOrderPart1 : BizActionStatus, IPlaceOrderPart1
    {
        private readonly IPlaceOrderDbAccess _dbAccess;

        public PlaceOrderPart1(IPlaceOrderDbAccess dbAccess)
        {
            _dbAccess = dbAccess;
        }

        public Part1ToPart2Dto BizAction(PlaceOrderInDto dto)           
        {
            if (!dto.AcceptTAndCs)
                AddError("You must accept the T&Cs to place an order.");

            if (!dto.LineItems.Any())
                AddError("No items in your basket.");

            var order = new Order                          
            {                                              
                CustomerName = dto.UserId      
            };                                             

            if (!HasErrors)                                
                _dbAccess.Add(order);

            return new Part1ToPart2Dto(dto.LineItems.ToImmutableList(), order);
        }
    }
}