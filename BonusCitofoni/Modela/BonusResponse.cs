namespace BonusCitofoni
{
    public sealed class BonusResponse
    {
        public BonusRequest Request { get; set; }
        public bool Accepted { get; set; }
        public string? Reason { get; set; }
    }
}
