namespace Loader
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
        private static readonly string dllPath = @"C:\Users\" + Environment.UserName + @"\AppData\Roaming\EloBuddy\Addons\Libraries\Rengar.dll";

        private const string dllAddress = "https://github.com/cttbot/Port/raw/master/Rengar/Rengar.dll";
        private const string dllVersion = "https://raw.githubusercontent.com/cttbot/Port/master/Rengar/version.txt";

        private static void Main(string[] Args)
        {
            Loading.OnLoadingComplete += eventArgs =>
            {
                if (!File.Exists(dllPath))
                {
                    Chat.Print("CTTBOT Loader : Now Download the Addon, please waiting...", Color.Red);
                    DownloadAddon();
                }
                else
                {
                    var GitVersion = DownloadVersion();

                    var myAddon = Assembly.LoadFrom(dllPath);
                    var myVersion = myAddon.GetName().Version.ToString();

                    if (GitVersion != myVersion)
                    {
                        Chat.Print("CTTBOT Loader : Have a new Update for this Addon, now Download the new Version", Color.Green);
                        DownloadAddon();
                        return;
                    }
                   
                    Assembly assembly = Assembly.LoadFrom(dllPath);
                    Type type = assembly.GetType("Rengar.Program");
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
                                result = methodInfo.Invoke(classInstance, null);
                            }
                            else
                            {
                                object[] parametersArray = new object[] { "ho ho" };          
                                result = methodInfo.Invoke(classInstance, parametersArray);
                            }
                        }
                    }
                    else
                        Chat.Print("load file false");
                }
            };
        }

        
        private static void DownloadAddon()
        {
            if (File.Exists(dllPath))
            {
                File.Delete(dllPath);
            }

            using (var web = new WebClient())
            {
                web.DownloadFile(dllAddress, dllPath);
            }

            Chat.Print("CTTBOT Loader: Download Successful ^^... Please F5 Reload the Addon!", Color.Yellow);
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
