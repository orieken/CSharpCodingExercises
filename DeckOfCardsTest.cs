namespace CSharpCodingExercises;
using NUnit.Framework.Constraints;
public enum CardValue
{
    Two = 2,
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Nine,
    Ten,
    Jack,
    Queen,
    King,
    Ace
}

public enum Suit
{
    Heart,
    Club,
    Diamond,
    Spade
}

public class Card
{
    public Suit Suit;
    public CardValue Value;
    
    public Card(Suit suit, CardValue value)
    {
        this.Suit = suit;
        this.Value = value;
    }
    
    public override string ToString()
    {
        return $"{this.Value} of {this.Suit}s";
    }
}

public class Deck
{
    public List<Card> Cards;

    public Deck()
    {
        CreateCards();
    }

    private void CreateCards()
    {
        Suit[] suits = (Suit[])Enum.GetValues(typeof(Suit));
        CardValue[] cardValues = (CardValue[])Enum.GetValues(typeof(CardValue));
        Cards = new List<Card>();
        foreach (Suit suit in suits)
        {
            foreach (CardValue cardValue in cardValues)
            {
                Cards.Add(new Card(suit, cardValue));
            }
        }
    }

    public void Shuffle()
    {
        this.Cards = this.Cards.OrderBy(x => Guid.NewGuid()).ToList();
    }

    public Card dealCard()
    {
        Card card = this.Cards[0];
        this.Cards.RemoveAt(0);
        return card;
    }

    public List<Card> DealHand(int i)
    {
        List<Card> hand = new List<Card>();
        if (this.Cards.Count < i)
        {
            throw new Exception("Not enough cards in deck");
        }
        for (int j = 0; j < i; j++)
        {
            hand.Add(dealCard());
        }

        return hand;
    }
}

public class DeckOfCardsTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void DeckHas52Cards()
    {
        var deck = new Deck();
        
        Assert.That(deck.Cards.Count, Is.EqualTo(52));
    }

    [Test]
    public void DeckCanBeShuffled()
    {
        var deck = new Deck();
        var originalDeck = new List<Card>(deck.Cards);
        deck.Shuffle();
        var shuffledDeck = deck.Cards;
        Assert.That(originalDeck, Is.Not.EqualTo(shuffledDeck));
    }

    [Test]
    public void DeckCanDealACard()
    {
        var deck = new Deck();
        deck.Shuffle();
        var card = deck.dealCard();
        Assert.That(card, Is.Not.Null);
    }

    [Test]
    public void DeckCanDealAHand()
    {
        var deck = new Deck();
        deck.Shuffle();
        var cards = deck.DealHand(4);
        Assert.That(cards.Count, Is.EqualTo(4));
    }
    
    [Test]
    public void DeckCanNotDealMoreCardsThanItHas()
    {
        var deck = new Deck();
        deck.Shuffle();
        Assert.Throws<Exception>(() => deck.DealHand(53));
    }
}