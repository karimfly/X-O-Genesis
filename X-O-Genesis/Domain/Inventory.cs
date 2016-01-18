﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetvetPOS_Inventory_System
{
    public class Inventory : IEntity
    {
        public int InventoryId { get; set; }
        public string Barcode { get; set; }
        public DateTime StockinDateTime { get; set; }
        public int QtyReceived { get; set; }
        public string Supplier { get; set; }
        public int QtyOnHand { get; set; }

        public Inventory()
        {

        }

        public Inventory(Entity entity, bool isFromInventoryView = false)
        {
            try
            {
                if (isFromInventoryView)
                {
                    StockinDateTime = (DateTime)entity.getField("Stock_since");
                    QtyOnHand = (int)entity.getField("Qty_on_Hand");
                    Supplier = (string)entity.getField("Supplier");
                }
                else
                {
                    Barcode = (string)entity.getField("product_id");
                    StockinDateTime = (DateTime)entity.getField("stockin_datetime");
                    QtyReceived = (int)entity.getField("qty_received");
                    QtyOnHand = (int)entity.getField("qty_onhand");
                    Supplier = (string)entity.getField("supplier");
                }

            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
            }
        }

    }
}
