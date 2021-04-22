using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace DOPrint_WD
{
    public class AppSettings
    {
        private int m_connectionIndex;

        public int ConnectionIndex
        {
            get { return m_connectionIndex; }
            set { m_connectionIndex = value; }
        }
        private string m_deviceMAC;

        public string DeviceMAC
        {
            get { return m_deviceMAC; }
            set { m_deviceMAC = value; }
        }

        private string m_deviceIP;

        public string DeviceIP
        {
            get { return m_deviceIP; }
            set { m_deviceIP = value; }
        }
        private int m_devicePort;

        public int DevicePort
        {
            get { return m_devicePort; }
            set { m_devicePort = value; }
        }
        private string m_devicePassKey;

        public string DevicePassKey
        {
            get { return m_devicePassKey; }
            set { m_devicePassKey = value; }
        }
        private bool m_isPrint;

        public bool IsPrint
        {
            get { return m_isPrint; }
            set { m_isPrint = value; }
        }
        private int m_langIndex;

        public int LangIndex
        {
            get { return m_langIndex; }
            set { m_langIndex = value; }
        }

        private int m_printHeadIndex;

        public int PrintHeadIndex
        {
            get { return m_printHeadIndex; }
            set { m_printHeadIndex = value; }
        }

        public bool LoadSettings()
        {
            return LoadSettings(Directory.GetCurrentDirectory() + "\\config.xml");
        }
        public bool LoadSettings(string path)
        {
            try {
                XmlSerializer ser = new XmlSerializer(typeof(AppSettings));
                FileStream fs = File.OpenRead(path);

                //Load Settings from file
                AppSettings localSettings = (AppSettings)ser.Deserialize(fs);

                //Update new parameters
                this.ConnectionIndex = localSettings.ConnectionIndex;
                this.DeviceIP = localSettings.DeviceIP;
                this.DeviceMAC = localSettings.DeviceMAC;
                this.DevicePassKey = localSettings.DevicePassKey;
                this.DevicePort = localSettings.DevicePort;
                this.IsPrint = localSettings.IsPrint;
                this.LangIndex = localSettings.LangIndex;
                this.PrintHeadIndex = localSettings.PrintHeadIndex;
                fs.Close();
            }
            catch (Exception ex) 
            {
                System.Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

        
        public bool SaveSettings()
        {
            return SaveSettings(Directory.GetCurrentDirectory() + "\\config.xml");
        }

        public bool SaveSettings(string path)
        {
            try {
                //Save settings
                StreamWriter sw = new StreamWriter(path);
                XmlSerializer ser = new XmlSerializer(typeof(AppSettings));
                ser.Serialize(sw, this);
                sw.Close();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

    }
}
