using PashaBank.Application.Features.Helpers;
using PashaBank.Domain.Enums;

namespace PashaBank.Application.Features.Account.Commands.Bonuses
{
    public class CalculateBonusesAsyncResponseItem
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public GenderTypeEnum Gender { get; set; }
        public long PersonalNumber { get; set; }
        public decimal AccummulatedBonus { get; set; }
        public Guid? RecommendedById { get; set; }

        public CalculateBonusesAsyncResponseItem(string firstName, string surname, DateTimeOffset dateOfBirth, GenderTypeEnum gender, long personalNumber, decimal accummulatedBonus, Guid? recommendedById)
        {
            FirstName = firstName;
            Surname = surname;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            PersonalNumber = personalNumber;
            AccummulatedBonus = accummulatedBonus;
            RecommendedById = recommendedById;
        }
    }
}
