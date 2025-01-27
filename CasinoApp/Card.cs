namespace CasinoApp
{
    public class Card
    {
        public string Suit {  get; set; }
        public int Rank { get; set; }

        public Card(string Suit, int Rank)
        {
            this.Suit = Suit;
            this.Rank = Rank;
        }

        public override string ToString()
        {
            switch (Rank)
            {
                case 11:
                    return "Valet of " + Suit;
                case 12:
                    return "Queen of " + Suit;
                case 13:
                    return "King of " + Suit;
                case 14:
                    return "Ace of " + Suit;
                default:
                    return Rank + " of " + Suit;

            }
        }
    }
}
