using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MCConsoleTest
{
    /// <summary>
    /// The fast HTML text extractor class is designed to, as quickly and as ignorantly as possible,
    /// extract text data from a given HTML character array. The class searches for and deletes
    /// script and style tags in a first and second pass, with an optional third pass to do the same
    /// to HTML comments, and then copies remaining non-whitespace character data to an ouput array.
    /// All whitespace encountered is replaced with a single whitespace in to avoid multiple
    /// whitespace in the output.
    ///
    /// Note that the returned text content still may have named character and numbered character
    /// references within that, when decoded, may produce multiple whitespace.
    /// </summary>
    public class FastHtmlTextExtractor
    {

        private readonly char[] SCRIPT_OPEN_TAG = new char[7] { '<', 's', 'c', 'r', 'i', 'p', 't' };
        private readonly char[] SCRIPT_CLOSE_TAG = new char[9] { '<', '/', 's', 'c', 'r', 'i', 'p', 't', '>' };

        private readonly char[] STYLE_OPEN_TAG = new char[6] { '<', 's', 't', 'y', 'l', 'e' };
        private readonly char[] STYLE_CLOSE_TAG = new char[8] { '<', '/', 's', 't', 'y', 'l', 'e', '>' };

        private readonly char[] COMMENT_OPEN_TAG = new char[3] { '<', '!', '-' };
        private readonly char[] COMMENT_CLOSE_TAG = new char[3] { '-', '-', '>' };

        private int[] m_deletionDictionary;

        public string Extract(char[] input, bool stripComments = false)
        {
            var len = input.Length;
            int next = 0;

            m_deletionDictionary = new int[len];

            // Whipe out all text content between style and script tags.
            FindAndWipe(SCRIPT_OPEN_TAG, SCRIPT_CLOSE_TAG, input);
            FindAndWipe(STYLE_OPEN_TAG, STYLE_CLOSE_TAG, input);

            if (stripComments)
            {
                // Whipe out everything between HTML comments.
                FindAndWipe(COMMENT_OPEN_TAG, COMMENT_CLOSE_TAG, input);
            }

            // Whipe text between all other tags now.
            while (next < len)
            {
                next = SkipUntil(next, '<', input);

                if (next < len)
                {
                    var closeNext = SkipUntil(next, '>', input);

                    if (closeNext < len)
                    {
                        m_deletionDictionary[next] = (closeNext + 1) - next;
                        WipeRange(next, closeNext + 1, input);
                    }

                    next = closeNext + 1;
                }
            }

            // Collect all non-whitespace and non-null chars into a new
            // char array. All whitespace characters are skipped and replaced
            // with a single space char. Multiple whitespace is ignored.
            var lastSpace = true;
            var extractedPos = 0;
            var extracted = new char[len];

            for (next = 0; next < len; ++next)
            {
                if (m_deletionDictionary[next] > 0)
                {
                    next += m_deletionDictionary[next];
                    continue;
                }

                if (char.IsWhiteSpace(input[next]) || input[next] == '\0')
                {
                    if (lastSpace)
                    {
                        continue;
                    }

                    extracted[extractedPos++] = ' ';
                    lastSpace = true;
                }
                else
                {
                    lastSpace = false;
                    extracted[extractedPos++] = input[next];
                }
            }

            return new string(extracted, 0, extractedPos);
        }

        /// <summary>
        /// Does a search in the input array for the characters in the supplied open and closing tag
        /// char arrays. Each match where both tag open and tag close are discovered causes the text
        /// in between the matches to be overwritten by Array.Clear().
        /// </summary>
        /// <param name="openingTag">
        /// The opening tag to search for.
        /// </param>
        /// <param name="closingTag">
        /// The closing tag to search for.
        /// </param>
        /// <param name="input">
        /// The input to search in.
        /// </param>
        private void FindAndWipe(char[] openingTag, char[] closingTag, char[] input)
        {
            int len = input.Length;
            int pos = 0;

            do
            {
                pos = FindNext(pos, openingTag, input);

                if (pos < len)
                {
                    var closenext = FindNext(pos, closingTag, input);

                    if (closenext < len)
                    {
                        m_deletionDictionary[pos - openingTag.Length] = closenext - (pos - openingTag.Length);
                        WipeRange(pos - openingTag.Length, closenext, input);
                    }

                    if (closenext > pos)
                    {
                        pos = closenext;
                    }
                    else
                    {
                        ++pos;
                    }
                }
            }
            while (pos < len);
        }

        /// <summary>
        /// Skips as many characters as possible within the input array until the given char is
        /// found. The position of the first instance of the char is returned, or if not found, a
        /// position beyond the end of the input array is returned.
        /// </summary>
        /// <param name="pos">
        /// The starting position to search from within the input array.
        /// </param>
        /// <param name="c">
        /// The character to find.
        /// </param>
        /// <param name="input">
        /// The input to search within.
        /// </param>
        /// <returns>
        /// The position of the found character, or an index beyond the end of the input array.
        /// </returns>
        private int SkipUntil(int pos, char c, char[] input)
        {
            if (pos >= input.Length)
            {
                return pos;
            }

            do
            {
                if (input[pos] == c)
                {
                    return pos;
                }

                ++pos;
            }
            while (pos < input.Length);

            return pos;
        }

        /// <summary>
        /// Clears a given range in the input array.
        /// </summary>
        /// <param name="start">
        /// The start position from which the array will begin to be cleared.
        /// </param>
        /// <param name="end">
        /// The end position in the array, the position to clear up-until.
        /// </param>
        /// <param name="input">
        /// The source array wherin the supplied range will be cleared.
        /// </param>
        /// <remarks>
        /// Note that the second parameter is called end, not lenghth. This parameter is meant to be
        /// a position in the array, not the amount of entries in the array to clear.
        /// </remarks>
        private void WipeRange(int start, int end, char[] input)
        {
            Array.Clear(input, start, end - start);
        }

        /// <summary>
        /// Finds the next occurance of the supplied char array within the input array. This search
        /// ignores whitespace.
        /// </summary>
        /// <param name="pos">
        /// The position to start searching from.
        /// </param>
        /// <param name="what">
        /// The sequence of characters to find.
        /// </param>
        /// <param name="input">
        /// The input array to perform the search on.
        /// </param>
        /// <returns>
        /// The position of the end of the first matching occurance. That is, the returned position
        /// points to the very end of the search criteria within the input array, not the start. If
        /// no match could be found, a position beyond the end of the input array will be returned.
        /// </returns>
        public int FindNext(int pos, char[] what, char[] input)
        {
            do
            {
                if (Next(ref pos, what, input))
                {
                    return pos;
                }
                ++pos;
            }
            while (pos < input.Length);

            return pos;
        }

        /// <summary>
        /// Probes the input array at the given position to determine if the next N characters
        /// matches the supplied character sequence. This check ignores whitespace.
        /// </summary>
        /// <param name="pos">
        /// The position at which to check within the input array for a match to the supplied
        /// character sequence.
        /// </param>
        /// <param name="what">
        /// The character sequence to attempt to match. Note that whitespace between characters
        /// within the input array is accebtale.
        /// </param>
        /// <param name="input">
        /// The input array to check within.
        /// </param>
        /// <returns>
        /// True if the next N characters within the input array matches the supplied search
        /// character sequence. Returns false otherwise.
        /// </returns>
        public bool Next(ref int pos, char[] what, char[] input)
        {
            int z = 0;

            do
            {
                if (char.IsWhiteSpace(input[pos]) || input[pos] == '\0')
                {
                    ++pos;
                    continue;
                }

                if (input[pos] == what[z])
                {
                    ++z;
                    ++pos;
                    continue;
                }

                return false;
            }
            while (pos < input.Length && z < what.Length);

            return z == what.Length;
        }
    }

    //using HtmlAgilityPack
    public class HtmlToText
    {
        #region Public Methods

        public string Convert(string path)
        {
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            string FullHTML= path;
            TextReader tr = new StringReader(FullHTML);
           
            doc.Load(tr);

            StringWriter sw = new StringWriter();
            ConvertTo(doc.DocumentNode, sw);
            sw.Flush();
            return sw.ToString();
        }

        public string ConvertHtml(string html)
        {
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);

            StringWriter sw = new StringWriter();
            ConvertTo(doc.DocumentNode, sw);
            sw.Flush();
            return sw.ToString();
        }

        public void ConvertTo(HtmlAgilityPack.HtmlNode node, TextWriter outText)
        {
            string html;
            switch (node.NodeType)
            {
                case HtmlAgilityPack.HtmlNodeType.Comment:
                    // don't output comments
                    break;

                case HtmlAgilityPack.HtmlNodeType.Document:
                    ConvertContentTo(node, outText);
                    break;

                case HtmlAgilityPack.HtmlNodeType.Text:
                    // script and style must not be output
                    string parentName = node.ParentNode.Name;
                    if ((parentName == "script") || (parentName == "style"))
                        break;

                    // get text
                    html = ((HtmlAgilityPack.HtmlTextNode)node).Text;

                    // is it in fact a special closing node output as text?
                    if (HtmlAgilityPack.HtmlNode.IsOverlappedClosingElement(html))
                        break;

                    // check the text is meaningful and not a bunch of whitespaces
                    if (html.Trim().Length > 0)
                    {
                        outText.Write(HtmlAgilityPack.HtmlEntity.DeEntitize(html));
                    }
                    break;

                case HtmlAgilityPack.HtmlNodeType.Element:
                    switch (node.Name)
                    {
                        case "p":
                            // treat paragraphs as crlf
                            outText.Write("\r\n");
                            break;
                    }

                    if (node.HasChildNodes)
                    {
                        ConvertContentTo(node, outText);
                    }
                    break;
            }
        }

        #endregion

        #region Private Methods

        private void ConvertContentTo(HtmlAgilityPack.HtmlNode node, TextWriter outText)
        {
            foreach (HtmlAgilityPack.HtmlNode subnode in node.ChildNodes)
            {
                ConvertTo(subnode, outText);
            }
        }

        #endregion
    }

    /// <summary>
    /// Converts HTML to plain text.//Custom
    /// </summary>
    public class HtmlToTextS
    {
        // Static data tables
        protected static Dictionary<string, string> _tags;
        protected static HashSet<string> _ignoreTags;

        // Instance variables
        protected TextBuilder _text;
        protected string _html;
        protected int _pos;

        // Static constructor (one time only)
        static HtmlToTextS()
        {
            _tags = new Dictionary<string, string>();

            //_tags.Add("html", "\n");
            //_tags.Add("head", "\n");
            //_tags.Add("body", "\n");
            //_tags.Add("footer", "\n");
            //_tags.Add("title", "\n");
            //_tags.Add("link", "\n");
            //_tags.Add("meta", "\n");

            _tags.Add("address", "\n");
            _tags.Add("blockquote", "\n");
            _tags.Add("div", "\n");
            _tags.Add("dl", "\n");
            _tags.Add("fieldset", "\n");
            _tags.Add("form", "\n");
            _tags.Add("h1", "\n");
            _tags.Add("/h1", "\n");
            _tags.Add("h2", "\n");
            _tags.Add("/h2", "\n");
            _tags.Add("h3", "\n");
            _tags.Add("/h3", "\n");
            _tags.Add("h4", "\n");
            _tags.Add("/h4", "\n");
            _tags.Add("h5", "\n");
            _tags.Add("/h5", "\n");
            _tags.Add("h6", "\n");
            _tags.Add("/h6", "\n");
            _tags.Add("p", "\n");
            _tags.Add("/p", "\n");
            _tags.Add("table", "\n");
            _tags.Add("/table", "\n");
            _tags.Add("ul", "\n");
            _tags.Add("/ul", "\n");
            _tags.Add("ol", "\n");
            _tags.Add("/ol", "\n");
            _tags.Add("/li", "\n");
            _tags.Add("br", "\n");
            _tags.Add("/td", "\t");
            _tags.Add("/tr", "\n");
            _tags.Add("/pre", "\n");

            _ignoreTags = new HashSet<string>();
            _ignoreTags.Add("script");
            _ignoreTags.Add("noscript");
            _ignoreTags.Add("style");
            _ignoreTags.Add("object");
        }

        /// <summary>
        /// Converts the given HTML to plain text and returns the result.
        /// </summary>
        /// <param name="html">HTML to be converted</param>
        /// <returns>Resulting plain text</returns>
        public string Convert(string html)
        {
            // Initialize state variables
            _text = new TextBuilder();
            _html = html;
            _pos = 0;

            // Process input
            while (!EndOfText)
            {
                if (Peek() == '<')
                {
                    // HTML tag
                    bool selfClosing;
                    string tag = ParseTag(out selfClosing);

                    // Handle special tag cases
                    if (tag == "body")
                    {
                        // Discard content before <body>
                        _text.Clear();
                    }
                    else if (tag == "/body")
                    {
                        // Discard content after </body>
                        _pos = _html.Length;
                    }
                    else if (tag == "pre")
                    {
                        // Enter preformatted mode
                        _text.Preformatted = true;
                        EatWhitespaceToNextLine();
                    }
                    else if (tag == "/pre")
                    {
                        // Exit preformatted mode
                        _text.Preformatted = false;
                    }

                    string value;
                    if (_tags.TryGetValue(tag, out value))
                        _text.Write(value);

                    if (_ignoreTags.Contains(tag))
                        EatInnerContent(tag);
                }
                else if (Char.IsWhiteSpace(Peek()))
                {
                    // Whitespace (treat all as space)
                    _text.Write(_text.Preformatted ? Peek() : ' ');
                    MoveAhead();
                }
                else
                {
                    // Other text
                    _text.Write(Peek());
                    MoveAhead();
                }
            }
            // Return result
            return  HttpUtility.HtmlDecode(_text.ToString());
        }

        // Eats all characters that are part of the current tag
        // and returns information about that tag
        protected string ParseTag(out bool selfClosing)
        {
            string tag = String.Empty;
            selfClosing = false;

            if (Peek() == '<')
            {
                MoveAhead();

                // Parse tag name
                EatWhitespace();
                int start = _pos;
                if (Peek() == '/')
                    MoveAhead();
                while (!EndOfText && !Char.IsWhiteSpace(Peek()) &&
                    Peek() != '/' && Peek() != '>')
                    MoveAhead();
                tag = _html.Substring(start, _pos - start).ToLower();

                // Parse rest of tag
                while (!EndOfText && Peek() != '>')
                {
                    if (Peek() == '"' || Peek() == '\'')
                        EatQuotedValue();
                    else
                    {
                        if (Peek() == '/')
                            selfClosing = true;
                        MoveAhead();
                    }
                }
                MoveAhead();
            }
            return tag;
        }

        // Consumes inner content from the current tag
        protected void EatInnerContent(string tag)
        {
            string endTag = "/" + tag;

            while (!EndOfText)
            {
                if (Peek() == '<')
                {
                    // Consume a tag
                    bool selfClosing;
                    if (ParseTag(out selfClosing) == endTag)
                        return;
                    // Use recursion to consume nested tags
                    if (!selfClosing && !tag.StartsWith("/"))
                        EatInnerContent(tag);
                }
                else MoveAhead();
            }
        }

        // Returns true if the current position is at the end of
        // the string
        protected bool EndOfText
        {
            get { return (_pos >= _html.Length); }
        }

        // Safely returns the character at the current position
        protected char Peek()
        {
            return (_pos < _html.Length) ? _html[_pos] : (char)0;
        }

        // Safely advances to current position to the next character
        protected void MoveAhead()
        {
            _pos = Math.Min(_pos + 1, _html.Length);
        }

        // Moves the current position to the next non-whitespace
        // character.
        protected void EatWhitespace()
        {
            while (Char.IsWhiteSpace(Peek()))
                MoveAhead();
        }

        // Moves the current position to the next non-whitespace
        // character or the start of the next line, whichever
        // comes first
        protected void EatWhitespaceToNextLine()
        {
            while (Char.IsWhiteSpace(Peek()))
            {
                char c = Peek();
                MoveAhead();
                if (c == '\n')
                    break;
            }
        }

        // Moves the current position past a quoted value
        protected void EatQuotedValue()
        {
            char c = Peek();
            if (c == '"' || c == '\'')
            {
                // Opening quote
                MoveAhead();
                // Find end of value
                int start = _pos;
                _pos = _html.IndexOfAny(new char[] { c, '\r', '\n' }, _pos);
                if (_pos < 0)
                    _pos = _html.Length;
                else
                    MoveAhead();    // Closing quote
            }
        }

        /// <summary>
        /// A StringBuilder class that helps eliminate excess whitespace.
        /// </summary>
        protected class TextBuilder
        {
            private StringBuilder _text;
            private StringBuilder _currLine;
            private int _emptyLines;
            private bool _preformatted;

            // Construction
            public TextBuilder()
            {
                _text = new StringBuilder();
                _currLine = new StringBuilder();
                _emptyLines = 0;
                _preformatted = false;
            }

            /// <summary>
            /// Normally, extra whitespace characters are discarded.
            /// If this property is set to true, they are passed
            /// through unchanged.
            /// </summary>
            public bool Preformatted
            {
                get
                {
                    return _preformatted;
                }
                set
                {
                    if (value)
                    {
                        // Clear line buffer if changing to
                        // preformatted mode
                        if (_currLine.Length > 0)
                            FlushCurrLine();
                        _emptyLines = 0;
                    }
                    _preformatted = value;
                }
            }

            /// <summary>
            /// Clears all current text.
            /// </summary>
            public void Clear()
            {
                _text.Length = 0;
                _currLine.Length = 0;
                _emptyLines = 0;
            }

            /// <summary>
            /// Writes the given string to the output buffer.
            /// </summary>
            /// <param name="s"></param>
            public void Write(string s)
            {
                foreach (char c in s)
                    Write(c);
            }

            /// <summary>
            /// Writes the given character to the output buffer.
            /// </summary>
            /// <param name="c">Character to write</param>
            public void Write(char c)
            {
                if (_preformatted)
                {
                    // Write preformatted character
                    _text.Append(c);
                }
                else
                {
                    if (c == '\r')
                    {
                        // Ignore carriage returns. We'll process
                        // '\n' if it comes next
                    }
                    else if (c == '\n')
                    {
                        // Flush current line
                        FlushCurrLine();
                    }
                    else if (Char.IsWhiteSpace(c))
                    {
                        // Write single space character
                        int len = _currLine.Length;
                        if (len == 0 || !Char.IsWhiteSpace(_currLine[len - 1]))
                            _currLine.Append(' ');
                    }
                    else
                    {
                        // Add character to current line
                        _currLine.Append(c);
                    }
                }
            }

            // Appends the current line to output buffer
            protected void FlushCurrLine()
            {
                // Get current line
                string line = _currLine.ToString().Trim();

                // Determine if line contains non-space characters
                string tmp = line.Replace("&nbsp;", String.Empty);
                if (tmp.Length == 0)
                {
                    // An empty line
                    _emptyLines++;
                    if (_emptyLines < 2 && _text.Length > 0)
                        _text.AppendLine(line);
                }
                else
                {
                    // A non-empty line
                    _emptyLines = 0;
                    _text.AppendLine(line);
                }

                // Reset current line
                _currLine.Length = 0;
            }

            /// <summary>
            /// Returns the current output as a string.
            /// </summary>
            public override string ToString()
            {
                if (_currLine.Length > 0)
                    FlushCurrLine();
                return _text.ToString();
            }
        }
    }

    //using microsoft.mshtml
    public class HTMLtoTxt
    {
        public string HTT(string HTML)
        {
            //using microsoft.mshtml
            mshtml.HTMLDocument htmldoc = new mshtml.HTMLDocument();
            mshtml.IHTMLDocument2 htmldoc2 = (mshtml.IHTMLDocument2)htmldoc;
            htmldoc2.write(new object[] { HTML });

            string txt = htmldoc2.body.outerText;
            return txt;
        }
    }
}
