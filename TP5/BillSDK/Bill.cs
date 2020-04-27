using System;
using RPCSDK;
using UserSDK;
using StockSDK;
using System.Collections.Generic;

namespace BillSDK
{
    public class Bill
    {

        public User user { get; set; }

        public struct BillLine
        {
            public ItemLine item { get; set; }

            //sans taxe
            public int sousTotal { get; set; }
        }

    }

        public List<BillLine> billLines { get; set; }

        public int sousTotalSansTaxe { get; set; }
        public int TotalAvecTaxe { get; set; }

        public Bill()
        {
        }

        public static Bill CreateBill(User user, List<ItemLine> lines)
        {
            Bill bill = new Bill();            

            string[] res = new string[2];
            bill.sousTotalSansTaxe += 0;
            bill.TotalAvecTaxe += 0;


            var rpcClient = new RpcClient();

            foreach (ItemLine item in lines) 
            {
                Console.WriteLine(" [x] Requesting lines " + lines.);
                var response = rpcClient.Call(item.item.name + ":" 
                                            + item.item.prixUnitaire + ":"
                                            + item.quantity, "billqueue");

                //expected response : sousTotal(sans taxes):Total(avec taxes)
                Console.WriteLine(" [.] Got '{0}'", response);
                res = response.Split(":");

                //sous total sans taxe
                bill.billLines.Add(item, int.Parse(res[0]));

                bill.sousTotalSansTaxe += int.Parse(res[0]);
                bill.TotalAvecTaxe += int.Parse(res[1]);

            }           
          
            rpcClient.Close();

            bill.user = user;

            return bill;

        }
    }
}