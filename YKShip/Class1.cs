using Newtonsoft.Json;
using System;
using YK.Ship.BirdCommon;
using YK.Ship.BirdCommon.Profile;
using YKShip.Bird.OrderQuery;
using YKShip.BirdCommon;
using YKShip.EOrderService;

namespace YKShip
{
    public class Class1
    {
        public static void test()
        {
            var req = new EOrderServiceRequest();
            req.OrderCode = "20034343";
            req.ShipperCode = ShipCompany.SF;
            req.PayType = 1;
            req.ExpType = 1;
            req.Sender = new EOrderServiceRequest.SenderModel()
            {
                Company = "LV",
                Name = "LIUHUA",
                Mobile = "13969777534",
                Tel = "",
                ProvinceName = "上海",
                CityName = "上海市",
                Address = "明珠路73号",
            };
            req.Receiver = new EOrderServiceRequest.SenderModel()
            {
                Company = "LV",
                Name = "LIUHUA",
                Mobile = "13969777534",
                Tel = "",
                ProvinceName = "上海",
                CityName = "上海市",
                Address = "明珠路73号",
            };
            req.Commodity = new System.Collections.Generic.List<EOrderServiceRequest.CommodityModel>();
            req.Commodity.Add(new EOrderServiceRequest.CommodityModel
            {
                GoodsName = "鞋子",
                Goodsquantity = 1,
            });
            req.Weight = 1;
            req.Quantity = 1;

            req.IsReturnPrintTemplate = 1;

            var st = JsonConvert.SerializeObject(req);

            //Credential credential = new Credential()
            //{
            //    SecretId = "1321750",
            //    SecretKey = "73e7c4e2-00f9-4636-a5ce-6dc7d2ac814e",
            //};

            Credential credential = new Credential()
            {
                SecretId = "test1321750",
                SecretKey = "738881f4-fa19-4e11-bae8-e5877ed8d1d4",
            };

            ClientProfile profile = new ClientProfile();

            EOrderServiceClient client = new EOrderServiceClient(credential, profile, true);

            var rep = client.EOrderService(req);




            Console.WriteLine(st);

            //req.Volume = 1;
        }

        public static void testquery()
        {
            var req = new OrderQueryRequest() { ShipperCode = "SF", LogisticCode = "1234561" };
            Credential credential = new Credential()
            {
                SecretId = "test1321750",
                SecretKey = "738881f4-fa19-4e11-bae8-e5877ed8d1d4",
            };

            var client = new OrderQueryClient(credential, new ClientProfile(), true);
            var rsp = client.Query(req);



        }

    }
}
