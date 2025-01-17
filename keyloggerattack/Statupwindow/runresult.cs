﻿using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Windows;
using System.Linq;
using System.Diagnostics;
using Microsoft.Win32;
using System.Net;
using System.Threading;
using System.Reflection;
using LibSVMsharp.Helpers;
using LibSVMsharp.Extensions;
using LibSVMsharp;

namespace Statupwindow
{
    class runresult
    {
        public static void Read(string i)//第一次創建SVM檔案時，是用此檔案
        {
            
            string logPath_1 = @"..\..\..\keyloggerattack\Dataset\SVM_"+ i +"_east.txt";//東(認知因子)
            string logPath_2 = @"..\..\..\keyloggerattack\Dataset\SVM_" + i + "_Ground.txt";//地(認知因子)
            string logPath_3 = @"..\..\..\keyloggerattack\Dataset\SVM_" + i + "_sea.txt";//海(認知因子)
            string logPath_4 = @"..\..\..\keyloggerattack\Dataset\SVM_" + i + "_station.txt";//臺(認知因子)
            StreamWriter sw_1 = new StreamWriter(logPath_1, true);//東
            StreamWriter sw_2 = new StreamWriter(logPath_2, true);//地
            StreamWriter sw_3 = new StreamWriter(logPath_3, true);//海
            StreamWriter sw_4 = new StreamWriter(logPath_4, true);//臺

            string logPath_5 = @"..\..\..\keyloggerattack\Dataset\SVM_Inertia_" + i + "_east.txt";//東(慣性因子)
            string logPath_6 = @"..\..\..\keyloggerattack\Dataset\SVM_Inertia_" + i + "_Ground.txt";//地(慣性因子)
            string logPath_7 = @"..\..\..\keyloggerattack\Dataset\SVM_Inertia_" + i + "_sea.txt";//海(慣性因子)
            string logPath_8 = @"..\..\..\keyloggerattack\Dataset\SVM_Inertia_" + i + "_station.txt";//臺(慣性因子)
            StreamWriter sw_5 = new StreamWriter(logPath_5, true);//東
            StreamWriter sw_6 = new StreamWriter(logPath_6, true);//地
            StreamWriter sw_7 = new StreamWriter(logPath_7, true);//海
            StreamWriter sw_8 = new StreamWriter(logPath_8, true);//臺
            //string s1;
            int txtLength = 0, s2int = 0, keyint = 0, Dwelltimeint = 0, Intervalint = 0, stringlength = 0;
            int j = 0, outputint = 0;
            string[] s2 = new string[40000];
            string[] key = new string[40000];
            int[] Dwelltime = new int[40000];
            int[] Interval = new int[40000];
            //var handle = GetConsoleWindow();
            //ShowWindow(handle,1);
            if (File.Exists(@"..\..\..\keyloggerattack\Dataset\"+i+".txt"))
            {
                //讀取txt code
                StreamReader sr = new StreamReader(@"..\..\..\keyloggerattack\Dataset\" + i + ".txt");
                string line = string.Empty;

                while (!sr.EndOfStream)
                {
                    txtLength++;
                    s2[s2int] = sr.ReadLine();
                    s2int++;
                }

                while (j < txtLength)
                {
                    if (s2[j].Substring(0, 1) == null)
                    {
                        break;
                    }
                    if (s2[j].Substring(0, 1) == "K")
                    {
                        stringlength = s2[j].Length - 1;
                        key[keyint] = s2[j].Substring(1, stringlength);
                        keyint++;
                        stringlength = 0;
                        j++;
                    }
                    else if (s2[j].Substring(0, 1) == "D")
                    {
                        stringlength = s2[j].Length - 1;
                        Dwelltime[Dwelltimeint] = Int32.Parse(s2[j].Substring(1, stringlength));
                        Dwelltimeint++;
                        stringlength = 0;
                        j++;
                    }
                    else if (s2[j].Substring(0, 1) == "I")
                    {
                        stringlength = s2[j].Length - 1;
                        Interval[Intervalint] = Int32.Parse(s2[j].Substring(1, stringlength));
                        Intervalint++;
                        stringlength = 0;
                        j++;
                    }
                }

                j = 0;

                /*while (j < txtLength)
                {
                    if (key[j] == "W" && key[j + 1] == "D9" && key[j + 2] == "D6" && key[j + 3] == "D1" && key[j + 4] == "O" && key[j + 5] == "D3")//台北
                    {
                        while (outputint < 5)
                        {
                            if (outputint == 1 || outputint == 2 || outputint == 4)
                            {
                                outputint++;
                                continue;
                            }
                            sw.WriteLine("1 1:1 2:" + Interval[j + outputint] + " 3:" + Dwelltime[j + outputint] + " 4:" + (Dwelltime[j + outputint] + Interval[j + outputint] + Dwelltime[j + outputint + 1]) + " 5:" + (Dwelltime[j + outputint] + Interval[j + outputint]) + " 6:" + (Interval[j + outputint] + Dwelltime[j + outputint + 1]));
                            outputint++;
                        }
                        outputint = 0;

                    }
                    if (key[j] == "W" && key[j + 1] == "D9" && key[j + 2] == "D6" && key[j + 3] == "D2" && key[j + 4] == "U" && key[j + 5] == "D4")//台地
                    {

                        while (outputint < 5)
                        {
                            if (outputint == 1 || outputint == 2 || outputint == 4)
                            {
                                outputint++;
                                continue;
                            }
                            sw.WriteLine("1 1:2 2:" + Interval[j + outputint] + " 3:" + Dwelltime[j + outputint] + " 4:" + (Dwelltime[j + outputint] + Interval[j + outputint] + Dwelltime[j + outputint + 1]) + " 5:" + (Dwelltime[j + outputint] + Interval[j + outputint]) + " 6:" + (Interval[j + outputint] + Dwelltime[j + outputint + 1]));
                            outputint++;
                        }
                        outputint = 0;
                    }
                    if (key[j] == "C" && key[j + 1] == "D9" && key[j + 2] == "D3" && key[j + 3] == "D0" && key[j + 4] == "D4")//海岸
                    {
                        while (outputint < 5)
                        {
                            sw.WriteLine("1 1:2 2:" + Interval[j + outputint] + " 3:" + Dwelltime[j + outputint] + " 4:" + (Dwelltime[j + outputint] + Interval[j + outputint] + Dwelltime[j + outputint + 1]) + " 5:" + (Dwelltime[j + outputint] + Interval[j + outputint]) + " 6:" + (Interval[j + outputint] + Dwelltime[j + outputint + 1]));
                            outputint++;
                        }
                        outputint = 0;
                    }
                    j++;
                }
            */
                /* while (j < txtLength)
                 {
                     if (key[j] == "D2" && key[j + 1] == "K" && key[j + 2] == "D7")//的
                     {
                         while (outputint < 2)
                         {
                             sw.WriteLine("1 1:1 2:" + Interval[j + outputint] + " 3:" + Dwelltime[j + outputint] + " 4:" + (Dwelltime[j + outputint] + Interval[j + outputint] + Dwelltime[j + outputint + 1]) + " 5:" + (Dwelltime[j + outputint] + Interval[j + outputint]) + " 6:" + (Interval[j + outputint] + Dwelltime[j + outputint + 1]));
                             outputint++;
                         }
                         outputint = 0;
                     }
                     if (key[j] == "U" && key[j + 1] == "Space")//一
                     {
                         while (outputint < 1)
                         {
                             sw.WriteLine("1 1:2 2:" + Interval[j + outputint] + " 3:" + Dwelltime[j + outputint] + " 4:" + (Dwelltime[j + outputint] + Interval[j + outputint] + Dwelltime[j + outputint + 1]) + " 5:" + (Dwelltime[j + outputint] + Interval[j + outputint]) + " 6:" + (Interval[j + outputint] + Dwelltime[j + outputint + 1]));
                             outputint++;
                         }
                         outputint = 0;
                     }
                     if (key[j] == "G" && key[j + 1] == "D4")//是
                     {
                         while (outputint < 1)
                         {
                             sw.WriteLine("1 1:3 2:" + Interval[j + outputint] + " 3:" + Dwelltime[j + outputint] + " 4:" + (Dwelltime[j + outputint] + Interval[j + outputint] + Dwelltime[j + outputint + 1]) + " 5:" + (Dwelltime[j + outputint] + Interval[j + outputint]) + " 6:" + (Interval[j + outputint] + Dwelltime[j + outputint + 1]));
                             outputint++;
                         }
                         outputint = 0;
                     }
                     if (key[j] == "X" && key[j + 1] == "K" && key[j + 2] == "D7")//了
                     {
                         while (outputint < 2)
                         {
                             sw.WriteLine("1 1:4 2:" + Interval[j + outputint] + " 3:" + Dwelltime[j + outputint] + " 4:" + (Dwelltime[j + outputint] + Interval[j + outputint] + Dwelltime[j + outputint + 1]) + " 5:" + (Dwelltime[j + outputint] + Interval[j + outputint]) + " 6:" + (Interval[j + outputint] + Dwelltime[j + outputint + 1]));
                             outputint++;
                         }
                         outputint = 0;
                     }
                     if (key[j] == "J" && key[j + 1] == "I" && key[j + 2] == "D3")//我
                     {
                         while (outputint < 2)
                         {
                             sw.WriteLine("1 1:5 2:" + Interval[j + outputint] + " 3:" + Dwelltime[j + outputint] + " 4:" + (Dwelltime[j + outputint] + Interval[j + outputint] + Dwelltime[j + outputint + 1]) + " 5:" + (Dwelltime[j + outputint] + Interval[j + outputint]) + " 6:" + (Interval[j + outputint] + Dwelltime[j + outputint + 1]));
                             outputint++;
                         }
                         outputint = 0;
                     }
                     if (key[j] == "D1" && key[j + 1] == "J" && key[j + 2] == "D4")//不
                     {
                         while (outputint < 2)
                         {
                             sw.WriteLine("1 1:6 2:" + Interval[j + outputint] + " 3:" + Dwelltime[j + outputint] + " 4:" + (Dwelltime[j + outputint] + Interval[j + outputint] + Dwelltime[j + outputint + 1]) + " 5:" + (Dwelltime[j + outputint] + Interval[j + outputint]) + " 6:" + (Interval[j + outputint] + Dwelltime[j + outputint + 1]));
                             outputint++;
                         }
                         outputint = 0;
                     }
                     if (key[j] == "B" && key[j + 1] == "P" && key[j + 2] == "D6")//人
                     {
                         while (outputint < 2)
                         {
                             sw.WriteLine("1 1:7 2:" + Interval[j + outputint] + " 3:" + Dwelltime[j + outputint] + " 4:" + (Dwelltime[j + outputint] + Interval[j + outputint] + Dwelltime[j + outputint + 1]) + " 5:" + (Dwelltime[j + outputint] + Interval[j + outputint]) + " 6:" + (Interval[j + outputint] + Dwelltime[j + outputint + 1]));
                             outputint++;
                         }
                         outputint = 0;
                     }
                     if (key[j] == "Y" && key[j + 1] == "D9" && key[j + 2] == "D4")//在
                     {
                         while (outputint < 2)
                         {
                             sw.WriteLine("1 1:8 2:" + Interval[j + outputint] + " 3:" + Dwelltime[j + outputint] + " 4:" + (Dwelltime[j + outputint] + Interval[j + outputint] + Dwelltime[j + outputint + 1]) + " 5:" + (Dwelltime[j + outputint] + Interval[j + outputint]) + " 6:" + (Interval[j + outputint] + Dwelltime[j + outputint + 1]));
                             outputint++;
                         }
                         outputint = 0;
                     }
                     if (key[j] == "W" && key[j + 1] == "D8" && key[j + 2] == "Space")//他
                     {
                         while (outputint < 2)
                         {
                             sw.WriteLine("1 1:9 2:" + Interval[j + outputint] + " 3:" + Dwelltime[j + outputint] + " 4:" + (Dwelltime[j + outputint] + Interval[j + outputint] + Dwelltime[j + outputint + 1]) + " 5:" + (Dwelltime[j + outputint] + Interval[j + outputint]) + " 6:" + (Interval[j + outputint] + Dwelltime[j + outputint + 1]));
                             outputint++;
                         }
                         outputint = 0;
                     }
                     if (key[j] == "U" && key[j + 1] == "OemPeriod" && key[j + 2] == "D3")//有
                     {
                         while (outputint < 2)
                         {
                             sw.WriteLine("1 1:10 2:" + Interval[j + outputint] + " 3:" + Dwelltime[j + outputint] + " 4:" + (Dwelltime[j + outputint] + Interval[j + outputint] + Dwelltime[j + outputint + 1]) + " 5:" + (Dwelltime[j + outputint] + Interval[j + outputint]) + " 6:" + (Interval[j + outputint] + Dwelltime[j + outputint + 1]));
                             outputint++;
                         }
                         outputint = 0;
                     }
                     j++;
                 }*/

                while (j < txtLength)
                {
                    if (key[j] == "D2" && key[j + 1] == "J" && key[j + 2] == "OemQuestion" && key[j + 3] == "Space")//東(ㄉㄨㄥ)
                    {
                        while (outputint < 1)
                        {
                            sw_5.WriteLine("1 1:1 2:" + Interval[j + outputint] + " 3:" + Dwelltime[j + outputint] + " 4:" + (Dwelltime[j + outputint] + Interval[j + outputint] + Dwelltime[j + outputint + 1]) + " 5:" + (Dwelltime[j + outputint] + Interval[j + outputint]) + " 6:" + (Interval[j + outputint] + Dwelltime[j + outputint + 1]));
                            outputint++;
                        }
                        outputint = 0;
                    }
                    if (key[j] == "D2" && key[j + 1] == "OemQuestion" && key[j + 2] == "J" && key[j + 3] == "Space")//東(ㄉㄥㄨ)
                    {
                        while (outputint < 1)
                        {
                            sw_5.WriteLine("1 1:2 2:" + Interval[j + outputint] + " 3:" + Dwelltime[j + outputint] + " 4:" + (Dwelltime[j + outputint] + Interval[j + outputint] + Dwelltime[j + outputint + 1]) + " 5:" + (Dwelltime[j + outputint] + Interval[j + outputint]) + " 6:" + (Interval[j + outputint] + Dwelltime[j + outputint + 1]));
                            outputint++;
                        }
                        outputint = 0;
                    }
                    if (key[j] == "D2" && key[j + 1] == "J" && key[j + 2] == "OemQuestion" && key[j + 3] == "Space" && key[j + 4] == "D1" && key[j + 5] == "O" && key[j + 6] == "D3")//東北
                    {
                        while (outputint < 1)
                        {
                            sw_1.WriteLine("1 1:1 2:" + Interval[j + outputint] + " 3:" + Dwelltime[j + outputint] + " 4:" + (Dwelltime[j + outputint] + Interval[j + outputint] + Dwelltime[j + outputint + 1]) + " 5:" + (Dwelltime[j + outputint] + Interval[j + outputint]) + " 6:" + (Interval[j + outputint] + Dwelltime[j + outputint + 1]));
                            outputint++;
                        }
                        outputint = 0;
                    }
                    if (key[j] == "D2" && key[j + 1] == "J" && key[j + 2] == "OemQuestion" && key[j + 3] == "Space" && key[j + 4] == "D1" && key[j + 5] == "J" && key[j + 6] == "D4")//東部
                    {
                        while (outputint < 1)
                        {
                            sw_1.WriteLine("1 1:2 2:" + Interval[j + outputint] + " 3:" + Dwelltime[j + outputint] + " 4:" + (Dwelltime[j + outputint] + Interval[j + outputint] + Dwelltime[j + outputint + 1]) + " 5:" + (Dwelltime[j + outputint] + Interval[j + outputint]) + " 6:" + (Interval[j + outputint] + Dwelltime[j + outputint + 1]));
                            outputint++;
                        }
                        outputint = 0;
                    }
                    if (key[j] == "D2" && key[j + 1] == "U" && key[j + 2] == "D4")//地(ㄉ一ˋ)
                    {
                        while (outputint < 1)
                        {
                            sw_6.WriteLine("1 1:1 2:" + Interval[j + outputint] + " 3:" + Dwelltime[j + outputint] + " 4:" + (Dwelltime[j + outputint] + Interval[j + outputint] + Dwelltime[j + outputint + 1]) + " 5:" + (Dwelltime[j + outputint] + Interval[j + outputint]) + " 6:" + (Interval[j + outputint] + Dwelltime[j + outputint + 1]));
                            outputint++;
                        }
                        outputint = 0;
                    }
                    if (key[j] == "U" && key[j + 1] == "D2" && key[j + 2] == "D4")//地(一ㄉˋ)
                    {
                        while (outputint < 1)
                        {
                            sw_6.WriteLine("1 1:2 2:" + Interval[j + outputint] + " 3:" + Dwelltime[j + outputint] + " 4:" + (Dwelltime[j + outputint] + Interval[j + outputint] + Dwelltime[j + outputint + 1]) + " 5:" + (Dwelltime[j + outputint] + Interval[j + outputint]) + " 6:" + (Interval[j + outputint] + Dwelltime[j + outputint + 1]));
                            outputint++;
                        }
                        outputint = 0;
                    }
                    if (key[j] == "D2" && key[j + 1] == "U" && key[j + 2] == "D4" && key[j + 3] == "V" && key[j + 4] == "U" && key[j + 5] == "OemQuestion" && key[j + 6] == "D6")//地形
                    {
                        while (outputint < 1)
                        {
                            sw_2.WriteLine("1 1:1 2:" + Interval[j + outputint] + " 3:" + Dwelltime[j + outputint] + " 4:" + (Dwelltime[j + outputint] + Interval[j + outputint] + Dwelltime[j + outputint + 1]) + " 5:" + (Dwelltime[j + outputint] + Interval[j + outputint]) + " 6:" + (Interval[j + outputint] + Dwelltime[j + outputint + 1]));
                            outputint++;
                        }
                        outputint = 0;
                    }
                    if (key[j] == "D2" && key[j + 1] == "U" && key[j + 2] == "D4" && key[j + 3] == "F" && key[j + 4] == "M" && key[j + 5] == "Space")//地區
                    {
                        while (outputint < 1)
                        {
                            sw_2.WriteLine("1 1:2 2:" + Interval[j + outputint] + " 3:" + Dwelltime[j + outputint] + " 4:" + (Dwelltime[j + outputint] + Interval[j + outputint] + Dwelltime[j + outputint + 1]) + " 5:" + (Dwelltime[j + outputint] + Interval[j + outputint]) + " 6:" + (Interval[j + outputint] + Dwelltime[j + outputint + 1]));
                            outputint++;
                        }
                        outputint = 0;
                    }
                    if (key[j] == "D2" && key[j + 1] == "U" && key[j + 2] == "D4" && key[j + 3] == "G" && key[j + 4] == "D4")//地勢
                    {
                        while (outputint < 1)
                        {
                            sw_2.WriteLine("1 1:3 2:" + Interval[j + outputint] + " 3:" + Dwelltime[j + outputint] + " 4:" + (Dwelltime[j + outputint] + Interval[j + outputint] + Dwelltime[j + outputint + 1]) + " 5:" + (Dwelltime[j + outputint] + Interval[j + outputint]) + " 6:" + (Interval[j + outputint] + Dwelltime[j + outputint + 1]));
                            outputint++;
                        }
                        outputint = 0;
                    }
                    if (key[j] == "C" && key[j + 1] == "D9" && key[j + 2] == "D3")//海(ㄏㄞˇ)
                    {
                        while (outputint < 1)
                        {
                            sw_7.WriteLine("1 1:1 2:" + Interval[j + outputint] + " 3:" + Dwelltime[j + outputint] + " 4:" + (Dwelltime[j + outputint] + Interval[j + outputint] + Dwelltime[j + outputint + 1]) + " 5:" + (Dwelltime[j + outputint] + Interval[j + outputint]) + " 6:" + (Interval[j + outputint] + Dwelltime[j + outputint + 1]));
                            outputint++;
                        }
                        outputint = 0;
                    }
                    if (key[j] == "D9" && key[j + 1] == "C" && key[j + 2] == "D3")//海(ㄞㄏˇ)
                    {
                        while (outputint < 1)
                        {
                            sw_7.WriteLine("1 1:2 2:" + Interval[j + outputint] + " 3:" + Dwelltime[j + outputint] + " 4:" + (Dwelltime[j + outputint] + Interval[j + outputint] + Dwelltime[j + outputint + 1]) + " 5:" + (Dwelltime[j + outputint] + Interval[j + outputint]) + " 6:" + (Interval[j + outputint] + Dwelltime[j + outputint + 1]));
                            outputint++;
                        }
                        outputint = 0;
                    }
                    if (key[j] == "C" && key[j + 1] == "D9" && key[j + 2] == "D3" && key[j + 3] == "D0" && key[j + 4] == "D4")//海岸
                    {
                        while (outputint < 1)
                        {
                            sw_3.WriteLine("1 1:1 2:" + Interval[j + outputint] + " 3:" + Dwelltime[j + outputint] + " 4:" + (Dwelltime[j + outputint] + Interval[j + outputint] + Dwelltime[j + outputint + 1]) + " 5:" + (Dwelltime[j + outputint] + Interval[j + outputint]) + " 6:" + (Interval[j + outputint] + Dwelltime[j + outputint + 1]));
                            outputint++;
                        }
                        outputint = 0;
                    }
                    if (key[j] == "C" && key[j + 1] == "D9" && key[j + 2] == "D3" && key[j + 3] == "E" && key[j + 4] == "OemPeriod" && key[j + 5] == "Space")//海溝
                    {
                        while (outputint < 1)
                        {
                            sw_3.WriteLine("1 1:2 2:" + Interval[j + outputint] + " 3:" + Dwelltime[j + outputint] + " 4:" + (Dwelltime[j + outputint] + Interval[j + outputint] + Dwelltime[j + outputint + 1]) + " 5:" + (Dwelltime[j + outputint] + Interval[j + outputint]) + " 6:" + (Interval[j + outputint] + Dwelltime[j + outputint + 1]));
                            outputint++;
                        }
                        outputint = 0;
                    }
                    if (key[j] == "W" && key[j + 1] == "D9" && key[j + 2] == "D6")//臺(ㄊㄞˊ)
                    {
                        while (outputint < 1)
                        {
                            sw_8.WriteLine("1 1:1 2:" + Interval[j + outputint] + " 3:" + Dwelltime[j + outputint] + " 4:" + (Dwelltime[j + outputint] + Interval[j + outputint] + Dwelltime[j + outputint + 1]) + " 5:" + (Dwelltime[j + outputint] + Interval[j + outputint]) + " 6:" + (Interval[j + outputint] + Dwelltime[j + outputint + 1]));
                            outputint++;
                        }
                        outputint = 0;
                    }
                    if (key[j] == "D9" && key[j + 1] == "W" && key[j + 2] == "D6")//臺(ㄞㄊˊ)
                    {
                        while (outputint < 1)
                        {
                            sw_8.WriteLine("1 1:2 2:" + Interval[j + outputint] + " 3:" + Dwelltime[j + outputint] + " 4:" + (Dwelltime[j + outputint] + Interval[j + outputint] + Dwelltime[j + outputint + 1]) + " 5:" + (Dwelltime[j + outputint] + Interval[j + outputint]) + " 6:" + (Interval[j + outputint] + Dwelltime[j + outputint + 1]));
                            outputint++;
                        }
                        outputint = 0;
                    }
                    if (key[j] == "W" && key[j + 1] == "D9" && key[j + 2] == "D6" && key[j + 3] == "D1" && key[j + 4] == "O" && key[j + 5] == "D3")//台北
                    {
                        while (outputint < 1)
                        {
                            sw_4.WriteLine("1 1:1 2:" + Interval[j + outputint] + " 3:" + Dwelltime[j + outputint] + " 4:" + (Dwelltime[j + outputint] + Interval[j + outputint] + Dwelltime[j + outputint + 1]) + " 5:" + (Dwelltime[j + outputint] + Interval[j + outputint]) + " 6:" + (Interval[j + outputint] + Dwelltime[j + outputint + 1]));
                            outputint++;
                        }
                        outputint = 0;
                    }
                    if (key[j] == "W" && key[j + 1] == "D9" && key[j + 2] == "D6" && key[j + 3] == "D2" && key[j + 4] == "U" && key[j + 5] == "D4")//台地
                    {
                        while (outputint < 1)
                        {
                            sw_4.WriteLine("1 1:2 2:" + Interval[j + outputint] + " 3:" + Dwelltime[j + outputint] + " 4:" + (Dwelltime[j + outputint] + Interval[j + outputint] + Dwelltime[j + outputint + 1]) + " 5:" + (Dwelltime[j + outputint] + Interval[j + outputint]) + " 6:" + (Interval[j + outputint] + Dwelltime[j + outputint + 1]));
                            outputint++;
                        }
                        outputint = 0;
                    }
                    j++;

                }

                sw_1.Close();
                sw_2.Close();
                sw_3.Close();
                sw_4.Close();
                sw_5.Close();
                sw_6.Close();
                sw_7.Close();
                sw_8.Close();
                sr.Close();
                sw_1.Dispose();
                sw_2.Dispose();
                sw_3.Dispose();
                sw_4.Dispose();
                sw_5.Dispose();
                sw_6.Dispose();
                sw_7.Dispose();
                sw_8.Dispose();
                sr.Dispose();
            }
    }
        //  [DllImport("kernel32.dll")]
        // static extern IntPtr GetConsoleWindow();
        // [DllImport("user32.dll")]
        // static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
    }
}
