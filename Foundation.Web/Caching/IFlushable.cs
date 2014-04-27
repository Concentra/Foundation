namespace Foundation.Web.Caching
{
    public interface IFlushable
    {
        bool ForceFlush { get; set; }
    }
}
