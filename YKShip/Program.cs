using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using YK.Ship.BirdCommon;
using YKShip.EOrderService;

namespace YKShip
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Class1.testquery();
            string text = File.ReadAllText(@"E:\workcode\test\YKShip\data.txt", Encoding.UTF8);

            var rsp = JsonConvert.DeserializeObject<EOrderServiceResponse>(text);

        }
    }
}
