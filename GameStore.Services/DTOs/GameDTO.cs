using System.Collections.Generic;

namespace GameStore.Services.DTOs
{
    public class GameDto
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public short UnitsInStock { get; set; }
        public bool Discontinued { get; set; }
        public ICollection<CommentDto> Comments { get; set; }
        public ICollection<GenreDto> Genres { get; set; }
        public ICollection<PlatformTypeDto> PlatformTypes { get; set; }
        public bool IsDeleted { get; set; }

        public GameDto()
        {
            Comments = new List<CommentDto>();
            Genres = new List<GenreDto>();
            PlatformTypes = new List<PlatformTypeDto>();
        }
    }
}
