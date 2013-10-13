// Guids.cs
// MUST match guids.h
using System;

namespace MortenEngelhardtOlsen.FileStimuli
{
    static class GuidList
    {
        public const string guidFileStimuliPkgString = "0e72c057-f129-425e-95a0-172a4b94464b";
        public const string guidFileStimuliCmdSetString = "bb2b663e-ef85-40be-90b1-e66bf1258e3e";

        public static readonly Guid guidFileStimuliCmdSet = new Guid(guidFileStimuliCmdSetString);
    };
}