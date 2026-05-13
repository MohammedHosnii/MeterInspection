using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public static class AppState
    {
        public static bool IsOnline
        {
            get;
            set;
        }

        public static DateTime LastCheck
        {
            get;
            set;
        } = DateTime.MinValue;
    }
}