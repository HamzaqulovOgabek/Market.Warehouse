﻿namespace Market.Warehouse.Application.Services.CartServices;

public class CartItemDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}