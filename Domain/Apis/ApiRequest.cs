namespace Domain.Apis;

public class ApiRequest<T>
{
    public required T Data { get; set; }
}
