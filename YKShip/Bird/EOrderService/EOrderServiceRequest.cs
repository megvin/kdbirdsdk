using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using YK.Ship.BirdCommon;

namespace YKShip.EOrderService
{
    public class EOrderServiceRequest : AbstractModel
    {
        [JsonProperty("OrderCode")]
        public string OrderCode { get; set; }

        [JsonProperty("ShipperCode")]
        public string ShipperCode { get; set; }

        [JsonProperty("PayType")]
        public int PayType { get; set; }

        [JsonProperty("ExpType")]
        public int ExpType { get; set; }


        [JsonProperty("Sender")]
        public SenderModel Sender { get; set; }

        public double Weight { get; set; }

        public string Remark { get; set; }

        public int IsReturnPrintTemplate { get; set; }

        public int Quantity { get; set; }

        public class SenderModel
        {

            public string Company { get; set; }
            public string Name { get; set; }
            public string Tel { get; set; }
            public string Mobile { get; set; }
            public string PostCode { get; set; }
            /// <summary>
            /// 直辖市，上海，北京，不带市
            /// </summary>
            public string ProvinceName { get; set; }
            /// <summary>
            /// 直辖市，带市北京市
            /// </summary>
            public string CityName { get; set; }
            public string Address { get; set; }
        }

        [JsonProperty("Receiver")]
        public SenderModel Receiver { get; set; }

        public class ReceiverModel
        {
            public string Company { get; set; }
            public string Name { get; set; }
            public string Tel { get; set; }
            public string Mobile { get; set; }
            public string PostCode { get; set; }
            public string ProvinceName { get; set; }
            public string CityName { get; set; }
            public string Address { get; set; }
        }

        public List<AddServiceModel> AddService { get; set; }

        public class AddServiceModel
        {

            public string Name { get; set; }
            public string Value { get; set; }
        }


        public List<CommodityModel> Commodity { get; set; }

        public class CommodityModel
        {
            [JsonProperty("GoodsName")]
            public string GoodsName { get; set; }

            public int Goodsquantity { get; set; }
        }


       
    }


}
