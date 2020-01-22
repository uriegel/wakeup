using System;
using System.Linq;
using System.Net.Sockets;

namespace Aufwecker
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(
@"Wecke auf:
1. cas-w200109
2. cas-ws121013");
            var which = Console.ReadLine();
            var mäck = which switch
            {
                "1" => "00:d8:61:af:8a:0c",
                "2" => "18:03:73:36:0a:1a",
                _ => throw new Exception("Falscher Rechner!")
            };
            var mäckBytes = mäck.Split(':').Select(n => byte.Parse(n, System.Globalization.NumberStyles.HexNumber));
            var payload = Enumerable.Repeat<byte>(0xff, 6)
                .Concat(Enumerable.Repeat(mäckBytes, 16).SelectMany(n => n)).ToArray();
            var udp = new UdpClient(11000);
            udp.Send(payload, payload.Length, "255.255.255.255", 11000);
        }
    }
}
