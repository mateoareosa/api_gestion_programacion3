namespace entity_library
{
    public class Course
    {
        private long id = 0;
        private string name = "";

        public long Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}
