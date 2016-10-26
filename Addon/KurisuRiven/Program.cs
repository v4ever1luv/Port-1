using System;
using System.Reflection;
using LeagueSharp.Common;
using EloBuddy.SDK.Events;

namespace KurisuRiven
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Game_OnGameLoad;
        }

        private static void Game_OnGameLoad(EventArgs args)
        {
            new KurisuRiven();
        }
    }
}