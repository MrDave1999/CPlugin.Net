namespace Example.SharedEntities;

public class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Document { get; set; }
    public DocumentTypes DocumentType { get; set; }
}
