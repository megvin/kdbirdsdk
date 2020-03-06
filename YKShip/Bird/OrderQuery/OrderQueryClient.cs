using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using YK.Ship.BirdCommon;
using YK.Ship.BirdCommon.Profile;
using YKShip.EOrderService;

namespace YKShip.Bird.OrderQuery
{
    public class OrderQueryClient: AbstractClient
    {
      

        /// <summary>
        /// Client Constructor.
        /// </summary>
        /// <param name="credential">Credentials.</param>
        /// <param name="region">Region name, such as "ap-guangzhou".</param>
        /// <param name="profile">Client profiles.</param>
        public OrderQueryClient(Credential credential, ClientProfile profile,bool isDebug)
            : base(credential, profile)
        {
            if (isDebug)
            {
                this.Path = "http://api.kdniao.com/api/dist";
            }
            else
            {
                this.Path = "http://sandboxapi.kdniao.com:8080/kdniaosandbox/gateway/exterfaceInvoke.json";
            }
        }
        public OrderQueryResponse Query(OrderQueryRequest req)
        {

            OrderQueryResponse rsp = null;
            try
            {
                var strResp = this.InternalRequestSync(req, RequestType.RT_Query);
                Console.WriteLine(strResp);
                rsp = JsonConvert.DeserializeObject<OrderQueryResponse>(strResp);
            }
            catch (JsonSerializationException e)
            {
                throw new Exception(e.Message);
            }
            return rsp;
        }
    }
}
