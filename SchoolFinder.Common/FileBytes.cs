namespace SchoolFinder.Common
{
    public class FileBytes : ICloneable
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public byte[]? Data { get; set; }
        public string Extension { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;

        public object Clone()
        {
            return new FileBytes()
            {
                Id = Id,
                Name = Name,
                Data = Data,
                Extension = Extension,
                Url = Url
            };
        }
    }
}
