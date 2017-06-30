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
            var rts = new Genre { Name = "RTS" };
            var tbs = new Genre { Name = "TBS" };
            var rally = new Genre { Name = "Rally" };
            var arcade = new Genre { Name = "Arcade" };
            var formula = new Genre { Name = "Formula" };
            var offRoad = new Genre { Name = "Off-road" };
            var fps = new Genre { Name = "FPS" };
            var tps = new Genre { Name = "TPS" };
            var subMisc = new Genre { Name = "Misc(Sub-genre)" };
            context.Genres.AddRange(new List<Genre> { rts, tbs, rally, arcade, formula, offRoad, fps, tps, subMisc });
            context.SaveChanges();
            var strategy = new Genre { Name = "Strategy", ChildGenres = new List<Genre> { rts, tbs } };
            var rpg = new Genre { Name = "RPG" };
            var races = new Genre { Name = "Races", ChildGenres = new List<Genre> { rally, arcade, formula, offRoad } };
            var action = new Genre { Name = "Action", ChildGenres = new List<Genre> { fps, tps, subMisc } };
            var adventure = new Genre { Name = "Adventure" };
            var puzzleAndSkill = new Genre { Name = "Puzzle&Skill" };
            var misc = new Genre { Name = "Misc" };
            context.Genres.AddRange(new List<Genre> { strategy, rpg, races, action, adventure, puzzleAndSkill, misc });
            context.SaveChanges();
            var mobile = new PlatformType { Type = "Mobile" };
            var browser = new PlatformType { Type = "Browser" };
            var desktop = new PlatformType { Type = "Desktop" };
            var console = new PlatformType { Type = "Console" };
            context.PlatformTypes.AddRange(new List<PlatformType> { mobile, browser, desktop, console });
            context.SaveChanges();
            var callOfDuty = new Game { Name = "Call Of Duty", Description = "You can shoot some enemies", Key = "COD123" };
            callOfDuty.Genres.Add(action);
            var burnout = new Game
            {
                Name = "Burnout Paradise",
                Key = "Burnout123",
                Description = "Drive a car and wreck it"
            };

            burnout.Genres.Add(races);
            context.Games.AddRange(new List<Game> {callOfDuty, burnout});
            context.SaveChanges();
            var firstComment = new Comment { Name = "Josh123", Game = callOfDuty, Body = "Cool game, I like it" };
            var secondComment = new Comment { Name = "Drake321", Game = callOfDuty, Body = "Nice game, but not as good as Quake" };
            var thirdComment = new Comment
            {
                Name = "Jake555",
                Game = callOfDuty,
                Body = "You know nothing, this game is bad as hell",
                ParentComment = firstComment
            };

            var fourthComment = new Comment
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