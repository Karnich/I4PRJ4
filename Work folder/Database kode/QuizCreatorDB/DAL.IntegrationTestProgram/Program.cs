using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
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

            myDb.Reset();
            Console.WriteLine("\n::: DB RESET. PRESS ANY KEY TO START TEST :::");
            Console.ReadKey();

            #region TESTING Quiz CRUD
            Console.WriteLine("\n::: TESTING Quiz CRUD :::");
            Console.WriteLine("::: INSERT TEST :::");
            Console.WriteLine("Inserting 3 Quizzes, Refresh DB and inspect.\nPress any key to continue");
            Model.Quiz firstQuiz = new Model.Quiz{ QuizName = "FirstQuiz"};
            Model.Quiz secondQuiz = new Model.Quiz { QuizName = "SecondQuiz" };
            Model.Quiz thirdQuiz = new Model.Quiz { QuizName = "ThirdQuiz" };
            firstQuiz = myDb.InsertQuiz(firstQuiz);
            secondQuiz = myDb.InsertQuiz(secondQuiz);
            thirdQuiz = myDb.InsertQuiz(thirdQuiz);
            Console.ReadKey();

            Console.WriteLine("\n::: DELETE TEST :::");
            Console.WriteLine("Deleting SecondQuiz, refresh DB and inspect.\nPress any key to continue");
            myDb.DeleteQuiz(secondQuiz.QuizId);
            Console.ReadKey();

            Console.WriteLine("\n::: GET AND UPDATE TEST :::");
            Console.WriteLine("Getting the quiz with id = 1 and updating that quiz to be named UpdatedQuiz.\nPress any key to continue");
            Model.Quiz newQ = myDb.GetQuiz(1);
            newQ.QuizName = "UpdatedQuiz";
            myDb.UpdateQuiz(newQ);
            Console.ReadKey();

            #endregion

            #region TESTING Question CRUD
            Console.WriteLine("\n::: TESTING Question CRUD :::");
            Console.WriteLine("::: INSERT TEST :::");
            Console.WriteLine("Inserting 3 Questions into Quiz: ThirdQuiz, Refresh DB and inspect.\nPress any key to continue");
            Model.Question firstQuestion = new Model.Question { Text = "This is the first question in ThirdQuiz, Or is it?", QuizId = thirdQuiz.QuizId};
            Model.Question secondQuestion = new Model.Question { Text = "This is the third question in ThirdQuiz, Or is it?", QuizId = thirdQuiz.QuizId };
            Model.Question thirdQuestion = new Model.Question { Text = "This is the fifth question in ThirdQuiz, Or is it?", QuizId = thirdQuiz.QuizId };
            myDb.InsertQuestion(firstQuestion);
            myDb.InsertQuestion(secondQuestion);
            myDb.InsertQuestion(thirdQuestion);
            Console.ReadKey();

            Console.WriteLine("\n::: DELETE TEST :::");
            Console.WriteLine("Deleting the Quesiton with id = 2, Refresh DB and inspect.\nPress any key to continue");
            myDb.DeleteQuestion(2);
            Console.ReadKey();

            Console.WriteLine("\n::: GET AND UPDATE TEST :::");
            Console.WriteLine("Getting the Quesiton with id = 1 and updating the question to UpdatedQuestion, Refresh DB and inspect.\nPress any key to continue");
            Model.Question newQuestion = myDb.GetQuestion(1);
            newQuestion.Text = "UpdatedQuestion";
            myDb.UpdateQuestion(newQuestion);
            Console.ReadKey();

            #endregion

            #region TESTING Answer CRUD
            Console.WriteLine("\n::: TESTING Answer CRUD :::");
            Console.WriteLine("\n::: INSERT TEST :::");
            Console.WriteLine("Adding 4 answers to Question with id = 3, Refresh DB and inspect.\nPress any key to continue\n");
            Model.Answer firstAnswer = new Model.Answer {QuestionId = thirdQuestion.QuestionId, Correct = false, Text = "Yes it is the third question"};
            Model.Answer secondAnswer = new Model.Answer {QuestionId = thirdQuestion.QuestionId, Correct = true, Text = "No this is the second question"};
            Model.Answer thirdAnswer = new Model.Answer {QuestionId = thirdQuestion.QuestionId, Correct = false, Text = "No this is the fourth question"};
            Model.Answer fourthAnswer = new Model.Answer {QuestionId = thirdQuestion.QuestionId, Correct = false, Text = "I havent seen no questions"};
            myDb.InsertAnswer(firstAnswer);
            myDb.InsertAnswer(secondAnswer);
            myDb.InsertAnswer(thirdAnswer);
            myDb.InsertAnswer(fourthAnswer);
            Console.ReadKey();

            Console.WriteLine("\n::: DELETE TEST :::");
            Console.WriteLine("Deleting the Answer with id = 3, Refresh DB and inspect.\nPress any key to continue");
            myDb.DeleteAnswer(3);
            Console.ReadKey();

            Console.WriteLine("\n::: GET AND UPDATE TEST :::");
            Console.WriteLine("Getting the Answer with id = 4 and updating the Answer to UpdatedAnswer and Correctness to true(1), Refresh DB and inspect.\nPress any key to continue");
            Model.Answer newAnswer = myDb.GetAnswer(4);
            newAnswer.Text = "UpdatedAnswer";
            newAnswer.Correct = true;
            myDb.UpdateAnswer(newAnswer);
            Console.ReadKey();
            #endregion

            #region TESTING Tag CRUD
            Console.WriteLine("\n::: TESTING Tag CRUD :::");
            Console.WriteLine("\n::: INSERT TEST :::");
            Console.WriteLine("Inserting 3 Tags, Refresh DB and inspect.\nPress any key to continue\n");
            Tag firstTag = new Tag {Text = "FirstTag"};
            Tag secondTag = new Tag { Text = "SecondTag" };
            Tag thirdTag = new Tag { Text = "ThirdTag" };
            firstTag = myDb.InsertTag(firstTag);
            secondTag = myDb.InsertTag(secondTag);
            thirdTag = myDb.InsertTag(thirdTag);
            Console.ReadKey();

            Console.WriteLine("\n::: DELETE TEST :::");
            Console.WriteLine("Deleting Tag with id = 3, Refresh DB and inspect.\nPress any key to continue\n");
            myDb.DeleteTag(3);
            Console.ReadKey();

            Console.WriteLine("\n::: GET AND UPDATE TEST :::");
            Console.WriteLine("Getting the Tag with id = 1 and the tag with the name SecondTag and updating them to to UpdatedTagById and UpdatedTagByName, Refresh DB and inspect.\nPress any key to continue");
            Tag newTag = myDb.GetTagById(1);
            Tag newTag2 = myDb.GetTagByName("SecondTag");
            newTag.Text = "UpdatedTagById";
            newTag2.Text = "UpdatedTagByName";
            myDb.UpdateTag(newTag);
            myDb.UpdateTag(newTag2);
            Console.ReadKey();
            #endregion

            #region TESTING QuizTagRelation CRUD
            Console.WriteLine("\n::: TESTING QuizTagRelation CRUD :::");
            Console.WriteLine("\n::: INSERT TEST :::");
            Console.WriteLine("Inserting 4 QuizTagRelations. We have 2 Quizzes and 2 Tags. Refresh DB and inspect.\nPress any key to continue");
            QuizTagRelation firstRelation = new QuizTagRelation {TagId = firstTag.TagId, QuizId = firstQuiz.QuizId};
            QuizTagRelation secondRelation = new QuizTagRelation { TagId = firstTag.TagId, QuizId = thirdQuiz.QuizId };
            QuizTagRelation thirdRelation = new QuizTagRelation { TagId = secondTag.TagId, QuizId = firstQuiz.QuizId };
            QuizTagRelation fourthRelation = new QuizTagRelation { TagId = secondTag.TagId, QuizId = thirdQuiz.QuizId };
            myDb.InsertRelation(firstRelation);
            myDb.InsertRelation(secondRelation);
            myDb.InsertRelation(thirdRelation);
            myDb.InsertRelation(fourthRelation);
            Console.ReadKey();

            Console.WriteLine("\n::: DELETE TEST :::");
            Console.WriteLine("Deleting the QuizTagRelation with TagId = 1 and QuizId = 1, Refresh DB and inspect.\nPress any key to continue");
            myDb.DeleteRelation(firstRelation);
            Console.ReadKey();

            Console.WriteLine("\n::: UPDATE TEST :::");
            Console.WriteLine("Updating the QuizTagRelation with TagId = 1 and QuizId = 3. Should now be TagId = 1 and QuizId = 1, Refresh DB and inspect.\nPress any key to continue");
            myDb.UpdateRelation(secondRelation, new QuizTagRelation{TagId = 1, QuizId = 1});
            Console.ReadKey();

            Console.WriteLine("\n::: GET TEST :::");
            Console.WriteLine("Getting QuizTagRelation through tagId and QuizId and outputting them in the in console");
            QuizTagRelation throughTag = myDb.GetRelationThroughTag(2);
            QuizTagRelation throughQuiz = myDb.GetRelationThroughQuiz(3);
            Console.WriteLine("GetRelationThroughTag(2) has QuizId {0} and TagId {1}", throughTag.QuizId, throughTag.TagId);
            Console.WriteLine("GetRelationThroughQuiz(3) has QuizId {0} and TagId {1}", throughQuiz.QuizId, throughQuiz.TagId);
            #endregion

            Console.WriteLine("\nThis is the end of the test, Press any key to continue");
            Console.ReadKey();

        }
    }
}
