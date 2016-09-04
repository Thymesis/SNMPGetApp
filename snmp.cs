using System;
using SnmpSharpNet;
using System.Net;

namespace SNMPLibrary
{
    public class snmp
    {
        String community;
        String ipaddr;
        int numint;

        public void snmpconnect()
        {
            OctetString snmpcommunity = new OctetString(community);
            AgentParameters param = new AgentParameters(snmpcommunity);
            param.Version = SnmpVersion.Ver2;
            IpAddress agent = new IpAddress(ipaddr);
            UdpTarget target = new UdpTarget((IPAddress)agent, 161, 2000, 1);
            Pdu pdu = new Pdu(PduType.Get);
            Pdu pdu1 = new Pdu(PduType.Get);
            Pdu pdu2 = new Pdu(PduType.Get);
            Pdu pdu3 = new Pdu(PduType.Get);
            Pdu pdu4 = new Pdu(PduType.Get);
            Pdu pdu5 = new Pdu(PduType.Get);
            pdu.VbList.Add(".1.3.6.1.2.1.47.1.1.1.1.13.1"); //model
            pdu.VbList.Add(".1.3.6.1.2.1.1.3.0"); //uptime
            pdu.VbList.Add(".1.3.6.1.2.1.2.1.0"); // number of interfaces
            pdu.VbList.Add(".1.3.6.1.2.1.47.1.1.1.1.10.1"); // IOS
            pdu.VbList.Add(".1.3.6.1.2.1.1.5.0"); //hostname
     
            SnmpV2Packet result = (SnmpV2Packet)target.Request(pdu, param);

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Model name: ");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine (result.Pdu.VbList[0].Value.ToString());

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("IOS: ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(result.Pdu.VbList[3].Value.ToString());

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Uptime: ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(result.Pdu.VbList[1].Value.ToString());

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Hostname: ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(result.Pdu.VbList[4].Value.ToString());


            numint = int.Parse(result.Pdu.VbList[2].Value.ToString());
            
            for (int i=0; i<numint; i++)
            {
                
                pdu1.VbList.Add(".1.3.6.1.2.1.2.2.1.2." + (i+1)); // name interface
                pdu2.VbList.Add(".1.3.6.1.2.1.2.2.1.7." + (i+1)); // admin state
                pdu3.VbList.Add(".1.3.6.1.2.1.2.2.1.8." + (i+1)); // link
                pdu4.VbList.Add(".1.3.6.1.2.1.2.2.1.14." + (i+1)); // input errors
                pdu5.VbList.Add(".1.3.6.1.2.1.2.2.1.20." + (i+1)); // output errors
                SnmpV2Packet result1 = (SnmpV2Packet)target.Request(pdu1, param);
                SnmpV2Packet result2 = (SnmpV2Packet)target.Request(pdu2, param);
                SnmpV2Packet result3 = (SnmpV2Packet)target.Request(pdu3, param);
                SnmpV2Packet result4 = (SnmpV2Packet)target.Request(pdu4, param);
                SnmpV2Packet result5 = (SnmpV2Packet)target.Request(pdu5, param);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Interface: ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(result1.Pdu.VbList[i].Value.ToString());

                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("       -- link: ");
                Console.ForegroundColor = ConsoleColor.Green;
                if (Convert.ToInt32(result3.Pdu.VbList[i].Value.ToString()) == 1) Console.WriteLine("up");
                else { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("down"); }
                
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("       -- admin status: ");
                Console.ForegroundColor = ConsoleColor.Green;
                if (Convert.ToInt32(result2.Pdu.VbList[i].Value.ToString()) == 1) Console.WriteLine("up");
                else { Console.ForegroundColor = ConsoleColor.Red; Console.Write("down"); }

                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("       -- input errors(CRC): ");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(result4.Pdu.VbList[i].Value.ToString());

                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("       -- output errors: ");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(result5.Pdu.VbList[i].Value.ToString());

            }
            Console.ReadLine();
  }

       public snmp(string snmpcommunity, string ipaddr) // constructor
        {
            this.community = snmpcommunity;
            this.ipaddr = ipaddr;
        }
       

    }
}
