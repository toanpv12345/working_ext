using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Reflection;

namespace BE_Stress_Tool
{
    class ComFunc
    {
        public static byte[] StructureToByteArray(object objStruct)
        {
            int iObjLength;                                             // length of input object struct
            byte[] byOutput;                                            // return byte array
            IntPtr ptrResult = IntPtr.Zero;                             // convert result byte array

            try
            {
                // serlialize object struct to bytes
                // Get length of input object struct
                iObjLength = Marshal.SizeOf(objStruct);

                // Create output byte array
                byOutput = new byte[iObjLength];

                // Alloc memory in Marshal libary for converted result
                ptrResult = Marshal.AllocHGlobal(iObjLength);

                // Call function of Marshal library to convert input object struct to byte array
                Marshal.StructureToPtr(objStruct, ptrResult, false);

                // Copy converted result in Marshal library memory to output byte array
                Marshal.Copy(ptrResult, byOutput, 0, iObjLength);
                Marshal.FreeHGlobal(ptrResult);

                // return output byte array
                return byOutput;
            }
            catch
            {
                Marshal.FreeHGlobal(ptrResult);
                return null;
            }
        }

        // Set value from directory to structure
        public static int SetValueToStruct(Dictionary<string, string> dicOptValue, ref object objStruct)
        {
            try
            {
                // Scan all field in input struct object
                Type tyObj = objStruct.GetType();
                foreach (FieldInfo fiField in tyObj.GetFields())
                {
                    // If type of field is unsigned integer 32 bit
                    if (fiField.FieldType == typeof(System.Int32))
                    {
                        // Get value from dictionary
                        string string_value = dicOptValue[fiField.Name];
                        // Convert to integer
                        Int32 int_value = ComFunc.StringToInt32(string_value);
                        // Set value to structure
                        fiField.SetValue(objStruct, (Int32)int_value);
                    }
                    else if (fiField.FieldType == typeof(System.Char[]))
                    {
                        // Get value from dictionary
                        string string_value = dicOptValue[fiField.Name];
                        // Get field length
                        char[] field_value = (char [])fiField.GetValue(objStruct);

                        // Copy value from directory to field
                        Array.Copy(Encoding.ASCII.GetBytes(string_value), 
                                   field_value, 
                                   System.Math.Min(field_value.Length, string_value.Length));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
            return DIOConst.RET_OK;
        }

        public static object ByteArrayToStructure(byte[] byteInput, object objStruct)
        {
            Type tyObj;                                                 // type of input struct object
            Int32 iUINT32FieldLitEndian;                                // unsigned integer 32 bit field in little endian
            Int16 iUINT16FieldLitEndian;                                // unsigned integer 16 bit field in little endian
            Int32 iINT32FieldLitEndian;                                 // integer 32 bit field in little endian
            int iObjLength;                                             // length of input object struct
            IntPtr ptrInput = IntPtr.Zero;                              // input byte array

            try
            {
                // Get length of input struct object
                iObjLength = Marshal.SizeOf(objStruct);
                // If length of struct is not equal length of byte array
                if (iObjLength == byteInput.Length)
                {
                    // Alloc memory in Marshal library for input byte array
                    ptrInput = Marshal.AllocHGlobal(iObjLength);

                    // Copy input byte array to Marshal library memory
                    Marshal.Copy(byteInput, 0, ptrInput, iObjLength);

                    // Call function of Marshal library to convert input byte array to object struct
                    objStruct = Marshal.PtrToStructure(ptrInput, objStruct.GetType());
                    Marshal.FreeHGlobal(ptrInput);

                    // Convert object to little endian
                    // Get type of object struct
                    tyObj = objStruct.GetType();
                    // Scan all field in object struct
                    foreach (FieldInfo fiField in tyObj.GetFields())
                    {
                        // If field is unsigned integer 32 bit
                        if (fiField.FieldType == typeof(System.UInt32))
                        {
                            // convert field to big endian
                            iUINT32FieldLitEndian = (Int32)System.Net.IPAddress.NetworkToHostOrder(unchecked((Int32)(UInt32)fiField.GetValue(objStruct)));
                            // set field to new value of little endian
                            fiField.SetValue(objStruct, (UInt32)iUINT32FieldLitEndian);
                        }
                        // If field is unsigned integer 16 bit
                        else if (fiField.FieldType == typeof(System.UInt16))
                        {
                            // convert field to big endian
                            iUINT16FieldLitEndian = (Int16)System.Net.IPAddress.NetworkToHostOrder(unchecked((Int16)(UInt16)fiField.GetValue(objStruct)));
                            // set field to new value of little endian
                            fiField.SetValue(objStruct, (UInt16)iUINT16FieldLitEndian);
                        }
                        // If field is integer 32 bit
                        else if (fiField.FieldType == typeof(System.Int32))
                        {
                            // convert field to big endian
                            iINT32FieldLitEndian = (Int32)System.Net.IPAddress.NetworkToHostOrder(unchecked((Int32)fiField.GetValue(objStruct)));
                            // set field to new value of little endian
                            fiField.SetValue(objStruct, iINT32FieldLitEndian);
                        }
                    }
                    // return converted object struct
                    return objStruct;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                Marshal.FreeHGlobal(ptrInput);
                // if exception occurs, return null
                return null;
            }
        }

        // Convert Byte Array to UINT32
        public static uint ByteArrayToUint32(byte[] byteArr)
        {
            UInt32 combined = (UInt32)((byteArr[0] << 32) | (byteArr[1] << 24) | (byteArr[2] << 16) | (byteArr[3] << 8));

            return combined;
        }

        // Convert string to INT32
        public static Int32 StringToInt32(string string_value)
        {
            Int32 convert_value = 0;

            if (string_value[0] == '0' && 
                (string_value[1] == 'x' || string_value[1] == 'X'))
            {
                convert_value = Convert.ToInt32(string_value, 16);
            }
            else
            {
                convert_value = Convert.ToInt32(string_value, 10);
            }
            return convert_value;
        }
    }
}
