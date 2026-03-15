namespace Calculator;
class ShuntingYard
{
    public static CustomQueue<Token> Postfix(CustomList<Token> tokens)
    {
        CustomQueue<Token> outQ = new CustomQueue<Token>();
        CustomStack<Token> opStack = new CustomStack<Token>();
        for(int i = 0; i < tokens.Length(); i++)
        {
            Token token = tokens.GetAt(i); 
            if (token is NumToken num)
            {
                outQ.Enqueue(num);
            }
            else if (token is FuncToken func)
            {
                opStack.Push(func);
            }
            else if (token is OpToken op1 && op1.Priority > 0)
            {
                while (opStack.Count() > 0 && opStack.Peek() is OpToken)
                {
                    OpToken op2 = (OpToken)opStack.Peek();
                    if (op2.Priority > op1.Priority || (op1.Priority == op2.Priority && op1.Value != "^"))
                    {
                        outQ.Enqueue(opStack.Pull());   
                    }
                    else
                    {
                        break;
                    }
                }
                opStack.Push(op1);
            }
            else if (token is CommaToken comma)
            {
                while (opStack.Count() > 0 && opStack.Peek().Value != "(")
                {
                    outQ.Enqueue(opStack.Pull());
                }
            }
            else if (token is OpToken par && par.Priority == -1)
            {
                if (par.Value == "(")
                {
                    opStack.Push(par);    
                }
                else if (par.Value == ")")
                {
                    while (opStack.Count() > 0)
                    {
                        OpToken op = (OpToken)opStack.Peek();
                        if (op.Value != "(")
                        {
                            outQ.Enqueue(opStack.Pull());
                        }
                        else
                        {
                            opStack.Pull();
                            if (opStack.Peek() is FuncToken)
                            {
                                outQ.Enqueue(opStack.Pull());
                            }
                            break;
                        }    
                    }                
                }
                    
            }
        }
        while (opStack.Count() > 0)
        {
            outQ.Enqueue(opStack.Pull());
        }
        return outQ;
    }
    public static double Calculate(CustomQueue<Token> tokens)
    {
        double result = 0;
        CustomStack<Token> s = new CustomStack<Token>();
        int count = tokens.Count();
        for (int i = 0; i < count; i++)
        {
            Token token = tokens.Dequeue();

            if (token is NumToken)
            {
                s.Push(token);
               
            }
            else if (token is OpToken)
            {
                double num1 = Convert.ToDouble(s.Pull().Value);
                double num2 = Convert.ToDouble(s.Pull().Value);
    
                switch (token.Value)
                {                    
                    case "+":
                        result = num2 + num1; 
                        break;
                    case "-":
                        result = num2 - num1; 
                        break;
                    case "*":
                        result = num2 * num1; 
                        break;
                    case "/":
                        result = num2 / num1; 
                        break;
                    case "^":
                        result = Math.Pow(num2, num1); 
                        break;

                }
                s.Push(new Token(result.ToString()));
                
            }
            else if (token is FuncToken func)
            {
                double num1 = Convert.ToDouble(s.Pull().Value);
                switch (func.Func)
                {                    
                    case Func.Sin:
                        result = Math.Sin(num1); 
                        break;
                    case Func.Cos:
                        result = Math.Cos(num1); 
                        break;
                    case Func.Max:
                        double num2 = Convert.ToDouble(s.Pull().Value);
                        result = Math.Max(num1, num2); 
                        break;

                }
                s.Push(new Token(result.ToString()));
            }
        }

        return result; 
    }
}
