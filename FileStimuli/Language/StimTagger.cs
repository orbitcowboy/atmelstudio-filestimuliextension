using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;

namespace Xoriath.FileStimuli.Language
{
    internal class StimErrorTag : ErrorTag
    {
        public StimErrorTag(string type, string tooltip) : base(type, tooltip) { }
    }

    class StimErrorTagger : ITagger<IErrorTag>
    {
        private readonly ITextBuffer mBuffer;

        public StimErrorTagger(ITextBuffer buffer)
        {
            mBuffer = buffer;
        }

        public IEnumerable<ITagSpan<IErrorTag>> GetTags(NormalizedSnapshotSpanCollection spans)
        {
            foreach (var span in spans)
            {
                StimErrorTag spanError = StimParser.ParseError(span);

                if (spanError != null)
                    yield return new TagSpan<IErrorTag>(span, spanError);
            }
        }

#pragma warning disable 0067
        public event EventHandler<SnapshotSpanEventArgs> TagsChanged;
#pragma warning restore 0067
    }
}
