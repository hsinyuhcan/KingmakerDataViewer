﻿using ModBase;
using ModMaker.Utils;
using System.Reflection;
using UnityModManagerNet;

namespace DataViewer
{
#if (DEBUG)
    [EnableReloading]
#endif
    static class Main
    {
        public static Core<Storage, Settings> Core;
        public static Menu Menu;

        static bool Load(UnityModManager.ModEntry modEntry)
        {
            Core = new Core<Storage, Settings>(modEntry, Assembly.GetExecutingAssembly());
            Menu = new Menu(modEntry, Assembly.GetExecutingAssembly());
            modEntry.OnToggle = OnToggle;
#if (DEBUG)
            modEntry.OnUnload = Unload;
            return true;
        }

        static bool Unload(UnityModManager.ModEntry modEntry)
        {
            Menu = null;
            Core.Disable(modEntry, true);
            Core = null;
            return true;
        }
#else
            return true;
        }
#endif

        static bool OnToggle(UnityModManager.ModEntry modEntry, bool value)
        {
            if (value)
            {
                Core.Enable(modEntry);
                Menu.Enable(modEntry);
            }
            else
            {
                Menu.Disable(modEntry);
                Core.Disable(modEntry, false);
                ReflectionCache.Clear();
            }
            return true;
        }
    }
}
