namespace Game3inRow.Domain
{
    public abstract class GameStatistic : BaseEntity
    {
        public string[] Turns {get;};
        public User User {get;};
    }
}