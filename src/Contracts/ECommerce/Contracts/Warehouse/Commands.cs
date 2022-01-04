﻿using System;
using ECommerce.Abstractions.Commands;

namespace ECommerce.Contracts.Warehouse;

public static class Commands
{
    public record ReceiveInventoryItem(string Sku, string Name, string Description, int Quantity) : Command;

    public record AdjustInventory(Guid ProductId, string Sku, int Quantity) : Command;

    public record ReserveInventory(Guid ProductId, Guid CartId, string Sku, int Quantity) : Command;
}