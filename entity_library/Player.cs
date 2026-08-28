namespace entity_library
{
    public class Player : Person
    {
        private int numero = 0;

        public int Numero
        {
            get { return numero; }
            set { numero = value; }
        }
    }
}
