namespace BeerSale.Infrastructure
{ 
    /// <summary>
    /// Helper class that validates arguments.
    /// </summary>
    public static class Ensure
    {
        /// <summary>
        /// Helper method for validating a list of argument that will be used by this API in any requests.
        /// </summary>
        /// <param name="arguments">The list of objects to be validated.</param>
        public static void ArgumentsNotNull(Dictionary<object, string> arguments)
        {
            foreach (var argument in arguments)
            {
                ArgumentNotNull(argument.Key, argument.Value);
            }
        }

        public static void ArgumentNotNull(object argument, string argumentName)
        {
            if (argument == null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }

        public static void ArgumentNotLessThan(int argument, int minimumValue, string argumentName)
        {
            if (argument < minimumValue)
            {
                throw new ArgumentOutOfRangeException(argumentName);
            }
        }

        public static void ArgumentNotNullOrEmpty(string argument, string argumentName)
        {
            if (string.IsNullOrWhiteSpace(argument))
            {
                throw new ArgumentNullException(argumentName);
            }
        }

        public static void ArgumentNotNullOrEmpty(IEnumerable<object> argument, string argumentName)
        {
            if (argument?.Any() != true)
            {
                throw new ArgumentException("Value should not be null and it should cointain at least 1 element!", argumentName);
            }
        }
    }
}
