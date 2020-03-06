using System;
using System.Collections.Generic;
using System.Text;
using YK.Ship.BirdCommon;

namespace YKShip.Bird.OrderQuery
{
    public class OrderQueryRequest : AbstractModel
    {
        public string OrderCode { get; set; }
        public string ShipperCode { get; set; }
        public string LogisticCode { get; set; }
    }

    public class OrderQueryResponse : AbstractModel
    {
        public string EBusinessID { get; set; }
        public string OrderCode { get; set; }
        public string ShipperCode { get; set; }
        public string LogisticCode { get; set; }
        public string Success { get; set; }
        public string Reason { get; set; }
        public string State { get; set; }
        public List<TraceModel> Traces { get; set; }

        public class TraceModel
        {
            public string AcceptTime { get; set; }
            public string AcceptStation { get; set; }
            public string Remark { get; set; }
        }
    }
}
