using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Honeywell.Printer;
using Honeywell.Connection;
using Honeywell.Printer.Configuration.DPL;
using Honeywell.Printer.Configuration.EZ;
using Honeywell.Printer.Configuration.ExPCL;
using System.Threading;
using System.IO;
using System.Runtime.Serialization;

namespace DOPrint_WD
{
    [Serializable]
    public partial class DOPrint_WD : Form
    {
        private string m_deviceIP = "192.168.101.105";
        private string m_deviceMAC = "0017AC0151B9";
        private int m_devicePort = 515;
        private string m_passKey = "0000";
        private int m_printHeadWidth = 384;
        private ConnectionBase conn = null;

        private DocumentDPL docDPL;
        private DocumentEZ docEZ;
        private DocumentLP docLP;
        private DocumentExPCL_LP docExPCL_LP;
        private DocumentExPCL_PP docExPCL_PP;
        private ParametersEZ paramEZ;
        private ParametersDPL paramDPL;
        private ParametersExPCL_LP paramExPCL_LP;
        private ParametersExPCL_PP paramExPCL_PP;
        private byte[] printData;

        private int selectedItemIndex;
        private int selectedLanguageIndex;
        private List<String> itemsArray = new List<String>();
        private List<String> selectedFilesList = new List<String>();
        private OpenFileDialog fileDlg;
        String connType;
        private bool printRadioChecked = true;
        AppSettings appSettings = new AppSettings();

        public DOPrint_WD()
        {
            InitializeComponent();
        }

        private void DOPrint_WD_Load(object sender, EventArgs e)
        {
            //check if there are settings to load
            reloadItemsArray();
            m_printItemsComboBox.DataSource = itemsArray.ToArray();
            //tries to load settings
            if (appSettings.LoadSettings())
            {
                //===========Load values from Settings==================//
                m_deviceIP = appSettings.DeviceIP;
                m_deviceMAC = appSettings.DeviceMAC;
                m_devicePort = appSettings.DevicePort;
                m_passKey = appSettings.DevicePassKey;

                m_connComboBox.SelectedIndex = appSettings.ConnectionIndex;
                m_printerLanguageComboBox.SelectedIndex = appSettings.LangIndex;
                m_printRadio.Checked = appSettings.IsPrint;
                m_queryRadio.Checked = !appSettings.IsPrint;
                m_printHeadCmbo.SelectedIndex = appSettings.PrintHeadIndex;
                //=========================================================//
            }
            else
            {
                m_printerLanguageComboBox.SelectedIndex = 0;
                m_connComboBox.SelectedIndex = 1;
                m_printHeadCmbo.SelectedIndex = 0;
            }
            connType = m_connComboBox.SelectedItem.ToString();
            
            if (connType == "TCP/IP")
            {
                m_deviceAddressTextBox.Text = m_deviceIP;
                m_portLabel.Text = "Port:";
                m_portTextBox.Text = m_devicePort.ToString();
            }
            else if (connType == "Bluetooth")
            {
                m_deviceAddressTextBox.Text = m_deviceMAC;
                m_portLabel.Text = "Passkey:";
                m_portTextBox.Text = m_passKey;
            }
        }

        private void DOPrint_WD_FormClosing(object sender, FormClosingEventArgs e)
        {
            appSettings.DeviceIP = m_deviceIP;
            appSettings.DeviceMAC = m_deviceMAC;
            appSettings.DevicePort = m_devicePort;
            appSettings.DevicePassKey = m_passKey;
            appSettings.ConnectionIndex = m_connComboBox.SelectedIndex;
            appSettings.IsPrint = m_printRadio.Checked;
            appSettings.LangIndex = m_printerLanguageComboBox.SelectedIndex;
            appSettings.PrintHeadIndex = m_printHeadCmbo.SelectedIndex;

            appSettings.SaveSettings();
        }

        private void performButton_Click(object sender, EventArgs e)
        {
            try 
            {
			    formPrintJob();
			    //======================Start background thread for connection===========================/
			    Thread printThread = new Thread(new ThreadStart(run));
			    printThread.IsBackground = true;
			    printThread.Start();
            }
            catch (Exception ex)
            {
                m_performButton.Enabled = true;
                m_statusTextBox.Text += "Error - " + (ex.Message) + "\r\n";
                m_statusTextBox.SelectionStart = m_statusTextBox.Text.Length;
                m_statusTextBox.ScrollToCaret();
                if (ex.InnerException != null)
                {
                    m_statusTextBox.Text += (ex.InnerException.Message) + "\r\n";
                    m_statusTextBox.SelectionStart = m_statusTextBox.Text.Length;
                    m_statusTextBox.ScrollToCaret();
                }
            }
        }

		private void formPrintJob()
		{
            m_statusTextBox.Text = "";
			selectedItemIndex = m_printItemsComboBox.SelectedIndex;
			selectedLanguageIndex = m_printerLanguageComboBox.SelectedIndex;
			m_performButton.Enabled = false;
			List<DocumentEZ> docList = new List<DocumentEZ>();
			docDPL = new DocumentDPL();
			docEZ = new DocumentEZ("MF204");
			docLP = new DocumentLP("!");
			docExPCL_LP = new DocumentExPCL_LP(3);
			docExPCL_PP = new DocumentExPCL_PP(DocumentExPCL_PP.PaperWidthValue.PaperWidth_384);

			paramEZ = new ParametersEZ();
			paramDPL = new ParametersDPL();
			paramExPCL_LP = new ParametersExPCL_LP();
			paramExPCL_PP = new ParametersExPCL_PP();
			//if we are printing
			if (m_printRadio.Checked)
			{
				//Checks current Mode
				if (m_printerLanguageComboBox.SelectedItem.ToString().Equals("EZ"))
				{
					//3-in sample
					if (selectedItemIndex == 0)
					{
						//=============GENERATING RECEIPT====================================//
                        Bitmap logo = new Bitmap(global::DOPrint_WD.Properties.Resources.DO_nobackground);
                        docLP.WriteImage(logo, 576);

                        docEZ.InitialPaperBackup = 35;
                        docEZ.WriteText("For", 1, 200);

                        //Bold delivery
                        paramEZ.IsBold = true;
                        docEZ.WriteText("Delivery", 1, 240, paramEZ);

                        //print image on same Delivery line
                        //docEZ.WriteImage("DOLGO", 1, 350);

                        docEZ.WriteText("Customer Code: ", 50, 1);

                        //Use italic font 
                        paramEZ.Font = "ZP96P";
                        docEZ.WriteText("00146", 50, 150, paramEZ);
                        docEZ.WriteText("Address: Manila", 75, 1);
                        docEZ.WriteText("Tin No.: 27987641", 100, 1);
                        docEZ.WriteText("Area Code: PN1-0004", 125, 1);
                        docEZ.WriteText("Business Style: SUPERMARKET A", 150, 1);

                        docEZ.WriteText("PRODUCT CODE  PRODUCT DESCRIPTION         QTY.  Delivr.", 205, 1);
                        docEZ.WriteText("------------  --------------------------  ----  -------", 230, 1);
                        docEZ.WriteText("    111       Wht Bread Classic 400g       51      51  ", 255, 1);
                        docEZ.WriteText("    112       Clsc Wht Bread 600g          77      77  ", 280, 1);
                        docEZ.WriteText("    113       Wht Bread Clsc 600g          153     25  ", 305, 1);
                        docEZ.WriteText("    121       H Fiber Wheat Bread 600g     144     77  ", 330, 1);
                        docEZ.WriteText("    122       H Fiber Wheat Bread 400g     112     36  ", 355, 1);
                        docEZ.WriteText("    123       H Calcium Loaf 400g          81      44  ", 380, 1);
                        docEZ.WriteText("    211       California Raisin Loaf       107     44  ", 405, 1);
                        docEZ.WriteText("    212       Chocolate Chip Loaf          159     102 ", 430, 1);
                        docEZ.WriteText("    213       Dbl Delights(Ube & Chse)     99      80  ", 455, 1);
                        docEZ.WriteText("    214       Dbl Delights(Choco & Mocha)  167     130 ", 480, 1);
                        docEZ.WriteText("    215       Mini Wonder Ube Cheese       171     179 ", 505, 1);
                        docEZ.WriteText("    216       Mini Wonder Ube Mocha        179     100 ", 530, 1);
                        docEZ.WriteText("  ", 580, 1);

                        printData = docEZ.GetDocumentData();
						//======================================================================//
					}

					//4-in sample
					else if (selectedItemIndex == 1)
					{
						docEZ.WriteText("For Delivery", 1, 300);
						docEZ.WriteText("Customer Code: 00146", 50, 1);
						docEZ.WriteText("Address: Manila", 75, 1);
						docEZ.WriteText("Tin No.: 27987641", 100, 1);
						docEZ.WriteText("Area Code: PN1-0004", 125, 1);
						docEZ.WriteText("Business Style: SUPERMARKET A", 150, 1);

						docEZ.WriteText("PRODUCT CODE      PRODUCT DESCRIPTION             QTY.    Delivered ", 205, 1);
						docEZ.WriteText("------------      --------------------------      ----    ----------", 230, 1);
						docEZ.WriteText("    111           Wht Bread Classic 400g           51          51   ", 255, 1);
						docEZ.WriteText("    112           Clsc Wht Bread 600g              77          77   ", 280, 1);
						docEZ.WriteText("    113           Wht Bread Clsc 600g              153         25   ", 305, 1);
						docEZ.WriteText("    121           H Fiber Wheat Bread 600g         144         77   ", 330, 1);
						docEZ.WriteText("    122           H Fiber Wheat Bread 400g         112         36   ", 355, 1);
						docEZ.WriteText("    123           H Calcium Loaf 400g              81          44   ", 380, 1);
						docEZ.WriteText("    211           California Raisin Loaf           107         44   ", 405, 1);
						docEZ.WriteText("    212           Chocolate Chip Loaf              159         102  ", 430, 1);
						docEZ.WriteText("    213           Dbl Delights(Ube & Chse)         99          80   ", 455, 1);
						docEZ.WriteText("    214           Dbl Delights(Choco & Mocha)      167         130  ", 480, 1);
						docEZ.WriteText("    215           Mini Wonder Ube Cheese           171         179  ", 505, 1);
						docEZ.WriteText("    216           Mini Wonder Ube Mocha            179         100  ", 530, 1);
						docEZ.WriteText("  ", 580, 1);
						printData = docEZ.GetDocumentData();

					}

					//Barcode Sample
					else if (selectedItemIndex == 2)
					{
						paramEZ.HorizontalMultiplier = 1;
						paramEZ.VerticalMultiplier = 2;

						//write GS1 barcodes with 2d Composite data
						int pixelMult = 3;

						docEZ.WriteText("GS1 Barcode", 1, 1);
						docEZ.WriteBarCodeGS1DataBar("GSONE", "123456789", "123", pixelMult, pixelMult, 1, 1, 22, 30, 1, paramEZ);

						docEZ.WriteText("GS1 Truncated", 330, 1);
						docEZ.WriteBarCodeGS1DataBar("GS1TR", "123456789", "123", pixelMult, pixelMult, 1, 1, 22, 360, 1, paramEZ);

						docEZ.WriteText("GS1 Limited", 530, 1);
						docEZ.WriteBarCodeGS1DataBar("GS1LM", "123456789", "123", pixelMult, pixelMult, 1, 1, 22, 560, 1, paramEZ);

						docEZ.WriteText("GS1 Stacked", 730, 1);
						docEZ.WriteBarCodeGS1DataBar("GS1ST", "123456789", "123", pixelMult, pixelMult, 1, 1, 22, 760, 1, paramEZ);


						docEZ.WriteText("GS1 Stacked Omnidirection", 930, 1);
						docEZ.WriteBarCodeGS1DataBar("GS1SO", "123456789", "123", pixelMult, pixelMult, 1, 1, 22, 960, 1, paramEZ);

						docEZ.WriteText("GS1 Expanded", 1530, 1);
						docEZ.WriteBarCodeGS1DataBar("GS1EX", "ABCDEFGHIJKL", "helloWorld!123", pixelMult, 2 * pixelMult, 1, 1, 4, 1560, 1, paramEZ);

						paramEZ.HorizontalMultiplier = 2;
						paramEZ.VerticalMultiplier = 10;
						//Interleave 2of 5 barcode ratio 2:1
						docEZ.WriteText("Interleave 2of5 Barcode ratio 2:1", 2230, 1);
						docEZ.WriteBarCode("BCI25", "0123456789", 2260, 1, paramEZ);

						//barcode 128
						docEZ.WriteText("Barcode 128", 2330, 1);
						docEZ.WriteBarCode("BC128", "00010203040506070809", 2360, 1, paramEZ);

						//barcode EAN 128
						docEZ.WriteText("EAN 128", 2430, 1);
						docEZ.WriteBarCode("EN128", "00010203040506070809", 2460, 1, paramEZ);

						//Code 39 barcodes
						docEZ.WriteText("Code 39 Barcodes", 2530, 1);
						docEZ.WriteBarCode("BC39N", "0123456789", 2560, 1, paramEZ);
						docEZ.WriteBarCode("BC39W", "0123456789", 2660, 1, paramEZ);

						//Code 93 barcode
						docEZ.WriteText("Code 93", 2730, 1);
						docEZ.WriteBarCode("BC093", "0123456789", 2760, 1, paramEZ);

						//Codabar
						docEZ.WriteText("CODABAR", 2830, 1);
						docEZ.WriteBarCode("COBAR", "00010203040506070809", 2860, 1, paramEZ);

						//8 digit europe art num
						docEZ.WriteText("8 DIGIT EUROPE ART NUM", 2930, 1);
						docEZ.WriteBarCode("EAN08", "0123456", 2960, 1, paramEZ);

						//13 digit europ art num
						docEZ.WriteText("13 DIGIT Europe Art Num", 3030, 1);
						docEZ.WriteBarCode("EAN13", "000123456789", 3060, 1, paramEZ);

						//INTLV 2of5
						docEZ.WriteText("Interleaved 2of5", 3130, 1);
						docEZ.WriteBarCode("I2OF5", "0123456789", 3160, 1, paramEZ);

						//PDF417
						docEZ.WriteText("PDF417", 3230, 1);
						docEZ.WriteBarCodePDF417("00010203040506070809", 3260, 1, 2, 1, paramEZ);

						//Plessy
						docEZ.WriteText("Plessy", 3350, 1);
						docEZ.WriteBarCode("PLESY", "8052", 3380, 1, paramEZ);

						//UPC-A
						docEZ.WriteText("UPC-A", 3450, 1);
						docEZ.WriteBarCode("UPC-A", "01234567890", 3480, 1, paramEZ);

						//UPC-E
						docEZ.WriteText("UPC-E", 3550, 1);
						docEZ.WriteBarCode("UPC-E", "0123456", 3580, 1, paramEZ);

						paramEZ.HorizontalMultiplier = 10;

						paramEZ.VerticalMultiplier = 1;
						//QR
						docEZ.WriteText("QR Barcode Manual Formating", 3650, 1);
						docEZ.WriteBarCodeQRCode("N0123456789,B0004(&#),QR//BARCODE", 2, 9, 1, 3680, 1, paramEZ);

						docEZ.WriteText("QR Barcode Auto Formatting 1", 3950, 1);
						docEZ.WriteBarCodeQRCode("0123456789012345678901234567890123456789", 2, 9, 0, 3980, 1, paramEZ);

						paramEZ.HorizontalMultiplier = 8;
						docEZ.WriteText("QR Barcode Auto Formatting 2", 4250, 1);
						docEZ.WriteBarCodeQRCode("0123456789ABCDE", 2, 9, 0, 4280, 1, paramEZ);

						//Aztec
						docEZ.WriteText("Aztec", 4550, 1);
						docEZ.WriteBarCodeAztec("Code 2D!", 104, 4580, 1, paramEZ);
						docEZ.WriteText("", 4500, 1);
						printData = docEZ.GetDocumentData();
					}
					//User selected a browsed file
					else
					{
						Bitmap anImage = null;
						String selectedItem = (String)m_printItemsComboBox.SelectedItem;
						//Check if item is an image
						String[] okFileExtensions = new String[] { ".jpg", ".png", ".gif", ".jpeg", ".bmp", ".tif", ".tiff", ".pcx" };
						foreach (String extension in okFileExtensions)
						{
							if (selectedItem.ToLower().EndsWith(extension))
							{
								anImage = new Bitmap(selectedItem);
								break;
							}
						}
						//selected item is not an image file
                        if (selectedItem.ToLower().EndsWith(".pdf"))
                        {
                            docLP.WritePDF(selectedItem, m_printHeadWidth);
                            printData = docLP.GetDocumentData();
                        }
                        //selected item is not an image file
                        else if (anImage == null)
						{
							FileStream fs = File.OpenRead(selectedItem);
							printData = new byte[fs.Length];
							fs.Read(printData, 0, (int)fs.Length);
							fs.Close();
						}
                        else
                        {
                            m_statusTextBox.Text = "Processing image..\r\n";
                            docLP.WriteImage(anImage, m_printHeadWidth);
                            printData = docLP.GetDocumentData();
                        }//end else
					}
				}
				//for LP 
				else if (m_printerLanguageComboBox.SelectedItem.ToString().Equals("LP"))
				{
					docLP.Clear();
					//3-inch sample to generate
					if (selectedItemIndex == 0)
					{
						docLP.WriteText("                   For Delivery");
						docLP.WriteText(" ");
						docLP.WriteText("Customer Code: 00146");
						docLP.WriteText("Address: Manila");
						docLP.WriteText("Tin No.: 27987641");
						docLP.WriteText("Area Code: PN1-0004");
						docLP.WriteText("Business Style: SUPERMARKET A");
						docLP.WriteText(" ");
						docLP.WriteText("PRODUCT CODE   PRODUCT DESCRIPTION          QTY.  Delivr.");
						docLP.WriteText("------------   --------------------------   ----  -------");
						docLP.WriteText("    111        Wht Bread Classic 400g        51     51   ");
						docLP.WriteText("    112        Clsc Wht Bread 600g           77     77   ");
						docLP.WriteText("    113        Wht Bread Clsc 600g           153    25   ");
						docLP.WriteText("    121        H Fiber Wheat Bread 600g      144    77   ");
						docLP.WriteText("    122        H Fiber Wheat Bread 400g      112    36   ");
						docLP.WriteText("    123        H Calcium Loaf 400g           81     44   ");
						docLP.WriteText("    211        California Raisin Loaf        107    44   ");
						docLP.WriteText("    212        Chocolate Chip Loaf           159    102  ");
						docLP.WriteText("    213        Dbl Delights(Ube & Chse)      99     80   ");
						docLP.WriteText("    214        Dbl Delights(Choco & Mocha)   167    130  ");
						docLP.WriteText("    215        Mini Wonder Ube Cheese        171    79   ");
						docLP.WriteText("    216        Mini Wonder Ube Mocha         179    100  ");
						docLP.WriteText("  ");
						docLP.WriteText("  ");
						printData = docLP.GetDocumentData();
					}
					//4-inch sample to generate
					else if (selectedItemIndex == 1)
					{
						docLP.WriteText("                            For Delivery");
						docLP.WriteText(" ");
						docLP.WriteText("Customer Code: 00146");
						docLP.WriteText("Address: Manila");
						docLP.WriteText("Tin No.: 27987641");
						docLP.WriteText("Area Code: PN1-0004");
						docLP.WriteText("Business Style: SUPERMARKET A");
						docLP.WriteText(" ");
						docLP.WriteText("PRODUCT CODE         PRODUCT DESCRIPTION          QTY.    Delivered");
						docLP.WriteText("------------      --------------------------      ----    ---------- ");
						docLP.WriteText("    111           Wht Bread Classic 400g           51         51     ");
						docLP.WriteText("    112           Clsc Wht Bread 600g              77         77     ");
						docLP.WriteText("    113           Wht Bread Clsc 600g              153        25     ");
						docLP.WriteText("    121           H Fiber Wheat Bread 600g         144        77     ");
						docLP.WriteText("    122           H Fiber Wheat Bread 400g         112        36     ");
						docLP.WriteText("    123           H Calcium Loaf 400g              81         44     ");
						docLP.WriteText("    211           California Raisin Loaf           107        44     ");
						docLP.WriteText("    212           Chocolate Chip Loaf              159        102    ");
						docLP.WriteText("    213           Dbl Delights(Ube & Chse)         99         80     ");
						docLP.WriteText("    214           Dbl Delights(Choco & Mocha)      167        130    ");
						docLP.WriteText("    215           Mini Wonder Ube Cheese           171        179    ");
						docLP.WriteText("    216           Mini Wonder Ube Mocha            179        100    ");
						docLP.WriteText("  ");
						docLP.WriteText("  ");
						printData = docLP.GetDocumentData();
					}
					//Print Image for 2 inch
					else if (selectedItemIndex == 2)
					{
						Bitmap anImage = new Bitmap(global::DOPrint_WD.Properties.Resources.dologo);
						docLP.WriteImage(anImage, m_printHeadWidth);
						printData = docLP.GetDocumentData();
					}
					//User selected a browsed file
					else
					{
						Bitmap anImage = null;
						String selectedItem = (String)m_printItemsComboBox.SelectedItem;
						//Check if item is an image
						String[] okFileExtensions = new String[] { ".jpg", ".png", ".gif", ".jpeg", ".bmp", ".tif", ".tiff", ".pcx" };
						foreach (String extension in okFileExtensions)
						{
							if (selectedItem.ToLower().EndsWith(extension))
							{
								anImage = new Bitmap(selectedItem);
								break;
							}
						}

                        if (selectedItem.ToLower().EndsWith(".pdf"))
                        {
                            docLP.WritePDF(selectedItem, m_printHeadWidth);
                            printData = docLP.GetDocumentData();
                        }
						//selected item is not an image file
						else if (anImage == null)
						{
							FileStream fs = File.OpenRead(selectedItem);
							printData = new byte[fs.Length];
							fs.Read(printData, 0, (int)fs.Length);
							fs.Close();
						}
						else
						{
							m_statusTextBox.Text = "Processing image..\r\n";
							docLP.WriteImage(anImage, m_printHeadWidth);
							printData = docLP.GetDocumentData();

						}//end else
					}
				}
				//for EXPCL(Apex Printers)
				else if (m_printerLanguageComboBox.SelectedItem.ToString().Equals("ExPCL_LP"))
				{
					//bool TEST_PAPER_ADVANCE = false;

					// TEXT SAMPLES
					if (selectedItemIndex == 0)
					{
						docExPCL_LP.WriteText("Hello World I am a printing sample");
						paramExPCL_LP.FontIndex = 5;
						docExPCL_LP.WriteText("Hello World I am a printing sample (Font - K5)", paramExPCL_LP);
						paramExPCL_LP.FontIndex = 3;
						paramExPCL_LP.IsBold = true;
						docExPCL_LP.WriteText("Hello World I am a printing sample (Bold)", paramExPCL_LP);
						paramExPCL_LP.IsBold = false;
						paramExPCL_LP.IsInverse = true;
						docExPCL_LP.WriteText("Hello World I am a printing sample (White On Black)", paramExPCL_LP);
						paramExPCL_LP.IsInverse = false;
						paramExPCL_LP.IsPCLineDrawCharSet = true;
						docExPCL_LP.WriteText("Hello World I am a printing sample (PC Line Draw)", paramExPCL_LP);
						for (int i = 179; i < 256; i++)
						{
							paramExPCL_LP.TextEncoding = Encoding.UTF8;
							docExPCL_LP.WriteTextPartial(((char)i).ToString(), paramExPCL_LP);
						}
						paramExPCL_LP.IsPCLineDrawCharSet = false;
						docExPCL_LP.WriteText("Hello World I am a printing sample (International)", paramExPCL_LP);
						for (int i = 179; i < 256; i++)
						{
							paramExPCL_LP.TextEncoding = Encoding.UTF8;
							docExPCL_LP.WriteTextPartial(((char)i).ToString(), paramExPCL_LP);
						}
						paramExPCL_LP.TextEncoding = Encoding.Default;
						docExPCL_LP.WriteText("", paramExPCL_LP);
						docExPCL_LP.WriteTextPartial("one ");
						docExPCL_LP.WriteTextPartial("two ");
						docExPCL_LP.WriteTextPartial("three ");
						docExPCL_LP.WriteText("<CR>");
						paramExPCL_LP.IsRightToLeftTextDirection = true;
						docExPCL_LP.WriteText("Hello World I am a printing sample (Right-To-Left Text Direction)", paramExPCL_LP);
						paramExPCL_LP.IsRightToLeftTextDirection = false;

						paramExPCL_LP.IsUnderline = true;
						docExPCL_LP.WriteText("Hello World I am a printing sample (Underline)", paramExPCL_LP);
						paramExPCL_LP.IsUnderline = false;
						paramExPCL_LP.PrintContrastLevel = 6;
						docExPCL_LP.WriteText("Hello World I am a printing sample (Contrast = 6)", paramExPCL_LP);
						paramExPCL_LP.PrintContrastLevel = 2;
						paramExPCL_LP.LineSpacing = 30;
						docExPCL_LP.WriteText("Hello World I am a printing sample (Line Spacing = 30)", paramExPCL_LP);
						paramExPCL_LP.LineSpacing = 3;
						paramExPCL_LP.VerticalTabHeight = 150;

						string data = String.Format("Tab0{0}Tab1{1}Tab2{2}Hello World I am a printing sample (Vertical Tab = 50)", (char)11, (char)11, (char)11);
						docExPCL_LP.WriteText(data, paramExPCL_LP);
						//paramExPCL_LP.VerticalTabHeight = 203;
						paramExPCL_LP.HorizontalTabWidth = 50;

						data = String.Format("Tab0{0}Tab1{1}Tab2{2}Hello World I am a printing sample (Horizontal Tab = 200)", (char)9, (char)9, (char)9);
						docExPCL_LP.WriteText(data, paramExPCL_LP);

						//paramExPCL_LP.HorizontalTabWidth = 100;
						paramExPCL_LP.SensorSensitivity = 100;
						docExPCL_LP.WriteText("Hello World I am a printing sample (Sensor Sensitivity = 100)", paramExPCL_LP);
						paramExPCL_LP.SensorSensitivity = 255;
						paramExPCL_LP.PaperPresenter = 100;
						docExPCL_LP.WriteText("Hello World I am a printing sample (Paper Presenter = 100)", paramExPCL_LP);
						paramExPCL_LP.PaperPresenter = 190;
						paramExPCL_LP.AutoPowerDownTimer = 30;
						docExPCL_LP.WriteText("Hello World I am a printing sample (Auto Power Down = 9 seconds)", paramExPCL_LP);
						paramExPCL_LP.AutoPowerDownTimer = 0;
						docExPCL_LP.WriteText("Hello World I am a printing sample (Auto Power Down = 0 seconds)", paramExPCL_LP);
						printData = docExPCL_LP.GetDocumentData();
					}
					// BARCODE SAMPLES
					else if (selectedItemIndex == 1)
					{
						docExPCL_LP.WriteBarCode((ParametersExPCL_LP.BarcodeExPCL_LP)(ParametersExPCL_LP.BarcodeExPCL_LP)1, "DMITRIY", true, 100);
						docExPCL_LP.WriteBarCode((ParametersExPCL_LP.BarcodeExPCL_LP)1, "DMITRIY", false, 50);

						paramExPCL_LP.BarCodeHeight = 100;
						paramExPCL_LP.IsAnnotate = false;
						paramExPCL_LP.BarCodeType = ParametersExPCL_LP.BarcodeExPCL_LP.Code128;
						paramExPCL_LP.FontIndex = 5;
						paramExPCL_LP.IsUnderline = true;
						docExPCL_LP.WriteBarCode((ParametersExPCL_LP.BarcodeExPCL_LP)1, "DMITRIY", true, 25, paramExPCL_LP);
						docExPCL_LP.WriteBarCode((ParametersExPCL_LP.BarcodeExPCL_LP)1, "DMITRIY", false, 50);

						docExPCL_LP.WriteBarCode((ParametersExPCL_LP.BarcodeExPCL_LP)2, "DMITRIY", true, 25);
						docExPCL_LP.WriteBarCode((ParametersExPCL_LP.BarcodeExPCL_LP)2, "dmitriy", true, 25);
						docExPCL_LP.WriteBarCode((ParametersExPCL_LP.BarcodeExPCL_LP)2, "1234567890", true, 25);
						docExPCL_LP.WriteBarCode((ParametersExPCL_LP.BarcodeExPCL_LP)3, "1234567890", true, 25);
						docExPCL_LP.WriteBarCode((ParametersExPCL_LP.BarcodeExPCL_LP)4, "1234567890$", true, 25);
						docExPCL_LP.WriteBarCode((ParametersExPCL_LP.BarcodeExPCL_LP)5, "12345678901", true, 25);
						docExPCL_LP.WriteBarCode((ParametersExPCL_LP.BarcodeExPCL_LP)5, "123456", true, 25);
						docExPCL_LP.WriteBarCode((ParametersExPCL_LP.BarcodeExPCL_LP)5, "1234567", true, 25);
						docExPCL_LP.WriteBarCode((ParametersExPCL_LP.BarcodeExPCL_LP)5, "123456789012", true, 25);

						paramExPCL_LP.FontIndex = 1;
						docExPCL_LP.WriteBarCode((ParametersExPCL_LP.BarcodeExPCL_LP)1, "DMITRIY", true, 25, paramExPCL_LP);
						paramExPCL_LP.FontIndex = 2;
						docExPCL_LP.WriteBarCode((ParametersExPCL_LP.BarcodeExPCL_LP)2, "DMITRIY", true, 25, paramExPCL_LP);
						paramExPCL_LP.FontIndex = 3;
						docExPCL_LP.WriteBarCode((ParametersExPCL_LP.BarcodeExPCL_LP)2, "dmitriy", true, 25, paramExPCL_LP);
						paramExPCL_LP.FontIndex = 4;
						docExPCL_LP.WriteBarCode((ParametersExPCL_LP.BarcodeExPCL_LP)2, "1234567890", true, 25, paramExPCL_LP);
						paramExPCL_LP.FontIndex = 5;
						docExPCL_LP.WriteBarCode((ParametersExPCL_LP.BarcodeExPCL_LP)3, "1234567890", true, 25, paramExPCL_LP);
						paramExPCL_LP.FontIndex = 6;
						docExPCL_LP.WriteBarCode((ParametersExPCL_LP.BarcodeExPCL_LP)4, "1234567890$", true, 25, paramExPCL_LP);
						paramExPCL_LP.FontIndex = 7;
						docExPCL_LP.WriteBarCode((ParametersExPCL_LP.BarcodeExPCL_LP)5, "12345678901", true, 25, paramExPCL_LP);
						paramExPCL_LP.FontIndex = 8;
						docExPCL_LP.WriteBarCode((ParametersExPCL_LP.BarcodeExPCL_LP)5, "123456", true, 25, paramExPCL_LP);
						paramExPCL_LP.FontIndex = 9;
						docExPCL_LP.WriteBarCode((ParametersExPCL_LP.BarcodeExPCL_LP)5, "1234567", true, 25, paramExPCL_LP);
						paramExPCL_LP.FontIndex = 10;
						docExPCL_LP.WriteBarCode((ParametersExPCL_LP.BarcodeExPCL_LP)5, "123456789012", true, 25, paramExPCL_LP);

						docExPCL_LP.WriteBarCodeGS1DataBar((ParametersExPCL_LP.GS1DataBar)1, "1234567890123", true, 2, 0, 0, 1);
						docExPCL_LP.WriteBarCodeGS1DataBar((ParametersExPCL_LP.GS1DataBar)1, "1234567890123", false, 3, 0, 0, 2);
						docExPCL_LP.WriteBarCodeGS1DataBar((ParametersExPCL_LP.GS1DataBar)1, "1234567890123", true, 4, 0, 0, 3);
						docExPCL_LP.WriteBarCodeGS1DataBar((ParametersExPCL_LP.GS1DataBar)1, "1234567890123", false, 5, 0, 0, 4);
						docExPCL_LP.WriteBarCodeGS1DataBar((ParametersExPCL_LP.GS1DataBar)1, "1234567890123", true, 6, 0, 0, 5);

						docExPCL_LP.WriteBarCodeGS1DataBar((ParametersExPCL_LP.GS1DataBar)2, "1234567890123", true, 2, 0, 0, 1);
						docExPCL_LP.WriteBarCodeGS1DataBar((ParametersExPCL_LP.GS1DataBar)3, "1234567890123", true, 2, 0, 0, 1);
						docExPCL_LP.WriteBarCodeGS1DataBar((ParametersExPCL_LP.GS1DataBar)4, "1234567890123", true, 2, 0, 0, 1);
						docExPCL_LP.WriteBarCodeGS1DataBar((ParametersExPCL_LP.GS1DataBar)5, "1234567890123", true, 2, 0, 0, 1);
						docExPCL_LP.WriteBarCodeGS1DataBar((ParametersExPCL_LP.GS1DataBar)6, "DATAMAX-O'NEIL", true, 2, 0, 0, 1);
						docExPCL_LP.WriteBarCodeGS1DataBar((ParametersExPCL_LP.GS1DataBar)7, "12345678901", true, 2, 0, 0, 1);
						docExPCL_LP.WriteBarCodeGS1DataBar((ParametersExPCL_LP.GS1DataBar)8, "1234500006", true, 2, 0, 0, 1);
						docExPCL_LP.WriteBarCodeGS1DataBar((ParametersExPCL_LP.GS1DataBar)9, "123456789012", true, 2, 0, 0, 1);
						docExPCL_LP.WriteBarCodeGS1DataBar((ParametersExPCL_LP.GS1DataBar)10, "1234567", true, 2, 0, 0, 1);
						docExPCL_LP.WriteBarCodeGS1DataBar((ParametersExPCL_LP.GS1DataBar)11, "123456789012", true, 2, 0, 0, 1);
						docExPCL_LP.WriteBarCodeGS1DataBar((ParametersExPCL_LP.GS1DataBar)12, "123456789012", true, 2, 0, 0, 1);

						paramExPCL_LP.FontIndex = 1;
						docExPCL_LP.WriteBarCodeGS1DataBar((ParametersExPCL_LP.GS1DataBar)1, "1234567890123", true, 2, 0, 0, 1, paramExPCL_LP);

						docExPCL_LP.WriteBarCodeQRCode("www.datamax-oneil.com", false, 2, (byte)'H', 2);

						paramExPCL_LP.FontIndex = 10;
						docExPCL_LP.WriteBarCodeQRCode("www.datamax-oneil.com", true, 2, (byte)'L', 3, paramExPCL_LP);

						docExPCL_LP.WriteBarCodePDF417("www.datamax-oneil.com", 2);

						paramExPCL_LP.FontIndex = 1;
						docExPCL_LP.WriteBarCodePDF417("www.datamax-oneil.com", 2, paramExPCL_LP);
						printData = docExPCL_LP.GetDocumentData();
					}
					//Graphics
					else if (selectedItemIndex == 2)
					{
						m_statusTextBox.Text = "Processing image..\r\n";
						m_statusTextBox.SelectionStart = m_statusTextBox.Text.Length;
						m_statusTextBox.ScrollToCaret();
						Bitmap anImage = new Bitmap(global::DOPrint_WD.Properties.Resources.dologo);
						docExPCL_LP.WriteImage(anImage, m_printHeadWidth);
						printData = docExPCL_LP.GetDocumentData();
					}
					//User selected a browsed file
					else
					{
						Bitmap anImage = null;
						String selectedItem = (String)m_printItemsComboBox.SelectedItem;
						//Check if item is an image
						String[] okFileExtensions = new String[] { ".jpg", ".png", ".gif", ".jpeg", ".bmp", ".tif", ".tiff", ".pcx" };
						foreach (String extension in okFileExtensions)
						{
							if (selectedItem.ToLower().EndsWith(extension))
							{
								anImage = new Bitmap(selectedItem);
								break;
							}
						}
						//selected item is not an image file
                        if (selectedItem.ToLower().EndsWith(".pdf"))
                        {
                            docExPCL_LP.WritePDF(selectedItem, m_printHeadWidth);
                            printData = docExPCL_LP.GetDocumentData();
                        }
                        //selected item is not an image file
                        else if (anImage == null)
						{
							FileStream fs = File.OpenRead(selectedItem);
							printData = new byte[fs.Length];
							fs.Read(printData, 0, (int)fs.Length);
							fs.Close();
						}
						else
						{
							m_statusTextBox.Text = "Processing image..\r\n";
							docExPCL_LP.WriteImage(anImage, m_printHeadWidth);
							printData = docExPCL_LP.GetDocumentData();

						}//end else
					}
					//// Paper feed
					//if (TEST_PAPER_ADVANCE)
					//{
					//    docExPCL_LP.WriteText("Start of advanceToNextPage");
					//    docExPCL_LP.PageLength = 510;
					//    docExPCL_LP.AdvanceToNextPage();
					//    docExPCL_LP.WriteText("End of advanceToNextPage");

					//    docExPCL_LP.WriteText("Start of advanceToQMark");
					//    docExPCL_LP.AdvanceToQueueMark(255);
					//    docExPCL_LP.WriteText("End of advanceToQMark");
					//    printData = docExPCL_LP.GetDocumentData();
					//}
				}
				else if (m_printerLanguageComboBox.SelectedItem.ToString().Equals("ExPCL_PP"))
				{
					// Text
					if (selectedItemIndex == 0)
					{
						docExPCL_PP.DrawText(0, 1600, true, (ParametersExPCL_PP.RotationAngle)0, "<f=1>This is a sample");
						docExPCL_PP.DrawText(0, 1625, true, (ParametersExPCL_PP.RotationAngle)0, "<f=2>This is a sample");
						docExPCL_PP.DrawText(0, 1650, true, (ParametersExPCL_PP.RotationAngle)0, "<f=3>This is a sample");
						docExPCL_PP.DrawText(0, 1675, true, (ParametersExPCL_PP.RotationAngle)0, "<f=4>This is a sample");
						docExPCL_PP.DrawText(0, 1700, true, (ParametersExPCL_PP.RotationAngle)0, "<f=5>This is a sample");
						docExPCL_PP.DrawText(0, 1725, true, (ParametersExPCL_PP.RotationAngle)0, "<f=6>This is a sample");
						docExPCL_PP.DrawText(0, 1750, true, (ParametersExPCL_PP.RotationAngle)0, "<f=7>This is a sample");
						docExPCL_PP.DrawText(0, 1775, true, (ParametersExPCL_PP.RotationAngle)0, "<f=8>This is a sample");
						docExPCL_PP.DrawText(0, 1800, true, (ParametersExPCL_PP.RotationAngle)0, "<f=9>This is a sample");
						docExPCL_PP.DrawText(0, 1825, true, (ParametersExPCL_PP.RotationAngle)0, "<f=10>This is a sample");
						docExPCL_PP.DrawText(0, 1850, true, (ParametersExPCL_PP.RotationAngle)0, "<f=11>This is a sample");
						docExPCL_PP.DrawText(0, 1875, true, (ParametersExPCL_PP.RotationAngle)0, "<f=12>This is a sample");
						docExPCL_PP.DrawText(0, 1900, true, (ParametersExPCL_PP.RotationAngle)0, "<f=13>This is a sample");
						docExPCL_PP.DrawText(0, 1950, true, (ParametersExPCL_PP.RotationAngle)0, "<f=14>This is a sample");
						docExPCL_PP.DrawText(0, 2000, true, (ParametersExPCL_PP.RotationAngle)0, "<f=15>This is a sample");

						// Rotate text by 180
						docExPCL_PP.DrawText(384, 2425, true, (ParametersExPCL_PP.RotationAngle)2, "<f=1>This is a sample");
						docExPCL_PP.DrawText(384, 2400, true, (ParametersExPCL_PP.RotationAngle)2, "<f=2>This is a sample");
						docExPCL_PP.DrawText(384, 2375, true, (ParametersExPCL_PP.RotationAngle)2, "<f=3>This is a sample");
						docExPCL_PP.DrawText(384, 2350, true, (ParametersExPCL_PP.RotationAngle)2, "<f=4>This is a sample");
						docExPCL_PP.DrawText(384, 2325, true, (ParametersExPCL_PP.RotationAngle)2, "<f=5>This is a sample");
						docExPCL_PP.DrawText(384, 2300, true, (ParametersExPCL_PP.RotationAngle)2, "<f=6>This is a sample");
						docExPCL_PP.DrawText(384, 2275, true, (ParametersExPCL_PP.RotationAngle)2, "<f=7>This is a sample");
						docExPCL_PP.DrawText(384, 2250, true, (ParametersExPCL_PP.RotationAngle)2, "<f=8>This is a sample");
						docExPCL_PP.DrawText(384, 2225, true, (ParametersExPCL_PP.RotationAngle)2, "<f=9>This is a sample");
						docExPCL_PP.DrawText(384, 2200, true, (ParametersExPCL_PP.RotationAngle)2, "<f=10>This is a sample");
						docExPCL_PP.DrawText(384, 2175, true, (ParametersExPCL_PP.RotationAngle)2, "<f=11>This is a sample");
						docExPCL_PP.DrawText(384, 2150, true, (ParametersExPCL_PP.RotationAngle)2, "<f=12>This is a sample");
						docExPCL_PP.DrawText(384, 2125, true, (ParametersExPCL_PP.RotationAngle)2, "<f=13>This is a sample");
						docExPCL_PP.DrawText(384, 2100, true, (ParametersExPCL_PP.RotationAngle)2, "<f=14>This is a sample");
						docExPCL_PP.DrawText(384, 2050, true, (ParametersExPCL_PP.RotationAngle)2, "<f=15>This is a sample");

						// Text
						docExPCL_PP.WriteText("<f=1>This is a sample", 2450, 0);
						paramExPCL_PP.IsAnnotate = true;
						paramExPCL_PP.IsBold = true;
						paramExPCL_PP.IsUnderline = true;
						paramExPCL_PP.FontIndex = 5;
						docExPCL_PP.WriteText("This is a sample", 2475, 0, paramExPCL_PP);
						paramExPCL_PP.IsAnnotate = false;
						paramExPCL_PP.IsBold = false;
						paramExPCL_PP.IsUnderline = false;
						paramExPCL_PP.HorizontalMultiplier = 2;
						paramExPCL_PP.VerticalMultiplier = 2;
						paramExPCL_PP.FontIndex = 5;
						docExPCL_PP.WriteText("This is a sample", 2425, 0, paramExPCL_PP);
						docExPCL_PP.PageHeight = 3000;
						printData = docExPCL_PP.GetDocumentData();
					}

					// Print all barcodes
					else if (selectedItemIndex == 1)
					{
						docExPCL_PP.DrawBarCode(0, 0, (ParametersExPCL_PP.RotationAngle)0, true, (ParametersExPCL_PP.BarcodeExPCL_PP)1, 25, "12345");
						docExPCL_PP.DrawBarCode(0, 50, (ParametersExPCL_PP.RotationAngle)0, true, (ParametersExPCL_PP.BarcodeExPCL_PP)2, 25, "SAMPLE");
						docExPCL_PP.DrawBarCode(0, 100, (ParametersExPCL_PP.RotationAngle)0, true, (ParametersExPCL_PP.BarcodeExPCL_PP)2, 25, "sample");
						docExPCL_PP.DrawBarCode(0, 150, (ParametersExPCL_PP.RotationAngle)0, true, (ParametersExPCL_PP.BarcodeExPCL_PP)2, 25, "12");
						docExPCL_PP.DrawBarCode(0, 200, (ParametersExPCL_PP.RotationAngle)0, true, (ParametersExPCL_PP.BarcodeExPCL_PP)3, 25, "1234567890");
						docExPCL_PP.DrawBarCode(0, 250, (ParametersExPCL_PP.RotationAngle)0, true, (ParametersExPCL_PP.BarcodeExPCL_PP)4, 40, "123456789012");
						docExPCL_PP.DrawBarCode(0, 325, (ParametersExPCL_PP.RotationAngle)0, true, (ParametersExPCL_PP.BarcodeExPCL_PP)4, 40, "1234567");
						docExPCL_PP.DrawBarCode(0, 400, (ParametersExPCL_PP.RotationAngle)0, true, (ParametersExPCL_PP.BarcodeExPCL_PP)4, 40, "12345678");
						docExPCL_PP.DrawBarCode(0, 475, (ParametersExPCL_PP.RotationAngle)0, true, (ParametersExPCL_PP.BarcodeExPCL_PP)4, 40, "1234567890123");
						docExPCL_PP.DrawBarCode(0, 550, (ParametersExPCL_PP.RotationAngle)0, true, (ParametersExPCL_PP.BarcodeExPCL_PP)5, 15, "1234567890");

						// Rotate 180 all barcodes
						docExPCL_PP.DrawBarCode(384, 1175, (ParametersExPCL_PP.RotationAngle)2, true, (ParametersExPCL_PP.BarcodeExPCL_PP)1, 25, "12345");
						docExPCL_PP.DrawBarCode(384, 1125, (ParametersExPCL_PP.RotationAngle)2, true, (ParametersExPCL_PP.BarcodeExPCL_PP)2, 25, "SAMPLE");
						docExPCL_PP.DrawBarCode(384, 1075, (ParametersExPCL_PP.RotationAngle)2, true, (ParametersExPCL_PP.BarcodeExPCL_PP)2, 25, "sample");
						docExPCL_PP.DrawBarCode(384, 1025, (ParametersExPCL_PP.RotationAngle)2, true, (ParametersExPCL_PP.BarcodeExPCL_PP)2, 25, "12");
						docExPCL_PP.DrawBarCode(384, 975, (ParametersExPCL_PP.RotationAngle)2, true, (ParametersExPCL_PP.BarcodeExPCL_PP)3, 25, "1234567890");
						docExPCL_PP.DrawBarCode(384, 925, (ParametersExPCL_PP.RotationAngle)2, true, (ParametersExPCL_PP.BarcodeExPCL_PP)4, 40, "123456789012");
						docExPCL_PP.DrawBarCode(384, 850, (ParametersExPCL_PP.RotationAngle)2, true, (ParametersExPCL_PP.BarcodeExPCL_PP)4, 40, "1234567");
						docExPCL_PP.DrawBarCode(384, 775, (ParametersExPCL_PP.RotationAngle)2, true, (ParametersExPCL_PP.BarcodeExPCL_PP)4, 40, "12345678");
						docExPCL_PP.DrawBarCode(384, 700, (ParametersExPCL_PP.RotationAngle)2, true, (ParametersExPCL_PP.BarcodeExPCL_PP)4, 40, "1234567890123");
						docExPCL_PP.DrawBarCode(384, 625, (ParametersExPCL_PP.RotationAngle)2, true, (ParametersExPCL_PP.BarcodeExPCL_PP)5, 15, "1234567890");

						// Barcodes
						docExPCL_PP.WriteBarCode((ParametersExPCL_PP.BarcodeExPCL_PP)1, 3, "sample", 2500, 0);
						paramExPCL_PP.IsAnnotate = true;
						paramExPCL_PP.IsBold = true;
						paramExPCL_PP.IsUnderline = true;
						paramExPCL_PP.BarCodeHeight = 50;
						paramExPCL_PP.Rotation = ParametersExPCL_PP.RotationAngle.Rotate_180;
						docExPCL_PP.WriteBarCode((ParametersExPCL_PP.BarcodeExPCL_PP)1, 3, "sample", 2650, 384, paramExPCL_PP);
						//paramExPCL_PP.IsAnnotate = true;
						paramExPCL_PP.IsBold = false;
						paramExPCL_PP.IsUnderline = false;
						paramExPCL_PP.BarCodeHeight = 40;
						paramExPCL_PP.Rotation = ParametersExPCL_PP.RotationAngle.Rotate_0;
						docExPCL_PP.WriteBarCode((ParametersExPCL_PP.BarcodeExPCL_PP)1, 5, "sample", 2650, 0);
						docExPCL_PP.PageHeight = 3000;
						printData = docExPCL_PP.GetDocumentData();
					}

					// Rectangle
					else if (selectedItemIndex == 2)
					{
						docExPCL_PP.DrawRectangle(0, 1200, 384, 1584, true, 0);
						docExPCL_PP.DrawRectangle(20, 1220, 364, 1564, false, 3);
						docExPCL_PP.DrawRectangle(40, 1240, 344, 1544, false, 10);
						docExPCL_PP.DrawRectangle(80, 1280, 304, 1504, false, 0);
						docExPCL_PP.DrawRectangle(110, 1310, 274, 1474, true, 3);
						docExPCL_PP.DrawRectangle(130, 1330, 254, 1454, true, 10);

						// Lines
						docExPCL_PP.WriteHorizontalLine(2450, 0, 384, 10);
						docExPCL_PP.WriteHorizontalLine(2475, 0, 384, 5, paramExPCL_PP);
						docExPCL_PP.WriteVerticalLine(2550, 5, 84, 10);
						docExPCL_PP.WriteVerticalLine(2550, 200, 84, 5, paramExPCL_PP);
						docExPCL_PP.WriteRectangle(2550, 50, 2634, 150);
						paramExPCL_PP.LineThickness = 10;
						docExPCL_PP.WriteRectangle(2550, 250, 2634, 379, paramExPCL_PP);
						docExPCL_PP.PageHeight = 3000;
						printData = docExPCL_PP.GetDocumentData();
					}
					//User selected a browsed file
					else
					{
						Bitmap anImage = null;
						String selectedItem = (String)m_printItemsComboBox.SelectedItem;
						//Check if item is an image
						String[] okFileExtensions = new String[] { ".jpg", ".png", ".gif", ".jpeg", ".bmp", ".tif", ".tiff", ".pcx" };
						foreach (String extension in okFileExtensions)
						{
							if (selectedItem.ToLower().EndsWith(extension))
							{
								anImage = new Bitmap(selectedItem);
								break;
							}
						}
						//selected item is not an image file
                        if (selectedItem.ToLower().EndsWith(".pdf"))
                        {
                            docExPCL_LP.WritePDF(selectedItem, m_printHeadWidth);
                            printData = docExPCL_LP.GetDocumentData();
                        }
                        //selected item is not an image file
                        else if (anImage == null)
						{
							FileStream fs = File.OpenRead(selectedItem);
							printData = new byte[fs.Length];
							fs.Read(printData, 0, (int)fs.Length);
							fs.Close();
						}
						else
						{
							m_statusTextBox.Text = "Processing image..\r\n";
							docExPCL_LP.WriteImage(anImage, m_printHeadWidth);
							printData = docExPCL_LP.GetDocumentData();

						}//end else
					}
				}
				//DPL printers
				else if (m_printerLanguageComboBox.SelectedItem.ToString().Equals("DPL"))
				{
					//text sample to generate
					if (selectedItemIndex == 0)
					{
                        //Enable text formatting (eg. bold, italic, underline)
                        docDPL.EnableAdvanceFormatAttribute = true;

                        //Using Internal Bitmapped Font with ID 0
                        docDPL.WriteTextInternalBitmapped("Hello World", 0, 100, 5, paramDPL);

                        //Using Downloaed Bitmapped Font with ID 100
                        docDPL.WriteTextDownloadedBitmapped("Hello World", 100, 125, 5, paramDPL);

                        //Using Internal Smooth Font with size 14
                        paramDPL.IsBold = true;
                        paramDPL.IsItalic = true;
                        paramDPL.IsUnderline = true;
                        docDPL.WriteTextInternalSmooth("Hello World", 14, 150, 5, paramDPL);

                        //write normal ASCII Text Scalable
                        paramDPL.IsBold = true;
                        paramDPL.IsItalic = false;
                        paramDPL.IsUnderline = false;
                        docDPL.WriteTextScalable("Hello World", "00", 175, 5, paramDPL);

                        //write normal ASCII Text Scalable
                        paramDPL.IsBold = false;
                        paramDPL.IsItalic = false;
                        paramDPL.IsUnderline = true;
                        docDPL.WriteTextScalable("Hello World", "00", 200, 5, paramDPL);

                        //write normal ASCII Text Scalable
                        paramDPL.IsBold = false;
                        paramDPL.IsItalic = true;
                        paramDPL.IsUnderline = false;
                        docDPL.WriteTextScalable("Hello World", "00", 225, 5, paramDPL);

                        //Using Chinese Font example
                        paramDPL.IsUnicode = true;
                        paramDPL.DBSymbolSet = ParametersDPL.DoubleByteSymbolSet.Unicode;
                        paramDPL.FontHeight = 8;
                        paramDPL.FontWidth = 8;

                        int width = 5;

                        paramDPL.IsBold = true;
                        paramDPL.IsItalic = true;
                        paramDPL.IsUnderline = false;
                        docDPL.WriteTextScalable("你好，世界 (Hello World in Chinese!)", "50", 250, width, paramDPL);
						printData = docDPL.GetDocumentData();
					}
					else if (selectedItemIndex == 1)
					{
						docDPL.PrintQuantity = 3;
						paramDPL.EmbeddedEnable = false;
						paramDPL.IncrementDecrementValue = 5;
						paramDPL.IncrementDecrementType = ParametersDPL.IncrementDecrementTypeValue.NumericIncrement;
						docDPL.WriteTextInternalBitmapped("12345", 3, 0, 0, paramDPL);

						paramDPL.EmbeddedEnable = false;
						paramDPL.IncrementDecrementValue = 5;
						paramDPL.IncrementDecrementType = ParametersDPL.IncrementDecrementTypeValue.AlphanumericIncrement;
						docDPL.WriteTextInternalBitmapped("ABC123", 3, 35, 0, paramDPL);

						paramDPL.EmbeddedEnable = false;
						paramDPL.IncrementDecrementValue = 5;
						paramDPL.IncrementDecrementType = ParametersDPL.IncrementDecrementTypeValue.HexdecimalIncrement;
						docDPL.WriteTextInternalBitmapped("0A0D", 3, 70, 0, paramDPL);

						paramDPL.EmbeddedEnable = true;
						paramDPL.EmbeddedIncrementDecrementValue = "001001001";
						paramDPL.IncrementDecrementType = ParametersDPL.IncrementDecrementTypeValue.NumericIncrement;
						docDPL.WriteTextInternalBitmapped("AB1CD1EF1", 3, 105, 0, paramDPL);

						printData = docDPL.GetDocumentData();
					}
					//Barcodes
					else if (selectedItemIndex == 2)
					{
						//Test print Code 3 of 9
						//Barcode A with default parameter
						docDPL.WriteBarCode("A", "BRCDA", 0, 0);
						docDPL.WriteTextInternalBitmapped("Barcode A", 1, 60, 0);

						//Barecode A with specified parameters
						paramDPL.IsUnicode = false;
						paramDPL.WideBarWidth = 3;
						paramDPL.NarrowBarWidth = 1;
						paramDPL.SymbolHeight = 20;

						docDPL.WriteBarCode("A", "BRCDA", 100, 0, paramDPL);
						docDPL.WriteTextInternalBitmapped("Barcode A", 1, 135, 0);

						//UPC-A with specified parameters
						paramDPL.IsUnicode = false;
						paramDPL.WideBarWidth = 3;
						paramDPL.NarrowBarWidth = 1;
						paramDPL.SymbolHeight = 10;
						docDPL.WriteBarCode("B", "012345678912", 160, 0, paramDPL);
						docDPL.WriteTextInternalBitmapped("UPC-A", 1, 185, 0);
						//Code 128
						//Barecode A with specified parameters
						paramDPL.IsUnicode = false;
						paramDPL.WideBarWidth = 3;
						paramDPL.NarrowBarWidth = 1;
						paramDPL.SymbolHeight = 20;
						docDPL.WriteBarCode("E", "ACODE128", 210, 0, paramDPL);
						docDPL.WriteTextInternalBitmapped("Code 128", 1, 250, 0);

						//EAN-13
						paramDPL.IsUnicode = false;
						paramDPL.WideBarWidth = 3;
						paramDPL.NarrowBarWidth = 1;
						paramDPL.SymbolHeight = 20;
						docDPL.WriteBarCode("F", "0123456789012", 285, 0, paramDPL);
						docDPL.WriteTextInternalBitmapped("EAN-13", 1, 315, 0);
						//EAN Code 128
						paramDPL.IsUnicode = false;
						paramDPL.WideBarWidth = 3;
						paramDPL.NarrowBarWidth = 1;
						paramDPL.SymbolHeight = 20;
						docDPL.WriteBarCode("Q", "0123456789012345678", 355, 0, paramDPL);
						docDPL.WriteTextInternalBitmapped("EAN Code 128", 1, 395, 0);
						//UPS MaxiCode, Mode 2 & 3
						paramDPL.IsUnicode = false;
						paramDPL.WideBarWidth = 0;
						paramDPL.NarrowBarWidth = 0;
						paramDPL.SymbolHeight = 0;

						UPSMessage upsMessage = new UPSMessage("920243507", 840, 1, "1Z00004951", "UPSN", "9BCJ43", 365, "625TH9", 1, 1, 10, true, "669 SECOND ST", "ENCINITAS", "CA");

						docDPL.WriteBarCodeUPSMaxiCode(2, upsMessage, 445, 0, paramDPL);
						docDPL.WriteTextInternalBitmapped("UPS MaxiCode", 1, 560, 0);

						//PDF-417
						paramDPL.IsUnicode = false;
						paramDPL.WideBarWidth = 0;
						paramDPL.NarrowBarWidth = 0;
						paramDPL.SymbolHeight = 0;
						docDPL.WriteBarCodePDF417("ABCDEF1234", false, 1, 0, 0, 0, 590, 0, paramDPL);
						docDPL.WriteTextInternalBitmapped("PDF-417", 1, 630, 0);

						//Data Matrix
						paramDPL.IsUnicode = false;
						paramDPL.WideBarWidth = 4;
						paramDPL.NarrowBarWidth = 4;
						paramDPL.SymbolHeight = 0;

						docDPL.WriteBarCodeDataMatrix("DATAMAX", 140, 0, 0, 0, 670, 0, paramDPL);
						docDPL.WriteTextInternalBitmapped("Data Matrix w/ ECC 140", 1, 770, 0);
						docDPL.WriteBarCodeDataMatrix("DATAMAX", 200, 0, 0, 0, 810, 0, paramDPL);
						docDPL.WriteTextInternalBitmapped("Data Matrix w/ ECC 200", 1, 880, 0);

						//QRCODE
						paramDPL.IsUnicode = false;
						paramDPL.WideBarWidth = 4;
						paramDPL.NarrowBarWidth = 4;
						paramDPL.SymbolHeight = 0;
						//AutoFormatting
						docDPL.WriteBarCodeQRCode("This is the data portion", true, 0, "", "", "", "", 920, 0, paramDPL);
						docDPL.WriteTextInternalBitmapped("QR Barcode w/ Auto Formatting", 1, 1030, 0);

						//Manual Formatting
						docDPL.WriteBarCodeQRCode("1234This is the data portion", false, 2, "H", "4", "M", "A", 1070, 0, paramDPL);
						docDPL.WriteTextInternalBitmapped("QR Barcode w/ Manual formatting", 1, 1200, 0);


						//Test BarcodeAzTec
						paramDPL.IsUnicode = false;
						paramDPL.WideBarWidth = 12;
						paramDPL.NarrowBarWidth = 12;
						paramDPL.SymbolHeight = 0;
						docDPL.WriteBarCodeAztec("ABCD1234", 0, false, 0, 1240, 0, paramDPL);
						docDPL.WriteTextInternalBitmapped("Aztec Barcode ECI 0, ECC 0", 1, 1360, 0);
						docDPL.WriteBarCodeAztec("ABCD1234", 17, true, 232, 1400, 0, paramDPL);
						docDPL.WriteTextInternalBitmapped("Aztec Barcode ECI 1, ECC 232", 1, 1500, 0);

						//GS1 Databars
						paramDPL.WideBarWidth = 2;
						paramDPL.NarrowBarWidth = 2;
						paramDPL.SymbolHeight = 0;

						docDPL.WriteBarCodeGS1DataBar("2001234567890", "", "E", 1, 0, 0, 2, 1540, 0, paramDPL);
						docDPL.WriteTextInternalBitmapped("GS1 Databar Expanded", 1, 1760, 0);

						docDPL.WriteBarCodeGS1DataBar("2001234567890", "hello123World", "D", 1, 0, 0, 0, 1800, 0, paramDPL);
						docDPL.WriteTextInternalBitmapped("GS1 Stacked Omni Direction", 1, 1980, 0);

						//Austrailia 4-State
						docDPL.WriteBarCodeAusPost4State("A124B", true, 59, 32211324, 2020, 0, paramDPL);
						docDPL.WriteTextInternalBitmapped("Aus Post 4 State readable", 1, 2100, 0);
						docDPL.WriteBarCodeAusPost4State("123456789012345", false, 62, 39987520, 2140, 0, paramDPL);
						docDPL.WriteTextInternalBitmapped("Aus Post 4 State non readable", 1, 2190, 0);


						//write CodaBlock
						paramDPL.WideBarWidth = 0;
						paramDPL.NarrowBarWidth = 0;
						docDPL.WriteBarCodeCODABLOCK("12345678", 25, "E", false, 4, 2, 2230, 0, paramDPL);
						docDPL.WriteTextInternalBitmapped("CODABLOCK", 1, 2320, 0);

						//write TCIF
						paramDPL.WideBarWidth = 0;
						paramDPL.NarrowBarWidth = 0;
						docDPL.WriteBarCodeTLC39("ABCD12345678901234589ABED", 0, 123456, 2360, 0, paramDPL);
						docDPL.WriteTextInternalBitmapped("TCIF", 1, 2480, 0);

						//write MicroPDF417
						paramDPL.WideBarWidth = 0;
						paramDPL.NarrowBarWidth = 0;
						docDPL.WriteBarCodeMicroPDF417("PDF417", 4, 4, false, false, 2520, 0, paramDPL);
						docDPL.WriteTextInternalBitmapped("Micro PDF417", 1, 2560, 0);
						printData = docDPL.GetDocumentData();
					}
					//graphics
					else if (selectedItemIndex == 3)
					{
						//WriteLine
						docDPL.WriteLine(0, 0, 10, 25);

						//WriteBox
						docDPL.WriteBox(50, 0, 25, 25, 1, 1);

						//WriteRectangle
						docDPL.WriteRectangle(9, 100, 10, 150, 10, 150, 200, 100, 200);
						docDPL.WriteTriangle(7, 200, 10, 250, 25, 200, 40);
						docDPL.WriteCircle(4, 300, 25, 25);
						printData = docDPL.GetDocumentData();
					}
					//image
					else if (selectedItemIndex == 4)
					{
						m_statusTextBox.Text = "Processing image..\r\n";
						m_statusTextBox.SelectionStart = m_statusTextBox.Text.Length;
						m_statusTextBox.ScrollToCaret();
						Bitmap anImage = new Bitmap(global::DOPrint_WD.Properties.Resources.dologo);
						docDPL.WriteTextInternalBitmapped("This is a D-O Logo", 1, 130, 200);
						docDPL.WriteImage(anImage, 150, 0, 0, paramDPL);
						printData = docDPL.GetDocumentData();
					}
					//User selected a browsed file
					else
					{
						bool isImage = false;
						String selectedItem = (String)m_printItemsComboBox.SelectedItem;
						//Check if item is an image
						String[] okFileExtensions = new String[] { ".jpg", ".png", ".gif", ".jpeg", ".bmp", ".tif", ".tiff", ".pcx" };
						foreach (String extension in okFileExtensions)
						{
							if (selectedItem.ToLower().EndsWith(extension))
							{
								isImage = true;
								break;
							}
						}
						//selected item is not an image file
                        if (selectedItem.ToLower().EndsWith(".pdf"))
                        {
                            docDPL.WritePDF(selectedItem, m_printHeadWidth, 0, 0);
                            printData = docDPL.GetDocumentData();
                        }
                        //selected item is not an image file
                        else if (!isImage)
						{
							FileStream fs = File.OpenRead(selectedItem);
                            printData = new byte[(int)fs.Length];
							fs.Read(printData, 0, (int)fs.Length);
							fs.Close();
						}
						else
						{
							m_statusTextBox.Text = "Processing image..\r\n";
							DocumentDPL.ImageType imgType = DocumentDPL.ImageType.Other;
							if (selectedItem.ToLower().EndsWith(".pcx"))
							{
								imgType = DocumentDPL.ImageType.PCXFlipped_8Bit;
							}
							else
							{
								imgType = DocumentDPL.ImageType.Other;
							}
							docDPL.WriteImage(selectedItem, imgType, 0, 0, paramDPL);
							printData = docDPL.GetDocumentData();
						}//end else
					}//end else
				}
			}
		}
        private void run()
        {
            try {
                enableButton(m_performButton, false);

                //Establishing connection
                conn = null;
                //====FOR BLUETOOTH CONNECTIONS========//
                if (connType.Equals("Bluetooth"))
                {
                    if (!Regex.IsMatch(m_deviceMAC, "^([0-9A-Fa-f]{2}[:-]){5}([0-9A-Fa-f]{2})$") && !Regex.IsMatch(m_deviceMAC, "^([0-9A-Fa-f]{12})$"))
                        throw new Exception("Invalid Bluetooth Address format entered.");
                    conn = (Connection_Bluetooth32Feet)Connection_Bluetooth32Feet.CreateClient(m_deviceMAC, m_passKey, false);
                }
                //====FOR TCP Connection==//
                else if (connType.Equals("TCP/IP"))
                {
                    if (!Regex.IsMatch(m_deviceIP, "^([01]?\\d\\d?|2[0-4]\\d|25[0-5])\\.([01]?\\d\\d?|2[0-4]\\d|25[0-5])\\.([01]?\\d\\d?|2[0-4]\\d|25[0-5])\\.([01]?\\d\\d?|2[0-4]\\d|25[0-5])$"))
                        throw new Exception("Invalid IP Address format entered.");
                    conn = (Connection_TCP)Connection_TCP.CreateClient(m_deviceIP, m_devicePort, false);
                }
                
                if (printRadioChecked)
                {
                    updateStatusText(String.Format("[{0:MM/dd/yyy hh:mm:ss.fff}]", System.DateTime.Now) + " Establishing connection..\r\n");

                    //Open bluetooth socket
                    if (!conn.IsOpen)
                        conn.Open();

                    updateStatusText(String.Format("[{0:MM/dd/yyy hh:mm:ss.fff}]", System.DateTime.Now) + " Checking if printer is ready..\r\n");
                    //=============Check status of printer==========//
                    int retryCount = 0;
                    //Wait for printer to finish printing before closing connection
                    while (retryCount < 5)
                    {
                        if (GetStatus())
                            break;
                        //sleep for a bit
                        Thread.Sleep(250);
                        retryCount++;
                    }
                    //=============================================//

                    updateStatusText(String.Format("[{0:MM/dd/yyy hh:mm:ss.fff}]", System.DateTime.Now) + " Sending data to printer..\r\n");

                    //====Send data in chunks of 1024 bytes=========//
                    uint bytesWritten = 0;
                    uint bytesToWrite = 1024;
                    uint totalBytes = (uint)printData.Length;
                    uint remainingBytes = totalBytes;
                    while (bytesWritten < totalBytes)
                    {
                        if (remainingBytes < bytesToWrite)
                            bytesToWrite = remainingBytes;
                        conn.Write(printData, (int)bytesWritten, (int)bytesToWrite);
                        bytesWritten += bytesToWrite;
                        remainingBytes = remainingBytes - bytesToWrite;
                        Thread.Sleep(100);
                    }
                    //==============================================//

                    //=============Check status of printer==========//
                    updateStatusText(String.Format("[{0:MM/dd/yyy hh:mm:ss.fff}]", System.DateTime.Now) + " Waiting for printer..\r\n");
                    retryCount = 0;
                    //Wait for printer to finish printing before closing connection
                    while (retryCount < 5)
                    {
                        if (GetStatus())
                            break;
                        //sleep for a bit
                        Thread.Sleep(250);
                        retryCount++;
                    }
                    //=============================================//

                    updateStatusText(String.Format("[{0:MM/dd/yyy hh:mm:ss.fff}]", System.DateTime.Now) + " Closing connection..\r\n");

                    //Signal to close connection
					//conn.IsClosing = true;
					conn.Close();

                    updateStatusText(String.Format("[{0:MM/dd/yyy hh:mm:ss.fff}]", System.DateTime.Now) + " Print success!\r\n");
                }//end print

                //For Query
                else if (printRadioChecked == false)
                {
                    String message = "";
                    String title = "";
                    updateStatusText(String.Format("[{0:MM/dd/yyy hh:mm:ss.fff}]", System.DateTime.Now) + " Establishing connection..\r\n");

                    //Open bluetooth socket
                    if (!conn.IsOpen)
                        conn.Open();

                    updateStatusText(String.Format("[{0:MM/dd/yyy hh:mm:ss.fff}]", System.DateTime.Now) + " Querying data..\r\n");

                    //If ExPCL is selected
                    if (selectedLanguageIndex == 3 || selectedLanguageIndex == 4)
                    {
                        //Battery Condition
                        if (selectedItemIndex == 0)
                        {
                            BatteryCondition_ExPCL batteryCond = new BatteryCondition_ExPCL(conn);
                            batteryCond.QueryPrinter(600);

                            if (batteryCond.Valid == false)
                            {
                                message += "No response from printer\r\n";
                            }
                            else {
                                message += String.Format("Battery Voltage: {0}\r\n", batteryCond.VoltageBatterySingle );
                            }
                            title = "Battery Condition";

                        }
                        //Bluetooth
                        else if (selectedItemIndex == 1)
                        {
                            BluetoothConfiguration_ExPCL btConfig = new BluetoothConfiguration_ExPCL(conn);
                            btConfig.QueryPrinter(600);

                            if (btConfig.Valid == false)
                            {
                                message += "No response from printer\r\n";
                            }
                            else
                            {
                                message += String.Format("Local Classic Name: {0}\r\n", btConfig.LocalClassicName);
                                message += String.Format("Local COD: {0}\r\n", btConfig.DeviceClass);
                                message += String.Format("Power Save Mode: {0}\r\n", btConfig.PowerSave);
                                message += String.Format("Security Mode: {0}\r\n", btConfig.Security);
                                message += String.Format("Discoverable: {0}\r\n", btConfig.Discoverable);
                                message += String.Format("Connectable: {0}\r\n", btConfig.Connectable);
                                message += String.Format("Bondable: {0}\r\n", btConfig.Bondable);
                                message += String.Format("Bluetooth Address: {0}\r\n", btConfig.BluetoothAddress);
                            }
                            title = "Bluetooth Config";
                        }
                        //General Status
                        if (selectedItemIndex == 2)
                        {
                            GeneralStatus_ExPCL generalStatus = new GeneralStatus_ExPCL(conn);
                            generalStatus.QueryPrinter(600);

                            if (generalStatus.Valid == false)
                            {
                                message += "No response from printer\r\n";
                            }
                            else
                            {
                                message += String.Format("Printer Error: {0}\r\n", generalStatus.PrinterError ? "Yes" : "No");
                                message += String.Format("Head Lever Latched: {0}\r\n", generalStatus.HeadLeverLatched ? "Yes" : "No");
                                message += String.Format("Paper Present: {0}\r\n", generalStatus.PaperPresent ? "Yes" : "No");
                                message += String.Format("Battery Status: {0}\r\n", generalStatus.BatteryVoltageStatus);
                                message += String.Format("Print Head Temperature Acceptable: {0}\r\n", generalStatus.PrintheadTemperatureAcceptable ? "Yes" : "No");
                                message += String.Format("Text Queue Empty: {0}\r\n", generalStatus.TextQueueEmpty ? "Yes" : "No");
                            }
                            title = "General Status";

                        }
                        //Magnetic Card Data
                        else if (selectedItemIndex == 3)
                        {
                            MagneticCardData_ExPCL mcrData = new MagneticCardData_ExPCL(conn);
                            mcrData.QueryPrinter(600);


                            if (mcrData.Valid == false)
                            {
                                message += "No response from printer\r\n";
                            }
                            else
                            {
                                message += String.Format("Track 1: {0}\r\n", mcrData.Track1Data);
                                message += String.Format("Track 2: {0}\r\n", mcrData.Track2Data);
                                message += String.Format("Track 3: {0}\r\n", mcrData.Track3Data);
                            }
                            title = "Magnetic Card Data";
                        }
                        //Memory Status
                        if (selectedItemIndex == 4)
                        {
                            MemoryStatus_ExPCL memoryStatus = new MemoryStatus_ExPCL(conn);
                            memoryStatus.QueryPrinter(600);

                            if (memoryStatus.Valid == false)
                            {
                                message += "No response from printer\r\n";
                            }
                            else
                            {
                                message += String.Format("Print Buffer KB Remaining: {0}\r\n", memoryStatus.RemainingRAM);
                                message += String.Format("Used RAM: {0}\r\n", memoryStatus.UsedRAM);
                            }
                            title = "Memory Status";

                        }
                        //Printer Options
                        if (selectedItemIndex == 5)
                        {
                            PrinterOptions_ExPCL printerOpt = new PrinterOptions_ExPCL(conn);
                            printerOpt.QueryPrinter(600);

                            if (printerOpt.Valid == false)
                            {
                                message += "No response from printer\r\n";
                            }
                            else
                            {
                                message += String.Format("Power Down Timer: {0}\r\n", printerOpt.PowerDownTimer);
                            }
                            title = "Printer Options";

                        }
                        //Printhead Status
                        if (selectedItemIndex == 6)
                        {
                            PrintheadStatus_ExPCL printheadStatus = new PrintheadStatus_ExPCL(conn);
                            printheadStatus.QueryPrinter(600);

                            if (printheadStatus.Valid == false)
                            {
                                message += "No response from printer\r\n";
                            }
                            else
                            {
                                message += String.Format("PrintHead Temperature: {0}\r\n", printheadStatus.PrintheadTemperature);
                            }
                            title = "General Status";

                        }
                        //Version information
                        else if (selectedItemIndex == 7)
                        {
                            VersionInformation_ExPCL versionInfo = new VersionInformation_ExPCL(conn);
                            versionInfo.QueryPrinter(600);

                            if (versionInfo.Valid == false)
                            {
                                message += "No response from printer\r\n";
                            }
                            else
                            {
                                message += String.Format("Hardware Version: {0}\r\n", versionInfo.HardwareControllerVersion);
                                message += String.Format("Firmware Version: {0}\r\n", versionInfo.FirmwareVersion);

                            }
                            title = "Version Information";

                        }
                    }//end of ExPCL mode
                    //DPL Mode
                    else if (selectedLanguageIndex == 2)
                    {
                        //Printer Info
                        if (selectedItemIndex == 0)
                        {
                            //Query Printer info
                            PrinterInformation_DPL printerInfo = new PrinterInformation_DPL(conn);

                            printerInfo.QueryPrinter(600);


                            if (printerInfo.Valid == false)
                            {
                                message += "No response from printer\r\n";
                            }
                            else
                            {
                                message += String.Format("Serial Number: {0}\r\n", printerInfo.PrinterSerialNumber);
                                message += String.Format("Boot 1 Version: {0}\r\n", printerInfo.Boot1Version);
                                message += String.Format("Boot 1 Part Number: {0}\r\n", printerInfo.Boot1PartNumber);
                                message += String.Format("Boot 2 Version: {0}\r\n", printerInfo.Boot2Version);
                                message += String.Format("Boot 2 PartNumber: {0}\r\n", printerInfo.Boot1PartNumber);
                                message += String.Format("Firmware Version: {0}\r\n", printerInfo.VersionInformation);
                                message += String.Format("AVR Version: {0}\r\n", printerInfo.AVRVersionInformation);
                                message += String.Format("xAVR Version: {0}\r\n", printerInfo.xAVRVersionInformation);
                            }
                            title = "Printer Information";

                        }
                        //Fonts and files
                        else if (selectedItemIndex == 1)
                        {
                            //Query Memory Module
                            Fonts_DPL fontsDPL = new Fonts_DPL(conn);
                            fontsDPL.QueryPrinter(1000);


                            if (fontsDPL.Valid == false)
                            {
                                message += "No response from printer\r\n";
                            }
                            else
                            {
                                message += "FILES IN G: \r\n";

                                //Get All Files
                                MemoryModules_DPL.FileInformation[] files = fontsDPL.GetFiles("G");
                                if (files != null)
                                {
                                    if (files.Length == 0)
                                        message += "No files found in module.\r\n";
                                    else
                                    {
                                        foreach (MemoryModules_DPL.FileInformation file in files)
                                        {
                                            message += String.Format("Name: {0}, Size: {1}, Type: {2}\r\n", file.FileName, file.FileSize, file.FileType);
                                        }
                                    }
                                }
                                //Get internal Fonts
                                message += "INTERNAL FONTS: \r\n";
                                String[] internalFonts = fontsDPL.GetInternalFonts();
                                foreach (String internalFont in internalFonts)
                                {
                                    message += String.Format("Name: {0}\r\n", internalFont);

                                }
                            }
                            title = "Files and Internal Fonts";
                        }

                        //Media Label
                        else if (selectedItemIndex == 2)
                        {
                            //Query Media Label
                            MediaLabel_DPL mediaLabel = new MediaLabel_DPL(conn);
                            mediaLabel.QueryPrinter(600);

                            if (mediaLabel.Valid == false)
                            {
                                message += "No response from printer\r\n";
                            }
                            else
                            {
                                message += String.Format("Media Type: {0}\r\n", mediaLabel.MediaType);
                                message += String.Format("Max Label Length: {0}\r\n", mediaLabel.MaxLabelLength);
                                message += String.Format("Continuous Label Length: {0}\r\n", mediaLabel.ContinousLabelLength);
                                message += String.Format("Sensor Type: {0}\r\n", mediaLabel.SensorType);
                                message += String.Format("Paper Empty Distance: {0}\r\n", mediaLabel.PaperEmptyDistance);
                                message += String.Format("Label Width: {0}\r\n", mediaLabel.LabelWidth);
                                message += String.Format("Head Cleaning Threshold: {0}\r\n", mediaLabel.HeadCleaningThreshold);
                                message += String.Format("Ribbon Low Diameter: {0}\r\n", mediaLabel.RibbonLowDiameter);
                                message += String.Format("Ribbon Low Pause Enable: {0}\r\n", mediaLabel.RibbonLowPause ? "Yes" : "No");
                                message += String.Format("Label Length Limit Enable: {0}\r\n", mediaLabel.LabelLengthLimit ? "Yes" : "No");
                                message += String.Format("Present Backup Enable: {0}\r\n", mediaLabel.PresentBackup ? "Yes" : "No");
                                message += String.Format("Present Distance: {0}\r\n", mediaLabel.PresentDistance);
                                message += String.Format("Backup Distance: {0}\r\n", mediaLabel.BackupDistance);
                                message += String.Format("Stop Location: {0}\r\n", mediaLabel.StopLocation);
                                message += String.Format("Backup After Print Enable: {0}\r\n", mediaLabel.BackupAfterPrint ? "Yes" : "No");
                                message += String.Format("Gap Alternative Mode: {0}\r\n", mediaLabel.GapAlternateMode ? "Yes" : "No");
                            }

                            title = "Media Label";
                        }

                        //Print Controls
                        else if (selectedItemIndex == 3)
                        {
                            //Print Controls
                            PrintSettings_DPL printSettings = new PrintSettings_DPL(conn);
                            printSettings.QueryPrinter(600);

                            if (printSettings.Valid == false)
                            {
                                message += "No response from printer\r\n";
                            }
                            else
                            {
                                message += String.Format("Backup Delay: {0}\r\n", printSettings.BackupDelay);
                                message += String.Format("Row Offset: {0}\r\n", printSettings.RowOffset);
                                message += String.Format("Column Offset: {0}\r\n", printSettings.ColumnOffset);
                                message += String.Format("Row Adjusted Fine Tune: {0}\r\n", printSettings.RowAdjustFineTune);
                                message += String.Format("Column Adjusted Fine Tune: {0}\r\n", printSettings.ColumnAdjustFineTune);
                                message += String.Format("Present Fine Tune: {0}\r\n", printSettings.PresentAdjustFineTune);
                                message += String.Format("Darkness Level: {0}\r\n", printSettings.DarknessLevel);
                                message += String.Format("Contrast Level: {0}\r\n", printSettings.ContrastLevel);
                                message += String.Format("Heat Level: {0}\r\n", printSettings.HeatLevel);
                                message += String.Format("Backup Speed: {0}\r\n", printSettings.BackupSpeed);
                                message += String.Format("Feed Speed: {0}\r\n", printSettings.FeedSpeed);
                                message += String.Format("Print Speed: {0}\r\n", printSettings.PrintSpeed);
                                message += String.Format("Slew Speed: {0}\r\n", printSettings.SlewSpeed);
                            }

                            title = "Print Controls";
                        }

                        //System Settings
                        else if (selectedItemIndex == 4)
                        {

                            //System Settings
                            SystemSettings_DPL sysSettings = new SystemSettings_DPL(conn);
                            sysSettings.QueryPrinter(600);

                            if (sysSettings.Valid == false)
                            {
                                message += "No response from printer\r\n";
                            }
                            else
                            {
                                message += String.Format("Unit Measure: {0}\r\n", sysSettings.UnitMeasure);
                                message += String.Format("ESC Sequence Enable: {0}\r\n", sysSettings.EscapeSequences ? "Yes" : "No");
                                message += String.Format("Single Byte Symbol: {0}\r\n", sysSettings.SingleByteSymbolSet);
                                message += String.Format("Double Byte Symbol: {0}\r\n", sysSettings.DoubleByteSymbolSet);
                                message += String.Format("Disable Symbol Set Value Selection: {0}\r\n", sysSettings.SymbolSetValueSelection ? "Yes" : "No");
                                message += String.Format("Menu Mode: {0}\r\n", sysSettings.MenuMode);
                                message += String.Format("Start of Print Emulation: {0}\r\n", sysSettings.StartOfPrintEmulation);
                                message += String.Format("Image mode: {0}\r\n", sysSettings.ImageMode);
                                message += String.Format("Menu Language: {0}\r\n", sysSettings.MenuLanguage);
                                message += String.Format("Display Mode: {0}\r\n", sysSettings.DisplayMode);
                                message += String.Format("Block Allocated for Internal Module: {0}\r\n", sysSettings.InternalModuleSize);
                                message += String.Format("Scalable Font Cache: {0}\r\n", sysSettings.ScalableFontCache);
                                message += String.Format("Legacy Emulation: {0}\r\n", sysSettings.LegacyEmulation);
                                message += String.Format("Column Emulation: {0}\r\n", sysSettings.ColumnEmulation);
                                message += String.Format("Row Emulation: {0}\r\n", sysSettings.RowEmulation);
                                message += String.Format("Fault Handling Level: {0}\r\n", sysSettings.FaultHandlingLevel);
                                message += String.Format("Fault Handling Void Distance: {0}\r\n", sysSettings.FaultHandlingVoidDistance);
                                message += String.Format("Fault Handling Retry Counts: {0}\r\n", sysSettings.FaultHandlingRetryCounts);
                                message += String.Format("Font Emulation: {0}\r\n", sysSettings.FontEmulation);
                                message += String.Format("Input Mode: {0}\r\n", sysSettings.InputMode);

                                Honeywell.Printer.Configuration.DPL.SystemSettings_DPL.InputModeValue[] inputModes = sysSettings.EmulationsUsedForAutoMode;

                                message += "Emulations Used for Input Mode: ";
                                int inputModeValue = 0;
                                foreach (Honeywell.Printer.Configuration.DPL.SystemSettings_DPL.InputModeValue value in inputModes)
                                    inputModeValue |= (1 << (int)value);
                                message += inputModeValue + "\r\n";

                                message += String.Format("Retract Delay: {0}\r\n", sysSettings.RetractDelay);
                                message += String.Format("Label Rotation: {0}\r\n", sysSettings.LabelRotation);
                                message += String.Format("Label Store Level: {0}\r\n", sysSettings.LabelStoreLevel);
                                message += String.Format("Scalable Font Bolding: {0}\r\n", sysSettings.ScalableFontBolding);
                                message += String.Format("Format Attribute: {0}\r\n", sysSettings.FormatAttribute);
                                message += String.Format("Beeper State: {0}\r\n", sysSettings.BeeperState);
                                message += String.Format("Host Timeout: {0}\r\n", sysSettings.HostTimeout);
                                message += String.Format("Printer Sleep Timeout: {0}\r\n", sysSettings.PrinterSleepTimeout);
                                message += String.Format("Backlight Mode: {0}\r\n", sysSettings.BacklightMode);
                                message += String.Format("Backlight Timer: {0}\r\n", sysSettings.BacklightTimer);
                                message += String.Format("Power Down Timeout: {0}\r\n", sysSettings.PowerDownTimeout);
                                message += String.Format("RF Power Down Timeout: {0}\r\n", sysSettings.RFPowerDownTimeout);
                                message += String.Format("User Label Mode Enable: {0}\r\n", sysSettings.UserLabelMode ? "Yes" : "No");
                                message += String.Format("Radio Status: {0}\r\n", sysSettings.RadioPowerState ? "Radio on" : "Radio off");
                                message += String.Format("Supress Auto Reset: {0}\r\n", sysSettings.SuppressAutoReset ? "Yes" : "No");
                            }

                            title = "System Settings";
                        }

                        //Sensor Calibration
                        else if (selectedItemIndex == 5)
                        {
                            //Sensor Calibration
                            SensorCalibration_DPL sensorCalibration = new SensorCalibration_DPL(conn);
                            sensorCalibration.QueryPrinter(600);

                            if (sensorCalibration.Valid == false)
                            {
                                message += "No response from printer\r\n";
                            }
                            else
                            {
                                message += String.Format("Black Mark Paper value: {0}\r\n", sensorCalibration.BlackMarkPaperValue);
                                message += String.Format("Black Mark Sensor Gain value: {0}\r\n", sensorCalibration.BlackMarkSensorGain);
                                message += String.Format("Black Mark value: {0}\r\n", sensorCalibration.BlackMarkValue);
                                message += String.Format("Gap Sensor Gain value: {0}\r\n", sensorCalibration.GapSensorGain);
                                message += String.Format("Gap Sensor Gain should be used with Thermal Transfer Media value: {0}\r\n", sensorCalibration.GapSensorGainWithThermalTransferMedia);
                                message += String.Format("Gap Mark Level value: {0}\r\n", sensorCalibration.GapMarkLevel);
                                message += String.Format("Gap Mark Level should be used with Thermal Transfer Media value: {0}\r\n", sensorCalibration.GapMarkLevelWithThermalTransferMedia);
                                message += String.Format("Paper Level value: {0}\r\n", sensorCalibration.PaperLevel);
                                message += String.Format("Paper Level should be used with Thermal Transfer Media value: {0}\r\n", sensorCalibration.PaperLevelWithThermalTransferMedia);
                                message += String.Format("Presenter Sensor Gain value: {0}\r\n", sensorCalibration.PresenterSensorGain);
                                message += String.Format("Sensor Clear Value: {0}\r\n", sensorCalibration.SensorClearValue);
                                message += String.Format("Sensor Clear Value should be used with Thermal Transfer Media: {0}\r\n", sensorCalibration.SensorClearValueWithThermalTransferMedia);
                                message += String.Format("Auto Calibration Mode Enable: {0}\r\n", sensorCalibration.AutoCalibrationMode ? "Yes" : "No");
                            }
                            title = "Sensor Calibration";
                        }

                        //Miscellaneous
                        else if (selectedItemIndex == 6)
                        {

                            //Misc
                            Miscellaneous_DPL misc = new Miscellaneous_DPL(conn);
                            misc.QueryPrinter(600);

                            if (misc.Valid == false)
                            {
                                message += "No response from printer\r\n";
                            }
                            else
                            {
                                message += String.Format("Delay Rate: {0}\r\n", misc.DelayRate);
                                message += String.Format("Present Sensor Equipped: {0}\r\n", misc.PresentSensorEquipped);
                                message += String.Format("Cutter Equipped: {0}\r\n", misc.CutterEquipped);
                                message += String.Format("Control Code: {0}\r\n", misc.ControlCode);
                                message += String.Format("Start of Print Signal: {0}\r\n", misc.StartOfPrintSignal);
                                message += String.Format("End of Print Signal: {0}\r\n", misc.EndOfPrintSignal);
                                message += String.Format("GPIO Slew: {0}\r\n", misc.GPIOSlew);
                                message += String.Format("Feedback Mode Enable: {0}\r\n", misc.FeedbackMode ? "Yes" : "No");
                                message += String.Format("Comm Heat Commands Enable: {0}\r\n", misc.CommunicationHeatCommands ? "Yes" : "No");
                                message += String.Format("Comm Speed Commands Enable: {0}\r\n", misc.CommunicationSpeedCommands ? "Yes" : "No");
                                message += String.Format("Comm TOF Commands Enable: {0}\r\n", misc.CommunicationTOFCommands ? "Yes" : "No");
                                message += String.Format("British Pound Enable: {0}\r\n", misc.BritishPound ? "Yes" : "No");
                                message += String.Format("GPIO Backup Label: {0}\r\n", misc.GPIOBackupLabel);
                                message += String.Format("Ignore Control Code Enable: {0}\r\n", misc.IgnoreControlCode ? "Yes" : "No");
                                message += String.Format("Sofware Switch Enable: {0}\r\n", misc.SoftwareSwitch ? "Yes" : "No");
                                message += String.Format("Max Length Ignore Enable: {0}\r\n", misc.MaximumLengthIgnore ? "Yes" : "No");
                                message += String.Format("Pause Mode Enable: {0}\r\n", misc.PauseMode ? "Yes" : "No");
                                message += String.Format("Peel Mode Enable: {0}\r\n", misc.PeelMode ? "Yes" : "No");
                                message += String.Format("USB Mode: {0}\r\n", misc.USBMode);
                                message += String.Format("Windows Driver For EZ RLE Enable: {0}\r\n", misc.WindowsDriverForEZ_RLE ? "Yes" : "No");
                                message += String.Format("Hex Dump Enable: {0}\r\n", misc.HexDumpMode ? "Yes" : "No");
                                message += String.Format("Display Mode for IP Host Name: {0}\r\n", misc.DisplayModeForIPHostname);
                            }
                            title = "Miscellaneous";
                        }
                        //Serial Port
                        else if (selectedItemIndex == 7)
                        {
                            //SerialPort
                            SerialPortConfiguration_DPL serialConfig = new SerialPortConfiguration_DPL(conn);
                            serialConfig.QueryPrinter(600);

                            if (serialConfig.Valid == false)
                            {
                                message += "No response from printer\r\n";
                            }
                            else
                            {
                                message += String.Format("Serial Port A Baud Rate: {0}\r\n", serialConfig.BaudRate);
                                message += String.Format("Serial Port A Stop Bit: {0}\r\n", serialConfig.StopBit);
                                message += String.Format("Serial Port A Data Bits: {0}\r\n", serialConfig.DataBits);
                                message += String.Format("Serial Port A Parity: {0}\r\n", serialConfig.Parity);
                                message += String.Format("Serial Port A HandShaking: {0}\r\n", serialConfig.Handshaking);
                            }

                            title = "Serial Port";
                        }

                        //Auto Update
                        else if (selectedItemIndex == 8)
                        {
                            //AutoUpdate
                            AutoUpdate_DPL autoUpdate = new AutoUpdate_DPL(conn);
                            autoUpdate.QueryPrinter(600);

                            if (autoUpdate.Valid == false)
                            {
                                message += "No response from printer\r\n";
                            }
                            else
                            {
                                message += String.Format("Wireless Upgrade Type: {0}\r\n", autoUpdate.WirelessUpgradeType);
                                message += String.Format("Status Message Print mode: {0}\r\n", autoUpdate.StatusMessagePrintMode);
                                message += String.Format("Security Credential File Format: {0}\r\n", autoUpdate.SecurityCredentialFileFormat);
                                message += String.Format("Config File Name: {0}\r\n", autoUpdate.ConfigurationFileName);
                                message += String.Format("TFTP Server IP: {0}\r\n", autoUpdate.TFTPServerIPAddress);
                                message += String.Format("Upgrade Package Version: {0}\r\n", autoUpdate.UpgradePackageVersion);
                                message += String.Format("Beeper Enable: {0}\r\n", autoUpdate.Beeper ? "Yes" : "No");
                                message += String.Format(" FTP Username: {0}\r\n", autoUpdate.FTPUsername);
                                message += String.Format("FTP Server Name: {0}\r\n", autoUpdate.FTPServerName);
                                message += String.Format("FTP Server Port: {0}\r\n", autoUpdate.FTPServerPort);
                            }
                            title = "Auto Update";
                        }

                        //Avalanche
                        else if (selectedItemIndex == 9)
                        {
                            //Avalanche
                            AvalancheEnabler_DPL avaEnabler = new AvalancheEnabler_DPL(conn);
                            avaEnabler.QueryPrinter(600);

                            if (avaEnabler.Valid == false)
                            {
                                message += "No response from printer\r\n";
                            }
                            else
                            {
                                message += String.Format("Agent IP Address: {0}\r\n", avaEnabler.AgentIPAddress);
                                message += String.Format("Agent Port: {0}\r\n", avaEnabler.AgentPort);
                                message += String.Format("Agent DNS Name: {0}\r\n", avaEnabler.AgentDNSName);
                                message += String.Format("Connectivity Type: {0}\r\n", avaEnabler.ConnectivityType);
                                message += String.Format("Printer Name: {0}\r\n", avaEnabler.PrinterName);
                                message += String.Format("Printer Model: {0}\r\n", avaEnabler.PrinterModel);
                                message += String.Format("Update Package Version: {0}\r\n", avaEnabler.UpdatePackageVersion);
                                message += String.Format("Update Mode: {0}\r\n", avaEnabler.UpdateMode);
                                message += String.Format("Update Interval: {0}\r\n", avaEnabler.UpdateInterval);
                                message += String.Format("Update Package Name: {0}\r\n", avaEnabler.UpdatePackageName);
                                message += String.Format("Print Status Result Enable: {0}\r\n", avaEnabler.PrintStatusResult ? "Yes" : "No");
                                message += String.Format("Avalanche Enabler Active: {0}\r\n", avaEnabler.AvalancheEnablerActive ? "Yes" : "No");
                                message += String.Format("Remove old Updates: {0}\r\n", avaEnabler.RemoveOldUpdatesBeforeUpdate ? "Yes" : "No");
                            }
                            title = "Avalanche Enable Settings";
                        }
                        //Bluetooth Config
                        else if (selectedItemIndex == 10)
                        {
                            BluetoothConfiguration_DPL btConfig = new BluetoothConfiguration_DPL(conn);
                            btConfig.QueryPrinter(600);

                            if (btConfig.Valid == false)
                            {
                                message += "No response from printer\r\n";
                            }
                            else
                            {
                                message += String.Format("Bluetooth Device Name: {0}\r\n", btConfig.BluetoothDeviceName);
                                message += String.Format("Bluetooth Service Name: {0}\r\n", btConfig.BluetoothServiceName);
                                message += String.Format("Authentication Type:{0}\r\n", btConfig.AuthenticationType);
                                message += String.Format("Discoverable: {0}\r\n", btConfig.Discoverable ? "Yes" : "No");
                                message += String.Format("Connectable: {0}\r\n", btConfig.Connectable ? "Yes" : "No");
                                message += String.Format("Bondable: {0}\r\n", btConfig.Bondable ? "Yes" : "No");
                                message += String.Format("Encryption: {0}\r\n", btConfig.Encryption ? "Yes" : "No");
                                message += String.Format("Inactive Disconnect Time: {0}\r\n", btConfig.InactiveDisconnectTime);
                                message += String.Format("Power Down Time: {0}\r\n", btConfig.PowerDownTime);
                                message += String.Format("Bluetooth Device Address: {0}\r\n", btConfig.BluetoothDeviceAddress);
                            }
                            title = "Bluetooth Configuration";
                        }
                        //Network General
                        else if (selectedItemIndex == 11)
                        {

                            NetworkGeneralSettings_DPL netGen = new NetworkGeneralSettings_DPL(conn);
                            netGen.QueryPrinter(600);

                            if (netGen.Valid == false)
                            {
                                message += "No response from printer\r\n";
                            }
                            else
                            {
                                message += String.Format("Primary Interface: {0}\r\n", netGen.PrimaryInterface);
                                message += String.Format("WiFi Module Type: {0}\r\n", netGen.WiFiType);
                                message += String.Format("Network Password:{0}\r\n", netGen.NetworkPassword);
                                message += String.Format("SNMP Enable: {0}\r\n", netGen.SNMPEnable ? "Yes" : "No");
                                message += String.Format("Telnet Enable: {0}\r\n", netGen.TelnetEnable ? "Yes" : "No");
                                message += String.Format("FTP Enable: {0}\r\n", netGen.FTPEnable ? "Yes" : "No");
                                message += String.Format("HTTP Enable: {0}\r\n", netGen.HTTPEnable ? "Yes" : "No");
                                message += String.Format("LPD Enable: {0}\r\n", netGen.LPDEnable ? "Yes" : "No");
                                message += String.Format("NetBIOS Enable: {0}\r\n", netGen.NetBIOSEnable ? "Yes" : "No");
                                message += String.Format("Netcenter Enable: {0}\r\n", netGen.NetcenterEnable ? "Yes" : "No");
                                message += String.Format("Gratuitous ARP Period: {0}\r\n", netGen.GratuitousARPPeriod);
                            }
                            title = "Network General Settings";
                        }
                        //Wifi
                        else if (selectedItemIndex == 12)
                        {

                            NetworkWirelessSettings_DPL wifiSettings = new NetworkWirelessSettings_DPL(conn);
                            wifiSettings.QueryPrinter(600);

                            if (wifiSettings.Valid == false)
                            {
                                message += "No response from printer\r\n";
                            }
                            else
                            {
                                //DNS Settings
                                message += String.Format("Static DNS Enable: {0}\r\n", wifiSettings.StaticDNS ? "Yes" : "No");
                                message += String.Format("Preferred DNS Server: {0}\r\n", wifiSettings.PreferredDNSServerIP);
                                message += String.Format("Secondary DNS Server: {0}\r\n", wifiSettings.SecondaryDNSServerIP);
                                message += String.Format("DNS Suffix: {0}\r\n", wifiSettings.DNSSuffix);

                                //Network Settings
                                message += String.Format("Inactive Timeout: {0}\r\n", wifiSettings.InactiveTimeout);
                                message += String.Format("IP Address Method: {0}\r\n", wifiSettings.IPAddressMethod);
                                message += String.Format("Active IP Address: {0}\r\n", wifiSettings.ActiveIPAddress);
                                message += String.Format("Active Subnet Mask: {0}\r\n", wifiSettings.ActiveSubnetMask);
                                message += String.Format("Printer DNS name: {0}\r\n", wifiSettings.PrinterDNSName);
                                message += String.Format("Register to DNS: {0}\r\n", wifiSettings.RegisterToDNS ? "Yes" : "No");
                                message += String.Format("Active Gateway: {0}\r\n", wifiSettings.ActiveGatewayAddress);
                                message += String.Format("UDP Port: {0}\r\n", wifiSettings.UDPPort);
                                message += String.Format("TCP Port: {0}\r\n", wifiSettings.TCPPort);
                                message += String.Format("Use DNS Suffix: {0}\r\n", wifiSettings.UseDNSSuffix ? "Yes" : "No");
                                message += String.Format("Enable Connection Status: {0}\r\n", wifiSettings.EnableConnectionStatusReport ? "Yes" : "No");
                                message += String.Format("DHCP User Class Option: {0}\r\n", Encoding.ASCII.GetString(wifiSettings.DHCPUserClassOption, 0, wifiSettings.DHCPUserClassOption.Length));
                                message += String.Format("Static IP Address: {0}\r\n", wifiSettings.StaticIPAddress);
                                message += String.Format("Static Subnet Mask: {0}\r\n", wifiSettings.StaticSubnetMask);
                                message += String.Format("Static Gateway: {0}\r\n", wifiSettings.StaticGateway);
                                message += String.Format("LPD Port: {0}\r\n", wifiSettings.LPDPort);
                                message += String.Format("LPD Enable: {0}\r\n", wifiSettings.LPDEnable ? "Yes" : "No");


                                //Wifi Settings
                                message += String.Format("Network Type: {0}\r\n", wifiSettings.NetworkType);
                                message += String.Format("ESSID: {0}\r\n", wifiSettings.ESSID);
                                message += String.Format("EAP Type: {0}\r\n", wifiSettings.EAPType);
                                message += String.Format("Network Authentication Type: {0}\r\n", wifiSettings.NetworkAuthenticationType);
                                message += String.Format("WEP Authentication Type: {0}\r\n", wifiSettings.WEPAuthenticationMethod);
                                message += String.Format("Phase 2 Method: {0}\r\n", wifiSettings.Phase2Method);
                                message += String.Format("WEP Data Encryption Enable: {0}\r\n", wifiSettings.WEPDataEncryption ? "Yes" : "No");
                                message += String.Format("Show Signal Strength: {0}\r\n", wifiSettings.ShowSignalStrength ? "Yes" : "No");
                                message += String.Format("Power Saving Mode: {0}\r\n", wifiSettings.PowerSavingMode ? "Yes" : "No");
                                message += String.Format("Group Cipher: {0}\r\n", wifiSettings.GroupCipher);
                                message += String.Format("MAC Address: {0}\r\n", wifiSettings.WiFiMACAddress);
                                message += String.Format("Regulatory Domain: {0}\r\n", wifiSettings.RegulatoryDomain);
                                message += String.Format("Radio Mode: {0}\r\n", wifiSettings.RadioMode);
                                message += String.Format("Wifi Testing Mode Enable: {0}\r\n", wifiSettings.WiFiTestingMode? "Yes" : "No");
                                message += String.Format("Max Active Channel Dwell Time: {0}\r\n", wifiSettings.MaxActiveChannelDwellTime);
                                message += String.Format("Min Active Channel Dwell Time: {0}\r\n", wifiSettings.MinActiveChannelDwellTime);
                                message += String.Format("Active Scanning Radio Channel: {0}\r\n", wifiSettings.RadioChannelSelection);
                                message += String.Format("Use Hex PSK: {0}\r\n", wifiSettings.UseHexPSK ? "Yes" : "No");
                            }

                            title = "Network Wireless Settings";
                        }
                        else if (selectedItemIndex == 13)
                        {
                            PrinterStatus_DPL printerStatus = new PrinterStatus_DPL(conn);
                            printerStatus.QueryPrinter(800);

                            if (printerStatus.Valid == false)
                            {
                                message += "No response from printer\r\n";
                            }
                            else
                            {
                                PrinterStatus_DPL.PrinterStatus currentStatus = printerStatus.CurrentStatus;
                                switch (currentStatus)
                                {
                                    case PrinterStatus_DPL.PrinterStatus.PrinterReady:
                                        message += "Printer is ready.";
                                        break;
                                    case PrinterStatus_DPL.PrinterStatus.BusyPrinting:
                                        message += "Printer is busy.";
                                        break;
                                    case PrinterStatus_DPL.PrinterStatus.PaperOutFault:
                                        message += "Printer is out of paper.";
                                        break;
                                    case PrinterStatus_DPL.PrinterStatus.PrintHeadUp:
                                        message += "Printer print head lid is open.";
                                        break;
                                    default:
                                        message += "Printer status unknown";
                                        break;
                                }
                            }
                        }
                    }
                    //EZ and LP mode
                    else
                    {
                        //Avalanche Settings
                        if (selectedItemIndex == 0)
                        {

                            AvalancheSettings avaSettings = new AvalancheSettings(conn);
                            avaSettings.QueryPrinter(600);

                            if (avaSettings.Valid == false)
                            {
                                message += "No response from printer\r\n";
                            }
                            else
                            {
                                message += String.Format("Agent IP: {0}\r\n", avaSettings.AgentIP);
                                message += String.Format("Show All Data on Self Test: {0}\r\n", avaSettings.ShowAllData ? "Yes" : "No");
                                message += String.Format("Agent Name: {0}\r\n", avaSettings.AgentName);
                                message += String.Format("Agent Port: {0}\r\n", avaSettings.AgentPort);
                                message += String.Format("Connection Type: {0}\r\n", avaSettings.ConnectionType);
                                message += String.Format("Avalanche Enable: {0}\r\n", avaSettings.IsAvalancheEnabled ? "Yes" : "No");
                                message += String.Format("Printer Name: {0}\r\n", avaSettings.PrinterName);
                                message += String.Format("Printer Model: {0}\r\n", avaSettings.PrinterModelName);
                                message += String.Format("Is Prelicensed: {0}\r\n", avaSettings.IsPrelicensed ? "Yes" : "No");
                                message += String.Format("Printer Result Flag: {0}\r\n", avaSettings.PrinterResultFlag ? "Yes" : "No");
                                message += String.Format("Update Interval: {0}\r\n", avaSettings.UpdateInterval);
                                message += String.Format("Update Flags: {0}\r\n", avaSettings.UpdateFlags);
                                message += String.Format("Is Wired: {0}\r\n", avaSettings.IsWired ? "Yes" : "No");
                            }
                            title = "Avalanche Settings";

                        }

                        //Battery Condition
                        else if (selectedItemIndex == 1)
                        {

                            BatteryCondition battCond = new BatteryCondition(conn);
                            battCond.QueryPrinter(600);

                            if (battCond.Valid == false)
                            {
                                message += "No response from printer\r\n";
                            }
                            else
                            {
                                message += String.Format("Power Source Plugged in: {0}\r\n", battCond.ChargerConnected ? "Yes" : "No");
                                message += String.Format("Power Source: {0}\r\n", battCond.PowerSource);
                                message += String.Format("Battery Temperature: {0}\r\n", battCond.BatteryTemperature);
                                message += String.Format("Voltage Battery: {0}\r\n", battCond.VoltageBatterySingle);
                                message += String.Format("Voltage Battery 1: {0}\r\n", battCond.VoltageBattery1);
                                message += String.Format("Votlage Battery 2: {0}\r\n", battCond.VoltageBattery2);
                                message += String.Format("Voltage of Battery Eliminator: {0}\r\n", battCond.VoltageBatteryEliminator);
                            }
                            title = "Battery Condition";

                        }
                        //Bluetooth Config
                        else if (selectedItemIndex == 2)
                        {
                            BluetoothConfiguration btConfig = new BluetoothConfiguration(conn);
                            btConfig.QueryPrinter(600);

                            if (btConfig.Valid == false)
                            {
                                message += "No response from printer\r\n";
                            }
                            else
                            {
                                message += String.Format("Authentication Enable: {0}\r\n", btConfig.Authentication ? "Yes" : "No");
                                message += String.Format("MAC Address: {0}\r\n", btConfig.BluetoothAddress);
                                message += String.Format("Bondable: {0}\r\n", btConfig.Bondable ? "Yes" : "No");
                                message += String.Format("Connectable: {0}\r\n", btConfig.Connectable ? "Yes" : "No");
                                message += String.Format("Discoverable: {0}\r\n", btConfig.Discoverable ? "Yes" : "No");
                                message += String.Format("Friendly Name: {0}\r\n", btConfig.FriendlyName);
                                message += String.Format("Inactivity timeout: {0}\r\n", btConfig.InactivityTimeout);
                                message += String.Format("Passkey enable: {0}\r\n", btConfig.Passkey ? "Yes" : "No");
                                message += String.Format("Bluetooth Profile: {0}\r\n", btConfig.Profile);
                                message += String.Format("Service Name: {0}\r\n", btConfig.ServiceName);
                                message += String.Format("Watchdog Period: {0}\r\n", btConfig.WatchdogPeriod);
                            }
                            title = "Bluetooth Config";
                        }

                        //Font List
                        else if (selectedItemIndex == 3)
                        {
                            FontList fontList = new FontList(conn);
                            fontList.QueryPrinter(600);

                            if (fontList.Valid == false)
                            {
                                message += "No response from printer\r\n";
                            }
                            else
                            {
                                List<FontData> files = fontList.Fonts;
                                foreach (FontData font in files)
                                {
                                    message += String.Format("Five Character Name: {0}\r\n", font.FiveCharacterName);
                                    message += String.Format("One Character Name: {0}\r\n", font.OneCharacterName);
                                    message += String.Format("Memory Location: {0}\r\n", font.MemoryLocation);
                                    message += String.Format("User Date: {0}\r\n", font.UserDate);
                                    message += String.Format("Description: {0}\r\n", font.UserDescription);
                                    message += String.Format("Version: {0}\r\n", font.UserVersion);
                                    message += "\r\n";
                                }
                            }
                            title = "Font List";
                        }
                        //Format list
                        else if (selectedItemIndex == 4)
                        {
                            FormatList formatList = new FormatList(conn);
                            formatList.QueryPrinter(600);

                            if (formatList.Valid == false)
                            {
                                message += "No response from printer\r\n";
                            }
                            else
                            {
                                List<FormatData> files = formatList.Formats;
                                foreach (FormatData formatData in files)
                                {
                                    message += String.Format("Five Character Name: {0}\r\n", formatData.FiveCharacterName);
                                    message += String.Format("One Character Name: {0}\r\n", formatData.OneCharacterName);
                                    message += String.Format("Memory Location: {0}\r\n", formatData.MemoryLocation);
                                    message += String.Format("User Date: {0}\r\n", formatData.UserDate);
                                    message += String.Format("Description: {0}\r\n", formatData.UserDescription);
                                    message += String.Format("Version: {0}\r\n", formatData.UserVersion);
                                    message += "\r\n";
                                }
                            }
                            title = "Format List";

                        }
                        //General Config
                        else if (selectedItemIndex == 5)
                        {
                            GeneralConfiguration genConfig = new GeneralConfiguration(conn);
                            genConfig.QueryPrinter(600);

                            if (genConfig.Valid == false)
                            {
                                message += "No response from printer\r\n";
                            }
                            else
                            {
                                message += String.Format("White Space Advance Enable: {0}\r\n", genConfig.WhiteSpaceAdvance ? "Yes" : "No");
                                message += String.Format("Darkness Adjustment: {0}\r\n", genConfig.DarknessAdjustment);
                                message += String.Format("Form Feed Enable: {0}\r\n", genConfig.FormFeed ? "Yes" : "No");
                                message += String.Format("Charger Beep Enable: {0}\r\n", genConfig.ChargerBeep ? "Yes" : "No");
                                message += String.Format("Sound Enable(Beeper On): {0}\r\n", genConfig.SoundEnabled);
                                message += String.Format("Lines Per Page: {0}\r\n", genConfig.LinesPerPage);
                                message += String.Format("Print Job Status Report Enable: {0}\r\n", genConfig.EZPrintJobStatusReport ? "Yes" : "No");
                                message += String.Format("Default Protocol: {0}\r\n", genConfig.DefaultProtocol);
                                message += String.Format("Self Test Print Language: {0}\r\n", genConfig.SelfTestPrintLanguage);
                                message += String.Format("Form Feed Centering: {0}\r\n", genConfig.FormFeedCentering ? "Yes" : "No");
                                message += String.Format("Form Feed Button Disabled: {0}\r\n", genConfig.FormfeedButtonDisabled ? "Yes" : "No");
                                message += String.Format("Power Button Disabled: {0}\r\n", genConfig.PowerButtonDisabled ? "Yes" : "No");
                                message += String.Format("RF Button Disabled: {0}\r\n", genConfig.PowerButtonDisabled ? "Yes" : "No");
                                message += String.Format("QStop Multiplier: {0}\r\n", genConfig.QStopMultiplier);
                                message += String.Format("RF Timeout: {0}\r\n", genConfig.RFPowerTimeout);
                                message += String.Format("System Timeout: {0}\r\n", genConfig.SystemTimeout);
                                message += String.Format("Special Test Print: {0}\r\n", genConfig.SpecialTestPrint);
                                message += String.Format("Paper Out Beep: {0}\r\n", genConfig.PaperOutBeep);
                                message += String.Format("USB Class: {0}\r\n", genConfig.USBClass);
                                message += String.Format("Using USB: {0}\r\n", genConfig.UsingUSB ? "Yes" : "No");
                                message += String.Format("Deep Sleep Enable: {0}\r\n", genConfig.DeepSleep ? "Yes" : "No");
                            }
                            title = "General Configuration";

                        }
                        //General status
                        else if (selectedItemIndex == 6)
                        {
                            GeneralStatus genStatus = new GeneralStatus(conn);
                            genStatus.QueryPrinter(600);

                            if (genStatus.Valid == false)
                            {
                                message += "No response from printer\r\n";
                            }
                            else
                            {
                                message += String.Format("Battery Temp and Voltage Status: {0}\r\n", genStatus.BatteryTempandVoltageStatus);
                                message += String.Format("Error Status: {0}\r\n", genStatus.ErrorStatus);
                                message += String.Format("Paper Jam: {0}\r\n", genStatus.PaperJam);
                                message += String.Format("Printer Status: {0}\r\n", genStatus.PrinterStatus);
                                message += String.Format("Remaining RAM: {0}\r\n", genStatus.RemainingRAM);
                                message += String.Format("Paper Present: {0}\r\n", genStatus.PaperPresent);
                                message += String.Format("Head Lever Position: {0}\r\n", genStatus.HeadLeverPosition);
                            }
                            title = "General Status";
                        }
                        //Graphic List
                        else if (selectedItemIndex == 7)
                        {
                            GraphicList graphList = new GraphicList(conn);
                            graphList.QueryPrinter(600);

                            if (graphList.Valid == false)
                            {
                                message += "No response from printer\r\n";
                            }
                            else
                            {
                                List<GraphicData> files = graphList.Graphics;
                                foreach (GraphicData graphic in files)
                                {
                                    message += String.Format("Five Character Name: {0}\r\n", graphic.FiveCharacterName);
                                    message += String.Format("One Character Name: {0}\r\n", graphic.OneCharacterName);
                                    message += String.Format("Memory Location: {0}\r\n", graphic.MemoryLocation);
                                    message += String.Format("User Date: {0}\r\n", graphic.UserDate);
                                    message += String.Format("Description: {0}\r\n", graphic.UserDescription);
                                    message += String.Format("Version: {0}\r\n", graphic.UserVersion);
                                    message += "\r\n";
                                }
                            }

                            title = "Graphic List";
                        }
                        //IrDA Config
                        else if (selectedItemIndex == 8)
                        {
                            IrDAConfiguration irDAConfig = new IrDAConfiguration(conn);
                            irDAConfig.QueryPrinter(600);

                            if (irDAConfig.Valid == false)
                            {
                                message += "No response from printer\r\n";
                            }
                            else
                            {
                                message += String.Format("Direct Version: {0}\r\n", irDAConfig.DirectVersion);
                                message += String.Format("IrDA Name: {0}\r\n", irDAConfig.IrDAName);
                                message += String.Format("IrDA Nickname: {0}\r\n", irDAConfig.IrDANickname);
                                message += String.Format("IrDA Version: {0}\r\n", irDAConfig.IrDAVersion);
                                message += String.Format("Protocol: {0}\r\n", irDAConfig.Protocol);
                            }
                            title = "IrDA Config";
                        }
                        //Label Config
                        else if (selectedItemIndex == 9)
                        {
                            LabelConfiguration labelConfig = new LabelConfiguration(conn);
                            labelConfig.QueryPrinter(600);

                            if (labelConfig.Valid == false)
                            {
                                message += "No response from printer\r\n";
                            }
                            else
                            {
                                message += String.Format("Backup Distance: {0}\r\n", labelConfig.BackUpDistance);
                                message += String.Format("Use Presenter: {0}\r\n", labelConfig.UsePresenter ? "Yes" : "No");
                                message += String.Format("Auto QMark Advance: {0}\r\n", labelConfig.AutoQMarkAdvance ? "Yes" : "No");
                                message += String.Format("Backup Offset: {0}\r\n", labelConfig.BackupOffset);
                                message += String.Format("Horizontal Offset: {0}\r\n", labelConfig.HorizontalOffset);
                                message += String.Format("QMark Stop Length: {0}\r\n", labelConfig.QMarkStopLength);
                                message += String.Format("Additional Self Test Prints: {0}\r\n", labelConfig.AdditionalSelfTestPrints);
                                message += String.Format("Max QMark Advance: {0}\r\n", labelConfig.MaximumQMarkAdvance);
                                message += String.Format("QMARKB offset: {0}\r\n", labelConfig.QMARKBOffset);
                                message += String.Format("QMARKG Offset: {0}\r\n", labelConfig.QMARKGOffset);
                                message += String.Format("QMARKT Offset: {0}\r\n", labelConfig.QMARKTOffset);
                                message += String.Format("White QMark Enable: {0}\r\n", labelConfig.WhiteQMark ? "Yes" : "No");
                                message += String.Format("Paperout Sensor: {0}\r\n", labelConfig.PaperoutSensor);
                                message += String.Format("Paper Stock Type: {0}\r\n", labelConfig.PaperStockType);
                                message += String.Format("Presenter Timeout: {0}\r\n", labelConfig.PresenterTimeout);
                                message += String.Format("Auto QMark Backup: {0}\r\n", labelConfig.AutoQMarkBackup ? "Yes" : "No");
                            }
                            title = "Label Config";

                        }
                        //Magnetic Card
                        else if (selectedItemIndex == 10)
                        {
                            MagneticCardConfiguration magConfig = new MagneticCardConfiguration(conn);
                            magConfig.QueryPrinter(600);

                            if (magConfig.Valid == false)
                            {
                                message += "No response from printer\r\n";
                            }
                            else
                            {
                                message += String.Format("Auto Print: {0}\r\n", magConfig.AutoPrint ? "Yes" : "No");
                                message += String.Format("Card Read Direction: {0}\r\n", magConfig.CardReadDirection);
                                message += String.Format("Magnetic Card Enabled: {0}\r\n", magConfig.Enabled ? "Yes" : "No");
                                message += String.Format("Auto Send: {0}\r\n", magConfig.AutoSend ? "On" : "Off");
                                message += String.Format("Track 1 Enabled: {0}\r\n", magConfig.Track1Enabled ? "Yes" : "No");
                                message += String.Format("Track 2 Enabled: {0}\r\n", magConfig.Track2Enabled ? "Yes" : "No");
                                message += String.Format("Track 3 Enabled: {0}\r\n", magConfig.Track3Enabled ? "Yes" : "No");
                            }
                            title = "Magnetic Card Config";
                        }
                        //Magnetic Card Data
                        else if (selectedItemIndex == 11)
                        {
                            MagneticCardData magCardData = new MagneticCardData(conn);
                            magCardData.QueryPrinter(600);

                            if (magCardData.Valid == false)
                            {
                                message += "No response from printer\r\n";
                            }
                            else
                            {
                                message += String.Format("Track 1 Data: {0}\r\n", magCardData.Track1Data);
                                message += String.Format("Track 2 Data: {0}\r\n", magCardData.Track2Data);
                                message += String.Format("Track 3 Data: {0}\r\n", magCardData.Track3Data);
                            }
                            title = "Magnetic Card Data";

                        }
                        //Manufacturing Date
                        else if (selectedItemIndex == 12)
                        {
                            ManufacturingDate manuDate = new ManufacturingDate(conn);
                            manuDate.QueryPrinter(600);

                            if (manuDate.Valid == false)
                            {
                                message += "No response from printer\r\n";
                            }
                            else
                            {
                                message += String.Format("Manufacturing Date: {0}\r\n", manuDate.MD);
                            }
                            title = "Manufacturing Date";
                        }
                        //Memory status
                        else if (selectedItemIndex == 13)
                        {
                            MemoryStatus memStatus = new MemoryStatus(conn);
                            memStatus.QueryPrinter(600);

                            if (memStatus.Valid == false)
                            {
                                message += "No response from printer\r\n";
                            }
                            else
                            {
                                message += String.Format("Download memory remaining: {0}\r\n", memStatus.DownloadMemoryRemaining);
                                message += String.Format("Download memory total: {0}\r\n", memStatus.DownloadMemoryTotal);
                                message += String.Format("EEPROM Size: {0}\r\n", memStatus.EEPROMSize);
                                message += String.Format("Flash Memory Size: {0}\r\n", memStatus.FlashMemorySize);
                                message += String.Format("RAM size: {0}\r\n", memStatus.RAMSize);
                                message += String.Format("Flash type: {0}\r\n", memStatus.FlashType);
                                message += String.Format("Download Format Memory Remaining: {0}\r\n", memStatus.DownloadFormatMemoryRemaining);
                                message += String.Format("Download Format Memory Total: {0}\r\n", memStatus.DownloadFormatMemoryTotal);
                            }
                            title = "Memory status";

                        }
                        //Printer Options
                        else if (selectedItemIndex == 14)
                        {
                            PrinterOptions printerOpt = new PrinterOptions(conn);
                            printerOpt.QueryPrinter(600);

                            if (printerOpt.Valid == false)
                            {
                                message += "No response from printer\r\n";
                            }
                            else
                            {
                                message += String.Format("SCR Device: {0}\r\n", printerOpt.SCRDevice);
                                message += String.Format("CF Device: {0}\r\n", printerOpt.CFDevice);
                                message += String.Format("Printer Description: {0}\r\n", printerOpt.PrinterDescription);
                                message += String.Format("Part Number: {0}\r\n", printerOpt.PartNumber);
                                message += String.Format("Serial Number: {0}\r\n", printerOpt.SerialNumber);
                                message += String.Format("Printer Type: {0}\r\n", printerOpt.PrinterType);
                                message += String.Format("SPI Device: {0}\r\n", printerOpt.SPIDevice);
                                message += String.Format("Manufacturing Date: {0}\r\n", printerOpt.ManufacturingDate);
                                message += String.Format("Text Fixture String: {0}\r\n", printerOpt.TextFixtureString);
                                message += String.Format("SDIO Device: {0}\r\n", printerOpt.SDIODevice);
                                message += String.Format("Certification Flag Status: {0}\r\n", printerOpt.CertificationFlagStatus ? "On" : "Off");
                            }
                            title = "Printer Options";
                        }
                        //PrintHead Status
                        else if (selectedItemIndex == 15)
                        {
                            PrintheadStatus printHeadStats = new PrintheadStatus(conn);
                            printHeadStats.QueryPrinter(600);

                            if (printHeadStats.Valid == false)
                            {
                                message += "No response from printer\r\n";
                            }
                            else
                            {
                                message += String.Format("DPI: {0}\r\n", printHeadStats.DPI);
                                message += String.Format("PrintHead Model: {0}\r\n", printHeadStats.PrintheadModel);
                                message += String.Format("Print Time: {0}\r\n", printHeadStats.PrintTime);
                                message += String.Format("PrintHead Pins: {0}\r\n", printHeadStats.PrintheadPins);
                                message += String.Format("PrintHead Temperature: {0}\r\n", printHeadStats.PrintheadTemperature);
                                message += String.Format("PrintHead Width: {0}\r\n", printHeadStats.PrintheadWidth);
                                message += String.Format("Page Width: {0}\r\n", printHeadStats.PageWidth);
                            }
                            title = "Print Head Status";

                        }
                        //Serial Number
                        else if (selectedItemIndex == 16)
                        {
                            SerialNumber serialNum = new SerialNumber(conn);
                            serialNum.QueryPrinter(600);

                            if (serialNum.Valid == false)
                            {
                                message += "No response from printer\r\n";
                            }
                            else
                            {
                                message += String.Format("Serial Number: {0}\r\n", serialNum.SN);
                            }
                            title = "Serial Number";
                        }
                        //Smart Card Config
                        else if (selectedItemIndex == 17)
                        {
                            SmartCardConfiguration smartCardConfig = new SmartCardConfiguration(conn);
                            smartCardConfig.QueryPrinter(600);

                            if (smartCardConfig.Valid == false)
                            {
                                message += "No response from printer\r\n";
                            }
                            else
                            {
                                message += String.Format("Command Format: {0}\r\n", smartCardConfig.CommandFormat);
                                message += String.Format("Enable: {0}\r\n", smartCardConfig.Enabled ? "Yes" : "No");
                                message += String.Format("Memory Tye: {0}\r\n", smartCardConfig.MemoryType);
                                message += String.Format("Response Format: {0}\r\n", smartCardConfig.ResponseFormat);
                                message += String.Format("Smart Card Protocol: {0}\r\n", smartCardConfig.Protocol);
                                message += String.Format("Smart Card Type: {0}\r\n", smartCardConfig.Type);
                            }
                            title = "Smart Card Config";
                        }
                        //Serial Config
                        else if (selectedItemIndex == 18)
                        {
                            GeneralConfiguration genConfig = new GeneralConfiguration(conn);
                            genConfig.QueryPrinter(600);

                            if (genConfig.Valid == false)
                            {
                                message += "No response from printer\r\n";
                            }
                            else
                            {
                                message += String.Format("Baud Rate: {0}\r\n", genConfig.BaudRate);
                                message += String.Format("Handshake: {0}\r\n", genConfig.RS232Handshake);
                                message += String.Format("Data Bits: {0}\r\n", genConfig.RS232DataBits);
                                message += String.Format("Parity: {0}\r\n", genConfig.RS232Parity);
                            }
                            title = "Serial Port Config";
                        }
                        //TCPIPStatus
                        else if (selectedItemIndex == 19)
                        {
                            TCPIPStatus tcpStatus = new TCPIPStatus(conn);
                            tcpStatus.QueryPrinter(600);

                            if (tcpStatus.Valid == false)
                            {
                                message += "No response from printer\r\n";
                            }
                            else
                            {
                                message += String.Format("Wireless Card Info: {0}\r\n", tcpStatus.WirelessCardInfo);
                                message += String.Format("Valid Cert. Present: {0}\r\n", tcpStatus.ValidCertificatePresent);
                                message += String.Format("Conn. Reporting Enable: {0}\r\n", tcpStatus.ConnectionReporting);
                                message += String.Format("Acquired IP: {0}\r\n", tcpStatus.AcquireIP);
                                message += String.Format("Radio Disable: {0}\r\n", tcpStatus.RadioDisabled ? "Yes" : "No");
                                message += String.Format("ESSID: {0}\r\n", tcpStatus.ESSID);
                                message += String.Format("EAP Type: {0}\r\n", tcpStatus.EAPType);
                                message += String.Format("Gateway Address: {0}\r\n", tcpStatus.GatewayAddress);
                                message += String.Format("IP Address: {0}\r\n", tcpStatus.IPAddress);
                                message += String.Format("Inactivity Timeout: {0}\r\n", tcpStatus.InactivityTimeout);
                                message += String.Format("Key to Use: {0}\r\n", tcpStatus.KeyToUse);
                                message += String.Format("Key 1 Type: {0}\r\n", tcpStatus.Key1Type);
                                message += String.Format("Key 2 Type: {0}\r\n", tcpStatus.Key2Type);
                                message += String.Format("Key 3 Type: {0}\r\n", tcpStatus.Key3Type);
                                message += String.Format("Key 4 Type: {0}\r\n", tcpStatus.Key4Type);
                                message += String.Format("Subnet Mask: {0}\r\n", tcpStatus.SubnetMask);
                                message += String.Format("MAC Address: {0}\r\n", tcpStatus.MACAddress);
                                message += String.Format("Station Name: {0}\r\n", tcpStatus.StationName);
                                message += String.Format("Network Authentication: {0}\r\n", tcpStatus.NetworkAuthentication);
                                message += String.Format("TCP Printing Port: {0}\r\n", tcpStatus.TCPPrintingPort);
                                message += String.Format("Power Saving Mode: {0}\r\n", tcpStatus.PowerSavingMode ? "Yes" : "No");
                                message += String.Format("Phase 2 Method: {0}\r\n", tcpStatus.Phase2Method);
                                message += String.Format("UDP Printing Port: {0}\r\n", tcpStatus.UDPPrintingPort);
                                message += String.Format("Card Powered: {0}\r\n", tcpStatus.CardPowered ? "On" : "Off");
                                message += String.Format("Signal Quality Indicator: {0}\r\n", tcpStatus.SignalQualityIndicator ? "Yes" : "No");
                                message += String.Format("Authentication Algorithm: {0}\r\n", tcpStatus.AuthenticationAlgorithm);
                                message += String.Format("Station Type: {0}\r\n", tcpStatus.NetworkType);
                                message += String.Format("Encryption Enabled: {0}\r\n", tcpStatus.EncryptionEnabled);
                                message += String.Format("Current Certificate CRC: {0}\r\n", tcpStatus.CurrentCertificateCRC);
                                message += String.Format("DNS1 Address: {0}\r\n", tcpStatus.DNS1Address);
                                message += String.Format("Register to DNS: {0}\r\n", tcpStatus.RegisterToDNS ? "Yes" : "No");
                                message += String.Format("DNS2 Address: {0}\r\n", tcpStatus.DNS2Address);
                                message += String.Format("Static DNS Enable: {0}\r\n", tcpStatus.StaticDNS ? "Yes" : "No");
                                message += String.Format("Group Cipher: {0}\r\n", tcpStatus.GroupCipher);
                                message += String.Format("Radio Type: {0}\r\n", tcpStatus.RadioType);
                                message += String.Format("Use DNS: {0}\r\n", tcpStatus.UseDNS ? "Yes" : "No");
                                message += String.Format("DNS Suffix: {0}\r\n", tcpStatus.DNSSuffix);
                                message += String.Format("Encryption Key Size: {0}\r\n", tcpStatus.EncryptionKeySize);
                                message += String.Format("Encryption Key Type: {0}\r\n", tcpStatus.EncryptionKeyType);
                            }
                            title = "TCP/IP Status";

                        }
                        //Upgrade Data
                        else if (selectedItemIndex == 20)
                        {
                            UpgradeData upgradeData = new UpgradeData(conn);
                            upgradeData.QueryPrinter(600);

                            if (upgradeData.Valid == false)
                            {
                                message += "No response from printer\r\n";
                            }
                            else
                            {
                                message += String.Format("Path and File: {0}\r\n", upgradeData.PathAndFile);
                                message += String.Format("Server IP: {0}\r\n", upgradeData.ServerIPAddress);
                                message += String.Format("Server Port: {0}\r\n", upgradeData.ServerPort);
                                message += String.Format("Upgrade Type: {0}\r\n", upgradeData.DataType);
                                message += String.Format("Upgrade Package Version: {0}\r\n", upgradeData.Version);
                            }
                            title = "Auto Update Settings";

                        }
                        //Version Info
                        else if (selectedItemIndex == 21)
                        {
                            VersionInformation versionInfo = new VersionInformation(conn);
                            versionInfo.QueryPrinter(600);

                            if (versionInfo.Valid == false)
                            {
                                message += "No response from printer\r\n";
                            }
                            else
                            {
                                message += String.Format("Boot Version: {0}\r\n", versionInfo.BootVersion);
                                message += String.Format("Comm Controller Version: {0}\r\n", versionInfo.CommControllerVersion);
                                message += String.Format("Download version: {0}\r\n", versionInfo.DownloadVersion);
                                message += String.Format("Firmware version: {0}\r\n", versionInfo.FirmwareVersion);
                                message += String.Format("Hardware Controller Version: {0}\r\n", versionInfo.HardwareControllerVersion);
                                message += String.Format("SCR Version: {0}\r\n", versionInfo.SCRVersion);
                                message += String.Format("Build Timestamp: {0}\r\n", versionInfo.BuildTimestamp);
                            }

                            title = "Version Info";
                        }
                    }
                    updateStatusText(String.Format("[{0:MM/dd/yyy hh:mm:ss.fff}]", System.DateTime.Now) + " Closing connection..\r\n");
                    //signals to close connection
					//conn.IsClosing = true;
					conn.Close();

                    updateStatusText(String.Format("[{0:MM/dd/yyy hh:mm:ss.fff}]", System.DateTime.Now) + " Query success..\r\n");

                    ShowMessageBox(message, title);
                }
                enableButton(m_performButton, true);
            }
            catch (Exception ex)
            {
                enableButton(m_performButton, true);
                if(ex.InnerException == null)
                    updateStatusText(String.Format("[{0:MM/dd/yyy hh:mm:ss.fff}]", System.DateTime.Now) + " Error : " + (ex.Message) + "\r\n");
                else
                    updateStatusText(String.Format("[{0:MM/dd/yyy hh:mm:ss.fff}]", System.DateTime.Now) + " Error : " + (ex.InnerException.Message) + "\r\n");
            }
        }//run()

        private void updateStatusText(String text)
        {
            m_statusTextBox.Invoke(new EventHandler(delegate
            {
                m_statusTextBox.Text += text;
                m_statusTextBox.SelectionStart = m_statusTextBox.Text.Length;
                m_statusTextBox.ScrollToCaret();
            }));
        }
        
		private void enableButton(Button button, bool enable)
        {
            button.Invoke(new EventHandler(delegate
            {
                button.Enabled = enable;
            }));
        }
        
		private bool GetStatus() 
        {
            //FOR DPL printers
            if (selectedLanguageIndex == 2)
            {
                PrinterStatus_DPL printerStatus = new PrinterStatus_DPL(conn);
                //Query for printer status
                printerStatus.QueryPrinter(500);

                //Unable to retreive data from query
                if (printerStatus.Valid == false)
                    return false;

                PrinterStatus_DPL.PrinterStatus currentStatus = printerStatus.CurrentStatus;

                if (currentStatus != PrinterStatus_DPL.PrinterStatus.PrinterReady)
                {
                    return false;
                }
            }
            return true; 
        } 

        void reloadItemsArray()
        {
            //Clear list 
            itemsArray.Clear();
            //For Printing
            if (m_printRadio.Checked)
            {
                //EZprint
                if (m_printerLanguageComboBox.SelectedIndex == 0)
                {
                    itemsArray.Add("3-inch Sample Receipt");
                    itemsArray.Add("4-inch Sample Receipt");
                    itemsArray.Add("Barcode Sample");
                }
                //LP
                else if (m_printerLanguageComboBox.SelectedIndex == 1)
                {
                    itemsArray.Add("3-inch Sample Receipt");
                    itemsArray.Add("4-inch Sample Receipt");
                    itemsArray.Add("Image Sample");
                }
                //EXPCL_LP
                else if (m_printerLanguageComboBox.SelectedIndex == 3)
                {
                    itemsArray.Add("Text Sample");
                    itemsArray.Add("Barcode Sample");
                    itemsArray.Add("Graphics Sample");
                }

                //EXPCL_PP
                else if (m_printerLanguageComboBox.SelectedIndex == 4)
                {
                    itemsArray.Add("Text Samples");
                    itemsArray.Add("Barcode Samples");
                    itemsArray.Add("Rectangles");
                }

                //DPL mode
                else
                {
                    itemsArray.Clear();
                    itemsArray.Add("Text Sample");
                    itemsArray.Add("Incrementing Sample");
                    itemsArray.Add("Barcode Sample");
                    itemsArray.Add("Graphics sample");
                    itemsArray.Add("Image sample");
                }
                //Add Files browsed to list
                if (selectedFilesList.Count > 0)
                {
                    foreach (String file in selectedFilesList)
                    {
                        itemsArray.Add(file);
                    }
                }
                m_printItemsComboBox.DataSource = itemsArray.ToArray();
            }
            //Query
            else {
                //EZ or LP
                if (m_printerLanguageComboBox.SelectedIndex == 0 || m_printerLanguageComboBox.SelectedIndex == 1)
                {
                    itemsArray.Add("Avalanche Settings");
                    itemsArray.Add("Battery Condition");
                    itemsArray.Add("Bluetooth Config");
                    itemsArray.Add("Font List");
                    itemsArray.Add("Format List");
                    itemsArray.Add("General Config");
                    itemsArray.Add("General Status");
                    itemsArray.Add("Graphic List");
                    itemsArray.Add("IrDA Config");
                    itemsArray.Add("Label Config");
                    itemsArray.Add("Magnetic Config");
                    itemsArray.Add("Magnetic Card Data");
                    itemsArray.Add("Manufacturing Date");
                    itemsArray.Add("Memory Status");
                    itemsArray.Add("Printer Options");
                    itemsArray.Add("PrintHead Status");
                    itemsArray.Add("Serial Number");
                    itemsArray.Add("SmartCard Config");
                    itemsArray.Add("Serial Port Config");
                    itemsArray.Add("TCP/IP Status");
                    itemsArray.Add("Upgrade Data");
                    itemsArray.Add("Version Information");
                }
                //EXPCL_LP
                else if (m_printerLanguageComboBox.SelectedIndex == 3 || m_printerLanguageComboBox.SelectedIndex == 4)
                {
                    itemsArray.Clear();
                    itemsArray.Add("Battery Condition");
                    itemsArray.Add("Bluetooth Configuration");
                    itemsArray.Add("General Status");
                    itemsArray.Add("Magnetic Card Data");
                    itemsArray.Add("Memory Status");
                    itemsArray.Add("Printer Options");
                    itemsArray.Add("PrintHead Status");
                    itemsArray.Add("Version Information");
                }

                //DPL mode
                else
                {
                    itemsArray.Add("Printer Information");
                    itemsArray.Add("Files and Internal Fonts");
                    itemsArray.Add("Media Label");
                    itemsArray.Add("Print Controls");
                    itemsArray.Add("System Settings");
                    itemsArray.Add("Sensor Calibration");
                    itemsArray.Add("Miscellaneous");
                    itemsArray.Add("Serial Config");
                    itemsArray.Add("Auto Update Settings");
                    itemsArray.Add("Avalanche Enabler Settings");
                    itemsArray.Add("Bluetooth Config");
                    itemsArray.Add("Network General");
                    itemsArray.Add("Network Wireless");
                    itemsArray.Add("Printer Status");
                }//end else
                m_printItemsComboBox.DataSource = itemsArray.ToArray();
            }//end else
        }

        private void m_printerLanguageComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            reloadItemsArray();
        }

        private void m_printRadio_CheckedChanged(object sender, EventArgs e)
        {
            reloadItemsArray();
            if (m_printRadio.Checked)
            {
                m_performButton.Text = "Print";
                m_actionLabel.Text = "Select what to print:";
                printRadioChecked = true;
                m_browseButton.Visible = true;
            }
            else
            {
                m_performButton.Text = "Query";
                m_actionLabel.Text = "Select what to query:";
                printRadioChecked = false;
                m_browseButton.Visible = false;
            }
        }

        private void m_queryRadio_CheckedChanged(object sender, EventArgs e)
        {
            reloadItemsArray();
            if (m_printRadio.Checked)
            {
                m_performButton.Text = "Print";
                m_actionLabel.Text = "Select what to print:";
                printRadioChecked = true;
                m_browseButton.Visible = true;
            }
            else
            {
                m_performButton.Text = "Query";
                m_actionLabel.Text = "Select what to query:";
                printRadioChecked = false;
                m_browseButton.Visible = false;
            }
        }

        private void m_connComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            connType = m_connComboBox.SelectedItem.ToString();
            if (connType == "TCP/IP")
            {
                m_portLabel.Text = "Port:";
                m_deviceAddressTextBox.Text = m_deviceIP;
                m_portTextBox.Text = m_devicePort.ToString();
            }
            else if (connType == "Bluetooth")
            {
                m_deviceAddressTextBox.Text = m_deviceMAC;
                m_portLabel.Text = "Passkey:";
                m_portTextBox.Text = m_passKey;
            }
        }

        //Format bluetooth address from format 001122334455 to 00-11-22-33-44-55
        public string formatBluetoothAddress(string bluetoothAddr)
        {
            //Format MAC address string
            StringBuilder formattedBTAddress = new StringBuilder(bluetoothAddr);
            for (int bluetoothAddrPosition = 2; bluetoothAddrPosition <= formattedBTAddress.Length - 2; bluetoothAddrPosition += 3)
                formattedBTAddress.Insert(bluetoothAddrPosition, ":");

            return formattedBTAddress.ToString();
        }

        private void ShowMessageBox(string message, string title)
        {
            MessageForm msgForm = new MessageForm(message, title);
            msgForm.ShowDialog();
        }

        private void m_browseButton_Click(object sender, EventArgs e)
        {
            fileDlg = new OpenFileDialog();
            if (fileDlg.ShowDialog() == DialogResult.OK)
            {
                selectedFilesList.Add(fileDlg.FileName);
                itemsArray.Add(fileDlg.FileName);
                m_printItemsComboBox.DataSource = itemsArray.ToArray();
                m_printItemsComboBox.SelectedIndex = itemsArray.IndexOf(fileDlg.FileName);
            }
        }

        private void m_printHeadCmbo_SelectedIndexChanged(object sender, EventArgs e)
        {
            String value = (String)m_printHeadCmbo.SelectedItem;
            m_printHeadWidth = int.Parse(value.Substring(0, 3));
        }

        private void m_deviceAddressTextBox_TextChanged(object sender, EventArgs e)
        {
            connType = m_connComboBox.SelectedItem.ToString();
            if (connType == "TCP/IP")
            {
                m_deviceIP = m_deviceAddressTextBox.Text.Trim();
            }
            else if (connType == "Bluetooth")
            {
                m_deviceMAC = m_deviceAddressTextBox.Text.Trim();
            }
        }

        private void m_deviceAddressTextBox_Enter(object sender, EventArgs e)
        {
            m_deviceAddressTextBox.SelectAll();
        }

        private void m_portTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                connType = m_connComboBox.SelectedItem.ToString();
                if (connType == "TCP/IP")
                {
                    m_portLabel.Text = "Port:";
                    m_devicePort = int.Parse(m_portTextBox.Text.Trim());
                }
                else if (connType == "Bluetooth")
                {
                    m_portLabel.Text = "Passkey:";
                    m_passKey = m_portTextBox.Text.Trim();
                }
            }
            catch (Exception ex)
            {
                m_statusTextBox.Text += "Error: " + ex.Message;
            }
        }

        private void m_portTextBox_Enter(object sender, EventArgs e)
        {
            m_portTextBox.SelectAll();
        }

        private void m_portTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                connType = m_connComboBox.SelectedItem.ToString();
                if (connType == "TCP/IP")
                {
                    m_portLabel.Text = "Port:";
                    if (!Regex.IsMatch(m_portTextBox.Text.Trim(), "^([0-9]+)$"))
                        throw new Exception("Invalid port entered.");
                    m_devicePort = int.Parse(m_portTextBox.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                m_statusTextBox.Text += "Error: " + ex.Message;
            }
        }
    }
}
