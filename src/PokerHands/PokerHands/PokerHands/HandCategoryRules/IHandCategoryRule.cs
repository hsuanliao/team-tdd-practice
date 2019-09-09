namespace PokerHands.HandCategoryRules
{
    internal interface IHandCategoryRule
    {
        HandCategory HandCategory { get; }
        bool Match();
    }
}