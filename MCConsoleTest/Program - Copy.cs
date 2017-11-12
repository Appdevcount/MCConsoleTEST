//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Globalization;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Media;

//namespace MCConsoleTest
//{
//    class Program
//    {
//        static void Main1(string[] args)
//        {
//            //Console.WriteLine(Convert.ToInt16(4/12));
//            //Console.WriteLine(Convert.ToInt16(12/12));
//            //Console.WriteLine(Convert.ToInt16(18/12));
//            //Console.WriteLine(Convert.ToInt16(34/12));

    
//            for (int LineNo = 1; LineNo <= 50; LineNo++)
//            {
//                int FindingMod = (LineNo % 12 == 0) ? -1 : 0;
//                Console.WriteLine(LineNo +"    "+FindingMod + "    " + Convert.ToInt16(LineNo / 12) + "    "+ Convert.ToInt16( Convert.ToInt16(LineNo / 12) +1+  FindingMod));
//            }

//            GenUtilities GU = new GenUtilities();

//            //DataSet DS= GU.ContentFormatter("");
//        }


//        static void Main(string[] args)
//        {

//            GenUtilities GU = new GenUtilities();

//            string Contenttext =
//            //"While the string object provides the LastIndexOf() method, which could be used to locate the last space character, I manually coded the loop myself so that I could use Char.IsWhiteSpace() to support all whitespace characters defined on the current system. If no whitespace is found, the line is simply broken at the maximum line length.\n" +
//            //"As each line is broken, that the code removes any spaces at the break. This avoids trailing spaces";
//                        "Word wordword word word wo word words word Word wordword word word wo Word wordword word wordword word wordword word Tahtamouni1 Tahtamouni111 word\n" +
//            "Provides low-level control for drawing text in Windows Presentation Foundation (WPF) applications\n" +
//            "word wo word words word Word wordword word\n" +
//            "Word wordword word word wo word words word Word wordword word word wo Word wordword word wordword word wordword word Tahtamouni2 word Word wordword word word wo word words word Word wordword word word wo Word wordword word wordword word Mohammed1 word wordword word\n" +
//            "Word wordword word word wo word words word Word wordword word word wo Word wordword word wordword word wordword word Tahtamouni3 word Word wordword word word wo word words word Word wordword word word wo Word wordword word wordword word Mohammed2 word wordword word\n" +
//            "Word wordword word word wo word words word Word wordword word word wo Word wordword word wordword word wordword word Tahtamouni4 word Word wordword word word wo word words word Word wordword word word wo Word wordword word wordword word Mohammed3 word wordword word\n" +
//            "word wo word words word Word wordword word\n" +
//            "word wo word words word Word wordword word\n" +
//            "word wo word words word Word wordword word\n" +
//            "word wo word words word Word wordword word\n" +
//            "Word wordword word word wo word words word Word wordword word word wo Word wordword word wordword word wordword word Tahtamouni5 word Word wordword word word wo word words word Word wordword word word wo Word wordword word wordword word Mohammed4 word wordword word";

//            //Console.Write( WordWrap(Contenttext, 70));

//            //Contenttext = System.IO.File.ReadAllText(@"C:\Temp\Content.txt");


//            List<string> lines =GU.WrapText(Contenttext, 565, "Calibri", 11);

//            //foreach (var item in lines)
//            //{
//            //    Console.WriteLine(item);
//            //}

//            DataSet DS = GU.ContentFormatter(lines);

//            Console.ReadLine();
//        }

     

//        ////========
//        //public static string WordWrap(string text, int width)
//        //{
//        //    int pos, next;
//        //    StringBuilder sb = new StringBuilder();

//        //    // Lucidity check
//        //    if (width < 1)
//        //        return text;

//        //    // Parse each line of text
//        //    for (pos = 0; pos < text.Length; pos = next)
//        //    {
//        //        // Find end of line
//        //        int eol = text.IndexOf(Environment.NewLine, pos);
//        //        if (eol == -1)
//        //            next = eol = text.Length;
//        //        else
//        //            next = eol + Environment.NewLine.Length;

//        //        // Copy this line of text, breaking into smaller lines as needed
//        //        if (eol > pos)
//        //        {
//        //            do
//        //            {
//        //                int len = eol - pos;
//        //                if (len > width)
//        //                    len = BreakLine(text, pos, width);
//        //                sb.Append(text, pos, len);
//        //                sb.Append(Environment.NewLine);

//        //                // Trim whitespace following break
//        //                pos += len;
//        //                while (pos < eol && Char.IsWhiteSpace(text[pos]))
//        //                    pos++;
//        //            } while (eol > pos);
//        //        }
//        //        else sb.Append(Environment.NewLine); // Empty line
//        //    }
//        //    return sb.ToString();
//        //}

//        ///// <summary>
//        ///// Locates position to break the given line so as to avoid
//        ///// breaking words.
//        ///// </summary>
//        ///// <param name="text">String that contains line of text</param>
//        ///// <param name="pos">Index where line of text starts</param>
//        ///// <param name="max">Maximum line length</param>
//        ///// <returns>The modified line length</returns>
//        //private static int BreakLine(string text, int pos, int max)
//        //{
//        //    // Find last whitespace in line
//        //    int i = max;
//        //    while (i >= 0 && !Char.IsWhiteSpace(text[pos + i]))
//        //        i--;

//        //    // If no whitespace found, break at maximum length
//        //    if (i < 0)
//        //        return max;

//        //    // Find start of whitespace
//        //    while (i >= 0 && Char.IsWhiteSpace(text[pos + i]))
//        //        i--;

//        //    // Return length of text before whitespace
//        //    return i + 1;
//        //}
//    }
//}
