using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.Domain.Entities;

namespace GameStore.DAL.Context
{
    public class GameStoreContextInitializer : CreateDatabaseIfNotExists<GameStoreContext>
    {
        protected override void Seed(GameStoreContext context)
        {
            Genre rts = new Genre() { Name = "RTS", IsSubGenre = true };
            Genre tbs = new Genre() { Name = "TBS", IsSubGenre = true };
            Genre rally = new Genre() { Name = "Rally", IsSubGenre = true };
            Genre arcade = new Genre() { Name = "Arcade", IsSubGenre = true };
            Genre formula = new Genre() { Name = "Formula", IsSubGenre = true };
            Genre offRoad = new Genre() { Name = "Off-road", IsSubGenre = true };
            Genre fps = new Genre() { Name = "FPS", IsSubGenre = true };
            Genre tps = new Genre() { Name = "TPS", IsSubGenre = true };
            Genre subMisc = new Genre() { Name = "Misc(Sub-genre)" };
            context.Genres.AddRange(new List<Genre>() { rts, tbs, rally, arcade, formula, offRoad, fps, tps, subMisc });
            context.SaveChanges();
            Genre strategy = new Genre() { Name = "Strategy", SubGenres = new List<Genre>() { rts, tbs } };
            Genre rpg = new Genre() { Name = "RPG" };
            Genre races = new Genre() { Name = "Races", SubGenres = new List<Genre>() { rally, arcade, formula, offRoad } };
            Genre action = new Genre() { Name = "Action", SubGenres = new List<Genre>() { fps, tps, subMisc } };
            Genre adventure = new Genre() { Name = "Adventure" };
            Genre puzzleAndSkill = new Genre() { Name = "Puzzle&Skill" };
            Genre misc = new Genre() { Name = "Misc" };
            context.Genres.AddRange(new List<Genre>() { strategy, rpg, races, action, adventure, puzzleAndSkill, misc });
            base.Seed(context);
        }
    }
}
