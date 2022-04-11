namespace Diff.Domain.Entities
{
    public class InputModel
    {
        public int Id { get; set; }
        public string Side { get; set; }
        public string Base64Str { get; set; }
    }
}