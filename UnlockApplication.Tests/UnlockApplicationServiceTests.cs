using Moq;
using System;
using System.Collections.Generic;
using UnlockApplication.Agent;
using UnlockApplication.Agent.Repositories;
using Xunit;

namespace UnlockApplication.Tests
{

    public class UnlockApplicationServiceTests
    {
        private const int WorkspaceId = 3;

        [Fact]
        public void UnlockApplicationsInEnvironment_WorkspaceHasApplicationInstalled_UnlocksApplication()
        {
            //ARRANGE
            var workspaceMock = new Mock<IWorkspaceRepository>();
            var applicationMock = new Mock<IApplicationRepository>();
            var workspaceApplication = new UnlockApplication.Agent.UnlockApplication();
            workspaceApplication.ArtifactID = 3;

            workspaceMock.Setup(x => x.GetWorkspaceIds()).Returns(new List<int> { WorkspaceId });
            applicationMock.Setup(x => x.DoesWorkspaceHaveApplication(It.IsAny<int>(), It.IsAny<Guid>())).Returns(true);
            applicationMock.Setup(x => x.GetApplicationsToUnlock(It.IsAny<int>())).Returns(new List<UnlockApplication.Agent.UnlockApplication> { workspaceApplication });

            //ACT
            var service = new UnlockApplicationService(workspaceMock.Object, applicationMock.Object);
            service.UnlockApplicationsInEnvironment();

            //ASSERT
            applicationMock.Verify(x => x.UnlockApplication(It.Is<int>(w => w == WorkspaceId), It.Is<UnlockApplication.Agent.UnlockApplication>(ua => ua.ArtifactID == workspaceApplication.ArtifactID)), Times.AtLeastOnce);
        }

        [Fact]
        public void UnlockApplicationsInEnvironment_WorkspaceDoesNotHaveApplicationInstalled_DoesNotUnlocksApplication()
        {
            //ARRANGE
            var workspaceMock = new Mock<IWorkspaceRepository>();
            var applicationMock = new Mock<IApplicationRepository>();
            var workspaceApplication = new UnlockApplication.Agent.UnlockApplication();
            workspaceApplication.ArtifactID = 3;

            workspaceMock.Setup(x => x.GetWorkspaceIds()).Returns(new List<int> { WorkspaceId });
            applicationMock.Setup(x => x.DoesWorkspaceHaveApplication(It.IsAny<int>(), It.IsAny<Guid>())).Returns(false);
            applicationMock.Setup(x => x.GetApplicationsToUnlock(It.IsAny<int>())).Returns(new List<UnlockApplication.Agent.UnlockApplication> { workspaceApplication });

            //ACT
            var service = new UnlockApplicationService(workspaceMock.Object, applicationMock.Object);
            service.UnlockApplicationsInEnvironment();

            //ASSERT
            applicationMock.Verify(x => x.UnlockApplication(It.Is<int>(w => w == WorkspaceId), It.Is<UnlockApplication.Agent.UnlockApplication>(ua => ua.ArtifactID == workspaceApplication.ArtifactID)), Times.Never);
        }
    }
}
