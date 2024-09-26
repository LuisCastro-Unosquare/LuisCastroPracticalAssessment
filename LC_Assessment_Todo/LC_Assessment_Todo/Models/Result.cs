namespace LC_Assessment_Todo.Models
{
    /// <summary>
    /// Result class to share data with FE
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Result<T>
    {
        /// <summary>
        /// Contructor to be used when data need to be provideed to the Front-End without handled errors.
        /// </summary>
        /// <param name="data"></param>
        public Result(T data)
        {
            this.Data = data;
        }

        /// <summary>
        /// Contructor to be used when handled error need to be provideed to the Front-End.
        /// </summary>
        /// <param name="errorMessage"></param>
        public Result(string errorMessage)
        {
            this.Error = errorMessage;
        }
        /// <summary>
        /// Data where information payload will be provided to the Front-End.
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// Error is where error message will be provided to the Back-End.
        /// </summary>
        public string? Error { get; set; } 
    }
}
