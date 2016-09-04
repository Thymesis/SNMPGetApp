using System;
using SNMPLibrary;



namespace SNMPApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter ip-address: ");
            string ip = Console.ReadLine();
            Console.WriteLine();
            Console.Write("Enter community read string: ");
            string comm = Console.ReadLine();
            Console.WriteLine();
            snmp p = new snmp (comm, ip);
            p.snmpconnect();
        }
    }
}
