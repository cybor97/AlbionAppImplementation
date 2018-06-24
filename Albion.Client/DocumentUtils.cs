using Albion.Data;
using System.Collections.Generic;
using System.Linq;
using Xceed.Words.NET;

namespace Albion.Client
{
    static class DocumentUtils
    {
        public static void SaveStatementWithTemplate(string studentFullName, string courseName,
            List<Mark> marks, string templateFilename, string saveFilename)
        {
            DocX.Load(templateFilename)
                .Fill("{StudentFullName}", studentFullName)
                .Fill("{CourseName}", courseName)

                .FillSimpleStatementResults(marks)

                .SaveAs(saveFilename);
        }

        public static void SaveCourses(string year, string month, List<Course> courses,
            string templateFilename, string saveFilename)
        {
            DocX.Load(templateFilename)
                .Fill("{Year}", year)
                .Fill("{Month}", month)

                .FillCoursesList(courses)

                .SaveAs(saveFilename);
        }

        static DocX FillSimpleStatementResults(this DocX document, List<Mark> marks)
        {
            var table = document.Tables.FirstOrDefault(c => c.Rows.Count >= 1);
            if (marks != null && table != null)
            {
                var templateRow = table.Rows.LastOrDefault();

                for (int result_i = 0; result_i < marks.Count; result_i++)
                {
                    var currentRow = table.InsertRow(templateRow);

                    for (int cell_i = 0; cell_i < templateRow.Cells.Count; cell_i++)
                    {
                        currentRow.Cells[cell_i]
                                  .Fill("{IndexNumber}", (result_i + 1).ToString())
                                  .Fill("{MarkDate}", marks[result_i].MarkDate.ToShortDateString())
                                  .Fill("{MarkValue}", marks[result_i].MarkValue.ToString())
                                  .Fill("{MarkNote}", marks[result_i].MarkNote)
                                  .Fill("{MarkValueTotal}", marks.CalculateFinalTotal().ToString());
                    }
                }

                templateRow.Remove();
            }
            return document;
        }

        static DocX FillCoursesList(this DocX document, List<Course> courses)
        {
            var table = document.Tables.FirstOrDefault(c => c.Rows.Count >= 1);
            if (courses != null && table != null)
            {
                var templateRow = table.Rows.LastOrDefault();

                for (int result_i = 0; result_i < courses.Count; result_i++)
                {
                    var currentRow = table.InsertRow(templateRow);

                    for (int cell_i = 0; cell_i < templateRow.Cells.Count; cell_i++)
                    {
                        currentRow.Cells[cell_i]
                                  .Fill("{IndexNumber}", (result_i + 1).ToString())
                                  .Fill("{CourseName}", courses[result_i].CourseName)
                                  .Fill("{TotalProfit}", courses[result_i].TotalProfit.ToString())
                                  .Fill("{FinalTotal}", courses.CalculateFinalTotal().ToString());
                    }
                }

                templateRow.Remove();
            }
            return document;
        }

        static T Fill<T>(this T docx, string placeHolder, string content) where T : Container
        {
            docx.ReplaceText(placeHolder, content ?? " ");
            return docx;
        }

        static int CalculateFinalTotal(this List<Course> courses)
        {
            int finalTotal = 0;
            courses.ForEach(c => finalTotal += c.TotalProfit);
            return finalTotal;
        }

        static int CalculateFinalTotal(this List<Mark> marks)
        {
            int finalTotal = 0;
            marks.ForEach(c => finalTotal += c.MarkValue);
            return finalTotal / marks.Count;
        }
    }
}