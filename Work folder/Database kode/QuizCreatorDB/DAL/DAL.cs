using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class DAL : IDAL
    {
        #region Constructor and attributes
        private SqlConnection _conn;

        /// <summary>
        /// Constructor
        /// </summary>
        public DAL()
        {                              
            _conn = new SqlConnection(@"Data Source=(localdb)\ProjectsV12;Initial Catalog=QuizCreatorDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False");
            //_conn = new SqlConnection(@"Data Source=10.29.0.29;Integrated Security=False;User ID=F15I4PRJ4Gr3;Password=F15I4PRJ4Gr3;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");
        }
        #endregion // constructor

        /// <summary>
        /// Deletes all the data in the db and resets the seed
        /// </summary>
        /// <remarks>Mostly for testing purposes</remarks>
        public void Reset()
        {
            try
            {
                _conn.Open();

                string deleteString;
                deleteString = @"TRUNCATE TABLE Answer ";
                using (SqlCommand cmd = new SqlCommand(deleteString, _conn)) { cmd.ExecuteNonQuery(); }

                deleteString = @"TRUNCATE TABLE QuizTagRelation ";
                using (SqlCommand cmd = new SqlCommand(deleteString, _conn)) { cmd.ExecuteNonQuery(); }

                deleteString = @"DELETE FROM Question ";
                using (SqlCommand cmd = new SqlCommand(deleteString, _conn)) { cmd.ExecuteNonQuery(); }

                deleteString = @"DBCC CHECKIDENT (Question, RESEED, 0)";
                using (SqlCommand cmd = new SqlCommand(deleteString, _conn)) { cmd.ExecuteScalar(); }

                deleteString = @"DELETE FROM Quiz ";
                using (SqlCommand cmd = new SqlCommand(deleteString, _conn)) { cmd.ExecuteNonQuery(); }

                deleteString = @"DBCC CHECKIDENT (Quiz, RESEED, 0)";
                using (SqlCommand cmd = new SqlCommand(deleteString, _conn)) { cmd.ExecuteScalar(); }

                deleteString = @"DELETE FROM Tag ";
                using (SqlCommand cmd = new SqlCommand(deleteString, _conn)) { cmd.ExecuteNonQuery(); }

                deleteString = @"DBCC CHECKIDENT (Tag, RESEED, 0)";
                using (SqlCommand cmd = new SqlCommand(deleteString, _conn)) { cmd.ExecuteScalar(); }
            }
            finally
            {
                if (_conn != null)
                    _conn.Close();
            }
        }

        #region QuizTable

        /// <summary>
        /// Inserts the Quiz given in the parameter list, into the db
        /// </summary>
        /// <returns>The same Quiz but with the QuizId property initialized</returns>
        public Quiz InsertQuiz(Quiz quiz)
        {
            if (quiz == null)
                throw new ArgumentNullException("quiz");

            try
            {
                _conn.Open();

                string insertStringParam;
                insertStringParam = @"INSERT INTO [Quiz] (QuizName)
                                        OUTPUT INSERTED.QuizId
                                        VALUES (@quizName)";

//                insertStringParam = @"INSERT INTO [Quiz] (QuizName) 
//                                                    VALUES (@quizName)";

                //insertStringParam = @"INSERT INTO [Quiz] (QuizName) VALUES (@quizName); "
                                   // + "SELECT CAST(scope_identity() AS int)";

                using (SqlCommand cmd = new SqlCommand(insertStringParam, _conn))
                {
                    // Set parameters
                    cmd.Parameters.AddWithValue("@quizName", quiz.QuizName);
                    //var result = (int)cmd.ExecuteScalar();
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

        /// <summary>
        /// Finds the Quiz with the QuizId matching the one given in the parameter list
        /// </summary>
        /// <returns>The found Quiz</returns>
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

        /// <summary>
        /// Removes the Quiz in the db, with the given QuizId
        /// </summary>
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

        /// <summary>
        /// Finds the Quiz with the same QuizId as the one in the parameter list, and Updates it with the given Quiz from the parameter list
        /// </summary>
        public void UpdateQuiz(Quiz q)
        {
            try
            {
                // Open the connection
                _conn.Open();

                // prepare command string
                const string updateString = @"  UPDATE Quiz
                                                SET QuizName   = @quizName
                                                WHERE QuizId = @quizId";

                using (SqlCommand cmd = new SqlCommand(updateString, _conn))
                {
                    // Get your parameters ready 
                    cmd.Parameters.AddWithValue("@quizName", q.QuizName);
                    cmd.Parameters.AddWithValue("@quizId", q.QuizId);

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
        /// <summary>
        /// Inserts the Question given in the parameter list, into the db
        /// </summary>
        /// <returns>The same Question but with the QuestionId property initialized</returns>
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

        /// <summary>
        /// Finds the Question with the QuestionId matching the one given in the parameter list
        /// </summary>
        /// <returns>The found Question</returns>
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

                return new Question
                {
                    QuestionId = (int)rdr["QuestionId"],
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

        /// <summary>
        /// Removes the Question in the db, with the given QuestionId
        /// </summary>
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

        /// <summary>
        /// Finds the Question with the same QuestionId as the one in the parameter list, and Updates it with the given Question from the parameter list
        /// </summary>
        public void UpdateQuestion(Question q)
        {
            try
            {
                // Open the connection
                _conn.Open();

                // prepare command string
                const string updateString = @"  UPDATE Question
                                                SET Question   = @text,
                                                    QuizId  = @quizId
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

        /// <summary>
        /// Inserts the Answer given in the parameter list, into the db
        /// </summary>
        /// <returns>The same Answer but with the AnswerId property initialized</returns>
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

        /// <summary>
        /// Removes the Answer in the db, with the given AnswerId
        /// </summary>
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

        /// <summary>
        /// Finds the Answer with the same AnswerId as the one in the parameter list, and Updates it with the given Answer from the parameter list
        /// </summary>
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
                                                    QuestionId  = @questionId
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

        /// <summary>
        /// Finds the Answer with the AnswerId matching the one given in the parameter list
        /// </summary>
        /// <returns>The found Answer</returns>
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

                return new Answer
                {
                    AnswerId = (int)rdr["AnswerId"],
                    Text = (string)rdr["Answer"],
                    Correct = (byte)rdr["Correct"] > 0,
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
        /// <summary>
        /// Inserts the QuizTagRelation givne in the parameter list, into the db
        /// </summary>
        public void InsertRelation(QuizTagRelation relation)
        {
            try
            {
                _conn.Open();

                string insertStringParam;
                insertStringParam = @"INSERT INTO [QuizTagRelation] (TagId, QuizId)
                                                    VALUES (@tagId, @quizId)";

                using (SqlCommand cmd = new SqlCommand(insertStringParam, _conn))
                {
                    // Set parameters
                    cmd.Parameters.AddWithValue("@tagId", relation.TagId);
                    cmd.Parameters.AddWithValue("@quizId", relation.QuizId);
                    cmd.ExecuteScalar();
                }
            }
            finally
            {
                if (_conn != null)
                    _conn.Close();
            }
        }

        /// <summary>
        /// Removes the QuizTagRelation in the db, with the same tagId and quizId as the relation given in the parameter list
        /// </summary>
        public void DeleteRelation(QuizTagRelation relation)
        {
            try
            {
                _conn.Open();

                const string deleteString = @"DELETE FROM QuizTagRelation
                                                WHERE (Tagid = @tagId AND QuizId = @quizId)";

                using (SqlCommand cmd = new SqlCommand(deleteString, _conn))
                {
                    cmd.Parameters.AddWithValue("@tagId", relation.TagId);
                    cmd.Parameters.AddWithValue("@quizId", relation.QuizId);

                    cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                if (_conn != null)
                    _conn.Close();
            }
        }

        /// <summary>
        /// Finds the QuizTagRelation matching the first QuizTagRelation in the parameter list, and Updates it to match the second QuizTagRelation in the parameter list
        /// </summary>
        public void UpdateRelation(QuizTagRelation prevRelation, QuizTagRelation newRelation)
        {
            try
            {
                // Open the connection
                _conn.Open();

                // prepare command string
                const string updateString = @"  UPDATE QuizTagRelation
                                                SET TagId   = @newTagId,
                                                    QuizId = @newQuizId
                                                WHERE (QuizId = @prevQuizId) AND (TagId  = @prevTagId)";

                using (SqlCommand cmd = new SqlCommand(updateString, _conn))
                {
                    // Get your parameters ready 
                    cmd.Parameters.AddWithValue("@newTagId", newRelation.TagId);
                    cmd.Parameters.AddWithValue("@newQuizId", newRelation.QuizId);
                    cmd.Parameters.AddWithValue("@prevQuizId", prevRelation.QuizId);
                    cmd.Parameters.AddWithValue("@prevTagId", prevRelation.TagId);


                    cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                if (_conn != null)
                    _conn.Close();
            }
        }

        /// <summary>
        /// Finds the QuizTagRelation with the TagId matching the one given in the parameter list
        /// </summary>
        /// <returns>The found QuizTagRelation</returns>
        public QuizTagRelation GetRelationThroughTag(int tagId)
        {
            SqlDataReader rdr = null;

            try
            {
                _conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM QuizTagRelation WHERE (TagId = @tagId)", _conn);

                cmd.Parameters.AddWithValue("@tagId", tagId);

                rdr = cmd.ExecuteReader();

                // If no data is found
                if (!rdr.Read()) return null;

                return new QuizTagRelation
                {
                    QuizId = (int)rdr["QuizId"],
                    TagId = (int)rdr["TagId"],

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

        /// <summary>
        /// Finds the QuizTagRelation with the QuizId matching the one given in the parameter list
        /// </summary>
        /// <returns>The found QuizTagRelation</returns>
        public QuizTagRelation GetRelationThroughQuiz(int QuizId)
        {
            SqlDataReader rdr = null;

            try
            {
                _conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM QuizTagRelation WHERE (QuizId = @quizId)", _conn);

                cmd.Parameters.AddWithValue("@quizId", QuizId);

                rdr = cmd.ExecuteReader();

                // If no data is found
                if (!rdr.Read()) return null;

                return new QuizTagRelation
                {
                    QuizId = (int)rdr["QuizId"],
                    TagId = (int)rdr["TagId"],

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
        #endregion // QuizTagRelation CRUD

        #region Tag CRD
        /// <summary>
        /// Inserts the Tag given in the parameter list, into the db
        /// </summary>
        /// <returns>The same Tag but with the TagId property initialized</returns>
        public Tag InsertTag(Tag t)
        {
            try
            {
                _conn.Open();

                string insertStringParam;
                insertStringParam = @"INSERT INTO [Tag] (Tag)
                                                    OUTPUT INSERTED.TagId
                                                    VALUES (@text)";

                using (SqlCommand cmd = new SqlCommand(insertStringParam, _conn))
                {
                    // Set parameters
                    cmd.Parameters.AddWithValue("@text", t.Text);


                    t.TagId = (int)cmd.ExecuteScalar(); //Returns the identity of the new tuple/record
                    return t;
                }
            }
            finally
            {
                if (_conn != null)
                    _conn.Close();
            }
        }

        /// <summary>
        /// Removes the Tag in the db, with the given TagId
        /// </summary>
        public void DeleteTag(int TagId)
        {
            try
            {
                _conn.Open();

                const string deleteString = @"DELETE FROM Tag
                                                WHERE (TagId = @tagId)";

                using (SqlCommand cmd = new SqlCommand(deleteString, _conn))
                {
                    cmd.Parameters.AddWithValue("@tagId", TagId);

                    cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                if (_conn != null)
                    _conn.Close();
            }
        }

        /// <summary>
        /// Finds the Tag with the same TagId as the one in the parameter list, and Updates it with the given Tag from the parameter list
        /// </summary>
        public void UpdateTag(Tag t)
        {
            try
            {
                // Open the connection
                _conn.Open();

                // prepare command string
                const string updateString = @"  UPDATE Tag
                                                SET Tag   = @text
                                                WHERE TagId = @tagId";

                using (SqlCommand cmd = new SqlCommand(updateString, _conn))
                {
                    // Get your parameters ready 
                    cmd.Parameters.AddWithValue("@text", t.Text);
                    cmd.Parameters.AddWithValue("@tagId", t.TagId);

                    cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                if (_conn != null)
                    _conn.Close();
            }
        }

        /// <summary>
        /// Finds the Tag with the TagId matching the one given in the parameter list
        /// </summary>
        /// <returns>The found Tag</returns>
        public Tag GetTagById(int tagId)
        {
            SqlDataReader rdr = null;

            try
            {
                _conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM Tag WHERE (TagId = @tagId)", _conn);

                cmd.Parameters.AddWithValue("@tagId", tagId);

                rdr = cmd.ExecuteReader();

                // If no data is found
                if (!rdr.Read()) return null;

                return new Tag
                {
                    TagId = (int)rdr["TagId"],
                    Text = (string)rdr["Tag"]
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

        /// <summary>
        /// Finds the Tag with the name matching the one given in the parameter list
        /// </summary>
        /// <returns>The found Tag</returns>
        public Tag GetTagByName(string tag)
        {
            SqlDataReader rdr = null;

            try
            {
                _conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM Tag WHERE (Tag = @tag)", _conn);

                cmd.Parameters.AddWithValue("@tag", tag);

                rdr = cmd.ExecuteReader();

                // If no data is found
                if (!rdr.Read()) return null;

                return new Tag
                {
                    TagId = (int)rdr["TagId"],
                    Text = (string)rdr["Tag"]
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
        #endregion // Tag CRUD
    }
}
