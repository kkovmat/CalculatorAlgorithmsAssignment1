namespace Calculator;
class Token
{
    
    static public string operations = "+-*/^";
    static public string allNumbers = "1234567890";

    private string value;

    public string Value
    {
        get => value;
        private set
        {
            this.value = value;
        }
    }
    public Token()
    {
        
    }
    public Token(string value)
    {
        this.value = value;
    }

    static public CustomList<Token> Tokenization(string expression)
    {
        string nBuffer = "";
        string letBuffer = "";
        CustomList<Token> tokens = new CustomList<Token>();
        foreach (char el in expression.ToLower())
        {
            if (el == ' ')
            {
                if (nBuffer.Length > 0)
                {
                    Token token = new NumToken(nBuffer);
                    tokens.Add(token);
                    nBuffer = "";
                }
                continue;
            }
            else if (operations.Contains(el))
            {
                if (nBuffer.Length > 0)
                {
                    Token token = new NumToken(nBuffer);
                    tokens.Add(token);
                    nBuffer = "";
                }
                Token op = new OpToken(el.ToString());
                tokens.Add(op);
            }
            else if (allNumbers.Contains(el))
            {
                nBuffer += el.ToString();
            }
            else if (char.IsLetter(el))
            {
                letBuffer += el;
            }
            else if (el == ',')
            {
                if (nBuffer.Length > 0)
                {
                    Token num = new NumToken(nBuffer);
                    tokens.Add(num);
                    nBuffer = "";
                }
                Token comma = new CommaToken(",");
                tokens.Add(comma);
            }
            else if (el == '(' || el == ')') 
            {
                if (nBuffer.Length > 0)
                {
                    Token token = new NumToken(nBuffer);
                    tokens.Add(token);
                    nBuffer = "";
                }
                if (letBuffer.Length > 0)
                {
                    Func funcType;
                    switch (letBuffer)
                    {
                        case "sin":
                            funcType = Func.Sin;
                            break;
                        case "cos":
                            funcType = Func.Cos;
                            break;
                        case "max":
                            funcType = Func.Max;
                            break;
                        default:
                            throw new Exception("Invalid function.");

                    }
                    Token token = new FuncToken(letBuffer, funcType);
                    
                    tokens.Add(token);
                    letBuffer = "";
                }
                Token parenthesis = new OpToken(el.ToString());
                tokens.Add(parenthesis);
            }
        }
        if (nBuffer.Length > 0) 
        {
            Token token = new NumToken(nBuffer);
            tokens.Add(token);
        }
        return tokens;
    }
     
}
class NumToken : Token
{
    public NumToken(string value) : base(value) {}
}
class OpToken : Token
{
    private int priority = 0;
    public int Priority
    {
        get => priority;
        private set
        {
            priority = value;
        }
    }
    public OpToken()
    {
        
    }
    public OpToken(string value) : base(value)
    {
        priority = CheckPriority(value);
    }
    private int CheckPriority(string value)
    {
        switch(value)
        {
            case "(":
                priority = -1;
                break;
            case ")":
                priority = -1;
                break;
            case "+":
                priority = 1;
                break;
            case "-":
                priority = 1;
                break;
            case "*":
                priority = 2;
                break;
            case "/":
                priority = 2;
                break;
            case "^":
                priority = 3;
                break;
        }
        return priority;
    }
    
}
class FuncToken : Token
{
    private Func func;
    public Func Func
    {
        get => func;
        private set
        {
            func = value;
        }
    }
    public FuncToken(string value, Func func) : base(value)
    {
        this.func = func;
    }
}
class CommaToken : Token
{
    public CommaToken(string value) : base(value) {}
}
enum Func
{
    Sin,
    Cos,
    Max
}