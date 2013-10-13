using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Text;
using System.Text.RegularExpressions;

namespace Xoriath.FileStimuli.Language
{
    internal abstract class StimParser
    {
        public enum StimLineTypes
        {
            DIRECTIVE,
            DELAY
        };

        private static readonly Regex mDirectiveRegex = new Regex(@"\$([a-zA-Z0-9]*)$", RegexOptions.Compiled);

        public static IEnumerable<Tuple<StimLineTypes, SnapshotSpan>> Parse(ITextSnapshotLine line)
        {
            string text = line.GetText();

            if (mDirectiveRegex.Match(text).Success)
            {
                yield return new Tuple<StimLineTypes, SnapshotSpan>(
                    StimLineTypes.DIRECTIVE, new SnapshotSpan(line.Snapshot, line.Start, line.Length));
            }
        }
    }
}
