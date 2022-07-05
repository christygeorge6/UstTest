using System;
//balance Notes calulator
public class Program
{
    public static void Main(string[] args)
    {
        bool done = false;
        do
        {
            Console.WriteLine("Enter Product Amount in EUR");
            decimal prodVal = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Enter Amount given by Customer in EUR:");
            decimal amtRec = Convert.ToDecimal(Console.ReadLine());
            if (prodVal > amtRec)
            {
                Console.WriteLine("Product amount higher than Recevied Amount!!");
            }
            else
            {

                if (decimal.TryParse(prodVal.ToString(), out decimal balanceAmt))
                {
                    balanceAmt = amtRec - prodVal;

                    BankNotes bank = new BankNotes(balanceAmt);
                    Console.WriteLine("Your change is:");
                    for (int i = BankNotes.Notes.Length; i-- > 0;)
                    {
                        if (bank.NoteCount[i] > 0)
                        {
                            Console.WriteLine($"  {bank.NoteCount[i]} x EUR {BankNotes.Notes[i]}");
                        }
                    }
                    Console.WriteLine($"  1 x {bank.Remainder}p");
                }
                else
                {
                    done = true;
                }
            }
        } while (!done);
    }
    public struct BankNotes
    {
        public readonly static Decimal[] Notes = { 1m, 2m, 5m, 10m, 20m, 50m, 100m ,200m,500m };
        public static implicit operator BankNotes(decimal value) => new BankNotes(value);
        public static implicit operator decimal(BankNotes balanceAmt) => balanceAmt.Value;
        public BankNotes(decimal value)
        {
            // Initilize note counts to 0
            this.NoteCount = new int[Notes.Length];
            // Go from lagest note down to smallest
            for (int i = NoteCount.Length - 1; i >= 0; i--)
            {
                // calc count for each note
                NoteCount[i] = (int)(value / Notes[i]);
                // adjust balance based on notes counted
                value -= NoteCount[i] * Notes[i];
            }
            // set the remaining moneys
            Remainder = value;
        }
        public decimal Remainder { get; }
        // Array of count for each note.
        public int[] NoteCount { get; }
        // Total value of notes.
        public decimal NotesValue
            => Notes.Zip(NoteCount, (note, count) => count * note).Sum();
        // Tota value including remainder (for checK)
        public decimal Value
            => NotesValue + Remainder;
    }
}
