using NRMDesktopUI.library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NRMDesktopUI.library.API
{
    public interface IProductEndPoint
    {
        Task<List<ProductModel>> GetAll();
    }
}