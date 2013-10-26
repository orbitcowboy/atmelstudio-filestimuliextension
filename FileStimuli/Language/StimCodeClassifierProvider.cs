using System;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Xoriath.FileStimuli.Language
{
    [Export(typeof(IClassifierProvider))]
    [ContentType("stim")]
    internal class StimCodeClassifierProvider : IClassifierProvider
    {
        [Import]
        internal IClassificationTypeRegistryService mClassificationRegistryService { get; set; }

        public IClassifier GetClassifier(ITextBuffer buffer)
        {
            Func<IClassifier> classifierFunc = () =>
                new StimCodeClassifier(buffer, mClassificationRegistryService) as IClassifier;
            return buffer.Properties.GetOrCreateSingletonProperty<IClassifier>(classifierFunc);
        }
    }
}
