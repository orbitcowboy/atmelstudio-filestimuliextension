using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Text;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.Text.Tagging;

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

            if (mDirectiveRegex.IsMatch(text)) {
                string match = mDirectiveRegex.Match(text).Groups[1].Value;
                yield return new Tuple<StimLineTypes, SnapshotSpan>(
                    StimLineTypes.DIRECTIVE, new SnapshotSpan(line.Snapshot, line.Start, text.IndexOf(match) + match.Length));
            }
            else if (mCommentRegex.IsMatch(text))
                yield return new Tuple<StimLineTypes, SnapshotSpan>(
                    StimLineTypes.COMMENT, new SnapshotSpan(line.Snapshot, line.Start, line.Length));
            else if (mDelayRegex.IsMatch(text))
                yield return new Tuple<StimLineTypes, SnapshotSpan>(
                    StimLineTypes.DELAY, new SnapshotSpan(line.Snapshot, line.Start, line.Length));
        }


        private static readonly Regex mNoArgumentRegex = new Regex(@"[$#][a-zA-Z0-9/\\:]+[ ]*$", RegexOptions.Compiled);
        private static readonly Regex mOneArgumentRegex = new Regex(@"[$#][a-zA-Z0-9/\\:]+[ ][a-zA-Z0-9/\\:]+[ ]*$", RegexOptions.Compiled);
        private static readonly Regex mTwoArgumentRegex = new Regex(@"[$#][a-zA-Z0-9/\\:]+[ ][a-zA-Z0-9/\\:]+[ ][a-zA-Z0-9/\\:]+[ ]*$", RegexOptions.Compiled);

        public static ITagSpan<ErrorTag> ParseError(SnapshotSpan span)
        {
            string text = span.GetText();

            if (text.Contains(@"$stimulate") && !mOneArgumentRegex.IsMatch(text))
                return new TagSpan<ErrorTag>(span, new ErrorTag("Argument number error"));

            return null;
        }
    }
}

