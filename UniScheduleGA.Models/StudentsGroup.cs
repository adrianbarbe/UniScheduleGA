namespace UniScheduleGA.Models
{
	public class StudentsGroup
    {
	    // Bind group to class
		public void AddClass(CourseClass courseClass)
        {			
			CourseClasses.Add(courseClass);
		}

        public override bool Equals(object obj)
        {
            return obj is StudentsGroup group && Id == group.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }

        public int Id { get; set; }

		public string Name { get; set; }

		public int NumberOfStudents { get; set; }

		public List<CourseClass> CourseClasses { get; set; }

	}
}
