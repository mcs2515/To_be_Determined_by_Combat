using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataEditor {
    public static class FileManager {
        //public static string[] filePaths; //TODO: Add multiple-file storage support, including file extension recognition
        public static string filePath;
        public static string baseFilePath;
        public static int varIndex;
        public static List<object> varData;
        public static List<string> varTypes, varNames;

        public static void Initialize() {
            varIndex = -1;
            varData = new List<object>();
            baseFilePath = Directory.GetCurrentDirectory();
            varTypes = new List<string>();
            varNames = new List<string>();
        }

        public static string UpdateGuideText() {
            string text = "";
            for (int i = 0; i < varTypes.Count; i++) {
                text += (i + 1) + " - " + varTypes[i] + " - " + varNames[i] + "\n";
            }
            return text;
        }

        public static void ReadData() {
            varIndex = -1;
            if (!File.Exists("../../../../TurtleGame/106Project2/Data/Data.dat")) {
                MessageBox.Show("Data file not found.");
                return;
            }
            using (BinaryReader br = new BinaryReader(File.OpenRead("../../../../TurtleGame/106Project2/Data/Data.dat"))) {
                while (br.BaseStream.Position != br.BaseStream.Length) {
                    varTypes.Add(br.ReadString().Trim().ToUpper());
                    varNames.Add(br.ReadString().Trim());
                    switch (varTypes[varTypes.Count - 1]) {
                        case "INT16":
                            varData.Add(br.ReadInt16());
                            break;
                        case "INT32":
                            varData.Add(br.ReadInt32());
                            break;
                        case "INT64":
                            varData.Add(br.ReadInt64());
                            break;
                        case "FLOAT":
                            varData.Add(br.ReadSingle());
                            break;
                        case "DOUBLE":
                            varData.Add(br.ReadDouble());
                            break;
                        case "STRING":
                            varData.Add(br.ReadString());
                            break;
                        case "BOOL":
                            varData.Add(br.ReadBoolean());
                            break;
                        default:
                            MessageBox.Show("Unidentified data type \"" + varTypes[varTypes.Count - 1] + "\". Attempting to skip.");
                            break;
                    }
                }
            }
        }

        public static void SaveVar(string text) {
            if (varIndex == -1) {
                MessageBox.Show("Read file before writing data.");
                return;
            }
            try {
                switch (varTypes[varIndex]) {
                    case "INT16":
                        varData[varIndex] = short.Parse(text);
                        break;
                    case "INT32":
                        varData[varIndex] = int.Parse(text);
                        break;
                    case "INT64":
                        varData[varIndex] = long.Parse(text);
                        break;
                    case "FLOAT":
                        varData[varIndex] = float.Parse(text);
                        break;
                    case "DOUBLE":
                        varData[varIndex] = double.Parse(text);
                        break;
                    case "STRING":
                        varData[varIndex] = text;
                        break;
                    case "BOOL":
                        varData[varIndex] = bool.Parse(text);
                        break;
                    default:
                        MessageBox.Show("This should never happen: Type 1"); //Type 1 failure
                        break;
                }
            } catch (FormatException) {
                MessageBox.Show("Data not of correct type. Retry.");
            }
        }

        public static void SaveFile() {
            using (BinaryWriter bw = new BinaryWriter(File.OpenWrite("../../../../TurtleGame/106Project2/Data/Data.dat"))) {
                for (int i = 0; i < varData.Count; i++) {
                    bw.Write(varTypes[i]);
                    bw.Write(varNames[i]);
                    switch (varTypes[i]) {
                        case "INT16":
                            bw.Write((short)varData[i]);
                            break;
                        case "INT32":
                            bw.Write((int)varData[i]);
                            break;
                        case "INT64":
                            bw.Write((long)varData[i]);
                            break;
                        case "FLOAT":
                            bw.Write((float)varData[i]);
                            break;
                        case "DOUBLE":
                            bw.Write((double)varData[i]);
                            break;
                        case "STRING":
                            bw.Write((string)varData[i]);
                            break;
                        case "BOOL":
                            bw.Write((bool)varData[i]);
                            break;
                        default:
                            MessageBox.Show("Unidentified data type \"" + varTypes[i] + "\". Attempting to skip.");
                            break;
                    }
                }
            }
        }

        public static string ReadNextVariable(int increment) {
            varIndex += increment;
            while (varIndex < 0)
                varIndex += varData.Count;
            varIndex = varIndex % (varData.Count);
            switch (varTypes[varIndex]) {
                case "INT16":
                    return (short)varData[varIndex] + "";
                case "INT32":
                    return (int)varData[varIndex] + "";
                case "INT64":
                    return (long)varData[varIndex] + "";
                case "FLOAT":
                    return (float)varData[varIndex] + "";
                case "DOUBLE":
                    return (double)varData[varIndex] + "";
                case "STRING":
                    return (string)varData[varIndex] + "";
                case "BOOL":
                    return (bool)varData[varIndex] + "";
                default:
                    return "Data Type Error: " + varData[varIndex].GetType();
            }
        }
    }
}
