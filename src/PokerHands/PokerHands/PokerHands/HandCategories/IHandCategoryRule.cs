namespace PokerHands.HandCategories
{
    internal interface IHandCategoryRule
    {
        HandCategory HandCategory { get; }

        bool Match();
    }
}