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
                    Operation(msg, false);
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
                if (Rules.IsNonZeroDigit(msg[0]))
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
                if (Rules.IsDigit(msg[0]))
                {
                    AccumulateDigits(msg, true);
                }
                else if (Rules.IsOperation(msg[0]))
                {
                    Operation(msg, true);
                }
                else if (Rules.IsResult(msg[0]))
                {
                    Result(msg, true);
                }
            }
        }

        void Operation(string msg, bool isInput)
        {
            if (isInput)
            {
                calcState = CalcState.Operation;

                if (operation.Length != 0)
                {
                    PerformCalculation();
                    tempNumber = "";
                }

                if (resultNumber == "")
                {
                    resultNumber = tempNumber;
                }
                operation = msg;
                tempNumber = "";
            }
            else
            {
                AccumulateDigits(msg, true);
            }
        }

        void Result(string msg, bool isInput)
        {
            if (isInput)
            {
                calcState = CalcState.Result;

                PerformCalculation();
                
                operation = "";

                changeTextDelegate.Invoke(resultNumber);
                

            }else if (Rules.IsOperation(msg[0]))
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

            else if (operation == "C")
            {
                calcState = CalcState.Zero;
                resultNumber = "";
                tempNumber = "";
                
                
            }
        }
    }
}
