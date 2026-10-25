using Microsoft.Extensions.Configuration;
using NSubstitute;
using Library.ApplicationCore;
using Library.ApplicationCore.Entities;
using Library.Infrastructure.Data;

namespace Library.UnitTests.Infrastructure.JsonLoanRepositoryTests;

public class GetLoanTest
{
    private readonly ILoanRepository _mockLoanRepository;
    private readonly JsonLoanRepository _jsonLoanRepository;
    private readonly IConfiguration _configuration;
    private readonly JsonData _jsonData;

    public GetLoanTest()
    {
        _mockLoanRepository = Substitute.For<ILoanRepository>();

        _configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["JsonPaths:Authors"] = Path.Combine("Json", "Authors.json"),
                ["JsonPaths:Books"] = Path.Combine("Json", "Books.json"),
                ["JsonPaths:BookItems"] = Path.Combine("Json", "BookItems.json"),
                ["JsonPaths:Patrons"] = Path.Combine("Json", "Patrons.json"),
                ["JsonPaths:Loans"] = Path.Combine("Json", "Loans.json")
            })
            .Build();

        _jsonData = new JsonData(_configuration);
        _jsonLoanRepository = new JsonLoanRepository(_jsonData);
    }

    [Fact(DisplayName = "JsonLoanRepository.GetLoan: Returns the loan when the loan ID exists")]
    public async Task GetLoan_ReturnsLoan_WhenLoanIdExists()
    {
        // Arrange
        var expectedLoan = new Loan { Id = 1 };
        
        // Act
        var actual = await _jsonLoanRepository.GetLoan(expectedLoan.Id);
       
        // Assert
        Assert.NotNull(actual);
        Assert.Equal(expectedLoan.Id, actual.Id);
    }
}
