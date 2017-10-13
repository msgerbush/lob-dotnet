namespace Lob
{
    public interface IApiResponse<out T>
    {
        T Body { get; }

        IResponse HttpResponse { get; }
    }
}