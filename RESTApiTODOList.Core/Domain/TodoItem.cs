namespace RESTApiTODOList.Core.Domain
{
    public sealed class TodoItem : EntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
