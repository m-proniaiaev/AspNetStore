using MediatR;

namespace Store.Core.Handlers.GetRecords
{
    public class GetRecordsQuery : IRequest<GetRecordsResponse>
    {
        public bool? IsSold { get; set; }
    }
}