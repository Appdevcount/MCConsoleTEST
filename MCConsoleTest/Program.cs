using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MCConsoleTest
{
    class Program
    {




        static string test1 ="1st push"+ "Change from Clone again"+
                            //"While the string object provides the LastIndexOf() method, which could be used to locate the last space character, I manually coded the loop myself so that I could use Char.IsWhiteSpace() to support all whitespace characters defined on the current system. If no whitespace is found, the line is simply broken at the maximum line length.\n" +
                            //"As each line is broken, that the code removes any spaces at the break. This avoids trailing spaces";
                            "Word wordword word word wo word words word Word wordword word word wo Word wordword word wordword word wordword word Tahtamouni1 Tahtamouni111 word\n" +
                "Provides low-level control for drawing text in Windows Presentation Foundation (WPF) applications\n" +
                "word wo word words word Word wordword word\n" +
                "Word wordword word word wo word words word Word wordword word word wo Word wordword word wordword word wordword word Tahtamouni2 word Word wordword word word wo word words word Word wordword word word wo Word wordword word wordword word Mohammed1 word wordword word\n" +
                "Word wordword word word wo word words word Word wordword word word wo Word wordword word wordword word wordword word Tahtamouni3 word Word wordword word word wo word words word Word wordword word word wo Word wordword word wordword word Mohammed2 word wordword word\n" +
                "Word wordword word word wo word words word Word wordword word word wo Word wordword word wordword word wordword word Tahtamouni4 word Word wordword word word wo word words word Word wordword word word wo Word wordword word wordword word Mohammed3 word wordword word\n" +
                "word wo word words word Word wordword word\n" +
                "\n\n\n" +
                "word wo word words word Word wordword word\n" +
                "word wo word words word Word wordword word\n" +
                "word wo word words word Word wordword word\n" +
                "Word wordword word word wo word words word Word wordword word word wo Word wordword word wordword word wordword word Tahtamouni5 word Word wordword word word wo word words word Word wordword word word wo Word wordword word wordword word Mohammed4 word wordword word";


        static string test2 = "إن الادارة العامة للجمارك هي أقدم جهة حكومية بدولة الكويت والتي أمر بانشاءها المغفور له باذن الله الشيخ مبارك الكبير حاكم الكويت السابع عام ١٨٩٩م ليرسخ من خلالها في ذالك الوقت مبادىء وسيادة اقتصادية وسياسية وامنية على الحدود الكويتية . ومن هذا المنطلق وعبر الاجيال المتلاحقة علينا ان نعمل وفق منظومة متقدمة وبات لزاما علينا ان نعرف إن تحسين و تطوير أداء المؤسسات الحكوميه بالكويت لم يعد أمراً اختياريا";



        static void Main(string[] args)
        {
            Program p = new Program();
            p.reflectiontest1();

            GenUtilities GU = new GenUtilities();
            //string HTMLFromFile = System.IO.File.ReadAllText(@"C:\Users\sn.ruknudeen\source\repos\TEST\Sample Bad html.txt");
            //string HTMLFromFile = System.IO.File.ReadAllText(@"C:\Users\sn.ruknudeen\Downloads\SampleTESTHTMLFiles\Sample Bad html.TXT");
            //string HTMLFromFile = System.IO.File.ReadAllText(@"C:\Users\sn.ruknudeen\Downloads\SampleTESTHTMLFiles\content.TXT");
            //string HTMLFromFile = System.IO.File.ReadAllText(@"C:\Users\sn.ruknudeen\Downloads\SampleTESTHTMLFiles\SIMPLEWSS.TXT");
            string HTMLFromFile = System.IO.File.ReadAllText(@"C:\Users\sn.ruknudeen\Downloads\SampleTESTHTMLFiles\SIMPLEWS.TXT");
            string dirtyHTML = "This is my textarea to be replaced with CKEditor.< script src = \"../js/jquery-1.7.1.js\" />";
            string TextFromHTML1 = GU.HTMLtoText(HTMLFromFile);

            dirtyHTML = "<h1>Heading</h1>" +
                            "<p onclick=\"alert('gotcha!')\">Some comments<span></span></p> " +
                            "<script type='text/javascript'>Illegal script()</script> " +
                            "<p><a href='http://www.google.com/'>Nofollow legal link</a> and here's another one:" +
                            "<a href=\"javascript:alert('test')\">Obviously I'm illegal</a></p>";



            dirtyHTML = "This is my textarea to be replaced with CKEditor.<script src = \"../js/jquery-1.7.1.js\" />";
            //var result = NUglify.Uglify.HtmlToText(dirtyHTML);
            //string TextFromHTML2 = result.Code;


            //var resultfromfile = NUglify.Uglify.HtmlToText(HTMLFromFile);
            //string resultcodefromfile = resultfromfile.Code;


            FastHtmlTextExtractor htmlContent = new FastHtmlTextExtractor();
            dirtyHTML = "<a id = \"shellmenu_56\" class=\"js-subm-uhf-nav-link\" href=\"https://www.microsoft.com/microsoft-hololens/en-us\" title=\"Products_DevicesAndXbox_MicrosoftHololens\">Microsoft HoloLens</a></li></ul></li><li class=\"f-sub-menu js-nav-menu\" ms.cmpnm=\"Products_ForBusiness\"><button id = \"Forbusiness-navigation\" aria-expanded=\"false\" ms.title=\"Products_ForBusiness\" ms.interactiontype=\"14\">For business</button><ul aria-labelledby= \"Forbusiness-navigation\" aria-hidden= \"true\" >< li ms.cmpnm= \"Products_ForBusiness_CloudPlatform\" >< a id= \"shellmenu_58\" class=\"js-subm-uhf-nav-link\" href=\"https://www.microsoft.com/en-us/server-cloud/\" ms.title=\"Products_ForBusiness_CloudPlatform\">Cloud Platform</a></li><li ms.cmpnm= \"Products_ForBusiness_MicrosoftAzure\" >< a id= \"shellmenu_59\" class=\"js-subm-uhf-nav-link\" href=\"https://azure.microsoft.com/\" ms.title=\"Products_ForBusiness_MicrosoftAzure\">Microsoft Azure</a></li><li ms.cmpnm= \"Products_ForBusiness_MicrosoftDynamics\" >< a id= \"shellmenu_60\" class=\"js-subm-uhf-nav-link\" href=\"https://www.microsoft.com/en-us/dynamics365/home\" ms.title=\"Products_ForBusiness_MicrosoftDynamics\">Microsoft Dynamics 365</a></li><li ms.cmpnm=\"Products_ForBusiness_WindowsForBusiness\"><a id = \"shellmenu_61\" class=\"js-subm-uhf-nav-link\" href=\"https://www.microsoft.com/en-us/windowsforbusiness \" ms.title=\"Products_ForBusiness_WindowsForBusiness\">Windows for business</a></li><li ms.cmpnm=\"Products_ForBusiness_OfficeForBusiness\"><a id = \"shellmenu_62\" class=\"js-subm-uhf-nav-link\" href=\"https://products.office.com/en-us/business/office\" ms.title=\"Products_ForBusiness_OfficeForBusiness\">Office for business</a></li><li ms.cmpnm=\"Products_ForBusiness_SkypeForBusiness\"><a id = \"shellmenu_63\" class=\"js-subm-uhf-nav-link\" href=\"https://products.office.com/en-us/skype-for-business\" ms.title=\"Products_ForBusiness_SkypeForBusiness\">Skype for business</a></li><li ms.cmpnm=\"Products_ForBusiness_SurfaceForBusiness\"><a id = \"shellmenu_64\" class=\"js-subm-uhf-nav-link\" href=\"https://www.microsoft.com/surface/en-us/business/overview\" ms.title=\"Products_ForBusiness_SurfaceForBusiness\">Surface for business</a></li><li ms.cmpnm=\"Products_ForBusiness_EnterpriseSolutions\"><a id = \"shellmenu_65\" class=\"js-subm-uhf-nav-link\" href=\"https://enterprise.microsoft.com/en-us/\" ms.title=\"Products_ForBusiness_EnterpriseSolutions\">Enterprise solutions</a></li><li ms.cmpnm= \"Products_ForBusiness_DataPlatform\" >< a id= \"shellmenu_66\" class=\"js-subm-uhf-nav-link\" href=\"https://www.microsoft.com/en-us/sql-server/ \" ms.title=\"Products_ForBusiness_DataPlatform\">Data platform</a></li>";
            string r1 = htmlContent.Extract(dirtyHTML.ToArray());
            string r2 = htmlContent.Extract(HTMLFromFile.ToArray());

            //using HtmlAgilityPack --READING HEAD TITLE AS WELL
            HtmlToText HT = new HtmlToText();
            string Ap1 = HT.Convert(dirtyHTML);
            string Ap2 = HT.Convert(HTMLFromFile);

            HtmlToTextS HTs = new HtmlToTextS();
            string Aps1 = HTs.Convert(dirtyHTML);
            string Aps2 = HTs.Convert(HTMLFromFile);

            //using microsoft.mshtml -- NEWLINE FOR EACH ELEMENT TEXT
            HTMLtoTxt HTt = new HTMLtoTxt();
            string Apsh1 = HTt.HTT(dirtyHTML);
            string Apsh2 = HTt.HTT(HTMLFromFile);


            //return;

            string Filtered = dirtyHTML.FilterHtmlToWhitelist();


            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex(@"\<\!\[CDATA\[(?<text>[^\]]*)\]\]\>", options);
            string input = @"TEST <![CDATA[<table><tr><td>Approved</td></tr></table>]]>";

            // Check for match
            bool isMatch = regex.IsMatch(input);

            if (isMatch)
            {
                Match match = regex.Match(input);
                string CDataContents = match.Groups["text"].Value;
                string completehtml = input.Replace(CDataContents, "");
            }




            string Contenttext = test1 + test2;

            // WrapText -Takes raw comments from DB and  Returns list of word-wrapped-lines having max of 121 characters per line (equivalent to 565 pixels). 
            List<string> wrappedLines = GU.WrapText(Contenttext, 565, "Calibri", 11);

            // ContentFormatter - Takes word-wrapped-lines and Returns it in DataTable along with line number , page number and page break flag on each line .  
            DataTable DT = GU.ContentFormatter(wrappedLines, 12);
            // ContentFormatter - Takes word-wrapped-lines and Returns it by One row per page content in DataTable along with lines per page and page number  .  
            DataTable DTbyPage = GU.ContentFormatterbyPage(wrappedLines, 12);


            Console.ReadLine();
        }
        public  void reflectiontest1()
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            Console.WriteLine(methodBase.Name); // Displays "WhatsmyName"
            try
            {
                throw new Exception();
            }
            catch
            {
                System.Diagnostics.StackFrame stackFrameex = new System.Diagnostics.StackFrame();
                System.Reflection.MethodBase methodBaseex = stackFrame.GetMethod();
                Console.WriteLine(methodBaseex.Name); // Displays "WhatsmyName"
            }
            try
            {

                Program pe = new Program();
                pe.reflectiontest2();//
            }
            catch
            {
                System.Diagnostics.StackFrame stackFrameex = new System.Diagnostics.StackFrame();
                System.Reflection.MethodBase methodBaseex = stackFrame.GetMethod();
                Console.WriteLine(methodBaseex.Name); // Displays "WhatsmyName"

                StackTrace stackTracep = new StackTrace();
                StackFrame stackFramep = stackTracep.GetFrame(1);
                MethodBase methodBasep = stackFrame.GetMethod();
                // Displays "WhatsmyName"
                Console.WriteLine(" Parent Method Name {0} ", methodBasep.Name);
            }
            
        }
        public  void reflectiontest2()
        {
            throw new Exception();

            StackTrace stackTrace = new StackTrace();
            StackFrame stackFrame = stackTrace.GetFrame(1);
            MethodBase methodBase = stackFrame.GetMethod();
            // Displays "WhatsmyName"
            Console.WriteLine(" Parent Method Name {0} ", methodBase.Name);
        }
    }
    public static class HtmlSanitizeExtension
    {
        private const string HTML_TAG_PATTERN = @"(?'tag_start'</?)(?'tag'\w+)((\s+(?'attr'(?'attr_name'\w+)(\s*=\s*(?:"".*?""|'.*?'|[^'"">\s]+)))?)+\s*|\s*)(?'tag_end'/?>)";

        /// <summary>
        /// A dictionary of allowed tags and their respectived allowed attributes.  If no
        /// attributes are provided, all attributes will be stripped from the allowed tag
        /// </summary>
        public static Dictionary<string, List<string>> ValidHtmlTags = new Dictionary<string, List<string>> {
            { "p", new List<string>() },
            { "strong", new List<string>() },
            { "ul", new List<string>() },
            { "li", new List<string>() },
            { "a", new List<string> { "href", "target" } }
    };

        /// <summary>
        /// Extension filters your HTML to the whitelist specified in the ValidHtmlTags dictionary
        /// </summary>
        public static string FilterHtmlToWhitelist(this string text)
        {
            Regex htmlTagExpression = new Regex(HTML_TAG_PATTERN, RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.Compiled);

            return htmlTagExpression.Replace(text, m =>
            {
                if (!ValidHtmlTags.ContainsKey(m.Groups["tag"].Value))
                    return String.Empty;

                StringBuilder generatedTag = new StringBuilder(m.Length);

                Group tagStart = m.Groups["tag_start"];
                Group tagEnd = m.Groups["tag_end"];
                Group tag = m.Groups["tag"];
                Group tagAttributes = m.Groups["attr"];

                generatedTag.Append(tagStart.Success ? tagStart.Value : "<");
                generatedTag.Append(tag.Value);

                foreach (Capture attr in tagAttributes.Captures)
                {
                    int indexOfEquals = attr.Value.IndexOf('=');

                    // don't proceed any futurer if there is no equal sign or just an equal sign
                    if (indexOfEquals < 1)
                        continue;

                    string attrName = attr.Value.Substring(0, indexOfEquals);

                    // check to see if the attribute name is allowed and write attribute if it is
                    if (ValidHtmlTags[tag.Value].Contains(attrName))
                    {
                        generatedTag.Append(' ');
                        generatedTag.Append(attr.Value);
                    }
                }

                generatedTag.Append(tagEnd.Success ? tagEnd.Value : ">");

                return generatedTag.ToString();
            });
        }

       
    }
}
