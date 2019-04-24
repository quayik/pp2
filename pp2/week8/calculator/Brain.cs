using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculator
{
    enum CalcState
    {
        Zero,
        AccumulateDigits,
        Operation,
        Result
    }

    public delegate void ChangeTextDelegate(string text);
    

    class Brain
    {
        ChangeTextDelegate changeTextDelegate;
        CalcState calcState = CalcState.Zero;
        string tempNumber = "";
        string resultNumber = "";
        string operation = "";

        public Brain(ChangeTextDelegate changeTextDelegate)
        {
            this.changeTextDelegate = changeTextDelegate;
        }

        public void Process(string msg)
        {
            switch (calcState)
            {
                case CalcState.Zero:
                    Zero(msg, false);
                    break;
                case CalcState.AccumulateDigits:
                    AccumulateDigits(msg, false);
                    break;
                case CalcState.Operation:
                    // Operation(msg, false);
                    AccumulateDigits(msg, true);
                    break;
                case CalcState.Result:
                    AccumulateDigits(msg, false);
                    break;
                default:
                    break;
            }
        }

        void Zero(string msg, bool isInput)
        {
            if (isInput)
            {
                calcState = CalcState.Zero;
            }
            else
            {
                if (Rules.IsNonZeroDigit(msg))
                {
                    AccumulateDigits(msg, true);
                }
            }
        }

        void AccumulateDigits (string msg, bool isInput)
        {
            if (isInput)
            {
                calcState = CalcState.AccumulateDigits;
                tempNumber += msg;

                changeTextDelegate.Invoke(tempNumber);
            }
            else
            {
                if (Rules.IsDigit(msg))
                {
                    AccumulateDigits(msg, true);
                }
                else if (Rules.IsOperation(msg))
                {
                    Operation(msg, true);
                }
                else if (Rules.IsResult(msg))
                {
                    Result(msg, true);
                }
            }
        }

        void Operation(string msg, bool isInput)
        {
            
            if (isInput)
            {
                //C, <, ±

                if (msg == "C" || msg == "<" || msg == "±")
                {
                    if (msg == "C")
                    {
                        calcState = CalcState.Zero;
                        resultNumber = "";
                        tempNumber = "";
                    }

                    //barinde msg, operation ozgerip, PerformCalculation
                    //ishinde tappai ignor bop kalady

                    else if (msg == "<")
                    {
                        //resultNumber = tempNumber.Remove(tempNumber.Length - 1, 1);
                        tempNumber = tempNumber.Remove(tempNumber.Length - 1, 1);
                    }

                    else if (msg == "±")
                    {
                        int d = int.Parse(tempNumber);
                        tempNumber = (-1 * d).ToString();
                    }

                    changeTextDelegate.Invoke(tempNumber);
                }
                else if (Rules.IsUnaryOperation(msg))
                {
                    if (msg == "fib")
                    {
                        int input = int.Parse(tempNumber);
                        int Fib(int a)
                        {
                            if (a == 0) return 0;
                            else if (a == 1) return 1;
                            else return Fib(a - 1) + Fib(a - 2);
                        }

                        tempNumber = Fib(input).ToString();
                    }

                    else if(msg == "x^2")
                    {
                        tempNumber = ((int.Parse(tempNumber)) * (int.Parse(tempNumber))).ToString() ;
                    }

                    else if (msg == "x^3")
                    {
                        tempNumber = ((int.Parse(tempNumber)) * (int.Parse(tempNumber)) * int.Parse(tempNumber)).ToString();
                    }

                    else if (msg == "x!")
                    {
                        int n = int.Parse(tempNumber);
                        for (int i = 2; i < n; ++i)
                        {
                            tempNumber = ((int.Parse(tempNumber)) * i).ToString();
                        }
                        
                    }

                    changeTextDelegate.Invoke(tempNumber);
                }
                else
                {
                    operation = msg;
                    calcState = CalcState.Operation;

                    if (resultNumber != "" && tempNumber != "")
                    {
                        PerformCalculation();
                    }

                    else if (resultNumber == "")
                    {
                        resultNumber = tempNumber;
                    }

                    tempNumber = "";
                }
                
            }
          //  else
           // {
             //   AccumulateDigits(msg, true);
            //}
        }

        void Result(string msg, bool isInput)
        {
            if (isInput)
            {
                calcState = CalcState.Result;

                PerformCalculation();
                
                operation = "";

                changeTextDelegate.Invoke(resultNumber);
                

            }else if (Rules.IsOperation(msg))
            {
                Operation(msg, true);
            }
        }

        void PerformCalculation()
        {
            if (operation == "+")
            {
                resultNumber = (int.Parse(tempNumber) + int.Parse(resultNumber)).ToString();
            }

            else if (operation == "-")
            {
                resultNumber = (int.Parse(resultNumber) - int.Parse(tempNumber)).ToString();
            }

            else if (operation == "*")
            {
                resultNumber = (int.Parse(tempNumber) * int.Parse(resultNumber)).ToString();
            }

            else if (operation == "/")
            {
                resultNumber = (int.Parse(resultNumber) / int.Parse(tempNumber)).ToString();
            }

            else if (operation == "cop")
            {
                int x = new int();
                int IsCop(int a, int b)
                {
                    if (a > b) x = a;
                    else x = b;

                    for (int i = 2; i < x; i++)
                    {
                        if (a % i == 0 && b % i == 0 )
                        {
                            // for(int j = 2; j < b; ++j)
                            //{
                            //   if ((b % j == 0) && i == j)
                            //    {
                            //      return j;
                            //   }
                            // }
                            return i;
                        } 
                        
                    }
                    return 1;
                }
                resultNumber = IsCop(int.Parse(resultNumber), int.Parse(tempNumber)).ToString();
            }

            else if (operation == "x^y")
            {
                resultNumber = (Math.Pow(int.Parse(resultNumber), int.Parse(tempNumber))).ToString();
            }
        }
    }
}
