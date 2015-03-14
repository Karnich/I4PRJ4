using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace DAL.IntegrationTestProgram
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Model.DAL myDb = new Model.DAL();

            

            Console.WriteLine("Inserting 3 Quizzes");
            Model.Quiz firstQuiz = new Model.Quiz{ QuizName = "FirstQuiz"};
            Console.WriteLine(firstQuiz.QuizName);
            Model.Quiz secondQuiz = new Model.Quiz { QuizName = "SecondQuiz" };
            Model.Quiz thirdQuiz = new Model.Quiz { QuizName = "ThirdQuiz" };

            myDb.InsertQuiz(firstQuiz);
            myDb.InsertQuiz(secondQuiz);
            myDb.InsertQuiz(thirdQuiz);


            //Console.WriteLine("Inserting 2 Questions into Quiz: FirstQuiz");
            //Model.Question firstQuestion = new Model.Question { Text = "This is the first question in FirstQuiz, Or is it?", QuizId = firstQuiz.QuizId};
            //Model.Question secondQuestion = new Model.Question { Text = "This is the third question in FirstQuiz, Or is it?", QuizId = firstQuiz.QuizId };
            //myDb.InsertQuestion(firstQuestion);
            //myDb.InsertQuestion(secondQuestion);

        }
    }
}
