using System.Linq.Expressions;

namespace UniScheduleGA.Models.Configuration;

public class DataProvider
{
	public readonly List<CourseClass> CourseClasses;

	public List<Professor> Professors;
    
    public List<Course> Courses;
    
    public List<Room> Rooms;

    public List<StudentsGroup> StudentGroups;


    public DataProvider()  {
	    CourseClasses = new List<CourseClass>();

        Professors = new List<Professor>();
        Courses = new List<Course>();
        StudentGroups = new List<StudentsGroup>();
        Rooms = new List<Room>();

        PopulateProfessors();
        PopulateCourses();
        PopulateRooms();
        PopulateStudentGroups();
        PopulateCourseClasses();
    }

    private void PopulateProfessors()
    {
        Professors.Add(new Professor
        {
            Id = 1,
            Name = "Name1",
            CourseClasses = new List<CourseClass>(),
        });
        Professors.Add(new Professor
        {
            Id = 2,
            Name = "Name2",
            CourseClasses = new List<CourseClass>(),
        });
        Professors.Add(new Professor
        {
            Id = 3,
            Name = "Name3",
            CourseClasses = new List<CourseClass>(),
        });
        Professors.Add(new Professor
        {
            Id = 4,
            Name = "Name4",
            CourseClasses = new List<CourseClass>(),
        });
        Professors.Add(new Professor
        {
            Id = 5,
            Name = "Name5",
            CourseClasses = new List<CourseClass>(),
        });
        Professors.Add(new Professor
        {
            Id = 6,
            Name = "Name6",
            CourseClasses = new List<CourseClass>(),
        });
        Professors.Add(new Professor
        {
            Id = 7,
            Name = "Name7",
            CourseClasses = new List<CourseClass>(),
        });
    }

    private void PopulateCourses()
    {
        Courses.Add(new Course
        {
            Id = 1,
            Name = "Web Programming",
            LabRequired = true,
        });
        Courses.Add(new Course
        {
            Id = 2,
            Name = "Computer Architecture",
            LabRequired = true,
        });
        Courses.Add(new Course
        {
            Id = 3,
            Name = "Advanced DB systems",
            LabRequired = true,
        });
        Courses.Add(new Course
        {
            Id = 4,
            Name = "AAPTM",
            LabRequired = false,
        });
        Courses.Add(new Course
        {
            Id = 5,
            Name = "PrP",
            LabRequired = false,
        });
        Courses.Add(new Course
        {
            Id = 6,
            Name = "Networks",
            LabRequired = true
        });
        Courses.Add(new Course
        {
            Id = 7,
            Name = "Ethics",
            LabRequired = false,
        });
        Courses.Add(new Course
        {
            Id = 8,
            Name = "System administration",
            LabRequired = false
        });
    }

    private void PopulateRooms()
    {
        Rooms.Add(new Room
        {
            Id = 1,
            Name = "Room3",
            Lab = true,
            NumberOfSeats = 46
        });
        Rooms.Add(new Room
        {
            Id = 2,
            Name = "Room5",
            Lab = false,
            NumberOfSeats = 16,
        });
        Rooms.Add(new Room
        {
	        Id = 3,
	        Name = "Room6",
	        Lab = true,
	        NumberOfSeats = 38,
        });
        Rooms.Add(new Room
        {
	        Id = 4,
	        Name = "Room8",
	        Lab = false,
	        NumberOfSeats = 38,
        });
    }

    private void PopulateStudentGroups()
    {
        StudentGroups.Add(new StudentsGroup
        {
            Id = 1,
            Name = "Group 1", 
            NumberOfStudents = 19,
            CourseClasses = new List<CourseClass>(),
        });
        StudentGroups.Add(new StudentsGroup
        {
            Id = 2,
            Name = "Group 2", 
            NumberOfStudents = 18,
            CourseClasses = new List<CourseClass>(),
        });
        StudentGroups.Add(new StudentsGroup
        {
            Id = 3,
            Name = "Group 3", 
            NumberOfStudents = 12,
            CourseClasses = new List<CourseClass>(),
        });
        StudentGroups.Add(new StudentsGroup
        {
            Id = 4,
            Name = "Group 4", 
            NumberOfStudents = 15,
            CourseClasses = new List<CourseClass>(),
        });
    }

    private void PopulateCourseClasses()
    {
	    // First class
	    var course1 = Courses.First(c => c.Id == 1); // max 8
	    var studentGroups1_2 = StudentGroups.Where(g => g.Id == 1 || g.Id == 2).ToList(); // max 4
	    var professor1 = Professors.First(p => p.Id == 1); // max 7

	    var courseClass1 = new CourseClass
	    {
		    Id = 1,
		    Course = course1,
		    LabRequired = course1.LabRequired,
		    Professor = professor1,
		    Groups = studentGroups1_2,
		    NumberOfSeats = studentGroups1_2.Select(g => g.NumberOfStudents).Sum(),
		    Duration = 2,
	    };
	    
	    CourseClasses.Add(courseClass1);
	    
	    studentGroups1_2.ForEach(st =>
	    {
		    st.CourseClasses.Add(courseClass1);
	    });
	    professor1.CourseClasses.Add(courseClass1);

	    // Second class
	    var courseClass2 = new CourseClass
	    {
		    Id = 2,
		    Course = course1,
		    LabRequired = course1.LabRequired,
		    Groups = studentGroups1_2,
		    NumberOfSeats = studentGroups1_2.Select(g => g.NumberOfStudents).Sum(),
		    Duration = 2,
	    };
	    
	    CourseClasses.Add(courseClass2);
	    
	    studentGroups1_2.ForEach(st =>
	    {
		    st.CourseClasses.Add(courseClass2);
	    });
	    professor1.CourseClasses.Add(courseClass2);
	    
	    // Third class
	    var course2 = Courses.First(c => c.Id == 2);
	    var studentGroups4 = StudentGroups.Where(g => g.Id == 4).ToList();
	    var professor2 = Professors.First(p => p.Id == 2);
	    
	    var courseClass3 = new CourseClass
	    {
		    Id = 3,
		    Course = course2,
		    LabRequired = course2.LabRequired,
		    Groups = studentGroups4,
		    NumberOfSeats = studentGroups1_2.Select(g => g.NumberOfStudents).Sum(),
		    Duration = 2,
	    };
	    
	    CourseClasses.Add(courseClass3);
	    
	    studentGroups4.ForEach(st =>
	    {
		    st.CourseClasses.Add(courseClass3);
	    });
	    professor2.CourseClasses.Add(courseClass3);
	    
	    
	    // Forth class
	    var course7 = Courses.First(c => c.Id == 7);  // max 8
	    var studentGroups3 = StudentGroups.Where(g => g.Id == 3).ToList();  // max 4
	    var professor6 = Professors.First(p => p.Id == 6);  // max 7
	    
	    var courseClass4 = new CourseClass
	    {
		    Id = 4,
		    Course = course7,
		    LabRequired = course7.LabRequired,
		    Groups = studentGroups3,
		    NumberOfSeats = studentGroups3.Select(g => g.NumberOfStudents).Sum(),
		    Duration = 2,
	    };
	    
	    CourseClasses.Add(courseClass4);
	    
	    studentGroups3.ForEach(st =>
	    {
		    st.CourseClasses.Add(courseClass4);
	    });
	    professor6.CourseClasses.Add(courseClass4);
	    
	    // Fifth class
	    var course8 = Courses.First(c => c.Id == 8);  // max 8
	    var studentGroups4a = StudentGroups.Where(g => g.Id == 4).ToList();  // max 4
	    var professor5 = Professors.First(p => p.Id == 5);  // max 5
	    
	    var courseClass5 = new CourseClass
	    {
		    Id = 5,
		    Course = course8,
		    LabRequired = course8.LabRequired,
		    Groups = studentGroups4a,
		    NumberOfSeats = studentGroups4a.Select(g => g.NumberOfStudents).Sum(),
		    Duration = 2,
	    };
	    
	    CourseClasses.Add(courseClass5);
	    
	    studentGroups4a.ForEach(st =>
	    {
		    st.CourseClasses.Add(courseClass5);
	    });
	    professor5.CourseClasses.Add(courseClass5);
    }
}