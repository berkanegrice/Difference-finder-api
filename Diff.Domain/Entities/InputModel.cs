namespace Diff.Domain.Entities
{
    public class InputModel
    {
        public int Id { get; set; }
        public string Side { get; set; }
        public string Data { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}, Side: {Side}, Data: {Data}";
        }
    }
}