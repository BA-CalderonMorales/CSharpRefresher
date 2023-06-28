namespace Fundamentals.LINQ
{
    public class Owner
    {
        public string _name { get; set; }
        public int _ownerID { get; set; }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int OwnerID
        {
            get { return _ownerID; }
            set { _ownerID = value; }
        }
    }
}
