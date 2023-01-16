namespace InnoGotchi.WEB.ViewModels
{
    public class PageIndexViewModel<T>
    {
        public IEnumerable<T> Items { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
