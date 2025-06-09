namespace Tournament.Management.API.Models.Domain
{
    public class PlayerStat
    {
        public int Id { get; set; }
        public Guid PlayerId { get; set; }
        public Guid MatchId { get; set; }
        public int Goals { get; set; }
        public int Assists { get; set; }
        public int YellowCards { get; set; }
        public int RedCards { get; set; }

        public User Player { get; set; } = null!;
        public TeamMatch Match { get; set; } = null!;
    }

}
