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
#pragma warning disable 0649
        [Export]
        [Name("stim")]
        [BaseDefinition("code")]
        internal static ContentTypeDefinition StimContentTypeDefinition;
#pragma warning restore 0649
#pragma warning disable 0649
        [Export]
        [ContentType("stim")]
        [FileExtension(".stim")]
        internal static FileExtensionToContentTypeDefinition StimContentTypeDefinitionFileExtension;
#pragma warning restore 0649
    }
}
