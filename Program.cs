namespace Calculator;
class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0) args = ["3 * (5 + 2) - 2"];

        Console.WriteLine("\nStart");
        
        Console.WriteLine("\nTokenization.");
       
        CustomList<Token> tokens = Token.Tokenization(args[0]);
        for (int i = 0; i < tokens.Length(); i++)
        {
            Console.Write(tokens.GetAt(i).Value + " ");
        }
        Console.WriteLine("\nPostfix.");
        CustomQueue<Token> postfixTokens = ShuntingYard.Postfix(tokens);
        CustomQueue<Token> postfixTokens2 = new CustomQueue<Token>();
        for (int i = 0; i < postfixTokens.Count(); i++)
        {
            postfixTokens2.Enqueue(postfixTokens.GetAt(i));
        }
        for (int i = 0; i < postfixTokens.Count(); i++)
        {
            Console.Write(postfixTokens.GetAt(i).Value + " ");
        }

        Console.WriteLine("\nCalculate.");
        double result = ShuntingYard.Calculate(postfixTokens);
        Console.WriteLine(result); 

        

    }
}