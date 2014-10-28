using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace DE0_NANO_UART
{
    class Program
    {


        static void Main(string[] args)
        {
            SerialPort  comport = new SerialPort("COM4");

            comport.BaudRate = 115200;
            comport.Parity = Parity.None;
            comport.StopBits = StopBits.One;
            comport.DataBits = 8;
            comport.Handshake = Handshake.None;
            comport.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

            if (!comport.IsOpen)
                comport.Open();
            else
                throw new Exception("已被開啟");


            ConsoleKeyInfo cki = Console.ReadKey();
            while (cki.Key != ConsoleKey.Enter)
            {
                Byte[] buffer = new Byte[1];
                buffer[0] = (Byte)cki.KeyChar;
                comport.Write(buffer, 0, buffer.Length);
                Console.WriteLine("\nSend Key = " + buffer[0]);

                cki = Console.ReadKey();
            }
            
            

            Console.Write("\nPress any key to exit...\n");
            Console.ReadKey();
            comport.Close();
        }

        static void DataReceivedHandler(
                        object sender,
                        SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            int indata = sp.ReadByte();
            String instr = Convert.ToString(indata, 16);
            //string indata = sp.ReadExisting();
            Console.WriteLine(String.Format("Data Received: {0:X}", indata));
            
        }


    }
}
