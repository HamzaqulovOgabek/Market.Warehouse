namespace E_CommerceProjectDemo.Application.Services.ProductServices;

public class ReviewDto
{
    public string UserName { get; set; }
    public required string Message { get; set; }
    public int Rate { get; set; }
}
