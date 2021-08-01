namespace Moss.ApiClient.SomaFm
{
    /// <summary>
    /// Represent a common response
    /// </summary>
    public abstract record Response
    {
        /// <summary>
        /// Indicates if API client call was successful
        /// </summary>
        public bool Success { get; protected set; }

        /// <summary>
        /// Error message
        /// </summary>
        public string ErrorMessage { get; protected set; }
    }
}
