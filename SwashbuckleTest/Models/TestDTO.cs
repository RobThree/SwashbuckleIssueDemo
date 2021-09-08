using SwashbuckleTest.Infrastructure.ObjectId;

namespace SwashbuckleTest.Models
{
    public class TestDTO
    {
        public ObjectId Id { get; init; }
        public string Name { get; init; } = string.Empty;
    }
}
