using System.Threading.Tasks;

namespace DrinkFinder.Infrastructure.Vat
{
    public interface IVatService
    {
        Task<VatResponse> Validate(string vatNumber);
    }
}
