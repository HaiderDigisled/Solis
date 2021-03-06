﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miscellaneous.Foundation
{
    public static class Settings
    {
        public static class Mediator {

        }

        public static class Database {
            public static string Connection { get; set; }
        }

        public static class FailureAlerts {
            public static string InternalRecipients { get; set; }
        }

        public static class RecoveryMode {
            public static bool Enable { get; set; }
            public static List<string> PlantIds { get; set; }
        }
    }
}
