using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.DAL.Entities;

namespace GameStore.DAL.Context
{
    public class GameStoreContextInitializer : CreateDatabaseIfNotExists<GameStoreContext>
    {
        protected override void Seed(GameStoreContext context)
        {
            Genre rts = new Genre { Name = "RTS" };
            Genre tbs = new Genre { Name = "TBS" };
            Genre rally = new Genre { Name = "Rally" };
            Genre arcade = new Genre { Name = "Arcade" };
            Genre formula = new Genre { Name = "Formula" };
            Genre offRoad = new Genre { Name = "Off-road" };
            Genre fps = new Genre { Name = "FPS" };
            Genre tps = new Genre { Name = "TPS" };
            Genre subMisc = new Genre { Name = "Misc(Sub-genre)" };
            context.Genres.AddRange(new List<Genre> { rts, tbs, rally, arcade, formula, offRoad, fps, tps, subMisc });
            context.SaveChanges();
            Genre strategy = new Genre { Name = "Strategy", ChildGenres = new List<Genre> { rts, tbs } };
            Genre rpg = new Genre { Name = "RPG" };
            Genre races = new Genre { Name = "Races", ChildGenres = new List<Genre> { rally, arcade, formula, offRoad } };
            Genre action = new Genre { Name = "Action", ChildGenres = new List<Genre> { fps, tps, subMisc } };
            Genre adventure = new Genre { Name = "Adventure" };
            Genre puzzleAndSkill = new Genre { Name = "Puzzle&Skill" };
            Genre misc = new Genre { Name = "Misc" };
            context.Genres.AddRange(new List<Genre> { strategy, rpg, races, action, adventure, puzzleAndSkill, misc });
            context.SaveChanges();
            PlatformType mobile = new PlatformType { Type = "Mobile" };
            PlatformType browser = new PlatformType { Type = "Browser" };
            PlatformType desktop = new PlatformType { Type = "Desktop" };
            PlatformType console = new PlatformType { Type = "Console" };
            context.PlatformTypes.AddRange(new List<PlatformType> { mobile, browser, desktop, console });
            context.SaveChanges();
            Game callOfDuty = new Game { Name = "Call Of Duty", Description = "You can shoot some enemies", Key = "COD123" };
            callOfDuty.Genres.Add(action);
            Game burnout = new Game
            {
                Name = "Burnout Paradise",
                Key = "Burnout123",
                Description = "Drive a car and wreck it"
            };

            burnout.Genres.Add(races);
            context.Games.AddRange(new List<Game> {callOfDuty, burnout});
            context.SaveChanges();
            Comment firstComment = new Comment { Name = "Josh123", Game = callOfDuty, Body = "Cool game, I like it" };
            Comment secondComment = new Comment { Name = "Drake321", Game = callOfDuty, Body = "Nice game, but not as good as Quake" };
            Comment thirdComment = new Comment
            {
                Name = "Jake555",
                Game = callOfDuty,
                Body = "You know nothing, this game is bad as hell",
                ParentComment = firstComment
            };

            Comment fourthComment = new Comment
            {
                Name = "Josh213",
                Game = callOfDuty,
                ParentComment = thirdComment,
                Body = "No, you know nothing. It is cool"
            };

            context.Comments.AddRange(new List<Comment> {firstComment, secondComment, thirdComment, fourthComment});
            context.SaveChanges();
            base.Seed(context);
        }
    }
}