﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetvetPOS_Inventory_System
{
    public class InventoryMapper : DatabaseMapper
    {
        public InventoryMapper(MySqlDB mySqlDb)
            : base(mySqlDb)
        {
            tableName = "inventory_tbl";
            id = "inventory_id";
            fieldsname = new string[] {
                "barcode",
                "stockin_date",
                "qty_received",
                "qty_onhand",
            };

            fieldsname_forselect = new string[]{
                "barcode AS Barcode",
                "stockin_date AS 'Stock since'",
                "qty_received AS 'Qty on Received'",
                "qty_onhand AS 'Qty on Hand'"
            };
        }

        public string createInventory(Inventory inventory)
        {
            return insertValues(
                inventory.Barcode, "NOW()", inventory.QtyReceived,
                inventory.QtyOnHand);
        }

        public bool pullInventory(Inventory inventory)
        {
            return create(
                insertValues(inventory.Barcode, "null", "null",
                inventory.QtyOnHand));
        }


        public DataTable loadInventoryTable(DataTable dt)
        {
            return loadTable(dt);
        }

        public bool insertInventory(Inventory inventory)
        {
            return create(createInventory(inventory));
        }

  
    }
}