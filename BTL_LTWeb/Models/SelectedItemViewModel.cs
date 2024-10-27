
namespace BTL_LTWeb.Models
{
    internal class SelectedItemViewModel
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        internal static IEnumerable<object> GetCartItems(int customerId)
        {
            throw new NotImplementedException();
        }
    }
}