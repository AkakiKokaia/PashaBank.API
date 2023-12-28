using MediatR;
using Microsoft.EntityFrameworkCore;
using PashaBank.Application.Wrappers;
using PashaBank.Domain.Interfaces;

namespace PashaBank.Application.Features.Account.Commands.Bonuses
{
    public class CalculateBonusesAsyncCommandHandler : IRequestHandler<CalculateBonusesAsyncCommand, Response<List<CalculateBonusesAsyncResponseItem>>>
    {
        private readonly IUnitOfWork _uow;

        public CalculateBonusesAsyncCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Response<List<CalculateBonusesAsyncResponseItem>>> Handle(CalculateBonusesAsyncCommand request, CancellationToken cancellationToken)
        {
            var products = _uow.productSalesRepository.GetAllWhere(x => !x.WasCalculated && x.DateOfSale > request.startDate && x.DateOfSale < request.endDate && !x.IsDeleted).ToList();
            await _uow.userService.AccummulatedBonusCalculator(products);

            var allProducts = await _uow.productSalesRepository.GetAllWhere(x => x.DateOfSale > request.startDate && x.DateOfSale < request.endDate && !x.IsDeleted).ToListAsync();
            var users = await _uow.userService.GetUsersByProductSales(allProducts);
            
            return new Response<List<CalculateBonusesAsyncResponseItem>>(users.Select(x => new CalculateBonusesAsyncResponseItem(x.FirstName, x.Surname, x.DateOfBirth, x.Gender, x.PersonalNumber, x.AccummulatedBonus, x.RecommendedById)).ToList(), "success");
        }
    }
}