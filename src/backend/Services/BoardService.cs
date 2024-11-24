using OurForum.Backend.Entities;
using OurForum.Backend.Utility;

namespace OurForum.Backend.Services;

public interface IBoardService
{
    public IEnumerable<Board> GetPublic();
}

public class BoardService(DatabaseContext context) : IBoardService
{
    private readonly DatabaseContext _context = context;

    public IEnumerable<Board> GetPublic()
    {
        return _context.Boards.Where(x => x.Visibility == (int)Board.VisibilitySetting.Public);
    }

    public Board? Create(Board b)
    {
        _context.Boards.Add(b);
        _context.SaveChanges();
        return _context.Boards.FirstOrDefault(b);
    }
}
