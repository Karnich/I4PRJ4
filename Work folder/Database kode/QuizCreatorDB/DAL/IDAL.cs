using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Model
{
    public interface IDAL
    {
        #region Quiz CRUD
        Quiz InsertQuiz(Quiz q);
        void DeleteQuiz(int QuizId);
        void UpdateQuiz(Quiz q);
        Quiz GetQuiz(int quitId);
        #endregion // Quiz CRUD

        #region Question CRUD
        Question InsertQuestion(Question q);
        void DeleteQuestion(int QuestionId);
        void UpdateQuestion(Question q);
        Question GetQuestion(int QuestionId);
        #endregion // Question CRUD

        #region Answer CRUD
        Answer InsertAnswer(Answer a);
        void DeleteAnswer(int AnswerId);
        void UpdateAnswer(Answer a);
        Answer GetAnswer(int AnswerId);
        #endregion // Answer CRUD

        #region QuizTagRelation CRUD
        void InsertRelation(QuizTagRelation relation);
        void DeleteRelation(QuizTagRelation relation);
        void UpdateRelation(QuizTagRelation prevRelation, QuizTagRelation newRelation);
        QuizTagRelation GetRelationThroughTag(int tagId);
        QuizTagRelation GetRelationThroughQuiz(int QuizId);
        #endregion // QuizTagRelation CRUD

        #region Tag CRD
        Tag InsertTag(Tag t);
        void DeleteTag(int TagId);
        void UpdateTag(Tag t);
        Tag GetTagById(int tagId);
        Tag GetTagByName(string name);
        #endregion // Tag CRD
    }
}
