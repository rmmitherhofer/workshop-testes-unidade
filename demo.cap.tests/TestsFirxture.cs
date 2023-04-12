using Bogus;

namespace demo.cap.tests;
public abstract class TestsFixture : IDisposable
{
    protected Faker Faker { get; private set; }

    protected TestsFixture() => Faker = new();
    public void Dispose() { }
}
