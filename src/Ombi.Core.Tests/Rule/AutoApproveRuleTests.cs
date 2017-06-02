using System.Security.Principal;
using Moq;
using Ombi.Core.Claims;
using Ombi.Core.Models.Requests;
using Ombi.Core.Rule.Rules;
using Xunit;

namespace Ombi.Core.Tests.Rule
{
    public class AutoApproveRuleTests
    {
        public AutoApproveRuleTests()
        {
            PrincipalMock = new Mock<IPrincipal>();
            Rule = new AutoApproveRule(PrincipalMock.Object);
        }

        private AutoApproveRule Rule { get; }
        private Mock<IPrincipal> PrincipalMock { get; }

        [Fact]
        public void Should_ReturnSuccess_WhenAdminAndRequestMovie()
        {
            PrincipalMock.Setup(x => x.IsInRole(OmbiClaims.Admin)).Returns(true);
            var request = new BaseRequestModel() { Type = Store.Entities.RequestType.Movie };
            var result = Rule.Execute(request);

            Assert.Equal(result.Success, true);
            Assert.Equal(request.Approved, true);
        }

        [Fact]
        public void Should_ReturnSuccess_WhenAdminAndRequestTV()
        {
            PrincipalMock.Setup(x => x.IsInRole(OmbiClaims.Admin)).Returns(true);
            var request = new BaseRequestModel() { Type = Store.Entities.RequestType.TvShow };
            var result = Rule.Execute(request);

            Assert.Equal(result.Success, true);
            Assert.Equal(request.Approved, true);
        }

        [Fact]
        public void Should_ReturnSuccess_WhenAutoApproveMovieAndRequestMovie()
        {
            PrincipalMock.Setup(x => x.IsInRole(OmbiClaims.AutoApproveMovie)).Returns(true);
            var request = new BaseRequestModel() { Type = Store.Entities.RequestType.Movie };
            var result = Rule.Execute(request);

            Assert.Equal(result.Success, true);
            Assert.Equal(request.Approved, true);
        }

        [Fact]
        public void Should_ReturnSuccess_WhenAutoApproveTVAndRequestTV()
        {
            PrincipalMock.Setup(x => x.IsInRole(OmbiClaims.AutoApproveTv)).Returns(true);
            var request = new BaseRequestModel() { Type = Store.Entities.RequestType.TvShow };
            var result = Rule.Execute(request);

            Assert.Equal(result.Success, true);
            Assert.Equal(request.Approved, true);
        }

        [Fact]
        public void Should_ReturnFail_WhenNoClaimsAndRequestMovie()
        {
            var request = new BaseRequestModel() { Type = Store.Entities.RequestType.Movie };
            var result = Rule.Execute(request);

            Assert.Equal(result.Success, true);
            Assert.Equal(request.Approved, false);
        }

        [Fact]
        public void Should_ReturnFail_WhenNoClaimsAndRequestTV()
        {
            var request = new BaseRequestModel() { Type = Store.Entities.RequestType.TvShow };
            var result = Rule.Execute(request);

            Assert.Equal(result.Success, true);
            Assert.Equal(request.Approved, false);
        }


    }
}
