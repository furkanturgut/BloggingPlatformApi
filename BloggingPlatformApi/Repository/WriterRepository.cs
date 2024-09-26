using BloggingPlatformApi.Data;
using BloggingPlatformApi.Interface;
using BloggingPlatformApi.Models;
using System.Security.Cryptography.X509Certificates;

namespace BloggingPlatformApi.Repository
{
    public class WriterRepository : IWriterRepository
    {
        private readonly DataContext _context;

        public WriterRepository(DataContext context)
        {
            _context = context;
        }
        public ICollection<Article> GetArticleByWriter(int WriterId)
        {
            return _context.Articles.Where(aw=> aw.Writer.Id == WriterId).ToList();
        }

        public Writer GetArticleWriter(int ArticleId)
        {
            return _context.Articles.Where(a => a.Id == ArticleId).Select(a => a.Writer).FirstOrDefault();
        }

        public Writer GetWriter(int WriterId)
        {
            return _context.Writers.FirstOrDefault(w => w.Id == WriterId);
        }

        public bool WriterExists(int WriterId)
        {
            return _context.Writers.Any(w => w.Id == WriterId);
        }

        public ICollection<Writer> GetWriters()
        {
            return _context.Writers.OrderBy(w => w.Id).ToList();
        }

        public bool CreateWriter(Writer writer)
        {
            _context.Add(writer);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateWriter(Writer writer)
        {
            _context.Update(writer);
            return Save();
        }
    }
}
