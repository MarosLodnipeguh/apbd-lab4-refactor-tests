namespace LegacyApp.Tests;

public class UserServiceTests
{
    [Fact]
    public void AddUser_ReturnsFalseWhenFirstNameIsEmpty()
    {
        // Arrange
        var userService = new UserService();
        
        // Act
        var result = userService.AddUser(
            null,
            "Kowalski",
            "kowalski@g.com",
            DateTime.Parse("2000-01-01"),
            1
        );
        
        // Assert
        Assert.False(result);
    }
    
    [Fact]
    public void AddUser_ReturnsFalseWhenLastNameIsEmpty()
    {
        // Arrange
        var userService = new UserService();

        // Act
        var result = userService.AddUser(
            "Jan",
            null,
            "jan@g.com",
            DateTime.Parse("2000-01-01"),
            1
        );

        // Assert
        Assert.False(result);
    }
    
    [Fact]
    public void AddUser_ReturnsFalseWhenEmailIsInvalid()
    {
        // Arrange
        var userService = new UserService();

        // Act
        var result = userService.AddUser(
            "Jan",
            "Kowalski",
            "invalid_email",
            DateTime.Parse("2000-01-01"),
            1
        );

        // Assert
        Assert.False(result);
    }
    
    [Fact]
    public void AddUser_ReturnsFalseWhenUserIsUnder21()
    {
        // Arrange
        var userService = new UserService();

        // Act
        var result = userService.AddUser(
            "Jan",
            "Kowalski",
            "jan@g.com",
            DateTime.Now.AddYears(-20), // User is under 21
            1
        );

        // Assert
        Assert.False(result);
    }
    
    [Fact]
    public void AddUser_ThrowsExceptionWhenClientDoesNotExist()
    {
        // Arrange
        var userService = new UserService();
        
        // Act
        Action action = () => userService.AddUser(
            "a",
            "Kowalski",
            "kowalski@g.com",
            DateTime.Parse("2000-01-01"),
            99 // client with ID 99 does not exist
        );
        
        // Assert
        Assert.Throws<ArgumentException>(action);
    }
    
    
    [Fact]
    public void AddUser_ReturnsFalseWhenCreditLimitIsBelow500()
    {
        // Arrange
        var userService = new UserService();

        // Act
        var result = userService.AddUser(
            "Jan",
            "Kowalski",
            "jan@g.com",
            DateTime.Parse("2000-01-01"),
            1
        );

        // Assert
        Assert.False(result);
    }


}