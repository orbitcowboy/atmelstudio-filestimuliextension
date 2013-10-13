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
            DELAY,
            COMMENT
        };

        private static readonly Regex mDirectiveRegex = new Regex(@"\$([a-zA-Z0-9]*) .*$", RegexOptions.Compiled);
        private static readonly Regex mCommentRegex = new Regex(@" *//.*$", RegexOptions.Compiled);
        private static readonly Regex mDelayRegex = new Regex(@"#[0-9]+.*$", RegexOptions.Compiled);

        public static IEnumerable<Tuple<StimLineTypes, SnapshotSpan>> Parse(ITextSnapshotLine line)
        {
            string text = line.GetText();

            if (mDirectiveRegex.IsMatch(text))
            {
                yield return new Tuple<StimLineTypes, SnapshotSpan>(
                    StimLineTypes.DIRECTIVE, new SnapshotSpan(line.Snapshot, line.Start, line.Length));
            }
            else if (mCommentRegex.IsMatch(text))
                yield return new Tuple<StimLineTypes, SnapshotSpan>(
                    StimLineTypes.COMMENT, new SnapshotSpan(line.Snapshot, line.Start, line.Length));
            else if (mDelayRegex.IsMatch(text))
                yield return new Tuple<StimLineTypes, SnapshotSpan>(
                    StimLineTypes.DELAY, new SnapshotSpan(line.Snapshot, line.Start, line.Length));
        }
    }
}
