using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class DAL
    {
        private SqlConnection _conn;

        public DAL()
        {
            _conn =
                 new SqlConnection(@"Data Source=10.29.0.29;Integrated Security=False;User ID=F15I4PRJ4Gr3;Password=F15I4PRJ4Gr3;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");
        }

        #region QuizTable

        public Quiz InsertQuiz(Quiz quiz)
        {
            try
            {
                _conn.Open();

                string insertStringParam;
                insertStringParam = @"INSERT INTO [Quiz] (QuizName)
                                                    OUTPUT INSERTED.QuizId
                                                    VALUES (@quizName)";

                using (SqlCommand cmd = new SqlCommand(insertStringParam, _conn))
                {
                    // Set parameters
                    cmd.Parameters.AddWithValue("@quizName", quiz.QuizName);

                    // Update local person
                    quiz.QuizId = (int)cmd.ExecuteScalar(); //Returns the identity of the new tuple/record
                    return quiz;
                }
            }
            finally
            {
                if(_conn != null)
                    _conn.Close();
            }
        }

        public Quiz GetQuiz(int id)
        {
            SqlDataReader rdr = null;

            try
            {
                _conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM Quiz WHERE (QuizId = @quizId)", _conn);

                cmd.Parameters.AddWithValue("@quizId", id);

                rdr = cmd.ExecuteReader();

                // If no data is found
                if (!rdr.Read()) return null;

                Console.WriteLine(rdr[0]);
                return new Quiz
                {
                    QuizId = (int)rdr["QuizId"],
                    QuizName = (string)rdr["QuizName"],
                };
            }
            finally
            {
                if (rdr != null)
                    rdr.Close();

                if (_conn != null)
                    _conn.Close();
            }
        }
        #endregion // QuizTable

        #region QuestionTable
        public Question InsertQuestion(Question question)
        {
            try
            {
                _conn.Open();

                string insertStringParam;
                insertStringParam = @"INSERT INTO [Question] (Text)
                                                    OUTPUT INSERTED.QuestionId
                                                    VALUES (@text)";

                using (SqlCommand cmd = new SqlCommand(insertStringParam, _conn))
                {
                    // Set parameters
                    cmd.Parameters.AddWithValue("@Text", question.Text);

                    // Update local person
                    question.QuestionId = (int)cmd.ExecuteScalar(); //Returns the identity of the new tuple/record
                    return question;
                }
            }
            finally
            {
                if (_conn != null)
                    _conn.Close();
            }
        }

        public Question GetQuestion(int id)
        {
            SqlDataReader rdr = null;

            try
            {
                _conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM Question WHERE (QuestionId = @questionId)", _conn);

                cmd.Parameters.AddWithValue("@questionId", id);

                rdr = cmd.ExecuteReader();

                // If no data is found
                if (!rdr.Read()) return null;

                Console.WriteLine(rdr[0]);
                return new Question
                {
                    Text = (string)rdr["Text"],
                    Quiz = (Quiz)rdr["QuizId"],
                };
            }
            finally
            {
                if (rdr != null)
                    rdr.Close();

                if (_conn != null)
                    _conn.Close();
            }
        }


        #endregion // QuestionTable

    }
}
