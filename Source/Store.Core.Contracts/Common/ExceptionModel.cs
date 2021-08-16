namespace Store.Core.Contracts.Common
{
    public class ExceptionModel
    {
        public string CorrelationId { get; set; }
        public object Error { get; set; }
    }
}