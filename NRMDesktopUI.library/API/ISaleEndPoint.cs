using NRMDesktopUI.library.Models;
using System.Threading.Tasks;

namespace NRMDesktopUI.library.API
{
    public interface ISaleEndPoint
    {
        Task PostSale(SaleModel sale);
    }
}