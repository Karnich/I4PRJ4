using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class DAL : IDAL
    {
        #region Constructor and attributes
        private SqlConnection _conn;

        public DAL()
        {
            _conn =
                 new SqlConnection(@"Data Source=10.29.0.29;Integrated Security=False;User ID=F15I4PRJ4Gr3;Password=F15I4PRJ4Gr3;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");
        }
        #endregion // constructor

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

        public void DeleteQuiz(int QuizId)
        {
            try
            {
                _conn.Open();

                const string deleteString = @"DELETE FROM Quiz
                                                WHERE (QuizId = @quizId)";

                using (SqlCommand cmd = new SqlCommand(deleteString, _conn))
                {
                    cmd.Parameters.AddWithValue("@quizId", QuizId);

                    cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                if (_conn != null)
                    _conn.Close();
            }
        }

        public void UpdateQuiz(Quiz q)
        {
            try
            {
                // Open the connection
                _conn.Open();

                // prepare command string
                const string updateString = @"  UPDATE Quiz
                                                SET QuizName   = @quizName,
                                                WHERE QuizId = @quizId";

                using (SqlCommand cmd = new SqlCommand(updateString, _conn))
                {
                    // Get your parameters ready 
                    cmd.Parameters.AddWithValue("@quizName", q.QuizName);
                    cmd.Parameters.AddWithValue("@QuizId", q.QuizId);

                    cmd.ExecuteNonQuery();
                }
            }
            finally
            {
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
                insertStringParam = @"INSERT INTO [Question] (Question, QuizId)
                                                    OUTPUT INSERTED.QuestionId
                                                    VALUES (@text, @quizId)";

                using (SqlCommand cmd = new SqlCommand(insertStringParam, _conn))
                {
                    // Set parameters
                    cmd.Parameters.AddWithValue("@Text", question.Text);
                    cmd.Parameters.AddWithValue("@QuizId", question.QuizId);

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
                    Text = (string)rdr["Question"],
                    QuizId = (int)rdr["QuizId"],
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

        public void DeleteQuestion(int  QuestionId)
        {
            try
            {
                _conn.Open();

                const string deleteString = @"DELETE FROM Question
                                                WHERE (QuestionId = @questionId)";

                using (SqlCommand cmd = new SqlCommand(deleteString, _conn))
                {
                    cmd.Parameters.AddWithValue("@questionId", QuestionId);

                    cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                if (_conn != null)
                    _conn.Close();
            }
        }

        public void UpdateQuestion(Question q)
        {
            try
            {
                // Open the connection
                _conn.Open();

                // prepare command string
                const string updateString = @"  UPDATE Question
                                                SET Question   = @text,
                                                    QuizId  = @quizId,
                                                WHERE QuestionId = @questionId";

                using (SqlCommand cmd = new SqlCommand(updateString, _conn))
                {
                    // Get your parameters ready 
                    cmd.Parameters.AddWithValue("@text", q.Text);
                    cmd.Parameters.AddWithValue("@quizId", q.QuizId);
                    cmd.Parameters.AddWithValue("@questionId", q.QuestionId);

                    cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                if (_conn != null)
                    _conn.Close();
            }
        }

        #endregion // QuestionTable

        #region Answer CRUD 

        public Answer InsertAnswer(Answer a)
        {
            try
            {
                _conn.Open();

                string insertStringParam;
                insertStringParam = @"INSERT INTO [Answer] (Answer, Correct, QuestionId)
                                                    OUTPUT INSERTED.AnswerId
                                                    VALUES (@text, @correct, @questionId)";

                using (SqlCommand cmd = new SqlCommand(insertStringParam, _conn))
                {
                    // Set parameters
                    cmd.Parameters.AddWithValue("@text", a.Text);
                    cmd.Parameters.AddWithValue("@correct", a.Correct);
                    cmd.Parameters.AddWithValue("@questionId", a.QuestionId);

                    // Update local person
                    a.AnswerId = (int)cmd.ExecuteScalar(); //Returns the identity of the new tuple/record
                    return a;
                }
            }
            finally
            {
                if (_conn != null)
                    _conn.Close();
            }
        }

        public void DeleteAnswer(int AnswerId)
        {
            try
            {
                _conn.Open();

                const string deleteString = @"DELETE FROM Answer
                                                WHERE (AnswerId = @answerId)";

                using (SqlCommand cmd = new SqlCommand(deleteString, _conn))
                {
                    cmd.Parameters.AddWithValue("@answerId", AnswerId);

                    cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                if (_conn != null)
                    _conn.Close();
            }
        }

        public void UpdateAnswer(Answer a)
        {
            try
            {
                // Open the connection
                _conn.Open();

                // prepare command string
                const string updateString = @"  UPDATE Answer
                                                SET Answer   = @text,
                                                    Correct = @correct,
                                                    QuestionId  = @questionId,
                                                WHERE AnswerId = @answerId";

                using (SqlCommand cmd = new SqlCommand(updateString, _conn))
                {
                    // Get your parameters ready 
                    cmd.Parameters.AddWithValue("@text", a.Text);
                    cmd.Parameters.AddWithValue("@correct", a.Correct);
                    cmd.Parameters.AddWithValue("@questionId", a.QuestionId);
                    cmd.Parameters.AddWithValue("@answerId", a.AnswerId);
                    

                    cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                if (_conn != null)
                    _conn.Close();
            }
        }

        public Answer GetAnswer(int AnswerId)
        {
            SqlDataReader rdr = null;

            try
            {
                _conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM Answer WHERE (AnswerId = @answerId)", _conn);

                cmd.Parameters.AddWithValue("@answerId", AnswerId);

                rdr = cmd.ExecuteReader();

                // If no data is found
                if (!rdr.Read()) return null;

                Console.WriteLine(rdr[0]);
                return new Answer
                {
                    AnswerId = (int)rdr["AnswerId"],
                    Text = (string)rdr["Answer"],
                    Correct = (bool)rdr["Correct"],
                    QuestionId = (int)rdr["QuestionId"]            
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

        #endregion // Answer CRUD

        #region QuizTagRelation CRUD
        public QuizTagRelation InsertRelation(QuizTagRelation relation)
        {
            throw new NotImplementedException();
        }

        public void DeleteRelation(QuizTagRelation relation)
        {
            throw new NotImplementedException();
        }

        public void UpdateRelation(QuizTagRelation relation)
        {
            throw new NotImplementedException();
        }

        public QuizTagRelation GetRelationThroughTag(int tagId)
        {
            throw new NotImplementedException();
        }

        public QuizTagRelation GetRelationThroughQuiz(int QuizId)
        {
            throw new NotImplementedException();
        }
        #endregion // QuizTagRelation CRUD

        #region Tag CRD
        public Tag InsertTag(Tag t)
        {
            throw new NotImplementedException();
        }

        public void DeleteTag(int TagId)
        {
            throw new NotImplementedException();
        }

        public Tag GetTag(int tagId)
        {
            throw new NotImplementedException();
        }
        #endregion // Tag CRD
    }
}
