namespace AIOPortLoader
{
    using EloBuddy;
    using EloBuddy.SDK.Events;

    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Net;
    using System.Reflection;

    internal class Program
    {
        private static readonly string dllPath = @"C:\Users\" + Environment.UserName + @"\AppData\Roaming\EloBuddy\Addons\Libraries\PORT_SHARP_AIO.dll";

        private const string dllAddress = "https://github.com/cttbot/Port/raw/master/AIO/PORT_SHARP_AIO.dll";
        private const string dllVersion = "https://github.com/cttbot/Port/raw/master/AIO/version.txt";

        private static void Main(string[] Args)
        {
            Loading.OnLoadingComplete += eventArgs =>
            {
                if (!File.Exists(dllPath))
                {
                    Chat.Print("PORT_SHARP_AIO not found : download PORT_SHARP_AIO.dll from cttbot's Github", Color.Orange);
                    Chat.Print("put PORT_SHARP_AIO.dll to " + dllPath, Color.Orange);
                    //DownloadAddon();
                }
                else
                {
                    var GitVersion = DownloadVersion();

                    var myAddon = Assembly.LoadFrom(dllPath);
                    var myVersion = myAddon.GetName().Version.ToString();

                    if (GitVersion != myVersion)
                    {
                        Chat.Print("PORT_SHARP_AIO : download PORT_SHARP_AIO.dll from cttbot's Github", Color.Orange);
                        Chat.Print("put PORT_SHARP_AIO.dll to " + dllPath, Color.Orange);

                        DownloadAddon();
                        return;
                    }
                   
                    Assembly assembly = Assembly.LoadFrom(dllPath);
                    Type type = assembly.GetType("PORT_SHARP_AIO.Program");
                    if (type != null)
                    {
                        MethodInfo methodInfo = type.GetMethod("Loading_OnLoadingComplete");
                        if (methodInfo != null)
                        {
                            object result = null;
                            ParameterInfo[] parameters = methodInfo.GetParameters();
                            object classInstance = Activator.CreateInstance(type, null);
                            if (parameters.Length == 0)
                            {
                                //This works fine
                                result = methodInfo.Invoke(classInstance, null);
                            }
                            else
                            {
                                object[] parametersArray = new object[] { "Hello" };

                                //The invoke does NOT work it throws "Object does not match target type"             
                                result = methodInfo.Invoke(classInstance, parametersArray);
                            }
                        }
                    }
                    else
                        Chat.Print("load false");
                }
            };
        }

        
        private static void DownloadAddon()
        {
            using (var web = new WebClient())
            {
                web.DownloadFile(dllAddress, dllPath);
            }
            Chat.Print("PORT_SHARP_AIO : download PORT_SHARP_AIO.dll from cttbot's Github", Color.Orange);
            Chat.Print("put PORT_SHARP_AIO.dll to " + dllPath, Color.Orange);
            Chat.Print("it's can't auto update ", Color.Orange);
        }

        private static string DownloadVersion()
        {
            using (var web = new WebClient())
            {
                var version = web.DownloadString(dllVersion);

                return version;
            }
        }
    }
}
