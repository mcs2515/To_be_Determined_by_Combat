using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace _106Project2 {
    public static class GameVariables {
        public static int TurtleHeight = 108;
        public static int TurtleWidth = 155;
        public static int BulletHeight = 500000;
        public static int BulletWidth = 500000;
        public static long CommandTimeout = 10;
        public static int InitialHealth = 3;

        public static void WriteAllData(string dataFilePath) {
            using (BinaryWriter bw = new BinaryWriter(File.Open(dataFilePath, FileMode.Create))) {
                //IMPORTANT: update this as attributes change
                bw.Write("Int32");
                bw.Write("TurtleHeight");
                bw.Write(TurtleHeight);

                bw.Write("Int32");
                bw.Write("TurtleWidth");
                bw.Write(TurtleWidth);

                bw.Write("Int32");
                bw.Write("BulletHeight");
                bw.Write(BulletHeight);

                bw.Write("Int32");
                bw.Write("BulletWidth");
                bw.Write(BulletWidth);

                bw.Write("Int64");
                bw.Write("CommandTimeout");
                bw.Write(CommandTimeout);

                bw.Write("Int32");
                bw.Write("InitialHealth");
                bw.Write(InitialHealth);
            }
        }

        public static void ReadAllData(string filepath) {
            using (BinaryReader br = new BinaryReader(File.OpenRead(filepath))) {
                //IMPORTANT: update this as attributes change
                br.ReadString();
                br.ReadString();
                TurtleHeight = br.ReadInt32();

                br.ReadString();
                br.ReadString();
                TurtleWidth = br.ReadInt32();

                br.ReadString();
                br.ReadString();
                BulletHeight = br.ReadInt32();

                br.ReadString();
                br.ReadString();
                BulletWidth = br.ReadInt32();

                br.ReadString();
                br.ReadString();
                CommandTimeout = br.ReadInt64();

                br.ReadString();
                br.ReadString();
                InitialHealth = br.ReadInt32();
            }
        }
    }
}
