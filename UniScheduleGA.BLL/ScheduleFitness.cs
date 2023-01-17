using GeneticSharp;
using UniScheduleGA.Models;

namespace UniScheduleGA.BLL;

public class ScheduleFitness : IFitness
{
    public double Evaluate(IChromosome chromosome)
    {
        // Chromosome's score
        Console.WriteLine("== Evaluate ==");
        double score = 0;
        var countCriteria = 0;

        var genes = chromosome.GetGenes();
        var courseClasses = genes.Select(g => g.Value as CourseClass).ToList();

        foreach (var courseClass in courseClasses)
        {
            if (IsComputerEnough(courseClass))
            {
                score++;
                countCriteria++;
            }
            else
            {
                score /= 2;
                countCriteria++;
            }
            
            if (IsSeatEnough(courseClass))
            {
                score += 1.2;
                countCriteria++;
            }
            else
            {
                score = 0;
                countCriteria++;
            }
        
            if (IsRoomNotOverlapped(courseClasses))
            {
                score++;
                countCriteria++;
            }
            else
            {
                score = 0;
                countCriteria++;
            }
        
            if (IsProfessorNotOverlapped(courseClasses))
            {
                score++;
                countCriteria++;
            }
            else
            {
                score = 0;
                countCriteria++;
            }
        
            if (IsStudentNotOverlapped(courseClasses))
            {
                score++;
                countCriteria++;
            }
            else
            {
                score = 0;
                countCriteria++;
            }
            
            // if (IsSeatOptimal(courseClass, courseClasses))
            // {
            //     score++;
            //     countCriteria++;
            // }
            // else
            // {
            //     score /= 2;
            //     countCriteria++;
            // }
        }

        double eval = (double)score / (double)countCriteria;

        Console.WriteLine($"Fitness Eval: {eval:0.000} => score: {score}, countCriteria: {countCriteria}");

        return eval;
    }

    private bool IsSeatOptimal(CourseClass courseClass, List<CourseClass> courseClasses)
    {
        var numberOfStudents = courseClass.NumberOfSeats;
        var currentRoomNumberOfSeats = courseClass.Room.NumberOfSeats;

        var roomsWhichFit = courseClasses.Select(cc => cc.Room).Where(cc => cc.NumberOfSeats >= numberOfStudents).ToList();

        if (roomsWhichFit.Count > 0)
        {
            return roomsWhichFit[0].NumberOfSeats == currentRoomNumberOfSeats;
        }
        else
        {
            return false;
        }
    }

    private bool IsSeatEnough(CourseClass courseClass)
    {
        return courseClass.Room.NumberOfSeats >= courseClass.NumberOfSeats;
    }

    private bool IsComputerEnough(CourseClass courseClass)
    {
        return courseClass.Room.Lab == courseClass.LabRequired;
    }

    private bool IsRoomNotOverlapped(List<CourseClass> courseClasses)
    {
        var roomIds = courseClasses.Select(cc => cc.Room.Id);

        foreach (var roomId in roomIds)
        {
            var classesWithSameRoomGroupBy = courseClasses
                .Where(cc => cc.Room.Id == roomId)
                .GroupBy(cc => new { cc.Day, cc.Hour });

            foreach (var classesWithSameRoom in classesWithSameRoomGroupBy)
            {
                if (classesWithSameRoom.Count() > 1)
                {
                    return false;
                }
            }
        }
        
        return true;
    }

    private bool IsProfessorNotOverlapped(List<CourseClass> courseClasses)
    {
        var professorIds = courseClasses.Select(cc => cc.Professor.Id);

        foreach (var professorId in professorIds)
        {
            var classesWithSameProfessorGroupBy = courseClasses
                .Where(cc => cc.Professor.Id == professorId)
                .GroupBy(cc => new { cc.Day, cc.Hour });

            foreach (var classesWithSameProfessor in classesWithSameProfessorGroupBy)
            {
                if (classesWithSameProfessor.Count() > 1)
                {
                    return false;
                }
            }
        }
        
        return true;
    }

    private bool IsStudentNotOverlapped(List<CourseClass> courseClasses)
    {
        var groupIds = courseClasses.SelectMany(cc => cc.Groups.Select(g => g.Id));

        foreach (var groupId in groupIds)
        {
            var classesWithSameGroupsGroupBy = courseClasses
                .Where(cc => cc.Groups.Any(g => g.Id == groupId))
                .GroupBy(cc => new { cc.Day, cc.Hour });

            foreach (var classesWithSameGroups in classesWithSameGroupsGroupBy)
            {
                if (classesWithSameGroups.Count() > 1)
                {
                    return false;
                }
            }
        }
        
        return true;
    }
}