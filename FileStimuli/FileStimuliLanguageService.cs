using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Package;
using Microsoft.VisualStudio.TextManager.Interop;

namespace MortenEngelhardtOlsen.FileStimuli
{
    class FileStimuliLanguageService : LanguageService
    {
        private LanguagePreferences m_preferences;
        private FileStimuliScanner m_scanner;

        public override string Name
        {
            get { return "Stimuli File Language"; }
        }

        public override string GetFormatFilterList()
        {
            return "*.stim";
        }

        public override LanguagePreferences GetLanguagePreferences()
        {
            if (m_preferences == null)
            {
                m_preferences = new LanguagePreferences(this.Site,
                                                        typeof(FileStimuliLanguageService).GUID,
                                                        this.Name);
                m_preferences.Init();
            }
            return m_preferences;
        }

        public override IScanner GetScanner(IVsTextLines buffer)
        {
            if (m_scanner == null)
                m_scanner = new FileStimuliScanner(buffer);
            return m_scanner;
        }

        public override AuthoringScope ParseSource(ParseRequest req)
        {
            return new FileStimuliAuthoringScope();
        }
    }

    internal class FileStimuliAuthoringScope : AuthoringScope
    {
        public override string GetDataTipText(int line, int col, out TextSpan span)
        {
            span = new TextSpan();
            return null;
        }

        public override Declarations GetDeclarations(IVsTextView view, int line, int col, TokenInfo info, ParseReason reason)
        {
            return null;
        }

        public override string Goto(Microsoft.VisualStudio.VSConstants.VSStd97CmdID cmd, IVsTextView textView, int line, int col, out TextSpan span)
        {
            span = new TextSpan();
            return null;
        }

        public override Methods GetMethods(int line, int col, string name)
        {
            return null;
        }
    }

    internal class FileStimuliScanner : IScanner
    {
        private IVsTextBuffer m_buffer;
        string m_source;

        public FileStimuliScanner(IVsTextBuffer buffer)
        {
            m_buffer = buffer;
        }

        public bool ScanTokenAndProvideInfoAboutIt(TokenInfo tokenInfo, ref int state)
        {
            tokenInfo.Type = TokenType.Unknown;
            tokenInfo.Color = TokenColor.Text;
            return true;
        }

        public void SetSource(string source, int offset)
        {
            m_source = source.Substring(offset);
        }
    }
}
