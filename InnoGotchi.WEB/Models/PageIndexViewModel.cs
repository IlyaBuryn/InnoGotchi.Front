namespace InnoGotchi.WEB.Models
{
    public class PageIndexViewModel<T>
    {
        public IEnumerable<T> Items { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
