using System;
using System.Collections.Generic;
using System.Text;
using YK.Ship.BirdCommon;

namespace YKShip.EOrderService
{
    public class EOrderServiceResponse : AbstractModel
    {
        public class OrderModel
        {
            public string ShipperInfo { get; set; }
            public string LogisticCode { get; set; }
            public string DestinatioCode { get; set; }
            public string ShipperCode { get; set; }
            public string OrderCode { get; set; }
            public string KDNOrderCode { get; set; }
            public string OriginCode { get; set; }
        }

        public OrderModel Order { get; set; }

        public string PrintTemplate { get; set; }

        public string EBusinessID { get; set; }
        public string UniquerRequestNumber { get; set; }
        public string ResultCode { get; set; }
        public string Reason { get; set; }
        public string Success { get; set; }

      
    }
}
