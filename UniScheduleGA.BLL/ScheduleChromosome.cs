using GeneticSharp;
using UniScheduleGA.BLL.Utils;
using UniScheduleGA.Models;
using UniScheduleGA.Models.Constants;

namespace UniScheduleGA.BLL;

public class ScheduleChromosome : ChromosomeBase
{
    private readonly List<CourseClass> _courseClasses;
    private readonly List<Professor> _professors;
    private readonly List<Course> _courses;
    private readonly List<Room> _rooms;

    public ScheduleChromosome(
        List<CourseClass> courseClasses, 
        List<Professor> professors,
        List<Course> courses,
        List<Room> rooms
    ) : base(courseClasses.Count)
    {
        _courseClasses = courseClasses;
        _professors = professors;
        _courses = courses;
        _rooms = rooms;

        CreateGenes();
    }

    protected override void CreateGenes()
    {
        for (int index = 0; index < this.Length; ++index)
        {
            this.ReplaceGene(index, GenerateGene(index));
        }
    }

    public override Gene GenerateGene(int geneIndex)
    {
        var courseClass = _courseClasses[geneIndex];

        var professorIndex = RandomUtil.Rand(_professors.Count);
        var roomIndex = RandomUtil.Rand(_rooms.Count);
        
        var hour = RandomUtil.RandEvenNumber(Constant.START_DAY_HOURS, (Constant.END_DAY_HOURS - courseClass.Duration));

        courseClass.Professor = _professors[professorIndex];
        courseClass.Room = _rooms[roomIndex];
        courseClass.Day = RandomUtil.Rand(Constant.START_DAYS_NUM, Constant.END_DAYS_NUM);
        courseClass.Hour = hour;

        return new Gene(courseClass);
    }

    public override IChromosome CreateNew()
    {
        return new ScheduleChromosome(_courseClasses, _professors, _courses, _rooms);
    }
}