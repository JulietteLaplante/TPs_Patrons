using System;
using RPCSDK;
using UserSDK;
using StockSDK;
using System.Collections.Generic;
using System.Text.Json;

namespace BillSDK
{
    public class Bill
    {

        public User user { get; set; }

        public struct BillLine
        {

            public BillLine(ItemLine item, float total) : this()
            {
                this.item = item;
                this.subTotal = total;
            }

            public ItemLine item { get; set; }

            public float subTotal { get; set; }
        }

        public List<BillLine> billLines { get; set; }

        public float subTotalWithoutTaxes { get; set; }
        public float TotalWithTaxes { get; set; }

        public Bill()
        {
        }

        public static Bill CreateBill(User user, List<ItemLine> lines)
        {
            Bill bill = new Bill();
            string[] res = new string[2];

            var rpcClient = new RpcClient();

            Console.WriteLine(" Requesting Totals");
            var response = rpcClient.Call(JsonSerializer.Serialize(lines), "bill_queue");

            //expected response : subTotal(without taxes):Total(with taxes)
            rpcClient.Close();

            res = response.Split(":");

            foreach (ItemLine item in lines)
            {
                bill.billLines.Add(new BillLine(item, item.item.price * item.quantity));
            }

            bill.subTotalWithoutTaxes = float.Parse(res[0]);
            bill.TotalWithTaxes = float.Parse(res[1]);

            bill.user = user;

            return bill;

        }
    }
}