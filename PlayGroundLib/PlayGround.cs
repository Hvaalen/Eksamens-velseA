namespace PlayGroundLib
{
    public class PlayGround
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MaxChildren { get; set; }
        public int MinAge { get; set; }


        public PlayGround() { }

        public PlayGround(int id, string name, int maxChildren, int minAge)
        {
            Id = id;
            Name = name;
            MaxChildren = maxChildren;
            MinAge = minAge;
        }

        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}, MaxChildren: {MaxChildren}, MinAge: {MinAge}";
        }
    }
}
