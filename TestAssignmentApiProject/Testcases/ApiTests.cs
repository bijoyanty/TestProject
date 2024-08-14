using FluentAssertions;
using Microsoft.Playwright;
using NUnit.Framework;
using TestAssignmentApiProject.Models;
using TestAssignmentApiProject.SetUp;
using TestAssignmentApiProject.Utilities;

namespace TestAssignmentApiProject.Testcases;

[TestFixture] [Parallelizable( ParallelScope.All )] public class ApiTests : ApiTestSetup
{
    [Test, Retry( 2 )] public async Task CreateUser()
    {
        //Arrange
        var createUserRequest = new CreateOrUpdateUserRequest
        {
            Name = "Dummy",
            Job = "Leader"
        };

        //Act
        var response = await ApiMethods.Post<CreateUserResponse>( ReqresRequestContext, "api/users", new APIRequestContextOptions() { DataObject = createUserRequest } );

        //Assert
        response.Id.Should().NotBeNullOrEmpty();
        response.CreatedAt.Date.Should().BeSameDateAs( DateTimeOffset.Now.Date );
    }


    [Test, Retry( 2 )] public async Task UpdateUser()
    {
        //Arrange
        var updateUserRequest = new CreateOrUpdateUserRequest
        {
            Name = "Dummy",
            Job = "zion resident"
        };

        //Act
        var response = await ApiMethods.Put<UpdateUserResponse>( ReqresRequestContext, "api/users/2", new APIRequestContextOptions() { DataObject = updateUserRequest } );

        //Assert
        response.Job.Should().Be( updateUserRequest.Job );
        response.UpdatedAt.Date.Should().BeSameDateAs( DateTimeOffset.Now.Date );
    }

    [Test, Retry( 2 )] public async Task RetrieveSingleUser()
    {
        //Act
        var response = await ApiMethods.Get<User>( ReqresRequestContext, $"api/users/2" );

        //Assert
        response.Data.Id.Should().BeGreaterThan( 0 );

        response.Data.First_Name.Should().NotBeNullOrEmpty();
        response.Data.Last_Name.Should().NotBeNullOrEmpty();

        response.Support.Text.Should().NotBeNullOrEmpty();
        response.Support.Url.Should().NotBeNullOrEmpty();
    }
}