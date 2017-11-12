using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Media;
using NUglify.Html;
using NUglify;

namespace MCConsoleTest
{
    public class GenUtilities
    {
        public string HTMLtoText(string HTML)
        {
            var result = NUglify.Uglify.HtmlToText(HTML);
            return result.Code;
        }

        public DataTable ContentFormatter(List<string> wrappedLines, int pageSize)
        {
            int LineNo = 0;

            DataTable DT = new DataTable();
            DT.Columns.Add("Content", typeof(string));
            DT.Columns.Add("LineNo", typeof(string));
            DT.Columns.Add("PageNo", typeof(string));
            DT.Columns.Add("PageBreak", typeof(bool));

            if (wrappedLines != null)
            {
                foreach (string ActualLine in wrappedLines)
                {
                    ++LineNo;
                    int FindingMod = (LineNo % pageSize == 0) ? -1 : 0;
                    DT.Rows.Add(ActualLine, LineNo, Convert.ToInt16(Convert.ToInt16(LineNo / pageSize) + 1 + FindingMod), (LineNo % pageSize == 0));
                }
            }
            return DT;
        }

        public DataTable ContentFormatterbyPage(List<string> wrappedLines, int pageSize)
        {
            int indexNoofList = 0;
            int LinesperPage = 0;

            DataTable DT = new DataTable();
            DT.Columns.Add("Content", typeof(string));
            DT.Columns.Add("Lines", typeof(string));
            DT.Columns.Add("PageNo", typeof(string));

            string stringbyPage = "";

            if (wrappedLines != null)
            {
                foreach (string ActualLine in wrappedLines)
                {
                    ++indexNoofList;
                    int FindingMod = (indexNoofList % pageSize == 0) ? 0 : 1;
                    string LineBreak = (wrappedLines.Count != indexNoofList ? "\r\n" : "");
                    stringbyPage = stringbyPage + ActualLine + LineBreak;
                    ++LinesperPage;
                    if (indexNoofList % pageSize == 0 | indexNoofList == wrappedLines.Count)
                    {
                        DT.Rows.Add(stringbyPage, LinesperPage, Convert.ToInt16(Convert.ToInt16(indexNoofList / pageSize) +  FindingMod));
                        LinesperPage = 0;
                        stringbyPage = "";
                    }
                }
            }
            return DT;
        }

        public List<string> WrapText(string text, double pixels, string fontFamily,
         float emSize)
        {
            string[] originalParas = text.Split(new string[] { "\n" },
                StringSplitOptions.None);
            List<string> wrappedLines = new List<string>();

            foreach (var para in originalParas)
            {
                string[] wordsinPara = para.Split(new string[] { " " },
                    StringSplitOptions.None);
                StringBuilder actualLine = new StringBuilder();
                double actualWidth = 0;

                foreach (var item in wordsinPara)
                {
                    FormattedText formatted = new FormattedText(item,
                        CultureInfo.CurrentCulture,
                        System.Windows.FlowDirection.LeftToRight,
                        new Typeface(fontFamily), emSize, Brushes.Black);

                    actualLine.Append(item + " ");
                    actualWidth += formatted.Width;

                    if (actualWidth > pixels)
                    {
                        wrappedLines.Add(actualLine.ToString());
                        actualLine.Clear();
                        actualWidth = 0;
                    }
                }
                if (actualLine.Length > 0)
                    wrappedLines.Add(actualLine.ToString());
            }
            return wrappedLines;
        }
        //public List<string> NativeBreak(string text)
        //{
        //    string[] originalParas = text.Split(new string[] { "\n" },
        //        StringSplitOptions.None);
        //    List<string> wrappedLines = new List<string>();

            
        //    foreach (var para in originalParas)
        //    {
        //        int paraCharLength = para.Length;
        //        int ApproxLinesinPara = (paraCharLength % 124 == 0) ? (paraCharLength / 124) : ((paraCharLength / 124) + 1);

        //    }
        //    foreach (var para in originalParas)
        //    {
        //        int paraCharLength = para.Length;
        //        int ApproxLinesinPara = (paraCharLength % 124 == 0) ? (paraCharLength / 124) : ((paraCharLength / 124) + 1);

        //        string[] originalLines = para.Split(new string[] { " " },
        //            StringSplitOptions.None);


        //        StringBuilder actualLine = new StringBuilder();
        //        double actualWidth = 0;

        //        foreach (var item in originalLines)
        //        {
        //            FormattedText formatted = new FormattedText(item,
        //                CultureInfo.CurrentCulture,
        //                System.Windows.FlowDirection.LeftToRight,
        //                new Typeface(fontFamily), emSize, Brushes.Black);

        //            actualLine.Append(item + " ");
        //            actualWidth += formatted.Width;

        //            if (actualWidth > pixels)
        //            {
        //                wrappedLines.Add(actualLine.ToString());
        //                actualLine.Clear();
        //                actualWidth = 0;
        //            }
        //        }

        //        if (actualLine.Length > 0)
        //            wrappedLines.Add(actualLine.ToString());
        //    }

        //    return wrappedLines;
        //}

    }
}
