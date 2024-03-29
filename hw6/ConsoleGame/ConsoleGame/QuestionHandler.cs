﻿using System;

namespace ConsoleGame
{
    /// <summary>
    /// Handles Question's actions.
    /// </summary>
    public class QuestionHandler
    {
        private Map map;
        private Question question;
        
        /// <summary>
        /// Initializes an object of the QuestionHandler class.
        /// </summary>
        public QuestionHandler(string path)
        {
            map = new Map(path);
            question = new Question(map.StartingPosition.top, map.StartingPosition.left);

            Console.CursorVisible = false;
            Console.WriteLine("Use arrows to move Question!\n\n");

            map.Display();
            question.Display();
        }

        /// <summary>
        /// Handles the movement to the left.
        /// </summary>
        public void LeftMovement(object sender, EventArgs e)
        {
            if (map[question.TopPosition, question.LeftPosition - 1] == ' ')
            {
                map.Display(question.TopPosition, question.LeftPosition);
                question.Left();
                question.Display();
                MovedLeft = true;
            }
            else
            {
                MovedLeft = false;
            }
        }

        /// <summary>
        /// Handles the movement to the right.
        /// </summary>
        public void RightMovement(object sender, EventArgs e)
        {
            if (map[question.TopPosition, question.LeftPosition + 1] == ' ')
            {
                map.Display(question.TopPosition, question.LeftPosition);
                question.Right();
                question.Display();
                MovedRight = true;
            }
            else
            {
                MovedRight = false;
            }
        }

        /// <summary>
        /// Handles upward movement.
        /// </summary>
        public void UpMovement(object sender, EventArgs e)
        {
            if (map[question.TopPosition - 1, question.LeftPosition] == ' ')
            {
                map.Display(question.TopPosition, question.LeftPosition);
                question.Up();
                question.Display();
                MovedUp = true;
            }
            else
            {
                MovedUp = false;
            }
        }

        /// <summary>
        /// Handles the downward movement.
        /// </summary>
        public void DownMovement(object sender, EventArgs e)
        {
            if (map[question.TopPosition + 1, question.LeftPosition] == ' ')
            {
                map.Display(question.TopPosition, question.LeftPosition);
                question.Down();
                question.Display();
                MovedDown = true;
            }
            else
            {
                MovedDown = false;
            }
        }

        public bool MovedDown { get; private set; } = false;

        public bool MovedUp { get; private set; } = false;

        public bool MovedLeft { get; private set; } = false;

        public bool MovedRight { get; private set; } = false;
    }
}
