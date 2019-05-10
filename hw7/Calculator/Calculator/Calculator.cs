﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Calculator : Form
    {
        public Calculator()
        {
            InitializeComponent();
        }

        private void NumeralButtonWasPressed(object sender)
        {
            textBoxExpression.Text += (sender as Button).Text;
        }

        private void OperationButtonWasPressed(object sender)
        {
            textBoxExpression.Text += " " + (sender as Button).Text + " ";
        }

        private void OperatorWasPressedWhenTheTextBoxExpressionIsEmpty()
        {
            textBoxExpression.Text += "0";
        }

        private double CalculateExpression(double operandLeft, char operation, double operandRight)
        {
            double result = 0;
            switch (operation)
            {
                case '+':
                    result = operandLeft + operandRight;
                    break;
                case '-':
                    result = operandLeft - operandRight;
                    break;
                case '*':
                    result = operandLeft * operandRight;
                    break;
                case '/':
                    try
                    {
                        result = operandLeft / operandRight;
                    }
                    catch(DivideByZeroException)
                    {
                        textBoxExpression.Text = "Division by zero is not allowed";
                    }
                    break;
                default:
                    throw new ArgumentException("Argument \"char operation\" is not operation!");
            }
            return result;
        }

        private bool IsOperator(char operation)
        {
            return operation == '+' || operation == '-' || operation == '*' || operation == '/';
        }

        private void ParseLeftOperandToDouble(out double operandLeft)
        {
            // left operand ends before the first space
            int lastPosOfTheLeftOperand = textBoxExpression.Text.IndexOf(' ') - 1;
            string leftOperand = textBoxExpression.Text.Substring(0, lastPosOfTheLeftOperand + 1);
            if (!double.TryParse(leftOperand, out operandLeft))
            {
                throw new FormatException("Invalid expression!");
            }
        }

        private void ParseRightOperandToDouble(out double operandRight)
        {
            int firstPosOfTheRightOperand = RightOperandFirstPosition();
            int lastPosOfTheRightOperand = RightOperandLastPosition();

            string rightOperand = textBoxExpression.Text.Substring(firstPosOfTheRightOperand, 
                    lastPosOfTheRightOperand - firstPosOfTheRightOperand + 1);
            if (!double.TryParse(rightOperand, out operandRight))
            {
                throw new FormatException("Invalid expression!");
            }
        }

        private int RightOperandFirstPosition()
        {
            // right operand starts after the second space
            int firstPosOfTheRightOperand = textBoxExpression.Text.IndexOf(' ') + 3;
            return firstPosOfTheRightOperand;
        }

        private int RightOperandLastPosition()
        {
            int lastPosOfTheRightOperand = textBoxExpression.Text.Length - 1;
            return lastPosOfTheRightOperand;
        }

        private void TextBoxExpressionIncludesAnOperatorWithTwoOperandsCalculate()
        {
            double operandLeft;
            double operandRight;
            ParseLeftOperandToDouble(out operandLeft);
            ParseRightOperandToDouble(out operandRight);

            double result;
            if (textBoxExpression.Text.Contains('+'))
            {
                result = CalculateExpression(operandLeft, '+', operandRight);
            }
            else if (textBoxExpression.Text.Contains('-'))
            {
                result = CalculateExpression(operandLeft, '-', operandRight);
            }
            else if (textBoxExpression.Text.Contains('*'))
            {
                result = CalculateExpression(operandLeft, '*', operandRight);
            }
            else
            {
                result = CalculateExpression(operandLeft, '/', operandRight);
            }
            int lastPositionOfTheRightOperand = RightOperandLastPosition();
            if (!(textBoxExpression.Text == "Division by zero is not allowed"))
            {
                textBoxExpression.Text = textBoxExpression.Text.Remove(0, lastPositionOfTheRightOperand + 1);
                textBoxExpression.Text = result.ToString() + textBoxExpression.Text;
            }
        }

        private void RemovesTheLastElementIfIsAnOperator()
        {
            // check if in the textBoxExpression (not to count spaces) could be an operator
            if (textBoxExpression.Text.Count() > 2)
            {
                int position = textBoxExpression.Text.Count() - 1;
                char symbolToCheckIfIsOperator = textBoxExpression.Text[position - 1];

                // if the last element is an operator then remove it from theTextBoxExpression
                if (IsOperator(symbolToCheckIfIsOperator))
                {
                    textBoxExpression.Text = textBoxExpression.Text.Remove(position - 2, 3);
                }
            }
        }

        private void OperatorClickHandler(object sender)
        {
            RemovesTheLastElementIfIsAnOperator();

            // discribes the situation when the textBoxExpresion already includes operator
            if (textBoxExpression.Text.Contains('+') || textBoxExpression.Text.Contains('-') || textBoxExpression.Text.Contains('*') ||
                    textBoxExpression.Text.Contains('/'))
            {
                TextBoxExpressionIncludesAnOperatorWithTwoOperandsCalculate();
            }

            // the situation when the operator was pressed when the textBoxExpression was empty
            if (textBoxExpression.Text.Count() == 0)
            {
                OperatorWasPressedWhenTheTextBoxExpressionIsEmpty();
            }

            // describes the situation when the comma is the last element
            if (textBoxExpression.Text[textBoxExpression.Text.Count() - 1] == ',')
            {
                textBoxExpression.Text += "0";
            }

            OperationButtonWasPressed(sender);
        }

        private void NumberClickHandler(object sender)
        {
            // trying to understand what operator is being written (left or right)

            // the right one
            if (textBoxExpression.Text.Contains('+') || textBoxExpression.Text.Contains('-') || textBoxExpression.Text.Contains('*') ||
                    textBoxExpression.Text.Contains('/'))
            {
                // find position of the last space after which the second operator is being written
                int posOfTheLastSpace = textBoxExpression.Text.LastIndexOf(' ');

                // remove insignificant zero in the beginning of the right operand
                if (posOfTheLastSpace < textBoxExpression.Text.Count() - 1 && textBoxExpression.Text[posOfTheLastSpace + 1] == '0' &&
                        textBoxExpression.Text.Count() == posOfTheLastSpace + 2)
                {
                    textBoxExpression.Text = textBoxExpression.Text.Remove(textBoxExpression.Text.Count() - 1);
                }
            }
            // the left one
            else if (textBoxExpression.Text.Count() > 0)
            {
                if (!textBoxExpression.Text.Contains(',') && textBoxExpression.Text[0] == '0')
                {
                    textBoxExpression.Clear();
                }
            }

            NumeralButtonWasPressed(sender);
        }

        private void Button0_Click(object sender, EventArgs e)
        {
            NumberClickHandler(sender);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            NumberClickHandler(sender);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            NumberClickHandler(sender);
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            NumberClickHandler(sender);
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            NumberClickHandler(sender);
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            NumberClickHandler(sender);
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            NumberClickHandler(sender);
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            NumberClickHandler(sender);
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            NumberClickHandler(sender);
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            NumberClickHandler(sender);
        }

        private void ButtonComma_Click(object sender, EventArgs e)
        {
            if (textBoxExpression.Text.Count() == 0)
            {
                textBoxExpression.Text += "0,";
            }
            // the last element (not to count spaces) is an operator
            else if (textBoxExpression.Text.Count() > 3 && IsOperator(textBoxExpression.Text[textBoxExpression.Text.Count() - 2]))
            {
                textBoxExpression.Text += "0,";
            }
            // check if the left operator already has a comma
            else if (!textBoxExpression.Text.Contains('+') && !textBoxExpression.Text.Contains('-') && !textBoxExpression.Text.Contains('*')
                    && !textBoxExpression.Text.Contains('/'))
            {
                if (!textBoxExpression.Text.Contains(','))
                {
                    textBoxExpression.Text += ',';
                }
            }
            // check if the right operator already has a comma
            else
            {
                int posOfTheOperator;
                if (textBoxExpression.Text.Contains('+'))
                {
                    posOfTheOperator = textBoxExpression.Text.IndexOf('+');
                }
                else if (textBoxExpression.Text.Contains('-'))
                {
                    posOfTheOperator = textBoxExpression.Text.IndexOf('-');
                }
                else if (textBoxExpression.Text.Contains('*'))
                {
                    posOfTheOperator = textBoxExpression.Text.IndexOf('*');
                }
                else
                {
                    posOfTheOperator = textBoxExpression.Text.IndexOf('/');
                }
                bool flagTheRightOperatorHasAComma = false;
                for (int i = posOfTheOperator + 2; i < textBoxExpression.Text.Count(); ++i)
                {
                    if (textBoxExpression.Text[i] == ',')
                    {
                        flagTheRightOperatorHasAComma = true;
                        break;
                    }
                }
                if (!flagTheRightOperatorHasAComma)
                {
                    textBoxExpression.Text += ',';
                }
            }            
        }

        private void ButtonDivide_Click(object sender, EventArgs e)
        {
            OperatorClickHandler(sender);
        }

        private void ButtonMultiply_Click(object sender, EventArgs e)
        {
            OperatorClickHandler(sender);
        }

        private void ButtonMinus_Click(object sender, EventArgs e)
        {
            OperatorClickHandler(sender);
        }

        private void ButtonPlus_Click(object sender, EventArgs e)
        {
            OperatorClickHandler(sender);
        }

        private void ButtonEqually_Click(object sender, EventArgs e)
        {
            if (textBoxExpression.Text.Count() == 0)
            {
                textBoxExpression.Text += "0";
            }
            else if (textBoxExpression.Text.Count() > 3 && IsOperator(textBoxExpression.Text[textBoxExpression.Text.Count() - 2]))
            {
                // duplicate the left operator if there is no right one
                textBoxExpression.Text += textBoxExpression.Text.Substring(0, textBoxExpression.Text.Count() - 3);
                TextBoxExpressionIncludesAnOperatorWithTwoOperandsCalculate();
            }
            else if (char.IsDigit(textBoxExpression.Text[textBoxExpression.Text.Count() - 1]))
            {
                if (textBoxExpression.Text.Contains('+') || textBoxExpression.Text.Contains('-') || textBoxExpression.Text.Contains('*')
                        || textBoxExpression.Text.Contains('/'))
                {
                    TextBoxExpressionIncludesAnOperatorWithTwoOperandsCalculate();
                }
            }            
        }

        private void ButtonClear_Click(object sender, EventArgs e)
        {
            textBoxExpression.Clear();
        }
    }
}