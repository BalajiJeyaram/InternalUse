
using Honeywell.Connection;
using System.IO;
using Honeywell.Printer;
using System.Linq;
using Honeywell.Printer.Configuration;
using Honeywell.Printer.Configuration.DPL;
using Honeywell.Printer.Configuration.ExPCL;
using Honeywell.Printer.Configuration.EZ;
using System.Collections.Generic;
using InTheHand.Net.Sockets;
using System;
using System.Drawing;

namespace Intermac_Printer_SDK
{
    class Program
    {
        //ConnectionBase.PrinterResponse printerResponse = ConnectionBase.PrinterResponse.DONE;
        static void Main(string[] args)
        {

            //try {
            //    BluetoothClient BT = new BluetoothClient();
            //    BluetoothDeviceInfo[] devices = BT.DiscoverDevicesInRange();
            //    foreach (BluetoothDeviceInfo d in devices)
            //        Console.WriteLine(d.DeviceName);

            //    Console.ReadLine();
            //}
            //catch (Exception ex) {
            //    Console.WriteLine(ex.StackTrace);
            //    Console.ReadLine();
            //}

            SendDPLCommandsToPrinter();
            /*
            //Bluetooth connection
            ConnectionBase conn =
                (Connection_Bluetooth32Feet)Connection_Bluetooth32Feet.CreateClient("84253F441187","0000");
            conn.Open();

            //TCP/IP Connection
            //ConnectionBase conn1 =
                //(Connection_TCP)Connection_TCP.CreateClient();
            //conn1.Open();

            //following code creates a printing job
            //====DPL Printers(eg. RL3, RL4, etc.)========//
            DocumentDPL docDPL = new DocumentDPL();
            ParametersDPL paramDPL = new ParametersDPL();

            //====Legacy Printers (OC2, OC3, MF4Te, etc.)========//
            DocumentEZ docEZ = new DocumentEZ("MF204");//EZ mode. MF204 is the font name
            ParametersEZ paramEZ = new ParametersEZ();

            DocumentLP docLP = new DocumentLP("!"); //LinePrint mode.“!” is the font name
            ParametersLP paramLP = new ParametersLP();

            //====Apex Printers(Apex 2, Apex 3, etc..)========//
            DocumentExPCL_LP docExPCL_LP = new DocumentExPCL_LP(3); //Line Print mode. “3” is the font index.
            ParametersExPCL_LP paramExPCL_LP = new ParametersExPCL_LP();
            DocumentExPCL_PP docExPCL_PP = new DocumentExPCL_PP(DocumentExPCL_PP.PaperWidthValue.PaperWidth_384); //Page print mode
            ParametersExPCL_PP paramExPCL_PP = new ParametersExPCL_PP();

            //following code send data to the connected printer
            //====DPL Printers(eg. RL3, RL4, etc.)========//
            docDPL.WriteTextInternalBitmapped("Hello World", 1, 5, 5);
            //write normal ASCII Text Scalable
            docDPL.WriteTextScalable("Hello World", "00", 25, 5);
            conn.Write(docDPL.GetDocumentData());

            //====Legacy Printers (OC2, OC3, MF4Te, etc.)========//
            //EZ Mode
            docEZ.WriteText("Customer Code: 00146 docEZ", 50, 1);
            DocumentDPL.Label_Format item = docDPL.LabelFormat;
            
            conn.Write(docEZ.GetDocumentData());

            //LP Mode
            docLP.WriteText("Customer Code: 00146");
            conn.Write(docLP.GetDocumentData());

            //====Apex Printers(Apex 2, Apex 3, etc..)========//
            //Line Print Mode
            paramExPCL_LP.FontIndex = 5;
            docExPCL_LP.WriteText("Hello World I am a printing sample (Font - K5)", paramExPCL_LP);
            conn.Write(docExPCL_LP.GetDocumentData());

            //Page Print Mode
            docExPCL_PP.DrawText(0, 1600, true, (ParametersExPCL_PP.RotationAngle)0, "<f=1>This is a sample");
            conn.Write(docExPCL_PP.GetDocumentData());
            */
        }
        private static byte[]  ImageToByteArray(string imageInfile)
        {
            byte[] imageBytes;
            using (Image image = Image.FromFile(imageInfile)) {
                using (var ms = new MemoryStream())
                {
                    image.Save(ms, image.RawFormat);
                    imageBytes = new byte[ms.Length];
                    imageBytes = ms.ToArray();

                }
            }
            return imageBytes;
        }

        private static byte[] PDFToByteArray(string pdffile)
        {
            byte[] pdfBytes;
            using (Image image = Image.FromFile(pdffile))
            {
                using (var ms = new MemoryStream())
                {
                    image.Save(ms, image.RawFormat);
                    pdfBytes = new byte[ms.Length];
                    pdfBytes = ms.ToArray();

                }
            }
            return pdfBytes;
        }

        static void SendDPLCommandsToPrinter()
        {

            ConnectionBase conn =
                (Connection_Bluetooth32Feet)Connection_Bluetooth32Feet.CreateClient("84253F441187",true);
            //ConnectionBase conn = (Connection_Serial)Connection_Serial.CreateClient("COM21", 9600, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One, System.IO.Ports.Handshake.None);
            conn.Open();

            LanguageFiles_DPL langdpl = new LanguageFiles_DPL(conn);
            PrinterStatus_DPL statusdpl = new PrinterStatus_DPL(conn);
            Images_DPL imagedpl = new Images_DPL(conn);
            DocumentDPL dpl = new DocumentDPL();
            //Document doc = new Document();
            //byte[] by = doc.GetDocumentData();
            
            Console.WriteLine("Enter 1 to start printing...");
            var input = Console.ReadLine();
            int count = 1;
            if (input == "1") { 
                while (input == "1" && count<=3)
                {
                    char SOH = (char)0x01;
                    char STX = (char)0x02;
                    char CR = (char)0x0D;
                    
                    string send_text = STX +"L"+ CR;
                    send_text += "D11" + CR;
                    send_text += "1Y11000002000451_Monochrome.DIM"+CR;
                    //send_text += "1911A1200200020HOW ARE YOU" + CR;
                    //send_text += "4911A1200200160HONEYWELL IS DOING GOOD" +
                    //    " 000000" + CR;
                    //send_text += "1A9304001300180123456" + CR;
                    //send_text += "2A9304001200520789" + CR;
                    send_text += "E" + CR;
                    conn.Write(send_text);
                    //conn.Write(ImageToByteArray(@"C:\Users\h387014\Documents\Cases\1_Monochrome.bmp"), 1, 10000);
                    conn.Write(PDFToByteArray(@"C:\Users\h387014\Downloads\DPL PatronTicket__POSTickets.pdf"), 1, 10000);

                    statusdpl.Update(3000);
                    List<string> printerstatusstring = statusdpl.QueryResults();

                    count = count + 1;
                    foreach (string item in printerstatusstring)
                        Console.WriteLine(item.ToString());
                    Console.ReadLine();
                }
                conn.Close();
            }
        }

    }
}
