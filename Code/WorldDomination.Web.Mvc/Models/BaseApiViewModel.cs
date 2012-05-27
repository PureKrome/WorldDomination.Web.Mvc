namespace WorldDomination.Web.Mvc.Models
{
    public abstract class BaseApiViewModel
    {
        public int MaximumQuota { get; set; }
        public int RemainingQuota { get; set; }
    }
}