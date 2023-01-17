using System.Text;
using UniScheduleGA.BLL;
using UniScheduleGA.Models;
using UniScheduleGA.Models.Constants;

namespace UniScheduleGA.ConsoleApp;

public class HtmlOutput
{
    public static string GetResult(ScheduleChromosome? chromosome)
    {
        var genes = chromosome?.GetGenes();
        var courseClasses = genes?.Select(gene => gene.Value as CourseClass).ToList();

        StringBuilder sb = new StringBuilder();

        sb.AppendFormat(@"
            <!DOCTYPE html>
            <html lang='en'>
                <head>
                <meta charset='UTF-8'>
                <title>Result</title>
                    <style>
                        table, th, td {{
                            border: 1px solid black;
                            border-collapse: collapse;
                        }}
                        td {{
                            padding: 5px;
                        }}
                        .class-block {{
                            width: 90%;
                            min-height: 100px;
                            background: #e4efe6;
                            border-radius: 10px;
                            margin-bottom: 10px;
                            padding: 5px;
                        }}
                    </style>
                </head>
        ");

        sb.AppendFormat(@"
            <body>
                <table style='width: 100%;'>
                    <thead>
                        <tr>
                            <th width='5%'>Hour / Day of week</th>
                            <th>Mon</th>
                            <th>Tue</th>
                            <th>Wed</th>
                            <th>Thu</th>
                            <th>Fri</th>
                        </tr>
                    </thead>
                    <tbody>
        ");

        for (int i = Constant.START_DAY_HOURS; i <= Constant.END_DAY_HOURS; i += 2)
        {
            sb.AppendFormat(@"
                <tr><td>{0}</td>", i);

            for (int j = Constant.START_DAYS_NUM; j <= Constant.END_DAYS_NUM; j++)
            {
                sb.AppendFormat(@"
                    <td>
                        {0}
                    </td>
                ", RenderClassBlocks(GetCoursesForDayAndHour(courseClasses, j, i)));
            }
            sb.AppendFormat("</tr>");
        }

        sb.AppendFormat(@"
                    </tbody>
                </table>
            </body>
         </html>");

        return sb.ToString();
    }
    
    private static List<CourseClass> GetCoursesForDayAndHour(List<CourseClass>? courseClasses, int day, int hour)
    {
        if (courseClasses == null)
        {
            return new List<CourseClass>();
        }

        return courseClasses.Where(c => c.Day == day && c.Hour == hour).ToList();
    }

    private static string RenderClassBlocks(List<CourseClass> courseClasses)
    {
        StringBuilder sb = new StringBuilder();

        foreach (var courseClass in courseClasses)
        {
            sb.AppendFormat(@"<div class='class-block'>");
            sb.AppendFormat(@"<div><strong>Professor:</strong> {0}</div>", courseClass.Professor?.Name);
            sb.AppendFormat(@"<div><strong>Course:</strong> {0}</div>", courseClass.Course?.Name);
            sb.AppendFormat(@"<div><strong>Room:</strong> {0}, seats: {1}</div>", courseClass.Room?.Name, courseClass.Room?.NumberOfSeats);
            sb.AppendFormat(@"<div><strong>Groups:</strong> {0}, students count: {1}</div>", 
                string.Join(", ", courseClass.Groups?.Select(g => g.Name).ToArray() ?? Array.Empty<string>()),
                string.Join(", ", courseClass.Groups?.Select(g => g.NumberOfStudents).ToArray() ?? Array.Empty<int>())
            );
            sb.AppendFormat(@"<div><strong>Total No of students:</strong> {0}</div>", courseClass.NumberOfSeats);
            sb.AppendFormat(@"</div>");
        }

        return sb.ToString();
    }
}