using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnStage.Business.Abstract;
using System.Web.Mvc;
using OnStage.Entities;

namespace OnStage.Presentation.CompositionRoot.Infrastructure
{
    public class DefaultDataCreator
    {

        private static IShowHandler showHandler = DependencyResolver.Current.GetService<IShowHandler>();

        public static void CreateDefaultData()
        {
            if (showHandler.GetAllShows().Count() == 0)
            {
                CreateBasicShow();
            }
        }

        private static void CreateBasicShow()
        {
            var book1cue1 = Cue.CreateCue(0, "1", "Book 1 Cue 1", "", 0); var book1cue2 = Cue.CreateCue(0, "2", "Book 1 Cue 2", "", 750);
            var book2cue1 = Cue.CreateCue(0, "1", "Book 2 Cue 1", "", 0); var book2cue2 = Cue.CreateCue(0, "2", "Book 2 Cue 2", "", 750);
            var book3cue1 = Cue.CreateCue(0, "1", "Book 3 Cue 1", "", 0); var book3cue2 = Cue.CreateCue(0, "2", "Book 3 Cue 2", "", 750);
            var book4cue1 = Cue.CreateCue(0, "1", "Book 4 Cue 1", "", 0); var book4cue2 = Cue.CreateCue(0, "2", "Book 4 Cue 2", "", 750);

            var book1 = CueBook.CreateCueBook(0, "Book 1", "A"); book1.Cues.Add(book1cue1); book1.Cues.Add(book1cue2);
            var book2 = CueBook.CreateCueBook(0, "Book 2", "B"); book2.Cues.Add(book2cue1); book2.Cues.Add(book2cue2);
            var book3 = CueBook.CreateCueBook(0, "Book 3", "C"); book3.Cues.Add(book3cue1); book3.Cues.Add(book3cue2);
            var book4 = CueBook.CreateCueBook(0, "Book 4", "D"); book4.Cues.Add(book4cue1); book4.Cues.Add(book4cue2);

            var smbook1cue1 = CueGroup.CreateCueGroup(0, "1", "Cue 1", "This is the first cue", 0);
            smbook1cue1.Cues.Add(book1cue1);
            smbook1cue1.Cues.Add(book2cue1);
            smbook1cue1.Cues.Add(book3cue1);
            smbook1cue1.Cues.Add(book4cue1);

            var smbook1cue2 = CueGroup.CreateCueGroup(0, "2", "Cue 2", "This is the second cue", 750);
            smbook1cue2.Cues.Add(book1cue2);
            smbook1cue2.Cues.Add(book2cue2);
            smbook1cue2.Cues.Add(book3cue2);
            smbook1cue2.Cues.Add(book4cue2);

            var smbook1 = StageManagerCueBook.CreateStageManagerCueBook(0); smbook1.Cues.Add(smbook1cue1); smbook1.Cues.Add(smbook1cue2);

            var show = Show.CreateShow(0, "Test Show", "This is the sample show.");
            show.CueBooks.Add(book1);
            show.CueBooks.Add(book2);
            show.CueBooks.Add(book3);
            show.CueBooks.Add(book4);
            show.StageManagerCueBook = smbook1;
            show.Script = CreateBasicScript();

            showHandler.AddShow(show);
        }

        private static Script CreateBasicScript()
        {
            var encoding = System.Text.Encoding.GetEncoding("UTF-8");
            var text = Hamlet.Text;
            var bytes = encoding.GetBytes(text);

            return Script.CreateScript(0, bytes, "text/plain", "UTF-8");
        }

    }
}