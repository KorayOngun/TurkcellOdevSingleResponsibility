using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;

namespace TurkcellOdevSingleResponsibility
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // https://www.hackerrank.com/challenges/validating-credit-card-number/problem?isFullScreen=true
            // _credit card number verification
            //► It must start with a 4,5 or 6. => StartNumber()
            //► It must contain exactly  digits(0-9).
            //► It must only consist of digits(-).
            //► It may have digits in groups of 4, separated by one hyphen "-".
            //► It must NOT use any other separator like ' ' , '_', etc.
            //► It must NOT have 4 or more consecutive repeated digits.
            string[] cards =
            {
                "42536258796157867", // invalid 17digits
                "5123-4567-8912-3456",  // valid
                "61234-567-8912-3456",  //  Invalid, because the card number is not divided into equal groups of 4 => 61234.
                "4123356789123456",     // valid
                "5133-3367-8912-3456", // Invalid, consecutive digits 3333 is repeating 4 times.
                "5123 - 3567 - 8912 - 3456" // Invalid, because space '  ' and - are used as separators.
            };

            CardControl cardControl = new CardControl("4123456789123456"); // valid
            Console.WriteLine(cardControl.Control());

            string result;

            foreach (string card in cards)
            {
                cardControl.ChangeCreditCard(card);
                result = cardControl.Control() ? "valid" : "invalid";
                Console.WriteLine($"{card} =>  {result}");
            }


        }
    }
}

class CardControl
{
    private string _credit;
    public CardControl(string credit = null)
    {
        _credit = credit;
    }
    public void ChangeCreditCard(string credit)
    {
        _credit = credit;
    }

    private bool StartNumber()
    {
        return _credit.StartsWith("4") ||
               _credit.StartsWith("5") ||
               _credit.StartsWith("6");
    }

    private bool Lenght()
    {
        return _credit.Count(x => x == '-') == 3 && _credit.Length == 19 ||
               _credit.Count(x => x == '-') == 0 && _credit.Length == 16;
    }


    private bool Sep()
    {
        if (_credit.Count(x => x == '-') == 3)
        {
            return Grouping();
        }
        else
        {
            return true;
        }
    }
    private bool Grouping()
    {
        return _credit.Split("-")[0].Length == _credit.Split("-")[1].Length &&
               _credit.Split("-")[2].Length == _credit.Split("-")[3].Length &&
               _credit.Split("-")[1].Length == _credit.Split("-")[2].Length;
    }



    private bool RepeatingNumber()
    {
        string c = _credit.Replace("-", "");
        if (c.Length == 16)
        {
            for (int i = 0; i < 12; i++)
            {
                bool isValid = c[i] == c[i + 1]  &&
                               c[i + 2] == c[i + 3] &&
                               c[i + 1] == c[i + 2];
                if (isValid)
                {
                    return false;
                }
            }
            return true;
        }
        return false;
    }

    private bool Numeric()
    {
        string _c = _credit.Replace("-", "");
        foreach (var c in _c)
        {
            if (!char.IsNumber(c))
            {
                return false;
            }
        }
        return true;
    }

    public bool Control()
    {
        bool isValid = StartNumber() &&
                        Lenght() &&
                        Sep() &&
                        RepeatingNumber() &&
                        Numeric();
        return isValid;
    }



}
