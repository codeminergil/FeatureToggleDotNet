namespace FeatureTogglesCoreTests.TestModels
{
    using System.Diagnostics.CodeAnalysis;
    using FeatureToggles;

    [ExcludeFromCodeCoverage]
    public class StrongToggleId : ToggleId
    {
        public StrongToggleId()
        {
            Name = GetType().Name;
        }
    }
}
