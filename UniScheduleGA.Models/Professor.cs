namespace UniScheduleGA.Models
{
    public class Professor
    {
        // Bind professor to course
	    public void AddCourseClass(CourseClass courseClass)
        {
            CourseClasses.Add(courseClass);
        }

        public override bool Equals(object obj)
        {
            return obj is Professor professor && Id == professor.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
        public int Id { get; set; }

        public string Name { get; set; }
        
        public List<CourseClass> CourseClasses { get; set; }

    }
}
