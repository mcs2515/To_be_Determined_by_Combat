using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace _106Project2 {
    class AIHandler {
        private bool Random;
        private Queue<Turtle.Command> CommandQueue;
        private string FilePath;
        private string[] FileList;
        private List<string> UsableFiles = new List<string>();
        private List<string> UnusableFiles = new List<string>();

        public AIHandler(string filePath, bool random) {
            FilePath = filePath;
            Random = random;
            InitializeAI();
        }

        private void InitializeAI() {
            FileList = Directory.GetFiles(FilePath);
            StreamReader sr;
            char[] tempCommands;
            if (FileList.Length == 0) {
                //If no files were found, default to random AI
                Random = true;
            } else {
                foreach (string p in FileList) {
                    //if (p.Substring(0, 8).ToLower() != "aiscript") {
                    //    continue; //Only check the file if it starts with "AIScript" (not case-sensitive)
                    //}
                    try {
                        sr = new StreamReader(File.OpenRead(p));
                        tempCommands = sr.ReadToEnd().ToLower().ToCharArray();
                        foreach (char c in tempCommands) {
                            if (c != 'j' && c != 'l' && c != 'r' && c != 'b' && c != 'f') {
                                //If unacceptable commands in file, ignore file
                                UnusableFiles.Add(p);
                                sr.Close();
                                continue;
                            }
                        }
                        //Add approved files to list
                        UsableFiles.Add(p);
                        sr.Close();
                    } catch (Exception) {
                        UnusableFiles.Add(p);
                    }
                }
            }
            if (UsableFiles.Count == 0) {
                Random = true;
            }
        }

        public void ResetAI() {
            if (Random) {
                return;
            }
            Random r = new Random();
            StreamReader sr;
            string path;
            char[] tempCommands;
            CommandQueue = new Queue<Turtle.Command>();
            for (int i = 0; i < 5; i++) {
                path = UsableFiles[r.Next(UsableFiles.Count)];
                sr = new StreamReader(File.OpenRead(path));
                tempCommands = sr.ReadToEnd().ToLower().ToCharArray();
                foreach (char c in tempCommands) {
                    switch (c) {
                        case 'j':
                            CommandQueue.Enqueue(Turtle.Command.JUMP);
                            break;
                        case 'l':
                            CommandQueue.Enqueue(Turtle.Command.LEFT);
                            break;
                        case 'r':
                            CommandQueue.Enqueue(Turtle.Command.RIGHT);
                            break;
                        case 'b':
                            CommandQueue.Enqueue(Turtle.Command.BLOCK);
                            break;
                        case 'f':
                            CommandQueue.Enqueue(Turtle.Command.FIRE);
                            break;
                    }
                }
                sr.Close();
            }
        }

        public Turtle.Command NextCommand() {
            if (Random) {
                return (Turtle.Command)(new Random().Next(5));
            } else {
            if (CommandQueue.Count == 0) {
                ResetAI();
            }
            return CommandQueue.Dequeue();
            }
        }
    }
}
