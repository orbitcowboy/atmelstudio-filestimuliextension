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


        private static readonly Regex mNoArgumentRegex = new Regex(@"[$#]([a-zA-Z0-9/\\:])*[ ]*$", RegexOptions.Compiled);
        private static readonly Regex mOneArgumentRegex = new Regex(@"[$#][a-zA-Z0-9/\\:]+[ ]([a-zA-Z0-9/\\:])+[ ]*$", RegexOptions.Compiled);
        private static readonly Regex mOneArgumentIsNumberRegex = new Regex(@"[#][0-9]+[ ]*$", RegexOptions.Compiled);
        private static readonly Regex mTwoArgumentRegex = new Regex(@"[$#][a-zA-Z0-9/\\:]+[ ]([a-zA-Z0-9/\\:])+[ ]([a-zA-Z0-9/\\:])+[ ]*$", RegexOptions.Compiled);

        private static readonly string ArgumentNumberErrorType = "Argument number error";
        private static readonly string UnknownDirectiveErrorType = "Unknown directive";

        public static ITagSpan<ErrorTag> ParseError(SnapshotSpan span)
        {
            string text = span.GetText();

            if (text.Contains("//"))
                return null;
            else if (text.Contains(@"$stimulate"))
            {
                if (!mOneArgumentRegex.IsMatch(text))
                    return new TagSpan<ErrorTag>(span, new ErrorTag(ArgumentNumberErrorType));
                return null;
            }
            else if (text.Contains(@"$quit"))
            {
                if (!mNoArgumentRegex.IsMatch(text))
                    return new TagSpan<ErrorTag>(span, new ErrorTag(ArgumentNumberErrorType));
                return null;
            }
            else if (text.Contains(@"$break"))
            {
                if (!mNoArgumentRegex.IsMatch(text))
                    return new TagSpan<ErrorTag>(span, new ErrorTag(ArgumentNumberErrorType));
                return null;
            }
            else if (text.Contains(@"$repeat"))
            {
                if (!mOneArgumentRegex.IsMatch(text))
                    return new TagSpan<ErrorTag>(span, new ErrorTag(ArgumentNumberErrorType));
                return null;
            }
            else if (text.Contains(@"$endrep"))
            {
                if (!mNoArgumentRegex.IsMatch(text))
                    return new TagSpan<ErrorTag>(span, new ErrorTag(ArgumentNumberErrorType));
                return null;
            }
            else if (text.Contains(@"$log"))
            {
                if (!mOneArgumentRegex.IsMatch(text))
                    return new TagSpan<ErrorTag>(span, new ErrorTag(ArgumentNumberErrorType));
                return null;
            }
            else if (text.Contains(@"$unlog"))
            {
                if (!mOneArgumentRegex.IsMatch(text))
                    return new TagSpan<ErrorTag>(span, new ErrorTag(ArgumentNumberErrorType));
                return null;
            }
            else if (text.Contains(@"$startlog"))
            {
                if (!mOneArgumentRegex.IsMatch(text))
                    return new TagSpan<ErrorTag>(span, new ErrorTag(ArgumentNumberErrorType));
                return null;
            }
            else if (text.Contains(@"$stoplog"))
            {
                if (!mNoArgumentRegex.IsMatch(text))
                    return new TagSpan<ErrorTag>(span, new ErrorTag(ArgumentNumberErrorType));
                return null;
            }
            else if (text.Contains(@"$fuse"))
            {
                if (!mTwoArgumentRegex.IsMatch(text))
                    return new TagSpan<ErrorTag>(span, new ErrorTag(ArgumentNumberErrorType));
                return null;
            }
            else if (text.Contains(@"#"))
            {
                if (!mOneArgumentIsNumberRegex.IsMatch(text))
                    return new TagSpan<ErrorTag>(span, new ErrorTag(ArgumentNumberErrorType));
                return null;
            }
            else if (text.Contains(@"$reset"))
            {
                if (!mOneArgumentRegex.IsMatch(text))
                {
                    string type = mOneArgumentRegex.Match(text).Groups[1].Value;
                    if (!(type == "p" || type == "e" || type == "b" || type == "s"))
                        return new TagSpan<ErrorTag>(span, new ErrorTag(ArgumentNumberErrorType));
                }
                return null;
            }
            else if (text.Contains(@"$"))
                return new TagSpan<ErrorTag>(span, new ErrorTag(UnknownDirectiveErrorType));
            return null;
        }
    }
}

