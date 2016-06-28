using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Configuration;
using System.Xml.Linq;

namespace BE_Stress_Tool
{
    #region[ struct variable for Step of scenario]
    public struct ConfigValue
    {
        public string name;
        public string value;
    }
    #endregion

    public class ConfigFile
    {
        public Dictionary<string, string> allItems;

        public ConfigFile()
        {
            // Initial variable
        }

        // Load config file
        public int LoadFile(string sFName)
        {

            try
            {
                var doc = XDocument.Load(sFName);
                var rootNodes = doc.Root.DescendantNodes().OfType<XElement>();
                allItems = rootNodes.ToDictionary(n => n.Name.ToString(), n => n.Value);
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                throw new ArgumentException(ex.Message);
            }

            return DIOConst.RET_OK;
        }

        // Write dictionary to file
        public int WriteFile(Dictionary<string, string> dicOptValue, string stFPath)
        {

            try
            {
                allItems = dicOptValue;
                var root = new XElement("All");
                foreach (KeyValuePair<string, string> pair in dicOptValue)
                {
                    root.Add(
                            new XElement(pair.Key, pair.Value)
                        );
                }

                root.Save(stFPath);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                throw new ArgumentException(ex.Message);
            }

            return DIOConst.RET_OK;
        }
    }
}
