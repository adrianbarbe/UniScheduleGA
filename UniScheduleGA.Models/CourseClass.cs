namespace UniScheduleGA.Models
{
    public class CourseClass : IComparable<CourseClass>
    {
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
