using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace BE_Stress_Tool
{
    #region[ struct variable for Step of scenario]
    public struct ScenarioStep
    {
        public string command;
        public string data;
        public string loop;
    }
    #endregion

    public class ScenarioFile
    {
        StreamReader reader = null;
        public List<ScenarioStep> lstStep;

        public ScenarioFile()
        {
            // Initial variable
            lstStep = new List<ScenarioStep>();
        }

        public int OpenFile(string sFPath)
        {
            try
            {
                reader = new StreamReader(File.OpenRead(sFPath));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                throw new ArgumentException(ex.Message);
            }

            return DIOConst.RET_OK;
        }

        public int ReadStep()
        {
            try
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(' ');
                    ScenarioStep stepCurrent;

                    if (values.Count() >= 2)
                    {
                        stepCurrent.command = values[0];
                        stepCurrent.data = values[1];
                        if (values.Count() >= 3)
                        {
                            stepCurrent.loop = values[2];
                        }
                        else
                        {
                            stepCurrent.loop = "1";
                        }

                        // Add to list data
                        lstStep.Add(stepCurrent);                       
                    }
                    else
                    {
                        Debug.WriteLine("Missing field in scenario file");

                        throw new ArgumentException("Missing field in scenario file");
                    }
                }
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
