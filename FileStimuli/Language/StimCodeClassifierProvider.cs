using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text;

namespace Xoriath.FileStimuli.Language
{
    [Export(typeof(IClassifierProvider))]
    [ContentType("stim")]
    internal class StimCodeClassifierProvider : IClassifierProvider
    {
        [Import]
        internal IClassificationTypeRegistryService mClassificationRegistryService = null;

        public IClassifier GetClassifier(ITextBuffer buffer)
        {
            Func<IClassifier> classifierFunc = () =>
                new StimCodeClassifier(buffer, mClassificationRegistryService) as IClassifier;
            return buffer.Properties.GetOrCreateSingletonProperty<IClassifier>(classifierFunc);
        }
    }
}
