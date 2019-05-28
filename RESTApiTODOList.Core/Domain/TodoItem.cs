namespace RESTApiTODOList.Core.Domain
{
    public sealed class TodoItem : EntityBase
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
