namespace UniScheduleGA.Models
{
    public class CourseClass : IComparable<CourseClass>
    {
	    //       public CourseClass(Professor professor, Course course, bool requiresLab, int duration, params StudentsGroup[] groups)
  //       {
  //           Id = _nextClassId++;
  //           Professor = professor;
		// 	Course = course;
		// 	NumberOfSeats = 0;
		// 	LabRequired = requiresLab;
		// 	Duration = duration;
		// 	Groups = new List<StudentsGroup>();
  //
		// 	// bind professor to class
		// 	Professor.AddCourseClass(this);
  //
		// 	// bind student groups to class
		// 	foreach(StudentsGroup group in groups)
  //           {
		// 		group.AddClass(this);
		// 		Groups.Add(group);
		// 		NumberOfSeats += group.NumberOfStudents;
		// 	}
		// }

		// Returns TRUE if another class has one or overlapping student groups.
		public bool GroupsOverlap(CourseClass c)
        {
			return Groups.Intersect(c.Groups).Any();
        }

		// Returns TRUE if another class has same professor.
		public bool ProfessorOverlaps(CourseClass c) {
			return Professor.Equals(c.Professor);
		}

		public int CompareTo(CourseClass other)
		{
			if (other == null)
			{
				return -1;
			}

			return other.Id - Id;
        }
		
		public int Id { get; set; }

        public Professor? Professor { get; set; }

		public Course Course { get; set; }

		public List<StudentsGroup> Groups { get; set; }

		public int NumberOfSeats { get; set; }

		public bool LabRequired { get; set; }
		
		public int Duration { get; set; }
		
		public int? Hour { get; set; }
		
		public int? Day { get; set; }

		public Room? Room { get; set; }
    }
}
