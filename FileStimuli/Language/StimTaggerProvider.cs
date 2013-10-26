using System;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;

namespace Xoriath.FileStimuli.Language
{
    [Export(typeof(ITaggerProvider))]
    [TagType(typeof(StimErrorTag))]
    [ContentType("stim")]
    public sealed class StimTaggerProvider : ITaggerProvider
    {
        public ITagger<T> CreateTagger<T>(ITextBuffer buffer) where T : ITag
        {
            Func<ITagger<T>> taggerFunc = () =>
                new StimErrorTagger(buffer) as ITagger<T>;
            return buffer.Properties.GetOrCreateSingletonProperty<ITagger<T>>(taggerFunc);
        }
    }
}
