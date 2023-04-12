using Moq.AutoMock;

namespace demo.cap.tests.Services.Fixture;

public abstract class ServiceTestsFixure : TestsFixture
{
    public AutoMocker Mocker { get; protected set; }
}
