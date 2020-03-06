using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YK.Ship.BirdCommon;
using YK.Ship.BirdCommon.Profile;

namespace YKShip.EOrderService
{
    public class EOrderServiceClient : AbstractClient
    {
        /// <summary>
        /// Client Constructor.
        /// </summary>
        /// <param name="credential">Credentials.</param>
        /// <param name="region">Region name, such as "ap-guangzhou".</param>
        /// <param name="profile">Client profiles.</param>
        public EOrderServiceClient(Credential credential, ClientProfile profile, bool isDebug)
            : base(credential, profile)
        {
            if (!isDebug)
            {
                this.Path = "http://api.kdniao.com/api/EOrderService";
              
            }
            else
            {
                this.Path = "http://sandboxapi.kdniao.com:8080/kdniaosandbox/gateway/exterfaceInvoke.json";
            }
        }




        public EOrderServiceResponse EOrderService(EOrderServiceRequest req)
        {

            EOrderServiceResponse rsp = null;
            try
            {
                var strResp = this.InternalRequestSync(req, RequestType.RT_EOS);
                Console.WriteLine(strResp);
                rsp = JsonConvert.DeserializeObject<EOrderServiceResponse>(strResp);
            }
            catch (JsonSerializationException e)
            {
                throw new Exception(e.Message);
            }
            return rsp;
        }


    }
}
