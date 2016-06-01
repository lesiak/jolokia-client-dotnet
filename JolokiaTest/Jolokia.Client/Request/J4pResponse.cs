namespace Jolokia.Client.Request
{
    public interface IJ4pResponse
    {
    }

    public abstract class J4pResponse<TReq> : IJ4pResponse where TReq : IJ4pRequest
    {
         
    }
}