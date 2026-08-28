namespace entity_library
{
    public class Student : Person
    {
        private string file = "";

        public string File
        {
            get { return file; }
            set { file = value; }
        }
    }
}
