using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Moq;
using Store.Core.Contracts.Interfaces.Services;
using Store.Core.Host.Authorization.CurrentUser;
using Store.Core.Services.Internal.Records.Commands.UpdateRecord;
using Xunit;
using Record = Store.Core.Contracts.Domain.Record;

namespace Store.Services.Records.Tests.Handlers
{
    public class MarkAsSoldRecordTests
    {
        private readonly Mock<IRecordService> _recordService;
        private readonly Mock<ICacheService> _cacheService;
        private readonly Mock<ICurrentUserService> _currentUser;

        public MarkAsSoldRecordTests()
        {
            _recordService = new Mock<IRecordService>();
            _cacheService = new Mock<ICacheService>();
            _currentUser = new Mock<ICurrentUserService>();
        }

        [Fact]
        public void HandleRequest_ForSoldRecord_ThrowsException()
        {
            var id = Guid.NewGuid();
            var request = new MarkAsSoldCommand
            {
                Id = id
            };

            var model = new Record()
            {
                Id = id,
                IsSold = true
            };

            _cacheService.Setup(x => x.GetCacheAsync<Record>(id.ToString(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(model);

            var handler = new RecordMarkAsSoldCommandHandler(_recordService.Object, _cacheService.Object, _currentUser.Object);

            Func<Task<Unit>> handle = async () => await handler.Handle(request, CancellationToken.None);

            handle.Should().Throw<ArgumentException>();
        }
        
        [Fact]
        public async Task HandleRequest_ChangesSoldStatus_WhenRequestIsCorrect()
        {
            var id = Guid.NewGuid();
            var editor = Guid.NewGuid();
            var request = new MarkAsSoldCommand
            {
                Id = id
            };

            var model = new Record()
            {
                Id = id,
                IsSold = false,
                EditedBy = editor
            };

            _currentUser.Setup(x => x.Id).Returns(editor);
            _cacheService.Setup(x => x.GetCacheAsync<Record>(id.ToString(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(model);
            _cacheService.Setup(x => x.AddCacheAsync(model, It.IsAny<TimeSpan?>(), It.IsAny<CancellationToken>()));
            _recordService.Setup(x => x.MarkRecordAsSoldAsync(id, editor, It.IsAny<CancellationToken>()));
            _recordService.Setup(x => x.GetRecordAsync(id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(model);

            var handler = new RecordMarkAsSoldCommandHandler(_recordService.Object, _cacheService.Object, _currentUser.Object);

            await handler.Handle(request, CancellationToken.None);

           _cacheService.Verify(X=>X.GetCacheAsync<Record>(id.ToString(), It.IsAny<CancellationToken>()), Times.Once);
           _recordService.Verify(x=>x.GetRecordAsync(id, It.IsAny<CancellationToken>()), Times.Once);
           _cacheService.Verify(x=>x.AddCacheAsync(model, It.IsAny<TimeSpan?>(), It.IsAny<CancellationToken>()), Times.Once);
           _recordService.Verify(x=>x.MarkRecordAsSoldAsync(id, editor, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}