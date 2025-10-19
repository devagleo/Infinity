using Currency.Application.Interfaces;
using Currency.Grpc.Mappers;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace Currency.Grpc.Services
{
    /// <summary>
    /// ������ ��������� �������� �������� � ������ �����
    /// </summary>
    public class CurrencyGrpcService : CurrencyProtoService.CurrencyProtoServiceBase
    {
        private readonly ICurrencyService _currencyService;
        private readonly IFavoriteService _favoriteService;
        private readonly ILogger<CurrencyGrpcService> _logger;


        public CurrencyGrpcService(ICurrencyService currencyService, IFavoriteService favoriteService, ILogger<CurrencyGrpcService> logger)
        {
            _currencyService = currencyService;
            _favoriteService = favoriteService;
            _logger = logger;
        }

        /// <summary>
        /// �������� ������ ���� �����
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<GetAllCurrenciesResponse> GetAllCurrencies(Empty request, ServerCallContext context)
        {
            var currencies = await _currencyService.GetAllAsync();
            var response = new GetAllCurrenciesResponse();
            response.Currencies.AddRange(currencies.Select(c => new CurrencyGrpcDTO
            {
                Id = c.Id.ToString(),
                Name = c.Name,
                Rate = (double)c.Rate,
            }));
            return response;
        }

        /// <summary>
        /// �������� ������ �� ID ����
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <exception cref="RpcException"></exception>
        public override async Task<CurrencyGrpcDTO> GetCurrencyById(GetCurrencyByIdRequest request, ServerCallContext context)
        {
            var model = await _currencyService.GetByIdAsync(Guid.Parse(request.Id));
            if (model == null) throw new RpcException(new Status(StatusCode.NotFound, "������ �� �������"));

            return new CurrencyGrpcDTO
            {
                Id = model.Id.ToString(),
                Name = model.Name,
                Rate = (double)model.Rate,
            };
        }

        /// <summary>
        /// �������� ������ ��������� ����� �������������
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<GetUserCurrenciesResponse> GetUserCurrencies(GetUserCurrenciesRequest request, ServerCallContext context)
        {
            var currencies = await _favoriteService.GetUserFavoritesAsync(Guid.Parse(request.UserId));
            var response = new GetUserCurrenciesResponse();
            response.Currencies.AddRange(currencies.Select(c => new CurrencyGrpcDTO
            {
                Id = c.Id.ToString(),
                Name = c.Name,
                Rate = (double)c.Rate
            }));

            return response;
        }

        /// <summary>
        /// �������� ������������ ��������� ������
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<ModifyFavoriteCurrencyResponse> AddFavoriteCurrency(ModifyFavoriteCurrencyRequest request, ServerCallContext context)
        {
            try
            {
                await _favoriteService.AddFavoriteAsync(Guid.Parse(request.UserId), Guid.Parse(request.CurrencyId));
                return new ModifyFavoriteCurrencyResponse { Success = true, Message = "Added" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "������ ���������� ����� ������");
                return new ModifyFavoriteCurrencyResponse { Success = false, Message = ex.Message };
            }
        }

        /// <summary>
        /// ������� � ������������ ��������� ������
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<ModifyFavoriteCurrencyResponse> RemoveFavoriteCurrency(ModifyFavoriteCurrencyRequest request, ServerCallContext context)
        {
            try
            {
                await _favoriteService.RemoveFavoriteAsync(Guid.Parse(request.UserId), Guid.Parse(request.CurrencyId));
                return new ModifyFavoriteCurrencyResponse { Success = true, Message = "Removed" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "������ �������� ��������� ������");
                return new ModifyFavoriteCurrencyResponse { Success = false, Message = ex.Message };
            }
        }
    }
}
