using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;

namespace Xoriath.FileStimuli.Language
{
    internal static class StimContentDefinition
    {
        [Export]
        [Name("stim")]
        [BaseDefinition("code")]
        internal static ContentTypeDefinition StimContentTypeDefinition;

        [Export]
        [ContentType("stim")]
        [FileExtension(".stim")]
        internal static FileExtensionToContentTypeDefinition StimContentTypeDefinitionFileExtension;
    }
}
