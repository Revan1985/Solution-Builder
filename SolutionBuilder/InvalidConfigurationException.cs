namespace SolutionBuilder
{
    public class InvalidConfigurationException : Exception
    {
        public InvalidConfigurationException() { }
        public InvalidConfigurationException(string message) : base(message) { }    
    }
}
